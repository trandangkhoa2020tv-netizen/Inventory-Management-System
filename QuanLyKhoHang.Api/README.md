# QuanLyKhoHang.Api

`QuanLyKhoHang.Api` là backend API server của hệ thống quản lý kho hàng.

Project này chịu trách nhiệm nhận request HTTP từ WinForms, validate dữ liệu cơ bản, xử lý nghiệp vụ kho và truy cập PostgreSQL.

## Vai Trò Trong Solution

```text
QuanLyKhoHang WinForms
  -> HTTP request
  -> QuanLyKhoHang.Api
  -> Repositories
  -> PostgreSQL
```

Backend hiện dùng ASP.NET Core Minimal API. Tất cả endpoint đang được khai báo trong `Program.cs`, chưa tách sang `Controllers`.

## Cấu Trúc Thư Mục

```text
QuanLyKhoHang.Api/
├── Data/
│   ├── DatabaseHelper.cs
│   ├── DatabaseMaintenance.cs
│   └── DbConnection.cs
├── Repositories/
│   ├── HangHoaRepository.cs
│   ├── KhachHangRepository.cs
│   ├── LoaiHangRepository.cs
│   ├── NhaCungCapRepository.cs
│   ├── NhanVienRepository.cs
│   ├── PhieuNhapRepository.cs
│   ├── PhieuXuatRepository.cs
│   └── TaiKhoanRepository.cs
├── Properties/
│   └── launchSettings.json
├── DataTableJson.cs
├── InventoryApiQueries.cs
├── Program.cs
├── appsettings.json
├── QuanLyKhoHang.Api.csproj
└── README.md
```

## Các Thành Phần Chính

### `Program.cs`

Chứa:

- Cấu hình `ApiSettings`.
- Cấu hình CORS.
- Đăng ký repositories vào DI container.
- Khai báo các endpoint Minimal API.
- Middleware kiểm tra API key nếu bật.
- Health check `/api/health`.
- Xử lý lỗi chung qua helper `Safe(...)`.

### `Data/`

| File | Vai trò |
| --- | --- |
| `DbConnection.cs` | Đọc cấu hình database và tạo `NpgsqlConnection`. |
| `DatabaseHelper.cs` | Helper chạy query, non-query, scalar. |
| `DatabaseMaintenance.cs` | Đồng bộ sequence khi API khởi động. |

### `Repositories/`

Repository là tầng truy cập database và chứa một phần nghiệp vụ gần database.

| Repository | Vai trò |
| --- | --- |
| `TaiKhoanRepository` | Đăng nhập, kiểm tra mật khẩu, vai trò. |
| `HangHoaRepository` | CRUD hàng hóa. |
| `LoaiHangRepository` | CRUD loại hàng. |
| `NhaCungCapRepository` | CRUD nhà cung cấp. |
| `KhachHangRepository` | CRUD khách hàng. |
| `NhanVienRepository` | CRUD nhân viên, xóa an toàn theo ràng buộc chứng từ. |
| `PhieuNhapRepository` | Tạo phiếu nhập và cộng tồn kho bằng transaction. |
| `PhieuXuatRepository` | Tạo phiếu xuất và trừ tồn kho bằng transaction. |

### Model dùng chung

API project link model từ project WinForms:

```xml
<Compile Include="..\QuanLyKhoHang\Models\**\*.cs" Link="Models\%(RecursiveDir)%(Filename)%(Extension)" />
```

Vì vậy nếu đổi vị trí folder hoặc đổi tên `QuanLyKhoHang/Models`, cần cập nhật lại đường dẫn này trong `QuanLyKhoHang.Api.csproj`.

## Cấu Hình

File:

```text
appsettings.json
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

### DatabaseSettings

| Key | Ý nghĩa |
| --- | --- |
| `Host` | Máy chủ PostgreSQL. |
| `Port` | Port PostgreSQL. |
| `Database` | Tên database. |
| `Username` | User kết nối database. |
| `Password` | Mật khẩu database. |

### ApiSettings

| Key | Ý nghĩa |
| --- | --- |
| `Url` | URL API lắng nghe. |
| `RequireApiKey` | Bật/tắt yêu cầu API key. |
| `ApiKey` | API key hợp lệ. |
| `AllowedOrigins` | Danh sách origin được CORS cho phép. Rỗng hoặc `*` nghĩa là cho phép mọi origin. |

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

API mặc định lắng nghe tại:

```text
http://localhost:5088
```

Kiểm tra API:

```http
GET http://localhost:5088/api/health
```

Route `/` redirect về `/api/health`, nên nếu mở `http://localhost:5088` bằng trình duyệt sẽ không còn gây hiểu nhầm là API lỗi.

