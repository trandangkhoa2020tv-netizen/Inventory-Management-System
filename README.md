# Inventory Management System

Hệ thống quản lý kho hàng viết bằng C# WinForms, sử dụng backend API riêng để xử lý nghiệp vụ và truy cập PostgreSQL.

Repository hiện được tổ chức theo mô hình 2 project:

```text
WinForms UI -> HTTP API -> PostgreSQL
```

Điểm chính của dự án là tách giao diện và backend ra riêng. Giao diện không truy cập database trực tiếp, mà gọi API qua `HttpClient`.

## Mục Tiêu

Dự án phục vụ bài toán quản lý kho cơ bản:

- Quản lý hàng hóa, loại hàng, nhà cung cấp.
- Quản lý khách hàng và nhân viên.
- Đăng nhập và phân quyền theo vai trò.
- Lập phiếu nhập kho, cộng tồn kho.
- Lập phiếu xuất kho, kiểm tra tồn kho và trừ tồn kho.
- Xem lịch sử nhập/xuất và chi tiết chứng từ.
- Xuất báo cáo Excel/PDF.
- Cung cấp API backend để có thể test bằng Postman hoặc mở rộng sang client khác sau này.

## Cấu Trúc Repository

```text
Inventory-Management-System/
├── .github/
│   └── workflows/
│       └── build.yml
├── QuanLyKhoHang/
│   ├── ApiClients/
│   ├── Config/
│   ├── Forms/
│   ├── Models/
│   ├── Reports/
│   ├── sql/
│   ├── Program.cs
│   └── QuanLyKhoHang.csproj
├── QuanLyKhoHang.Api/
│   ├── Data/
│   ├── Repositories/
│   ├── Properties/
│   ├── Program.cs
│   ├── appsettings.json
│   └── QuanLyKhoHang.Api.csproj
├── .gitignore
├── QuanLyKhoHang.sln
└── README.md
```

Ý nghĩa các project:

| Project | Vai trò |
| --- | --- |
| `QuanLyKhoHang` | Ứng dụng WinForms, hiển thị giao diện và gọi API. |
| `QuanLyKhoHang.Api` | Backend ASP.NET Core Minimal API, xử lý nghiệp vụ và database. |

## Kiến Trúc

Luồng xử lý tiêu biểu:

```text
FrmNhanVien
  -> NhanVienApiClient
  -> DELETE /api/nhan-vien/{id}
  -> QuanLyKhoHang.Api
  -> NhanVienRepository
  -> PostgreSQL
```

Luồng nhập kho:

```text
FrmNhapKho
  -> KhoApiClient
  -> POST /api/phieu-nhap
  -> PhieuNhapRepository
  -> transaction:
       1. tạo phiếu nhập
       2. thêm chi tiết phiếu
       3. cộng tồn kho
```

Luồng xuất kho:

```text
FrmXuatKho
  -> KhoApiClient
  -> POST /api/phieu-xuat
  -> PhieuXuatRepository
  -> transaction:
       1. tạo phiếu xuất
       2. kiểm tra tồn kho
       3. trừ tồn kho
       4. thêm chi tiết phiếu
```

## Công Nghệ

| Thành phần | Công nghệ |
| --- | --- |
| Desktop UI | C# WinForms |
| UI target framework | `net10.0-windows` |
| Backend | ASP.NET Core Minimal API |
| API target framework | `net10.0` |
| Database | PostgreSQL |
| Database driver | Npgsql |
| Excel export | ClosedXML |
| PDF export | iTextSharp.LGPLv2.Core |
| CI | GitHub Actions |

## Chức Năng Chính

### Tài khoản và phân quyền

- Đăng nhập bằng tài khoản trong bảng `taikhoan`.
- Hỗ trợ vai trò `Admin` và `NhanVien`.
- Nhân viên thường bị giới hạn menu tùy tài khoản.

Tài khoản mẫu:

```text
admin / 123456
nhanvienkho / 123456
nhanvienbanhang / 123456
```

### Danh mục

- Hàng hóa.
- Loại hàng.
- Nhà cung cấp.
- Khách hàng.
- Nhân viên.

### Kho

- Tạo phiếu nhập.
- Tạo phiếu xuất.
- Tự động cập nhật tồn kho.
- Chặn xuất âm bằng điều kiện database trong transaction.
- Xem hàng tồn thấp.

### Báo cáo

- Xuất Excel.
- Xuất PDF.
- Xem chi tiết phiếu nhập/xuất.

## Cấu Hình

### Cấu hình WinForms

File:

```text
QuanLyKhoHang/Config/appsettings.json
```

Ví dụ:

