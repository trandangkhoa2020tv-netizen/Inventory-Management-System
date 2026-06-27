# QuanLyKhoHang

Ứng dụng quản lý kho hàng viết bằng C# WinForms, dùng PostgreSQL để lưu dữ liệu và có API HTTP nội bộ để tích hợp hoặc kiểm thử dữ liệu bằng JSON.

## Mục tiêu chương trình

Phần mềm hỗ trợ các nghiệp vụ kho cơ bản:

- Quản lý danh mục hàng hóa, loại hàng, nhà cung cấp, khách hàng và nhân viên.
- Lập phiếu nhập kho, tự cộng số lượng tồn.
- Lập phiếu xuất kho, kiểm tra tồn kho và chặn xuất âm.
- Tra cứu lịch sử phiếu nhập/xuất.
- Xuất chi tiết phiếu ra Excel hoặc PDF.
- Cung cấp API chạy kèm ứng dụng để hệ thống khác có thể đọc/ghi dữ liệu.

## Công nghệ sử dụng

- Ngôn ngữ: C#.
- Giao diện: Windows Forms.
- Framework: .NET 10 Windows.
- Database: PostgreSQL.
- Thư viện database: `Npgsql`.
- Xuất Excel: `ClosedXML`.
- Xuất PDF: `iTextSharp.LGPLv2.Core`.
- API nội bộ: ASP.NET Core Minimal API chạy chung tiến trình với WinForms.

## Cấu trúc thư mục

```text
QuanLyKhoHang/
  Api/                 API HTTP nội bộ, chuyển dữ liệu kho thành JSON.
  Config/              File cấu hình database và API.
  Data/                Lớp kết nối và helper chạy SQL.
  Forms/               Giao diện WinForms và xử lý sự kiện người dùng.
  Models/              Các class dữ liệu tương ứng bảng database.
  Repositories/        Tầng truy cập dữ liệu và nghiệp vụ database.
  Reports/             Xuất báo cáo Excel/PDF.
  sql/                 Script tạo bảng, dữ liệu mẫu và migration.
  Program.cs           Điểm khởi động ứng dụng.
```

## Ghi chú từng nhóm file

### `Program.cs`

Điểm khởi động chương trình:

- Khởi tạo cấu hình WinForms.
- Bật API nội bộ nếu `ApiSettings.Enabled = true`.
- Mở form đăng nhập `FrmDangNhap`.
- Khi ứng dụng đóng, API cũng tự dừng.

### `Data/DbConnection.cs`

Quản lý chuỗi kết nối PostgreSQL:

- Đọc thông tin kết nối từ `Config/appsettings.json`.
- Có chuỗi kết nối mặc định để tránh crash khi thiếu file config.
- Cung cấp `GetConnection()` cho toàn bộ repository.
- Cung cấp `TestConnection()` cho API health check.

### `Data/DatabaseHelper.cs`

Helper chạy SQL:

- `ExecuteNonQuery`: chạy `INSERT`, `UPDATE`, `DELETE`.
- `ExecuteQuery`: chạy `SELECT` và trả về `DataTable`.
- `ExecuteScalar`: chạy SQL trả về một giá trị như `COUNT`, `SUM`, `RETURNING id`.

### `Api/ApiHost.cs`

Host API nội bộ:

- Đọc cấu hình API từ `appsettings.json`.
- Map toàn bộ endpoint `/api/...`.
- Bọc lỗi API bằng hàm `Safe()` để trả lỗi HTTP thay vì làm sập ứng dụng.
- Trả kết quả CRUD thống nhất bằng `ExecuteCreate`, `ExecuteUpdate`, `ExecuteDelete`.

### `Api/InventoryApiService.cs`

Lớp nghiệp vụ cho API:

- Gọi repository có sẵn để tái sử dụng logic CRUD.
- Chạy một số query riêng để trả tên cột JSON dạng `camelCase`.
- Cung cấp dữ liệu hàng hóa, loại hàng, nhà cung cấp, khách hàng, nhân viên, phiếu nhập, phiếu xuất và tồn kho thấp.

### `Api/DataTableJson.cs`

Chuyển `DataTable` thành danh sách object JSON:

- Mỗi dòng `DataRow` thành một object.
- Tên cột là key JSON.
- `DBNull.Value` được đổi thành `null`.

### `Models/`

Các class dữ liệu:

