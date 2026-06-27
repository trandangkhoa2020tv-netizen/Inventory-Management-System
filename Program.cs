using System;
using System.Windows.Forms;
using QuanLyKhoHang.Api;
using QuanLyKhoHang.Forms;

namespace QuanLyKhoHang
{
    /// <summary>
    /// Điểm khởi động chính của chương trình.
    /// File này chịu trách nhiệm khởi tạo cấu hình WinForms, bật API nội bộ nếu được cấu hình,
    /// sau đó mở màn hình đăng nhập đầu tiên.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            // Khởi tạo cấu hình mặc định cho WinForms: font, DPI, visual style.
            ApplicationConfiguration.Initialize();

            // API chạy cùng vòng đời ứng dụng. Khi đóng WinForms, khối using sẽ tự dừng API.
            using (ApiHost apiHost = ApiHost.StartIfEnabled())
            {
                // Màn hình đầu tiên của hệ thống là form đăng nhập.
                Application.Run(new FrmDangNhap());
            }
        }
    }
}