```json
{
  "ApiClientSettings": {
    "BaseUrl": "http://localhost:5088",
    "ApiKey": ""
  }
}
```

### Cấu hình API

File:

```text
QuanLyKhoHang.Api/appsettings.json
```

Ví dụ:

```json
{
  "DatabaseSettings": {
    "Host": "localhost",
    "Port": 5432,
    "Database": "quanlyhanghoa",
    "Username": "postgres",
    "Password": "1234"
  },
  "ApiSettings": {
    "Url": "http://localhost:5088",
    "RequireApiKey": false,
    "ApiKey": "",
    "AllowedOrigins": []
  }
}
```

Biến môi trường có thể ghi đè cấu hình database:

```text
QLKH_DB_HOST
QLKH_DB_PORT
QLKH_DB_NAME
QLKH_DB_USER
QLKH_DB_PASSWORD
```

## Cài Đặt Database

Database mặc định:

```text
Database: quanlyhanghoa
Username: postgres
Password: 1234
Port: 5432
```

Script SQL nằm trong:

```text
QuanLyKhoHang/sql/
```

Thứ tự chạy với database mới:

```text
create_tables.sql
sample_data.sql
```

Với database cũ, có thể chạy thêm:

```text
sync_existing_database.sql
migrate_add_trang_thai.sql
migrate_hash_sample_passwords.sql
```

## Build Và Chạy

Từ thư mục root repository:

```powershell
dotnet restore QuanLyKhoHang.sln
dotnet build QuanLyKhoHang.sln
```

Chạy giao diện WinForms:

```powershell
dotnet run --project QuanLyKhoHang/QuanLyKhoHang.csproj
```

Chạy API thủ công:

```powershell
dotnet run --project QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj
```

Khi chạy WinForms, app sẽ kiểm tra API tại:

```text
http://localhost:5088/api/health
```

Nếu API chưa chạy, WinForms sẽ tự bật backend `QuanLyKhoHang.Api`.

## API Nhanh

Base URL mặc định:

```text
http://localhost:5088
```

Endpoint hệ thống:

```http
GET  /api/health
GET  /api/chuc-nang
GET  /api/docs
POST /api/auth/login
```

Endpoint danh mục:

```http
GET,POST   /api/hang-hoa
PUT,DELETE /api/hang-hoa/{id}

GET,POST   /api/loai-hang
PUT,DELETE /api/loai-hang/{id}

GET,POST   /api/nha-cung-cap
PUT,DELETE /api/nha-cung-cap/{id}

GET,POST   /api/khach-hang
PUT,DELETE /api/khach-hang/{id}

GET,POST   /api/nhan-vien
PUT,DELETE /api/nhan-vien/{id}
```

Endpoint kho:

```http
GET  /api/ton-kho/thap?soLuongToiDa=10

GET  /api/phieu-nhap
POST /api/phieu-nhap
GET  /api/phieu-nhap/{id}/chi-tiet

GET  /api/phieu-xuat
POST /api/phieu-xuat
GET  /api/phieu-xuat/{id}/chi-tiet
GET  /api/phieu-xuat/{id}/thong-tin
```

## Lỗi Thường Gặp

### Visual Studio chỉ chạy API, không mở giao diện

Đặt startup project là:

```text
QuanLyKhoHang
```

Không đặt startup project là:

```text
QuanLyKhoHang.Api
```

### API hiện cửa sổ terminal

API là server nên khi chạy trực tiếp nó sẽ chạy liên tục để nhận request. Nếu chạy đúng từ WinForms, app có thể tự bật API nền. Nếu chạy API thủ công, terminal API là bình thường.

### Không kết nối được database

Kiểm tra:

- PostgreSQL đã chạy chưa.
- Database `quanlyhanghoa` đã tồn tại chưa.
- `appsettings.json` của API đã đúng host/user/password chưa.
- Đã chạy `create_tables.sql` và `sample_data.sql` chưa.

### Xóa nhân viên không được

Nếu nhân viên đã có phiếu nhập/xuất, API sẽ không xóa để giữ lịch sử chứng từ. Nhân viên chưa có chứng từ thì có thể xóa; tài khoản liên quan sẽ được xóa trong cùng transaction.

## Ghi Chú Phát Triển

- Mở solution bằng `QuanLyKhoHang.sln` ở root repository.
- Không commit `bin/`, `obj/`, `.vs/`.
- Giữ repository database ở project `QuanLyKhoHang.Api`.
- Giữ UI ở project `QuanLyKhoHang`.
- Khi đổi tên cột trả về từ API, kiểm tra lại các form đang bind `DataGridView`.
- Các thao tác nhập/xuất kho phải tiếp tục dùng transaction để tránh lệch tồn kho.
