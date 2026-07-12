# QuanLyKhoHang.WinForms - Desktop App

`QuanLyKhoHang.WinForms` la ung dung giao dien desktop cua he thong quan ly kho hang.

Project nay phu trach:

- Hien thi man hinh nguoi dung.
- Kiem tra API da chay chua va tu khoi dong backend local khi cau hinh cho phep.
- Dang nhap thong qua API va nhan JWT.
- Luu thong tin phien dang nhap trong `UserSession`.
- Goi backend API thong qua `ApiClients/`.
- Gan `X-API-Key` neu backend bat API key.
- Gan `Authorization: Bearer <jwt>` sau khi dang nhap.
- Hien thi du lieu len DataGridView, ComboBox va cac form.
- Xuat bao cao Excel/PDF.

WinForms khong truy cap PostgreSQL truc tiep. Moi thao tac du lieu di qua `QuanLyKhoHang.Api`.

## Vai Tro Trong Solution

```txt
QuanLyKhoHang.WinForms
->
ApiServerLauncher
  - GET /api/health
  - tu khoi dong QuanLyKhoHang.Api neu dung local dev localhost:8088
->
FrmDangNhap
->
AuthApiClient
->
POST /api/auth/login
->
ApiHttpClient.SetBearerToken(token)
->
FrmMain + cac form nghiep vu
->
ApiClients
->
QuanLyKhoHang.Api
->
PostgreSQL
```

## Cau Truc Thu Muc

```txt
QuanLyKhoHang.WinForms/
|   Program.cs
|   QuanLyKhoHang.WinForms.csproj
|   README.md
|
+---ApiClients/
|       ApiClientSettings.cs
|       ApiHttpClient.cs
|       ApiServerLauncher.cs
|       DanhMucApiClients.cs
|       KhoApiClients.cs
|
+---Config/
|       appsettings.example.json
|       appsettings.json
|
+---Forms/
|       FrmDangNhap.cs
|       FrmDangNhap.Designer.cs
|       FrmDangNhap.resx
|       FrmHangHoa.cs
|       FrmHangHoa.Designer.cs
|       FrmHangHoa.resx
|       FrmKhachHang.cs
|       FrmKhachHang.Designer.cs
|       FrmKhachHang.resx
|       FrmMain.cs
|       FrmMain.Designer.cs
|       FrmMain.resx
|       FrmNhanVien.cs
|       FrmNhanVien.Designer.cs
|       FrmNhanVien.resx
|       FrmNhapKho.cs
|       FrmNhapKho.Designer.cs
|       FrmNhapKho.resx
|       FrmXuatKho.cs
|       FrmXuatKho.Designer.cs
|       FrmXuatKho.resx
|       UiTheme.cs
|
+---Reports/
|       ExportExcel.cs
|       ExportPdf.cs
|
\---sql/
        backup_database.ps1
        create_tables.sql
        migrate_add_trang_thai.sql
        migrate_hash_sample_passwords.sql
        sample_data.sql
        sync_existing_database.sql
```

## Y Nghia Tung Thu Muc

| Thu muc/file | Vai tro |
| --- | --- |
| `Program.cs` | Diem chay dau tien. Khoi tao app, dam bao API dang chay, mo form dang nhap. |
| `ApiClients/` | Lop trung gian goi HTTP API. Form khong goi `HttpClient` truc tiep. |
| `Config/` | Cau hinh client nhu `ApiBaseUrl`, `ApiKey` va `AutoStartLocalApi`. |
| `Forms/` | Cac man hinh WinForms. |
| `../QuanLyKhoHang.Shared/Models/` | Model du lieu dung chung qua ProjectReference. |
| `Reports/` | Xuat Excel/PDF. |
| `sql/` | Script tao, migrate, seed, sync va backup database. |
| `QuanLyKhoHang.WinForms.csproj` | Cau hinh target `net10.0-windows`, WinForms, package export. |

## ApiClients

```txt
ApiClients/
|   ApiClientSettings.cs
|   ApiHttpClient.cs
|   ApiServerLauncher.cs
|   DanhMucApiClients.cs
|   KhoApiClients.cs
```

| File | Chuc nang |
| --- | --- |
| `ApiClientSettings.cs` | Doc `Config/appsettings.json` de lay `ApiBaseUrl`, `ApiKey`; van ho tro cau hinh cu `ApiClientSettings.BaseUrl`. |
| `ApiHttpClient.cs` | Wrapper dung chung cho GET/POST/PUT/DELETE, doc JSON ve DataTable, xu ly loi API, gan API key va Bearer token. |
| `ApiServerLauncher.cs` | Goi `/api/health`; chi tu khoi dong `QuanLyKhoHang.Api` khi dung local dev `localhost:8088` va `AutoStartLocalApi = true`. |
| `DanhMucApiClients.cs` | Client cho hang hoa, loai hang, nha cung cap, khach hang, nhan vien. |
| `KhoApiClients.cs` | Client cho ton kho, phieu nhap, phieu xuat va dang nhap. |

