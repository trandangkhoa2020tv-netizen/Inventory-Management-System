namespace QuanLyKhoHang.Forms
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlShell;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.Label lblBrandIcon;
        private System.Windows.Forms.Label lblBrandText;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel pnlSidebarFooter;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlHeaderLine;
        private System.Windows.Forms.Panel pnlSidebarLine;
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlShell = new Panel();
            pnlContent = new Panel();
            pnlHeader = new Panel();
            btnUserMenu = new Button();
            pnlHeaderLine = new Panel();
            pnlSidebar = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnHangHoa = new Button();
            btnKhachHang = new Button();
            btnNhanVien = new Button();
            btnNhapKho = new Button();
            btnXuatKho = new Button();
            pnlSidebarFooter = new Panel();
            lblVersion = new Label();
            pnlBrand = new Panel();
            lblBrandIcon = new Label();
            lblBrandText = new Label();
            pnlSidebarLine = new Panel();
            cmsUser = new ContextMenuStrip(components);
            menuTaiKhoan = new ToolStripMenuItem();
            menuChucVu = new ToolStripMenuItem();
            menuDangXuat = new ToolStripMenuItem();
            pnlShell.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlSidebar.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            pnlSidebarFooter.SuspendLayout();
            pnlBrand.SuspendLayout();
            cmsUser.SuspendLayout();
            SuspendLayout();
            // 
            // pnlShell
            // 
            pnlShell.BackColor = Color.FromArgb(245, 247, 251);
            pnlShell.Controls.Add(pnlContent);
            pnlShell.Controls.Add(pnlHeader);
            pnlShell.Controls.Add(pnlSidebar);
            pnlShell.Dock = DockStyle.Fill;
            pnlShell.Location = new Point(0, 0);
            pnlShell.Name = "pnlShell";
            pnlShell.Size = new Size(1200, 700);
            pnlShell.TabIndex = 1;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(240, 242, 245);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(260, 60);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(940, 640);
            pnlContent.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(btnUserMenu);
            pnlHeader.Controls.Add(pnlHeaderLine);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(260, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(940, 60);
            pnlHeader.TabIndex = 1;
            // 
            // btnUserMenu
            // 
            btnUserMenu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnUserMenu.BackColor = Color.White;
            btnUserMenu.Cursor = Cursors.Hand;
            btnUserMenu.FlatAppearance.BorderColor = Color.FromArgb(224, 229, 238);
            btnUserMenu.FlatStyle = FlatStyle.Flat;
            btnUserMenu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUserMenu.ForeColor = Color.FromArgb(30, 41, 59);
            btnUserMenu.Location = new Point(792, 10);
            btnUserMenu.Name = "btnUserMenu";
            btnUserMenu.Size = new Size(133, 44);
            btnUserMenu.TabIndex = 0;
            btnUserMenu.Text = "Admin";
            btnUserMenu.UseVisualStyleBackColor = false;
            btnUserMenu.Click += btnUserMenu_Click;
            // 
            // pnlHeaderLine
            // 
            pnlHeaderLine.BackColor = Color.FromArgb(224, 229, 238);
            pnlHeaderLine.Dock = DockStyle.Bottom;
            pnlHeaderLine.Location = new Point(0, 59);
            pnlHeaderLine.Name = "pnlHeaderLine";
            pnlHeaderLine.Size = new Size(940, 1);
            pnlHeaderLine.TabIndex = 1;
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.White;
            pnlSidebar.Controls.Add(flowLayoutPanel1);
            pnlSidebar.Controls.Add(pnlSidebarFooter);
            pnlSidebar.Controls.Add(pnlBrand);
            pnlSidebar.Controls.Add(pnlSidebarLine);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(260, 700);
            pnlSidebar.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.White;
            flowLayoutPanel1.Controls.Add(btnHangHoa);
            flowLayoutPanel1.Controls.Add(btnKhachHang);
            flowLayoutPanel1.Controls.Add(btnNhanVien);
            flowLayoutPanel1.Controls.Add(btnNhapKho);
            flowLayoutPanel1.Controls.Add(btnXuatKho);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 120);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(20, 8, 20, 8);
            flowLayoutPanel1.Size = new Size(259, 470);
            flowLayoutPanel1.TabIndex = 0;
            flowLayoutPanel1.WrapContents = false;
            // 
            // btnHangHoa
            // 
            btnHangHoa.BackColor = Color.FromArgb(30, 112, 235);
            btnHangHoa.Cursor = Cursors.Hand;
            btnHangHoa.FlatAppearance.BorderSize = 0;
            btnHangHoa.FlatStyle = FlatStyle.Flat;
            btnHangHoa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnHangHoa.ForeColor = Color.White;
            btnHangHoa.Location = new Point(20, 8);
            btnHangHoa.Margin = new Padding(0, 0, 0, 10);
            btnHangHoa.Name = "btnHangHoa";
            btnHangHoa.Padding = new Padding(22, 0, 0, 0);
            btnHangHoa.Size = new Size(220, 44);
            btnHangHoa.TabIndex = 0;
            btnHangHoa.Text = "Hàng Hóa";
            btnHangHoa.TextAlign = ContentAlignment.MiddleLeft;
            btnHangHoa.UseVisualStyleBackColor = false;
            btnHangHoa.Click += btnHangHoa_Click;
            // 
            // btnKhachHang
            // 
            btnKhachHang.BackColor = Color.White;
            btnKhachHang.Cursor = Cursors.Hand;
            btnKhachHang.FlatAppearance.BorderSize = 0;
            btnKhachHang.FlatStyle = FlatStyle.Flat;
            btnKhachHang.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnKhachHang.ForeColor = Color.FromArgb(43, 54, 73);
            btnKhachHang.Location = new Point(20, 62);
            btnKhachHang.Margin = new Padding(0, 0, 0, 10);
            btnKhachHang.Name = "btnKhachHang";
            btnKhachHang.Padding = new Padding(22, 0, 0, 0);
            btnKhachHang.Size = new Size(220, 44);
            btnKhachHang.TabIndex = 1;
            btnKhachHang.Text = "Khách Hàng";
            btnKhachHang.TextAlign = ContentAlignment.MiddleLeft;
            btnKhachHang.UseVisualStyleBackColor = false;
            btnKhachHang.Click += btnKhachHang_Click;
            // 
            // btnNhanVien
            // 
            btnNhanVien.BackColor = Color.White;
            btnNhanVien.Cursor = Cursors.Hand;
            btnNhanVien.FlatAppearance.BorderSize = 0;
            btnNhanVien.FlatStyle = FlatStyle.Flat;
            btnNhanVien.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnNhanVien.ForeColor = Color.FromArgb(43, 54, 73);
            btnNhanVien.Location = new Point(20, 116);
            btnNhanVien.Margin = new Padding(0, 0, 0, 10);
            btnNhanVien.Name = "btnNhanVien";
            btnNhanVien.Padding = new Padding(22, 0, 0, 0);
            btnNhanVien.Size = new Size(220, 44);
            btnNhanVien.TabIndex = 2;
            btnNhanVien.Text = "Nhân Viên";
            btnNhanVien.TextAlign = ContentAlignment.MiddleLeft;
            btnNhanVien.UseVisualStyleBackColor = false;
            btnNhanVien.Click += btnNhanVien_Click;
            // 
            // btnNhapKho
            // 
            btnNhapKho.BackColor = Color.White;
            btnNhapKho.Cursor = Cursors.Hand;
            btnNhapKho.FlatAppearance.BorderSize = 0;
            btnNhapKho.FlatStyle = FlatStyle.Flat;
            btnNhapKho.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnNhapKho.ForeColor = Color.FromArgb(43, 54, 73);
            btnNhapKho.Location = new Point(20, 170);
            btnNhapKho.Margin = new Padding(0, 0, 0, 10);
            btnNhapKho.Name = "btnNhapKho";
            btnNhapKho.Padding = new Padding(22, 0, 0, 0);
            btnNhapKho.Size = new Size(220, 44);
            btnNhapKho.TabIndex = 3;
            btnNhapKho.Text = "Nhập Kho";
            btnNhapKho.TextAlign = ContentAlignment.MiddleLeft;
            btnNhapKho.UseVisualStyleBackColor = false;
            btnNhapKho.Click += btnNhapKho_Click;
            // 
            // btnXuatKho
            // 
            btnXuatKho.BackColor = Color.White;
            btnXuatKho.Cursor = Cursors.Hand;
            btnXuatKho.FlatAppearance.BorderSize = 0;
            btnXuatKho.FlatStyle = FlatStyle.Flat;
            btnXuatKho.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnXuatKho.ForeColor = Color.FromArgb(43, 54, 73);
            btnXuatKho.Location = new Point(20, 224);
            btnXuatKho.Margin = new Padding(0, 0, 0, 10);
            btnXuatKho.Name = "btnXuatKho";
            btnXuatKho.Padding = new Padding(22, 0, 0, 0);
            btnXuatKho.Size = new Size(220, 44);
            btnXuatKho.TabIndex = 4;
            btnXuatKho.Text = "Xuất Kho";
            btnXuatKho.TextAlign = ContentAlignment.MiddleLeft;
            btnXuatKho.UseVisualStyleBackColor = false;
            btnXuatKho.Click += btnXuatKho_Click;
            // 
            // pnlSidebarFooter
            // 
            pnlSidebarFooter.BackColor = Color.White;
            pnlSidebarFooter.Controls.Add(lblVersion);
            pnlSidebarFooter.Dock = DockStyle.Bottom;
            pnlSidebarFooter.Location = new Point(0, 590);
            pnlSidebarFooter.Name = "pnlSidebarFooter";
            pnlSidebarFooter.Size = new Size(259, 110);
            pnlSidebarFooter.TabIndex = 1;
            // 
            // lblVersion
            // 
            lblVersion.BackColor = Color.FromArgb(248, 250, 253);
            lblVersion.BorderStyle = BorderStyle.FixedSingle;
            lblVersion.Font = new Font("Segoe UI", 8.5F);
            lblVersion.ForeColor = Color.FromArgb(105, 116, 135);
            lblVersion.Location = new Point(50, 36);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(155, 45);
            lblVersion.TabIndex = 0;
            lblVersion.Text = "Phiên bản 1.0\r\n© 2026 Quản Lý Kho Hàng";
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            lblVersion.Click += lblVersion_Click;
            // 
            // pnlBrand
            // 
            pnlBrand.BackColor = Color.White;
            pnlBrand.Controls.Add(lblBrandIcon);
            pnlBrand.Controls.Add(lblBrandText);
            pnlBrand.Dock = DockStyle.Top;
            pnlBrand.Location = new Point(0, 0);
            pnlBrand.Name = "pnlBrand";
            pnlBrand.Size = new Size(259, 120);
            pnlBrand.TabIndex = 2;
            // 
            // lblBrandIcon
            // 
            lblBrandIcon.BackColor = Color.FromArgb(30, 112, 235);
            lblBrandIcon.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblBrandIcon.ForeColor = Color.White;
            lblBrandIcon.Location = new Point(24, 38);
            lblBrandIcon.Name = "lblBrandIcon";
            lblBrandIcon.Size = new Size(48, 48);
            lblBrandIcon.TabIndex = 0;
            lblBrandIcon.Text = "QL";
            lblBrandIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblBrandText
            // 
            lblBrandText.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblBrandText.ForeColor = Color.FromArgb(30, 112, 235);
            lblBrandText.Location = new Point(84, 35);
            lblBrandText.Name = "lblBrandText";
            lblBrandText.Size = new Size(160, 62);
            lblBrandText.TabIndex = 1;
            lblBrandText.Text = "Quản Lý\r\nKho Hàng";
            lblBrandText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlSidebarLine
            // 
            pnlSidebarLine.BackColor = Color.FromArgb(224, 229, 238);
            pnlSidebarLine.Dock = DockStyle.Right;
            pnlSidebarLine.Location = new Point(259, 0);
            pnlSidebarLine.Name = "pnlSidebarLine";
            pnlSidebarLine.Size = new Size(1, 700);
            pnlSidebarLine.TabIndex = 3;
            // 
            // cmsUser
            // 
            cmsUser.ImageScalingSize = new Size(20, 20);
            cmsUser.Items.AddRange(new ToolStripItem[] { menuTaiKhoan, menuChucVu, menuDangXuat });
            cmsUser.Name = "cmsUser";
            cmsUser.Size = new Size(240, 76);
            // 
            // menuTaiKhoan
            // 
            menuTaiKhoan.Name = "menuTaiKhoan";
            menuTaiKhoan.Size = new Size(239, 24);
            menuTaiKhoan.Text = "Tai khoan";
            // 
            // menuChucVu
            // 
            menuChucVu.Name = "menuChucVu";
            menuChucVu.Size = new Size(239, 24);
            menuChucVu.Text = "Quyen han";
            // 
            // menuDangXuat
            // 
            menuDangXuat.Name = "menuDangXuat";
            menuDangXuat.Size = new Size(239, 24);
            menuDangXuat.Text = "Đăng xuất / Chuyển tài khoản";
            menuDangXuat.Click += menuDangXuat_Click;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 700);
            Controls.Add(pnlShell);
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ Thống Quản Lý Kho Hàng - Dashboard";
            Load += FrmMain_Load;
            pnlShell.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlSidebar.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            pnlSidebarFooter.ResumeLayout(false);
            pnlBrand.ResumeLayout(false);
            cmsUser.ResumeLayout(false);
            ResumeLayout(false);
        }
        private ToolStripMenuItem menuTaiKhoan;
        private ToolStripMenuItem menuChucVu;
        private ToolStripMenuItem menuDangXuat;
    }
}
