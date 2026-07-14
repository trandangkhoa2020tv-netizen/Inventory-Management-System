# QuanLyKhoHang.Api - Backend API

`QuanLyKhoHang.Api` la backend ASP.NET Core Minimal API cua he thong quan ly kho hang.

API phu trach:

- Nhan request HTTP tu WinForms hoac client khac.
- Xac thuc dang nhap va phat hanh JWT.
- Doc Bearer token, gan user/role vao request va phan quyen endpoint can quyen `Admin`.
- Validate du lieu dau vao.
- Xu ly nghiep vu nhap/xuat kho bang transaction.
- Goi repository de thao tac PostgreSQL.
- Ghi audit log cho thao tac thay doi du lieu.
- Tra JSON ve cho client.

Project dung Minimal API nen khong co `Controllers/`; route nam trong `Endpoints/`.

## Luong Xu Ly Hien Tai

```txt
HTTP request
->
API key middleware neu RequireApiKey = true
->
JWT reader middleware
  - doc Authorization: Bearer <jwt>
  - gan username/role vao HttpContext.User
->
JWT guard neu RequireJwt = true
->
Rate limiter
->
Endpoint
->
ApiAuthorization neu endpoint can role Admin
->
Service validate va xu ly nghiep vu
->
Repository thao tac PostgreSQL
->
AuditLogService ghi log neu co thay doi du lieu
->
ApiResults chuan hoa response JSON
```

Route public khi `JwtSettings.RequireJwt = true`:

```txt
/
/api/health
/api/chuc-nang
/api/docs
/api/auth/login
/swagger
```

## Cau Truc Thu Muc

```txt
QuanLyKhoHang.Api/
|   .gitignore
|   appsettings.json
|   appsettings.Production.json
|   DataTableJson.cs
|   Dockerfile
|   InventoryApiQueries.cs
|   Program.cs
|   QuanLyKhoHang.Api.csproj
|   README.md
|
+---Config/
|       ApiSettings.cs
|       JwtSettings.cs
|
+---Data/
|       DatabaseHelper.cs
|       DatabaseMaintenance.cs
|       DbConnection.cs
|
+---DTOs/
|       AuthDtos.cs
|       DataTableDtoMapper.cs
|       PhieuKhoDtos.cs
|       ResponseDtos.cs
|
+---Endpoints/
|       AuthEndpoints.cs
|       HangHoaEndpoints.cs
|       KhachHangEndpoints.cs
|       KhoEndpoints.cs
|       LoaiHangEndpoints.cs
|       NhaCungCapEndpoints.cs
|       NhanVienEndpoints.cs
|       PhieuNhapEndpoints.cs
|       PhieuXuatEndpoints.cs
|       SystemEndpoints.cs
|
+---Properties/
|       launchSettings.json
|
+---Repositories/
|       HangHoaRepository.cs
|       KhachHangRepository.cs
|       LoaiHangRepository.cs
|       NhaCungCapRepository.cs
|       NhanVienRepository.cs
|       PhieuNhapRepository.cs
|       PhieuXuatRepository.cs
|       TaiKhoanRepository.cs
|
\---Services/
        ApiAuthorization.cs
        ApiKeyValidator.cs
        ApiResults.cs
        ApiValidationException.cs
        AuditLogService.cs
        AuthService.cs
        DesktopClientLauncher.cs
        HangHoaService.cs
        JwtTokenService.cs
        KhachHangService.cs
        KhoService.cs
        LoaiHangService.cs
        NhaCungCapService.cs
        NhanVienService.cs
        PhieuNhapService.cs
        PhieuXuatService.cs
        ValidationHelper.cs
```

## Program.cs

`Program.cs` giu phan bootstrap:

```txt
Program.cs
|-- Doc ApiSettings va JwtSettings
|-- Validate production configuration neu khong phai Development
|-- Cau hinh URL lang nghe
|-- Cau hinh JSON case-insensitive
|-- Cau hinh rate limiter
|-- Cau hinh Swagger cho Development
|-- Cau hinh CORS
|-- Dang ky Repository vao DI
|-- Dang ky Service vao DI
|-- Dang ky InventoryApiQueries
|-- Dang ky JwtTokenService va AuditLogService
|-- Build app
|-- EnsureRuntimeSchema, EnsureSampleAccountPasswords, EnsureSerialSequences
|-- Bat HSTS/HTTPS redirection ngoai Development
|-- Bat CORS
|-- Bat Swagger trong Development
|-- Kiem tra API key neu RequireApiKey = true
|-- Doc JWT va gan user/role vao HttpContext
|-- Chan endpoint nghiep vu neu RequireJwt = true ma token sai/thieu
|-- UseRateLimiter
|-- Map endpoint theo tung nhom
|-- Dang ky DesktopClientLauncher neu khong bi disable bang bien moi truong
|-- app.Run()
```

