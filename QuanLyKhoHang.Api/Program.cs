using Microsoft.AspNetCore.Http.Json;
using QuanLyKhoHang.ApiServer;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;
using System.Security.Cryptography;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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

try
{
    DatabaseMaintenance.EnsureSerialSequences();
}
catch (Exception ex)
{
    Console.Error.WriteLine("Khong the dong bo sequence database khi khoi dong API: " + ex.Message);
}

app.UseCors("ApiCors");

if (apiSettings.RequireApiKey)
{
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

app.MapGet("/api/health", () => Safe(() => Results.Ok(new
{
    status = "running",
    database = DbConnection.TestConnection() ? "connected" : "disconnected",
    time = DateTime.Now
})));

app.MapGet("/api/chuc-nang", () => Safe(() => Results.Ok(new[]
{
    new { nhom = "Danh muc", moTa = "CRUD hang hoa, loai hang, nha cung cap, khach hang, nhan vien." },
    new { nhom = "Kho", moTa = "Tra cuu ton kho va canh bao hang ton thap." },
    new { nhom = "Nhap kho", moTa = "Tao phieu nhap, cong ton kho, tra cuu lich su va chi tiet." },
    new { nhom = "Xuat kho", moTa = "Tao phieu xuat, tru ton kho, tra cuu lich su va chi tiet." },
    new { nhom = "Dang nhap", moTa = "Xac thuc tai khoan va tra ve vai tro nguoi dung." }
})));

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

app.MapGet("/api/hang-hoa", (HangHoaRepository repo) => Safe(() => OkTable(repo.GetAll())));
app.MapPost("/api/hang-hoa", (HangHoa input, HangHoaRepository repo) => Safe(() => WithValidation(ValidateHangHoa(input), () => ExecuteCreate(repo.Them(input)))));
app.MapPut("/api/hang-hoa/{id:int}", (int id, HangHoa input, HangHoaRepository repo) => Safe(() =>
{
    input.MaHangHoa = id;
    return WithValidation(ValidateHangHoa(input), () => ExecuteUpdate(repo.Sua(input)));
}));
app.MapDelete("/api/hang-hoa/{id:int}", (int id, HangHoaRepository repo) => Safe(() => ExecuteDelete(repo.Xoa(id))));

app.MapGet("/api/loai-hang", (LoaiHangRepository repo) => Safe(() => OkTable(repo.GetAll())));
app.MapPost("/api/loai-hang", (LoaiHang input, LoaiHangRepository repo) => Safe(() => WithValidation(ValidateLoaiHang(input), () => ExecuteCreate(repo.Them(input)))));
app.MapPut("/api/loai-hang/{id:int}", (int id, LoaiHang input, LoaiHangRepository repo) => Safe(() =>
{
    input.MaLoaiHang = id;
    return WithValidation(ValidateLoaiHang(input), () => ExecuteUpdate(repo.Sua(input)));
}));
app.MapDelete("/api/loai-hang/{id:int}", (int id, LoaiHangRepository repo) => Safe(() => ExecuteDelete(repo.Xoa(id))));

app.MapGet("/api/nha-cung-cap", (NhaCungCapRepository repo) => Safe(() => OkTable(repo.GetAll())));
app.MapPost("/api/nha-cung-cap", (NhaCungCap input, NhaCungCapRepository repo) => Safe(() => WithValidation(ValidateNhaCungCap(input), () => ExecuteCreate(repo.Them(input)))));
app.MapPut("/api/nha-cung-cap/{id:int}", (int id, NhaCungCap input, NhaCungCapRepository repo) => Safe(() =>
{
    input.MaNhaCungCap = id;
    return WithValidation(ValidateNhaCungCap(input), () => ExecuteUpdate(repo.Sua(input)));
}));
app.MapDelete("/api/nha-cung-cap/{id:int}", (int id, NhaCungCapRepository repo) => Safe(() => ExecuteDelete(repo.Xoa(id))));

app.MapGet("/api/khach-hang", (KhachHangRepository repo) => Safe(() => OkTable(repo.GetAll())));
app.MapPost("/api/khach-hang", (KhachHang input, KhachHangRepository repo) => Safe(() => WithValidation(ValidateKhachHang(input), () => ExecuteCreate(repo.Them(input)))));
app.MapPut("/api/khach-hang/{id:int}", (int id, KhachHang input, KhachHangRepository repo) => Safe(() =>
{
    input.MaKhachHang = id;
    return WithValidation(ValidateKhachHang(input), () => ExecuteUpdate(repo.Sua(input)));
}));
app.MapDelete("/api/khach-hang/{id:int}", (int id, KhachHangRepository repo) => Safe(() => ExecuteDelete(repo.Xoa(id))));

app.MapGet("/api/nhan-vien", (NhanVienRepository repo) => Safe(() => OkTable(repo.GetAll())));
app.MapPost("/api/nhan-vien", (NhanVien input, NhanVienRepository repo) => Safe(() => WithValidation(ValidateNhanVien(input), () => ExecuteCreate(repo.Them(input)))));
app.MapPut("/api/nhan-vien/{id:int}", (int id, NhanVien input, NhanVienRepository repo) => Safe(() =>
{
    input.MaNhanVien = id;
    return WithValidation(ValidateNhanVien(input), () => ExecuteUpdate(repo.Sua(input)));
}));
app.MapDelete("/api/nhan-vien/{id:int}", (int id, NhanVienRepository repo) => Safe(() => ExecuteDelete(repo.Xoa(id))));

app.MapGet("/api/ton-kho/thap", (int? soLuongToiDa, InventoryApiQueries queries) => Safe(() => OkTable(queries.GetTonKhoThap(soLuongToiDa ?? 10))));

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

app.Run();

static IResult Safe(Func<IResult> action)
{
    try
    {
        return action();
    }
    catch (Exception ex)
    {
        return Results.Problem(title: "Loi xu ly API", detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
    }
}

static IResult OkTable(System.Data.DataTable table) => Results.Ok(DataTableJson.ToRows(table));

static IResult ExecuteCreate(int affectedRows) => Results.Ok(new { message = "Da them du lieu.", affectedRows });

static IResult ExecuteUpdate(int affectedRows)
{
    return affectedRows == 0
        ? Results.NotFound(new { message = "Khong tim thay du lieu can cap nhat." })
        : Results.Ok(new { message = "Da cap nhat du lieu.", affectedRows });
}

static IResult ExecuteDelete(int affectedRows)
{
    return affectedRows == 0
        ? Results.NotFound(new { message = "Khong tim thay du lieu can xoa." })
        : Results.Ok(new { message = "Da xoa du lieu.", affectedRows });
}

static IResult WithValidation(List<string> errors, Func<IResult> action)
{
    return errors.Count > 0
        ? Results.BadRequest(new { message = "Du lieu khong hop le.", errors })
        : action();
}

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

static void ValidateChiTietNhapXuat<T>(List<string> errors, List<T> chiTietList)
{
    if (chiTietList == null || chiTietList.Count == 0)
    {
        errors.Add("Phieu phai co it nhat mot mat hang.");
    }
}

static void RequireText(List<string> errors, string value, string fieldName, int maxLength)
{
    if (string.IsNullOrWhiteSpace(value))
    {
        errors.Add(fieldName + " khong duoc de trong.");
        return;
    }

    OptionalMaxLength(errors, value, fieldName, maxLength);
}

static void OptionalMaxLength(List<string> errors, string value, string fieldName, int maxLength)
{
    if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
    {
        errors.Add(fieldName + " khong duoc vuot qua " + maxLength + " ky tu.");
    }
}

static void OptionalEmail(List<string> errors, string value, string fieldName, int maxLength)
{
    OptionalMaxLength(errors, value, fieldName, maxLength);
    if (!string.IsNullOrWhiteSpace(value) && (!value.Contains('@') || value.StartsWith("@") || value.EndsWith("@")))
    {
        errors.Add(fieldName + " khong dung dinh dang email.");
    }
}

static void RequirePositive(List<string> errors, int value, string fieldName)
{
    if (value <= 0)
    {
        errors.Add(fieldName + " phai lon hon 0.");
    }
}

static void RequireNonNegative(List<string> errors, int value, string fieldName)
{
    if (value < 0)
    {
        errors.Add(fieldName + " khong duoc am.");
    }
}

static void RequireNonNegativeDecimal(List<string> errors, decimal value, string fieldName)
{
    if (value < 0)
    {
        errors.Add(fieldName + " khong duoc am.");
    }
}

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

public sealed class ApiSettings
{
    public string Url { get; set; } = "http://localhost:5088";
    public bool RequireApiKey { get; set; }
    public string ApiKey { get; set; } = string.Empty;
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();

    public bool AllowsAnyOrigin()
    {
        return AllowedOrigins == null || AllowedOrigins.Length == 0 || AllowedOrigins.Contains("*");
    }
}

public sealed class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public sealed class LuuPhieuNhapRequest
{
    public PhieuNhap PhieuNhap { get; set; }
    public List<ChiTietPhieuNhap> ChiTietList { get; set; } = new List<ChiTietPhieuNhap>();
}

public sealed class LuuPhieuXuatRequest
{
    public PhieuXuat PhieuXuat { get; set; }
    public List<ChiTietPhieuXuat> ChiTietList { get; set; } = new List<ChiTietPhieuXuat>();
}
