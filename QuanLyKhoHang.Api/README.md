# QuanLyKhoHang.Api - Backend API

`QuanLyKhoHang.Api` la backend API cua he thong quan ly kho hang.

Project nay phu trach:

- Nhan request HTTP tu WinForms.
- Validate du lieu dau vao.
- Xu ly logic nghiep vu.
- Goi repository de thao tac PostgreSQL.
- Tra JSON ve cho WinForms.

Backend hien tai dung ASP.NET Core Minimal API. Vi vay project khong co `Controllers/`; cac route duoc tach vao thu muc `Endpoints/`.

## Vai Tro Trong Solution

```txt
QuanLyKhoHang.WinForms
->
HTTP API
->
QuanLyKhoHang.Api
->
Services
->
Repositories
->
PostgreSQL
```

## Cau Truc Thu Muc

```txt
QuanLyKhoHang.Api/
|   .gitignore
|   appsettings.json
|   DataTableJson.cs
|   InventoryApiQueries.cs
|   Program.cs
|   QuanLyKhoHang.Api.csproj
|   README.md
|
+---Config/
|       ApiSettings.cs
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
        ApiKeyValidator.cs
        ApiResults.cs
        ApiValidationException.cs
        AuthService.cs
        DesktopClientLauncher.cs
        HangHoaService.cs
        KhachHangService.cs
        KhoService.cs
        LoaiHangService.cs
        NhaCungCapService.cs
        NhanVienService.cs
        PhieuNhapService.cs
        PhieuXuatService.cs
        ValidationHelper.cs
```

## Y Nghia Tung Phan

| Thu muc/file | Vai tro |
| --- | --- |
| `Program.cs` | Khoi tao API, cau hinh CORS, API key, DI, map endpoint. |
| `appsettings.json` | Cau hinh database va API. |
| `QuanLyKhoHang.Api.csproj` | Cau hinh project API, package Npgsql/Swagger va link model tu WinForms. |
| `DataTableJson.cs` | Chuyen `DataTable` thanh list object de serialize JSON. |
| `InventoryApiQueries.cs` | Truy van ton kho/canh bao ton kho thap. |
| `Config/` | Class mapping cau hinh API. |
| `Data/` | Ket noi database va helper chay SQL. |
| `DTOs/` | Request/response DTO cho endpoint API, gom DTO output cho `/api/v2`. |
| `Endpoints/` | Noi khai bao route API Minimal API. |
| `Repositories/` | Tang thao tac database. |
| `Services/` | Tang xu ly nghiep vu va validate. |
| `Properties/launchSettings.json` | Cau hinh chay API trong Visual Studio. |

## Program.cs

`Program.cs` hien tai chi giu phan bootstrap:

```txt
Program.cs
|-- Doc ApiSettings
|-- Cau hinh URL lang nghe
|-- Cau hinh JSON
|-- Cau hinh Swagger/OpenAPI
|-- Cau hinh CORS
|-- Dang ky Repository vao DI
|-- Dang ky Service vao DI
|-- Dong bo sequence database khi khoi dong
|-- Bat middleware API key neu RequireApiKey = true
|-- Map cac nhom endpoint
|-- Dang ky DesktopClientLauncher
|-- app.Run()
```

Nhung phan route, validate va logic da duoc tach sang `Endpoints/` va `Services/`.

## Config

```txt
Config/
|   ApiSettings.cs
```

`ApiSettings.cs` mapping phan cau hinh:

```json
{
  "ApiSettings": {
    "Url": "http://localhost:5088",
    "RequireApiKey": false,
    "ApiKey": "",
    "AllowedOrigins": []
  },
  "JwtSettings": {
    "RequireJwt": false,
    "Issuer": "QuanLyKhoHang.Api",
    "Audience": "QuanLyKhoHang.WinForms",
    "SecretKey": "QuanLyKhoHang-Development-Secret-Key-Change-Me",
    "ExpirationMinutes": 480
  }
}
```

| Property | Y nghia |
| --- | --- |
| `Url` | URL API lang nghe. |
| `RequireApiKey` | Bat/tat yeu cau API key. |
| `ApiKey` | API key hop le. |
| `AllowedOrigins` | Danh sach origin duoc CORS cho phep. Rong hoac `*` la cho phep tat ca. |

