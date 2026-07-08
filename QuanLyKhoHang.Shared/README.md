# QuanLyKhoHang.Shared

Thu muc nay la project dung chung cho ca hai project chinh:

- `QuanLyKhoHang`: ung dung WinForms.
- `QuanLyKhoHang.Api`: backend Web API.

Muc dich cua project nay la tach cac model du lieu ra khoi WinForms. Nhu vay API khong con phu thuoc nguoc vao project giao dien, va ca hai ben cung dung mot bo class model giong nhau.

## Vai tro trong he thong

```txt
QuanLyKhoHang.Shared
|
+-- Models/        -> Model du lieu dung chung
+-- README.md      -> Giai thich chuc nang thu muc
+-- .gitignore     -> Bo qua file build/local cua project Shared
\-- QuanLyKhoHang.Shared.csproj
```

Project nay khong xu ly database, khong goi API, khong chua giao dien va khong chua nghiep vu phuc tap. No chi nen chua class du lieu dung chung.

## Vi sao can tach Shared?

Truoc day model nam trong:

```txt
QuanLyKhoHang/Models
```

Neu API lay model truc tiep tu folder do thi backend bi phu thuoc vao WinForms. Huong phu thuoc do khong tot, vi giao dien la lop ben ngoai, con API la lop xu ly rieng.

Sau khi tach:

```txt
QuanLyKhoHang.Shared/Models
```

Ca WinForms va API cung reference project Shared:

```txt
QuanLyKhoHang
        \
         -> QuanLyKhoHang.Shared
        /
QuanLyKhoHang.Api
```

## Cau truc file

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

## Ghi chu tung file

| File | Chuc nang |
| --- | --- |
| `.gitignore` | Bo qua file build/local nhu `bin/`, `obj/`, `.vs/`, `*.user`, `*.log`. |
| `QuanLyKhoHang.Shared.csproj` | Cau hinh class library, target framework va nullable/implicit using. |
| `Models/HangHoa.cs` | Model mat hang trong kho. Chua ma hang, ten hang, loai hang, nha cung cap, gia nhap, gia ban, so luong ton, don vi tinh va ghi chu. |
| `Models/LoaiHang.cs` | Model nhom/loai hang hoa. Dung de phan loai hang hoa trong kho. |
| `Models/NhaCungCap.cs` | Model nha cung cap. Dung cho nghiep vu nhap kho va thong tin nguon hang. |
| `Models/KhachHang.cs` | Model khach hang. Dung cho nghiep vu xuat kho/ban hang. |
| `Models/NhanVien.cs` | Model nhan vien. Dung cho quan ly nhan su va gan nguoi lap phieu. |
| `Models/TaiKhoan.cs` | Model tai khoan dang nhap. Chua ten tai khoan, mat khau da luu va vai tro. |
| `Models/PhieuNhap.cs` | Model thong tin chung cua phieu nhap kho. Chi tiet mat hang nam trong `ChiTietPhieuNhap`. |
| `Models/ChiTietPhieuNhap.cs` | Model tung dong hang trong phieu nhap. Chua hang hoa, so luong, don gia va thanh tien. |
| `Models/PhieuXuat.cs` | Model thong tin chung cua phieu xuat kho. Chi tiet mat hang nam trong `ChiTietPhieuXuat`. |
| `Models/ChiTietPhieuXuat.cs` | Model tung dong hang trong phieu xuat. Chua hang hoa, so luong, don gia va thanh tien. |
| `Models/UserSession.cs` | Bo nho phien dang nhap hien tai cua WinForms. Sau login, WinForms luu ten tai khoan va vai tro vao day de phan quyen menu. |

## Nguyen tac su dung

- WinForms duoc dung cac model nay de bind du lieu len form, grid va request API.
- API duoc dung cac model nay de nhan du lieu tu request cu va truyen sang service/repository.
- Khong dat code truy van SQL trong project Shared.
- Khong dat code goi `HttpClient` trong project Shared.
- Khong dat code WinForms, control, form hoac message box trong project Shared.
- Neu can kieu request/response rieng cho API, dat trong `QuanLyKhoHang.Api/DTOs`, khong dat vao Shared tru khi ca WinForms va API cung can dung chung.

## Khi nao them model moi?

Them model moi vao `Models/` khi database co bang hoac khai niem du lieu dung chung giua WinForms va API.

Vi du:

```txt
Models/Kho.cs
Models/DonViTinh.cs
Models/PhanQuyen.cs
```

Sau khi them model, nen cap nhat:

- README nay.
- API DTO neu model co endpoint rieng.
- Service validation neu model co rule nghiep vu.
- Test neu model lien quan den luong quan trong.
