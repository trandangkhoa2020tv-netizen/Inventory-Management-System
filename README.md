# QUẢN LÝ KHO HÀNG

## 1. Giới thiệu

Phần mềm Quản Lý Kho Hàng được xây dựng bằng C# WinForms kết hợp PostgreSQL nhằm hỗ trợ quản lý hàng hóa, khách hàng, nhân viên, nhập kho, xuất kho và thống kê dữ liệu một cách hiệu quả.

Hệ thống cho phép lưu trữ dữ liệu tập trung trong PostgreSQL, giúp đảm bảo tính toàn vẹn dữ liệu thông qua Primary Key, Foreign Key và các mối quan hệ giữa các bảng.

---

## 2. Công nghệ sử dụng

### Ngôn ngữ lập trình

- C# (.NET WinForms)

### Hệ quản trị cơ sở dữ liệu

- PostgreSQL 17

### Thư viện

- Npgsql (Kết nối PostgreSQL)
- ClosedXML (Xuất Excel)
- iTextSharp (Xuất PDF - dự kiến)

### Công cụ phát triển

- Visual Studio Code
- PostgreSQL
- DBeaver
- Git/GitHub

---

## 3. Chức năng hệ thống

### Đăng nhập

- Xác thực tài khoản nhân viên
- Phân quyền người dùng

### Quản lý hàng hóa

- Thêm hàng hóa
- Sửa thông tin hàng hóa
- Xóa hàng hóa
- Tìm kiếm hàng hóa

### Quản lý khách hàng

- Thêm khách hàng
- Cập nhật thông tin khách hàng
- Xóa khách hàng
- Tìm kiếm khách hàng

### Quản lý nhân viên

- Thêm nhân viên
- Cập nhật thông tin nhân viên
- Xóa nhân viên

### Nhập kho

- Lập phiếu nhập
- Ghi nhận nhà cung cấp
- Cập nhật số lượng tồn kho

### Xuất kho

- Lập phiếu xuất
- Chọn khách hàng
- Trừ số lượng tồn kho

### Báo cáo

- Xuất dữ liệu ra Excel
- Thống kê nhập xuất
- Thống kê tồn kho

---

## 4. Cấu trúc cơ sở dữ liệu

Tên Database:

quanlyhanghoa

### Các bảng chính

- hanghoa
- loaihang
- nhacungcap
- khachhang
- nhanvien
- taikhoan
- phieunhap
- chitietphieunhap
- phieuxuat
- chitietphieuxuat

### Quan hệ dữ liệu

- Loại hàng → Hàng hóa
- Nhà cung cấp → Hàng hóa
- Nhà cung cấp → Phiếu nhập
- Nhân viên → Phiếu nhập
- Nhân viên → Phiếu xuất
- Khách hàng → Phiếu xuất
- Phiếu nhập → Chi tiết phiếu nhập
- Phiếu xuất → Chi tiết phiếu xuất
- Hàng hóa → Chi tiết phiếu nhập
- Hàng hóa → Chi tiết phiếu xuất
- Nhân viên → Tài khoản

---

## 5. Cấu trúc thư mục dự án

QuanLyKhoHang
│
├── Data
├── Forms
├── Models
├── Repositories
├── Reports
├── sql
├── Program.cs
└── README.md

---

## 6. Luồng hoạt động hệ thống

Đăng nhập
↓
Màn hình chính
↓
Quản lý dữ liệu
↓
PostgreSQL
↓
Lưu trữ dữ liệu
↓
Báo cáo Excel

---

## 7. Hướng phát triển

- Phân quyền Admin/Nhân viên
- Xuất PDF
- Dashboard thống kê
- Kết nối Excel tự động
- Sao lưu dữ liệu
- Nhật ký hoạt động người dùng

---

## 8. Thông tin sinh viên

Đề tài: Quản Lý Kho Hàng

Sinh viên thực hiện: Khoa

Năm thực hiện: 2026

## 9. Phân quyền người dùng

### Admin

- Quản lý tài khoản
- Quản lý nhân viên
- Quản lý hàng hóa
- Quản lý khách hàng
- Quản lý nhập kho
- Quản lý xuất kho
- Xuất báo cáo Excel
- Xem thống kê

### Nhân viên

- Quản lý hàng hóa
- Quản lý khách hàng
- Lập phiếu nhập
- Lập phiếu xuất
- Tìm kiếm dữ liệu
- Xuất Excel

---

## 10. Quy tắc cơ sở dữ liệu

### Primary Key

Mỗi bảng đều có khóa chính:

- ma_hang
- makh
- manv
- mapn
- mapx
- matk

### Foreign Key

Liên kết dữ liệu giữa các bảng:

- hanghoa → loaihang
- hanghoa → nhacungcap
- phieunhap → nhacungcap
- phieunhap → nhanvien
- phieuxuat → khachhang
- phieuxuat → nhanvien
- taikhoan → nhanvien

---

## 11. Yêu cầu hệ thống

### Phần mềm

- Windows 10 hoặc Windows 11
- PostgreSQL 17
- .NET 8 SDK trở lên
- Visual Studio Code

### Thư viện

- Npgsql
- ClosedXML

---

## 12. Cách chạy dự án

### Clone dự án

git clone <repository-url>

### Khôi phục thư viện

dotnet restore

### Chạy chương trình

dotnet run

### Database

1. Tạo database quanlyhanghoa
2. Chạy create_tables.sql
3. Chạy sample_data.sql
4. Kiểm tra kết nối PostgreSQL
5. Chạy ứng dụng

QuanLyKhoHang
│
├── Data
│ ├── DbConnection.cs
│ └── DatabaseHelper.cs
│
├── Forms
│ ├── FrmDangNhap.cs
│ ├── FrmDangNhap.Designer.cs
│ ├── FrmMain.cs
│ ├── FrmMain.Designer.cs
│ ├── FrmHangHoa.cs
│ ├── FrmHangHoa.Designer.cs
│ ├── FrmKhachHang.cs
│ ├── FrmKhachHang.Designer.cs
│ ├── FrmNhanVien.cs
│ ├── FrmNhanVien.Designer.cs
│ ├── FrmNhapKho.cs
│ ├── FrmNhapKho.Designer.cs
│ ├── FrmXuatKho.cs
│ └── FrmXuatKho.Designer.cs
│
├── Models
│ ├── HangHoa.cs
│ ├── LoaiHang.cs
│ ├── NhaCungCap.cs
│ ├── KhachHang.cs
│ ├── NhanVien.cs
│ ├── TaiKhoan.cs
│ ├── PhieuNhap.cs
│ ├── ChiTietPhieuNhap.cs
│ ├── PhieuXuat.cs
│ └── ChiTietPhieuXuat.cs
│
├── Repositories
│ ├── HangHoaRepository.cs
│ ├── LoaiHangRepository.cs
│ ├── NhaCungCapRepository.cs
│ ├── KhachHangRepository.cs
│ ├── NhanVienRepository.cs
│ ├── TaiKhoanRepository.cs
│ ├── PhieuNhapRepository.cs
│ └── PhieuXuatRepository.cs
│
├── Reports
│ ├── ExportExcel.cs
│ └── ExportPdf.cs
│
├── sql
│ ├── create_tables.sql
│ ├── sample_data.sql
│ ├── joins.sql
│ ├── views.sql
│ └── procedures.sql
│
├── .gitignore
├── README.md
├── Program.cs
├── QuanLyKhoHang.csproj
├── QuanLyKhoHang.csproj.user
│
├── Assets
│ ├── Images
│ └── Icons
├── Config
│ └── appsettings.json
│  
├── bin
└── obj
