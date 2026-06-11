using System;
using System.Windows.Forms;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;
using System.Data;

namespace QuanLyKhoHang.Forms
{
    public partial class FrmKhachHang : Form
    {
        private readonly KhachHangRepository _khachHangRepo = new KhachHangRepository();
        private int _selectedId = 0;
        private string _vaiTro = "NhanVien"; // ĐÃ THÊM: Biến lưu vai trò

        // ĐÃ SỬA: Hàm khởi tạo nhận tham số vai trò truyền vào từ FrmMain
        public FrmKhachHang(string vaiTro) 
        { 
            InitializeComponent(); 
            this._vaiTro = vaiTro;
        }

        private void FrmKhachHang_Load(object sender, EventArgs e) 
        { 
            LoadData(); 

            // ĐÃ THÊM: Khóa chức năng Xóa khách hàng đối với Nhân viên
            if (_vaiTro == "NhanVien")
            {
                btnXoa.Enabled = false; // Nhân viên không được quyền xóa khách hàng
            }
            else
            {
                btnXoa.Enabled = true;  // Admin toàn quyền
            }
        }

        private void LoadData() { dgvKhachHang.DataSource = _khachHangRepo.GetAll(); }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTen.Text.Trim())) { MessageBox.Show("Vui lòng nhập tên khách hàng!"); return; }
            _khachHangRepo.Them(new KhachHang { TenKhachHang = txtTen.Text.Trim(), DiaChiKH = txtDiaChi.Text.Trim(), SoDienThoai = txtSDT.Text.Trim(), Email = txtEmail.Text.Trim(), GhiChu = txtGhiChu.Text.Trim() });
            MessageBox.Show("Thêm thành công!"); ClearInputs(); LoadData();
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvKhachHang.Rows[e.RowIndex];
                _selectedId = Convert.ToInt32(row.Cells["Mã KH"].Value);
                txtTen.Text = row.Cells["Tên Khách Hàng"].Value.ToString();
                txtDiaChi.Text = row.Cells["Địa Chỉ"].Value.ToString();
                txtSDT.Text = row.Cells["SĐT"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtGhiChu.Text = row.Cells["Ghi Chú"].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0) return;
            _khachHangRepo.Sua(new KhachHang { MaKhachHang = _selectedId, TenKhachHang = txtTen.Text.Trim(), DiaChiKH = txtDiaChi.Text.Trim(), SoDienThoai = txtSDT.Text.Trim(), Email = txtEmail.Text.Trim(), GhiChu = txtGhiChu.Text.Trim() });
            MessageBox.Show("Cập nhật thành công!"); ClearInputs(); LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0) return;
            if (MessageBox.Show("Bạn có muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _khachHangRepo.Xoa(_selectedId); ClearInputs(); LoadData();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
{
    // Lấy bảng dữ liệu DataTable từ lưới dgvKhachHang
    System.Data.DataTable dt = dgvKhachHang.DataSource as System.Data.DataTable;

    if (dt != null)
    {
        // Loại bỏ dấu nháy đơn để tránh lỗi logic bộ lọc
        string tuKhoa = txtTimKiem.Text.Trim().Replace("'", "''"); 

        if (string.IsNullOrEmpty(tuKhoa))
        {
            // Nếu ô tìm kiếm trống, hiển thị lại toàn bộ danh sách gốc
            dt.DefaultView.RowFilter = "";
        }
        else
        {
            // ĐÃ SỬA: Ép kiểu cột [Mã KH] và [SĐT] sang String để tìm kiếm tương đối bằng LIKE
            dt.DefaultView.RowFilter = $"[Tên Khách Hàng] LIKE '%{tuKhoa}%' " +
                                       $"OR [Địa Chỉ] LIKE '%{tuKhoa}%' " +
                                       $"OR [Email] LIKE '%{tuKhoa}%' " +
                                       $"OR [Ghi Chú] LIKE '%{tuKhoa}%' " +
                                       $"OR [SĐT] LIKE '%{tuKhoa}%' " + 
                                       $"OR Convert([Mã KH], 'System.String') LIKE '%{tuKhoa}%'";
            }
        }
    }

        private void btnLamMoi_Click(object sender, EventArgs e) { ClearInputs(); LoadData(); }

        private void ClearInputs() { _selectedId = 0; txtTen.Clear(); txtDiaChi.Clear(); txtSDT.Clear(); txtEmail.Clear(); txtGhiChu.Clear(); }
    }
}