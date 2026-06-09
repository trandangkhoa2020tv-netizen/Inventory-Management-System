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

        public FrmNhapKho() { InitializeComponent(); }

        private void FrmNhapKho_Load(object sender, EventArgs e)
        {
            // Load ComboBoxes
            cbNCC.DataSource = _nccRepo.GetAll(); cbNCC.DisplayMember = "Tên Nhà Cung Cấp"; cbNCC.ValueMember = "Mã NCC";
            cbNhanVien.DataSource = _nvRepo.GetAll(); cbNhanVien.DisplayMember = "Tên Nhân Viên"; cbNhanVien.ValueMember = "Mã NV";
            cbHangHoa.DataSource = _hhRepo.GetAll(); cbHangHoa.DisplayMember = "Tên Hàng Hóa"; cbHangHoa.ValueMember = "Mã Hàng";

            // Khởi tạo bảng chi tiết tạm thời
            _dtChiTietLocal = new DataTable();
            _dtChiTietLocal.Columns.Add("MaHang", typeof(int));
            _dtChiTietLocal.Columns.Add("Tên Mặt Hàng", typeof(string));
            _dtChiTietLocal.Columns.Add("Số Lượng", typeof(int));
            _dtChiTietLocal.Columns.Add("Đơn Giá", typeof(decimal));
            _dtChiTietLocal.Columns.Add("Thành Tiền", typeof(decimal));
            dgvChiTiet.DataSource = _dtChiTietLocal;
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
            lblTongTien.Text = $"TỔNG TIỀN: {_tongTienPhieu:N0} VNĐ";
        }

        // HÀM XUẤT EXCEL
        private void btnExcel_Click(object sender, EventArgs e)
        {
            QuanLyKhoHang.Reports.ExportExcel.ToExcel(_dtChiTietLocal, "Bao_Cao_Phieu_Nhap_Kho");
        }

        // HÀM XUẤT PDF
        private void btnPdf_Click(object sender, EventArgs e)
        {
            QuanLyKhoHang.Reports.ExportPdf.ToPdf(_dtChiTietLocal, "Hóa Đơn Phiếu Nhập Kho Hàng");
        }

        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (_dtChiTietLocal.Rows.Count == 0) { MessageBox.Show("Chưa có mặt hàng nào!"); return; }

            try
            {
                PhieuNhap pn = new PhieuNhap { MaNhaCungCap = Convert.ToInt32(cbNCC.SelectedValue), MaNhanVien = Convert.ToInt32(cbNhanVien.SelectedValue), TongTien = _tongTienPhieu, GhiChu = "Nhập kho phần mềm" };
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

                MessageBox.Show("Lưu phiếu nhập kho và cập nhật số lượng tồn thành công!");
                _dtChiTietLocal.Rows.Clear(); TinhTongTien();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
    }
}