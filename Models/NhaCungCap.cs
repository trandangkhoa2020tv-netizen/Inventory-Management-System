namespace QuanLyKhoHang.Models
{
    public class NhaCungCap
    {
        public int MaNhaCungCap { get; set; }
        public string TenNhaCungCap { get; set; } = string.Empty;
        public string DiaChiNCC { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? GhiChu { get; set; }
    }
}