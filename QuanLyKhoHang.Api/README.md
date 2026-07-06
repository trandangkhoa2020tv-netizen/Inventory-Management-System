# QuanLyKhoHang.Api

Backend API server cho hệ thống quản lý kho hàng.

Project này chứa endpoint API, cấu hình database và repository truy cập PostgreSQL. Giao diện WinForms trong project `QuanLyKhoHang` gọi API này qua `ApiClients`.

## Vai trò

```text
QuanLyKhoHang.Api
  -> nhận request HTTP từ WinForms
  -> validate dữ liệu cơ bản
  -> xử lý nghiệp vụ nhập/xuất kho
  -> gọi repository
  -> đọc/ghi PostgreSQL
```

## Cấu trúc

```text
QuanLyKhoHang.Api/
├── Data/                 # DbConnection, DatabaseHelper, maintenance
├── Repositories/         # Truy cập dữ liệu và nghiệp vụ database
├── Properties/
│   └── launchSettings.json
├── DataTableJson.cs      # Chuyển DataTable sang JSON rows
├── InventoryApiQueries.cs
├── Program.cs            # Minimal API endpoints
├── appsettings.json      # DatabaseSettings và ApiSettings
└── QuanLyKhoHang.Api.csproj
```

API hiện dùng Minimal API trong `Program.cs`, nên chưa có thư mục `Controllers`. Nếu refactor sau này có thể tách endpoint sang Controllers/Services/DTOs.

## Cấu hình

File:

```text
appsettings.json
```

Các phần chính:

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

Biến môi trường có thể ghi đè database:

```text
QLKH_DB_HOST
QLKH_DB_PORT
QLKH_DB_NAME
QLKH_DB_USER
QLKH_DB_PASSWORD
```

## Chạy API

Từ root repository:

```powershell
dotnet run --project QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj
```

Mặc định API lắng nghe tại:

```text
http://localhost:5088
```

Kiểm tra health:

```http
GET http://localhost:5088/api/health
```

## Endpoint chính

```http
GET /api/health
GET /api/chuc-nang
GET /api/docs
POST /api/auth/login

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

GET /api/ton-kho/thap?soLuongToiDa=10

GET  /api/phieu-nhap
POST /api/phieu-nhap
GET  /api/phieu-nhap/{id}/chi-tiet

GET  /api/phieu-xuat
POST /api/phieu-xuat
GET  /api/phieu-xuat/{id}/chi-tiet
GET  /api/phieu-xuat/{id}/thong-tin
```

## Xử lý nghiệp vụ đáng chú ý

- Phiếu nhập và phiếu xuất được lưu trong transaction.
- Xuất kho trừ tồn bằng điều kiện `so_luong_ton >= @soluong` để tránh xuất âm.
- Xóa nhân viên sẽ xóa tài khoản liên quan nếu nhân viên chưa có phiếu nhập/xuất.
- Nếu nhân viên đã có lịch sử chứng từ, API trả `400` để giữ toàn vẹn dữ liệu.

## Gọi từ WinForms

WinForms đọc base URL từ:

```text
QuanLyKhoHang/Config/appsettings.json
```

Mặc định:

```text
http://localhost:5088
```
