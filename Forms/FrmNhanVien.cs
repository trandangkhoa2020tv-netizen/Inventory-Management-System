using System;
using System.Windows.Forms;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;
using System.Data;
namespace QuanLyKhoHang.Forms
{
    public partial class FrmNhanVien : Form
    {
        private readonly NhanVienRepository _nhanVienRepo = new NhanVienRepository();
        private int _selectedId = 0;

        // Giữ nguyên hàm khởi tạo mặc định không tham số vì nút mở form này đã bị nhân viên ẩn đi ở Menu chính rồi
        public FrmNhanVien() { InitializeComponent(); }

        private void FrmNhanVien_Load(object sender, EventArgs e) { LoadData(); }

        private void LoadData() { dgvNhanVien.DataSource = _nhanVienRepo.GetAll(); }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTen.Text.Trim())) { MessageBox.Show("Vui lòng nhập tên nhân viên!"); return; }
            _nhanVienRepo.Them(new NhanVien { TenNhanVien = txtTen.Text.Trim(), DiaChiNV = txtDiaChi.Text.Trim(), SoDienThoai = txtSDT.Text.Trim(), Email = txtEmail.Text.Trim(), ChucVu = txtChucVu.Text.Trim(), GhiChu = txtGhiChu.Text.Trim() });
            MessageBox.Show("Thêm thành công!"); ClearInputs(); LoadData();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvNhanVien.Rows[e.RowIndex];
                _selectedId = Convert.ToInt32(row.Cells["Mã NV"].Value);
                txtTen.Text = row.Cells["Tên Nhân Viên"].Value.ToString();
                txtDiaChi.Text = row.Cells["Địa Chỉ"].Value.ToString();
                txtSDT.Text = row.Cells["SĐT"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtChucVu.Text = row.Cells["Chức Vụ"].Value.ToString();
                txtGhiChu.Text = row.Cells["Ghi Chú"].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0) return;
            _nhanVienRepo.Sua(new NhanVien { MaNhanVien = _selectedId, TenNhanVien = txtTen.Text.Trim(), DiaChiNV = txtDiaChi.Text.Trim(), SoDienThoai = txtSDT.Text.Trim(), Email = txtEmail.Text.Trim(), ChucVu = txtChucVu.Text.Trim(), GhiChu = txtGhiChu.Text.Trim() });
            MessageBox.Show("Cập nhật thành công!"); ClearInputs(); LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0) return;
            if (MessageBox.Show("Bạn có muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _nhanVienRepo.Xoa(_selectedId); ClearInputs(); LoadData();
            }
        }

          // thêm chức năng tìm kiếm theo tên hàng hóa    
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
        // Lấy bảng dữ liệu DataTable đang gán làm nguồn cho DataGridView
        // Lưu ý: Nếu trong hàm Load Khoa gán dgvNhanVien.DataSource = _nhanVienRepo.GetAll(), 
        // thì Khoa cần ép kiểu nó về DataTable như dòng dưới:
        DataTable dt = dgvNhanVien.DataSource as DataTable;

        if (dt != null)
        {
            string tuKhoa = txtTimKiem.Text.Trim().Replace("'", "''"); // Loại bỏ dấu nháy đơn để tránh lỗi SQL logic

            if (string.IsNullOrEmpty(tuKhoa))
            {
                // Nếu ô tìm kiếm trống, hiển thị lại toàn bộ dữ liệu gốc
                dt.DefaultView.RowFilter = "";
            }
            else
            {
                // Xây dựng chuỗi điều kiện lọc: Tìm kiếm theo Tên hàng hoá HOẶC Loại hàng HOẶC Nhà cung cấp
                // Sử dụng từ khóa LIKE kết hợp dấu % để tìm kiếm tương đối (chứa từ khóa)
               dt.DefaultView.RowFilter = $"[Tên Nhân Viên] LIKE '%{tuKhoa}%' OR [Chức Vụ] LIKE '%{tuKhoa}%' OR [SĐT] LIKE '%{tuKhoa}%' OR [Địa Chỉ] LIKE '%{tuKhoa}%'";
            }
        }
    }

        private void btnLamMoi_Click(object sender, EventArgs e) { ClearInputs(); LoadData(); }

        private void ClearInputs() { _selectedId = 0; txtTen.Clear(); txtDiaChi.Clear(); txtSDT.Clear(); txtEmail.Clear(); txtChucVu.Clear(); txtGhiChu.Clear(); }
    }
}