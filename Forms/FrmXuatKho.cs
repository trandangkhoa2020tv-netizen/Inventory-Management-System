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

        public FrmXuatKho() { InitializeComponent(); }

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

        // KÍCH HOẠT XUẤT FILE EXCEL CHO PHIẾU XUẤT
        private void btnExcel_Click(object sender, EventArgs e)
        {
            QuanLyKhoHang.Reports.ExportExcel.ToExcel(_dtChiTietLocal, "Bao_Cao_Phieu_Xuat_Kho");
        }

        // KÍCH HOẠT XUẤT FILE PDF CHO PHIẾU XUẤT
        private void btnPdf_Click(object sender, EventArgs e)
        {
            QuanLyKhoHang.Reports.ExportPdf.ToPdf(_dtChiTietLocal, "Hóa Đơn Phiếu Xuất Hàng Mới");
        }

        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (_dtChiTietLocal.Rows.Count == 0) { MessageBox.Show("Chưa chọn mặt hàng xuất!"); return; }

            try
            {
                PhieuXuat px = new PhieuXuat { MaKhachHang = Convert.ToInt32(cbKhachHang.SelectedValue), MaNhanVien = Convert.ToInt32(cbNhanVien.SelectedValue), TongTien = _tongTienPhieu, GhiChu = "Xuất kho bán sỉ lẻ" };
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

                MessageBox.Show("Lưu phiếu xuất kho và tự động khấu trừ hàng tồn thành công!");
                _dtChiTietLocal.Rows.Clear(); TinhTongTien();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
    }
}