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
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblTimKiem;
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();

            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
            this.pnlTopControls.SuspendLayout();
            this.SuspendLayout();

            // pnlTopControls (Khung nhập liệu bám đỉnh cố định)
            this.pnlTopControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopControls.Height = 230;
            this.pnlTopControls.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTopControls.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.txtTen, this.txtDiaChi, this.txtSDT, this.txtEmail, this.txtGhiChu,
                this.btnThem, this.btnSua, this.btnXoa, this.btnLamMoi,
                this.lblTen, this.lblDiaChi, this.lblSDT, this.lblEmail, this.lblGhiChu,
                this.lblTimKiem, this.txtTimKiem
            });

            this.lblTitle.Text = "QUẢN LÝ KHÁCH HÀNG";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.AutoSize = true;
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;

            this.lblTen.Text = "Tên khách hàng:";
            this.lblTen.Location = new System.Drawing.Point(20, 55);
            this.lblTen.AutoSize = true;

            this.lblDiaChi.Text = "Địa chỉ:";
            this.lblDiaChi.Location = new System.Drawing.Point(20, 95);
            this.lblDiaChi.AutoSize = true;

            this.lblSDT.Text = "Số điện thoại:";
            this.lblSDT.Location = new System.Drawing.Point(400, 55);
            this.lblSDT.AutoSize = true;

            this.lblEmail.Text = "Email:";
            this.lblEmail.Location = new System.Drawing.Point(400, 95);
            this.lblEmail.AutoSize = true;

            this.lblGhiChu.Text = "Ghi chú:";
            this.lblGhiChu.Location = new System.Drawing.Point(700, 55);
            this.lblGhiChu.AutoSize = true;

            // Tọa độ các ô TextBox nhập liệu
            this.txtTen.Location = new System.Drawing.Point(140, 52); this.txtTen.Size = new System.Drawing.Size(230, 27);
            this.txtDiaChi.Location = new System.Drawing.Point(140, 92); this.txtDiaChi.Size = new System.Drawing.Size(230, 27);
            this.txtSDT.Location = new System.Drawing.Point(500, 52); this.txtSDT.Size = new System.Drawing.Size(180, 27);
            this.txtEmail.Location = new System.Drawing.Point(500, 92); this.txtEmail.Size = new System.Drawing.Size(180, 27);
            
            this.txtGhiChu.Location = new System.Drawing.Point(770, 52); this.txtGhiChu.Size = new System.Drawing.Size(180, 67); this.txtGhiChu.Multiline = true;
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // KÍCH HOẠT ĐẦY ĐỦ CÁC SỰ KIỆN CLICK CHO NÚT BẤM
            this.btnThem.Text = "Thêm"; this.btnThem.Location = new System.Drawing.Point(140, 180); this.btnThem.Size = new System.Drawing.Size(95, 35);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnSua.Text = "Sửa"; this.btnSua.Location = new System.Drawing.Point(250, 180); this.btnSua.Size = new System.Drawing.Size(95, 35);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Text = "Xóa"; this.btnXoa.Location = new System.Drawing.Point(360, 180); this.btnXoa.Size = new System.Drawing.Size(95, 35);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnLamMoi.Text = "Làm Mới"; this.btnLamMoi.Location = new System.Drawing.Point(470, 180); this.btnLamMoi.Size = new System.Drawing.Size(95, 35);
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // dgvKhachHang (KÍCH HOẠT CELLCLICK ĐỂ CHỌN DÒNG DỮ LIỆU)
            this.dgvKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKhachHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhachHang.AllowUserToAddRows = false;
            this.dgvKhachHang.ReadOnly = true;
            this.dgvKhachHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhachHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKhachHang_CellClick);

            // Cấu hình Form tổng thể và kích hoạt FrmKhachHang_Load
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
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
            this.lblTimKiem.Text = "Tìm kiếm nhanh:";
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTimKiem.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTimKiem.AutoSize = true;
            // Tọa độ mới: Cách cụm nút bấm bên trái một khoảng vừa vặn, không bị đè chữ
            this.lblTimKiem.Location = new System.Drawing.Point(565, 186); 

            // 2. Cấu hình Ô TextBox txtTimKiem
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            // Tọa độ mới: Nằm ngay sau nhãn chữ, thẳng hàng với hàng nút bấm
            this.txtTimKiem.Location = new System.Drawing.Point(705, 182);
            this.txtTimKiem.Size = new System.Drawing.Size(245, 27);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
        }
    }
}
