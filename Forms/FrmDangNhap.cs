using System;
using System.Windows.Forms;
using QuanLyKhoHang.Models;       // THÊM DÒNG NÀY: Để sử dụng lớp lưu vai trò UserSession
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.Forms
{
    public partial class FrmDangNhap : Form
    {
        private readonly TaiKhoanRepository _taiKhoanRepository;

        public FrmDangNhap()
        {
            InitializeComponent();
            _taiKhoanRepository = new TaiKhoanRepository();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // 1. Kiểm tra tính hợp lệ của dữ liệu đầu vào (Validation)
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên tài khoản và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            try
            {
                // 2. Gọi CheckLogin để lấy vai trò từ Database trả về
                string vaiTro = _taiKhoanRepository.CheckLogin(username, password);

                // Nếu chuỗi vai trò trả về không rỗng nghĩa là tài khoản đúng
                if (!string.IsNullOrEmpty(vaiTro))
                {
                    // ĐÃ THÊM: Lưu trực tiếp vai trò nhận được từ DB vào bộ nhớ tạm hệ thống
                    UserSession.VaiTro = vaiTro; 
                    UserSession.TenTaiKhoan = username;

                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // 3. Mở Form Main
                    FrmMain frmMain = new FrmMain();
                    this.Hide();
                    frmMain.ShowDialog(); 
                    this.Close(); 
                }
                else
                {
                    // 4. Nếu thất bại -> Thông báo lỗi
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