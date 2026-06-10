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
        private System.Windows.Forms.DataGridView dgvLichSuPhieu; 
        private System.Windows.Forms.Button btnThemMon;
        private System.Windows.Forms.Button btnLuuPhieu;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPdf;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblLichSuTitle; 
        private System.Windows.Forms.TextBox txtGhiChuPhieu; // Thêm linh kiện ô nhập Ghi chú gõ tay

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
            this.dgvLichSuPhieu = new System.Windows.Forms.DataGridView(); 
            this.btnThemMon = new System.Windows.Forms.Button();
            this.btnLuuPhieu = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPdf = new System.Windows.Forms.Button();
            this.lblTongTien = new System.Windows.Forms.Label();

            this.lblLichSuTitle = new System.Windows.Forms.Label() { Text = " LỊCH SỬ DANH SÁCH CÁC PHIẾU XUẤT ĐÃ LƯU TRÊN HỆ THỐNG (Click chọn dòng để xuất file):", Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold), BackColor = System.Drawing.Color.LightGray, ForeColor = System.Drawing.Color.Black, Dock = System.Windows.Forms.DockStyle.Top, Height = 30, TextAlign = System.Drawing.ContentAlignment.MiddleLeft };
            var title = new System.Windows.Forms.Label() { Text = "LẬP PHIẾU XUẤT KHO (BÁN HÀNG)", Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold), Location = new System.Drawing.Point(20, 12), AutoSize = true, ForeColor = System.Drawing.Color.DarkBlue };
            
            // ĐÃ SỬA: Tăng chiều cao lên 115 để chứa vừa ô ghi chú hàng thứ 2
            var groupPhieu = new System.Windows.Forms.GroupBox() { Text = "Thông tin hóa đơn", Location = new System.Drawing.Point(20, 50), Size = new System.Drawing.Size(940, 115), Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right) };
            
            // ĐÃ SỬA: Đẩy lùi group chọn hàng xuống vị trí 175 để giao diện thoáng đẹp
            var groupHang = new System.Windows.Forms.GroupBox() { Text = "Chọn hàng xuất kho", Location = new System.Drawing.Point(20, 175), Size = new System.Drawing.Size(940, 80), Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right) };

            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuPhieu)).BeginInit(); 
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            // pnlTop (ĐÃ SỬA: Tăng chiều cao tổng thể lên 270)
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 270;
            this.pnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTop.Controls.AddRange(new System.Windows.Forms.Control[] { title, groupPhieu, groupHang });

            var l1 = new System.Windows.Forms.Label() { Text = "Khách hàng:", Location = new System.Drawing.Point(15, 33), AutoSize = true };
            var l2 = new System.Windows.Forms.Label() { Text = "Nhân viên xuất:", Location = new System.Drawing.Point(450, 33), AutoSize = true };
            
            // ĐÃ THÊM: Nhãn và cấu hình ô TextBox nhập ghi chú xuất kho thủ công
            var lGhiChu = new System.Windows.Forms.Label() { Text = "Ghi chú phiếu:", Location = new System.Drawing.Point(15, 73), AutoSize = true };
            this.txtGhiChuPhieu = new System.Windows.Forms.TextBox();
            this.txtGhiChuPhieu.Location = new System.Drawing.Point(120, 70); 
            this.txtGhiChuPhieu.Size = new System.Drawing.Size(720, 27);

            this.cbKhachHang.Location = new System.Drawing.Point(120, 30); this.cbKhachHang.Size = new System.Drawing.Size(280, 27); this.cbKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNhanVien.Location = new System.Drawing.Point(560, 30); this.cbNhanVien.Size = new System.Drawing.Size(280, 27); this.cbNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            
            // Gắn linh kiện mới vào cấu trúc bảng thông tin hóa đơn
            groupPhieu.Controls.AddRange(new System.Windows.Forms.Control[] { l1, l2, this.cbKhachHang, this.cbNhanVien, lGhiChu, this.txtGhiChuPhieu });

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
            this.btnThemMon.ForeColor = System.Drawing.Color.White;
            this.btnThemMon.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
            this.btnThemMon.Click += new System.EventHandler(this.btnThemMon_Click);
            groupHang.Controls.AddRange(new System.Windows.Forms.Control[] { l3, l4, l5, this.cbHangHoa, this.txtSoLuong, this.txtDonGia, this.btnThemMon });

            // pnlBottom
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

            this.btnLuuPhieu.Text = "LƯU PHIẾU XUẤT KHO"; 
            this.btnLuuPhieu.Width = 180; 
            this.btnLuuPhieu.BackColor = System.Drawing.Color.MediumSeaGreen; 
            this.btnLuuPhieu.ForeColor = System.Drawing.Color.White;
            this.btnLuuPhieu.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLuuPhieu.Click += new System.EventHandler(this.btnLuuPhieu_Click);

            this.btnExcel.Text = "Xuất Excel"; 
            this.btnExcel.Width = 110; 
            this.btnExcel.BackColor = System.Drawing.Color.SteelBlue; 
            this.btnExcel.ForeColor = System.Drawing.Color.White; 
            this.btnExcel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);

            this.btnPdf.Text = "Xuất PDF"; 
            this.btnPdf.Width = 110; 
            this.btnPdf.BackColor = System.Drawing.Color.IndianRed; 
            this.btnPdf.ForeColor = System.Drawing.Color.White; 
            this.btnPdf.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);

            // dgvChiTiet
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvChiTiet.Height = 160;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.AllowUserToAddRows = false;

            // dgvLichSuPhieu
            this.dgvLichSuPhieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLichSuPhieu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLichSuPhieu.AllowUserToAddRows = false;
            this.dgvLichSuPhieu.ReadOnly = true;
            this.dgvLichSuPhieu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSuPhieu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLichSuPhieu_CellClick);

            // Ép phân tách dấu chấm định dạng Việt Nam
            var cultureVN = new System.Globalization.CultureInfo("vi-VN");

            this.dgvChiTiet.DataBindingComplete += (s, e) => {
                if (this.dgvChiTiet.Columns["Đơn Giá"] != null) {
                    this.dgvChiTiet.Columns["Đơn Giá"].DefaultCellStyle.FormatProvider = cultureVN;
                    this.dgvChiTiet.Columns["Đơn Giá"].DefaultCellStyle.Format = "N0";
                    this.dgvChiTiet.Columns["Đơn Giá"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                }
                if (this.dgvChiTiet.Columns["Thành Tiền"] != null) {
                    this.dgvChiTiet.Columns["Thành Tiền"].DefaultCellStyle.FormatProvider = cultureVN;
                    this.dgvChiTiet.Columns["Thành Tiền"].DefaultCellStyle.Format = "N0";
                    this.dgvChiTiet.Columns["Thành Tiền"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                }
            };

            this.dgvLichSuPhieu.DataBindingComplete += (s, e) => {
                if (this.dgvLichSuPhieu.Columns["Tổng Tiền"] != null) {
                    this.dgvLichSuPhieu.Columns["Tổng Tiền"].DefaultCellStyle.FormatProvider = cultureVN;
                    this.dgvLichSuPhieu.Columns["Tổng Tiền"].DefaultCellStyle.Format = "N0";
                    this.dgvLichSuPhieu.Columns["Tổng Tiền"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                }
            };

            // Form Properties
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 680); 
            
            this.Controls.Add(this.dgvLichSuPhieu);
            this.Controls.Add(this.lblLichSuTitle); 
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            
            this.Text = "Lập Phiếu Xuất Kho";
            this.Load += new System.EventHandler(this.FrmXuatKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuPhieu)).EndInit(); 
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}