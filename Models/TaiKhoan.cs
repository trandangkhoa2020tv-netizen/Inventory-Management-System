namespace QuanLyKhoHang.Models
{
    public class TaiKhoan
    {
        public int MaTaiKhoan { get; set; }
        public int MaNhanVien { get; set; }
        public string TenTaiKhoan { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string VaiTro { get; set; } = string.Empty; // Ví dụ: Admin, NhanVien
    }
}