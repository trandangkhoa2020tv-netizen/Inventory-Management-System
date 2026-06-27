using System;
using Npgsql;
using QuanLyKhoHang.Data;

namespace QuanLyKhoHang.Repositories
{
    /// <summary>
    /// Repository kiểm tra tài khoản đăng nhập.
    /// Form đăng nhập dùng lớp này để xác thực username/password và lấy vai trò phân quyền.
    /// </summary>
    public class TaiKhoanRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        /// <summary>
        /// Kiểm tra đăng nhập.
        /// Nếu database có cột trang_thai thì chỉ cho tài khoản đang hoạt động đăng nhập.
        /// Nếu database cũ chưa có cột này thì vẫn đăng nhập theo user/password để tránh lỗi runtime.
        /// </summary>
        public string CheckLogin(string username, string password)
        {
            string sql = @"SELECT vai_tro
                           FROM taikhoan
                           WHERE ten_taikhoan = @username
                             AND mat_khau = @password";

            if (HasTrangThaiColumn())
            {
                sql += " AND trang_thai = true";
            }

            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@username", username),
                new NpgsqlParameter("@password", password)
            };

            object result = _dbHelper.ExecuteScalar(sql, parameters);
            return result != null ? result.ToString() : string.Empty;
        }

        /// <summary>
        /// Kiểm tra schema hiện tại có cột trang_thai trong bảng taikhoan hay chưa.
        /// Hàm này giúp chương trình tương thích database cũ.
        /// </summary>
        private bool HasTrangThaiColumn()
        {
            const string sql = @"SELECT COUNT(*)
                                 FROM information_schema.columns
                                 WHERE table_schema = 'public'
                                   AND table_name = 'taikhoan'
                                   AND column_name = 'trang_thai'";

            object result = _dbHelper.ExecuteScalar(sql);
            return Convert.ToInt32(result) > 0;
        }
    }
}
