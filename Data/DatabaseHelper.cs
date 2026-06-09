using System;
using System.Data;
using Npgsql;

namespace QuanLyKhoHang.Data
{
    public class DatabaseHelper
    {
        // Thay đổi Password bằng mật khẩu PostgreSQL thực tế của bạn
        private readonly string _connectionString = "Host=localhost;Port=5432;Database=quanlyhanghoa;Username=postgres;Password=1234";

        // Hàm 1: Lấy kết nối
        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        // Hàm 2: Dùng cho các câu lệnh INSERT, UPDATE, DELETE
        public int ExecuteNonQuery(string sql, NpgsqlParameter[] parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Hàm 3: Dùng cho các câu lệnh SELECT trả về bảng dữ liệu (DataTable)
        public DataTable ExecuteQuery(string sql, NpgsqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // Hàm 4: Dùng cho các câu lệnh SELECT trả về 1 giá trị duy nhất (như COUNT, SUM)
        public object ExecuteScalar(string sql, NpgsqlParameter[] parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}