using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
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
            string sql = @"SELECT vai_tro, mat_khau
                           FROM taikhoan
                           WHERE ten_taikhoan = @username";

            if (HasTrangThaiColumn())
            {
                sql += " AND trang_thai = true";
            }

            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@username", username)
            };

            DataTable result = _dbHelper.ExecuteQuery(sql, parameters);
            if (result.Rows.Count == 0)
            {
                return string.Empty;
            }

            DataRow row = result.Rows[0];
            string storedPassword = Convert.ToString(row["mat_khau"]) ?? string.Empty;
            if (!VerifyPassword(password, storedPassword))
            {
                return string.Empty;
            }

            return Convert.ToString(row["vai_tro"]) ?? string.Empty;
        }

        /// <summary>
        /// Kiem tra mat khau. Uu tien PBKDF2, van chap nhan SHA-256/plain text de tuong thich database cu.
        /// </summary>
        private static bool VerifyPassword(string password, string storedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedPassword))
            {
                return false;
            }

            if (storedPassword.StartsWith("pbkdf2$", StringComparison.OrdinalIgnoreCase))
            {
                return VerifyPbkdf2(password, storedPassword);
            }

            if (IsSha256Hex(storedPassword))
            {
                return string.Equals(HashSha256(password), storedPassword, StringComparison.OrdinalIgnoreCase);
            }

            return string.Equals(password, storedPassword, StringComparison.Ordinal);
        }

        private static bool VerifyPbkdf2(string password, string storedPassword)
        {
            string[] parts = storedPassword.Split('$');
            if (parts.Length != 4 || !int.TryParse(parts[1], out int iterations) || iterations <= 0)
            {
                return false;
            }

            try
            {
                byte[] salt = Convert.FromBase64String(parts[2]);
                byte[] expectedHash = Convert.FromBase64String(parts[3]);

                byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    iterations,
                    HashAlgorithmName.SHA256,
                    expectedHash.Length);

                return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static string HashSha256(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder(bytes.Length * 2);

            foreach (byte value in bytes)
            {
                builder.Append(value.ToString("x2"));
            }

            return builder.ToString();
        }

        private static bool IsSha256Hex(string value)
        {
            if (value.Length != 64)
            {
                return false;
            }

            foreach (char c in value)
            {
                bool isHex = (c >= '0' && c <= '9')
                    || (c >= 'a' && c <= 'f')
                    || (c >= 'A' && c <= 'F');

                if (!isHex)
                {
                    return false;
                }
            }

            return true;
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
