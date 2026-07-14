# QuanLyKhoHang.Tests

`QuanLyKhoHang.Tests` la project test tu dong cua solution `QuanLyKhoHang`.

Muc dich cua project nay la kiem tra cac rule quan trong ma khong can thao tac thu cong tren WinForms. Hien tai test tap trung vao:

- Validation o service.
- Tao, kiem tra va doc thong tin JWT.
- Cac rule dau vao cua phieu nhap/phieu xuat.

Test hien tai la unit test, khong can PostgreSQL that va khong can cau hinh mat khau database.

Luong chay test hien tai:

```txt
dotnet test
->
xUnit
->
JwtTokenServiceTests
  - tao JwtSettings rieng cho test
  - tao/validate/doc token trong bo nho
->
ServiceValidationTests
  - tao service voi repository that
  - gui input sai
  - service validate va throw truoc khi repository cham PostgreSQL
```

## Vai Tro Trong He Thong

```txt
QuanLyKhoHang.Tests
|
+-- JwtTokenServiceTests.cs      -> Test tao/validate/doc user-role tu JWT
+-- ServiceValidationTests.cs    -> Test cac rule validate trong service
+-- QuanLyKhoHang.Tests.csproj   -> Cau hinh xUnit va project reference
+-- README.md                    -> Giai thich chuc nang thu muc
\-- .gitignore                   -> Bo qua file build/test/local
```

Project test reference:

- `QuanLyKhoHang.Api`: de test service, DTO, config va JWT.
- `QuanLyKhoHang.Shared`: de dung model nhu `HangHoa`, `PhieuNhap`, `PhieuXuat`.

## Cach Chay Test

Chay tat ca test trong solution:

```powershell
dotnet test QuanLyKhoHang.sln
```

Chay rieng project test:

```powershell
dotnet test QuanLyKhoHang.Tests/QuanLyKhoHang.Tests.csproj
```

Neu da restore package roi va muon chay nhanh:

```powershell
dotnet test QuanLyKhoHang.sln --no-restore
```

## Ghi Chu Tung File

| File | Chuc nang |
| --- | --- |
| `QuanLyKhoHang.Tests.csproj` | Cau hinh xUnit, test SDK, coverlet va reference den API/Shared. |
| `ServiceValidationTests.cs` | Kiem tra rule validate o service ma khong can database that. |
| `JwtTokenServiceTests.cs` | Kiem tra JWT tao ra hop le, token sai bi tu choi, token doc duoc username/role. |
| `.gitignore` | Bo qua `bin/`, `obj/`, `TestResults/`, coverage, `.vs/`, `*.user`, `*.log`. |

`QuanLyKhoHang.Tests.csproj` hien target `net10.0`, bat nullable, dung `xunit`, `xunit.runner.visualstudio`, `Microsoft.NET.Test.Sdk` va `coverlet.collector`.

## Test Trong ServiceValidationTests.cs

| Test | Kiem tra |
| --- | --- |
| `AuthService_ShouldRejectEmptyLoginRequest` | Username/password rong phai bi tu choi truoc khi xu ly dang nhap. |
| `HangHoaService_ShouldRejectMissingName` | Them hang hoa thieu ten phai bao loi `tenHangHoa`. |
| `HangHoaService_ShouldRejectNegativeStock` | So luong ton am phai bi chan. |
| `PhieuNhapService_ShouldRejectEmptyDetails` | Phieu nhap khong co chi tiet hang phai bi chan. |
| `PhieuNhapService_ShouldRejectInvalidDetailQuantity` | Chi tiet phieu nhap co so luong khong hop le phai bi chan. |
| `PhieuXuatService_ShouldRejectEmptyDetails` | Phieu xuat khong co chi tiet hang phai bi chan. |
| `PhieuXuatService_ShouldRejectInvalidDetailQuantity` | Chi tiet phieu xuat co so luong khong hop le phai bi chan. |

## Test Trong JwtTokenServiceTests.cs

| Test | Kiem tra |
| --- | --- |
| `CreateToken_ShouldReturnTokenThatCanBeValidated` | Token vua tao khong rong, con han va validate thanh cong. |
| `TryReadUser_ShouldReturnUsernameAndRole` | Token hop le doc duoc username va role de API phan quyen. |
| `IsValid_ShouldRejectInvalidToken` | Token sai dinh dang bi tu choi. |
| `CreateService` | Helper tao `JwtTokenService` voi cau hinh test rieng, khong phu thuoc `appsettings.json`. |

## Nhung Phan Dang Duoc Bao Ve

- Login thieu username/password.
- Them hang hoa thieu ten.
- Them hang hoa co ton kho am.
- Luu phieu nhap khong co chi tiet.
- Luu phieu nhap co so luong chi tiet khong hop le.
- Luu phieu xuat khong co chi tiet.
- Luu phieu xuat co so luong chi tiet khong hop le.
- JWT tao ra phai validate duoc.
- JWT hop le phai doc duoc username/role.
- JWT sai dinh dang phai bi tu choi.

## Test Nen Bo Sung Tiep

Nhung test sau can PostgreSQL test database rieng, nen nen tach thanh integration test:

- Login dung voi tai khoan mau trong `sample_data.sql`.
- Login sai mat khau.
- Them/sua/xoa hang hoa thanh cong.
- Khong cho xoa hang hoa da co chung tu.
- Nhap kho phai cong ton.
- Xuat kho du ton phai tru ton.
- Xuat kho thieu ton phai bi chan.
- Xoa nhan vien da co chung tu phai bi chan.
- Endpoint can Admin tra `403` khi token khong du quyen.
- Endpoint nghiep vu tra `401` khi `JwtSettings.RequireJwt = true` va thieu token.
- Audit log duoc ghi khi them/sua/xoa hoac lap phieu.

## Nguyen Tac Viet Test Moi

- Ten test nen noi ro hanh vi can kiem tra.
- Moi test chi nen kiem tra mot rule chinh.
- Khong dung database that trong unit test.
- Neu test can database, tao nhom integration test rieng va dung database test rieng.
- Khong de test phu thuoc vao thu tu chay.
- Khong dua mat khau database vao README, test output hoac source control.
- Nen test ca truong hop dung va sai cho nghiep vu quan trong.
