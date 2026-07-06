using System.Diagnostics;
using System.Net.Http;

namespace QuanLyKhoHang.ApiClients
{
    internal static class ApiServerLauncher
    {
        private static Process _startedProcess;

        public static void EnsureStarted()
        {
            ApiClientSettings settings = ApiClientSettings.Load();
            if (IsHealthy(settings.BaseUrl))
            {
                return;
            }

            ProcessStartInfo startInfo = CreateStartInfo();
            _startedProcess = Process.Start(startInfo);

            for (int i = 0; i < 30; i++)
            {
                Thread.Sleep(300);
                if (IsHealthy(settings.BaseUrl))
                {
                    return;
                }

                if (_startedProcess != null && _startedProcess.HasExited)
                {
                    break;
                }
            }

            throw new InvalidOperationException("Khong khoi dong duoc backend QuanLyKhoHang.Api. Hay build project API va kiem tra cau hinh database.");
        }

        public static void StopIfStartedByApp()
        {
            try
            {
                if (_startedProcess != null && !_startedProcess.HasExited)
                {
                    _startedProcess.Kill(entireProcessTree: true);
                    _startedProcess.Dispose();
                }
            }
            catch
            {
                // Khong chan viec thoat WinForms neu API da tu dung.
            }
        }

        private static bool IsHealthy(string baseUrl)
        {
            try
            {
                using HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };
                Uri healthUrl = new Uri(new Uri(EnsureTrailingSlash(baseUrl)), "api/health");
                using HttpResponseMessage response = client.GetAsync(healthUrl).GetAwaiter().GetResult();
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private static ProcessStartInfo CreateStartInfo()
        {
            string apiDirectory = FindApiDirectory();
            string debugDll = Path.Combine(apiDirectory, "bin", "Debug", "net10.0", "QuanLyKhoHang.Api.dll");
            string releaseDll = Path.Combine(apiDirectory, "bin", "Release", "net10.0", "QuanLyKhoHang.Api.dll");
            string projectFile = Path.Combine(apiDirectory, "QuanLyKhoHang.Api.csproj");

            if (File.Exists(debugDll))
            {
                return CreateDotnetStartInfo(apiDirectory, $"\"{debugDll}\"");
            }

            if (File.Exists(releaseDll))
            {
                return CreateDotnetStartInfo(apiDirectory, $"\"{releaseDll}\"");
            }

            if (File.Exists(projectFile))
            {
                return CreateDotnetStartInfo(apiDirectory, $"run --project \"{projectFile}\"");
            }

            throw new FileNotFoundException("Khong tim thay backend QuanLyKhoHang.Api.");
        }

        private static ProcessStartInfo CreateDotnetStartInfo(string workingDirectory, string arguments)
        {
            return new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
                UseShellExecute = false,
                CreateNoWindow = true
            };
        }

        private static string FindApiDirectory()
        {
            DirectoryInfo directory = new DirectoryInfo(AppContext.BaseDirectory);
            while (directory != null)
            {
                string apiDirectory = Path.Combine(directory.FullName, "QuanLyKhoHang.Api");
                if (Directory.Exists(apiDirectory))
                {
                    return apiDirectory;
                }

                directory = directory.Parent;
            }

            string localApiDirectory = Path.Combine(AppContext.BaseDirectory, "QuanLyKhoHang.Api");
            if (Directory.Exists(localApiDirectory))
            {
                return localApiDirectory;
            }

            throw new DirectoryNotFoundException("Khong tim thay thu muc QuanLyKhoHang.Api.");
        }

        private static string EnsureTrailingSlash(string value)
        {
            return value.EndsWith("/", StringComparison.Ordinal) ? value : value + "/";
        }
    }
}
