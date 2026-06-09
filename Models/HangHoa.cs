namespace QuanLyKhoHang.Models
{
    public class HangHoa
    {
        public int MaHangHoa { get; set; }
        public string TenHangHoa { get; set; } = string.Empty;
        public int MaLoaiHang { get; set; }
        public int MaNhaCungCap { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
        public string DonViTinh { get; set; } = string.Empty;
        public string? GhiChu { get; set; }
    }
}