## File Goc Cua Project

| File | Chuc nang |
| --- | --- |
| `Program.cs` | Bootstrap Minimal API, middleware, DI, runtime database maintenance va map route. |
| `InventoryApiQueries.cs` | Truy van ton kho thap cho `KhoService`. |
| `DataTableJson.cs` | Chuyen `DataTable` thanh danh sach dictionary de route `/api/...` serialize JSON cho WinForms. |
| `Dockerfile` | Build/publish API bang .NET SDK 10 va chay bang ASP.NET runtime 10 tai cong container `8080`. |
| `.gitignore` | Bo qua output build, publish, `.env` va file database local trong project API. |
| `appsettings.json` | Cau hinh development/local. |
| `appsettings.Production.json` | Cau hinh production override, de secret rong va set qua moi truong chay that. |

## Cau Hinh

File chinh:

```txt
QuanLyKhoHang.Api/appsettings.json
QuanLyKhoHang.Api/appsettings.Production.json
```

Vi du cau hinh. Mat khau database va secret khong duoc ghi vao README; de rong trong vi du va set bang cau hinh local/bien moi truong:

```json
{
  "DatabaseSettings": {
    "Host": "localhost",
    "Port": 5432,
    "Database": "quanlyhanghoa",
    "Username": "postgres",
    "Password": ""
  },
  "ApiSettings": {
    "Url": "http://localhost:8088",
    "RequireApiKey": false,
    "ApiKey": "",
    "AllowedOrigins": []
  },
  "JwtSettings": {
    "RequireJwt": true,
    "Issuer": "QuanLyKhoHang.Api",
    "Audience": "QuanLyKhoHang.WinForms",
    "SecretKey": "",
    "ExpirationMinutes": 480
  }
}
```

Khi chay API local/Visual Studio, `DatabaseSettings` tro toi PostgreSQL local tren `localhost:5432`. Khi chay API trong Docker, `docker-compose.yml` ghi de database thanh `QLKH_DB_HOST=postgres` va `QLKH_DB_PORT=5432`; day la dia chi noi bo container, khong phu thuoc port publish tren Windows.

### DatabaseSettings

| Key | Y nghia |
| --- | --- |
| `Host` | May chu PostgreSQL. |
| `Port` | Port PostgreSQL. |
| `Database` | Ten database. |
| `Username` | User database. |
| `Password` | Mat khau database. Trong README de rong; set rieng tren may chay that. |

Bien moi truong ghi de database:

```txt
QLKH_DB_HOST
QLKH_DB_PORT
QLKH_DB_NAME
QLKH_DB_USER
QLKH_DB_PASSWORD
```

### ApiSettings

| Key | Y nghia |
| --- | --- |
| `Url` | Dia chi API lang nghe. Production phai dung HTTPS. |
| `RequireApiKey` | Neu `true`, client phai gui API key hop le. |
| `ApiKey` | API key hop le. De rong trong README; set rieng neu bat RequireApiKey. |
| `AllowedOrigins` | CORS origins duoc phep. Development co the rong; production phai gioi han. |

Client gui API key bang mot trong hai header:

```http
X-API-Key: <api-key>
Authorization: Bearer <api-key>
```

### JwtSettings

| Key | Y nghia |
| --- | --- |
| `RequireJwt` | Neu `true`, endpoint nghiep vu can Bearer token hop le. |
| `Issuer` | Issuer ghi trong token. |
| `Audience` | Audience ghi trong token. |
| `SecretKey` | Khoa ky HMAC SHA-256. De rong trong README; set rieng tren may chay that. |
| `ExpirationMinutes` | Thoi gian song cua token tinh bang phut. |

Login thanh cong tra token:

```json
{
  "tenTaiKhoan": "admin",
  "vaiTro": "Admin",
  "token": "<jwt>",
  "expiresAt": "2026-07-10T12:00:00Z"
}
```

