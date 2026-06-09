using System;
using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Repositories
{
    public class HangHoaRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        public DataTable GetAll()
        {
            // Kết hợp inner join để lấy tên loại và tên NCC hiển thị lên GridView cho đẹp
            string sql = @"SELECT h.ma_hanghoa AS ""Mã Hàng"", h.ten_hanghoa AS ""Tên Hàng Hóa"", 
                           l.ten_loaihang AS ""Loại Hàng"", n.ten_nhacungcap AS ""Nhà Cung Cấp"", 
                           h.gia_nhap AS ""Giá Nhập"", h.gia_ban AS ""Giá Bán"", 
                           h.so_luong_ton AS ""Tồn Kho"", h.don_vi_tinh AS ""ĐVT"", h.ghi_chu AS ""Ghi Chú""
                           FROM hanghoa h
                           JOIN loaihang l ON h.ma_loaihang = l.ma_loaihang
                           JOIN nhacungcap n ON h.ma_nhacungcap = n.ma_nhacungcap
                           ORDER BY h.ma_hanghoa DESC";
            return _dbHelper.ExecuteQuery(sql);
        }

        public int Them(HangHoa hh)
        {
            string sql = @"INSERT INTO hanghoa (ten_hanghoa, ma_loaihang, ma_nhacungcap, gia_nhap, gia_ban, so_luong_ton, don_vi_tinh, ghi_chu) 
                           VALUES (@ten, @maloai, @mancc, @gianhap, @giaban, @ton, @dvt, @ghichu)";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ten", hh.TenHangHoa),
                new NpgsqlParameter("@maloai", hh.MaLoaiHang),
                new NpgsqlParameter("@mancc", hh.MaNhaCungCap),
                new NpgsqlParameter("@gianhap", hh.GiaNhap),
                new NpgsqlParameter("@giaban", hh.GiaBan),
                new NpgsqlParameter("@ton", hh.SoLuongTon),
                new NpgsqlParameter("@dvt", hh.DonViTinh),
                new NpgsqlParameter("@ghichu", (object)hh.GhiChu ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        public int Sua(HangHoa hh)
        {
            string sql = @"UPDATE hanghoa SET ten_hanghoa = @ten, ma_loaihang = @maloai, ma_nhacungcap = @mancc, 
                           gia_nhap = @gianhap, gia_ban = @giaban, so_luong_ton = @ton, don_vi_tinh = @dvt, ghi_chu = @ghichu 
                           WHERE ma_hanghoa = @ma";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", hh.MaHangHoa),
                new NpgsqlParameter("@ten", hh.TenHangHoa),
                new NpgsqlParameter("@maloai", hh.MaLoaiHang),
                new NpgsqlParameter("@mancc", hh.MaNhaCungCap),
                new NpgsqlParameter("@gianhap", hh.GiaNhap),
                new NpgsqlParameter("@giaban", hh.GiaBan),
                new NpgsqlParameter("@ton", hh.SoLuongTon),
                new NpgsqlParameter("@dvt", hh.DonViTinh),
                new NpgsqlParameter("@ghichu", (object)hh.GhiChu ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        public int Xoa(int maHang)
        {
            string sql = "DELETE FROM hanghoa WHERE ma_hanghoa = @ma";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", maHang)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }
    }
}