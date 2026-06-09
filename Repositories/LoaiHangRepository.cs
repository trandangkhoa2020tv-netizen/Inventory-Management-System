using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Repositories
{
    public class LoaiHangRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        public DataTable GetAll()
        {
            string sql = "SELECT ma_loaihang AS \"Mã Loại\", ten_loaihang AS \"Tên Loại Hàng\", ghi_chu AS \"Ghi Chú\" FROM loaihang ORDER BY ma_loaihang DESC";
            return _dbHelper.ExecuteQuery(sql);
        }

        public int Them(LoaiHang lh)
        {
            string sql = "INSERT INTO loaihang (ten_loaihang, ghi_chu) VALUES (@ten, @ghichu)";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ten", lh.TenLoaiHang),
                new NpgsqlParameter("@ghichu", (object)lh.GhiChu ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        public int Sua(LoaiHang lh)
        {
            string sql = "UPDATE loaihang SET ten_loaihang = @ten, ghi_chu = @ghichu WHERE ma_loaihang = @ma";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", lh.MaLoaiHang),
                new NpgsqlParameter("@ten", lh.TenLoaiHang),
                new NpgsqlParameter("@ghichu", (object)lh.GhiChu ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        public int Xoa(int maLoai)
        {
            string sql = "DELETE FROM loaihang WHERE ma_loaihang = @ma";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", maLoai)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }
    }
}