## Data

```txt
Data/
|   DatabaseHelper.cs
|   DatabaseMaintenance.cs
|   DbConnection.cs
```

| File | Chuc nang |
| --- | --- |
| `DbConnection.cs` | Doc cau hinh database, tao `NpgsqlConnection`, test ket noi. |
| `DatabaseHelper.cs` | Helper chay `ExecuteQuery`, `ExecuteNonQuery`, `ExecuteScalar`. |
| `DatabaseMaintenance.cs` | Dong bo sequence tu tang khi API khoi dong de tranh trung khoa sau khi import data. |

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
| `AuthDtos.cs` | Chua `LoginRequest`. |
| `PhieuKhoDtos.cs` | Chua `LuuPhieuNhapRequest`, `LuuPhieuXuatRequest`. |
| `ResponseDtos.cs` | Chua output DTO: `HangHoaDto`, `KhachHangDto`, `NhanVienDto`, `PhieuNhapDto`, `PhieuXuatDto`... |
| `DataTableDtoMapper.cs` | Chuyen DataTable cu sang DTO typed object cho endpoint `/api/v2`. |

DTO dung de bieu dien body request gui len API, tach khoi model database khi request can dong goi nhieu du lieu.

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

| File | Route chinh |
| --- | --- |
| `SystemEndpoints.cs` | `/`, `/api/health`, `/api/chuc-nang`, `/api/docs`. |
| `AuthEndpoints.cs` | `/api/auth/login`. |
| `HangHoaEndpoints.cs` | `/api/hang-hoa`. |
| `LoaiHangEndpoints.cs` | `/api/loai-hang`. |
| `NhaCungCapEndpoints.cs` | `/api/nha-cung-cap`. |
| `KhachHangEndpoints.cs` | `/api/khach-hang`. |
| `NhanVienEndpoints.cs` | `/api/nhan-vien`. |
| `KhoEndpoints.cs` | `/api/ton-kho/thap`. |
| `PhieuNhapEndpoints.cs` | `/api/phieu-nhap`, `/api/phieu-nhap/{id}/chi-tiet`. |
| `PhieuXuatEndpoints.cs` | `/api/phieu-xuat`, `/api/phieu-xuat/{id}/chi-tiet`, `/api/phieu-xuat/{id}/thong-tin`. |

Trong kien truc hien tai:

```txt
Endpoints = noi nhan HTTP request
Services  = noi xu ly nghiep vu
Repos     = noi truy cap database
```

## Services

```txt
Services/
|   ApiKeyValidator.cs
|   ApiResults.cs
|   ApiValidationException.cs
|   AuthService.cs
|   DesktopClientLauncher.cs
|   HangHoaService.cs
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
| `AuthService.cs` | Kiem tra request dang nhap va goi `TaiKhoanRepository`. |
| `HangHoaService.cs` | Validate va xu ly CRUD hang hoa. |
| `LoaiHangService.cs` | Validate va xu ly CRUD loai hang. |
| `NhaCungCapService.cs` | Validate va xu ly CRUD nha cung cap. |
| `KhachHangService.cs` | Validate va xu ly CRUD khach hang. |
| `NhanVienService.cs` | Validate va xu ly CRUD nhan vien. |
| `KhoService.cs` | Lay canh bao ton kho thap. |
| `PhieuNhapService.cs` | Validate va luu phieu nhap. |
| `PhieuXuatService.cs` | Validate va luu phieu xuat. |
| `ApiResults.cs` | Chuan hoa response thanh cong, loi validate, loi he thong. |
| `ApiValidationException.cs` | Exception rieng cho loi validate. |
| `ApiKeyValidator.cs` | Kiem tra `X-API-Key` hoac `Authorization: Bearer`. |
| `DesktopClientLauncher.cs` | Tu mo WinForms neu API duoc chay truc tiep. |
| `ValidationHelper.cs` | Ham validate dung chung: bat buoc nhap, email, so duong, khong am. |

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

| Repository | Chuc nang |
| --- | --- |
| `TaiKhoanRepository.cs` | Kiem tra dang nhap, mat khau, vai tro, trang thai tai khoan. |
| `HangHoaRepository.cs` | CRUD bang hang hoa. |
| `LoaiHangRepository.cs` | CRUD bang loai hang. |
| `NhaCungCapRepository.cs` | CRUD bang nha cung cap. |
| `KhachHangRepository.cs` | CRUD bang khach hang. |
| `NhanVienRepository.cs` | CRUD nhan vien, chan xoa neu da co phieu nhap/xuat. |
| `PhieuNhapRepository.cs` | Luu phieu nhap va cong ton kho trong transaction. |
| `PhieuXuatRepository.cs` | Luu phieu xuat, kiem tra ton kho va tru ton trong transaction. |

Repository chi nen lam viec voi database. Validate nghiep vu nen nam o service.

## Model Dung Chung

Model da tach sang project rieng:

```txt
QuanLyKhoHang.Shared/
\---Models/
    HangHoa.cs
    KhachHang.cs
    LoaiHang.cs
    NhaCungCap.cs
    NhanVien.cs
    PhieuNhap.cs
    PhieuXuat.cs
    ...
