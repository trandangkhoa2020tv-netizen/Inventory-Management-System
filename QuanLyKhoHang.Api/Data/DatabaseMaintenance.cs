using Npgsql;

namespace QuanLyKhoHang.Data
{
    public static class DatabaseMaintenance
    {
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

        private static string BuildSetValSql(string tableName, string columnName)
        {
            return $@"SELECT setval(
                        pg_get_serial_sequence('public.{EscapeLiteral(tableName)}', '{EscapeLiteral(columnName)}'),
                        GREATEST(COALESCE((SELECT MAX({QuoteIdentifier(columnName)}) FROM {QuoteIdentifier(tableName)}), 0) + 1, 1),
                        false
                      );";
        }

        private static string QuoteIdentifier(string value)
        {
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        private static string EscapeLiteral(string value)
        {
            return value.Replace("'", "''");
        }
    }
}
