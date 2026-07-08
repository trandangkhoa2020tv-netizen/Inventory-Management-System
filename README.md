# Quan Ly Kho Hang

He thong quan ly kho hang gom 4 project chinh:

- `QuanLyKhoHang`: ung dung desktop WinForms.
- `QuanLyKhoHang.Api`: backend ASP.NET Core API lam viec voi PostgreSQL.
- `QuanLyKhoHang.Shared`: class library chua model dung chung.
- `QuanLyKhoHang.Tests`: test tu dong cho service va JWT.

Kien truc hien tai:

```txt
WinForms
->
ApiClient
->
Endpoints
->
Services
->
Repositories
->
PostgreSQL
```

WinForms chi phu trach giao dien, nhap lieu, hien thi du lieu va goi API. API phu trach validate, xu ly nghiep vu, thao tac database va tra ket qua JSON ve cho WinForms.

## Cau Truc Thu Muc Tong Quan

```txt
QuanLyKhoHang/
|   .gitignore
|   QuanLyKhoHang.sln
|   README.md
|
+---.github/
|   |   copilot-instructions.md
|   |
|   \---workflows/
|           build.yml
|
+---QuanLyKhoHang/                  -> App giao dien WinForms
|   |   .dockerignore
|   |   .gitignore
|   |   docker-compose.yml
|   |   Dockerfile.build
|   |   Program.cs
|   |   QuanLyKhoHang.csproj
|   |   README.md
|   |
|   +---ApiClients/
|   |       ApiClientSettings.cs
|   |       ApiHttpClient.cs
|   |       ApiServerLauncher.cs
|   |       DanhMucApiClients.cs
|   |       KhoApiClients.cs
|   |
|   +---Config/
|   |       appsettings.example.json
|   |       appsettings.json
|   |
|   +---Forms/
|   |       FrmDangNhap.cs
|   |       FrmDangNhap.Designer.cs
|   |       FrmDangNhap.resx
|   |       FrmHangHoa.cs
|   |       FrmHangHoa.Designer.cs
|   |       FrmHangHoa.resx
|   |       FrmKhachHang.cs
|   |       FrmKhachHang.Designer.cs
|   |       FrmKhachHang.resx
|   |       FrmMain.cs
|   |       FrmMain.Designer.cs
|   |       FrmMain.resx
|   |       FrmNhanVien.cs
|   |       FrmNhanVien.Designer.cs
|   |       FrmNhanVien.resx
|   |       FrmNhapKho.cs
|   |       FrmNhapKho.Designer.cs
|   |       FrmNhapKho.resx
|   |       FrmXuatKho.cs
|   |       FrmXuatKho.Designer.cs
|   |       FrmXuatKho.resx
|   |       UiTheme.cs
|   |
|   +---Reports/
|   |       ExportExcel.cs
|   |       ExportPdf.cs
|   |
|   \---sql/
|           create_tables.sql
|           migrate_add_trang_thai.sql
|           migrate_hash_sample_passwords.sql
|           sample_data.sql
|           sync_existing_database.sql
|
\---QuanLyKhoHang.Api/              -> Backend API
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
    |       LoaiHangEndpoints.cs
    |       NhaCungCapEndpoints.cs
    |       KhachHangEndpoints.cs
    |       NhanVienEndpoints.cs
    |       KhoEndpoints.cs
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
            HangHoaService.cs
            LoaiHangService.cs
            NhaCungCapService.cs
            KhachHangService.cs
            NhanVienService.cs
            DesktopClientLauncher.cs
            KhoService.cs
            PhieuNhapService.cs
            PhieuXuatService.cs
            JwtTokenService.cs
            ValidationHelper.cs
|
+---QuanLyKhoHang.Shared/           -> Model dung chung
|   |   QuanLyKhoHang.Shared.csproj
|   |
|   \---Models/
|           ChiTietPhieuNhap.cs
|           ChiTietPhieuXuat.cs
|           HangHoa.cs
|           KhachHang.cs
|           LoaiHang.cs
|           NhaCungCap.cs
|           NhanVien.cs
|           PhieuNhap.cs
|           PhieuXuat.cs
|           TaiKhoan.cs
|           UserSession.cs
|
\---QuanLyKhoHang.Tests/            -> Test tu dong
        JwtTokenServiceTests.cs
        ServiceValidationTests.cs
        QuanLyKhoHang.Tests.csproj
```