Luong goi API:

```txt
Form
->
*ApiClient
->
ApiHttpClient
  - ApiBaseUrl tu Config/appsettings.json
  - X-API-Key neu co
  - Bearer token sau login
->
QuanLyKhoHang.Api
```

## Config

```txt
Config/
|   appsettings.example.json
|   appsettings.json
```

Vi du:

```json
{
  "ApiBaseUrl": "http://localhost:8088",
  "ApiKey": "",
  "AutoStartLocalApi": false
}
```

| Key | Y nghia |
| --- | --- |
| `ApiBaseUrl` | Dia chi backend API. Dung `http://localhost:8088` khi API chay bang Docker local hoac Visual Studio. |
| `ApiKey` | API key gui len backend neu backend bat `ApiSettings.RequireApiKey`. De rong neu khong dung API key. |
| `AutoStartLocalApi` | `true` de WinForms tu khoi dong API local `localhost:8088` khi phat trien; `false` khi goi API Docker/server. |

WinForms khong ket noi PostgreSQL truc tiep. Neu dung API Docker, DBeaver ket noi database Docker bang `localhost:5433`, con API Docker van ket noi PostgreSQL noi bo bang `postgres:5432`.

JWT khong nam trong file config WinForms. Token duoc lay tu `/api/auth/login` va gan vao `ApiHttpClient` trong bo nho khi ung dung dang chay.

## Forms

```txt
Forms/
|   FrmDangNhap.cs
|   FrmHangHoa.cs
|   FrmKhachHang.cs
|   FrmMain.cs
|   FrmNhanVien.cs
|   FrmNhapKho.cs
|   FrmXuatKho.cs
|   UiTheme.cs
```

| Form | Chuc nang |
| --- | --- |
| `FrmDangNhap` | Doc username/password, goi API auth, nhan role/JWT, luu `UserSession`, mo `FrmMain`. |
| `FrmMain` | Man hinh chinh, menu dieu huong, hien user/role, phan quyen menu va dang xuat. |
| `FrmHangHoa` | Quan ly hang hoa. |
| `FrmKhachHang` | Quan ly khach hang. |
| `FrmNhanVien` | Quan ly nhan vien, chi hien cho Admin. |
| `FrmNhapKho` | Lap phieu nhap kho, them chi tiet phieu, xem lich su nhap. |
| `FrmXuatKho` | Lap phieu xuat kho, xem lich su xuat, in/xuat thong tin phieu. |
| `UiTheme` | Mau sac, font, style dung chung. |

Moi form thuong co:

```txt
FrmTenForm.cs           -> logic xu ly su kien
FrmTenForm.Designer.cs  -> code UI do WinForms Designer sinh ra
FrmTenForm.resx         -> resource cua form
```

## Phan Quyen Tren Desktop

WinForms phan quyen UI dua tren `UserSession.VaiTro` va ten tai khoan:

- `Admin`: xem tat ca chuc nang.
- `NhanVien`: khong vao man hinh quan ly nhan vien.
- Tai khoan kho chi hien luong nhap kho.
- Tai khoan ban hang chi hien luong xuat kho.

Phan quyen that su van duoc API kiem tra lai bang JWT/role. UI chi giup an nut khong phu hop; backend van la lop chan cuoi.

## Models Dung Chung

```txt
../QuanLyKhoHang.Shared/Models/
|   ChiTietPhieuNhap.cs
|   ChiTietPhieuXuat.cs
|   HangHoa.cs
|   KhachHang.cs
|   LoaiHang.cs
|   NhaCungCap.cs
|   NhanVien.cs
|   PhieuNhap.cs
|   PhieuXuat.cs
|   TaiKhoan.cs
|   UserSession.cs
```

WinForms va API cung reference `QuanLyKhoHang.Shared`, nen backend khong phu thuoc vao folder model cua UI.

`UserSession` chi luu thong tin can cho UI nhu ten tai khoan va vai tro. JWT duoc luu trong `ApiHttpClient` header, khong dat trong model Shared.

## Reports

```txt
Reports/
|   ExportExcel.cs
|   ExportPdf.cs
```

| File | Chuc nang |
| --- | --- |
| `ExportExcel.cs` | Xuat du lieu ra Excel. |
| `ExportPdf.cs` | Xuat du lieu ra PDF. |

## sql

```txt
sql/
|   backup_database.ps1
|   create_tables.sql
|   migrate_add_trang_thai.sql
|   migrate_hash_sample_passwords.sql
|   sample_data.sql
|   sync_existing_database.sql
```