Client gui token cho request tiep theo:

```http
Authorization: Bearer <jwt>
```

## Production Hardening

Khi khong phai Development, API kiem tra cau hinh truoc khi chay:

- `JwtSettings.RequireJwt` phai bat.
- `JwtSettings.SecretKey` phai la secret rieng, khong dung gia tri development va khong de rong.
- Neu `ApiSettings.RequireApiKey = true` thi `ApiSettings.ApiKey` khong duoc rong.
- `ApiSettings.AllowedOrigins` phai gioi han origin, khong de mo toan bo.
- `ApiSettings.Url` phai dung HTTPS.
- Mat khau database khong duoc dung gia tri demo; trong tai lieu luon de rong va set qua moi truong chay that.

Vi du override bang bien moi truong:

```txt
QLKH_DB_PASSWORD
ApiSettings__ApiKey
ApiSettings__AllowedOrigins__0
JwtSettings__SecretKey
```

## Data

```txt
Data/
|   DatabaseHelper.cs
|   DatabaseMaintenance.cs
|   DbConnection.cs
```

| File | Chuc nang |
| --- | --- |
| `DbConnection.cs` | Doc cau hinh database, ap dung bien moi truong va tao `NpgsqlConnection`. |
| `DatabaseHelper.cs` | Helper chay `ExecuteQuery`, `ExecuteNonQuery`, `ExecuteScalar`. |
| `DatabaseMaintenance.cs` | Tao/cap nhat runtime schema can thiet, hash lai mat khau mau cu, dong bo sequence khi API khoi dong. |

## DTOs

```txt
DTOs/
|   AuthDtos.cs
|   DataTableDtoMapper.cs
|   PhieuKhoDtos.cs
|   ResponseDtos.cs
```

| File | Chuc nang |
| --- | --- |
| `AuthDtos.cs` | `LoginRequest`. |
| `PhieuKhoDtos.cs` | `LuuPhieuNhapRequest`, `LuuPhieuXuatRequest`. |
| `ResponseDtos.cs` | DTO output typed object cho hang hoa, khach hang, nhan vien, phieu nhap, phieu xuat... |
| `DataTableDtoMapper.cs` | Chuyen DataTable cu sang DTO cho endpoint `/api/v2`. |

## Endpoints

```txt
Endpoints/
|   AuthEndpoints.cs
|   HangHoaEndpoints.cs
|   KhachHangEndpoints.cs
|   KhoEndpoints.cs
|   LoaiHangEndpoints.cs
|   NhaCungCapEndpoints.cs
|   NhanVienEndpoints.cs
|   PhieuNhapEndpoints.cs
|   PhieuXuatEndpoints.cs
|   SystemEndpoints.cs
```

| File | Route chinh | Ghi chu |
| --- | --- | --- |
| `SystemEndpoints.cs` | `/`, `/api/health`, `/api/chuc-nang`, `/api/docs` | Public. |
| `AuthEndpoints.cs` | `/api/auth/login` | Public, rate limit Login. |
| `HangHoaEndpoints.cs` | `/api/hang-hoa`, `/api/v2/hang-hoa` | Read/create/update/delete co rate limit; delete can Admin. |
| `LoaiHangEndpoints.cs` | `/api/loai-hang`, `/api/v2/loai-hang` | CRUD loai hang. |
| `NhaCungCapEndpoints.cs` | `/api/nha-cung-cap`, `/api/v2/nha-cung-cap` | CRUD nha cung cap. |
| `KhachHangEndpoints.cs` | `/api/khach-hang`, `/api/v2/khach-hang` | Delete can Admin. |
| `NhanVienEndpoints.cs` | `/api/nhan-vien`, `/api/v2/nhan-vien` | Create/update/delete can Admin. |
| `KhoEndpoints.cs` | `/api/ton-kho/thap` | Tra cuu ton kho thap. |
| `PhieuNhapEndpoints.cs` | `/api/phieu-nhap`, `/api/v2/phieu-nhap` | Luu phieu nhap va cong ton trong transaction. |
| `PhieuXuatEndpoints.cs` | `/api/phieu-xuat`, `/api/v2/phieu-xuat` | Luu phieu xuat, kiem tra/tru ton trong transaction. |