Thu muc build nhu `bin/`, `obj/`, `.vs/` khong duoc dua vao cay tren vi do la file sinh ra khi build.

## Y Nghia Tung Phan

| Thanh phan | Vai tro |
| --- | --- |
| `.github/workflows/build.yml` | Cau hinh GitHub Actions de build/test tu dong. |
| `QuanLyKhoHang.sln` | Solution gom WinForms, API, Shared va Tests. |
| `QuanLyKhoHang/` | Project giao dien desktop WinForms. |
| `QuanLyKhoHang.Api/` | Project backend API ket noi PostgreSQL. |
| `QuanLyKhoHang.Shared/` | Project chua model dung chung cho WinForms va API. |
| `QuanLyKhoHang.Tests/` | Project test tu dong bang xUnit. |
| `QuanLyKhoHang/ApiClients/` | Noi WinForms goi HTTP API. |
| `QuanLyKhoHang/Forms/` | Cac man hinh giao dien nguoi dung. |
| `QuanLyKhoHang/Reports/` | Xuat bao cao Excel/PDF. |
| `QuanLyKhoHang/sql/` | Script tao database va du lieu mau. |
| `QuanLyKhoHang.Api/Endpoints/` | Khai bao route API theo Minimal API, thay vai tro controller. |
| `QuanLyKhoHang.Api/Services/` | Xu ly logic nghiep vu va validate. |
| `QuanLyKhoHang.Api/Repositories/` | Truy van database PostgreSQL. |
| `QuanLyKhoHang.Api/Data/` | Ket noi database va helper chay SQL. |
| `QuanLyKhoHang.Api/DTOs/` | Kieu du lieu request/response rieng cho API, gom DTO output cho `/api/v2`. |
| `QuanLyKhoHang.Api/Config/` | Class cau hinh API. |

## Luong Xu Ly Chuan

Vi du them hoac sua hang hoa:

```txt
FrmHangHoa.cs
->
HangHoaApiClient
->
POST/PUT /api/hang-hoa
->
HangHoaEndpoints.cs
->
HangHoaService.cs
->
HangHoaRepository.cs
->
PostgreSQL
```

Vi du lap phieu nhap:

```txt
FrmNhapKho.cs
->
PhieuNhapApiClient
->
POST /api/phieu-nhap
->
PhieuNhapEndpoints.cs
->
PhieuNhapService.cs
->
PhieuNhapRepository.cs
->
Transaction:
  1. Tao phieu nhap
  2. Them chi tiet phieu
  3. Cong ton kho
```

Vi du lap phieu xuat:

```txt
FrmXuatKho.cs
->
PhieuXuatApiClient
->
POST /api/phieu-xuat
->
PhieuXuatEndpoints.cs
->
PhieuXuatService.cs
->
PhieuXuatRepository.cs
->
Transaction:
  1. Tao phieu xuat
  2. Kiem tra ton kho
  3. Tru ton kho
  4. Them chi tiet phieu
```

## Cong Nghe Su Dung

| Thanh phan | Cong nghe |
| --- | --- |
| Giao dien | C# WinForms |
| Desktop target | `net10.0-windows` |
| Backend | ASP.NET Core Minimal API |
| API target | `net10.0` |
| API docs/test | Swagger UI / Swashbuckle |
| Database | PostgreSQL |
| Driver database | Npgsql |
| Excel | ClosedXML |
| PDF | iTextSharp.LGPLv2.Core |
| CI/CD | GitHub Actions |

## Chuc Nang Chinh

