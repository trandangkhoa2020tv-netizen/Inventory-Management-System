USE QuanLyKhoHang;
GO

INSERT INTO LoaiHang (TenLoaiHang, GhiChu) VALUES (N'Điện Tử', N'Các sản phẩm điện tử');
INSERT INTO LoaiHang (TenLoaiHang, GhiChu) VALUES (N'Quần Áo', N'Các loại quần áo');
INSERT INTO LoaiHang (TenLoaiHang, GhiChu) VALUES (N'Thực Phẩm', N'Các loại thực phẩm');
GO

INSERT INTO NhaCungCap (TenNhaCungCap, DiaChiNCC, SoDienThoai, Email, GhiChu)
VALUES (N'Công ty A', N'123 Đường ABC', N'0123456789', N'a@company.com', N'Nhà cung cấp 1');
INSERT INTO NhaCungCap (TenNhaCungCap, DiaChiNCC, SoDienThoai, Email, GhiChu)
VALUES (N'Công ty B', N'456 Đường DEF', N'0987654321', N'b@company.com', N'Nhà cung cấp 2');
GO

INSERT INTO HangHoa (TenHangHoa, MaLoaiHang, MaNhaCungCap, GiaNhap, GiaBan, SoLuongTon, DonViTinh, GhiChu)
VALUES (N'Laptop Dell', 1, 1, 5000000, 6000000, 10, N'Cái', N'Laptop hiệu Dell');
INSERT INTO HangHoa (TenHangHoa, MaLoaiHang, MaNhaCungCap, GiaNhap, GiaBan, SoLuongTon, DonViTinh, GhiChu)
VALUES (N'Áo Thun Nam', 2, 2, 50000, 80000, 50, N'Cái', N'Áo thun');
GO

INSERT INTO KhachHang (TenKhachHang, DiaChiKH, SoDienThoai, Email, GhiChu)
VALUES (N'Nguyễn Văn A', N'789 Đường GHI', N'0111111111', N'nguyenvana@gmail.com', N'Khách hàng 1');
INSERT INTO KhachHang (TenKhachHang, DiaChiKH, SoDienThoai, Email, GhiChu)
VALUES (N'Trần Thị B', N'101 Đường JKL', N'0222222222', N'tranthib@gmail.com', N'Khách hàng 2');
GO

INSERT INTO NhanVien (TenNhanVien, DiaChiNV, SoDienThoai, Email, NgaySinh, ChucVu, GhiChu)
VALUES (N'Lê Văn C', N'202 Đường MNO', N'0333333333', N'levanc@gmail.com', '1990-01-15', N'Nhân Viên Bán Hàng', N'NV 1');
INSERT INTO NhanVien (TenNhanVien, DiaChiNV, SoDienThoai, Email, NgaySinh, ChucVu, GhiChu)
VALUES (N'Phạm Thị D', N'303 Đường PQR', N'0444444444', N'phamthid@gmail.com', '1995-05-20', N'Kho Thủ Kho', N'NV 2');
GO

INSERT INTO TaiKhoan (MaNhanVien, TenTaiKhoan, MatKhau, VaiTro, TrangThai)
VALUES (1, 'levanc', '123456', N'Nhân Viên', 1);
INSERT INTO TaiKhoan (MaNhanVien, TenTaiKhoan, MatKhau, VaiTro, TrangThai)
VALUES (2, 'phamthid', '123456', N'Kho Thủ', 1);
GO
