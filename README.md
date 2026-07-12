# Quan Ly Kho Hang

He thong quan ly kho hang gom 4 project chinh:

- `QuanLyKhoHang.WinForms`: ung dung desktop WinForms.
- `QuanLyKhoHang.Api`: backend ASP.NET Core Minimal API.
- `QuanLyKhoHang.Shared`: class library chua model dung chung.
- `QuanLyKhoHang.Tests`: test tu dong cho service, validation va JWT.

WinForms chi phu trach giao dien va goi API. API phu trach xac thuc, phan quyen, validate, xu ly nghiep vu, ghi audit log, thao tac PostgreSQL va tra JSON ve cho WinForms.

## Luong Moi Cua He Thong

Luong tong quat:

```txt
WinForms
->
ApiClients
->
ApiHttpClient
  - X-API-Key neu API bat RequireApiKey
  - Authorization: Bearer <jwt> sau khi dang nhap
->
Minimal API Endpoints
->
JWT middleware + role authorization
->
Services
->
Repositories
->
PostgreSQL
->
AuditLog cho thao tac thay doi du lieu
```

Luong khoi dong desktop:

```txt
Program.cs (WinForms)
->
ApplicationConfiguration.Initialize()
->
ApiServerLauncher.EnsureStarted()
->
GET /api/health
->
Neu API chua chay thi khoi dong QuanLyKhoHang.Api
  - chi ap dung local dev voi ApiBaseUrl localhost:8088 va AutoStartLocalApi = true
->
FrmDangNhap
```

Luong dang nhap:

```txt
FrmDangNhap
->
AuthApiClient.CheckLogin(username, password)
->
POST /api/auth/login
->
AuthService + TaiKhoanRepository
->
API tra ve vaiTro, token, expiresAt
->
ApiHttpClient.SetBearerToken(token)
->
UserSession luu ten tai khoan va vai tro
->
FrmMain phan quyen menu
```

Luong nghiep vu sau khi dang nhap:

```txt
Form nghiep vu
->
*ApiClient
->
ApiHttpClient gui Bearer token
->
Endpoint
->
Service validate va xu ly rule
->
Repository chay SQL/transaction
->
AuditLogService ghi lich su voi user/role/ip
->
Form load lai du lieu
```

## Cau Truc Thu Muc Tong Quan