Route `/api/...` tra du lieu tu DataTable de WinForms hien tai dung. Route `/api/v2/...` tra DTO typed object cho client moi, Swagger hoac test.

## Services

```txt
Services/
|   ApiAuthorization.cs
|   ApiKeyValidator.cs
|   ApiResults.cs
|   ApiValidationException.cs
|   AuditLogService.cs
|   AuthService.cs
|   DesktopClientLauncher.cs
|   HangHoaService.cs
|   JwtTokenService.cs
|   KhachHangService.cs
|   KhoService.cs
|   LoaiHangService.cs
|   NhaCungCapService.cs
|   NhanVienService.cs
|   PhieuNhapService.cs
|   PhieuXuatService.cs
|   ValidationHelper.cs
```

| File | Chuc nang |
| --- | --- |
| `ApiAuthorization.cs` | Helper phan quyen theo role trong JWT, hien dung cho role `Admin`. |
| `ApiKeyValidator.cs` | Kiem tra `X-API-Key` hoac `Authorization: Bearer <api-key>`. |
| `ApiResults.cs` | Chuan hoa response thanh cong, loi validate, loi database va loi he thong. |
| `ApiValidationException.cs` | Exception rieng cho loi validate gom danh sach loi. |
| `AuditLogService.cs` | Ghi user, role, hanh dong, bang, id, noi dung va IP vao `auditlog`. |
| `AuthService.cs` | Validate login va goi `TaiKhoanRepository`. |
| `DesktopClientLauncher.cs` | Tu mo WinForms neu API duoc chay truc tiep; bo qua khi `QUANLYKHOHANG_DISABLE_DESKTOP_LAUNCH=1` hoac API do desktop khoi dong. |
| `JwtTokenService.cs` | Tao, ky, kiem tra va doc thong tin JWT. |
| `*Service.cs` | Validate va xu ly nghiep vu cho tung module. |
| `ValidationHelper.cs` | Ham validate dung chung. |

## Repositories

```txt
Repositories/
|   HangHoaRepository.cs
|   KhachHangRepository.cs
|   LoaiHangRepository.cs
|   NhaCungCapRepository.cs
|   NhanVienRepository.cs
|   PhieuNhapRepository.cs
|   PhieuXuatRepository.cs
|   TaiKhoanRepository.cs
```

Repository chi lam viec voi database. Validate va phan quyen nam o service/endpoint.

| Repository | Chuc nang |
| --- | --- |
| `TaiKhoanRepository.cs` | Kiem tra dang nhap, hash mat khau, vai tro va trang thai tai khoan. |
| `HangHoaRepository.cs` | CRUD hang hoa, ho tro soft delete. |
| `NhanVienRepository.cs` | CRUD nhan vien, chan xoa neu da co chung tu, ho tro soft delete. |
| `PhieuNhapRepository.cs` | Luu phieu nhap va cong ton kho trong transaction. |
| `PhieuXuatRepository.cs` | Luu phieu xuat, kiem tra ton kho va tru ton trong transaction. |
| Cac repository danh muc khac | CRUD loai hang, nha cung cap, khach hang. |

## Rate Limit

API dang cau hinh cac policy:

| Policy | Gioi han |
| --- | --- |
| `Login` | 5 request / 5 phut / partition. |
| `HangHoaRead` | 100 request / phut / partition. |
| `CreateProduct` | 10 request / phut / partition. |
| `UpdateProduct` | 20 request / phut / partition. |
| `DeleteProduct` | 5 request / phut / partition. |

Partition uu tien theo user trong JWT, sau do API key, Bearer token, cuoi cung la IP.

## Endpoint He Thong

```http
GET /api/health
GET /api/chuc-nang
GET /api/docs
GET /swagger
GET /swagger/v1/swagger.json
POST /api/auth/login
```

Swagger chi bat trong Development.

Dang nhap:

```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "<ten-tai-khoan>",
  "password": "<mat-khau>"
}
```

## Endpoint Danh Muc

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

Endpoint v2:

```http
GET /api/v2/hang-hoa
GET /api/v2/loai-hang
GET /api/v2/nha-cung-cap
GET /api/v2/khach-hang
GET /api/v2/nhan-vien
```

## Endpoint Kho

```http
GET /api/ton-kho/thap?soLuongToiDa=10
```

## Endpoint Nhap Kho