| File | Chuc nang |
| --- | --- |
| `create_tables.sql` | Tao schema chinh, constraint, index va bang audit log. |
| `sample_data.sql` | Them du lieu mau, tai khoan mau dung mat khau hash. |
| `migrate_add_trang_thai.sql` | Them cot trang thai cho tai khoan neu database cu chua co. |
| `migrate_hash_sample_passwords.sql` | Chuyen mat khau mau sang dang hash. |
| `sync_existing_database.sql` | Dong bo database cu voi schema hien tai. |
| `backup_database.ps1` | Backup PostgreSQL bang `pg_dump`, doc thong tin ket noi tu bien moi truong. |

Khi tai lieu can minh hoa database password, de trong:

```txt
Password:
```

Neu can chay script backup voi mat khau database, dat bien moi truong `QLKH_DB_PASSWORD` tren may local thay vi ghi vao README.

## Luong Khoi Dong App

```txt
Program.cs
->
ApplicationConfiguration.Initialize()
->
ApiServerLauncher.EnsureStarted()
->
GET /api/health
->
Neu API chua chay thi tim DLL/project API va chay dotnet
  - chi ap dung khi AutoStartLocalApi = true va ApiBaseUrl la localhost:8088
->
Application.Run(new FrmDangNhap())
```

## Luong Dang Nhap

```txt
FrmDangNhap.cs
->
AuthApiClient.CheckLogin(username, password)
->
POST /api/auth/login
->
API tra ve vaiTro, token, expiresAt
->
ApiHttpClient.SetBearerToken(token)
->
UserSession luu ten tai khoan va vai tro
->
FrmMain phan quyen menu
```

## Luong Danh Muc

Vi du quan ly hang hoa:

```txt
FrmHangHoa.cs
->
HangHoaApiClient
->
ApiHttpClient gui Bearer token
->
GET/POST/PUT/DELETE /api/hang-hoa
->
API validate, phan quyen neu can, ghi audit log neu thay doi du lieu
->
DataTable JSON tra ve Form
->
DataGridView hien thi
```

## Luong Nhap Kho

```txt
FrmNhapKho.cs
->
PhieuNhapApiClient
->
POST /api/phieu-nhap
->
API validate chi tiet
->
Repository tao phieu, them chi tiet va cong ton trong transaction
->
AuditLogService ghi CREATE phieunhap
->
Load lai danh sach phieu va ton kho
```

## Luong Xuat Kho

```txt
FrmXuatKho.cs
->
PhieuXuatApiClient
->
POST /api/phieu-xuat
->
API validate chi tiet
->
Repository kiem tra ton, tao phieu, tru ton va them chi tiet trong transaction
->
AuditLogService ghi CREATE phieuxuat
->
Load lai danh sach phieu va ton kho
```

## Chay Project

Tu root repository:

```powershell
dotnet run --project QuanLyKhoHang.WinForms/QuanLyKhoHang.WinForms.csproj
```

## Publish WinForms

WinForms chay ngoai Docker. Tao ban `.exe` Windows:

```powershell
dotnet publish QuanLyKhoHang.WinForms/QuanLyKhoHang.WinForms.csproj `
  -c Release `
  -r win-x64 `
  --self-contained true
```

Neu API dang chay bang `docker compose up -d --build` tai root repository, dat cau hinh client:

```json
{
  "ApiBaseUrl": "http://localhost:8088",
  "ApiKey": "",
  "AutoStartLocalApi": false
}
```

Port dung khi publish:

```txt
WinForms -> http://localhost:8088
API Docker -> postgres:5432
DBeaver/Windows -> localhost:5433
PostgreSQL local neu chay khong Docker -> localhost:5432
```

Trong Visual Studio:

```txt
1. Mo QuanLyKhoHang.sln
2. Chuot phai project QuanLyKhoHang.WinForms
3. Chon Set as Startup Project
4. Bam Start
```

## Ghi Chu Khi Sua WinForms

- Sua logic trong `*.cs`, han che sua tay `*.Designer.cs`.
- Neu sua control bang Designer thi Visual Studio se tu cap nhat `*.Designer.cs`.
- Khong viet SQL trong Form.
- Khong tao `HttpClient` truc tiep trong Form; goi qua `ApiClients/`.
- Khi doi ten cot API tra ve, kiem tra DataGridView/ComboBox dang bind cot.
- Khi doi route API, cap nhat client tuong ung trong `ApiClients/`.
- Khi backend bat `RequireJwt`, dam bao login thanh cong truoc khi mo form nghiep vu.
- Khong ghi mat khau database vao README; neu can minh hoa thi de password rong.
