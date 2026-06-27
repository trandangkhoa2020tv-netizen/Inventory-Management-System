-- ============================================================
-- Dong bo database PostgreSQL da ton tai voi code hien tai.
-- Dung file nay khi database da co bang roi, khong muon tao lai tu dau.
-- Ket noi vao database "quanlyhanghoa" roi chay toan bo file.
-- ============================================================

-- Bang taikhoan: code dang nhap can ten_taikhoan, mat_khau, vai_tro, trang_thai.
ALTER TABLE taikhoan ADD COLUMN IF NOT EXISTS trang_thai boolean NOT NULL DEFAULT true;
ALTER TABLE taikhoan ADD COLUMN IF NOT EXISTS vai_tro varchar(50) NOT NULL DEFAULT 'NhanVien';

-- Bang hanghoa: code quan ly hang hoa va nhap/xuat kho can cac cot nay.
ALTER TABLE hanghoa ADD COLUMN IF NOT EXISTS gia_nhap numeric(18, 2) NOT NULL DEFAULT 0;
ALTER TABLE hanghoa ADD COLUMN IF NOT EXISTS gia_ban numeric(18, 2) NOT NULL DEFAULT 0;
ALTER TABLE hanghoa ADD COLUMN IF NOT EXISTS so_luong_ton integer NOT NULL DEFAULT 0;
ALTER TABLE hanghoa ADD COLUMN IF NOT EXISTS don_vi_tinh varchar(50);
ALTER TABLE hanghoa ADD COLUMN IF NOT EXISTS ghi_chu text;

-- Bang loaihang.
ALTER TABLE loaihang ADD COLUMN IF NOT EXISTS ghi_chu text;

-- Bang nhacungcap.
ALTER TABLE nhacungcap ADD COLUMN IF NOT EXISTS dia_chi_ncc varchar(500);
ALTER TABLE nhacungcap ADD COLUMN IF NOT EXISTS so_dien_thoai varchar(20);
ALTER TABLE nhacungcap ADD COLUMN IF NOT EXISTS email varchar(100);
ALTER TABLE nhacungcap ADD COLUMN IF NOT EXISTS ghi_chu text;

-- Bang khachhang.
ALTER TABLE khachhang ADD COLUMN IF NOT EXISTS dia_chi_kh varchar(500);
ALTER TABLE khachhang ADD COLUMN IF NOT EXISTS so_dien_thoai varchar(20);
ALTER TABLE khachhang ADD COLUMN IF NOT EXISTS email varchar(100);
ALTER TABLE khachhang ADD COLUMN IF NOT EXISTS ghi_chu text;

-- Bang nhanvien.
ALTER TABLE nhanvien ADD COLUMN IF NOT EXISTS dia_chi_nv varchar(500);
ALTER TABLE nhanvien ADD COLUMN IF NOT EXISTS so_dien_thoai varchar(20);
ALTER TABLE nhanvien ADD COLUMN IF NOT EXISTS email varchar(100);
ALTER TABLE nhanvien ADD COLUMN IF NOT EXISTS ngay_sinh date;
ALTER TABLE nhanvien ADD COLUMN IF NOT EXISTS chuc_vu varchar(100);
ALTER TABLE nhanvien ADD COLUMN IF NOT EXISTS ghi_chu text;

-- Bang phieunhap.
ALTER TABLE phieunhap ADD COLUMN IF NOT EXISTS ngay_nhap date NOT NULL DEFAULT CURRENT_DATE;
ALTER TABLE phieunhap ADD COLUMN IF NOT EXISTS tong_tien numeric(18, 2) NOT NULL DEFAULT 0;
ALTER TABLE phieunhap ADD COLUMN IF NOT EXISTS ghi_chu text;

-- Bang phieuxuat.
ALTER TABLE phieuxuat ADD COLUMN IF NOT EXISTS ngay_xuat date NOT NULL DEFAULT CURRENT_DATE;
ALTER TABLE phieuxuat ADD COLUMN IF NOT EXISTS tong_tien numeric(18, 2) NOT NULL DEFAULT 0;
ALTER TABLE phieuxuat ADD COLUMN IF NOT EXISTS ghi_chu text;

-- Bang chitietphieunhap: code hien tai dung so_luong, don_gia_nhap, thanh_tien.
ALTER TABLE chitietphieunhap ADD COLUMN IF NOT EXISTS so_luong integer NOT NULL DEFAULT 1;
ALTER TABLE chitietphieunhap ADD COLUMN IF NOT EXISTS don_gia_nhap numeric(18, 2) NOT NULL DEFAULT 0;
ALTER TABLE chitietphieunhap ADD COLUMN IF NOT EXISTS thanh_tien numeric(18, 2) NOT NULL DEFAULT 0;

-- Bang chitietphieuxuat: code hien tai dung so_luong, don_gia_xuat, thanh_tien.
ALTER TABLE chitietphieuxuat ADD COLUMN IF NOT EXISTS so_luong integer NOT NULL DEFAULT 1;
ALTER TABLE chitietphieuxuat ADD COLUMN IF NOT EXISTS don_gia_xuat numeric(18, 2) NOT NULL DEFAULT 0;
ALTER TABLE chitietphieuxuat ADD COLUMN IF NOT EXISTS thanh_tien numeric(18, 2) NOT NULL DEFAULT 0;

-- Tao index neu chua co.
CREATE INDEX IF NOT EXISTS idx_hanghoa_ten ON hanghoa(ten_hanghoa);
CREATE INDEX IF NOT EXISTS idx_phieunhap_ngay ON phieunhap(ngay_nhap);
CREATE INDEX IF NOT EXISTS idx_phieuxuat_ngay ON phieuxuat(ngay_xuat);
