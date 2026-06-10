# 📦 HỆ THỐNG QUẢN LÝ KHO HÀNG (WAREHOUSE MANAGEMENT SYSTEM - WMS)

Hệ thống phần mềm quản lý kho hàng (WMS) là một giải pháp ứng dụng máy tính toàn diện được thiết kế và triển khai bằng ngôn ngữ **C#** trên nền tảng **Windows Forms (.NET)**, kết hợp với hệ quản trị cơ sở dữ liệu quan hệ **PostgreSQL**. Phần mềm được xây dựng nhằm mục đích tối ưu hóa chuỗi cung ứng nội bộ, theo dõi chính xác biến động vật tư, quản lý định mức tồn kho theo thời gian thực, đồng thời cung cấp các công cụ kiểm soát nhân sự và trích xuất báo cáo báo biểu phục vụ công tác điều hành doanh nghiệp.

---

## 📌 MỤC LỤC

1. [Phân Tích Các Phân Hệ Nghiệp Vụ Cốt Lõi](https://www.google.com/search?q=%231-ph%C3%A2n-t%C3%ADch-c%C3%A1c-ph%C3%A2n-h%E1%BB%87-nghi%E1%BB%87p-v%E1%BB%A5-c%E1%BB%91t-l%C3%B5i)
2. [Cơ Chế Kiểm Tra Ràng Buộc & Bảo Mật 4 Lớp](https://www.google.com/search?q=%232-c%C6%A1-ch%E1%BA%BF-ki%E1%BB%83m-tra-r%C3%A0ng-bu%E1%BB%99c--b%E1%BA%A3o-m%E1%BA%ADt-4-l%E1%BB%9Bp)
3. [Kiến Trúc Phân Tầng Hệ Thống (Three-Tier Architecture)](https://www.google.com/search?q=%233-ki%E1%BA%BFn-tr%C3%BAc-ph%C3%A2n-t%E1%BA%A7ng-h%E1%BB%87-th%E1%BB%91ng-three-tier-architecture)
4. [Môi Trường Công Nghệ & Thư Viện Liên Kết](https://www.google.com/search?q=%234-m%C3%B4i-tr%C6%B0%E1%BB%9Dng-c%C3%B4ng-ngh%E1%BB%87--th%C6%B0-vi%E1%BB%87n-li%C3%AAn-k%E1%BA%BFt)
5. [Thiết Kế Cơ Sở Dữ Liệu & Chuẩn Hóa Dữ Liệu](https://www.google.com/search?q=%235-thi%E1%BA%BFt-k%E1%BA%BF-c%C6%A1-s%E1%BB%9F-d%E1%BB%AF-li%E1%BB%87u--chu%E1%BA%A9n-h%C3%B3a-d%E1%BB%AF-li%E1%BB%87u)
6. [Hướng Dẫn Cài Đặt & Triển Khai Chi Tiết](https://www.google.com/search?q=%236-h%C6%B0%E1%BB%9Bng-d%E1%BA%ABn-c%C3%A0i-%C4%91%E1%BA%B7t--tri%E1%BB%83n-khai-chi-ti%E1%BA%BFt)
7. [Quy Chuẩn Thiết Kế Giao Diện (UI/UX Flat Design)](https://www.google.com/search?q=%237-quy-chu%E1%BA%A9n-thi%E1%BA%BFt-k%E1%BA%BF-giao-di%E1%BB%87n-uiux-flat-design)
8. [Kế Hoạch Mở Rộng Hệ Thống (System Roadmap)](https://www.google.com/search?q=%238-k%E1%BA%BF-ho%E1%BA%A1ch-m%E1%BB%9F-r%E1%BB%99ng-h%E1%BB%87-th%E1%BB%91ng-system-roadmap)

---

## 1. PHÂN TÍCH CÁC PHÂN HỆ NGHIỆP VỤ CỐT LÕI

Hệ thống được Module hóa thành các phân hệ chức năng độc lập nhưng có mối liên kết chặt chẽ về mặt dữ liệu để xử lý trọn vẹn các tác vụ vận hành kho:

### A. Phân hệ Xác thực & Phân quyền Người dùng (Role-Based Access Control - RBAC)

* **Xác thực danh tính:** Kiểm tra thông tin cặp khóa `Tên tài khoản` và `Mật khẩu` đối chiếu trực tiếp với bảng `TaiKhoan` trong PostgreSQL. Mật khẩu được xử lý qua thuật toán băm để đảm bảo tính an toàn lưu trữ.
* **Phân quyền động:** Hệ thống nhận diện quyền hạn dựa trên cột `VaiTro` (ví dụ: `Admin`, `Thủ kho`, `Nhân viên kiểm kho`). Khi khởi tạo Form chính (`FrmMain`), lớp logic sẽ duyệt qua danh mục Menu và thiết lập thuộc tính `Visible` hoặc `Enabled` thành `True/False` tương ứng với quyền của tài khoản, ngăn chặn việc truy cập trái phép vào các vùng chức năng không thuộc phạm vi trách nhiệm.

### B. Phân hệ Quản lý Danh mục Nền (Master Data Management)

* **Danh mục Hàng hóa:** Quản lý thông tin mã hàng, tên hàng, đơn vị tính, giá nhập, giá bán và số lượng tồn hiện thời. Form tích hợp cơ chế kiểm soát dữ liệu Complete-Binding để hiển thị giá trị số theo định dạng phân cách hàng nghìn bằng dấu chấm (`.`) trực quan.
* **Danh mục Đối tác (Khách hàng & Nhà cung cấp):** Lưu trữ hồ sơ định danh, địa chỉ, số điện thoại liên lạc và địa chỉ thư điện tử nhằm phục vụ truy xuất thông tin lập hóa đơn và chứng từ xuất/nhập kho.

### C. Phân hệ Nghiệp vụ Nhập Kho (Inbound Management)

* **Lập phiếu nhập tạm:** Người dùng chọn Nhà cung cấp, Nhân viên lập phiếu, sau đó thêm danh sách các mặt hàng cùng số lượng và đơn giá nhập vào một bảng cục bộ (`DataTable` tạm thời hiển thị trên `dgvChiTiet`). Hệ thống tự động tính `Thành Tiền` từng dòng và cộng dồn vào `Tổng Tiền` của toàn bộ phiếu.
* **Đóng gói dữ liệu & Commit:** Khi nhấn nút "Lưu phiếu nhập", hệ thống thực hiện hai tác vụ đồng thời (Transaction): Tạo một bản ghi mới trong bảng `PhieuNhap` để lấy ra ID tự tăng (`MaPhieuNhap`), sau đó chạy vòng lặp duyệt qua bảng tạm để chèn hàng loạt bản ghi tương ứng vào bảng `ChiTietPhieuNhap`. Đồng thời, hệ thống kích hoạt logic tăng số lượng tồn (`SoLuongTon`) của các mặt hàng đó trong bảng `HangHoa`.

### D. Phân hệ Nghiệp vụ Xuất Kho (Outbound Management)

* **Kiểm tra điều kiện xuất:** Tương tự quy trình nhập kho nhưng bổ sung bước kiểm tra điều kiện nghiêm ngặt. Khi nhân viên thêm hàng vào phiếu xuất, hệ thống sẽ truy vấn số lượng tồn thực tế trong cơ sở dữ liệu. Nếu số lượng xuất vượt quá số lượng tồn kho hiện tại, phần mềm lập tức chặn thao tác và đưa ra cảnh báo lỗi để ngăn chặn tình trạng xuất âm kho.
* **Khấu trừ tồn kho:** Khi xác nhận lưu phiếu xuất, hệ thống thực hiện trừ trực tiếp số lượng vật tư tương ứng trong bảng `HangHoa`, ghi nhận doanh thu và cập nhật tức thì vào bảng lưới lịch sử phiếu xuất (`dgvLichSuPhieu`).

---

## 2. CƠ CHẾ KIỂM TRA RÀNG BUỘC & BẢO MẬT 4 LỚP

Hệ thống áp dụng kiến trúc bảo mật và kiểm tra dữ liệu qua 4 tầng phòng thủ độc lập để hạn chế tối đa sai sót từ người dùng và ngăn ngừa các nguy cơ tấn công dữ liệu:

```
[Người dùng] ──> [Lớp 1: Client-Side] ──> [Lớp 2: Trạng thái & Mã hóa] ──> [Lớp 3: Brute-Force] ──> [Lớp 4: Phiên Session] ──> [Database]

```

### Lớp 1: Kiểm tra tính hợp lệ dữ liệu tại Giao diện (Client-Side Validation)

* **Chuẩn hóa chuỗi nhập:** Sử dụng phương thức `.Trim()` đối với các ô dữ liệu dạng văn bản để tự động cắt bỏ các khoảng trắng vô nghĩa ở đầu và cuối chuỗi, đảm bảo tính chính xác khi so khớp dữ liệu.
* **Kiểm tra rỗng và kiểu dữ liệu:** Hệ thống thực hiện kiểm tra cấu trúc dữ liệu trước khi xử lý. Nếu các trường bắt buộc (như tài khoản, mật khẩu, số lượng, đơn giá) bị bỏ trống hoặc sai kiểu dữ liệu (gõ chữ vào ô số lượng), phần mềm sẽ kích hoạt thông báo cảnh báo `MessageBoxIcon.Warning` và ngắt tiến trình xử lý ngay tại local mà không gửi yêu cầu về server, giảm tải cho hệ thống mạng.
* **Giới hạn trường dữ liệu:** Cấu hình thuộc tính `MaxLength` cho các TextBox tương ứng với độ dài tối đa của trường dữ liệu trong Database để loại bỏ lỗi tràn bộ đệm hoặc lỗi cắt ngắn chuỗi khi thực thi câu lệnh SQL.

### Lớp 2: Xác thực danh tính và Kiểm tra trạng thái tài khoản

* **Mã hóa một chiều:** Hệ thống không lưu trữ mật khẩu dưới dạng văn bản thuần túy (Plain Text) mà sử dụng thuật toán băm (Hash) an toàn. Khi người dùng đăng nhập, mật khẩu nhập vào sẽ được băm và đem so khớp với chuỗi mã hóa trong bảng `TaiKhoan`.
* **Kiểm tra cờ trạng thái hoạt động:** Sau khi xác thực đúng tài khoản và mật khẩu, hệ thống tiến hành kiểm tra thuộc tính trạng thái (`TrangThai` dạng `BIT/BOOLEAN`). Nếu tài khoản đó có giá trị là `0` hoặc `False` (tài khoản bị khóa do nhân viên nghỉ việc hoặc vi phạm quy chế), hệ thống sẽ từ chối quyền truy cập và đưa ra thông báo: *"Tài Khoản đã bị vô hiệu hóa"*.

### Lớp 3: Cơ chế chống tấn công dò mật khẩu (Brute-Force Protection)

* Hệ thống tích hợp một bộ đếm số lần đăng nhập sai liên tiếp ngay trong phiên làm việc. Nếu người dùng nhập sai mật khẩu vượt quá số lần quy định (mặc định là 5 lần), hệ thống sẽ tự động tạm khóa chức năng đăng nhập của tài khoản đó hoặc yêu cầu nhập mã xác thực bổ sung để ngăn chặn các phần mềm dò mật khẩu tự động.

### Lớp 4: Quản lý phiên làm việc và Nhật ký hệ thống (Session Management)

* Khi đăng nhập thành công, một thực thể tĩnh (`Session.CurrentUser`) được khởi tạo và lưu giữ trong suốt vòng đời hoạt động của ứng dụng. Thực thể này lưu trữ các thông tin bao gồm: `MaNhanVien`, `TenTaiKhoan` và `VaiTro`.
* Mọi thao tác lập phiếu nhập, phiếu xuất hoặc chỉnh sửa danh mục hàng hóa sau đó đều sử dụng thông tin từ `Session` này để tự động điền vào trường người lập phiếu và ghi nhận nhật ký hệ thống, đảm bảo tính minh bạch (biết rõ ai là người thực hiện từng hành động cụ thể).

---

## 3. KIẾN TRÚC PHÂN TẦNG HỆ THỐNG (THREE-TIER ARCHITECTURE)

Dự án được tổ chức nghiêm ngặt theo mô hình kiến trúc 3 tầng độc lập, giúp tách biệt các phân đoạn mã nguồn, tăng khả năng bảo trì, nâng cấp và dễ dàng kiểm thử phần mềm:

### Lớp hiển thị (Presentation Tier - GUI)

* **Nhiệm vụ:** Là lớp giao diện người dùng trực quan, bao gồm các file thiết kế (`Designer.cs`) và file xử lý sự kiện đồ họa (`.cs`) như `FrmDangNhap`, `FrmMain`, `FrmHangHoa`, `FrmNhapKho`, `FrmXuatKho`.
* **Cơ chế:** Chỉ đảm nhận việc tiếp nhận các thao tác click chuột, nhập liệu của người dùng, hiển thị dữ liệu lên các bảng lưới (`DataGridView`), các ô lựa chọn (`ComboBox`) và chuyển tiếp dữ liệu thô xuống tầng bên dưới.

### Lớp xử lý nghiệp vụ (Business Logic Tier - BUS / BLL)

* **Nhiệm vụ:** Là bộ não của hệ thống, đóng vai trò trung gian điều hướng dữ liệu giữa giao diện và cơ sở dữ liệu.
* **Cơ chế:** Tiếp nhận dữ liệu thô từ GUI, áp dụng các quy tắc nghiệp vụ của kho hàng vào để tính toán (ví dụ: nhân số lượng với đơn giá để tính thành tiền, kiểm tra lượng tồn kho tối thiểu, tính toán tổng tiền sau thuế, kiểm tra logic ngày tháng của phiếu). Nếu dữ liệu thỏa mãn toàn bộ điều kiện nghiệp vụ, BLL mới gọi chức năng của tầng DAL để thực thi ghi dữ liệu.

### Lớp truy cập dữ liệu (Data Access Tier - DAL / Repository)

* **Nhiệm vụ:** Tầng tương tác trực tiếp với Hệ quản trị cơ sở dữ liệu PostgreSQL.
* **Cơ chế:** Chứa các lớp chức năng như `HangHoaRepository`, `PhieuNhapRepository`, `KhachHangRepository`. Tầng này sử dụng các thư viện kết nối dữ liệu để mở kết nối, khởi tạo câu lệnh SQL (`SELECT`, `INSERT`, `UPDATE`), nạp tham số qua `Parameters` (để chống SQL Injection) và trả kết quả về dưới dạng các đối tượng Model hoặc `DataTable` cho tầng BLL sử dụng.

---

## 4. MÔI TRƯỜNG CÔNG NGHỆ & THƯ VIỆN LIÊN KẾT

Dự án sử dụng bộ công nghệ tiêu chuẩn, có tính ổn định cao và phù hợp với mô hình quản lý vừa và nhỏ:

* **Nền tảng phát triển:** Microsoft .NET Framework / .NET Core (tùy thuộc vào phiên bản cài đặt của hệ thống).
* **Ngôn ngữ lập trình:** C# với các tính năng hướng đối tượng (OOP) mạnh mẽ.
* **Hệ quản trị cơ sở dữ liệu:** PostgreSQL (Hệ quản trị mã nguồn mở có hiệu năng xử lý các trường dữ liệu lớn và các kiểu dữ liệu tiền tệ `DECIMAL` chính xác).
* **Thư viện kết nối DB:** `Npgsql` - Thư viện driver chính thức để kết nối và thực thi các câu lệnh từ ứng dụng .NET tới PostgreSQL.
* **Thư viện hỗ trợ xuất bản báo cáo:**
* `EPPlus` hoặc `Microsoft.Office.Interop.Excel` phục vụ bóc tách dữ liệu và trích xuất báo cáo thống kê ra file Excel (`.xlsx`).
* `iTextSharp` hoặc `PdfSharp` hỗ trợ đóng gói thông tin hóa đơn và in ấn chứng từ ra file định dạng PDF (`.pdf`).



---

## 5. THIẾK KẾ CƠ SỞ DỮ LIỆU & CHUẨN HÓA DỮ LIỆU

Cơ sở dữ liệu của hệ thống được chuẩn hóa về dạng chuẩn 3NF để loại bỏ tình trạng dư thừa dữ liệu và đảm bảo tính toàn vẹn tham chiếu thông qua các khóa ngoại. Hệ thống bao gồm các thực thể chính sau:

```
 [NhaCungCap] ──< [PhieuNhap] ──< [ChiTietPhieuNhap] >── [HangHoa]
                                                           │
 [KhachHang]  ──< [PhieuXuat] ──< [ChiTietPhieuXuat] >─────┘
                                                           │
 [NhanVien]   ──< [TaiKhoan]                               └──> [LoaiHang]

```

* **Bảng Danh mục:** `LoaiHang`, `NhaCungCap`, `KhachHang`, `HangHoa`. Trong đó, bảng `HangHoa` liên kết với `LoaiHang` và `NhaCungCap` qua các khóa ngoại `MaLoaiHang` và `MaNhaCungCap`.
* **Bảng Nhân sự:** `NhanVien` và `TaiKhoan`. Một nhân viên có thể có một tài khoản hệ thống, liên kết qua khóa ngoại `MaNhanVien`.
* **Bảng Chứng từ và Chi tiết:**
* Phân hệ nhập bao gồm bảng `PhieuNhap` (Thông tin chung về ngày nhập, tổng tiền, người lập) và bảng `ChiTietPhieuNhap` (Mã hàng cụ thể, số lượng nhập thực tế, đơn giá nhập tại thời điểm đó).
* Phân hệ xuất tương tự bao gồm bảng `PhieuXuat` và bảng `ChiTietPhieuXuat`. Các bảng chi tiết sử dụng khóa ngoại kép tham chiếu trực tiếp đến bảng Phiếu chính và bảng Hàng Hóa.



---

## 6. HƯỚNG DẪN CÀI ĐẶT & TRIỂN KHAI CHI TIẾT

Để triển khai ứng dụng này trên môi trường phát triển cục bộ, bạn thực hiện theo các bước quy chuẩn sau:

### Bước 1: Khởi tạo Cơ sở dữ liệu trên PostgreSQL

1. Khởi động công cụ quản lý cơ sở dữ liệu **DBeaver** hoặc **pgAdmin**.
2. Tạo mới một Database trống với tên gọi: `QuanLyKhoHang`.
3. Mở một trình soạn thảo SQL mới (`SQL Editor`), sao chép toàn bộ nội dung script cấu trúc bảng (`CREATE TABLE`, `FOREIGN KEY`) và các đoạn dữ liệu mẫu (`INSERT INTO`) có sẵn trong tài liệu dự án, sau đó nhấn `Execute` để thiết lập cấu trúc nền tảng.

### Bước 2: Cấu hình Chuỗi kết nối trên Ứng dụng C#

1. Mở thư mục dự án và kích hoạt file giải pháp `.sln` bằng công cụ **Visual Studio**.
2. Tìm đến file cấu hình hệ thống **`App.config`** (hoặc file lớp hằng số chứa cấu hình kết nối trong tầng DAL).
3. Tìm thẻ `<connectionStrings>` và điều chỉnh lại các tham số `Host` (Server), `Port`, `User Id`, `Password` cho khớp với tài khoản quản trị PostgreSQL cài đặt trên máy của bạn:
```xml

```



### Bước 3: Cài đặt Thư viện phụ thuộc và Vận hành

1. Mở cửa sổ quản lý thư viện Nuget (`Nuget Package Manager`).
2. Xác minh hoặc tiến hành cài đặt các package bổ trợ bao gồm: `Npgsql` (Kết nối Postgres), `EPPlus` (Xuất Excel).
3. Tiến hành biên dịch hệ thống (`Build Solution` hoặc nhấn tổ hợp phím `Ctrl + Shift + B`).
4. Nhấn nút `Start` hoặc phím `F5` để chạy phần mềm. Giao diện Đăng nhập (`FrmDangNhap`) sẽ xuất hiện. Tiến hành nhập tài khoản mẫu (ví dụ: `levanc` / mật khẩu: `123456`) để vào hệ thống.

---

## 7. QUY CHUẨN THIẾT KẾ GIAO DIỆN (UI/UX FLAT DESIGN)

Giao diện của hệ thống được tái cấu trúc loại bỏ hoàn toàn các viền khối 3D đổ bóng gồ ghề của các ứng dụng Windows Forms thế hệ cũ, chuyển dịch sang ngôn ngữ thiết kế phẳng hiện đại (**Modern Flat UI**):

* **Tổ chức không gian linh hoạt (Layout Docking):** Các form nghiệp vụ phức tạp như Lập phiếu nhập/xuất kho được phân chia không gian rõ ràng qua hệ thống các Panel. Phân vùng nhập liệu điều khiển được bám đỉnh (`Dock = Top`), bảng lưới dữ liệu hiển thị lịch sử bám khít toàn bộ vùng trống (`Dock = Fill`), và thanh trạng thái hiển thị tổng tiền cùng nút tác vụ bám đáy (`Dock = Bottom`). Quy cách này giúp form tự động co giãn đều đặn khi phóng to, thu nhỏ hoặc thay đổi độ phân giải màn hình.
* **Bảng phối màu công nghệ (Color Palette):**
* Nền các vùng nhập liệu sử dụng màu xám khói nhẹ (`WhiteSmoke`) tạo cảm giác dễ chịu cho mắt.
* Tiêu đề chính sử dụng màu xanh đen Navy đậm (`FromArgb(44, 62, 80)`) tạo tính trang trọng, chuyên nghiệp.
* Các nhãn phụ trợ dùng màu xám tro nhạt (`#7F8C8D`) giúp người dùng tập trung vào các trường dữ liệu quan trọng hơn.


* **Định dạng dữ liệu số chuẩn hóa:** Toàn bộ dữ liệu liên quan đến tiền tệ, đơn giá và thành tiền trên các bảng lưới (`DataGridView`) đều được can thiệp qua sự kiện `DataBindingComplete` kết hợp đối tượng `CultureInfo("vi-VN")`. Cơ chế này ép buộc hệ thống luôn luôn hiển thị số tiền theo định dạng phân cách hàng nghìn bằng dấu chấm (`.`), căn lề phải (`MiddleRight`) chuẩn xác, giúp nhân viên kế toán kho đọc số liệu nhanh và tránh nhầm lẫn.

---

## 8. KẾ HOẠCH MỞ RỘNG HỆ THỐNG (SYSTEM ROADMAP)

Dự án hiện đã hoàn thiện các tính năng cốt lõi đáp ứng đầy đủ yêu cầu quản trị kho tiêu chuẩn. Trong các giai đoạn tiếp theo, hệ thống có kế hoạch tích hợp và nâng cấp các tính năng nâng cao sau:

* **Tích hợp phần cứng quét mã vạch (Barcode / QR Code):** Phát triển Module giải mã chuỗi ký tự nhận diện từ máy quét mã vạch cổng USB hoặc COM. Khi nhân viên xuất/nhập kho chỉ cần quét mã vạch trên bao bì sản phẩm, hệ thống sẽ tự động điền mã hàng, tăng/giảm số lượng vào phiếu tạm mà không cần chọn thủ công từ ComboBox, tăng tốc độ xử lý nghiệp vụ lên gấp nhiều lần.
* **Xây dựng Dashboard Biểu đồ Thống kê Trực quan:** Nhúng các thư viện đồ họa như `LiveCharts` hoặc `ZedGraph` vào Form chính nhằm tự động tính toán và hiển thị các biểu đồ cột, biểu đồ hình quạt biểu diễn: Tốc độ luân chuyển hàng hóa, danh sách top 5 mặt hàng tiêu thụ nhanh nhất, và sơ đồ trực quan hóa giá trị hàng tồn kho theo từng tháng.
* **Module Sao lưu Tự động (Auto Backup):** Phát triển một dịch vụ nền chạy ngầm (Background Worker) thực hiện gọi tiến trình `pg_dump` của PostgreSQL để tự động sao lưu dữ liệu dự phòng định kỳ vào một mốc giờ cố định hàng ngày, đảm bảo an toàn tuyệt đối dữ liệu kho hàng trước các sự cố phần cứng.

---

## *Tài liệu hướng dẫn kỹ thuật được biên soạn phục vụ công tác phát triển, kiểm thử và bàn giao hệ thống phần mềm Quản Lý Kho Hàng.*

