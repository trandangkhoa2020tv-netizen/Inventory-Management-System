using System;
using System.Windows.Forms;
using QuanLyKhoHang.Forms; // THÊM DÒNG NÀY

namespace QuanLyKhoHang
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Khởi chạy FrmDangNhap
            Application.Run(new FrmDangNhap()); 
        }
    }
}