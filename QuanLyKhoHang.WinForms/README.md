# QuanLyKhoHang - WinForms App

`QuanLyKhoHang` la project giao dien desktop WinForms cua he thong quan ly kho hang.

Project nay phu trach:

- Hien thi man hinh nguoi dung.
- Nhan thao tac them, sua, xoa, tim kiem.
- Goi backend API thong qua `ApiClients/`.
- Hien thi du lieu len DataGridView, ComboBox va cac form.
- Xuat bao cao Excel/PDF.

Project nay khong nen truy cap PostgreSQL truc tiep. Moi thao tac du lieu nen di qua backend API.

## Vai Tro Trong Solution

```txt
QuanLyKhoHang WinForms
->
ApiClients
->
QuanLyKhoHang.Api
->
PostgreSQL
```

## Cau Truc Thu Muc

```txt
QuanLyKhoHang/
|   .dockerignore
|   .gitignore
|   docker-compose.yml
|   Dockerfile.build
|   Program.cs
|   QuanLyKhoHang.csproj
|   QuanLyKhoHang.csproj.user
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
        create_tables.sql
        migrate_add_trang_thai.sql
        migrate_hash_sample_passwords.sql
        sample_data.sql
        sync_existing_database.sql
```

## Y Nghia Tung Thu Muc

| Thu muc/file | Vai tro |
| --- | --- |
| `Program.cs` | Diem chay dau tien cua WinForms. Khoi tao app, dam bao API dang chay, mo form dang nhap. |
| `ApiClients/` | Lop trung gian goi HTTP API. Form khong nen goi `HttpClient` truc tiep. |
| `Config/` | Cau hinh client nhu BaseUrl va ApiKey. |
| `Forms/` | Cac man hinh giao dien WinForms. |
| `../QuanLyKhoHang.Shared/Models/` | Model du lieu dung chung qua ProjectReference. |
| `Reports/` | Xuat file Excel/PDF. |
| `sql/` | Script tao database va du lieu mau. |
| `QuanLyKhoHang.csproj` | Cau hinh project WinForms. |

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
| `ApiClientSettings.cs` | Doc `Config/appsettings.json` de lay BaseUrl va ApiKey. |
| `ApiHttpClient.cs` | Wrapper dung chung cho GET, POST, PUT, DELETE, xu ly JSON va loi API. |
| `ApiServerLauncher.cs` | Goi `/api/health`, neu API chua chay thi tu khoi dong project API. |
| `DanhMucApiClients.cs` | Client cho hang hoa, loai hang, nha cung cap, khach hang, nhan vien. |
| `KhoApiClients.cs` | Client cho ton kho, phieu nhap, phieu xuat va dang nhap. |

Luong goi API mau:

```txt
Form
->
*ApiClient
->
ApiHttpClient
->
HTTP request
->
QuanLyKhoHang.Api
```

## Config

```txt
Config/
|   appsettings.example.json
|   appsettings.json
```

Vi du `appsettings.json`:

```json
{
  "ApiClientSettings": {
    "BaseUrl": "http://localhost:5088",
    "ApiKey": ""
  }
}
```

| Key | Y nghia |
| --- | --- |
| `BaseUrl` | Dia chi backend API. |
| `ApiKey` | API key gui len backend neu backend bat `RequireApiKey`. |

## Forms

```txt
Forms/
|   FrmDangNhap.cs
|   FrmDangNhap.Designer.cs
|   FrmDangNhap.resx
|   FrmHangHoa.cs
|   FrmHangHoa.Designer.cs
|   FrmHangHoa.resx
|   FrmKhachHang.cs
|   FrmKhachHang.Designer.cs
|   FrmKhachHang.resx
|   FrmMain.cs
|   FrmMain.Designer.cs
|   FrmMain.resx
|   FrmNhanVien.cs
|   FrmNhanVien.Designer.cs
|   FrmNhanVien.resx
|   FrmNhapKho.cs
|   FrmNhapKho.Designer.cs
|   FrmNhapKho.resx
|   FrmXuatKho.cs
|   FrmXuatKho.Designer.cs
|   FrmXuatKho.resx
|   UiTheme.cs
```

