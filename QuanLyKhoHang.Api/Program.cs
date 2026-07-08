using Microsoft.AspNetCore.Http.Json;
using QuanLyKhoHang.ApiServer;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Đọc cấu hình URL, CORS và API key cho backend.
ApiSettings apiSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>() ?? new ApiSettings();
builder.WebHost.UseUrls(apiSettings.Url);
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("ApiCors", policy =>
    {
        if (apiSettings.AllowsAnyOrigin())
        {
            policy.AllowAnyOrigin();
        }
        else
        {
            policy.WithOrigins(apiSettings.AllowedOrigins);
        }

        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

// Đăng ký repository và service truy vấn để minimal API inject vào từng endpoint.
builder.Services.AddScoped<HangHoaRepository>();
builder.Services.AddScoped<LoaiHangRepository>();
builder.Services.AddScoped<NhaCungCapRepository>();
builder.Services.AddScoped<KhachHangRepository>();
builder.Services.AddScoped<NhanVienRepository>();
builder.Services.AddScoped<PhieuNhapRepository>();
builder.Services.AddScoped<PhieuXuatRepository>();
builder.Services.AddScoped<TaiKhoanRepository>();
builder.Services.AddScoped<InventoryApiQueries>();

WebApplication app = builder.Build();

// Đồng bộ sequence tự tăng khi API khởi động để dữ liệu import sẵn không làm trùng khóa.
try
{
    DatabaseMaintenance.EnsureSerialSequences();
}
catch (Exception ex)
{
    Console.Error.WriteLine("Khong the dong bo sequence database khi khoi dong API: " + ex.Message);
}

app.UseCors("ApiCors");

// Route gốc chuyển về health check để kiểm tra nhanh API đang chạy.
app.MapGet("/", () => Results.Redirect("/api/health"));

if (apiSettings.RequireApiKey)
{
    // Middleware kiểm tra API key trên header X-API-Key hoặc Authorization Bearer.
    app.Use(async (context, next) =>
    {
        if (!HasValidApiKey(context.Request, apiSettings.ApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { message = "Khong co quyen truy cap API." });
            return;
        }

        await next();
    });
}

// Endpoint kiểm tra trạng thái API và kết nối database.
app.MapGet("/api/health", () => Safe(() => Results.Ok(new
{
    status = "running",
    database = DbConnection.TestConnection() ? "connected" : "disconnected",
    time = DateTime.Now
})));

// Endpoint mô tả ngắn các nhóm chức năng mà API cung cấp.
app.MapGet("/api/chuc-nang", () => Safe(() => Results.Ok(new[]
{
    new { nhom = "Danh muc", moTa = "CRUD hang hoa, loai hang, nha cung cap, khach hang, nhan vien." },
    new { nhom = "Kho", moTa = "Tra cuu ton kho va canh bao hang ton thap." },
    new { nhom = "Nhap kho", moTa = "Tao phieu nhap, cong ton kho, tra cuu lich su va chi tiet." },
    new { nhom = "Xuat kho", moTa = "Tao phieu xuat, tru ton kho, tra cuu lich su va chi tiet." },
    new { nhom = "Dang nhap", moTa = "Xac thuc tai khoan va tra ve vai tro nguoi dung." }
})));

// Endpoint tài liệu nhanh để người dùng biết các route đang có.
app.MapGet("/api/docs", () => Safe(() => Results.Ok(new
{
    basePath = "/api",
    auth = "Neu ApiSettings.RequireApiKey = true, gui header X-API-Key hoac Authorization: Bearer <key>.",
    endpoints = new[]
    {
        "GET /api/health",
        "GET /api/chuc-nang",
        "GET /api/docs",
        "POST /api/auth/login",
        "GET,POST /api/hang-hoa",
        "PUT,DELETE /api/hang-hoa/{id}",
        "GET,POST /api/loai-hang",
        "PUT,DELETE /api/loai-hang/{id}",
        "GET,POST /api/nha-cung-cap",
        "PUT,DELETE /api/nha-cung-cap/{id}",
        "GET,POST /api/khach-hang",
        "PUT,DELETE /api/khach-hang/{id}",
        "GET,POST /api/nhan-vien",
        "PUT,DELETE /api/nhan-vien/{id}",
        "GET /api/ton-kho/thap?soLuongToiDa=10",
        "GET,POST /api/phieu-nhap",
        "GET /api/phieu-nhap/{id}/chi-tiet",
        "GET,POST /api/phieu-xuat",
        "GET /api/phieu-xuat/{id}/chi-tiet",
        "GET /api/phieu-xuat/{id}/thong-tin"
    }
})));

// Endpoint đăng nhập: xác thực tài khoản và trả về vai trò để WinForms phân quyền.
app.MapPost("/api/auth/login", (LoginRequest input, TaiKhoanRepository repo) => Safe(() =>
{
    if (input == null || string.IsNullOrWhiteSpace(input.Username) || string.IsNullOrWhiteSpace(input.Password))
    {
        return Results.BadRequest(new { message = "Vui long nhap ten tai khoan va mat khau." });
    }

    string vaiTro = repo.CheckLogin(input.Username.Trim(), input.Password);
    return string.IsNullOrEmpty(vaiTro)
        ? Results.Unauthorized()
        : Results.Ok(new { tenTaiKhoan = input.Username.Trim(), vaiTro });
}));

// Nhóm endpoint CRUD danh mục hàng hóa.
app.MapGet("/api/hang-hoa", (HangHoaRepository repo) => Safe(() => OkTable(repo.GetAll())));
app.MapPost("/api/hang-hoa", (HangHoa input, HangHoaRepository repo) => Safe(() => WithValidation(ValidateHangHoa(input), () => ExecuteCreate(repo.Them(input)))));
app.MapPut("/api/hang-hoa/{id:int}", (int id, HangHoa input, HangHoaRepository repo) => Safe(() =>
{
    input.MaHangHoa = id;
    return WithValidation(ValidateHangHoa(input), () => ExecuteUpdate(repo.Sua(input)));
}));
app.MapDelete("/api/hang-hoa/{id:int}", (int id, HangHoaRepository repo) => Safe(() => ExecuteDelete(repo.Xoa(id))));

// Nhóm endpoint CRUD danh mục loại hàng.
app.MapGet("/api/loai-hang", (LoaiHangRepository repo) => Safe(() => OkTable(repo.GetAll())));
app.MapPost("/api/loai-hang", (LoaiHang input, LoaiHangRepository repo) => Safe(() => WithValidation(ValidateLoaiHang(input), () => ExecuteCreate(repo.Them(input)))));
app.MapPut("/api/loai-hang/{id:int}", (int id, LoaiHang input, LoaiHangRepository repo) => Safe(() =>
{
    input.MaLoaiHang = id;
    return WithValidation(ValidateLoaiHang(input), () => ExecuteUpdate(repo.Sua(input)));
}));
app.MapDelete("/api/loai-hang/{id:int}", (int id, LoaiHangRepository repo) => Safe(() => ExecuteDelete(repo.Xoa(id))));

// Nhóm endpoint CRUD danh mục nhà cung cấp.
app.MapGet("/api/nha-cung-cap", (NhaCungCapRepository repo) => Safe(() => OkTable(repo.GetAll())));
app.MapPost("/api/nha-cung-cap", (NhaCungCap input, NhaCungCapRepository repo) => Safe(() => WithValidation(ValidateNhaCungCap(input), () => ExecuteCreate(repo.Them(input)))));
app.MapPut("/api/nha-cung-cap/{id:int}", (int id, NhaCungCap input, NhaCungCapRepository repo) => Safe(() =>
{
    input.MaNhaCungCap = id;
    return WithValidation(ValidateNhaCungCap(input), () => ExecuteUpdate(repo.Sua(input)));
}));
app.MapDelete("/api/nha-cung-cap/{id:int}", (int id, NhaCungCapRepository repo) => Safe(() => ExecuteDelete(repo.Xoa(id))));

// Nhóm endpoint CRUD danh mục khách hàng.
app.MapGet("/api/khach-hang", (KhachHangRepository repo) => Safe(() => OkTable(repo.GetAll())));
app.MapPost("/api/khach-hang", (KhachHang input, KhachHangRepository repo) => Safe(() => WithValidation(ValidateKhachHang(input), () => ExecuteCreate(repo.Them(input)))));
app.MapPut("/api/khach-hang/{id:int}", (int id, KhachHang input, KhachHangRepository repo) => Safe(() =>
{
    input.MaKhachHang = id;
    return WithValidation(ValidateKhachHang(input), () => ExecuteUpdate(repo.Sua(input)));
}));
app.MapDelete("/api/khach-hang/{id:int}", (int id, KhachHangRepository repo) => Safe(() => ExecuteDelete(repo.Xoa(id))));

// Nhóm endpoint CRUD danh mục nhân viên.
app.MapGet("/api/nhan-vien", (NhanVienRepository repo) => Safe(() => OkTable(repo.GetAll())));
app.MapPost("/api/nhan-vien", (NhanVien input, NhanVienRepository repo) => Safe(() => WithValidation(ValidateNhanVien(input), () => ExecuteCreate(repo.Them(input)))));
app.MapPut("/api/nhan-vien/{id:int}", (int id, NhanVien input, NhanVienRepository repo) => Safe(() =>
{
    input.MaNhanVien = id;
    return WithValidation(ValidateNhanVien(input), () => ExecuteUpdate(repo.Sua(input)));
}));
app.MapDelete("/api/nhan-vien/{id:int}", (int id, NhanVienRepository repo) => Safe(() => ExecuteDelete(repo.Xoa(id))));

// Endpoint cảnh báo hàng tồn kho thấp theo ngưỡng người dùng truyền vào.
app.MapGet("/api/ton-kho/thap", (int? soLuongToiDa, InventoryApiQueries queries) => Safe(() => OkTable(queries.GetTonKhoThap(soLuongToiDa ?? 10))));

// Nhóm endpoint nghiệp vụ nhập kho: xem lịch sử, xem chi tiết và lưu phiếu nhập.
app.MapGet("/api/phieu-nhap", (PhieuNhapRepository repo) => Safe(() => OkTable(repo.GetAllPhieuNhap())));
app.MapGet("/api/phieu-nhap/{id:int}/chi-tiet", (int id, PhieuNhapRepository repo) => Safe(() => OkTable(repo.GetChiTietTheoMaPhieu(id))));
app.MapPost("/api/phieu-nhap", (LuuPhieuNhapRequest input, PhieuNhapRepository repo) => Safe(() =>
{
    List<string> errors = ValidatePhieuNhap(input);
    return WithValidation(errors, () =>
    {
        int maPhieu = repo.LuuPhieuNhap(input.PhieuNhap, input.ChiTietList);
        return Results.Ok(new { message = "Da luu phieu nhap va cap nhat ton kho.", maPhieu });
    });
}));

// Nhóm endpoint nghiệp vụ xuất kho: xem lịch sử, xem chi tiết, lấy thông tin in phiếu và lưu phiếu xuất.
app.MapGet("/api/phieu-xuat", (PhieuXuatRepository repo) => Safe(() => OkTable(repo.GetAllPhieuXuat())));
app.MapGet("/api/phieu-xuat/{id:int}/chi-tiet", (int id, PhieuXuatRepository repo) => Safe(() => OkTable(repo.GetChiTietTheoMaPhieu(id))));
app.MapGet("/api/phieu-xuat/{id:int}/thong-tin", (int id, PhieuXuatRepository repo) => Safe(() => OkTable(repo.GetThongTinPhieuXuat(id))));
app.MapPost("/api/phieu-xuat", (LuuPhieuXuatRequest input, PhieuXuatRepository repo) => Safe(() =>
{
    List<string> errors = ValidatePhieuXuat(input);
    return WithValidation(errors, () =>
    {
        int maPhieu = repo.LuuPhieuXuat(input.PhieuXuat, input.ChiTietList);
        return Results.Ok(new { message = "Da luu phieu xuat va cap nhat ton kho.", maPhieu });
    });
}));

// Nếu API được chạy riêng, tự mở giao diện desktop sau khi backend sẵn sàng.
app.Lifetime.ApplicationStarted.Register(StartDesktopClientIfNeeded);

app.Run();

// Bọc xử lý endpoint để trả lỗi JSON thống nhất thay vì làm API dừng đột ngột.
static IResult Safe(Func<IResult> action)
{
    try
    {
        return action();
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        return Results.Problem(title: "Loi xu ly API", detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
    }
}

// Chuyển DataTable từ repository thành JSON object list cho client.
static IResult OkTable(System.Data.DataTable table) => Results.Ok(DataTableJson.ToRows(table));

// Trả kết quả chuẩn cho thao tác thêm mới.
static IResult ExecuteCreate(int affectedRows) => Results.Ok(new { message = "Da them du lieu.", affectedRows });

// Trả kết quả chuẩn cho thao tác cập nhật, bao gồm trường hợp không tìm thấy dữ liệu.
static IResult ExecuteUpdate(int affectedRows)
{
    return affectedRows == 0
        ? Results.NotFound(new { message = "Khong tim thay du lieu can cap nhat." })
        : Results.Ok(new { message = "Da cap nhat du lieu.", affectedRows });
}

// Trả kết quả chuẩn cho thao tác xóa, bao gồm trường hợp không tìm thấy dữ liệu.
static IResult ExecuteDelete(int affectedRows)
{
    return affectedRows == 0
        ? Results.NotFound(new { message = "Khong tim thay du lieu can xoa." })
        : Results.Ok(new { message = "Da xoa du lieu.", affectedRows });
}

// Nếu có lỗi validation thì trả BadRequest, nếu không thì chạy thao tác chính.
static IResult WithValidation(List<string> errors, Func<IResult> action)
{
    return errors.Count > 0
        ? Results.BadRequest(new { message = "Du lieu khong hop le.", errors })
        : action();
}

// Kiểm tra dữ liệu hàng hóa trước khi thêm hoặc cập nhật.
static List<string> ValidateHangHoa(HangHoa input)
{
    List<string> errors = new List<string>();
    if (input == null)
    {
        errors.Add("Body JSON khong duoc de trong.");
        return errors;
    }

    RequireText(errors, input.TenHangHoa, "tenHangHoa", 255);
    RequirePositive(errors, input.MaLoaiHang, "maLoaiHang");
    RequirePositive(errors, input.MaNhaCungCap, "maNhaCungCap");
    RequireNonNegativeDecimal(errors, input.GiaNhap, "giaNhap");
    RequireNonNegativeDecimal(errors, input.GiaBan, "giaBan");
    RequireNonNegative(errors, input.SoLuongTon, "soLuongTon");
    OptionalMaxLength(errors, input.DonViTinh, "donViTinh", 50);
    return errors;
}

// Kiểm tra dữ liệu loại hàng trước khi thêm hoặc cập nhật.
static List<string> ValidateLoaiHang(LoaiHang input)
{
    List<string> errors = new List<string>();
    if (input == null)
    {
        errors.Add("Body JSON khong duoc de trong.");
        return errors;
    }

    RequireText(errors, input.TenLoaiHang, "tenLoaiHang", 100);
    return errors;
}

// Kiểm tra dữ liệu nhà cung cấp trước khi thêm hoặc cập nhật.
static List<string> ValidateNhaCungCap(NhaCungCap input)
{
    List<string> errors = new List<string>();
    if (input == null)
    {
        errors.Add("Body JSON khong duoc de trong.");
        return errors;
    }

    RequireText(errors, input.TenNhaCungCap, "tenNhaCungCap", 255);
    OptionalMaxLength(errors, input.DiaChiNCC, "diaChiNCC", 500);
    OptionalMaxLength(errors, input.SoDienThoai, "soDienThoai", 20);
    OptionalEmail(errors, input.Email, "email", 100);
    return errors;
}

// Kiểm tra dữ liệu khách hàng trước khi thêm hoặc cập nhật.
static List<string> ValidateKhachHang(KhachHang input)
{
    List<string> errors = new List<string>();
    if (input == null)
    {
        errors.Add("Body JSON khong duoc de trong.");
        return errors;
    }

    RequireText(errors, input.TenKhachHang, "tenKhachHang", 255);
    OptionalMaxLength(errors, input.DiaChiKH, "diaChiKH", 500);
    OptionalMaxLength(errors, input.SoDienThoai, "soDienThoai", 20);
    OptionalEmail(errors, input.Email, "email", 100);
    return errors;
}

// Kiểm tra dữ liệu nhân viên trước khi thêm hoặc cập nhật.
static List<string> ValidateNhanVien(NhanVien input)
{
    List<string> errors = new List<string>();
    if (input == null)
    {
        errors.Add("Body JSON khong duoc de trong.");
        return errors;
    }

    RequireText(errors, input.TenNhanVien, "tenNhanVien", 255);
    OptionalMaxLength(errors, input.DiaChiNV, "diaChiNV", 500);
    OptionalMaxLength(errors, input.SoDienThoai, "soDienThoai", 20);
    OptionalEmail(errors, input.Email, "email", 100);
    OptionalMaxLength(errors, input.ChucVu, "chucVu", 100);
    return errors;
}

// Kiểm tra phiếu nhập phải có thông tin phiếu, nhà cung cấp, nhân viên và ít nhất một dòng chi tiết.
static List<string> ValidatePhieuNhap(LuuPhieuNhapRequest input)
{
    List<string> errors = new List<string>();
    if (input?.PhieuNhap == null)
    {
        errors.Add("Thong tin phieu nhap khong duoc de trong.");
        return errors;
    }

    RequirePositive(errors, input.PhieuNhap.MaNhaCungCap, "maNhaCungCap");
    RequirePositive(errors, input.PhieuNhap.MaNhanVien, "maNhanVien");
    ValidateChiTietNhapXuat(errors, input.ChiTietList);
    return errors;
}

// Kiểm tra phiếu xuất phải có thông tin phiếu, khách hàng, nhân viên và ít nhất một dòng chi tiết.
static List<string> ValidatePhieuXuat(LuuPhieuXuatRequest input)
{
    List<string> errors = new List<string>();
    if (input?.PhieuXuat == null)
    {
        errors.Add("Thong tin phieu xuat khong duoc de trong.");
        return errors;
    }

    RequirePositive(errors, input.PhieuXuat.MaKhachHang, "maKhachHang");
    RequirePositive(errors, input.PhieuXuat.MaNhanVien, "maNhanVien");
    ValidateChiTietNhapXuat(errors, input.ChiTietList);
    return errors;
}

// Kiểm tra danh sách chi tiết nhập/xuất không được rỗng.
static void ValidateChiTietNhapXuat<T>(List<string> errors, List<T> chiTietList)
{
    if (chiTietList == null || chiTietList.Count == 0)
    {
        errors.Add("Phieu phai co it nhat mot mat hang.");
    }
}

// Bắt buộc chuỗi phải có nội dung và không vượt quá độ dài tối đa.
static void RequireText(List<string> errors, string value, string fieldName, int maxLength)
{
    if (string.IsNullOrWhiteSpace(value))
    {
        errors.Add(fieldName + " khong duoc de trong.");
        return;
    }

    OptionalMaxLength(errors, value, fieldName, maxLength);
}

// Kiểm tra độ dài tối đa cho trường không bắt buộc.
static void OptionalMaxLength(List<string> errors, string value, string fieldName, int maxLength)
{
    if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
    {
        errors.Add(fieldName + " khong duoc vuot qua " + maxLength + " ky tu.");
    }
}

// Kiểm tra email không bắt buộc: nếu có nhập thì phải đúng dạng cơ bản và không quá dài.
static void OptionalEmail(List<string> errors, string value, string fieldName, int maxLength)
{
    OptionalMaxLength(errors, value, fieldName, maxLength);
    if (!string.IsNullOrWhiteSpace(value) && (!value.Contains('@') || value.StartsWith("@") || value.EndsWith("@")))
    {
        errors.Add(fieldName + " khong dung dinh dang email.");
    }
}

// Bắt buộc giá trị số nguyên phải lớn hơn 0, thường dùng cho khóa ngoại.
static void RequirePositive(List<string> errors, int value, string fieldName)
{
    if (value <= 0)
    {
        errors.Add(fieldName + " phai lon hon 0.");
    }
}

// Bắt buộc giá trị số nguyên không được âm.
static void RequireNonNegative(List<string> errors, int value, string fieldName)
{
    if (value < 0)
    {
        errors.Add(fieldName + " khong duoc am.");
    }
}

// Bắt buộc giá trị tiền tệ/số thập phân không được âm.
static void RequireNonNegativeDecimal(List<string> errors, decimal value, string fieldName)
{
    if (value < 0)
    {
        errors.Add(fieldName + " khong duoc am.");
    }
}

// Kiểm tra API key bằng so sánh constant-time để giảm rủi ro lộ key qua timing.
static bool HasValidApiKey(HttpRequest request, string configuredApiKey)
{
    if (string.IsNullOrWhiteSpace(configuredApiKey))
    {
        return false;
    }

    string providedApiKey = null;
    if (request.Headers.TryGetValue("X-API-Key", out var apiKeyValues))
    {
        providedApiKey = apiKeyValues.ToString();
    }

    if (string.IsNullOrWhiteSpace(providedApiKey) && request.Headers.TryGetValue("Authorization", out var authValues))
    {
        string authorization = authValues.ToString();
        const string bearerPrefix = "Bearer ";
        if (authorization.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
        {
            providedApiKey = authorization.Substring(bearerPrefix.Length).Trim();
        }
    }

    if (string.IsNullOrWhiteSpace(providedApiKey))
    {
        return false;
    }

    byte[] expectedBytes = Encoding.UTF8.GetBytes(configuredApiKey);
    byte[] providedBytes = Encoding.UTF8.GetBytes(providedApiKey);
    return expectedBytes.Length == providedBytes.Length && CryptographicOperations.FixedTimeEquals(expectedBytes, providedBytes);
}

// Tự mở ứng dụng WinForms nếu API được chạy trực tiếp thay vì do desktop khởi động.
static void StartDesktopClientIfNeeded()
{
    if (Environment.GetEnvironmentVariable("QUANLYKHOHANG_STARTED_BY_DESKTOP") == "1")
    {
        return;
    }

    string desktopExecutable = FindDesktopExecutable();
    if (string.IsNullOrEmpty(desktopExecutable))
    {
        return;
    }

    try
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = desktopExecutable,
            WorkingDirectory = Path.GetDirectoryName(desktopExecutable) ?? AppContext.BaseDirectory,
            UseShellExecute = true
        });
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine("Khong the mo giao dien QuanLyKhoHang: " + ex.Message);
    }
}

// Tìm file exe của ứng dụng WinForms trong các thư mục build Debug/Release.
static string FindDesktopExecutable()
{
    DirectoryInfo directory = new DirectoryInfo(AppContext.BaseDirectory);
    while (directory != null)
    {
        string desktopExecutable = Path.Combine(
            directory.FullName,
            "QuanLyKhoHang",
            "bin",
            "Debug",
            "net10.0-windows",
            "QuanLyKhoHang.exe");

        if (File.Exists(desktopExecutable))
        {
            return desktopExecutable;
        }

        desktopExecutable = Path.Combine(
            directory.FullName,
            "QuanLyKhoHang",
            "bin",
            "Release",
            "net10.0-windows",
            "QuanLyKhoHang.exe");

        if (File.Exists(desktopExecutable))
        {
            return desktopExecutable;
        }

        directory = directory.Parent;
    }

    return string.Empty;
}

/// <summary>
/// Cấu hình chung của API: URL lắng nghe, API key và danh sách origin được phép gọi CORS.
/// </summary>
public sealed class ApiSettings
{
    /// <summary>URL backend dùng để lắng nghe request.</summary>
    public string Url { get; set; } = "http://localhost:5088";

    /// <summary>Bật/tắt yêu cầu API key cho các request.</summary>
    public bool RequireApiKey { get; set; }

    /// <summary>Khóa API hợp lệ khi RequireApiKey được bật.</summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>Danh sách origin được phép gọi API; rỗng hoặc "*" nghĩa là cho phép tất cả.</summary>
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Kiểm tra cấu hình CORS có đang cho phép mọi origin hay không.
    /// </summary>
    public bool AllowsAnyOrigin()
    {
        return AllowedOrigins == null || AllowedOrigins.Length == 0 || AllowedOrigins.Contains("*");
    }
}

/// <summary>
/// Dữ liệu body gửi lên endpoint đăng nhập.
/// </summary>
public sealed class LoginRequest
{
    /// <summary>Tên tài khoản đăng nhập.</summary>
    public string Username { get; set; }

    /// <summary>Mật khẩu đăng nhập.</summary>
    public string Password { get; set; }
}

/// <summary>
/// Dữ liệu body khi lưu phiếu nhập kèm danh sách chi tiết hàng nhập.
/// </summary>
public sealed class LuuPhieuNhapRequest
{
    /// <summary>Thông tin phiếu nhập chính.</summary>
    public PhieuNhap PhieuNhap { get; set; }

    /// <summary>Danh sách mặt hàng trong phiếu nhập.</summary>
    public List<ChiTietPhieuNhap> ChiTietList { get; set; } = new List<ChiTietPhieuNhap>();
}

/// <summary>
/// Dữ liệu body khi lưu phiếu xuất kèm danh sách chi tiết hàng xuất.
/// </summary>
public sealed class LuuPhieuXuatRequest
{
    /// <summary>Thông tin phiếu xuất chính.</summary>
    public PhieuXuat PhieuXuat { get; set; }

    /// <summary>Danh sách mặt hàng trong phiếu xuất.</summary>
    public List<ChiTietPhieuXuat> ChiTietList { get; set; } = new List<ChiTietPhieuXuat>();
}
