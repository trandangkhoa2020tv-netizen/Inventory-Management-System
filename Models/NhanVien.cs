namespace QuanLyKhoHang.Models
{
    public class NhanVien
    {
        public int MaNhanVien { get; set; }
        public string TenNhanVien { get; set; } = string.Empty;
        public string DiaChiNV { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string ChucVu { get; set; } = string.Empty;
        public string? GhiChu { get; set; }
    }
}