```

API reference shared project bang:

```xml
<ProjectReference Include="..\QuanLyKhoHang.Shared\QuanLyKhoHang.Shared.csproj" />
```

Vi vay API va WinForms cung dung cac class:

```txt
HangHoa
LoaiHang
NhaCungCap
KhachHang
NhanVien
TaiKhoan
PhieuNhap
ChiTietPhieuNhap
PhieuXuat
ChiTietPhieuXuat
UserSession
```

## Cau Hinh appsettings.json

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

| Key | Y nghia |
| --- | --- |
| `Host` | May chu PostgreSQL. |
| `Port` | Port PostgreSQL. |
| `Database` | Ten database. |
| `Username` | User database. |
| `Password` | Mat khau database. |

### ApiSettings

| Key | Y nghia |
| --- | --- |
| `Url` | Dia chi API se lang nghe. |
| `RequireApiKey` | Neu `true`, client phai gui API key. |
| `ApiKey` | Key hop le. |
| `AllowedOrigins` | CORS origins duoc phep. |

Bien moi truong co the ghi de database:

```txt
QLKH_DB_HOST
QLKH_DB_PORT
QLKH_DB_NAME
QLKH_DB_USER
QLKH_DB_PASSWORD
```

## Endpoint He Thong

```http
GET /api/health
GET /api/chuc-nang
GET /api/docs
GET /swagger
GET /swagger/v1/swagger.json
POST /api/auth/login
```

Dang nhap:

```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "123456"
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

Endpoint v2 tra DTO typed object, phu hop client moi hoac test Swagger:

```http
GET /api/v2/hang-hoa
GET /api/v2/loai-hang
GET /api/v2/nha-cung-cap
GET /api/v2/khach-hang
GET /api/v2/nhan-vien
GET /api/v2/phieu-nhap
GET /api/v2/phieu-nhap/{id}/chi-tiet
GET /api/v2/phieu-xuat
GET /api/v2/phieu-xuat/{id}/chi-tiet
GET /api/v2/phieu-xuat/{id}/thong-tin
```

Route cu `/api/...` van duoc giu de WinForms hien tai tuong thich voi DataTable.

## Endpoint Kho

```http
GET /api/ton-kho/thap?soLuongToiDa=10
```

## Endpoint Nhap Kho

