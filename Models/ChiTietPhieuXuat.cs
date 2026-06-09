namespace QuanLyKhoHang.Models
{
    public class ChiTietPhieuXuat
    {
        public int MaChiTietPX { get; set; } // Tuỳ chọn
        public int MaPhieuXuat { get; set; }
        public int MaHangHoa { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGiaXuat { get; set; }
        public decimal ThanhTien { get; set; } // Thường = SoLuong * DonGiaXuat
    }
}