## Endpoint Hệ Thống

```http
GET /api/health
GET /api/chuc-nang
GET /api/docs
POST /api/auth/login
```

Đăng nhập:

```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "123456"
}
```

## Endpoint Danh Mục

```http
GET    /api/hang-hoa
POST   /api/hang-hoa
PUT    /api/hang-hoa/{id}
DELETE /api/hang-hoa/{id}

GET    /api/loai-hang
POST   /api/loai-hang
PUT    /api/loai-hang/{id}
DELETE /api/loai-hang/{id}

GET    /api/nha-cung-cap
POST   /api/nha-cung-cap
PUT    /api/nha-cung-cap/{id}
DELETE /api/nha-cung-cap/{id}

GET    /api/khach-hang
POST   /api/khach-hang
PUT    /api/khach-hang/{id}
DELETE /api/khach-hang/{id}

GET    /api/nhan-vien
POST   /api/nhan-vien
PUT    /api/nhan-vien/{id}
DELETE /api/nhan-vien/{id}
```

## Endpoint Kho

```http
GET /api/ton-kho/thap?soLuongToiDa=10

GET  /api/phieu-nhap
POST /api/phieu-nhap
GET  /api/phieu-nhap/{id}/chi-tiet

GET  /api/phieu-xuat
POST /api/phieu-xuat
GET  /api/phieu-xuat/{id}/chi-tiet
GET  /api/phieu-xuat/{id}/thong-tin
```

## Xử Lý Lỗi

API dùng helper `Safe(...)` để gom lỗi:

- Dữ liệu không hợp lệ trả `400 Bad Request`.
- Không tìm thấy dữ liệu khi update/delete trả `404 Not Found`.
- Lỗi hệ thống trả `500`.
- Lỗi nghiệp vụ như xóa nhân viên đã có phiếu nhập/xuất trả `400`.

Client WinForms đọc `message`, `detail` hoặc `errors` trong JSON response để hiển thị MessageBox.

## Nghiệp Vụ Quan Trọng

### Nhập kho

`PhieuNhapRepository.LuuPhieuNhap(...)` chạy trong transaction:

1. Tạo phiếu nhập.
2. Thêm từng dòng chi tiết phiếu.
3. Cộng `so_luong_ton`.
4. Commit nếu tất cả thành công.
5. Rollback nếu có lỗi.

### Xuất kho

`PhieuXuatRepository.LuuPhieuXuat(...)` chạy trong transaction:

1. Tạo phiếu xuất.
2. Trừ tồn kho bằng điều kiện:

```sql
WHERE ma_hanghoa = @mahang AND so_luong_ton >= @soluong
```

3. Nếu không đủ tồn, throw lỗi nghiệp vụ.
4. Thêm chi tiết phiếu xuất.
5. Commit nếu tất cả thành công.

### Xóa nhân viên

`NhanVienRepository.Xoa(...)` xử lý:

- Nếu nhân viên chưa có phiếu nhập/xuất: xóa tài khoản liên quan rồi xóa nhân viên.
- Nếu nhân viên đã có chứng từ: không xóa để giữ lịch sử dữ liệu.

## API Key

Nếu bật:

```json
"RequireApiKey": true
```

Client cần gửi một trong hai header:

```http
X-API-Key: <api-key>
Authorization: Bearer <api-key>
```

## Ghi Chú Phát Triển

- API hiện là Minimal API, chưa dùng `Controllers`.
- Nếu project lớn hơn, có thể tách dần thành `Controllers`, `Services`, `DTOs`.
- Không đưa logic UI vào API.
- Không đưa SQL vào WinForms.
- Luôn dùng `NpgsqlParameter` khi viết query có input từ người dùng.
- Với nghiệp vụ nhập/xuất kho, luôn dùng transaction.