```http
GET  /api/phieu-nhap
GET  /api/v2/phieu-nhap
GET  /api/phieu-nhap/{id}/chi-tiet
GET  /api/v2/phieu-nhap/{id}/chi-tiet
POST /api/phieu-nhap
```

Body tao phieu nhap:

```json
{
  "phieuNhap": {
    "maNhaCungCap": 1,
    "maNhanVien": 1,
    "tongTien": 100000,
    "ghiChu": ""
  },
  "chiTietList": [
    {
      "maHangHoa": 1,
      "soLuong": 10,
      "donGiaNhap": 10000,
      "thanhTien": 100000
    }
  ]
}
```

## Endpoint Xuat Kho

```http
GET  /api/phieu-xuat
GET  /api/v2/phieu-xuat
GET  /api/phieu-xuat/{id}/chi-tiet
GET  /api/v2/phieu-xuat/{id}/chi-tiet
GET  /api/phieu-xuat/{id}/thong-tin
GET  /api/v2/phieu-xuat/{id}/thong-tin
POST /api/phieu-xuat
```

Body tao phieu xuat:

```json
{
  "phieuXuat": {
    "maKhachHang": 1,
    "maNhanVien": 1,
    "tongTien": 150000,
    "ghiChu": ""
  },
  "chiTietList": [
    {
      "maHangHoa": 1,
      "soLuong": 5,
      "donGiaXuat": 30000,
      "thanhTien": 150000
    }
  ]
}
```

## Xu Ly Loi

API dung `ApiResults.Safe(...)` de chuan hoa loi:

| Truong hop | HTTP status |
| --- | --- |
| Validate sai | `400 Bad Request` |
| Loi nghiep vu | `400 Bad Request` |
| Sai/thieu API key hoac JWT | `401 Unauthorized` |
| Khong du role | `403 Forbidden` |
| Update/delete khong thay du lieu | `404 Not Found` |
| Qua rate limit | `429 Too Many Requests` |
| Loi he thong | `500 Internal Server Error` |

Response loi thuong co:

```json
{
  "message": "Du lieu khong hop le.",
  "errors": [
    "tenHangHoa khong duoc de trong."
  ]
}
```

## Chay API

Tu root repository:

```powershell
dotnet run --project QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj
```

Chay API kem PostgreSQL bang Docker tu root repository. Docker compose can bien `QLKH_DB_PASSWORD` trong `.env` hoac environment:

```powershell
Copy-Item .env.example .env
# Sua QLKH_DB_PASSWORD trong .env truoc khi chay docker compose
docker compose up -d --build
```

Trong Docker compose, API lang nghe tren host tai:

```txt
http://localhost:8088
```

PostgreSQL Docker dung image `postgres:17`. Docker publish PostgreSQL ra Windows tai `localhost:5432`, va API container ket noi noi bo bang `postgres:5432`. Neu may dang co PostgreSQL local dung `localhost:5432`, can dung mot nguon database tai mot thoi diem hoac doi port publish trong `docker-compose.yml`.

Bang cong:

| Muc dich | Dia chi |
| --- | --- |
| Client/WinForms goi API | `http://localhost:8088` |
| API Docker trong container | `http://+:8080` |
| DBeaver ket noi PostgreSQL Docker | `localhost:5432` |
| API Docker ket noi PostgreSQL Docker | `postgres:5432` |
| API local ket noi PostgreSQL local | `localhost:5432` |

Kiem tra:

```http
GET http://localhost:8088/api/health
```

Mo Swagger UI trong Development:

```txt
http://localhost:8088/swagger
```

Lay OpenAPI JSON trong Development:

```txt
http://localhost:8088/swagger/v1/swagger.json
```

## Ghi Chu Phat Trien

- Route API nam trong `Endpoints/`.
- Logic nghiep vu nam trong `Services/`.
- SQL va transaction nam trong `Repositories/`.
- Khong dua logic WinForms vao API.
- Khong viet SQL trong Endpoint.
- Luon dung `NpgsqlParameter` khi SQL co input tu nguoi dung.
- Nhap/xuat kho phai giu transaction.
- Khi them endpoint thay doi du lieu, can can nhac audit log va phan quyen.
- Khi doi route, cap nhat WinForms `ApiClients/`.
- Khong ghi mat khau database vao README; cac vi du cau hinh luon de `Password` rong.
