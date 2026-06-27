using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.Api
{
    /// <summary>
    /// Lớp nghiệp vụ trung gian cho API.
    /// API không gọi trực tiếp SQL ở ApiHost mà đi qua lớp này để gom logic đọc/ghi dữ liệu.
    /// </summary>
    public class InventoryApiService
    {
        // DatabaseHelper dùng cho các truy vấn API cần tên cột JSON riêng.
        private readonly DatabaseHelper _db = new DatabaseHelper();

        // Các repository hiện có được tái sử dụng để API và WinForms không bị lệch logic CRUD.
        private readonly HangHoaRepository _hangHoaRepository = new HangHoaRepository();
        private readonly LoaiHangRepository _loaiHangRepository = new LoaiHangRepository();
        private readonly NhaCungCapRepository _nhaCungCapRepository = new NhaCungCapRepository();
        private readonly KhachHangRepository _khachHangRepository = new KhachHangRepository();
        private readonly NhanVienRepository _nhanVienRepository = new NhanVienRepository();
        private readonly PhieuNhapRepository _phieuNhapRepository = new PhieuNhapRepository();
        private readonly PhieuXuatRepository _phieuXuatRepository = new PhieuXuatRepository();

        /// <summary>
        /// Lấy danh sách hàng hóa với tên cột camelCase phù hợp JSON.
        /// </summary>
        public DataTable GetHangHoa()
        {
            const string sql = @"SELECT ma_hanghoa AS ""maHangHoa"", ten_hanghoa AS ""tenHangHoa"",
                                        ma_loaihang AS ""maLoaiHang"", ma_nhacungcap AS ""maNhaCungCap"",
                                        gia_nhap AS ""giaNhap"", gia_ban AS ""giaBan"",
                                        so_luong_ton AS ""soLuongTon"", don_vi_tinh AS ""donViTinh"",
                                        ghi_chu AS ""ghiChu""
                                 FROM hanghoa
                                 ORDER BY ma_hanghoa DESC";
            return _db.ExecuteQuery(sql);
        }

        /// <summary>
        /// Thêm hàng hóa mới từ body JSON gửi lên API.
        /// </summary>
        public int ThemHangHoa(HangHoa hangHoa) => _hangHoaRepository.Them(hangHoa);

        /// <summary>
        /// Cập nhật hàng hóa theo id trên URL.
        /// </summary>
        public int SuaHangHoa(int id, HangHoa hangHoa)
        {
            hangHoa.MaHangHoa = id;
            return _hangHoaRepository.Sua(hangHoa);
        }

        /// <summary>
        /// Xóa hàng hóa theo mã hàng.
        /// </summary>
        public int XoaHangHoa(int id) => _hangHoaRepository.Xoa(id);

        /// <summary>
        /// Lấy danh sách loại hàng.
        /// </summary>
        public DataTable GetLoaiHang()
        {
            const string sql = @"SELECT ma_loaihang AS ""maLoaiHang"", ten_loaihang AS ""tenLoaiHang"", ghi_chu AS ""ghiChu""
                                 FROM loaihang
                                 ORDER BY ma_loaihang DESC";
            return _db.ExecuteQuery(sql);
        }

        /// <summary>
        /// Thêm loại hàng mới.
        /// </summary>
        public int ThemLoaiHang(LoaiHang loaiHang) => _loaiHangRepository.Them(loaiHang);

        /// <summary>
        /// Cập nhật loại hàng theo id.
        /// </summary>
        public int SuaLoaiHang(int id, LoaiHang loaiHang)
        {
            loaiHang.MaLoaiHang = id;
            return _loaiHangRepository.Sua(loaiHang);
        }

        /// <summary>
        /// Xóa loại hàng theo id.
        /// </summary>
        public int XoaLoaiHang(int id) => _loaiHangRepository.Xoa(id);

        /// <summary>
        /// Lấy danh sách nhà cung cấp.
        /// </summary>
        public DataTable GetNhaCungCap()
        {
            const string sql = @"SELECT ma_nhacungcap AS ""maNhaCungCap"", ten_nhacungcap AS ""tenNhaCungCap"",
                                        dia_chi_ncc AS ""diaChiNCC"", so_dien_thoai AS ""soDienThoai"",
                                        email AS ""email"", ghi_chu AS ""ghiChu""
                                 FROM nhacungcap
                                 ORDER BY ma_nhacungcap DESC";
            return _db.ExecuteQuery(sql);
        }

        /// <summary>
        /// Thêm nhà cung cấp.
        /// </summary>
        public int ThemNhaCungCap(NhaCungCap nhaCungCap) => _nhaCungCapRepository.Them(nhaCungCap);

        /// <summary>
        /// Cập nhật nhà cung cấp.
        /// </summary>
        public int SuaNhaCungCap(int id, NhaCungCap nhaCungCap)
        {
            nhaCungCap.MaNhaCungCap = id;
            return _nhaCungCapRepository.Sua(nhaCungCap);
        }

        /// <summary>
        /// Xóa nhà cung cấp.
        /// </summary>
        public int XoaNhaCungCap(int id) => _nhaCungCapRepository.Xoa(id);

        /// <summary>
        /// Lấy danh sách khách hàng.
        /// </summary>
        public DataTable GetKhachHang()
        {
            const string sql = @"SELECT ma_khachhang AS ""maKhachHang"", ten_khachhang AS ""tenKhachHang"",
                                        dia_chi_kh AS ""diaChiKH"", so_dien_thoai AS ""soDienThoai"",
                                        email AS ""email"", ghi_chu AS ""ghiChu""
                                 FROM khachhang
                                 ORDER BY ma_khachhang DESC";
            return _db.ExecuteQuery(sql);
        }

        /// <summary>
        /// Thêm khách hàng.
        /// </summary>
        public int ThemKhachHang(KhachHang khachHang) => _khachHangRepository.Them(khachHang);

        /// <summary>
        /// Cập nhật khách hàng.
        /// </summary>
        public int SuaKhachHang(int id, KhachHang khachHang)
        {
            khachHang.MaKhachHang = id;
            return _khachHangRepository.Sua(khachHang);
        }

        /// <summary>
        /// Xóa khách hàng.
        /// </summary>
        public int XoaKhachHang(int id) => _khachHangRepository.Xoa(id);

        /// <summary>
        /// Lấy danh sách nhân viên.
        /// </summary>
        public DataTable GetNhanVien()
        {
            const string sql = @"SELECT ma_nhanvien AS ""maNhanVien"", ten_nhanvien AS ""tenNhanVien"",
                                        dia_chi_nv AS ""diaChiNV"", so_dien_thoai AS ""soDienThoai"",
                                        email AS ""email"", chuc_vu AS ""chucVu"", ghi_chu AS ""ghiChu""
                                 FROM nhanvien
                                 ORDER BY ma_nhanvien DESC";
            return _db.ExecuteQuery(sql);
        }

        /// <summary>
        /// Thêm nhân viên.
        /// </summary>
        public int ThemNhanVien(NhanVien nhanVien) => _nhanVienRepository.Them(nhanVien);

        /// <summary>
        /// Cập nhật nhân viên.
        /// </summary>
        public int SuaNhanVien(int id, NhanVien nhanVien)
        {
            nhanVien.MaNhanVien = id;
            return _nhanVienRepository.Sua(nhanVien);
        }

        /// <summary>
        /// Xóa nhân viên.
        /// </summary>
        public int XoaNhanVien(int id) => _nhanVienRepository.Xoa(id);

        /// <summary>
        /// Lấy lịch sử phiếu nhập.
        /// </summary>
        public DataTable GetPhieuNhap() => _phieuNhapRepository.GetAllPhieuNhap();

        /// <summary>
        /// Lấy chi tiết hàng hóa trong một phiếu nhập.
        /// </summary>
        public DataTable GetChiTietPhieuNhap(int id) => _phieuNhapRepository.GetChiTietTheoMaPhieu(id);

        /// <summary>
        /// Lấy lịch sử phiếu xuất.
        /// </summary>
        public DataTable GetPhieuXuat() => _phieuXuatRepository.GetAllPhieuXuat();

        /// <summary>
        /// Lấy chi tiết hàng hóa trong một phiếu xuất.
        /// </summary>
        public DataTable GetChiTietPhieuXuat(int id) => _phieuXuatRepository.GetChiTietTheoMaPhieu(id);

        /// <summary>
        /// Kiểm tra database có kết nối được hay không.
        /// </summary>
        public bool TestDatabase() => DbConnection.TestConnection();

        /// <summary>
        /// Lấy các mặt hàng có số lượng tồn nhỏ hơn hoặc bằng ngưỡng cảnh báo.
        /// </summary>
        public DataTable GetTonKhoThap(int soLuongToiDa)
        {
            const string sql = @"SELECT ma_hanghoa AS ""maHangHoa"", ten_hanghoa AS ""tenHangHoa"",
                                        so_luong_ton AS ""soLuongTon"", don_vi_tinh AS ""donViTinh""
                                 FROM hanghoa
                                 WHERE so_luong_ton <= @soLuongToiDa
                                 ORDER BY so_luong_ton ASC, ten_hanghoa ASC";
            return _db.ExecuteQuery(sql, new[] { new NpgsqlParameter("@soLuongToiDa", soLuongToiDa) });
        }
    }
}
