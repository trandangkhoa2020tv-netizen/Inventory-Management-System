using Npgsql;

namespace QuanLyKhoHang.Data
{
    /// <summary>
    /// Các tác vụ bảo trì database cần chạy khi API khởi động.
    /// </summary>
    public static class DatabaseMaintenance
    {
        private const string Admin123456Hash =
            "pbkdf2$100000$cWxraC1hZG1pbi0xMjM0NTYtc2FsdC12MQ==$D42Ak1eqSBNJflAoIDRvaAMOsz7NF5X7UQjvDwGr0xk=";

        private const string Staff123456Hash =
            "pbkdf2$100000$cWxraC1zdGFmZi1zYWx0LXYx$dS8VgTfJ0gRv1mu5WUKd36fm95MT4+wSG9lI5rlplZk=";

        /// <summary>
        /// Tao hoac cap nhat cac cot va bang bat buoc khi API khoi dong.
        /// </summary>
        public static void EnsureRuntimeSchema()
        {
            using NpgsqlConnection connection = DbConnection.GetConnection();
            connection.Open();

            ExecuteNonQuery(connection, @"
                ALTER TABLE IF EXISTS hanghoa ADD COLUMN IF NOT EXISTS is_deleted boolean DEFAULT false;
                UPDATE hanghoa SET is_deleted = false WHERE is_deleted IS NULL;
                ALTER TABLE IF EXISTS hanghoa ALTER COLUMN is_deleted SET DEFAULT false;
                ALTER TABLE IF EXISTS hanghoa ALTER COLUMN is_deleted SET NOT NULL;");

            ExecuteNonQuery(connection, @"
                ALTER TABLE IF EXISTS nhanvien ADD COLUMN IF NOT EXISTS is_deleted boolean DEFAULT false;
                UPDATE nhanvien SET is_deleted = false WHERE is_deleted IS NULL;
                ALTER TABLE IF EXISTS nhanvien ALTER COLUMN is_deleted SET DEFAULT false;
                ALTER TABLE IF EXISTS nhanvien ALTER COLUMN is_deleted SET NOT NULL;");

            ExecuteNonQuery(connection, @"
                CREATE TABLE IF NOT EXISTS auditlog
                (
                    ma_audit serial PRIMARY KEY,
                    ten_taikhoan varchar(100) NOT NULL,
                    vai_tro varchar(50) NOT NULL,
                    hanh_dong varchar(50) NOT NULL,
                    bang_du_lieu varchar(100) NOT NULL,
                    ma_ban_ghi integer NULL,
                    noi_dung text NULL,
                    ip_address varchar(100) NULL,
                    thoi_gian timestamp without time zone NOT NULL DEFAULT CURRENT_TIMESTAMP
                );");
        }

        /// <summary>
        /// Đồng bộ các sequence tự tăng với dữ liệu hiện có để tránh trùng khóa sau khi import dữ liệu mẫu.
        /// </summary>
        public static void EnsureSerialSequences()
        {
            using NpgsqlConnection connection = DbConnection.GetConnection();
            connection.Open();

            foreach ((string TableName, string ColumnName) column in GetSerialColumns(connection))
            {
                try
                {
                    using NpgsqlCommand command = new NpgsqlCommand(BuildSetValSql(column.TableName, column.ColumnName), connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Khong the dong bo sequence {column.TableName}.{column.ColumnName}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Dong bo mat khau mau sang PBKDF2 de database cu van dang nhap duoc sau khi tat plain text/SHA-256.
        /// </summary>
        public static void EnsureSampleAccountPasswords()
        {
            using NpgsqlConnection connection = DbConnection.GetConnection();
            connection.Open();

            UpdatePasswordIfCurrentValueMatches(
                connection,
                "admin",
                Admin123456Hash,
                new[]
                {
                    "admin123",
                    "123456",
                    "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9",
                    "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                    "pbkdf2$100000$cWxraC1hZG1pbi1zYWx0LXYx$lOctHBqPmdhFZLUgAMvE2r5aknrFc/20Khp5yLTyr+s="
                });

            UpdatePasswordIfCurrentValueMatches(
                connection,
                "nhanvienkho",
                Staff123456Hash,
                new[]
                {
                    "123456",
                    "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92"
                });

            UpdatePasswordIfCurrentValueMatches(
                connection,
                "nhanvienbanhang",
                Staff123456Hash,
                new[]
                {
                    "123456",
                    "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92"
                });
        }

        /// <summary>
        /// Cap nhat hash mat khau cho tai khoan mau neu database dang giu mat khau cu.
        /// </summary>
        private static void UpdatePasswordIfCurrentValueMatches(
            NpgsqlConnection connection,
            string username,
            string newPasswordHash,
            string[] oldPasswordValues)
        {
            const string sql = @"UPDATE taikhoan
                                 SET mat_khau = @newPasswordHash
                                 WHERE ten_taikhoan = @username
                                   AND mat_khau = ANY(@oldPasswordValues)";

            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@newPasswordHash", newPasswordHash);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@oldPasswordValues", oldPasswordValues);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Thuc thi mot lenh SQL khong tra du lieu tren ket noi dang mo.
        /// </summary>
        private static void ExecuteNonQuery(NpgsqlConnection connection, string sql)
        {
            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Lấy danh sách các cột dùng sequence nextval trong schema public.
        /// </summary>
        private static List<(string TableName, string ColumnName)> GetSerialColumns(NpgsqlConnection connection)
        {
            const string sql = @"SELECT table_name, column_name
                                 FROM information_schema.columns
                                 WHERE table_schema = 'public'
                                   AND column_default LIKE 'nextval(%'
                                 ORDER BY table_name, ordinal_position";

            List<(string TableName, string ColumnName)> columns = new List<(string TableName, string ColumnName)>();
            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            using NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                columns.Add((reader.GetString(0), reader.GetString(1)));
            }

            return columns;
        }

        /// <summary>
        /// Tạo câu SQL setval cho một sequence dựa trên giá trị lớn nhất hiện có của cột khóa.
        /// </summary>
        private static string BuildSetValSql(string tableName, string columnName)
        {
            return $@"SELECT setval(
                        pg_get_serial_sequence('public.{EscapeLiteral(tableName)}', '{EscapeLiteral(columnName)}'),
                        GREATEST(COALESCE((SELECT MAX({QuoteIdentifier(columnName)}) FROM {QuoteIdentifier(tableName)}), 0) + 1, 1),
                        false
                      );";
        }

        /// <summary>
        /// Bao tên bảng/cột bằng dấu nháy kép để dùng an toàn trong SQL identifier.
        /// </summary>
        private static string QuoteIdentifier(string value)
        {
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        /// <summary>
        /// Escape dấu nháy đơn trong chuỗi literal SQL.
        /// </summary>
        private static string EscapeLiteral(string value)
        {
            return value.Replace("'", "''");
        }
    }
}
