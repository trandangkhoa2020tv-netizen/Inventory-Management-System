using System;
using System.Data;
using System.Windows.Forms;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.Forms
{
    public partial class FrmHangHoa : Form
    {
        private readonly HangHoaRepository _hangHoaRepo = new HangHoaRepository();
        private readonly LoaiHangRepository _loaiHangRepo = new LoaiHangRepository();
        private readonly NhaCungCapRepository _nccRepo = new NhaCungCapRepository();
        private int _selectedId = 0; // Lưu ID dòng đang chọn để Sửa/Xóa
        private string _vaiTro = "NhanVien"; // ĐÃ THÊM: Biến lưu vai trò

        // ĐÃ SỬA: Hàm khởi tạo nhận tham số từ FrmMain truyền xuống
        public FrmHangHoa(string vaiTro)
        {
            InitializeComponent();
            this._vaiTro = vaiTro;
        }

        private void FrmHangHoa_Load(object sender, EventArgs e)
        {
            LoadDataGrid();
            LoadComboBoxes();

            // ĐÃ THÊM: Phân quyền khóa nút Xóa nếu là Nhân viên
            if (_vaiTro == "NhanVien")
            {
                btnXoa.Enabled = false; // Nhân viên chỉ xem, thêm, sửa danh mục, không được xóa bậy
            }
            else
            {
                btnXoa.Enabled = true;  // Admin có quyền xóa
            }
        }

        private void LoadDataGrid()
        {
            dgvHangHoa.DataSource = _hangHoaRepo.GetAll();
            if (dgvHangHoa.Columns["Mã Hàng"] != null)
                dgvHangHoa.Columns["Mã Hàng"].Width = 70;
        }

        private void LoadComboBoxes()
        {
            DataTable dtLoai = _loaiHangRepo.GetAll();
            cbLoaiHang.DataSource = dtLoai;
            cbLoaiHang.DisplayMember = "Tên Loại Hàng";
            cbLoaiHang.ValueMember = "Mã Loại";

            DataTable dtNcc = _nccRepo.GetAll();
            cbNhaCungCap.DataSource = dtNcc;
            cbNhaCungCap.DisplayMember = "Tên Nhà Cung Cấp";
            cbNhaCungCap.ValueMember = "Mã NCC";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenHang.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên hàng hóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                HangHoa hh = new HangHoa
                {
                    TenHangHoa = txtTenHang.Text.Trim(),
                    MaLoaiHang = Convert.ToInt32(cbLoaiHang.SelectedValue),
                    MaNhaCungCap = Convert.ToInt32(cbNhaCungCap.SelectedValue),
                    GiaNhap = string.IsNullOrEmpty(txtGiaNhap.Text) ? 0 : Convert.ToDecimal(txtGiaNhap.Text),
                    GiaBan = string.IsNullOrEmpty(txtGiaBan.Text) ? 0 : Convert.ToDecimal(txtGiaBan.Text),
                    SoLuongTon = string.IsNullOrEmpty(txtSoLuong.Text) ? 0 : Convert.ToInt32(txtSoLuong.Text),
                    DonViTinh = txtDVT.Text.Trim(),
                    GhiChu = txtGhiChu.Text.Trim()
                };

                _hangHoaRepo.Them(hh);
                MessageBox.Show("Thêm mới hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHangHoa.Rows[e.RowIndex];
                _selectedId = Convert.ToInt32(row.Cells["Mã Hàng"].Value);
                txtTenHang.Text = row.Cells["Tên Hàng Hóa"].Value.ToString();
                txtGiaNhap.Text = row.Cells["Giá Nhập"].Value.ToString();
                txtGiaBan.Text = row.Cells["Giá Bán"].Value.ToString();
                txtSoLuong.Text = row.Cells["Tồn Kho"].Value.ToString();
                txtDVT.Text = row.Cells["ĐVT"].Value.ToString();
                txtGhiChu.Text = row.Cells["Ghi Chú"].Value.ToString();

                cbLoaiHang.Text = row.Cells["Loại Hàng"].Value.ToString();
                cbNhaCungCap.Text = row.Cells["Nhà Cung Cấp"].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                MessageBox.Show("Vui lòng chọn một mặt hàng từ bảng để sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                HangHoa hh = new HangHoa
                {
                    MaHangHoa = _selectedId,
                    TenHangHoa = txtTenHang.Text.Trim(),
                    MaLoaiHang = Convert.ToInt32(cbLoaiHang.SelectedValue),
                    MaNhaCungCap = Convert.ToInt32(cbNhaCungCap.SelectedValue),
                    GiaNhap = Convert.ToDecimal(txtGiaNhap.Text),
                    GiaBan = Convert.ToDecimal(txtGiaBan.Text),
                    SoLuongTon = Convert.ToInt32(txtSoLuong.Text),
                    DonViTinh = txtDVT.Text.Trim(),
                    GhiChu = txtGhiChu.Text.Trim()
                };

                _hangHoaRepo.Sua(hh);
                MessageBox.Show("Cập nhật hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                MessageBox.Show("Vui lòng chọn một mặt hàng từ bảng để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa mặt hàng này khỏi danh mục?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _hangHoaRepo.Xoa(_selectedId);
                    MessageBox.Show("Xóa hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadDataGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xóa hàng hóa này (Mặt hàng này đang nằm trong phiếu nhập/xuất)!\nChi tiết: {ex.Message}", "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
            LoadDataGrid();
        }

        private void ClearInputs()
        {
            _selectedId = 0;
            txtTenHang.Clear();
            txtGiaNhap.Clear();
            txtGiaBan.Clear();
            txtSoLuong.Clear();
            txtDVT.Clear();
            txtGhiChu.Clear();
            if (cbLoaiHang.Items.Count > 0) cbLoaiHang.SelectedIndex = 0;
            if (cbNhaCungCap.Items.Count > 0) cbNhaCungCap.SelectedIndex = 0;
            txtTenHang.Focus();
        }
    }
}