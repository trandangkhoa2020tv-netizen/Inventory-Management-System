namespace QuanLyKhoHang.Models
{
    public static class UserSession
    {
        public static string TenTaiKhoan { get; set; } = "";
        public static string VaiTro { get; set; } = ""; // Sẽ giữ giá trị "Admin" hoặc "NhanVien"
    }
}