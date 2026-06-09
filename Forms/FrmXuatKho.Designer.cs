namespace QuanLyKhoHang.Forms
{
    partial class FrmXuatKho
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop; 
        private System.Windows.Forms.Panel pnlBottom; 
        private System.Windows.Forms.ComboBox cbKhachHang;
        private System.Windows.Forms.ComboBox cbNhanVien;
        private System.Windows.Forms.ComboBox cbHangHoa;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Button btnThemMon;
        private System.Windows.Forms.Button btnLuuPhieu;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPdf;
        private System.Windows.Forms.Label lblTongTien;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.cbKhachHang = new System.Windows.Forms.ComboBox();
            this.cbNhanVien = new System.Windows.Forms.ComboBox();
            this.cbHangHoa = new System.Windows.Forms.ComboBox();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.btnThemMon = new System.Windows.Forms.Button();
            this.btnLuuPhieu = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPdf = new System.Windows.Forms.Button();
            this.lblTongTien = new System.Windows.Forms.Label();

            var title = new System.Windows.Forms.Label() { Text = "LẬP PHIẾU XUẤT KHO (BÁN HÀNG)", Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold), Location = new System.Drawing.Point(20, 12), AutoSize = true, ForeColor = System.Drawing.Color.DarkBlue };
            var groupPhieu = new System.Windows.Forms.GroupBox() { Text = "Thông tin hóa đơn", Location = new System.Drawing.Point(20, 50), Size = new System.Drawing.Size(940, 80), Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right) };
            var groupHang = new System.Windows.Forms.GroupBox() { Text = "Chọn hàng xuất kho", Location = new System.Drawing.Point(20, 140), Size = new System.Drawing.Size(940, 80), Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right) };

            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            // pnlTop
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 235;
            this.pnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTop.Controls.AddRange(new System.Windows.Forms.Control[] { title, groupPhieu, groupHang });

            var l1 = new System.Windows.Forms.Label() { Text = "Khách hàng:", Location = new System.Drawing.Point(15, 33), AutoSize = true };
            var l2 = new System.Windows.Forms.Label() { Text = "Nhân viên xuất:", Location = new System.Drawing.Point(450, 33), AutoSize = true };
            this.cbKhachHang.Location = new System.Drawing.Point(120, 30); this.cbKhachHang.Size = new System.Drawing.Size(280, 27); this.cbKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNhanVien.Location = new System.Drawing.Point(560, 30); this.cbNhanVien.Size = new System.Drawing.Size(280, 27); this.cbNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            groupPhieu.Controls.AddRange(new System.Windows.Forms.Control[] { l1, l2, this.cbKhachHang, this.cbNhanVien });

            var l3 = new System.Windows.Forms.Label() { Text = "Mặt hàng:", Location = new System.Drawing.Point(15, 38), AutoSize = true };
            var l4 = new System.Windows.Forms.Label() { Text = "Số lượng:", Location = new System.Drawing.Point(360, 38), AutoSize = true };
            var l5 = new System.Windows.Forms.Label() { Text = "Đơn giá xuất:", Location = new System.Drawing.Point(540, 38), AutoSize = true };
            this.cbHangHoa.Location = new System.Drawing.Point(90, 35); this.cbHangHoa.Size = new System.Drawing.Size(240, 27); this.cbHangHoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtSoLuong.Location = new System.Drawing.Point(430, 35); this.txtSoLuong.Size = new System.Drawing.Size(90, 27);
            this.txtDonGia.Location = new System.Drawing.Point(640, 35); this.txtDonGia.Size = new System.Drawing.Size(130, 27);
            
            this.btnThemMon.Text = "Thêm hàng"; 
            this.btnThemMon.Location = new System.Drawing.Point(790, 32); 
            this.btnThemMon.Size = new System.Drawing.Size(120, 33); 
            this.btnThemMon.BackColor = System.Drawing.Color.Green;
            this.btnThemMon.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
            this.btnThemMon.Click += new System.EventHandler(this.btnThemMon_Click);
            groupHang.Controls.AddRange(new System.Windows.Forms.Control[] { l3, l4, l5, this.cbHangHoa, this.txtSoLuong, this.txtDonGia, this.btnThemMon });

            // pnlBottom (Thanh tính toán bám đáy cố định tích hợp 3 nút liên hoàn)
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Height = 65;
            this.pnlBottom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlBottom.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblTongTien, this.btnLuuPhieu, this.btnExcel, this.btnPdf });

            this.lblTongTien.Text = "TỔNG TIỀN: 0 VNĐ"; 
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold); 
            this.lblTongTien.ForeColor = System.Drawing.Color.Green;
            this.lblTongTien.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTongTien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTongTien.AutoSize = false;
            this.lblTongTien.Width = 300;

            // Nút Lưu Phiếu Xuất Kho
            this.btnLuuPhieu.Text = "LƯU PHIẾU XUẤT KHO"; 
            this.btnLuuPhieu.Width = 180; 
            this.btnLuuPhieu.BackColor = System.Drawing.Color.MediumSeaGreen; 
            this.btnLuuPhieu.ForeColor = System.Drawing.Color.White;
            this.btnLuuPhieu.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLuuPhieu.Click += new System.EventHandler(this.btnLuuPhieu_Click);

            // Nút Xuất Excel
            this.btnExcel.Text = "Xuất Excel"; 
            this.btnExcel.Width = 110; 
            this.btnExcel.BackColor = System.Drawing.Color.SteelBlue; 
            this.btnExcel.ForeColor = System.Drawing.Color.White; 
            this.btnExcel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);

            // Nút Xuất PDF
            this.btnPdf.Text = "Xuất PDF"; 
            this.btnPdf.Width = 110; 
            this.btnPdf.BackColor = System.Drawing.Color.IndianRed; 
            this.btnPdf.ForeColor = System.Drawing.Color.White; 
            this.btnPdf.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);

            // dgvChiTiet
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.AllowUserToAddRows = false;

            // Form Properties
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 600);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Text = "Lập Phiếu Xuất Kho";
            this.Load += new System.EventHandler(this.FrmXuatKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}