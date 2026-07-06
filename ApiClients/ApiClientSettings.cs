using System.Text.Json;

namespace QuanLyKhoHang.ApiClients
{
    public sealed class ApiClientSettings
    {
        public string BaseUrl { get; private set; } = "http://localhost:5088";
        public string ApiKey { get; private set; } = string.Empty;

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
