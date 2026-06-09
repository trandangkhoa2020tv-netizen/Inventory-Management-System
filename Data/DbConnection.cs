using Npgsql;

namespace QuanLyKhoHang.Data
{
    public class DbConnection
    {
        private static readonly string ConnectionString =
            "Host=localhost;Port=5432;Database=quanlyhanghoa;Username=postgres;Password=1234";

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (NpgsqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}