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
            this.pnlShell = new System.Windows.Forms.Panel();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlBrand = new System.Windows.Forms.Panel();
            this.lblBrandIcon = new System.Windows.Forms.Label();
            this.lblBrandText = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnHangHoa = new System.Windows.Forms.Button();
            this.btnKhachHang = new System.Windows.Forms.Button();
            this.btnNhanVien = new System.Windows.Forms.Button();
            this.btnNhapKho = new System.Windows.Forms.Button();
            this.btnXuatKho = new System.Windows.Forms.Button();
            this.pnlSidebarFooter = new System.Windows.Forms.Panel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnUserMenu = new System.Windows.Forms.Button();
            this.pnlHeaderLine = new System.Windows.Forms.Panel();
            this.pnlSidebarLine = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.cmsUser = new System.Windows.Forms.ContextMenuStrip();
            this.pnlShell.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlBrand.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnlSidebarFooter.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();

            System.Drawing.Size menuButtonSize = new System.Drawing.Size(220, 44);
            System.Drawing.Font menuFont = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            System.Drawing.Color menuTextColor = System.Drawing.Color.FromArgb(43, 54, 73);

            // pnlShell
            this.pnlShell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlShell.Location = new System.Drawing.Point(0, 0);
            this.pnlShell.Name = "pnlShell";
            this.pnlShell.Size = new System.Drawing.Size(1200, 700);
            this.pnlShell.BackColor = System.Drawing.Color.FromArgb(245, 247, 251);
            this.pnlShell.Controls.Add(this.pnlContent);
            this.pnlShell.Controls.Add(this.pnlHeader);
            this.pnlShell.Controls.Add(this.pnlSidebar);

            // pnlSidebar
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(260, 700);
            this.pnlSidebar.BackColor = System.Drawing.Color.White;
            this.pnlSidebar.Controls.Add(this.flowLayoutPanel1);
            this.pnlSidebar.Controls.Add(this.pnlSidebarFooter);
            this.pnlSidebar.Controls.Add(this.pnlBrand);
            this.pnlSidebar.Controls.Add(this.pnlSidebarLine);

            // pnlSidebarLine
            this.pnlSidebarLine.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSidebarLine.Location = new System.Drawing.Point(259, 0);
            this.pnlSidebarLine.Name = "pnlSidebarLine";
            this.pnlSidebarLine.Size = new System.Drawing.Size(1, 700);
            this.pnlSidebarLine.BackColor = System.Drawing.Color.FromArgb(224, 229, 238);

            // pnlBrand
            this.pnlBrand.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBrand.Location = new System.Drawing.Point(0, 0);
            this.pnlBrand.Name = "pnlBrand";
            this.pnlBrand.Size = new System.Drawing.Size(259, 120);
            this.pnlBrand.BackColor = System.Drawing.Color.White;
            this.pnlBrand.Controls.Add(this.lblBrandIcon);
            this.pnlBrand.Controls.Add(this.lblBrandText);

            this.lblBrandIcon.Text = "QL";
            this.lblBrandIcon.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblBrandIcon.BackColor = System.Drawing.Color.FromArgb(30, 112, 235);
            this.lblBrandIcon.ForeColor = System.Drawing.Color.White;
            this.lblBrandIcon.Location = new System.Drawing.Point(24, 38);
            this.lblBrandIcon.Name = "lblBrandIcon";
            this.lblBrandIcon.Size = new System.Drawing.Size(48, 48);
            this.lblBrandIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblBrandText.Text = "Quản Lý\r\nKho Hàng";
            this.lblBrandText.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblBrandText.ForeColor = System.Drawing.Color.FromArgb(30, 112, 235);
            this.lblBrandText.Location = new System.Drawing.Point(84, 35);
            this.lblBrandText.Name = "lblBrandText";
            this.lblBrandText.Size = new System.Drawing.Size(160, 62);
            this.lblBrandText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // flowLayoutPanel1
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 120);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(259, 470);
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.WrapContents = false;

            // Menu buttons
            this.btnHangHoa.Size = menuButtonSize;
            this.btnHangHoa.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnHangHoa.Text = "Hàng Hóa";
            this.btnHangHoa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHangHoa.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnHangHoa.Font = menuFont;
            this.btnHangHoa.BackColor = System.Drawing.Color.FromArgb(30, 112, 235);
            this.btnHangHoa.ForeColor = System.Drawing.Color.White;
            this.btnHangHoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHangHoa.FlatAppearance.BorderSize = 0;
            this.btnHangHoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHangHoa.Click += new System.EventHandler(this.btnHangHoa_Click);

            this.btnKhachHang.Size = menuButtonSize;
            this.btnKhachHang.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnKhachHang.Text = "Khách Hàng";
            this.btnKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKhachHang.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnKhachHang.Font = menuFont;
            this.btnKhachHang.BackColor = System.Drawing.Color.White;
            this.btnKhachHang.ForeColor = menuTextColor;
            this.btnKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKhachHang.FlatAppearance.BorderSize = 0;
            this.btnKhachHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);

            this.btnNhanVien.Size = menuButtonSize;
            this.btnNhanVien.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnNhanVien.Text = "Nhân Viên";
            this.btnNhanVien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNhanVien.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnNhanVien.Font = menuFont;
            this.btnNhanVien.BackColor = System.Drawing.Color.White;
            this.btnNhanVien.ForeColor = menuTextColor;
            this.btnNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhanVien.FlatAppearance.BorderSize = 0;
            this.btnNhanVien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNhanVien.Click += new System.EventHandler(this.btnNhanVien_Click);

            this.btnNhapKho.Size = menuButtonSize;
            this.btnNhapKho.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnNhapKho.Text = "Nhập Kho";
            this.btnNhapKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNhapKho.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnNhapKho.Font = menuFont;
            this.btnNhapKho.BackColor = System.Drawing.Color.White;
            this.btnNhapKho.ForeColor = menuTextColor;
            this.btnNhapKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhapKho.FlatAppearance.BorderSize = 0;
            this.btnNhapKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNhapKho.Click += new System.EventHandler(this.btnNhapKho_Click);

            this.btnXuatKho.Size = menuButtonSize;
            this.btnXuatKho.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnXuatKho.Text = "Xuất Kho";
            this.btnXuatKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXuatKho.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnXuatKho.Font = menuFont;
            this.btnXuatKho.BackColor = System.Drawing.Color.White;
            this.btnXuatKho.ForeColor = menuTextColor;
            this.btnXuatKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatKho.FlatAppearance.BorderSize = 0;
            this.btnXuatKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatKho.Click += new System.EventHandler(this.btnXuatKho_Click);

            this.flowLayoutPanel1.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.btnHangHoa,
                this.btnKhachHang,
                this.btnNhanVien,
                this.btnNhapKho,
                this.btnXuatKho
            });

            // pnlSidebarFooter
            this.pnlSidebarFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSidebarFooter.Location = new System.Drawing.Point(0, 590);
            this.pnlSidebarFooter.Name = "pnlSidebarFooter";
            this.pnlSidebarFooter.Size = new System.Drawing.Size(259, 110);
            this.pnlSidebarFooter.BackColor = System.Drawing.Color.White;
            this.pnlSidebarFooter.Controls.Add(this.lblVersion);

            this.lblVersion.Text = "Phiên bản 1.0\r\n© 2026 Quản Lý Kho Hàng";
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(105, 116, 135);
            this.lblVersion.Location = new System.Drawing.Point(20, 26);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(220, 58);
            this.lblVersion.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            this.lblVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // pnlHeader
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(260, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(940, 60);
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnUserMenu);
            this.pnlHeader.Controls.Add(this.pnlHeaderLine);

            this.btnUserMenu.Text = "Admin";
            this.btnUserMenu.Size = new System.Drawing.Size(170, 40);
            this.btnUserMenu.Location = new System.Drawing.Point(755, 10);
            this.btnUserMenu.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnUserMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserMenu.FlatAppearance.BorderSize = 1;
            this.btnUserMenu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(224, 229, 238);
            this.btnUserMenu.BackColor = System.Drawing.Color.White;
            this.btnUserMenu.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnUserMenu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUserMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUserMenu.Click += new System.EventHandler(this.btnUserMenu_Click);

            this.pnlHeaderLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlHeaderLine.Location = new System.Drawing.Point(0, 59);
            this.pnlHeaderLine.Name = "pnlHeaderLine";
            this.pnlHeaderLine.Size = new System.Drawing.Size(940, 1);
            this.pnlHeaderLine.BackColor = System.Drawing.Color.FromArgb(224, 229, 238);

            // pnlContent
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(260, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(940, 640);
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);

            // cmsUser
            var menuTaiKhoan = new System.Windows.Forms.ToolStripMenuItem() { Enabled = false };
            var menuChucVu = new System.Windows.Forms.ToolStripMenuItem() { Enabled = false };
            var menuDangXuat = new System.Windows.Forms.ToolStripMenuItem() { Text = "Đăng xuất tài khoản", ForeColor = System.Drawing.Color.Crimson };
            menuDangXuat.Click += new System.EventHandler(this.menuDangXuat_Click);
            this.cmsUser.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                menuTaiKhoan,
                menuChucVu,
                new System.Windows.Forms.ToolStripSeparator(),
                menuDangXuat
            });

            // FrmMain
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlShell);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý Kho Hàng - Dashboard";
            this.Load += new System.EventHandler(this.FrmMain_Load);

            this.pnlHeader.ResumeLayout(false);
            this.pnlSidebarFooter.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnlBrand.ResumeLayout(false);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlShell.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
