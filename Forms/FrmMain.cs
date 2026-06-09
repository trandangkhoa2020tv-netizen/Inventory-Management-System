using System;
using System.Windows.Forms;

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
            // Mở sẵn màn hình Hàng Hóa làm trang chủ mặc định khi vừa đăng nhập vào
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

        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmHangHoa());
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmKhachHang());
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmNhanVien());
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmNhapKho());
        }

        private void btnXuatKho_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmXuatKho());
        }
    }
}