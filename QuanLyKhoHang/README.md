# QuanLyKhoHang

`QuanLyKhoHang` là project WinForms giao diện của hệ thống quản lý kho hàng.

Project này chịu trách nhiệm hiển thị màn hình, nhận thao tác người dùng, gọi backend API và xuất báo cáo. Project này không kết nối PostgreSQL trực tiếp.

## Vai Trò Trong Solution

```text
QuanLyKhoHang/
  giao diện WinForms
  -> ApiClients
  -> QuanLyKhoHang.Api
  -> PostgreSQL
```

Trong mô hình hiện tại:

- `QuanLyKhoHang` là client desktop.
- `QuanLyKhoHang.Api` là backend server.
- `QuanLyKhoHang/Models` chứa các model được UI sử dụng và cũng được API link vào để tránh lặp class.

## Cấu Trúc Thư Mục

```text
QuanLyKhoHang/
├── ApiClients/
│   ├── ApiClientSettings.cs
│   ├── ApiHttpClient.cs
│   ├── ApiServerLauncher.cs
│   ├── DanhMucApiClients.cs
│   └── KhoApiClients.cs
├── Config/
│   ├── appsettings.json
│   └── appsettings.example.json
├── Forms/
│   ├── FrmDangNhap.*
│   ├── FrmMain.*
│   ├── FrmHangHoa.*
│   ├── FrmKhachHang.*
│   ├── FrmNhanVien.*
│   ├── FrmNhapKho.*
│   └── FrmXuatKho.*
├── Models/
├── Reports/
├── sql/
├── Program.cs
└── QuanLyKhoHang.csproj
```

## Các Thành Phần Chính

### `Program.cs`

Điểm khởi động WinForms:

- Khởi tạo WinForms bằng `ApplicationConfiguration.Initialize()`.
- Gọi `ApiServerLauncher.EnsureStarted()` để đảm bảo backend API đang chạy.
- Mở form đăng nhập đầu tiên.
- Khi app thoát, dừng API nếu API do app tự bật.

### `ApiClients/`

Lớp trung gian để WinForms gọi backend API:

| File | Vai trò |
| --- | --- |
| `ApiHttpClient.cs` | Wrapper dùng chung cho `GET`, `POST`, `PUT`, `DELETE`, đọc lỗi API và chuyển JSON thành `DataTable`. |
| `ApiClientSettings.cs` | Đọc `Config/appsettings.json`. |
| `ApiServerLauncher.cs` | Kiểm tra `/api/health` và tự bật API nếu cần. |
| `DanhMucApiClients.cs` | Client cho hàng hóa, loại hàng, nhà cung cấp, khách hàng, nhân viên. |
| `KhoApiClients.cs` | Client cho tồn kho, phiếu nhập, phiếu xuất. |

### `Forms/`

Các màn hình chính:

| Form | Chức năng |
| --- | --- |
| `FrmDangNhap` | Đăng nhập và lấy vai trò người dùng. |
| `FrmMain` | Dashboard chính, menu điều hướng và phân quyền. |
| `FrmHangHoa` | Quản lý hàng hóa. |
| `FrmKhachHang` | Quản lý khách hàng. |
| `FrmNhanVien` | Quản lý nhân viên. |
| `FrmNhapKho` | Tạo phiếu nhập kho. |
| `FrmXuatKho` | Tạo phiếu xuất kho. |

### `Models/`

Model dữ liệu dùng ở client và được API project link vào:

```text
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

### `Reports/`

Xuất dữ liệu ra:

- Excel bằng ClosedXML.
- PDF bằng iTextSharp.LGPLv2.Core.

### `sql/`

Script PostgreSQL dùng để tạo database và dữ liệu mẫu. Thư mục này nằm trong project WinForms vì ban đầu app được tổ chức một project, nhưng hiện vẫn được giữ lại để tiện setup database.

## Cấu Hình API Client

File:

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

| Key | Ý nghĩa |
| --- | --- |
| `BaseUrl` | Địa chỉ backend API. |
| `ApiKey` | Key gửi lên API nếu backend bật `RequireApiKey`. |

## Chạy Project

Từ root repository:

```powershell
dotnet run --project QuanLyKhoHang/QuanLyKhoHang.csproj
```

Hoặc trong Visual Studio:

1. Mở `QuanLyKhoHang.sln` ở thư mục root.
2. Chuột phải project `QuanLyKhoHang`.
3. Chọn `Set as Startup Project`.
4. Bấm Start.

## Luồng Khởi Động

```text
Program.Main()
  -> ApplicationConfiguration.Initialize()
  -> ApiServerLauncher.EnsureStarted()
       -> gọi GET /api/health
       -> nếu API chưa chạy thì bật QuanLyKhoHang.Api
  -> Application.Run(new FrmDangNhap())
```

Nếu Visual Studio đang chạy `QuanLyKhoHang.Api` thay vì `QuanLyKhoHang`, giao diện có thể không mở đúng như mong muốn. Khi đó cần đặt startup project lại.

## Luồng Gọi Dữ Liệu

Ví dụ xóa nhân viên:

```text
FrmNhanVien.btnXoa_Click
  -> NhanVienApiClient.Xoa(id)
  -> ApiHttpClient.Delete("api/nhan-vien/{id}")
  -> QuanLyKhoHang.Api
  -> NhanVienRepository.Xoa(id)
```

Ví dụ lưu phiếu nhập:

```text
FrmNhapKho
  -> KhoApiClient.LuuPhieuNhap(...)
  -> POST /api/phieu-nhap
  -> API lưu phiếu trong transaction
```

## Phân Quyền Giao Diện

Tài khoản mẫu:

```text
admin / 123456
nhanvienkho / 123456
nhanvienbanhang / 123456
```

Vai trò:

- `admin`: dùng toàn bộ menu.
- `nhanvienkho`: dùng nhập kho, không dùng xuất kho, không vào nhân viên.
- `nhanvienbanhang`: dùng xuất kho, không dùng nhập kho, không vào nhân viên.

## Ghi Chú Khi Sửa UI

- Hạn chế sửa `*.Designer.cs` nếu chỉ thay logic xử lý.
- Nếu đổi alias cột từ API/repository, kiểm tra lại `DataGridView` và `ComboBox` đang đọc cột theo index/tên.
- Nếu đổi route API, cập nhật tương ứng trong `ApiClients`.
- Nếu thêm màn hình mới, nên tạo client API riêng hoặc mở rộng client hiện có thay vì gọi `HttpClient` trực tiếp trong form.
