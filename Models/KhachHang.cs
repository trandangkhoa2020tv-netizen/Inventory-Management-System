namespace QuanLyKhoHang.Models
{
    public class KhachHang
    {
        public int MaKhachHang { get; set; }
        public string TenKhachHang { get; set; } = string.Empty;
        public string DiaChiKH { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? GhiChu { get; set; }
    }
}