```http
GET  /api/phieu-nhap
POST /api/phieu-nhap
GET  /api/phieu-nhap/{id}/chi-tiet
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
POST /api/phieu-xuat
GET  /api/phieu-xuat/{id}/chi-tiet
GET  /api/phieu-xuat/{id}/thong-tin
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

## API Key

Neu bat:

```json
"RequireApiKey": true
```

Client can gui mot trong hai header:

```http
X-API-Key: <api-key>
Authorization: Bearer <api-key>
```

## JWT

Login thanh cong se tra them token:

```json
{
  "tenTaiKhoan": "admin",
  "vaiTro": "Admin",
  "token": "<jwt>",
  "expiresAt": "2026-07-08T12:00:00Z"
}
```

Mac dinh `JwtSettings.RequireJwt = false` de khong pha WinForms khi chay noi bo. Neu muon bat bao mat Bearer token cho cac endpoint nghiep vu:

```json
"JwtSettings": {
  "RequireJwt": true
}
```

Client can gui:

```http
Authorization: Bearer <jwt>
```

Nhung route public khong can JWT:

```txt
/
/api/health
/api/chuc-nang
/api/docs
/api/auth/login
/swagger
```

## Xu Ly Loi

API dung `ApiResults.Safe(...)` de chuan hoa loi:

| Truong hop | HTTP status |
| --- | --- |
| Validate sai | `400 Bad Request` |
| Loi nghiep vu | `400 Bad Request` |
| Update/delete khong thay du lieu | `404 Not Found` |
| Loi he thong | `500 Internal Server Error` |
| Sai/thieu API key | `401 Unauthorized` |

Response loi thuong co:

```json
{
  "message": "Du lieu khong hop le.",
  "errors": [
    "tenHangHoa khong duoc de trong."
  ]
}
```

## Nghiep Vu Quan Trong

### Nhap kho

```txt
PhieuNhapEndpoints.cs
->
PhieuNhapService.cs
->
PhieuNhapRepository.LuuPhieuNhap(...)
->
Transaction:
  1. Tao phieu nhap
  2. Them chi tiet phieu nhap
  3. Cong so_luong_ton
  4. Commit neu thanh cong
  5. Rollback neu co loi
```

### Xuat kho

```txt
PhieuXuatEndpoints.cs
->
PhieuXuatService.cs
->
PhieuXuatRepository.LuuPhieuXuat(...)
->
Transaction:
  1. Tao phieu xuat
  2. Tru ton kho voi dieu kien so_luong_ton >= so_luong
  3. Them chi tiet phieu xuat
  4. Commit neu thanh cong
  5. Rollback neu khong du ton hoac co loi
```

### Xoa nhan vien

```txt
NhanVienRepository.Xoa(id)
->
Kiem tra phieunhap/phieuxuat
->
Neu da co chung tu: khong cho xoa
->
Neu chua co chung tu:
  1. Xoa tai khoan lien quan
  2. Xoa nhan vien
  3. Commit transaction
```

## Chay API

Tu root repository:

```powershell
dotnet run --project QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj
```

Kiem tra:

```http
GET http://localhost:5088/api/health
```

Mo Swagger UI de xem va test endpoint:

```txt
http://localhost:5088/swagger
```

Lay file OpenAPI JSON:

```txt
http://localhost:5088/swagger/v1/swagger.json
```

## Ghi Chu Ve Controllers

Hien tai project khong co thu muc `Controllers/`.

Ly do: project dang dung Minimal API. Thu muc `Endpoints/` dang dam nhan vai tro nhan request giong controller:

```txt
Controllers/  -> cach MVC Controller
Endpoints/    -> cach Minimal API hien tai
```

Neu sau nay muon doi sang controller, co the tao:

```txt
Controllers/
|   AuthController.cs
|   HangHoaController.cs
|   LoaiHangController.cs
|   NhaCungCapController.cs
|   KhachHangController.cs
|   NhanVienController.cs
|   PhieuNhapController.cs
|   PhieuXuatController.cs
|   KhoController.cs
```

Khi do can them:

```csharp
builder.Services.AddControllers();
app.MapControllers();
```

Nhung voi cau truc hien tai, `Endpoints/` la du sach va phu hop voi Minimal API.

## Ghi Chu Phat Trien

- Route API nam trong `Endpoints/`.
- Logic nghiep vu nam trong `Services/`.
- SQL va transaction nam trong `Repositories/`.
- Khong dua logic WinForms vao API.
- Khong viet SQL trong Endpoint.
- Luon dung `NpgsqlParameter` khi SQL co input tu nguoi dung.
- Nhap/xuat kho phai giu transaction.
- Neu doi route, cap nhat WinForms `ApiClients/`.
