# QuanLyKhoHang.Tests

Thu muc nay la project test tu dong cua solution `QuanLyKhoHang`.

Muc dich cua project nay la kiem tra cac rule quan trong ma khong can thao tac thu cong tren WinForms. Hien tai test tap trung vao validation o service va JWT, vi cac phan nay co the test nhanh ma khong can PostgreSQL that.

## Vai tro trong he thong

```txt
QuanLyKhoHang.Tests
|
+-- JwtTokenServiceTests.cs      -> Test tao va kiem tra JWT
+-- ServiceValidationTests.cs    -> Test cac rule validate trong service
+-- QuanLyKhoHang.Tests.csproj   -> Cau hinh xUnit va project reference
+-- README.md                    -> Giai thich chuc nang thu muc
\-- .gitignore                   -> Bo qua file build/test/local
```

Project test dang reference:

- `QuanLyKhoHang.Api`: de test service, DTO, config va JWT.
- `QuanLyKhoHang.Shared`: de dung chung model nhu `HangHoa`, `PhieuNhap`, `PhieuXuat`.

## Cach chay test

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

## Ghi chu tung file

| File | Chuc nang |
| --- | --- |
| `QuanLyKhoHang.Tests.csproj` | Cau hinh project test xUnit, package test SDK va reference den API/Shared. |
| `ServiceValidationTests.cs` | Kiem tra cac rule validate o service ma khong can database that. |
| `JwtTokenServiceTests.cs` | Kiem tra JWT tao ra co hop le va token sai bi tu choi. |
| `.gitignore` | Bo qua file build/test/local nhu `bin/`, `obj/`, `TestResults/`, coverage, `.vs/`, `*.user`, `*.log`. |
| `README.md` | Tai lieu giai thich muc dich, cach chay va noi dung test. |

## Ghi chu tung test trong `ServiceValidationTests.cs`

| Ham test | Kiem tra gi |
| --- | --- |
| `AuthService_ShouldRejectEmptyLoginRequest` | Khi username/password rong, `AuthService.CheckLogin` phai nem loi. Test nay giup tranh viec form login gui request thieu du lieu. |
| `HangHoaService_ShouldRejectMissingName` | Khi them hang hoa ma thieu ten hang, `HangHoaService.Them` phai bao loi `tenHangHoa`. |
| `HangHoaService_ShouldRejectNegativeStock` | Khi so luong ton nho hon 0, service phai chan lai. Day la rule quan trong vi ton kho khong duoc am ngay tu dau vao. |
| `PhieuNhapService_ShouldRejectEmptyDetails` | Khi luu phieu nhap khong co dong chi tiet hang, service phai bao loi. Phieu nhap khong co hang la chung tu khong hop le. |
| `PhieuXuatService_ShouldRejectEmptyDetails` | Khi luu phieu xuat khong co dong chi tiet hang, service phai bao loi. Phieu xuat khong co hang la chung tu khong hop le. |

## Ghi chu tung test trong `JwtTokenServiceTests.cs`

| Ham test | Kiem tra gi |
| --- | --- |
| `CreateToken_ShouldReturnTokenThatCanBeValidated` | Goi `JwtTokenService.CreateToken`, sau do dung `IsValid` de dam bao token vua tao ra hop le. Test cung kiem tra token khong rong va han het han nam trong tuong lai. |
| `IsValid_ShouldRejectInvalidToken` | Truyen token sai dinh dang vao `JwtTokenService.IsValid`, ket qua phai la `false`. Test nay dam bao API khong chap nhan token rac. |
| `CreateService` | Ham helper tao `JwtTokenService` voi cau hinh test rieng. Ham nay giup cac test khong phu thuoc vao `appsettings.json`. |

## Nhung test hien co bao ve phan nao?

- Login thieu username/password.
- Them hang hoa thieu ten.
- Them hang hoa co ton kho am.
- Luu phieu nhap khong co chi tiet.
- Luu phieu xuat khong co chi tiet.
- JWT tao ra phai validate duoc.
- JWT sai dinh dang phai bi tu choi.

## Nhung test nen bo sung tiep

Hien tai project nay chu yeu la unit test. Cac test sau can PostgreSQL test database rieng, nen nen bo sung thanh integration test:

- Login dung voi tai khoan `admin / 123456`.
- Login sai mat khau.
- Them hang hoa thanh cong.
- Sua hang hoa thanh cong.
- Xoa hang hoa khi chua co chung tu.
- Khong cho xoa hang hoa da co phieu nhap/xuat.
- Nhap kho phai cong ton.
- Xuat kho du ton phai tru ton.
- Xuat kho thieu ton phai bi chan.
- Xoa nhan vien da co chung tu phai bi chan.

## Nguyen tac viet test moi

- Ten test nen noi ro hanh vi can kiem tra.
- Moi test chi nen kiem tra mot rule chinh.
- Khong dung database that trong unit test.
- Neu test can database, tao nhom integration test rieng va dung database test rieng.
- Khong de test phu thuoc vao thu tu chay.
- Nen test ca truong hop dung va sai cho nghiep vu quan trong.
