using System;
using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Repositories
{
    public class KhachHangRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        public DataTable GetAll()
        {
            string sql = "SELECT ma_khachhang AS \"Mã KH\", ten_khachhang AS \"Tên Khách Hàng\", dia_chi_kh AS \"Địa Chỉ\", so_dien_thoai AS \"SĐT\", email AS \"Email\", ghi_chu AS \"Ghi Chú\" FROM khachhang ORDER BY ma_khachhang DESC";
            return _dbHelper.ExecuteQuery(sql);
        }

        public int Them(KhachHang kh)
        {
            string sql = "INSERT INTO khachhang (ten_khachhang, dia_chi_kh, so_dien_thoai, email, ghi_chu) VALUES (@ten, @diachi, @sdt, @email, @ghichu)";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ten", kh.TenKhachHang),
                new NpgsqlParameter("@diachi", kh.DiaChiKH),
                new NpgsqlParameter("@sdt", kh.SoDienThoai),
                new NpgsqlParameter("@email", (object)kh.Email ?? DBNull.Value),
                new NpgsqlParameter("@ghichu", (object)kh.GhiChu ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        public int Sua(KhachHang kh)
        {
            string sql = "UPDATE khachhang SET ten_khachhang = @ten, dia_chi_kh = @diachi, so_dien_thoai = @sdt, email = @email, ghi_chu = @ghichu WHERE ma_khachhang = @ma";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", kh.MaKhachHang),
                new NpgsqlParameter("@ten", kh.TenKhachHang),
                new NpgsqlParameter("@diachi", kh.DiaChiKH),
                new NpgsqlParameter("@sdt", kh.SoDienThoai),
                new NpgsqlParameter("@email", (object)kh.Email ?? DBNull.Value),
                new NpgsqlParameter("@ghichu", (object)kh.GhiChu ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        public int Xoa(int maKh)
        {
            string sql = "DELETE FROM khachhang WHERE ma_khachhang = @ma";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", maKh)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }
    }
}