```txt
QuanLyKhoHang/
|   .gitignore
|   .dockerignore
|   docker-compose.yml
|   QuanLyKhoHang.sln
|   README.md
|
+---.github/
|   |   copilot-instructions.md
|   |
|   \---workflows/
|           build.yml
|
+---QuanLyKhoHang.WinForms/
|   |   Program.cs
|   |   QuanLyKhoHang.WinForms.csproj
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
|   |       FrmHangHoa.cs
|   |       FrmKhachHang.cs
|   |       FrmMain.cs
|   |       FrmNhanVien.cs
|   |       FrmNhapKho.cs
|   |       FrmXuatKho.cs
|   |       UiTheme.cs
|   |
|   +---Reports/
|   |       ExportExcel.cs
|   |       ExportPdf.cs
|   |
|   \---sql/
|           backup_database.ps1
|           create_tables.sql
|           migrate_add_trang_thai.sql
|           migrate_hash_sample_passwords.sql
|           sample_data.sql
|           sync_existing_database.sql
|
+---QuanLyKhoHang.Api/
|   |   appsettings.json
|   |   appsettings.Production.json
|   |   DataTableJson.cs
|   |   Dockerfile
|   |   InventoryApiQueries.cs
|   |   Program.cs
|   |   QuanLyKhoHang.Api.csproj
|   |   README.md
|   |
|   +---Config/
|   |       ApiSettings.cs
|   |       JwtSettings.cs
|   |
|   +---Data/
|   |       DatabaseHelper.cs
|   |       DatabaseMaintenance.cs
|   |       DbConnection.cs
|   |
|   +---DTOs/
|   |       AuthDtos.cs
|   |       DataTableDtoMapper.cs
|   |       PhieuKhoDtos.cs
|   |       ResponseDtos.cs
|   |
|   +---Endpoints/
|   |       AuthEndpoints.cs
|   |       HangHoaEndpoints.cs
|   |       KhachHangEndpoints.cs
|   |       KhoEndpoints.cs
|   |       LoaiHangEndpoints.cs
|   |       NhaCungCapEndpoints.cs
|   |       NhanVienEndpoints.cs
|   |       PhieuNhapEndpoints.cs
|   |       PhieuXuatEndpoints.cs
|   |       SystemEndpoints.cs
|   |
|   +---Repositories/
|   |       HangHoaRepository.cs
|   |       KhachHangRepository.cs
|   |       LoaiHangRepository.cs
|   |       NhaCungCapRepository.cs
|   |       NhanVienRepository.cs
|   |       PhieuNhapRepository.cs
|   |       PhieuXuatRepository.cs
|   |       TaiKhoanRepository.cs
|   |
|   \---Services/
|           ApiAuthorization.cs
|           ApiKeyValidator.cs
|           ApiResults.cs
|           ApiValidationException.cs
|           AuditLogService.cs
|           AuthService.cs
|           DesktopClientLauncher.cs
|           HangHoaService.cs
|           JwtTokenService.cs
|           KhachHangService.cs
|           KhoService.cs
|           LoaiHangService.cs
|           NhaCungCapService.cs
|           NhanVienService.cs
|           PhieuNhapService.cs
|           PhieuXuatService.cs
|           ValidationHelper.cs
|
+---QuanLyKhoHang.Shared/
|   |   QuanLyKhoHang.Shared.csproj
|   |   README.md
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
\---QuanLyKhoHang.Tests/
        JwtTokenServiceTests.cs
        ServiceValidationTests.cs
        QuanLyKhoHang.Tests.csproj
        README.md
```

Thu muc build nhu `bin/`, `obj/`, `.vs/` la file sinh ra khi build va khong dua vao cay tren.

## Vai Tro Tung Project

| Project | Vai tro |
| --- | --- |
| `QuanLyKhoHang.WinForms` | Giao dien desktop, dieu huong, form nghiep vu, goi API, xuat Excel/PDF. |
| `QuanLyKhoHang.Api` | Backend Minimal API, xac thuc JWT, API key, phan quyen, rate limit, validate, transaction, audit log. |
| `QuanLyKhoHang.Shared` | Model dung chung cho WinForms va API. |
| `QuanLyKhoHang.Tests` | Unit test cho validation service va JWT. |

## Mo Hinh Docker

Docker chi chay backend va database:

```txt
May Windows nguoi dung
QuanLyKhoHang.WinForms.exe
  -> HTTP/JSON
Docker server
  -> QuanLyKhoHang.Api container
  -> PostgreSQL container
```

WinForms la ung dung desktop Windows nen khong dua vao Docker. Khi dung API Docker local, cau hinh client:

```json
{
  "ApiBaseUrl": "http://localhost:8088",
  "ApiKey": "",
  "AutoStartLocalApi": false
}
```

`QuanLyKhoHang.Shared` duoc build kem theo project nao reference no. `QuanLyKhoHang.Tests` chi dung de kiem thu.

Port khi chay Docker local:

```txt
WinForms -> http://localhost:8088 -> API Docker
API Docker -> postgres:5432 -> PostgreSQL container
DBeaver/Windows -> localhost:5433 -> PostgreSQL container
PostgreSQL local cu tren Windows -> localhost:5432
```

PostgreSQL Docker dung image `postgres:17` de cung major version voi PostgreSQL local. Trong `docker-compose.yml`, giu `QLKH_DB_HOST=postgres` va `QLKH_DB_PORT=5432` cho API Docker. Khong doi thanh `5433`, vi `5433` chi la port Windows dung de DBeaver ket noi vao container.

Bang cong chuan:

