namespace QuanLyKhoHang.Models
{
    /// <summary>
    /// Model tài khoản đăng nhập.
    /// Dữ liệu tương ứng bảng taikhoan và dùng cho phân quyền hệ thống.
    /// </summary>
    public class TaiKhoan
    {
        /// <summary>Mã tài khoản.</summary>
        public int MaTaiKhoan { get; set; }

        /// <summary>Mã nhân viên sở hữu tài khoản.</summary>
        public int MaNhanVien { get; set; }

        /// <summary>Tên đăng nhập.</summary>
        public string TenTaiKhoan { get; set; } = string.Empty;

        /// <summary>Mật khẩu đăng nhập. Dự án hiện đang lưu dạng text theo dữ liệu mẫu.</summary>
        public string MatKhau { get; set; } = string.Empty;

        /// <summary>Vai trò phân quyền, ví dụ Admin hoặc NhanVien.</summary>
        public string VaiTro { get; set; } = string.Empty;
    }
}
