namespace QuanLyKhoHang.Forms
{
    partial class FrmNhanVien
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTopControls; 
        private System.Windows.Forms.DataGridView dgvNhanVien; 
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtChucVu;
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
            this.txtChucVu = new System.Windows.Forms.TextBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();

            var lblTitle = new System.Windows.Forms.Label() { Text = "QUẢN LÝ NHÂN VIÊN", Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold), Location = new System.Drawing.Point(20, 12), AutoSize = true, ForeColor = System.Drawing.Color.DarkBlue };
            var lbl1 = new System.Windows.Forms.Label() { Text = "Tên nhân viên:", Location = new System.Drawing.Point(20, 55), AutoSize = true };
            var lbl2 = new System.Windows.Forms.Label() { Text = "Địa chỉ:", Location = new System.Drawing.Point(20, 95), AutoSize = true };
            var lbl3 = new System.Windows.Forms.Label() { Text = "Số điện thoại:", Location = new System.Drawing.Point(400, 55), AutoSize = true };
            var lbl4 = new System.Windows.Forms.Label() { Text = "Email:", Location = new System.Drawing.Point(400, 95), AutoSize = true };
            var lbl5 = new System.Windows.Forms.Label() { Text = "Chức vụ:", Location = new System.Drawing.Point(700, 55), AutoSize = true };
            var lbl6 = new System.Windows.Forms.Label() { Text = "Ghi chú:", Location = new System.Drawing.Point(700, 95), AutoSize = true };

            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            this.pnlTopControls.SuspendLayout();
            this.SuspendLayout();

            // pnlTopControls (Khung bám đỉnh)
            this.pnlTopControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopControls.Height = 190;
            this.pnlTopControls.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTopControls.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTitle, this.txtTen, this.txtDiaChi, this.txtSDT, this.txtEmail, this.txtChucVu, this.txtGhiChu,
                this.btnThem, this.btnSua, this.btnXoa, this.btnLamMoi, lbl1, lbl2, lbl3, lbl4, lbl5, lbl6
            });

            this.txtTen.Location = new System.Drawing.Point(140, 52); this.txtTen.Size = new System.Drawing.Size(230, 27);
            this.txtDiaChi.Location = new System.Drawing.Point(140, 92); this.txtDiaChi.Size = new System.Drawing.Size(230, 27);
            this.txtSDT.Location = new System.Drawing.Point(500, 52); this.txtSDT.Size = new System.Drawing.Size(170, 27);
            this.txtEmail.Location = new System.Drawing.Point(500, 92); this.txtEmail.Size = new System.Drawing.Size(170, 27);
            
            this.txtChucVu.Location = new System.Drawing.Point(770, 52); this.txtChucVu.Size = new System.Drawing.Size(180, 27);
            this.txtChucVu.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            
            this.txtGhiChu.Location = new System.Drawing.Point(770, 92); this.txtGhiChu.Size = new System.Drawing.Size(180, 47); this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);

            // KÍCH HOẠT SỰ KIỆN KHÓA CLICK CHO CÁC NÚT BẤM TẠI ĐÂY
            this.btnThem.Text = "Thêm"; this.btnThem.Location = new System.Drawing.Point(140, 145); this.btnThem.Size = new System.Drawing.Size(85, 35);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnSua.Text = "Sửa"; this.btnSua.Location = new System.Drawing.Point(240, 145); this.btnSua.Size = new System.Drawing.Size(85, 35);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Text = "Xóa"; this.btnXoa.Location = new System.Drawing.Point(340, 145); this.btnXoa.Size = new System.Drawing.Size(85, 35);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnLamMoi.Text = "Làm Mới"; this.btnLamMoi.Location = new System.Drawing.Point(440, 145); this.btnLamMoi.Size = new System.Drawing.Size(85, 35);
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // dgvNhanVien (KÍCH HOẠT CELLCLICK ĐỂ ĐỔ DỮ LIỆU LÊN Ô NHẬP)
            this.dgvNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhanVien_CellClick);

            // Form Properties
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvNhanVien);
            this.Controls.Add(this.pnlTopControls);
            this.Text = "Quản Lý Nhân Viên";
            this.Load += new System.EventHandler(this.FrmNhanVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            this.pnlTopControls.ResumeLayout(false);
            this.pnlTopControls.PerformLayout();
            this.ResumeLayout(false);

            // THÊM CHỨC NĂNG TÌM KIẾM THEO TÊN NHÂN VIÊN, CHỨC VỤ, SĐT, ĐỊA CHỈ
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