| Form | Chuc nang |
| --- | --- |
| `FrmDangNhap` | Dang nhap, goi API auth, tao session nguoi dung. |
| `FrmMain` | Man hinh chinh, menu dieu huong va phan quyen. |
| `FrmHangHoa` | Quan ly hang hoa. |
| `FrmKhachHang` | Quan ly khach hang. |
| `FrmNhanVien` | Quan ly nhan vien. |
| `FrmNhapKho` | Lap phieu nhap kho, them chi tiet phieu, xem lich su nhap. |
| `FrmXuatKho` | Lap phieu xuat kho, xem lich su xuat, in/xuat thong tin phieu. |
| `UiTheme` | Mau sac, font, style dung chung cho giao dien. |

Moi form thuong co 3 file:

```txt
FrmTenForm.cs           -> logic xu ly su kien
FrmTenForm.Designer.cs  -> code UI do WinForms Designer sinh ra
FrmTenForm.resx         -> resource cua form
```

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

| Model | Y nghia |
| --- | --- |
| `HangHoa` | Mat hang trong kho. |
| `LoaiHang` | Nhom/loai hang hoa. |
| `NhaCungCap` | Don vi cung cap hang. |
| `KhachHang` | Khach hang mua hang. |
| `NhanVien` | Nhan vien lap phieu va su dung he thong. |
| `TaiKhoan` | Tai khoan dang nhap. |
| `PhieuNhap` | Phieu nhap kho. |
| `ChiTietPhieuNhap` | Tung dong hang trong phieu nhap. |
| `PhieuXuat` | Phieu xuat kho. |
| `ChiTietPhieuXuat` | Tung dong hang trong phieu xuat. |
| `UserSession` | Luu thong tin nguoi dung dang dang nhap. |

Luu y: model da tach sang project `QuanLyKhoHang.Shared`. WinForms va API cung reference project nay, nen backend khong con phu thuoc vao folder model cua WinForms.

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
|   create_tables.sql
|   migrate_add_trang_thai.sql
|   migrate_hash_sample_passwords.sql
|   sample_data.sql
|   sync_existing_database.sql
```

| File | Chuc nang |
| --- | --- |
| `create_tables.sql` | Tao bang database chinh. |
| `sample_data.sql` | Them du lieu mau. |
| `migrate_add_trang_thai.sql` | Them cot trang thai cho tai khoan neu database cu chua co. |
| `migrate_hash_sample_passwords.sql` | Chuyen mat khau mau sang dang hash. |
| `sync_existing_database.sql` | Dong bo database cu voi schema hien tai. |

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
Neu API chua chay thi khoi dong QuanLyKhoHang.Api
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
API tra ve vai tro
->
UserSession luu thong tin nguoi dung
->
FrmMain phan quyen menu
```

Tai khoan mau:

```txt
admin / 123456
nhanvienkho / 123456
nhanvienbanhang / 123456
```

## Luong Danh Muc

Vi du quan ly hang hoa:

```txt
FrmHangHoa.cs
->
HangHoaApiClient
->
GET/POST/PUT/DELETE /api/hang-hoa
->
API xu ly
->
DataTable tra ve Form
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
API tao phieu nhap trong transaction
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
API kiem tra ton kho va tru ton trong transaction
->
Load lai danh sach phieu va ton kho
```

## Chay Project

Tu root repository:

```powershell
dotnet run --project QuanLyKhoHang.WinForms/QuanLyKhoHang.WinForms.csproj
```

Trong Visual Studio:

```txt
1. Mo QuanLyKhoHang.sln
2. Chuot phai project QuanLyKhoHang
3. Chon Set as Startup Project
4. Bam Start
```

## Ghi Chu Khi Sua WinForms

- Sua logic trong `*.cs`, han che sua tay `*.Designer.cs`.
- Neu sua control bang Designer thi Visual Studio se tu cap nhat `*.Designer.cs`.
- Khong viet SQL trong Form.
- Khong tao `HttpClient` truc tiep trong Form, nen goi qua `ApiClients/`.
- Khi doi ten cot API tra ve, kiem tra lai DataGridView/ComboBox dang bind cot.
- Khi doi route API, cap nhat client tuong ung trong `ApiClients/`.
