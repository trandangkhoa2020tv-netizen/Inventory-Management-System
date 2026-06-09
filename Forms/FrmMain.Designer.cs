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

            // pnlHeader (Thanh menu bám đỉnh)
            this.pnlHeader.Controls.Add(this.flowLayoutPanel1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 55);
            this.pnlHeader.BackColor = System.Drawing.Color.Gainsboro;

            // flowLayoutPanel1
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10, 12, 10, 10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1200, 55);

            System.Drawing.Size btnSize = new System.Drawing.Size(120, 32);

            this.btnHangHoa.Size = btnSize;
            this.btnHangHoa.Text = "Hàng Hóa";
            this.btnHangHoa.Click += new System.EventHandler(this.btnHangHoa_Click);

            this.btnKhachHang.Size = btnSize;
            this.btnKhachHang.Text = "Khách Hàng";
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);

            this.btnNhanVien.Size = btnSize;
            this.btnNhanVien.Text = "Nhân Viên";
            this.btnNhanVien.Click += new System.EventHandler(this.btnNhanVien_Click);

            this.btnNhapKho.Size = btnSize;
            this.btnNhapKho.Text = "Nhập Kho";
            this.btnNhapKho.Click += new System.EventHandler(this.btnNhapKho_Click);

            this.btnXuatKho.Size = btnSize;
            this.btnXuatKho.Text = "Xuất Kho";
            this.btnXuatKho.Click += new System.EventHandler(this.btnXuatKho_Click);

            this.flowLayoutPanel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnHangHoa, this.btnKhachHang, this.btnNhanVien, this.btnNhapKho, this.btnXuatKho
            });

            // pnlContent (Khung chứa nội dung full màn hình phía dưới)
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 55);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1200, 645);
            this.pnlContent.BackColor = System.Drawing.Color.WhiteSmoke;

            // FrmMain tổng thể
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
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