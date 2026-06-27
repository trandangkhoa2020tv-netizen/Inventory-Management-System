using System;
using System.Data;
using System.Windows.Forms;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.Forms
{
    /// <summary>
    /// Form quản lý danh mục nhân viên.
    /// Form này thường chỉ dành cho Admin vì FrmMain đã ẩn menu nhân viên với tài khoản nhân viên.
    /// </summary>
    public partial class FrmNhanVien : Form
    {
        private readonly NhanVienRepository _nhanVienRepo = new NhanVienRepository();
        private int _selectedId = 0;

        public FrmNhanVien()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Nạp danh sách nhân viên khi form được mở.
        /// </summary>
        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// Đổ dữ liệu nhân viên lên DataGridView.
        /// </summary>
        private void LoadData()
        {
            dgvNhanVien.DataSource = _nhanVienRepo.GetAll();
        }

        /// <summary>
        /// Thêm nhân viên mới.
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTen.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên!");
                return;
            }

            _nhanVienRepo.Them(BuildNhanVienFromInput());
            MessageBox.Show("Thêm thành công!");
            ClearInputs();
            LoadData();
        }

        /// <summary>
        /// Khi chọn một nhân viên trên lưới, đưa dữ liệu lên form để sửa/xóa.
        /// </summary>
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
            _selectedId = Convert.ToInt32(row.Cells[0].Value);
            txtTen.Text = row.Cells[1].Value?.ToString();
            txtDiaChi.Text = row.Cells[2].Value?.ToString();
            txtSDT.Text = row.Cells[3].Value?.ToString();
            txtEmail.Text = row.Cells[4].Value?.ToString();
            txtChucVu.Text = row.Cells[5].Value?.ToString();
            txtGhiChu.Text = row.Cells[6].Value?.ToString();
        }

        /// <summary>
        /// Cập nhật nhân viên đang chọn.
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                return;
            }

            NhanVien nv = BuildNhanVienFromInput();
            nv.MaNhanVien = _selectedId;
            _nhanVienRepo.Sua(nv);
            MessageBox.Show("Cập nhật thành công!");
            ClearInputs();
            LoadData();
        }

        /// <summary>
        /// Xóa nhân viên đang chọn.
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                return;
            }

            if (MessageBox.Show("Bạn có muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _nhanVienRepo.Xoa(_selectedId);
                ClearInputs();
                LoadData();
            }
        }

        /// <summary>
        /// Tìm kiếm nhanh theo mã, tên, chức vụ, số điện thoại hoặc địa chỉ.
        /// </summary>
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dgvNhanVien.DataSource is not DataTable dt)
            {
                return;
            }

            string tuKhoa = txtTimKiem.Text.Trim().Replace("'", "''");
            if (string.IsNullOrEmpty(tuKhoa))
            {
                dt.DefaultView.RowFilter = string.Empty;
                return;
            }

            dt.DefaultView.RowFilter =
                $"Convert([{dt.Columns[0].ColumnName}], 'System.String') LIKE '%{tuKhoa}%' " +
                $"OR [{dt.Columns[1].ColumnName}] LIKE '%{tuKhoa}%' " +
                $"OR [{dt.Columns[2].ColumnName}] LIKE '%{tuKhoa}%' " +
                $"OR [{dt.Columns[3].ColumnName}] LIKE '%{tuKhoa}%' " +
                $"OR [{dt.Columns[5].ColumnName}] LIKE '%{tuKhoa}%'";
        }

        /// <summary>
        /// Làm mới danh sách và xóa trạng thái chọn.
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
            LoadData();
        }

        /// <summary>
        /// Gom dữ liệu nhập liệu thành model NhanVien.
        /// </summary>
        private NhanVien BuildNhanVienFromInput()
        {
            return new NhanVien
            {
                TenNhanVien = txtTen.Text.Trim(),
                DiaChiNV = txtDiaChi.Text.Trim(),
                SoDienThoai = txtSDT.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                ChucVu = txtChucVu.Text.Trim(),
                GhiChu = txtGhiChu.Text.Trim()
            };
        }

        /// <summary>
        /// Xóa các ô nhập trên form.
        /// </summary>
        private void ClearInputs()
        {
            _selectedId = 0;
            txtTen.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtChucVu.Clear();
            txtGhiChu.Clear();
        }
    }
}
