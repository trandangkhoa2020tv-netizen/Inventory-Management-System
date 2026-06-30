namespace QuanLyKhoHang.Forms
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnHangHoa;
        private System.Windows.Forms.Button btnKhachHang;
        private System.Windows.Forms.Button btnNhanVien;
        private System.Windows.Forms.Button btnNhapKho;
        private System.Windows.Forms.Button btnXuatKho;
        private System.Windows.Forms.Button btnUserMenu;
        private System.Windows.Forms.ContextMenuStrip cmsUser;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnHangHoa = new System.Windows.Forms.Button();
            this.btnKhachHang = new System.Windows.Forms.Button();
            this.btnNhanVien = new System.Windows.Forms.Button();
            this.btnNhapKho = new System.Windows.Forms.Button();
            this.btnXuatKho = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();

            // ======================================================================
            // 1. TỐI ƯU THANH MENU BÁM ĐỈNH (pnlHeader)
            // Thay màu Gainsboro cũ bằng màu trắng tinh tế, thêm bo viền phẳng nhẹ nhàng
            this.pnlHeader.Controls.Add(this.flowLayoutPanel1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 60); // Tăng nhẹ lên 60px cho menu thoáng đạt
            this.pnlHeader.BackColor = System.Drawing.Color.White;

            // 2. CẤU HÌNH BỘ BỐ TRÍ TỰ ĐỘNG (flowLayoutPanel1)
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(15, 12, 10, 10); // Căn lề lùi vào chút cho thoáng
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1200, 60);
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;

            // ======================================================================
            // 3. THIẾT KẾ LẠI HỆ THỐNG NÚT BẤM CHỨC NĂNG (Giao diện Phẳng - Hiện đại)
            // Kích thước chuẩn chỉnh, font chữ Segoe UI SemiBold chuyên nghiệp, màu nền đồng bộ
            System.Drawing.Size btnSize = new System.Drawing.Size(145, 36); // Tăng nhẹ kích thước nút cho dễ bấm
            System.Drawing.Font menuFont = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            System.Drawing.Color btnDefaultBg = System.Drawing.Color.FromArgb(245, 247, 250); // Màu xám siêu nhạt chuẩn SaaS
            System.Drawing.Color btnDefaultText = System.Drawing.Color.FromArgb(64, 74, 96);   // Màu chữ xám đen lịch lãm

            // Nút Hàng Hóa
            this.btnHangHoa.Size = btnSize;
            this.btnHangHoa.Text = "📦 Hàng Hóa";
            this.btnHangHoa.Font = menuFont;
            this.btnHangHoa.BackColor = btnDefaultBg;
            this.btnHangHoa.ForeColor = btnDefaultText;
            this.btnHangHoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHangHoa.FlatAppearance.BorderSize = 0;
            this.btnHangHoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHangHoa.Click += new System.EventHandler(this.btnHangHoa_Click);

            // Nút Khách Hàng
            this.btnKhachHang.Size = btnSize;
            this.btnKhachHang.Text = "👥 Khách Hàng";
            this.btnKhachHang.Font = menuFont;
            this.btnKhachHang.BackColor = btnDefaultBg;
            this.btnKhachHang.ForeColor = btnDefaultText;
            this.btnKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKhachHang.FlatAppearance.BorderSize = 0;
            this.btnKhachHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);

            // Nút Nhân Viên
            this.btnNhanVien.Size = btnSize;
            this.btnNhanVien.Text = "🧑‍💼 Nhân Viên";
            this.btnNhanVien.Font = menuFont;
            this.btnNhanVien.BackColor = btnDefaultBg;
            this.btnNhanVien.ForeColor = btnDefaultText;
            this.btnNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhanVien.FlatAppearance.BorderSize = 0;
            this.btnNhanVien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNhanVien.Click += new System.EventHandler(this.btnNhanVien_Click);

            // Nút Nhập Kho
            this.btnNhapKho.Size = btnSize;
            this.btnNhapKho.Text = "📥 Nhập Kho";
            this.btnNhapKho.Font = menuFont;
            this.btnNhapKho.BackColor = btnDefaultBg;
            this.btnNhapKho.ForeColor = btnDefaultText;
            this.btnNhapKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhapKho.FlatAppearance.BorderSize = 0;
            this.btnNhapKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNhapKho.Click += new System.EventHandler(this.btnNhapKho_Click);

            // Nút Xuất Kho
            this.btnXuatKho.Size = btnSize;
            this.btnXuatKho.Text = "📤 Xuất Kho";
            this.btnXuatKho.Font = menuFont;
            this.btnXuatKho.BackColor = btnDefaultBg;
            this.btnXuatKho.ForeColor = btnDefaultText;
            this.btnXuatKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatKho.FlatAppearance.BorderSize = 0;
            this.btnXuatKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatKho.Click += new System.EventHandler(this.btnXuatKho_Click);

            this.flowLayoutPanel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnHangHoa, this.btnKhachHang, this.btnNhanVien, this.btnNhapKho, this.btnXuatKho
            });

            // ======================================================================
            // 4. KHU VỰC CHỨA NỘI DUNG PHÂN HỆ (pnlContent)
            // Màu nền xám nhẹ dịu mắt, làm nổi bật các lưới và bảng nhập liệu bên trong
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1200, 640);
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);

            // FrmMain Tổng Thể
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);

            // ======================================================================
            // 5. CHUẨN HÓA MENU THẢ XUỐNG USER (ContextMenuStrip)
            this.cmsUser = new System.Windows.Forms.ContextMenuStrip();
            var menuTaiKhoan = new System.Windows.Forms.ToolStripMenuItem() { Enabled = false }; 
            var menuChucVu = new System.Windows.Forms.ToolStripMenuItem() { Enabled = false };
            var menuDangXuat = new System.Windows.Forms.ToolStripMenuItem() { Text = "🔒 Đăng xuất tài khoản", ForeColor = System.Drawing.Color.Crimson };

            menuDangXuat.Click += new System.EventHandler(this.menuDangXuat_Click);
            this.cmsUser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { menuTaiKhoan, menuChucVu, new System.Windows.Forms.ToolStripSeparator(), menuDangXuat });

            // ======================================================================
            // 6. TỐI ƯU NÚT BẤM USER TÀI KHOẢN (ĐỒNG BỘ MÀU XANH CÔNG NGHỆ 100%)
            this.btnUserMenu = new System.Windows.Forms.Button();
            this.btnUserMenu.Text = "👤 Tài khoản: admin"; 
            this.btnUserMenu.Size = new System.Drawing.Size(185, 38);
            this.btnUserMenu.Location = new System.Drawing.Point(990, 11); // Giữ nguyên neo góc phải chuẩn mực
            this.btnUserMenu.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right); 
            this.btnUserMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserMenu.FlatAppearance.BorderSize = 0;
            this.btnUserMenu.BackColor = System.Drawing.Color.FromArgb(0, 114, 198); // Mã xanh dương đậm Khoa chọn
            this.btnUserMenu.ForeColor = System.Drawing.Color.White; 
            this.btnUserMenu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnUserMenu.Cursor = System.Windows.Forms.Cursors.Hand;

            this.btnUserMenu.Click += new System.EventHandler(this.btnUserMenu_Click);

            // Nạp các control vào Form chính (Giữ nguyên thứ tự nạp gốc, tránh đảo lộn hiển thị)
            this.Controls.Add(this.btnUserMenu);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            
            // Ép nút tài khoản admin nổi lên trên thanh header màu trắng
            this.btnUserMenu.BringToFront();

            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý Kho Hàng - Dashboard";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.pnlHeader.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
