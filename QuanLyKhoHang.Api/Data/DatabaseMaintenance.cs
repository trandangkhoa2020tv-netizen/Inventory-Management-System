using Npgsql;

namespace QuanLyKhoHang.Data
{
    /// <summary>
    /// Các tác vụ bảo trì database cần chạy khi API khởi động.
    /// </summary>
    public static class DatabaseMaintenance
    {
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
