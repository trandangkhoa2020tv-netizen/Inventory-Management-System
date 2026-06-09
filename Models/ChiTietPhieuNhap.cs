namespace QuanLyKhoHang.Models
{
    public class ChiTietPhieuNhap
    {
        public int MaChiTietPN { get; set; } // Tuỳ chọn, nếu DB bạn có khóa chính riêng cho bảng chi tiết
        public int MaPhieuNhap { get; set; }
        public int MaHangHoa { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGiaNhap { get; set; }
        public decimal ThanhTien { get; set; } // Thường = SoLuong * DonGiaNhap
    }
}