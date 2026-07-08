using Npgsql;
using System;
using System.IO;
using System.Text.Json;

namespace QuanLyKhoHang.Data
{
    /// <summary>
    /// Quản lý chuỗi kết nối PostgreSQL dùng chung cho toàn bộ chương trình.
    /// Lớp này đọc cấu hình từ Config/appsettings.json, nếu không đọc được thì dùng cấu hình mặc định.
    /// </summary>
    public class DbConnection
    {
        // Chuỗi kết nối dự phòng để chương trình vẫn có thể chạy khi thiếu file cấu hình.
        private const string DefaultConnectionString =
            "Host=localhost;Port=5432;Database=quanlyhanghoa;Username=postgres;Password=1234";

        // Chuỗi kết nối cuối cùng được tính một lần khi ứng dụng khởi động.
        public static string ConnectionString { get; } = BuildConnectionString();

        /// <summary>
        /// Tạo đối tượng kết nối mới. Mỗi lần gọi sẽ trả về connection chưa mở.
        /// </summary>
        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }

        /// <summary>
        /// Kiểm tra nhanh database có kết nối được hay không. API /api/health dùng hàm này.
        /// </summary>
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

        /// <summary>
        /// Đọc file appsettings.json và dựng chuỗi kết nối PostgreSQL.
        /// Hàm cố tình bắt lỗi và trả về cấu hình mặc định để tránh crash khi thiếu file config.
        /// </summary>
        private static string BuildConnectionString()
        {
            try
            {
                string host = "localhost";
                int port = 5432;
                string database = "quanlyhanghoa";
                string username = "postgres";
                string password = "1234";

                string configPath = Path.Combine(AppContext.BaseDirectory, "Config", "appsettings.json");
                if (!File.Exists(configPath))
                {
                    configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
                }

                if (File.Exists(configPath))
                {
                    using FileStream stream = File.OpenRead(configPath);
                    using JsonDocument document = JsonDocument.Parse(stream);
                    JsonElement db = document.RootElement.GetProperty("DatabaseSettings");

                    host = GetString(db, "Host", host);
                    port = GetInt(db, "Port", port);
                    database = GetString(db, "Database", database);
                    username = GetString(db, "Username", username);
                    password = GetString(db, "Password", password);
                }

                host = GetEnvironmentString("QLKH_DB_HOST", host);
                port = GetEnvironmentInt("QLKH_DB_PORT", port);
                database = GetEnvironmentString("QLKH_DB_NAME", database);
                username = GetEnvironmentString("QLKH_DB_USER", username);
                password = GetEnvironmentString("QLKH_DB_PASSWORD", password);

                return new NpgsqlConnectionStringBuilder
                {
                    Host = host,
                    Port = port,
                    Database = database,
                    Username = username,
                    Password = password
                }.ConnectionString;
            }
            catch
            {
                return DefaultConnectionString;
            }
        }

        /// <summary>
        /// Lấy giá trị chuỗi trong JSON; nếu thiếu key thì dùng giá trị fallback.
        /// </summary>
        private static string GetString(JsonElement parent, string propertyName, string fallback)
        {
            return parent.TryGetProperty(propertyName, out JsonElement value)
                ? value.GetString() ?? fallback
                : fallback;
        }

        /// <summary>
        /// Lấy giá trị số nguyên trong JSON; nếu thiếu hoặc sai kiểu thì dùng giá trị fallback.
        /// </summary>
        private static int GetInt(JsonElement parent, string propertyName, int fallback)
        {
            return parent.TryGetProperty(propertyName, out JsonElement value) && value.TryGetInt32(out int result)
                ? result
                : fallback;
        }

        /// <summary>
        /// Lấy giá trị chuỗi từ biến môi trường; nếu không có thì giữ nguyên giá trị fallback.
        /// </summary>
        private static string GetEnvironmentString(string variableName, string fallback)
        {
            string value = Environment.GetEnvironmentVariable(variableName);
            return string.IsNullOrWhiteSpace(value) ? fallback : value;
        }

        /// <summary>
        /// Lấy giá trị số nguyên từ biến môi trường; nếu thiếu hoặc không parse được thì dùng fallback.
        /// </summary>
        private static int GetEnvironmentInt(string variableName, int fallback)
        {
            string value = Environment.GetEnvironmentVariable(variableName);
            return int.TryParse(value, out int result) ? result : fallback;
        }
    }
}
