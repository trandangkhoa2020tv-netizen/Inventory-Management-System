using System;
using System.Windows.Forms;
using QuanLyKhoHang.Models; // BẮT BUỘC CÓ: Để lôi vai trò từ bộ nhớ tạm UserSession ra check

namespace QuanLyKhoHang.Forms
{
    public partial class FrmMain : Form
    {
        private Form? activeForm = null;

        public FrmMain()
        {
            InitializeComponent();
            // Ép màn hình chính tự động phóng to toàn màn hình khi vừa mở
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // 1. KIỂM TRA QUYỀN HẠN ĐỂ PHÂN PHỐI GIAO DIỆN MENU BÊN TRÁI
            string vaiTroHienTai = UserSession.VaiTro;
            string taiKhoanHienTai = UserSession.TenTaiKhoan;

            if (vaiTroHienTai == "NhanVien")
            {
                // Cả 2 nhân viên đều KHÔNG được xem danh sách nhân sự
                btnNhanVien.Visible = false; 

                // TÁCH BIỆT CHI TIẾT THEO TÊN TÀI KHOẢN ĐĂNG NHẬP
                if (taiKhoanHienTai == "nhanvienkho")
                {
                    btnXuatKho.Visible = false; // Nhân viên kho không được đi bán hàng
                    btnNhapKho.Visible = true;
                }
                else if (taiKhoanHienTai == "nhanvienbanhang")
                {
                    btnNhapKho.Visible = false;  // Nhân viên bán hàng không được vào nhập kho
                    btnXuatKho.Visible = true;
                }
            }
            else
            {
                // Nếu là Admin thì luôn luôn hiển thị đầy đủ không ẩn bất kỳ nút nào
                btnNhanVien.Visible = true;
                btnHangHoa.Visible = true;
                btnKhachHang.Visible = true;
                btnNhapKho.Visible = true;
                btnXuatKho.Visible = true;
            }

            // 2. CẬP NHẬT THÔNG TIN NGƯỜI DÙNG LÊN NÚT BẤM GÓC TRÊN BÊN PHẢI
            this.btnUserMenu.Text = $"👤 Tài khoản: {UserSession.TenTaiKhoan}"; 
            
            // Đổ thông tin chi tiết vào menu thả xuống khi click
            cmsUser.Items[0].Text = $"Tài khoản: {UserSession.TenTaiKhoan}";
            cmsUser.Items[1].Text = $"Quyền hạn: {UserSession.VaiTro}";

            // 3. Mở sẵn màn hình Hàng Hóa làm trang chủ mặc định khi vừa đăng nhập vào
            btnHangHoa_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// Hàm nhúng Form con trực tiếp vào vùng không gian trống của FrmMain
        /// </summary>
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close(); // Đóng form cũ đang mở để giải phóng RAM
            }
            
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None; // Xóa viền Form con để nhúng mượt mà
            childForm.Dock = DockStyle.Fill;                  // Ép giãn đều 100% không gian vùng chứa
            
            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // HÀM XỬ LÝ ĐĂNG XUẤT CHUYỂN ĐỔI TÀI KHOẢN
        private void menuDangXuat_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất tài khoản hiện tại để chuyển đổi không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // 1. Xóa sạch dữ liệu phiên làm việc cũ để bảo mật hệ thống
                UserSession.TenTaiKhoan = "";
                UserSession.VaiTro = "";

                // 2. Ẩn màn hình chính hiện tại đi
                this.Hide(); 

                // 3. Khởi tạo và hiển thị lại màn hình Đăng Nhập ban đầu của bạn
                // Khoa kiểm tra xem class form đăng nhập thật của bạn tên là FrmDangNhap hay FrmLogin rồi sửa lại chữ FrmLogin này nhé
                FrmDangNhap loginForm = new FrmDangNhap(); 
                loginForm.ShowDialog();

                // 4. Giải phóng hoàn toàn bộ nhớ của màn hình Dashboard
                this.Close(); 
            }
        }

        // CÁC SỰ KIỆN CLICK MỞ FORM CON
        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmHangHoa(UserSession.VaiTro));
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmKhachHang(UserSession.VaiTro));
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmNhanVien());
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
           OpenChildForm(new FrmNhapKho(UserSession.VaiTro));
        }

        private void btnXuatKho_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmXuatKho(UserSession.VaiTro));
        }
    }
}