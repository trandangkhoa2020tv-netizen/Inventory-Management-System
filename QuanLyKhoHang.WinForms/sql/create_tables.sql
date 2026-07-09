-- ============================================================
-- Tao cau truc database PostgreSQL cho phan mem QuanLyKhoHang.
-- Ket noi vao database "quanlyhanghoa" truoc khi chay file nay.
-- File nay an toan khi chay lai nhieu lan vi co IF NOT EXISTS.
-- ============================================================

CREATE TABLE IF NOT EXISTS loaihang
(
    ma_loaihang serial PRIMARY KEY,
    ten_loaihang varchar(100) NOT NULL,
    ghi_chu text
);

CREATE TABLE IF NOT EXISTS nhacungcap
(
    ma_nhacungcap serial PRIMARY KEY,
    ten_nhacungcap varchar(255) NOT NULL,
    dia_chi_ncc varchar(500),
    so_dien_thoai varchar(20),
    email varchar(100),
    ghi_chu text
);

CREATE TABLE IF NOT EXISTS khachhang
(
    ma_khachhang serial PRIMARY KEY,
    ten_khachhang varchar(255) NOT NULL,
    dia_chi_kh varchar(500),
    so_dien_thoai varchar(20),
    email varchar(100),
    ghi_chu text
);

CREATE TABLE IF NOT EXISTS nhanvien
(
    ma_nhanvien serial PRIMARY KEY,
    ten_nhanvien varchar(255) NOT NULL,
    dia_chi_nv varchar(500),
    so_dien_thoai varchar(20),
    email varchar(100),
    ngay_sinh date,
    chuc_vu varchar(100),
    ghi_chu text
);

CREATE TABLE IF NOT EXISTS hanghoa
(
    ma_hanghoa serial PRIMARY KEY,
    ten_hanghoa varchar(255) NOT NULL,
    ma_loaihang integer NOT NULL,
    ma_nhacungcap integer NOT NULL,
    gia_nhap numeric(18, 2) NOT NULL DEFAULT 0,
    gia_ban numeric(18, 2) NOT NULL DEFAULT 0,
    so_luong_ton integer NOT NULL DEFAULT 0,
    don_vi_tinh varchar(50),
    ghi_chu text,
    CONSTRAINT fk_hanghoa_loaihang FOREIGN KEY (ma_loaihang) REFERENCES loaihang(ma_loaihang),
    CONSTRAINT fk_hanghoa_nhacungcap FOREIGN KEY (ma_nhacungcap) REFERENCES nhacungcap(ma_nhacungcap),
    CONSTRAINT ck_hanghoa_ton_khong_am CHECK (so_luong_ton >= 0)
);

CREATE TABLE IF NOT EXISTS taikhoan
(
    ma_taikhoan serial PRIMARY KEY,
    ma_nhanvien integer NOT NULL,
    ten_taikhoan varchar(50) NOT NULL UNIQUE,
    mat_khau varchar(100) NOT NULL,
    vai_tro varchar(50) NOT NULL DEFAULT 'NhanVien',
    trang_thai boolean NOT NULL DEFAULT true,
    CONSTRAINT fk_taikhoan_nhanvien FOREIGN KEY (ma_nhanvien) REFERENCES nhanvien(ma_nhanvien)
);

CREATE TABLE IF NOT EXISTS phieunhap
(
    ma_phieunhap serial PRIMARY KEY,
    ma_nhacungcap integer NOT NULL,
    ma_nhanvien integer NOT NULL,
    ngay_nhap date NOT NULL DEFAULT CURRENT_DATE,
    tong_tien numeric(18, 2) NOT NULL DEFAULT 0,
    ghi_chu text,
    CONSTRAINT fk_phieunhap_nhacungcap FOREIGN KEY (ma_nhacungcap) REFERENCES nhacungcap(ma_nhacungcap),
    CONSTRAINT fk_phieunhap_nhanvien FOREIGN KEY (ma_nhanvien) REFERENCES nhanvien(ma_nhanvien)
);

CREATE TABLE IF NOT EXISTS chitietphieunhap
(
    ma_chitiet serial PRIMARY KEY,
    ma_phieunhap integer NOT NULL,
    ma_hanghoa integer NOT NULL,
    so_luong integer NOT NULL,
    don_gia_nhap numeric(18, 2) NOT NULL DEFAULT 0,
    thanh_tien numeric(18, 2) NOT NULL DEFAULT 0,
    CONSTRAINT fk_ctpn_phieunhap FOREIGN KEY (ma_phieunhap) REFERENCES phieunhap(ma_phieunhap) ON DELETE CASCADE,
    CONSTRAINT fk_ctpn_hanghoa FOREIGN KEY (ma_hanghoa) REFERENCES hanghoa(ma_hanghoa),
    CONSTRAINT ck_ctpn_so_luong_duong CHECK (so_luong > 0)
);

CREATE TABLE IF NOT EXISTS phieuxuat
(
    ma_phieuxuat serial PRIMARY KEY,
    ma_khachhang integer NOT NULL,
    ma_nhanvien integer NOT NULL,
    ngay_xuat date NOT NULL DEFAULT CURRENT_DATE,
    tong_tien numeric(18, 2) NOT NULL DEFAULT 0,
    ghi_chu text,
    CONSTRAINT fk_phieuxuat_khachhang FOREIGN KEY (ma_khachhang) REFERENCES khachhang(ma_khachhang),
    CONSTRAINT fk_phieuxuat_nhanvien FOREIGN KEY (ma_nhanvien) REFERENCES nhanvien(ma_nhanvien)
);

CREATE TABLE IF NOT EXISTS chitietphieuxuat
(
    ma_chitiet serial PRIMARY KEY,
    ma_phieuxuat integer NOT NULL,
    ma_hanghoa integer NOT NULL,
    so_luong integer NOT NULL,
    don_gia_xuat numeric(18, 2) NOT NULL DEFAULT 0,
    thanh_tien numeric(18, 2) NOT NULL DEFAULT 0,
    CONSTRAINT fk_ctpx_phieuxuat FOREIGN KEY (ma_phieuxuat) REFERENCES phieuxuat(ma_phieuxuat) ON DELETE CASCADE,
    CONSTRAINT fk_ctpx_hanghoa FOREIGN KEY (ma_hanghoa) REFERENCES hanghoa(ma_hanghoa),
    CONSTRAINT ck_ctpx_so_luong_duong CHECK (so_luong > 0)
);

-- Bo sung cot cho database cu neu thieu.
ALTER TABLE taikhoan ADD COLUMN IF NOT EXISTS trang_thai boolean NOT NULL DEFAULT true;

-- Index phuc vu tim kiem/tra cuu nhanh.
CREATE INDEX IF NOT EXISTS idx_hanghoa_ten ON hanghoa(ten_hanghoa);
CREATE INDEX IF NOT EXISTS idx_phieunhap_ngay ON phieunhap(ngay_nhap);
CREATE INDEX IF NOT EXISTS idx_phieuxuat_ngay ON phieuxuat(ngay_xuat);
