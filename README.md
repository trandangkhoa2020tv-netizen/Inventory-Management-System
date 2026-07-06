# QuanLyKhoHang

Ứng dụng quản lý kho hàng viết bằng C# WinForms, dùng backend HTTP API riêng trong project `QuanLyKhoHang.Api` để xử lý nghiệp vụ và truy cập PostgreSQL.

## Chức năng chính

- Đăng nhập và phân quyền theo vai trò `Admin` hoặc `NhanVien`.
- Quản lý danh mục hàng hóa, khách hàng và nhân viên.
- Quản lý dữ liệu liên quan đến hàng hóa gồm loại hàng và nhà cung cấp qua backend API.
- Lập phiếu nhập kho, tính tổng tiền, lưu chi tiết phiếu và tự động cộng tồn kho.
- Lập phiếu xuất kho, kiểm tra tồn kho, chặn xuất âm và tự động trừ tồn kho.
- Tra cứu lịch sử phiếu nhập/xuất và chi tiết từng phiếu.
- Xuất dữ liệu phiếu ra Excel hoặc PDF.
- Cung cấp API riêng cho đăng nhập, danh mục, tồn kho thấp và chứng từ nhập/xuất.

## Công nghệ

- C# WinForms, target `net10.0-windows`.
- PostgreSQL.
- ASP.NET Core Minimal API chạy riêng trong project `QuanLyKhoHang.Api`.
- `Npgsql` cho truy cập PostgreSQL ở backend API.
- `ClosedXML` cho xuất Excel.
- `iTextSharp.LGPLv2.Core` cho xuất PDF.

## Cấu trúc thư mục

```text
QuanLyKhoHang/
  QuanLyKhoHang.sln     Solution gồm WinForms và backend API.
  QuanLyKhoHang.csproj  Project WinForms.
  ApiClients/          Client HTTP để WinForms gọi backend API.
  Config/              File cấu hình địa chỉ backend API cho WinForms.
  Forms/               Giao diện WinForms.
  Models/              Model dữ liệu dùng chung cho UI và API.
  Reports/             Xuất Excel/PDF.
  sql/                 Script tạo bảng, dữ liệu mẫu và migration.
  Dockerfile.build     Build/publish app bằng Windows container.
  docker-compose.yml   PostgreSQL phục vụ phát triển.
  Program.cs           Điểm khởi động ứng dụng WinForms.
  QuanLyKhoHang.Api/
    Program.cs             Backend API server riêng.
    appsettings.json       Cấu hình database và API server.
    Data/                  Kết nối PostgreSQL và helper chạy SQL.
    Repositories/          Tầng truy cập dữ liệu và nghiệp vụ database.
    QuanLyKhoHang.Api.csproj
```

## Cấu hình

File cấu hình WinForms:

```text
Config/appsettings.json
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

Ý nghĩa:

- `ApiClientSettings.BaseUrl`: địa chỉ backend API mà WinForms sẽ gọi.
- `ApiClientSettings.ApiKey`: key gửi lên API nếu backend bật yêu cầu API key.

File cấu hình backend API nằm ở:

```text
QuanLyKhoHang.Api\appsettings.json
```

Backend API giữ `DatabaseSettings` để kết nối PostgreSQL và `ApiSettings` để cấu hình URL, CORS, API key.

Biến môi trường có thể ghi đè cấu hình database:

```text
QLKH_DB_HOST
QLKH_DB_PORT
QLKH_DB_NAME
QLKH_DB_USER
QLKH_DB_PASSWORD
```

## Khởi tạo database

Cách nhanh bằng Docker:

```powershell
docker compose up -d postgres
```

Container PostgreSQL dùng database `quanlyhanghoa`, user `postgres`, mật khẩu `1234`. Khi volume được tạo lần đầu, Docker tự chạy:

```text
sql/create_tables.sql
sql/sample_data.sql
```

Cách thủ công:

1. Tạo database PostgreSQL tên `quanlyhanghoa`.
2. Kết nối vào database đó.
3. Chạy `sql/create_tables.sql`.
4. Chạy `sql/sample_data.sql`.

Với database cũ, có thể chạy thêm các script sau khi cần:

```text
sql/sync_existing_database.sql
sql/migrate_add_trang_thai.sql
sql/migrate_hash_sample_passwords.sql
```

## Tài khoản mẫu

```text
admin / 123456
nhanvienkho / 123456
nhanvienbanhang / 123456
```

Phân quyền hiện tại:

- `admin`: xem và dùng toàn bộ chức năng trên menu.
- `nhanvienkho`: dùng nhập kho, không dùng xuất kho và không vào màn hình nhân viên.
- `nhanvienbanhang`: dùng xuất kho, không dùng nhập kho và không vào màn hình nhân viên.

Mật khẩu mẫu trong database mới dùng PBKDF2. Code vẫn tương thích với database cũ đang lưu SHA-256 hoặc plain text để tránh lỗi khi nâng cấp.

## Chạy chương trình

Vào thư mục project:

```powershell
cd D:\QuanLyKhoHang\QuanLyKhoHang
```

Restore và build:

```powershell
dotnet restore
dotnet build QuanLyKhoHang.csproj
```

Chạy app WinForms:

```powershell
cd D:\QuanLyKhoHang\QuanLyKhoHang
dotnet run
```

Khi mở app, WinForms sẽ tự kiểm tra `http://localhost:5088/api/health`. Nếu API chưa chạy, app tự bật backend `QuanLyKhoHang.Api` bằng process riêng.

Nếu muốn chạy API thủ công để test Postman:

