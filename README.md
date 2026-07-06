# Inventory Management System

Ứng dụng quản lý kho hàng viết bằng C# WinForms, tách backend API riêng để xử lý nghiệp vụ và truy cập PostgreSQL.

## Cấu trúc solution

```text
Inventory-Management-System/
├── QuanLyKhoHang.sln
├── QuanLyKhoHang/        # WinForms giao diện
└── QuanLyKhoHang.Api/    # ASP.NET Core Minimal API backend
```

Luồng chạy chính:

```text
WinForms UI
  -> ApiClients
  -> QuanLyKhoHang.Api
  -> Repositories
  -> PostgreSQL
```

## Chức năng

- Đăng nhập và phân quyền `Admin` / `NhanVien`.
- Quản lý hàng hóa, khách hàng, nhân viên, loại hàng, nhà cung cấp.
- Nhập kho, xuất kho, tự động cập nhật tồn kho.
- Chặn xuất âm bằng kiểm tra tồn kho trong transaction.
- Tra cứu lịch sử phiếu nhập/xuất và chi tiết phiếu.
- Xuất báo cáo Excel/PDF từ WinForms.
- API backend cho đăng nhập, danh mục, tồn kho và chứng từ.

## Công nghệ

- .NET 10
- C# WinForms `net10.0-windows`
- ASP.NET Core Minimal API `net10.0`
- PostgreSQL
- Npgsql
- ClosedXML
- iTextSharp.LGPLv2.Core

## Cấu hình

WinForms đọc địa chỉ API từ:

```text
QuanLyKhoHang/Config/appsettings.json
```

API đọc cấu hình database và server từ:

```text
QuanLyKhoHang.Api/appsettings.json
```

Mặc định API chạy ở:

```text
http://localhost:5088
```

## Chạy nhanh

Từ thư mục root repository:

```powershell
dotnet restore QuanLyKhoHang.sln
dotnet build QuanLyKhoHang.sln
```

Chạy giao diện WinForms:

```powershell
dotnet run --project QuanLyKhoHang/QuanLyKhoHang.csproj
```

Khi mở app, WinForms sẽ kiểm tra API tại `/api/health`. Nếu API chưa chạy, app sẽ tự bật backend `QuanLyKhoHang.Api`.

Chạy API thủ công để test Postman:

```powershell
dotnet run --project QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj
```

## Database

Database mặc định:

```text
Database: quanlyhanghoa
User: postgres
Password: 1234
```

Script SQL nằm trong:

```text
QuanLyKhoHang/sql/
```

Thứ tự chạy thủ công:

```text
create_tables.sql
sample_data.sql
```

Với database cũ, chạy thêm khi cần:

```text
sync_existing_database.sql
migrate_add_trang_thai.sql
migrate_hash_sample_passwords.sql
```

## Tài khoản mẫu

```text
admin / 123456
nhanvienkho / 123456
nhanvienbanhang / 123456
```

## API chính

```http
GET  /api/health
POST /api/auth/login

GET,POST        /api/hang-hoa
PUT,DELETE      /api/hang-hoa/{id}
GET,POST        /api/khach-hang
PUT,DELETE      /api/khach-hang/{id}
GET,POST        /api/nhan-vien
PUT,DELETE      /api/nhan-vien/{id}

GET  /api/ton-kho/thap?soLuongToiDa=10
GET  /api/phieu-nhap
POST /api/phieu-nhap
GET  /api/phieu-nhap/{id}/chi-tiet
GET  /api/phieu-xuat
POST /api/phieu-xuat
GET  /api/phieu-xuat/{id}/chi-tiet
```

## Ghi chú

- Project `QuanLyKhoHang` là app giao diện, không truy cập database trực tiếp.
- Project `QuanLyKhoHang.Api` là backend, chứa kết nối database và repositories.
- Nếu dời folder, cần mở `QuanLyKhoHang.sln` ở root repository.
