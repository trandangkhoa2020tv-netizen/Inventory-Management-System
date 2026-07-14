# QuanLyKhoHang.Shared

`QuanLyKhoHang.Shared` la class library dung chung cho:

- `QuanLyKhoHang.WinForms`: ung dung desktop.
- `QuanLyKhoHang.Api`: backend Minimal API.

Muc dich cua project nay la tach model du lieu ra khoi WinForms de API khong phu thuoc nguoc vao UI. Shared chi chua class du lieu dung chung; khong chua SQL, HTTP client, form, JWT service hay cau hinh bao mat.

## Vi Tri Trong Luong He Thong

```txt
QuanLyKhoHang.WinForms
        |
        | ProjectReference
        v
QuanLyKhoHang.Shared/Models
        ^
        | ProjectReference
        |
QuanLyKhoHang.Api
```

Luong runtime hien tai:

```txt
WinForms Form
->
ApiClient
->
API Endpoint
->
Service/Repository
->
PostgreSQL
```

Shared chi cung cap model cho WinForms va API trong luong tren. Token JWT, API key, audit log, transaction, DTO response, JSON `DataTable` va truy van database khong nam trong Shared.

## Cau Truc File

```txt
QuanLyKhoHang.Shared/
|
+-- Models/
|   +-- ChiTietPhieuNhap.cs
|   +-- ChiTietPhieuXuat.cs
|   +-- HangHoa.cs
|   +-- KhachHang.cs
|   +-- LoaiHang.cs
|   +-- NhaCungCap.cs
|   +-- NhanVien.cs
|   +-- PhieuNhap.cs
|   +-- PhieuXuat.cs
|   +-- TaiKhoan.cs
|   \-- UserSession.cs
|
+-- .gitignore
+-- README.md
\-- QuanLyKhoHang.Shared.csproj
```

## Ghi Chu Tung File

| File | Chuc nang |
| --- | --- |
| `QuanLyKhoHang.Shared.csproj` | Cau hinh class library target `net10.0`. |
| `.gitignore` | Bo qua output build, IDE file va log local cua project Shared. |
| `Models/HangHoa.cs` | Model mat hang trong kho. |
| `Models/LoaiHang.cs` | Model nhom/loai hang hoa. |
| `Models/NhaCungCap.cs` | Model nha cung cap. |
| `Models/KhachHang.cs` | Model khach hang. |
| `Models/NhanVien.cs` | Model nhan vien. |
| `Models/TaiKhoan.cs` | Model tai khoan dang nhap; mat khau luu trong database duoi dang hash. |
| `Models/PhieuNhap.cs` | Model thong tin chung cua phieu nhap kho. |
| `Models/ChiTietPhieuNhap.cs` | Model tung dong hang trong phieu nhap. |
| `Models/PhieuXuat.cs` | Model thong tin chung cua phieu xuat kho. |
| `Models/ChiTietPhieuXuat.cs` | Model tung dong hang trong phieu xuat. |
| `Models/UserSession.cs` | Bo nho phien dang nhap tren WinForms: ten tai khoan va vai tro. |

## Nguyen Tac Su Dung

- WinForms duoc dung model de bind du lieu len form, grid va tao request API.
- API duoc dung model cho request `/api/...` hien tai va truyen sang service/repository khi phu hop.
- Request/response typed object cho `/api/v2/...` dat trong `QuanLyKhoHang.Api/DTOs`.
- JSON tu `DataTable` cho WinForms duoc chuyen doi trong `QuanLyKhoHang.Api/DataTableJson.cs`, khong dat trong Shared.
- `UserSession` chi dung cho UI desktop; backend phan quyen bang JWT trong `HttpContext.User`.
- Khong dat code SQL trong Shared.
- Khong dat code `HttpClient` trong Shared.
- Khong dat code WinForms, control, form hoac message box trong Shared.
- Khong dat secret, API key, JWT secret hay mat khau database trong Shared.

## Khi Nao Them Model Moi?

Them model moi vao `Models/` khi database co bang hoac khai niem du lieu dung chung giua WinForms va API.

Vi du:

```txt
Models/Kho.cs
Models/DonViTinh.cs
Models/PhanQuyen.cs
```

Sau khi them model, can cap nhat:

- README nay.
- API DTO neu model co endpoint rieng.
- Service validation neu model co rule nghiep vu.
- ApiClient/Form neu WinForms can hien thi hoac gui model do.
- Test neu model lien quan den luong quan trong.
