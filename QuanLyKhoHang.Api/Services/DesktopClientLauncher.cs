using System.Diagnostics;

namespace QuanLyKhoHang.ApiServer.Services;

/// <summary>
/// Lớp hỗ trợ tự mở ứng dụng WinForms khi backend API được chạy trực tiếp.
/// </summary>
public static class DesktopClientLauncher
{
    /// <summary>
    /// Mở WinForms nếu API không được khởi động bởi chính ứng dụng desktop.
    /// </summary>
    public static void StartIfNeeded()
    {
        if (Environment.GetEnvironmentVariable("QUANLYKHOHANG_STARTED_BY_DESKTOP") == "1")
        {
            return;
        }

        string desktopExecutable = FindDesktopExecutable();
        if (string.IsNullOrEmpty(desktopExecutable))
        {
            return;
        }

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = desktopExecutable,
                WorkingDirectory = Path.GetDirectoryName(desktopExecutable) ?? AppContext.BaseDirectory,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Khong the mo giao dien QuanLyKhoHang: " + ex.Message);
        }
    }

    /// <summary>
    /// Tìm file QuanLyKhoHang.exe trong các thư mục build Debug hoặc Release.
    /// </summary>
    private static string FindDesktopExecutable()
    {
        DirectoryInfo directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory != null)
        {
            string desktopExecutable = Path.Combine(
                directory.FullName,
                "QuanLyKhoHang",
                "bin",
                "Debug",
                "net10.0-windows",
                "QuanLyKhoHang.exe");

            if (File.Exists(desktopExecutable))
            {
                return desktopExecutable;
            }

            desktopExecutable = Path.Combine(
                directory.FullName,
                "QuanLyKhoHang",
                "bin",
                "Release",
                "net10.0-windows",
                "QuanLyKhoHang.exe");

            if (File.Exists(desktopExecutable))
            {
                return desktopExecutable;
            }

            directory = directory.Parent;
        }

        return string.Empty;
    }
}
