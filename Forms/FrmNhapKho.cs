using System;
using System.Data;
using System.Windows.Forms;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.Forms
{
    public partial class FrmNhapKho : Form
    {
        private readonly PhieuNhapRepository _pnRepo = new PhieuNhapRepository();
        private readonly NhaCungCapRepository _nccRepo = new NhaCungCapRepository();
        private readonly NhanVienRepository _nvRepo = new NhanVienRepository();
        private readonly HangHoaRepository _hhRepo = new HangHoaRepository();
        
        private DataTable _dtChiTietLocal;
        private decimal _tongTienPhieu = 0;
        private string _vaiTro = "NhanVien";
        private int _maPhieuDuocChon = 0; 

        public FrmNhapKho(string vaiTro) 
        { 
            InitializeComponent(); 
            this._vaiTro = vaiTro;
        }

        private void FrmNhapKho_Load(object sender, EventArgs e)
        {
            cbNCC.DataSource = _nccRepo.GetAll(); cbNCC.DisplayMember = "Tên Nhà Cung Cấp"; cbNCC.ValueMember = "Mã NCC";
            cbNhanVien.DataSource = _nvRepo.GetAll(); cbNhanVien.DisplayMember = "Tên Nhân Viên"; cbNhanVien.ValueMember = "Mã NV";
            cbHangHoa.DataSource = _hhRepo.GetAll(); cbHangHoa.DisplayMember = "Tên Hàng Hóa"; cbHangHoa.ValueMember = "Mã Hàng";

            _dtChiTietLocal = new DataTable();
            _dtChiTietLocal.Columns.Add("MaHang", typeof(int));
            _dtChiTietLocal.Columns.Add("Tên Mặt Hàng", typeof(string));
            _dtChiTietLocal.Columns.Add("Số Lượng", typeof(int));
            _dtChiTietLocal.Columns.Add("Đơn Giá", typeof(decimal));
            _dtChiTietLocal.Columns.Add("Thành Tiền", typeof(decimal));
            dgvChiTiet.DataSource = _dtChiTietLocal;

            HienThiLichSuPhieu();
        }

        private void HienThiLichSuPhieu()
        {
            try
            {
                dgvLichSuPhieu.DataSource = _pnRepo.GetAllPhieuNhap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể nạp danh sách lịch sử phiếu nhập: " + ex.Message, "Lỗi");
            }
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (cbHangHoa.SelectedValue == null || string.IsNullOrEmpty(txtSoLuong.Text) || string.IsNullOrEmpty(txtDonGia.Text)) return;
            
            int maHang = Convert.ToInt32(cbHangHoa.SelectedValue);
            string tenHang = cbHangHoa.Text;
            int soLuong = Convert.ToInt32(txtSoLuong.Text);
            decimal donGia = Convert.ToDecimal(txtDonGia.Text);
            decimal thanhTien = soLuong * donGia;

            _dtChiTietLocal.Rows.Add(maHang, tenHang, soLuong, donGia, thanhTien);
            TinhTongTien();
            txtSoLuong.Clear(); txtDonGia.Clear();
        }

        private void TinhTongTien()
        {
            _tongTienPhieu = 0;
            foreach (DataRow r in _dtChiTietLocal.Rows) _tongTienPhieu += Convert.ToDecimal(r["Thành Tiền"]);
            lblTongTien.Text = string.Format(new System.Globalization.CultureInfo("vi-VN"), "TỔNG TIỀN: {0:N0} VNĐ", _tongTienPhieu);
        }

        private void dgvLichSuPhieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLichSuPhieu.Rows[e.RowIndex];
                _maPhieuDuocChon = Convert.ToInt32(row.Cells["Mã Phiếu"].Value);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (_maPhieuDuocChon == 0)
            {
                MessageBox.Show("Vui lòng click chọn một phiếu nhập từ bảng lịch sử bên dưới trước khi bấm xuất Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable dtChiTietPhieu = _pnRepo.GetChiTietTheoMaPhieu(_maPhieuDuocChon);
                QuanLyKhoHang.Reports.ExportExcel.ToExcel(dtChiTietPhieu, $"Bao_Cao_Phieu_Nhap_So_{_maPhieuDuocChon}");
                MessageBox.Show($"Đã xuất file báo cáo Excel cho phiếu nhập số {_maPhieuDuocChon} thành công!", "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trích xuất dữ liệu Excel: " + ex.Message, "Lỗi");
            }
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (_maPhieuDuocChon == 0)
            {
                MessageBox.Show("Vui lòng click chọn một phiếu nhập từ bảng lịch sử bên dưới trước khi bấm in hóa đơn PDF!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable dtChiTietPhieu = _pnRepo.GetChiTietTheoMaPhieu(_maPhieuDuocChon);
                QuanLyKhoHang.Reports.ExportPdf.ToPdf(dtChiTietPhieu, $"Hoa_Don_Phieu_Nhap_So_{_maPhieuDuocChon}");
                MessageBox.Show($"Đã xuất file hóa đơn PDF cho phiếu nhập số {_maPhieuDuocChon} thành công!", "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi bóc tách in hóa đơn PDF: " + ex.Message, "Lỗi");
            }
        }

        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (_dtChiTietLocal.Rows.Count == 0) { MessageBox.Show("Chưa có mặt hàng nào!"); return; }

            try
            {
                // ĐÃ SỬA: Lấy chính xác chuỗi ghi chú động do người dùng gõ tay từ TextBox
                string ghiChuThucTe = string.IsNullOrWhiteSpace(txtGhiChuPhieu.Text) ? "" : txtGhiChuPhieu.Text;

                // ĐÃ SỬA: Gán ghiChuThucTe vào thực thể Phiếu Nhập thay vì chữ "Nhập kho phần mềm"
                PhieuNhap pn = new PhieuNhap { 
                    MaNhaCungCap = Convert.ToInt32(cbNCC.SelectedValue), 
                    MaNhanVien = Convert.ToInt32(cbNhanVien.SelectedValue), 
                    TongTien = _tongTienPhieu, 
                    GhiChu = ghiChuThucTe 
                };
                
                int maPnVuaTao = _pnRepo.ThemPhieuNhap(pn);

                foreach (DataRow r in _dtChiTietLocal.Rows)
                {
                    _pnRepo.ThemChiTiet(new ChiTietPhieuNhap {
                        MaPhieuNhap = maPnVuaTao,
                        MaHangHoa = Convert.ToInt32(r["MaHang"]),
                        SoLuong = Convert.ToInt32(r["Số Lượng"]),
                        DonGiaNhap = Convert.ToDecimal(r["Đơn Giá"]),
                        ThanhTien = Convert.ToDecimal(r["Thành Tiền"])
                    });
                }

                MessageBox.Show("Lưu phiếu nhập kho và cập nhật số lượng tồn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Giải phóng bảng tạm nhập hàng nửa trên
                _dtChiTietLocal.Rows.Clear(); 
                TinhTongTien();

                // ĐÃ THÊM: Làm sạch ô TextBox ghi chú bằng tay sau khi lưu thành công dữ liệu
                txtGhiChuPhieu.Clear();

                // Làm mới bảng lịch sử
                HienThiLichSuPhieu();
                _maPhieuDuocChon = 0; 
            }
            catch (Exception ex) { MessageBox.Show("Lỗi lưu trữ phiếu nhập: " + ex.Message); }
        }
    }
}