- Dang nhap va phan quyen theo vai tro.
- Quan ly hang hoa.
- Quan ly loai hang.
- Quan ly nha cung cap.
- Quan ly khach hang.
- Quan ly nhan vien.
- Lap phieu nhap kho va cong ton kho.
- Lap phieu xuat kho va tru ton kho.
- Chan xuat am bang transaction o database.
- Xem hang ton kho thap.
- Xem lich su phieu nhap/phieu xuat.
- Xem chi tiet tung phieu.
- Xuat Excel/PDF.

## Cau Hinh Database

File cau hinh database nam o:

```txt
QuanLyKhoHang.Api/appsettings.json
```

Vi du:

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

Bien moi truong co the ghi de cau hinh database:

```txt
QLKH_DB_HOST
QLKH_DB_PORT
QLKH_DB_NAME
QLKH_DB_USER
QLKH_DB_PASSWORD
```

## Cau Hinh WinForms Goi API

File:

```txt
QuanLyKhoHang/Config/appsettings.json
```

Vi du:

```json
{
  "ApiClientSettings": {
    "BaseUrl": "http://localhost:5088",
    "ApiKey": ""
  }
}
```

## Cai Dat Database

Database mac dinh:

```txt
Database: quanlyhanghoa
Username: postgres
Password: 1234
Port: 5432
```

Script SQL nam trong:

```txt
QuanLyKhoHang/sql/
```

Thu tu chay voi database moi:

```txt
1. create_tables.sql
2. sample_data.sql
```

Voi database cu, co the chay them:

```txt
migrate_add_trang_thai.sql
migrate_hash_sample_passwords.sql
sync_existing_database.sql
```

## Build Va Chay

Tai root repository:

```powershell
dotnet restore QuanLyKhoHang.sln
dotnet build QuanLyKhoHang.sln
```

Chay WinForms:

```powershell
dotnet run --project QuanLyKhoHang/QuanLyKhoHang.csproj
```

Chay API rieng:

```powershell
dotnet run --project QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj
```

Mac dinh API lang nghe tai:

```txt
http://localhost:5088
```

Kiem tra API:

```http
GET http://localhost:5088/api/health
```

Mo Swagger UI de test API truc quan:

```txt
http://localhost:5088/swagger
```

## API Chinh

```http
GET  /api/health
GET  /api/chuc-nang
GET  /api/docs
GET  /swagger
POST /api/auth/login
GET  /api/v2/hang-hoa
GET  /api/v2/loai-hang
GET  /api/v2/nha-cung-cap
GET  /api/v2/khach-hang
GET  /api/v2/nhan-vien
GET  /api/v2/phieu-nhap
GET  /api/v2/phieu-xuat

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

GET  /api/ton-kho/thap?soLuongToiDa=10

GET  /api/phieu-nhap
POST /api/phieu-nhap
GET  /api/phieu-nhap/{id}/chi-tiet

GET  /api/phieu-xuat
POST /api/phieu-xuat
GET  /api/phieu-xuat/{id}/chi-tiet
GET  /api/phieu-xuat/{id}/thong-tin
```

## Ghi Chu Ve Controller

Project hien tai chua dung `Controllers/`. Backend dang dung Minimal API nen route duoc tach vao:

```txt
QuanLyKhoHang.Api/Endpoints/
```

Trong cau truc hien tai:

```txt
Controller tuong duong Endpoints
Service    xu ly nghiep vu
Repository thao tac database
```

Neu muon chuyen sang MVC Controller sau nay, co the tao them `Controllers/` va chuyen route tu `Endpoints/` sang controller, nhung hien tai khong can thiet de app chay.

## Ghi Chu Phat Trien

- Khong commit `bin/`, `obj/`, `.vs/`.
- Khong dua SQL truc tiep vao Form.
- Khong de WinForms truy cap PostgreSQL truc tiep.
- Khi doi route API, phai cap nhat file trong `QuanLyKhoHang/ApiClients/`.
- Khi doi alias cot SQL tra ve, kiem tra lai DataGridView va ComboBox trong cac Form.
- Nghiep vu nhap/xuat kho phai giu transaction de tranh lech ton kho.
