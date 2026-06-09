using System;
using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Repositories
{
    public class PhieuNhapRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        // Lấy danh sách phiếu nhập hiển thị lên lưới dữ liệu
        public DataTable GetAll()
        {
            string sql = @"SELECT p.ma_phieunhap AS ""Mã Phiếu"", n.ten_nhacungcap AS ""Nhà Cung Cấp"", 
                           nv.ten_nhanvien AS ""Nhân Viên Lập"", p.ngay_nhap AS ""Ngày Nhập"", 
                           p.tong_tien AS ""Tổng Tiền"", p.ghi_chu AS ""Ghi Chú""
                           FROM phieunhap p
                           JOIN nhacungcap n ON p.ma_nhacungcap = n.ma_nhacungcap
                           JOIN nhanvien nv ON p.ma_nhanvien = nv.ma_nhanvien
                           ORDER BY p.ma_phieunhap DESC";
            return _dbHelper.ExecuteQuery(sql);
        }

        // Thêm mới một phiếu nhập và nhận lại mã ID tự tăng vừa tạo
        public int ThemPhieuNhap(PhieuNhap pn)
        {
            string sql = @"INSERT INTO phieunhap (ma_nhacungcap, ma_nhanvien, tong_tien, ghi_chu) 
                           VALUES (@mancc, @manv, @tongtien, @ghichu) RETURNING ma_phieunhap";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@mancc", pn.MaNhaCungCap),
                new NpgsqlParameter("@manv", pn.MaNhanVien),
                new NpgsqlParameter("@tongtien", pn.TongTien),
                new NpgsqlParameter("@ghichu", (object)pn.GhiChu ?? DBNull.Value)
            };
            object result = _dbHelper.ExecuteScalar(sql, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        // Thêm chi tiết mặt hàng vào phiếu nhập
        public int ThemChiTiet(ChiTietPhieuNhap ct)
        {
            string sql = @"INSERT INTO chitietphieunhap (ma_phieunhap, ma_hanghoa, so_luong, don_gia_nhap, thanh_tien) 
                           VALUES (@mapn, @mahang, @soluong, @dongia, @thanhtien);
                           UPDATE hanghoa SET so_luong_ton = so_luong_ton + @soluong WHERE ma_hanghoa = @mahang;";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@mapn", ct.MaPhieuNhap),
                new NpgsqlParameter("@mahang", ct.MaHangHoa),
                new NpgsqlParameter("@soluong", ct.SoLuong),
                new NpgsqlParameter("@dongia", ct.DonGiaNhap),
                new NpgsqlParameter("@thanhtien", ct.ThanhTien)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }
    }
}