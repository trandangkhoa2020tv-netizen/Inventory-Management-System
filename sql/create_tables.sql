CREATE DATABASE QuanLyKhoHang;
GO

USE QuanLyKhoHang;
GO

CREATE TABLE LoaiHang
(
    MaLoaiHang INT PRIMARY KEY IDENTITY(1,1),
    TenLoaiHang NVARCHAR(100) NOT NULL,
    GhiChu NVARCHAR(MAX)
);

CREATE TABLE NhaCungCap
(
    MaNhaCungCap INT PRIMARY KEY IDENTITY(1,1),
    TenNhaCungCap NVARCHAR(100) NOT NULL,
    DiaChiNCC NVARCHAR(200),
    SoDienThoai NVARCHAR(20),
    Email NVARCHAR(100),
    GhiChu NVARCHAR(MAX)
);

CREATE TABLE HangHoa
(
    MaHangHoa INT PRIMARY KEY IDENTITY(1,1),
    TenHangHoa NVARCHAR(100) NOT NULL,
    MaLoaiHang INT NOT NULL,
    MaNhaCungCap INT NOT NULL,
    GiaNhap DECIMAL(10, 2),
    GiaBan DECIMAL(10, 2),
    SoLuongTon INT DEFAULT 0,
    DonViTinh NVARCHAR(50),
    GhiChu NVARCHAR(MAX),
    FOREIGN KEY (MaLoaiHang) REFERENCES LoaiHang(MaLoaiHang),
    FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap)
);

CREATE TABLE KhachHang
(
    MaKhachHang INT PRIMARY KEY IDENTITY(1,1),
    TenKhachHang NVARCHAR(100) NOT NULL,
    DiaChiKH NVARCHAR(200),
    SoDienThoai NVARCHAR(20),
    Email NVARCHAR(100),
    GhiChu NVARCHAR(MAX)
);

CREATE TABLE NhanVien
(
    MaNhanVien INT PRIMARY KEY IDENTITY(1,1),
    TenNhanVien NVARCHAR(100) NOT NULL,
    DiaChiNV NVARCHAR(200),
    SoDienThoai NVARCHAR(20),
    Email NVARCHAR(100),
    NgaySinh DATE,
    ChucVu NVARCHAR(50),
    GhiChu NVARCHAR(MAX)
);

CREATE TABLE TaiKhoan
(
    MaTaiKhoan INT PRIMARY KEY IDENTITY(1,1),
    MaNhanVien INT NOT NULL,
    TenTaiKhoan NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(100) NOT NULL,
    VaiTro NVARCHAR(50),
    TrangThai BIT DEFAULT 1,
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

CREATE TABLE PhieuNhap
(
    MaPhieuNhap INT PRIMARY KEY IDENTITY(1,1),
    MaNhaCungCap INT NOT NULL,
    MaNhanVien INT NOT NULL,
    NgayNhap DATE NOT NULL,
    TongTien DECIMAL(12, 2),
    GhiChu NVARCHAR(MAX),
    FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

CREATE TABLE ChiTietPhieuNhap
(
    MaChiTiet INT PRIMARY KEY IDENTITY(1,1),
    MaPhieuNhap INT NOT NULL,
    MaHangHoa INT NOT NULL,
    SoLuongNhap INT NOT NULL,
    GiaNhap DECIMAL(10, 2),
    ThanhTien DECIMAL(12, 2),
    FOREIGN KEY (MaPhieuNhap) REFERENCES PhieuNhap(MaPhieuNhap),
    FOREIGN KEY (MaHangHoa) REFERENCES HangHoa(MaHangHoa)
);

CREATE TABLE PhieuXuat
(
    MaPhieuXuat INT PRIMARY KEY IDENTITY(1,1),
    MaKhachHang INT NOT NULL,
    MaNhanVien INT NOT NULL,
    NgayXuat DATE NOT NULL,
    TongTien DECIMAL(12, 2),
    GhiChu NVARCHAR(MAX),
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

CREATE TABLE ChiTietPhieuXuat
(
    MaChiTiet INT PRIMARY KEY IDENTITY(1,1),
    MaPhieuXuat INT NOT NULL,
    MaHangHoa INT NOT NULL,
    SoLuongXuat INT NOT NULL,
    GiaBan DECIMAL(10, 2),
    ThanhTien DECIMAL(12, 2),
    FOREIGN KEY (MaPhieuXuat) REFERENCES PhieuXuat(MaPhieuXuat),
    FOREIGN KEY (MaHangHoa) REFERENCES HangHoa(MaHangHoa)
);
