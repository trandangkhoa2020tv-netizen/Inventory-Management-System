using System;
using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Repositories
{
    public class TaiKhoanRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        // Kiểm tra đăng nhập và trả về vai trò (Admin/NhanVien), nếu sai trả về chuỗi rỗng
        public string CheckLogin(string username, string password)
        {
            string sql = "SELECT vai_tro FROM taikhoan WHERE ten_taikhoan = @username AND mat_khau = @password";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@username", username),
                new NpgsqlParameter("@password", password)
            };

            object result = _dbHelper.ExecuteScalar(sql, parameters);
            return result != null ? result.ToString() : string.Empty;
        }
    }
}