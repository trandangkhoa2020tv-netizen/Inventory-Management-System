using System;
using System.Windows.Forms;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.Forms
{
    /// <summary>
    /// Form đăng nhập hệ thống.
    /// Form này kiểm tra tài khoản/mật khẩu, lưu session và mở FrmMain khi đăng nhập thành công.
    /// </summary>
    public partial class FrmDangNhap : Form
    {
        private readonly TaiKhoanRepository _taiKhoanRepository;

        public FrmDangNhap()
        {
            InitializeComponent();
            UiTheme.Apply(this);
            _taiKhoanRepository = new TaiKhoanRepository();
        }

        /// <summary>
        /// Xử lý nút đăng nhập.
        /// Luồng xử lý: đọc input, validate rỗng, kiểm tra database, lưu UserSession, mở màn hình chính.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên tài khoản và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            try
            {
                string vaiTro = _taiKhoanRepository.CheckLogin(username, password);

                if (!string.IsNullOrEmpty(vaiTro))
                {
                    // Lưu thông tin phiên để FrmMain biết ai đang đăng nhập và có quyền gì.
                    UserSession.VaiTro = vaiTro;
                    UserSession.TenTaiKhoan = username;

                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FrmMain frmMain = new FrmMain();
                    Hide();
                    frmMain.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Thoát toàn bộ ứng dụng khi người dùng xác nhận.
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát phần mềm?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