| Thanh phan | Dia chi |
| --- | --- |
| WinForms goi API | `http://localhost:8088` |
| API Visual Studio/local | `http://localhost:8088` |
| API Docker publish ra Windows | `localhost:8088` -> container `8080` |
| PostgreSQL local Windows | `localhost:5432` |
| PostgreSQL Docker cho DBeaver/Windows | `localhost:5433` -> container `5432` |
| API Docker goi PostgreSQL Docker | `postgres:5432` |

## Cau Hinh Database

File cau hinh database cua API:

```txt
QuanLyKhoHang.Api/appsettings.json
```

Vi du cau hinh. Mat khau database trong README luon de trong de khong lo thong tin nhay cam:

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

`DatabaseSettings` tren la cau hinh khi chay API local/Visual Studio voi PostgreSQL local `localhost:5432`. Khi chay Docker, cac bien moi truong trong `docker-compose.yml` se ghi de thanh `QLKH_DB_HOST=postgres` va `QLKH_DB_PORT=5432`.

Dat mat khau database bang file local hoac bien moi truong tren may chay that. Khong ghi mat khau database vao README, issue, commit message hoac tai lieu chia se.

Bien moi truong co the ghi de cau hinh database:

```txt
QLKH_DB_HOST
QLKH_DB_PORT
QLKH_DB_NAME
QLKH_DB_USER
QLKH_DB_PASSWORD
```

Bien moi truong ASP.NET Core co the ghi de API/JWT:

```txt
ApiSettings__ApiKey
ApiSettings__RequireApiKey
ApiSettings__AllowedOrigins__0
JwtSettings__RequireJwt
JwtSettings__SecretKey
JwtSettings__ExpirationMinutes
```

## Cai Dat Database

Database mac dinh:

```txt
Database: quanlyhanghoa
Username: postgres
Password:
Port local Windows: 5432
Port Docker tren Windows: 5433
Port noi bo Docker: 5432
```

Script SQL nam trong:

```txt
QuanLyKhoHang.WinForms/sql/
```

Thu tu chay voi database moi:

```txt
1. create_tables.sql
2. sample_data.sql
```

Voi database cu, chay script dong bo schema:

```txt
sync_existing_database.sql
```

Neu chi can migrate rieng:

```txt
migrate_add_trang_thai.sql
migrate_hash_sample_passwords.sql
```

Sao luu database:

```powershell
.\QuanLyKhoHang.WinForms\sql\backup_database.ps1
```

Script backup doc `QLKH_DB_PASSWORD` tu bien moi truong; khong can ghi mat khau vao README.

## Restore Database That Vao Docker

Docker compose khong tu nap `create_tables.sql` va `sample_data.sql` vao PostgreSQL container. Khi can dung database that, backup database local `quanlyhanghoa` tu DBeaver thanh:

```txt
D:\QuanLyKhoHang\quanlyhanghoa.backup
```

Sau khi da co backup, tao lai PostgreSQL Docker trong bang:

```powershell
docker compose down -v
docker compose up -d
```

Lenh `down -v` se xoa volume PostgreSQL Docker hien tai. Chi chay sau khi da co backup database that.

Them connection DBeaver moi cho Docker:

```txt
Host: localhost
Port: 5433
Database: quanlyhanghoa
Username: postgres
Password: 1234
```

Restore file `D:\QuanLyKhoHang\quanlyhanghoa.backup` vao database Docker, roi kiem tra:

```sql
SELECT * FROM chitietphieunhap;
```

## Bao Mat Va Phan Quyen

