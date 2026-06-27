using System;
using System.Data;
using System.Windows.Forms;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.Forms
{
    /// <summary>
    /// Form quản lý danh mục khách hàng.
    /// Khách hàng được dùng trong nghiệp vụ xuất kho/bán hàng.
    /// </summary>
    public partial class FrmKhachHang : Form
    {
        private readonly KhachHangRepository _khachHangRepo = new KhachHangRepository();
        private int _selectedId = 0;
        private readonly string _vaiTro;

        public FrmKhachHang(string vaiTro)
        {
            InitializeComponent();
            _vaiTro = vaiTro;
        }

        /// <summary>
        /// Nạp dữ liệu khách hàng và khóa nút xóa nếu người dùng là nhân viên.
        /// </summary>
        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
            btnXoa.Enabled = _vaiTro != "NhanVien";
        }

        /// <summary>
        /// Nạp danh sách khách hàng lên lưới.
        /// </summary>
        private void LoadData()
        {
            dgvKhachHang.DataSource = _khachHangRepo.GetAll();
        }

        /// <summary>
        /// Thêm khách hàng mới từ dữ liệu nhập trên form.
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTen.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!");
                return;
            }

            _khachHangRepo.Them(BuildKhachHangFromInput());
            MessageBox.Show("Thêm thành công!");
            ClearInputs();
            LoadData();
        }

        /// <summary>
        /// Khi chọn khách hàng trên lưới, đưa dữ liệu lên form để sửa/xóa.
        /// </summary>
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
            _selectedId = Convert.ToInt32(row.Cells[0].Value);
            txtTen.Text = row.Cells[1].Value?.ToString();
            txtDiaChi.Text = row.Cells[2].Value?.ToString();
            txtSDT.Text = row.Cells[3].Value?.ToString();
            txtEmail.Text = row.Cells[4].Value?.ToString();
            txtGhiChu.Text = row.Cells[5].Value?.ToString();
        }

        /// <summary>
        /// Cập nhật khách hàng đang được chọn.
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                return;
            }

            KhachHang kh = BuildKhachHangFromInput();
            kh.MaKhachHang = _selectedId;
            _khachHangRepo.Sua(kh);
            MessageBox.Show("Cập nhật thành công!");
            ClearInputs();
            LoadData();
        }

        /// <summary>
        /// Xóa khách hàng đang được chọn.
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                return;
            }

            if (MessageBox.Show("Bạn có muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _khachHangRepo.Xoa(_selectedId);
                ClearInputs();
                LoadData();
            }
        }

        /// <summary>
        /// Tìm kiếm nhanh theo mã, tên, địa chỉ, số điện thoại, email hoặc ghi chú.
        /// </summary>
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dgvKhachHang.DataSource is not DataTable dt)
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
                $"OR [{dt.Columns[4].ColumnName}] LIKE '%{tuKhoa}%' " +
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
        /// Gom dữ liệu nhập liệu thành model KhachHang.
        /// </summary>
        private KhachHang BuildKhachHangFromInput()
        {
            return new KhachHang
            {
                TenKhachHang = txtTen.Text.Trim(),
                DiaChiKH = txtDiaChi.Text.Trim(),
                SoDienThoai = txtSDT.Text.Trim(),
                Email = txtEmail.Text.Trim(),
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
            txtGhiChu.Clear();
        }
    }
}
