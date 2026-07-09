using System.Text.Json;

namespace QuanLyKhoHang.ApiClients
{
    /// <summary>
    /// Cấu hình kết nối từ ứng dụng WinForms tới backend API.
    /// Giá trị được đọc từ file appsettings trong thư mục chạy ứng dụng.
    /// </summary>
    public sealed class ApiClientSettings
    {
        /// <summary>Địa chỉ gốc của API server.</summary>
        public string BaseUrl { get; private set; } = "http://localhost:5088";

        /// <summary>Khóa API gửi kèm request nếu backend bật xác thực bằng API key.</summary>
        public string ApiKey { get; private set; } = string.Empty;

        /// <summary>
        /// Đọc cấu hình API client; nếu thiếu file hoặc lỗi định dạng thì dùng cấu hình mặc định.
        /// </summary>
        public static ApiClientSettings Load()
        {
            ApiClientSettings settings = new ApiClientSettings();

            try
            {
                string configPath = Path.Combine(AppContext.BaseDirectory, "Config", "appsettings.json");
                if (!File.Exists(configPath))
                {
                    configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
                }

                if (!File.Exists(configPath))
                {
                    return settings;
                }

                using FileStream stream = File.OpenRead(configPath);
                using JsonDocument document = JsonDocument.Parse(stream);
                if (!document.RootElement.TryGetProperty("ApiClientSettings", out JsonElement api))
                {
                    return settings;
                }

                if (api.TryGetProperty("BaseUrl", out JsonElement baseUrl))
                {
                    settings.BaseUrl = baseUrl.GetString() ?? settings.BaseUrl;
                }

                if (api.TryGetProperty("ApiKey", out JsonElement apiKey))
                {
                    settings.ApiKey = apiKey.GetString() ?? settings.ApiKey;
                }
            }
            catch
            {
                return settings;
            }

            return settings;
        }
    }
}