- `/api/auth/login` tra ve `token` va `expiresAt`.
- WinForms luu token vao `ApiHttpClient` va gui `Authorization: Bearer <jwt>` cho cac request tiep theo.
- Khi `JwtSettings.RequireJwt = true`, cac endpoint nghiep vu yeu cau JWT hop le.
- Cac route public: `/`, `/api/health`, `/api/chuc-nang`, `/api/docs`, `/api/auth/login`, `/swagger`.
- Mot so thao tac nhay cam yeu cau role `Admin`, vi du xoa hang hoa, xoa khach hang, them/sua/xoa nhan vien.
- API co the bat them `ApiSettings.RequireApiKey`; client gui `X-API-Key`.
- API ghi audit log cho thao tac them, sua, xoa va lap phieu.
- API co rate limit cho login va mot so endpoint hang hoa.
- Moi truong production bat kiem tra cau hinh: phai bat JWT, dung HTTPS, gioi han CORS, khong dung secret/mac dinh yeu.

## Chuc Nang Chinh

- Dang nhap, JWT va phan quyen theo vai tro.
- Quan ly hang hoa, loai hang, nha cung cap, khach hang, nhan vien.
- Lap phieu nhap kho, cong ton kho bang transaction.
- Lap phieu xuat kho, kiem tra ton kho va tru ton bang transaction.
- Chan xuat am bang transaction va rang buoc database.
- Xem hang ton kho thap.
- Xem lich su va chi tiet phieu nhap/phieu xuat.
- Xuat Excel/PDF.
- Ghi audit log cac thao tac thay doi du lieu.

## Build Va Chay

Tai root repository:

```powershell
dotnet restore QuanLyKhoHang.sln
dotnet build QuanLyKhoHang.sln
```

Chay WinForms:

```powershell
dotnet run --project QuanLyKhoHang.WinForms/QuanLyKhoHang.WinForms.csproj
```

Chay API rieng:

```powershell
dotnet run --project QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj
```

Chay API va PostgreSQL bang Docker:

```powershell
docker compose up -d --build
```

Khi chay Docker, API lang nghe tai:

```txt
http://localhost:8088
```

PostgreSQL Docker dung cho DBeaver tren Windows:

```txt
localhost:5433
```

PostgreSQL Docker trong container van lang nghe cong noi bo `5432`; `5433` chi la cong Windows publish ra ngoai.

Mac dinh API lang nghe tai:

```txt
http://localhost:8088
```

Kiem tra API:

```http
GET http://localhost:8088/api/health
```

Swagger chi bat trong moi truong development:

```txt
http://localhost:8088/swagger
```

Chay test:

```powershell
dotnet test QuanLyKhoHang.sln
```

Publish WinForms thanh file `.exe` cho Windows:

```powershell
dotnet publish QuanLyKhoHang.WinForms/QuanLyKhoHang.WinForms.csproj `
  -c Release `
  -r win-x64 `
  --self-contained true
```

## API Chinh

```http
GET  /
GET  /api/health
GET  /api/chuc-nang
GET  /api/docs
POST /api/auth/login

GET  /api/v2/hang-hoa
GET  /api/v2/loai-hang
GET  /api/v2/nha-cung-cap
GET  /api/v2/khach-hang
GET  /api/v2/nhan-vien
GET  /api/v2/phieu-nhap
GET  /api/v2/phieu-nhap/{id}/chi-tiet
GET  /api/v2/phieu-xuat
GET  /api/v2/phieu-xuat/{id}/chi-tiet
GET  /api/v2/phieu-xuat/{id}/thong-tin

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

Route `/api/...` tra JSON tu DataTable de WinForms hien tai tuong thich. Route `/api/v2/...` tra DTO typed object, phu hop cho client moi, Swagger va test.

## Ghi Chu Phat Trien

- Khong commit `bin/`, `obj/`, `.vs/`.
- Khong dua SQL truc tiep vao Form.
- Khong de WinForms truy cap PostgreSQL truc tiep.
- Khi doi route API, cap nhat file trong `QuanLyKhoHang.WinForms/ApiClients/`.
- Khi doi alias cot SQL tra ve, kiem tra DataGridView va ComboBox trong cac Form.
- Nghiep vu nhap/xuat kho phai giu transaction de tranh lech ton kho.
- Khong ghi mat khau database vao README; cac vi du cau hinh luon de `Password` rong.
