using System;
using System.Data;
using System.Windows.Forms;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.Forms
{
    public partial class FrmXuatKho : Form
    {
        private readonly PhieuXuatRepository _pxRepo = new PhieuXuatRepository();
        private readonly KhachHangRepository _khRepo = new KhachHangRepository();
        private readonly NhanVienRepository _nvRepo = new NhanVienRepository();
        private readonly HangHoaRepository _hhRepo = new HangHoaRepository();
        
        private DataTable _dtChiTietLocal; 
        private decimal _tongTienPhieu = 0;
        private string _vaiTro = "NhanVien";
        private int _maPhieuDuocChon = 0; 

        public FrmXuatKho(string vaiTro) 
        { 
            InitializeComponent(); 
            this._vaiTro = vaiTro;
        }

        private void FrmXuatKho_Load(object sender, EventArgs e)
        {
            cbKhachHang.DataSource = _khRepo.GetAll(); cbKhachHang.DisplayMember = "Tên Khách Hàng"; cbKhachHang.ValueMember = "Mã KH";
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
                dgvLichSuPhieu.DataSource = _pxRepo.GetAllPhieuXuat();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể nạp danh sách lịch sử phiếu xuất: " + ex.Message, "Lỗi");
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
                MessageBox.Show("Vui lòng click chọn một phiếu xuất từ bảng lịch sử bên dưới trước khi bấm xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable dtChiTietPhieu = _pxRepo.GetChiTietTheoMaPhieu(_maPhieuDuocChon);
                QuanLyKhoHang.Reports.ExportExcel.ToExcel(dtChiTietPhieu, $"Bao_Cao_Phieu_Xuat_So_{_maPhieuDuocChon}");
                MessageBox.Show($"Đã xuất file báo cáo Excel cho phiếu xuất số {_maPhieuDuocChon} thành công!", "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi bóc tách xuất dữ liệu Excel: " + ex.Message, "Lỗi");
            }
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (_maPhieuDuocChon == 0)
            {
                MessageBox.Show("Vui lòng click chọn một phiếu xuất từ bảng lịch sử bên dưới trước khi bấm in hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable dtChiTietPhieu = _pxRepo.GetChiTietTheoMaPhieu(_maPhieuDuocChon);
                QuanLyKhoHang.Reports.ExportPdf.ToPdf(dtChiTietPhieu, $"Hoa_Don_Phieu_Xuat_So_{_maPhieuDuocChon}");
                MessageBox.Show($"Đã xuất file hóa đơn PDF cho phiếu xuất số {_maPhieuDuocChon} thành công!", "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi bóc tách xuất hóa đơn PDF: " + ex.Message, "Lỗi");
            }
        }

        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (_dtChiTietLocal.Rows.Count == 0) { MessageBox.Show("Chưa chọn mặt hàng xuất!"); return; }

            try
            {
                // ĐÃ SỬA: Lấy chính xác chuỗi ghi chú động từ TextBox do người dùng tự gõ
                string ghiChuThucTe = string.IsNullOrWhiteSpace(txtGhiChuPhieu.Text) ? "" : txtGhiChuPhieu.Text;

                // ĐÃ SỬA: Truyền trực tiếp chuỗi biến ghiChuThucTe thay thế cho chữ gán cứng ban đầu
                PhieuXuat px = new PhieuXuat { 
                    MaKhachHang = Convert.ToInt32(cbKhachHang.SelectedValue), 
                    MaNhanVien = Convert.ToInt32(cbNhanVien.SelectedValue), 
                    NgayXuat = DateTime.Now,
                    TongTien = _tongTienPhieu, 
                    GhiChu = ghiChuThucTe 
                };
                
                int maPxVuaTao = _pxRepo.ThemPhieuXuat(px);

                foreach (DataRow r in _dtChiTietLocal.Rows)
                {
                    _pxRepo.ThemChiTiet(new ChiTietPhieuXuat {
                        MaPhieuXuat = maPxVuaTao,
                        MaHangHoa = Convert.ToInt32(r["MaHang"]),
                        SoLuong = Convert.ToInt32(r["Số Lượng"]),
                        DonGiaXuat = Convert.ToDecimal(r["Đơn Giá"]),
                        ThanhTien = Convert.ToDecimal(r["Thành Tiền"])
                    });
                }

                MessageBox.Show("Lưu phiếu xuất kho và tự động khấu trừ hàng tồn kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                _dtChiTietLocal.Rows.Clear(); 
                TinhTongTien();

                // ĐÃ THÊM: Làm sạch dữ liệu chữ cũ trên TextBox ghi chú sau khi ghi thành công phiếu
                txtGhiChuPhieu.Clear();

                HienThiLichSuPhieu(); 
                _maPhieuDuocChon = 0; 
            }
            catch (Exception ex) { MessageBox.Show("Lỗi lưu trữ: " + ex.Message); }
        }
    }
}