- `HangHoa`: thông tin mặt hàng, giá nhập, giá bán, tồn kho.
- `LoaiHang`: nhóm hàng hóa.
- `NhaCungCap`: thông tin nhà cung cấp.
- `KhachHang`: thông tin khách hàng.
- `NhanVien`: thông tin nhân viên.
- `TaiKhoan`: thông tin đăng nhập và vai trò.
- `UserSession`: lưu tài khoản/vai trò đang đăng nhập.
- `PhieuNhap`: thông tin chung phiếu nhập.
- `ChiTietPhieuNhap`: từng dòng hàng trong phiếu nhập.
- `PhieuXuat`: thông tin chung phiếu xuất.
- `ChiTietPhieuXuat`: từng dòng hàng trong phiếu xuất.

### `Repositories/`

Tầng truy cập dữ liệu:

- `HangHoaRepository`: CRUD hàng hóa.
- `LoaiHangRepository`: CRUD loại hàng.
- `NhaCungCapRepository`: CRUD nhà cung cấp.
- `KhachHangRepository`: CRUD khách hàng.
- `NhanVienRepository`: CRUD nhân viên.
- `TaiKhoanRepository`: kiểm tra đăng nhập, tương thích database cũ chưa có cột `trang_thai`.
- `PhieuNhapRepository`: lấy lịch sử phiếu nhập, lấy chi tiết, lưu phiếu nhập bằng transaction và cộng tồn kho.
- `PhieuXuatRepository`: lấy lịch sử phiếu xuất, lấy chi tiết, lưu phiếu xuất bằng transaction, kiểm tra và trừ tồn kho.

### `Forms/`

Giao diện người dùng:

- `FrmDangNhap`: đăng nhập, lưu `UserSession`, mở màn hình chính.
- `FrmMain`: menu chính, phân quyền theo vai trò, nhúng các form con.
- `FrmHangHoa`: quản lý hàng hóa, tìm kiếm theo tên hàng/loại hàng/nhà cung cấp.
- `FrmKhachHang`: quản lý khách hàng.
- `FrmNhanVien`: quản lý nhân viên.
- `FrmNhapKho`: lập phiếu nhập, thêm chi tiết tạm, tính tổng tiền, lưu phiếu và cộng tồn.
- `FrmXuatKho`: lập phiếu xuất, kiểm tra tồn kho, lưu phiếu và trừ tồn.

Không nên sửa trực tiếp các file `*.Designer.cs` nếu không cần thiết, vì đây là file WinForms sinh ra để lưu layout và event binding.

### `Reports/`

Xuất báo cáo:

- `ExportExcel.ToExcel`: xuất `DataTable` ra file `.xlsx`.
- `ExportPdf.ToPdf`: xuất `DataTable` ra file `.pdf`, dùng font Arial để hỗ trợ tiếng Việt.

### `sql/`

Script database:

- `create_tables.sql`: tạo schema PostgreSQL.
- `sample_data.sql`: thêm dữ liệu mẫu và tài khoản mẫu.
- `migrate_add_trang_thai.sql`: thêm cột `trang_thai` cho database cũ nếu thiếu.

## Cấu hình

File cấu hình nằm tại:

```text
Config/appsettings.json
```

Ví dụ:

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
    "Enabled": true,
    "Url": "http://localhost:5088"
  }
}
```

Ý nghĩa:

- `DatabaseSettings`: thông tin kết nối PostgreSQL.
- `ApiSettings.Enabled`: bật/tắt API nội bộ.
- `ApiSettings.Url`: địa chỉ API lắng nghe.

## Khởi tạo database

1. Tạo database PostgreSQL:

```sql
CREATE DATABASE quanlyhanghoa;
```

2. Kết nối vào database `quanlyhanghoa`.

3. Chạy script tạo bảng:

```text
sql/create_tables.sql
```

4. Chạy dữ liệu mẫu:

```text
sql/sample_data.sql
```

Nếu database cũ báo lỗi thiếu cột `trang_thai`, chạy thêm:

```text
sql/migrate_add_trang_thai.sql
```

Hoặc chạy trực tiếp:

```sql
ALTER TABLE taikhoan
ADD COLUMN IF NOT EXISTS trang_thai boolean NOT NULL DEFAULT true;
```

## Tài khoản mẫu

```text
admin / 123456
nhanvienkho / 123456
nhanvienbanhang / 123456
```

Phân quyền mẫu:

- `admin`: xem toàn bộ chức năng.
- `nhanvienkho`: dùng nhập kho, không dùng xuất kho.
- `nhanvienbanhang`: dùng xuất kho, không dùng nhập kho.
- Tài khoản vai trò `NhanVien` không được vào form quản lý nhân viên.

## Chạy chương trình

Vào thư mục project:

```powershell
cd D:\QuanLyKhoHang\QuanLyKhoHang
```

Restore package nếu cần:

```powershell
dotnet restore
```

Chạy ứng dụng:

```powershell
dotnet run
```

Build kiểm tra:

```powershell
dotnet build QuanLyKhoHang.csproj
```

Nếu build báo file `.exe` hoặc `.dll` đang bị khóa, nghĩa là ứng dụng đang mở. Đóng cửa sổ chương trình rồi build lại.

## API nội bộ

API tự chạy cùng WinForms nếu `ApiSettings.Enabled = true`.

Base URL mặc định:

```text
http://localhost:5088
```

### Endpoint hệ thống

```http
GET /api/health
GET /api/chuc-nang
```

`/api/health` trả trạng thái API và kết nối database.

### Endpoint hàng hóa

```http
GET    /api/hang-hoa
POST   /api/hang-hoa
PUT    /api/hang-hoa/{id}
DELETE /api/hang-hoa/{id}
```

Ví dụ thêm hàng hóa:

```http
POST http://localhost:5088/api/hang-hoa
Content-Type: application/json

