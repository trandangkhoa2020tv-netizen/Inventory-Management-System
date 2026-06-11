namespace QuanLyKhoHang.Forms
{
    partial class FrmKhachHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTopControls; 
        private System.Windows.Forms.DataGridView dgvKhachHang; 
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.TextBox txtTimKiem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTopControls = new System.Windows.Forms.Panel();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.dgvKhachHang = new System.Windows.Forms.DataGridView();
            
            var lblTitle = new System.Windows.Forms.Label() { Text = "QUẢN LÝ KHÁCH HÀNG", Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold), Location = new System.Drawing.Point(20, 12), AutoSize = true, ForeColor = System.Drawing.Color.DarkBlue };
            var lbl1 = new System.Windows.Forms.Label() { Text = "Tên khách hàng:", Location = new System.Drawing.Point(20, 55), AutoSize = true };
            var lbl2 = new System.Windows.Forms.Label() { Text = "Địa chỉ:", Location = new System.Drawing.Point(20, 95), AutoSize = true };
            var lbl3 = new System.Windows.Forms.Label() { Text = "Số điện thoại:", Location = new System.Drawing.Point(400, 55), AutoSize = true };
            var lbl4 = new System.Windows.Forms.Label() { Text = "Email:", Location = new System.Drawing.Point(400, 95), AutoSize = true };
            var lbl5 = new System.Windows.Forms.Label() { Text = "Ghi chú:", Location = new System.Drawing.Point(700, 55), AutoSize = true };

            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
            this.pnlTopControls.SuspendLayout();
            this.SuspendLayout();

            // pnlTopControls (Khung nhập liệu bám đỉnh cố định)
            this.pnlTopControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopControls.Height = 190;
            this.pnlTopControls.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTopControls.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTitle, this.txtTen, this.txtDiaChi, this.txtSDT, this.txtEmail, this.txtGhiChu,
                this.btnThem, this.btnSua, this.btnXoa, this.btnLamMoi, lbl1, lbl2, lbl3, lbl4, lbl5
            });

            // Tọa độ các ô TextBox nhập liệu
            this.txtTen.Location = new System.Drawing.Point(140, 52); this.txtTen.Size = new System.Drawing.Size(230, 27);
            this.txtDiaChi.Location = new System.Drawing.Point(140, 92); this.txtDiaChi.Size = new System.Drawing.Size(230, 27);
            this.txtSDT.Location = new System.Drawing.Point(500, 52); this.txtSDT.Size = new System.Drawing.Size(180, 27);
            this.txtEmail.Location = new System.Drawing.Point(500, 92); this.txtEmail.Size = new System.Drawing.Size(180, 27);
            
            this.txtGhiChu.Location = new System.Drawing.Point(770, 52); this.txtGhiChu.Size = new System.Drawing.Size(180, 67); this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);

            // KÍCH HOẠT ĐẦY ĐỦ CÁC SỰ KIỆN CLICK CHO NÚT BẤM
            this.btnThem.Text = "Thêm"; this.btnThem.Location = new System.Drawing.Point(140, 140); this.btnThem.Size = new System.Drawing.Size(85, 35);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnSua.Text = "Sửa"; this.btnSua.Location = new System.Drawing.Point(240, 140); this.btnSua.Size = new System.Drawing.Size(85, 35);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Text = "Xóa"; this.btnXoa.Location = new System.Drawing.Point(340, 140); this.btnXoa.Size = new System.Drawing.Size(85, 35);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnLamMoi.Text = "Làm Mới"; this.btnLamMoi.Location = new System.Drawing.Point(440, 140); this.btnLamMoi.Size = new System.Drawing.Size(85, 35);
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // dgvKhachHang (KÍCH HOẠT CELLCLICK ĐỂ CHỌN DÒNG DỮ LIỆU)
            this.dgvKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKhachHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhachHang.AllowUserToAddRows = false;
            this.dgvKhachHang.ReadOnly = true;
            this.dgvKhachHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhachHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKhachHang_CellClick);

            // Cấu hình Form tổng thể và kích hoạt FrmKhachHang_Load
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvKhachHang);
            this.Controls.Add(this.pnlTopControls);
            this.Text = "Quản Lý Khách Hàng";
            this.Load += new System.EventHandler(this.FrmKhachHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).EndInit();
            this.pnlTopControls.ResumeLayout(false);
            this.pnlTopControls.PerformLayout();
            this.ResumeLayout(false);

            // THÊM CHỨC NĂNG TÌM KIẾM THEO TÊN KHÁCH HÀNG
            // thêm TextBox tìm kiếm nhanh ở góc trên bên phải
            // 2. Dán đoạn cấu hình này vào trong InitializeComponent(), ngay trên đoạn cấu hình dgvHangHoa:
           // 1. Cấu hình Nhãn "Tìm kiếm nhanh:"
            var lblTimKiem = new System.Windows.Forms.Label();
            lblTimKiem.Text = "Tìm kiếm nhanh:";
            lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblTimKiem.ForeColor = System.Drawing.Color.DarkBlue;
            lblTimKiem.AutoSize = true;
            // Tọa độ mới: Cách cụm nút bấm bên trái một khoảng vừa vặn, không bị đè chữ
            lblTimKiem.Location = new System.Drawing.Point(565, 186); 

            // 2. Cấu hình Ô TextBox txtTimKiem
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            // Tọa độ mới: Nằm ngay sau nhãn chữ, thẳng hàng với hàng nút bấm
            this.txtTimKiem.Location = new System.Drawing.Point(705, 182);
            this.txtTimKiem.Size = new System.Drawing.Size(245, 27);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);

            // ======================================================================
            // QUAN TRỌNG: PHẢI CÓ 2 DÒNG NÀY ĐỂ NẠP CONTROL VÀO PANEL (TRÁNH BỊ ẨN)
            this.pnlTopControls.Controls.Add(lblTimKiem);
            this.pnlTopControls.Controls.Add(this.txtTimKiem);
        }
    }
}