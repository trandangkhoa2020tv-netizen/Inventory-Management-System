using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Forms
{
    /// <summary>
    /// Form chính sau khi đăng nhập.
    /// Form này điều phối menu trái, phân quyền theo UserSession và nhúng các form nghiệp vụ vào vùng nội dung.
    /// </summary>
    public partial class FrmMain : Form
    {
        // Form con hiện tại đang được nhúng trong panel nội dung.
        private Form activeForm = null;
        private readonly Color activeMenuBackColor = Color.FromArgb(30, 112, 235);
        private readonly Color inactiveMenuBackColor = Color.White;
        private readonly Color inactiveMenuTextColor = Color.FromArgb(43, 54, 73);

        /// <summary>
        /// Khởi tạo form chính và mở ở chế độ toàn màn hình.
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Khi form chính mở: phân quyền menu, hiển thị thông tin tài khoản và mở mặc định form hàng hóa.
        /// </summary>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            string vaiTroHienTai = UserSession.VaiTro;
            string taiKhoanHienTai = UserSession.TenTaiKhoan;

            if (vaiTroHienTai == "NhanVien")
            {
                // Nhân viên không được vào màn hình quản lý nhân sự.
                btnNhanVien.Visible = false;

                // Tách quyền chi tiết theo tài khoản mẫu.
                if (taiKhoanHienTai == "nhanvienkho")
                {
                    btnXuatKho.Visible = false;
                    btnNhapKho.Visible = true;
                }
                else if (taiKhoanHienTai == "nhanvienbanhang")
                {
                    btnNhapKho.Visible = false;
                    btnXuatKho.Visible = true;
                }
            }
            else
            {
                // Admin được xem toàn bộ chức năng.
                btnNhanVien.Visible = true;
                btnHangHoa.Visible = true;
                btnKhachHang.Visible = true;
                btnNhapKho.Visible = true;
                btnXuatKho.Visible = true;
            }

            btnUserMenu.Text = UserSession.TenTaiKhoan;
            cmsUser.Items[0].Text = $"Tài khoản: {UserSession.TenTaiKhoan}";
            cmsUser.Items[1].Text = $"Quyền hạn: {UserSession.VaiTro}";

            btnHangHoa_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// Nhúng form con vào panel nội dung.
        /// Mỗi lần mở form mới sẽ đóng form cũ để tránh chồng giao diện và giữ bộ nhớ gọn.
        /// </summary>
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        /// <summary>
        /// Đăng xuất tài khoản hiện tại, xóa session và quay lại màn hình đăng nhập.
        /// </summary>
        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất tài khoản hiện tại để chuyển đổi không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UserSession.TenTaiKhoan = string.Empty;
                UserSession.VaiTro = string.Empty;

                Hide();

                FrmDangNhap loginForm = new FrmDangNhap();
                loginForm.ShowDialog();

                Close();
            }
        }

        /// <summary>
        /// Hiển thị menu tài khoản tại vị trí nút người dùng.
        /// </summary>
        private void btnUserMenu_Click(object sender, EventArgs e)
        {
            cmsUser.Show(btnUserMenu, new System.Drawing.Point(0, btnUserMenu.Height));
        }

        /// <summary>
        /// Đổi màu nút menu đang được chọn và trả các nút còn lại về trạng thái thường.
        /// </summary>
        private void SetActiveMenu(Button activeButton)
        {
            Button[] menuButtons = { btnHangHoa, btnKhachHang, btnNhanVien, btnNhapKho, btnXuatKho };
            foreach (Button button in menuButtons)
            {
                button.BackColor = button == activeButton ? activeMenuBackColor : inactiveMenuBackColor;
                button.ForeColor = button == activeButton ? Color.White : inactiveMenuTextColor;
            }
        }

        /// <summary>Mở form quản lý hàng hóa.</summary>
        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            SetActiveMenu(btnHangHoa);
            OpenChildForm(new FrmHangHoa(UserSession.VaiTro));
        }

        /// <summary>Mở form quản lý khách hàng.</summary>
        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            SetActiveMenu(btnKhachHang);
            OpenChildForm(new FrmKhachHang(UserSession.VaiTro));
        }

        /// <summary>Mở form quản lý nhân viên.</summary>
        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            SetActiveMenu(btnNhanVien);
            OpenChildForm(new FrmNhanVien());
        }

        /// <summary>Mở form lập phiếu nhập kho.</summary>
        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            SetActiveMenu(btnNhapKho);
            OpenChildForm(new FrmNhapKho(UserSession.VaiTro));
        }

        /// <summary>Mở form lập phiếu xuất kho.</summary>
        private void btnXuatKho_Click(object sender, EventArgs e)
        {
            SetActiveMenu(btnXuatKho);
            OpenChildForm(new FrmXuatKho(UserSession.VaiTro));
        }

        private void lblVersion_Click(object sender, EventArgs e)
        {

        }
    }
}