{
  "tenHangHoa": "Ban phim co",
  "maLoaiHang": 1,
  "maNhaCungCap": 1,
  "giaNhap": 300000,
  "giaBan": 450000,
  "soLuongTon": 20,
  "donViTinh": "Cai",
  "ghiChu": "Hang moi"
}
```

### Endpoint danh mục

```http
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

### Endpoint kho và chứng từ

```http
GET /api/ton-kho/thap?soLuongToiDa=10
GET /api/phieu-nhap
GET /api/phieu-nhap/{id}/chi-tiet
GET /api/phieu-xuat
GET /api/phieu-xuat/{id}/chi-tiet
```

## Luồng nghiệp vụ nhập kho

1. Người dùng mở `FrmNhapKho`.
2. Chọn nhà cung cấp, nhân viên, mặt hàng, số lượng và đơn giá.
3. Bấm thêm hàng để đưa vào bảng chi tiết tạm.
4. Hệ thống tính tổng tiền.
5. Bấm lưu phiếu.
6. `PhieuNhapRepository.LuuPhieuNhap()` tạo transaction.
7. Tạo phiếu nhập.
8. Thêm từng dòng chi tiết.
9. Cộng tồn kho từng mặt hàng.
10. Commit nếu thành công, rollback nếu có lỗi.

## Luồng nghiệp vụ xuất kho

1. Người dùng mở `FrmXuatKho`.
2. Chọn khách hàng, nhân viên, mặt hàng, số lượng và đơn giá.
3. Khi thêm hàng, form kiểm tra số lượng muốn xuất không vượt tồn kho.
4. Bấm lưu phiếu.
5. `PhieuXuatRepository.LuuPhieuXuat()` tạo transaction.
6. Tạo phiếu xuất.
7. Trừ tồn kho bằng điều kiện `so_luong_ton >= @soluong`.
8. Thêm chi tiết phiếu xuất.
9. Commit nếu thành công, rollback nếu thiếu tồn hoặc có lỗi.

## Lỗi thường gặp

### `column "trang_thai" does not exist`

Database cũ chưa có cột trạng thái tài khoản. Có thể xử lý bằng một trong hai cách:

- Chạy `sql/migrate_add_trang_thai.sql`.
- Hoặc dùng code hiện tại vì `TaiKhoanRepository` đã tự tương thích database cũ.

### Build báo file đang bị khóa

Thông báo thường gặp:

```text
The process cannot access the file ... QuanLyKhoHang.exe because it is being used by another process
```

Cách xử lý: đóng ứng dụng `QuanLyKhoHang` đang chạy rồi build lại.

### Không kết nối được database

Kiểm tra:

- PostgreSQL đã chạy chưa.
- Database có tên đúng là `quanlyhanghoa` chưa.
- `Host`, `Port`, `Username`, `Password` trong `Config/appsettings.json` có đúng không.
- Đã chạy `create_tables.sql` và `sample_data.sql` chưa.

## Ghi chú bảo trì

- Nên giữ các câu SQL có tham số `NpgsqlParameter` để tránh SQL injection.
- Không sửa file `*.Designer.cs` nếu chỉ thay đổi logic.
- Nếu đổi alias tên cột trong repository, cần kiểm tra các form đang đọc cột đó.
- Các thao tác nhập/xuất kho nên luôn dùng transaction để không lệch tồn kho.
