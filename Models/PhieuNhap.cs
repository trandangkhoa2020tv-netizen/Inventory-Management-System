using System;

namespace QuanLyKhoHang.Models
{
    public class PhieuNhap
    {
        public int MaPhieuNhap { get; set; }
        public int MaNhaCungCap { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime NgayNhap { get; set; } = DateTime.Now;
        public decimal TongTien { get; set; }
        public string? GhiChu { get; set; }
    }
}