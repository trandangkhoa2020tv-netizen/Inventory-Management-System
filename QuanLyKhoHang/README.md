# QuanLyKhoHang

Project WinForms giao diện của hệ thống quản lý kho hàng.

Project này không xử lý database trực tiếp. Mọi thao tác dữ liệu đi qua HTTP API trong project `QuanLyKhoHang.Api`.

## Vai trò

```text
QuanLyKhoHang
  -> hiển thị giao diện
  -> gọi API bằng HttpClient
  -> xuất Excel/PDF
  -> tự bật API nếu API chưa chạy
```

## Cấu trúc

```text
QuanLyKhoHang/
├── ApiClients/      # HttpClient wrapper gọi backend API
├── Config/          # Cấu hình địa chỉ API cho WinForms
├── Forms/           # Màn hình WinForms
├── Models/          # Model dùng cho UI và request API
├── Reports/         # Xuất Excel/PDF
├── sql/             # Script PostgreSQL dùng khi setup database
├── Program.cs       # Điểm khởi động WinForms
└── QuanLyKhoHang.csproj
```

## Cấu hình API client

File cấu hình:

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

## Chạy từ root solution

Từ thư mục ngoài cùng repository:

```powershell
dotnet run --project QuanLyKhoHang/QuanLyKhoHang.csproj
```

Hoặc mở `QuanLyKhoHang.sln` trong Visual Studio và đặt startup project là `QuanLyKhoHang`.

## Luồng gọi API

Ví dụ màn hình nhân viên:

```text
FrmNhanVien
  -> NhanVienApiClient
  -> http://localhost:5088/api/nhan-vien
  -> QuanLyKhoHang.Api
  -> PostgreSQL
```

## Lưu ý khi chạy

- API mặc định chạy ở `http://localhost:5088`.
- Khi app mở, `ApiServerLauncher` kiểm tra `/api/health`.
- Nếu API chưa chạy, WinForms tự bật `QuanLyKhoHang.Api`.
- Nếu Visual Studio chỉ chạy API mà không mở giao diện, đặt startup project lại là `QuanLyKhoHang`.

## Tài khoản mẫu

```text
admin / 123456
nhanvienkho / 123456
nhanvienbanhang / 123456
```
