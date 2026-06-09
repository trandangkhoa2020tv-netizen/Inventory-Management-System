using System;

namespace QuanLyKhoHang.Models
{
    public class PhieuXuat
    {
        public int MaPhieuXuat { get; set; }
        public int MaKhachHang { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime NgayXuat { get; set; } = DateTime.Now;
        public decimal TongTien { get; set; }
        public string? GhiChu { get; set; }
    }
}