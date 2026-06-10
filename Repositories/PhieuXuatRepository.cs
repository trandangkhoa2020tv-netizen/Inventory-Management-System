using System;
using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Repositories
{
    public class PhieuXuatRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        // Lấy danh sách phiếu xuất hiển thị lên lưới dữ liệu
        public DataTable GetAll()
        {
            string sql = @"SELECT p.ma_phieuxuat AS ""Mã Phiếu"", k.ten_khachhang AS ""Khách Hàng"", 
                           nv.ten_nhanvien AS ""Nhân Viên Lập"", p.ngay_xuat AS ""Ngày Xuất"", 
                           p.tong_tien AS ""Tổng Tiền"", p.ghi_chu AS ""Ghi Chú""
                           FROM phieuxuat p
                           JOIN khachhang k ON p.ma_khachhang = k.ma_khachhang
                           JOIN nhanvien nv ON p.ma_nhanvien = nv.ma_nhanvien
                           ORDER BY p.ma_phieuxuat DESC";
            return _dbHelper.ExecuteQuery(sql);
        }

        // Thêm mới một phiếu xuất và nhận lại mã ID tự tăng vừa tạo
        public int ThemPhieuXuat(PhieuXuat px)
        {
            string sql = @"INSERT INTO phieuxuat (ma_khachhang, ma_nhanvien, tong_tien, ghi_chu) 
                           VALUES (@makh, @manv, @tongtien, @ghichu) RETURNING ma_phieuxuat";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@makh", px.MaKhachHang),
                new NpgsqlParameter("@manv", px.MaNhanVien),
                new NpgsqlParameter("@tongtien", px.TongTien),
                new NpgsqlParameter("@ghichu", (object)px.GhiChu ?? DBNull.Value)
            };
            object result = _dbHelper.ExecuteScalar(sql, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        // Thêm chi tiết mặt hàng vào phiếu xuất và tự động trừ hàng tồn kho
        public int ThemChiTiet(ChiTietPhieuXuat ct)
        {
            string sql = @"INSERT INTO chitietphieuxuat (ma_phieuxuat, ma_hanghoa, so_luong, don_gia_xuat, thanh_tien) 
                           VALUES (@mapx, @mahang, @soluong, @dongia, @thanhtien);
                           UPDATE hanghoa SET so_luong_ton = so_luong_ton - @soluong WHERE ma_hanghoa = @mahang;";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@mapx", ct.MaPhieuXuat),
                new NpgsqlParameter("@mahang", ct.MaHangHoa),
                new NpgsqlParameter("@soluong", ct.SoLuong),
                new NpgsqlParameter("@dongia", ct.DonGiaXuat),
                new NpgsqlParameter("@thanhtien", ct.ThanhTien)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        // ======================================================================
        // ĐÃ THÊM: 2 HÀM DƯỚI ĐÂY ĐỂ PHỤC VỤ HIỂN THỊ LỊCH SỬ VÀ XUẤT FILE ĐỒ AN

        // 1. Hàm trả về lịch sử danh sách phiếu cho FrmXuatKho gọi đến
        public DataTable GetAllPhieuXuat()
        {
            return GetAll(); // Tận dụng luôn hàm GetAll() siêu chuẩn bạn đã viết ở trên
        }

        // 2. Hàm gom trọn gói chi tiết mặt hàng của 1 phiếu (Dùng cho in Excel / PDF)
        public DataTable GetChiTietTheoMaPhieu(int maPhieu)
        {
            // Sử dụng JOIN để lấy thẳng Tên mặt hàng dựa theo mã hàng, đặt tên cột có dấu để ra Excel đẹp mắt
            string sql = @"SELECT hh.ten_hanghoa AS ""Tên Mặt Hàng"", 
                           ct.so_luong AS ""Số Lượng"", 
                           ct.don_gia_xuat AS ""Đơn Giá"", 
                           ct.thanh_tien AS ""Thành Tiền""
                           FROM chitietphieuxuat ct
                           JOIN hanghoa hh ON ct.ma_hanghoa = hh.ma_hanghoa
                           WHERE ct.ma_phieuxuat = @maphieu";

            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@maphieu", maPhieu)
            };

            return _dbHelper.ExecuteQuery(sql, parameters);
        }
    }
}