```powershell
cd D:\QuanLyKhoHang\QuanLyKhoHang
dotnet run --project QuanLyKhoHang.Api\QuanLyKhoHang.Api.csproj
```

Nếu build báo file `.exe` hoặc `.dll` đang bị khóa, hãy đóng cửa sổ ứng dụng `QuanLyKhoHang` đang chạy rồi build lại.

## Backend API

API chạy riêng với project `QuanLyKhoHang.Api`.

Base URL mặc định:

```text
http://localhost:5088
```

Endpoint hệ thống:

```http
GET /api/health
GET /api/chuc-nang
GET /api/docs
POST /api/auth/login
```

Nếu bật `RequireApiKey`, gửi một trong hai header:

```http
X-API-Key: <api-key>
Authorization: Bearer <api-key>
```

Endpoint hàng hóa:

```http
GET    /api/hang-hoa
POST   /api/hang-hoa
PUT    /api/hang-hoa/{id}
DELETE /api/hang-hoa/{id}
```

Ví dụ thêm hàng hóa:

```http
POST http://localhost:5088/api/hang-hoa
Content-Type: application/json

{
  "tenHangHoa": "Ban phim co",
  "maLoaiHang": 1,
  "maNhaCungCap": 1,
  "giaNhap": 300000,
  "giaBan": 450000,
  "soLuongTon": 20,
  "donViTinh": "Cai",
  "ghiChu": "Hang moi"
}
```

Endpoint danh mục:

```http
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

Endpoint kho và chứng từ:

```http
GET /api/ton-kho/thap?soLuongToiDa=10
GET /api/phieu-nhap
POST /api/phieu-nhap
GET /api/phieu-nhap/{id}/chi-tiet
GET /api/phieu-xuat
POST /api/phieu-xuat
GET /api/phieu-xuat/{id}/chi-tiet
GET /api/phieu-xuat/{id}/thong-tin
```

Các endpoint `POST`/`PUT` có validate dữ liệu cơ bản và trả `400` nếu body không hợp lệ. `PUT`/`DELETE` trả `404` nếu không tìm thấy dòng cần xử lý.

## Luồng nghiệp vụ

Nhập kho:

1. Người dùng mở màn hình nhập kho.
2. Chọn nhà cung cấp, nhân viên, hàng hóa, số lượng và đơn giá.
3. Thêm hàng vào bảng chi tiết tạm.
4. Hệ thống tính tổng tiền.
5. Khi lưu, WinForms gọi API; backend tạo transaction, thêm phiếu, thêm chi tiết, cộng tồn kho và commit.
6. Nếu có lỗi, transaction rollback.

Xuất kho:

1. Người dùng mở màn hình xuất kho.
2. Chọn khách hàng, nhân viên, hàng hóa, số lượng và đơn giá.
3. Form kiểm tra số lượng xuất không vượt tồn kho.
4. Khi lưu, WinForms gọi API; backend tạo transaction, thêm phiếu, trừ tồn kho theo điều kiện `so_luong_ton >= @soluong`, thêm chi tiết và commit.
5. Nếu thiếu tồn hoặc có lỗi, transaction rollback.

## Docker build ứng dụng

Dự án là WinForms target `net10.0-windows`, vì vậy `Dockerfile.build` chỉ dùng để build/publish artifact bằng Windows container, không dùng để chạy giao diện GUI trong container.

Yêu cầu Docker Desktop ở chế độ Windows containers.

Build image chứa bản publish:

```powershell
docker build -f Dockerfile.build -t quan-ly-kho-hang:build .
```

Lấy thư mục publish ra máy host:

```powershell
docker create --name qlkh-build quan-ly-kho-hang:build
docker cp qlkh-build:C:\app .\publish
docker rm qlkh-build
```

Đổi runtime nếu cần:

```powershell
docker build -f Dockerfile.build --build-arg RUNTIME=win-x64 -t quan-ly-kho-hang:build .
```

## Lỗi thường gặp

`column "trang_thai" does not exist`

Database cũ thiếu cột trạng thái tài khoản. Chạy `sql/migrate_add_trang_thai.sql` hoặc `sql/sync_existing_database.sql`. Code hiện tại cũng có kiểm tra tương thích để tránh lỗi đăng nhập với schema cũ.

Không kết nối được database

- Kiểm tra PostgreSQL đã chạy chưa.
- Kiểm tra database tên `quanlyhanghoa` đã tồn tại chưa.
- Kiểm tra `Host`, `Port`, `Username`, `Password` trong `QuanLyKhoHang.Api\appsettings.json`.
- Kiểm tra đã chạy `create_tables.sql` và `sample_data.sql` chưa.

API không truy cập được

- Kiểm tra backend `QuanLyKhoHang.Api` đã chạy chưa.
- Kiểm tra port trong `QuanLyKhoHang.Api\appsettings.json` chưa bị ứng dụng khác chiếm.
- Kiểm tra `ApiClientSettings.BaseUrl` trong WinForms trỏ đúng URL backend.
- Nếu bật `RequireApiKey`, kiểm tra header `X-API-Key` hoặc `Authorization`.
- Nếu gọi từ trình duyệt/web app khác origin, kiểm tra `AllowedOrigins`.

## Ghi chú bảo trì

- Giữ SQL có tham số `NpgsqlParameter` ở backend API để tránh SQL injection.
- Không sửa `*.Designer.cs` nếu chỉ thay đổi logic xử lý.
- Khi đổi alias tên cột trong repository/API, kiểm tra các form và JSON response đang đọc tên cột đó.
- Các thao tác nhập/xuất kho cần tiếp tục dùng transaction để không lệch tồn kho.
