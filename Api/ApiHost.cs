using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Api
{
    /// <summary>
    /// Host API HTTP chạy kèm ứng dụng WinForms.
    /// API này dùng Minimal API của ASP.NET Core để cung cấp dữ liệu JSON cho Postman, web/mobile app,
    /// hoặc các hệ thống khác cần tích hợp với phần mềm quản lý kho.
    /// </summary>
    public sealed class ApiHost : IDisposable
    {
        private readonly WebApplication _app;

        /// <summary>
        /// Constructor private để buộc bên ngoài khởi động qua StartIfEnabled().
        /// </summary>
        private ApiHost(WebApplication app)
        {
            _app = app;
        }

        /// <summary>
        /// Đọc ApiSettings trong appsettings.json. Nếu Enabled = true thì khởi động API tại Url cấu hình.
        /// Nếu API lỗi khi bật, chương trình WinForms vẫn chạy bình thường.
        /// </summary>
        public static ApiHost StartIfEnabled()
        {
            ApiSettings settings = ApiSettings.Load();
            if (!settings.Enabled)
            {
                return null;
            }

            try
            {
                WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions
                {
                    Args = Array.Empty<string>(),
                    ContentRootPath = AppContext.BaseDirectory
                });

                builder.WebHost.UseUrls(settings.Url);
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("ApiCors", policy =>
                    {
                        if (AllowsAnyOrigin(settings.AllowedOrigins))
                        {
                            policy.AllowAnyOrigin();
                        }
                        else
                        {
                            policy.WithOrigins(settings.AllowedOrigins);
                        }

                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                    });
                });

                WebApplication app = builder.Build();
                app.UseCors("ApiCors");

                if (settings.RequireApiKey)
                {
                    app.Use(async (context, next) =>
                    {
                        if (!HasValidApiKey(context.Request, settings.ApiKey))
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await context.Response.WriteAsJsonAsync(new { message = "Khong co quyen truy cap API." });
                            return;
                        }

                        await next();
                    });
                }

                MapEndpoints(app);
                app.StartAsync().GetAwaiter().GetResult();

                return new ApiHost(app);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Khong the khoi dong API: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Dừng web server khi ứng dụng WinForms đóng.
        /// </summary>
        public void Dispose()
        {
            _app.StopAsync().GetAwaiter().GetResult();
            _app.DisposeAsync().AsTask().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Khai báo toàn bộ route API.
        /// Các route danh mục hỗ trợ CRUD, route phiếu chỉ tra cứu lịch sử và chi tiết.
        /// </summary>
        private static void MapEndpoints(IEndpointRouteBuilder app)
        {
            InventoryApiService service = new InventoryApiService();

            // Kiểm tra API đang chạy và database có kết nối được không.
            app.MapGet("/api/health", () => Safe(() => Results.Ok(new
            {
                status = "running",
                database = service.TestDatabase() ? "connected" : "disconnected",
                time = DateTime.Now
            })));

            // Trả về danh sách nhóm chức năng để người test API biết hệ thống đang có gì.
            app.MapGet("/api/chuc-nang", () => Safe(() => Results.Ok(new[]
            {
                new { nhom = "Danh muc", moTa = "CRUD hang hoa, loai hang, nha cung cap, khach hang, nhan vien." },
                new { nhom = "Kho", moTa = "Tra cuu ton kho, canh bao hang ton thap." },
                new { nhom = "Nhap kho", moTa = "Tra cuu phieu nhap va chi tiet phieu nhap." },
                new { nhom = "Xuat kho", moTa = "Tra cuu phieu xuat va chi tiet phieu xuat." },
                new { nhom = "Bao cao", moTa = "WinForms ho tro xuat Excel/PDF tu cac phieu da luu." }
            })));

            // Nhóm API hàng hóa: xem, thêm, sửa, xóa.
            app.MapGet("/api/docs", () => Safe(() => Results.Ok(new
            {
                basePath = "/api",
                auth = "Neu ApiSettings.RequireApiKey = true, gui header X-API-Key hoac Authorization: Bearer <key>.",
                endpoints = new[]
                {
                    "GET /api/health",
                    "GET /api/chuc-nang",
                    "GET /api/docs",
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
                    "GET /api/phieu-nhap",
                    "GET /api/phieu-nhap/{id}/chi-tiet",
                    "GET /api/phieu-xuat",
                    "GET /api/phieu-xuat/{id}/chi-tiet"
                }
            })));

            app.MapGet("/api/hang-hoa", () => Safe(() => OkTable(service.GetHangHoa())));
            app.MapPost("/api/hang-hoa", (HangHoa input) => Safe(() => WithValidation(ValidateHangHoa(input), () => ExecuteCreate(service.ThemHangHoa(input)))));
            app.MapPut("/api/hang-hoa/{id:int}", (int id, HangHoa input) => Safe(() => WithValidation(ValidateHangHoa(input), () => ExecuteUpdate(service.SuaHangHoa(id, input)))));
            app.MapDelete("/api/hang-hoa/{id:int}", (int id) => Safe(() => ExecuteDelete(service.XoaHangHoa(id))));

            // Nhóm API loại hàng: phục vụ phân loại hàng hóa.
            app.MapGet("/api/loai-hang", () => Safe(() => OkTable(service.GetLoaiHang())));
            app.MapPost("/api/loai-hang", (LoaiHang input) => Safe(() => WithValidation(ValidateLoaiHang(input), () => ExecuteCreate(service.ThemLoaiHang(input)))));
            app.MapPut("/api/loai-hang/{id:int}", (int id, LoaiHang input) => Safe(() => WithValidation(ValidateLoaiHang(input), () => ExecuteUpdate(service.SuaLoaiHang(id, input)))));
            app.MapDelete("/api/loai-hang/{id:int}", (int id) => Safe(() => ExecuteDelete(service.XoaLoaiHang(id))));

            // Nhóm API nhà cung cấp: phục vụ nghiệp vụ nhập kho.
            app.MapGet("/api/nha-cung-cap", () => Safe(() => OkTable(service.GetNhaCungCap())));
            app.MapPost("/api/nha-cung-cap", (NhaCungCap input) => Safe(() => WithValidation(ValidateNhaCungCap(input), () => ExecuteCreate(service.ThemNhaCungCap(input)))));
            app.MapPut("/api/nha-cung-cap/{id:int}", (int id, NhaCungCap input) => Safe(() => WithValidation(ValidateNhaCungCap(input), () => ExecuteUpdate(service.SuaNhaCungCap(id, input)))));
            app.MapDelete("/api/nha-cung-cap/{id:int}", (int id) => Safe(() => ExecuteDelete(service.XoaNhaCungCap(id))));

            // Nhóm API khách hàng: phục vụ nghiệp vụ xuất kho/bán hàng.
            app.MapGet("/api/khach-hang", () => Safe(() => OkTable(service.GetKhachHang())));
            app.MapPost("/api/khach-hang", (KhachHang input) => Safe(() => WithValidation(ValidateKhachHang(input), () => ExecuteCreate(service.ThemKhachHang(input)))));
            app.MapPut("/api/khach-hang/{id:int}", (int id, KhachHang input) => Safe(() => WithValidation(ValidateKhachHang(input), () => ExecuteUpdate(service.SuaKhachHang(id, input)))));
            app.MapDelete("/api/khach-hang/{id:int}", (int id) => Safe(() => ExecuteDelete(service.XoaKhachHang(id))));

            // Nhóm API nhân viên: phục vụ quản lý người lập phiếu và phân quyền.
            app.MapGet("/api/nhan-vien", () => Safe(() => OkTable(service.GetNhanVien())));
            app.MapPost("/api/nhan-vien", (NhanVien input) => Safe(() => WithValidation(ValidateNhanVien(input), () => ExecuteCreate(service.ThemNhanVien(input)))));
            app.MapPut("/api/nhan-vien/{id:int}", (int id, NhanVien input) => Safe(() => WithValidation(ValidateNhanVien(input), () => ExecuteUpdate(service.SuaNhanVien(id, input)))));
            app.MapDelete("/api/nhan-vien/{id:int}", (int id) => Safe(() => ExecuteDelete(service.XoaNhanVien(id))));

            // Tra cứu các mặt hàng có tồn kho thấp hơn ngưỡng truyền vào.
            app.MapGet("/api/ton-kho/thap", (int? soLuongToiDa) => Safe(() => OkTable(service.GetTonKhoThap(soLuongToiDa ?? 10))));

            // Tra cứu chứng từ nhập/xuất và chi tiết chứng từ.
            app.MapGet("/api/phieu-nhap", () => Safe(() => OkTable(service.GetPhieuNhap())));
            app.MapGet("/api/phieu-nhap/{id:int}/chi-tiet", (int id) => Safe(() => OkTable(service.GetChiTietPhieuNhap(id))));
            app.MapGet("/api/phieu-xuat", () => Safe(() => OkTable(service.GetPhieuXuat())));
            app.MapGet("/api/phieu-xuat/{id:int}/chi-tiet", (int id) => Safe(() => OkTable(service.GetChiTietPhieuXuat(id))));
        }

        /// <summary>
        /// Bọc xử lý lỗi chung cho mọi endpoint để API trả JSON lỗi thay vì làm sập server.
        /// </summary>
        private static IResult Safe(Func<IResult> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Loi xu ly API: " + ex);
                return Results.Problem(title: "Loi xu ly API", detail: "Vui long kiem tra log ung dung.", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Chuẩn hóa DataTable thành HTTP 200 JSON array.
        /// </summary>
        private static IResult OkTable(System.Data.DataTable table)
        {
            return Results.Ok(DataTableJson.ToRows(table));
        }

        /// <summary>
        /// Trả kết quả sau khi thêm dữ liệu.
        /// </summary>
        private static IResult ExecuteCreate(int affectedRows)
        {
            return Results.Ok(new { message = "Da them du lieu.", affectedRows });
        }

        /// <summary>
        /// Trả kết quả sau khi cập nhật; nếu không có dòng nào bị ảnh hưởng thì trả 404.
        /// </summary>
        private static IResult ExecuteUpdate(int affectedRows)
        {
            return affectedRows == 0
                ? Results.NotFound(new { message = "Khong tim thay du lieu can cap nhat." })
                : Results.Ok(new { message = "Da cap nhat du lieu.", affectedRows });
        }

        /// <summary>
        /// Trả kết quả sau khi xóa; nếu không có dòng nào bị ảnh hưởng thì trả 404.
        /// </summary>
        private static IResult ExecuteDelete(int affectedRows)
        {
            return affectedRows == 0
                ? Results.NotFound(new { message = "Khong tim thay du lieu can xoa." })
                : Results.Ok(new { message = "Da xoa du lieu.", affectedRows });
        }

        /// <summary>
        /// Cấu hình API đọc từ Config/appsettings.json.
        /// Enabled tắt/mở API, Url là địa chỉ lắng nghe mặc định.
        /// </summary>
        private static IResult WithValidation(List<string> errors, Func<IResult> action)
        {
            return errors.Count > 0
                ? Results.BadRequest(new { message = "Du lieu khong hop le.", errors })
                : action();
        }

        private static List<string> ValidateHangHoa(HangHoa input)
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
            RequireNonNegative(errors, input.GiaNhap, "giaNhap");
            RequireNonNegative(errors, input.GiaBan, "giaBan");
            RequireNonNegative(errors, input.SoLuongTon, "soLuongTon");
            OptionalMaxLength(errors, input.DonViTinh, "donViTinh", 50);
            return errors;
        }

        private static List<string> ValidateLoaiHang(LoaiHang input)
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

        private static List<string> ValidateNhaCungCap(NhaCungCap input)
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

        private static List<string> ValidateKhachHang(KhachHang input)
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

        private static List<string> ValidateNhanVien(NhanVien input)
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

        private static void RequireText(List<string> errors, string value, string fieldName, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                errors.Add(fieldName + " khong duoc de trong.");
                return;
            }

            OptionalMaxLength(errors, value, fieldName, maxLength);
        }

        private static void OptionalMaxLength(List<string> errors, string value, string fieldName, int maxLength)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
            {
                errors.Add(fieldName + " khong duoc vuot qua " + maxLength + " ky tu.");
            }
        }

        private static void OptionalEmail(List<string> errors, string value, string fieldName, int maxLength)
        {
            OptionalMaxLength(errors, value, fieldName, maxLength);
            if (!string.IsNullOrWhiteSpace(value) && (!value.Contains("@") || value.StartsWith("@") || value.EndsWith("@")))
            {
                errors.Add(fieldName + " khong dung dinh dang email.");
            }
        }

        private static void RequirePositive(List<string> errors, int value, string fieldName)
        {
            if (value <= 0)
            {
                errors.Add(fieldName + " phai lon hon 0.");
            }
        }

        private static void RequireNonNegative(List<string> errors, int value, string fieldName)
        {
            if (value < 0)
            {
                errors.Add(fieldName + " khong duoc am.");
            }
        }

        private static void RequireNonNegative(List<string> errors, decimal value, string fieldName)
        {
            if (value < 0)
            {
                errors.Add(fieldName + " khong duoc am.");
            }
        }

        private static bool HasValidApiKey(HttpRequest request, string configuredApiKey)
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

        private static bool AllowsAnyOrigin(string[] allowedOrigins)
        {
            if (allowedOrigins == null || allowedOrigins.Length == 0)
            {
                return true;
            }

            foreach (string origin in allowedOrigins)
            {
                if (origin == "*")
                {
                    return true;
                }
            }

            return false;
        }

        private sealed class ApiSettings
        {
            public bool Enabled { get; private set; } = true;
            public string Url { get; private set; } = "http://localhost:5088";
            public bool RequireApiKey { get; private set; } = false;
            public string ApiKey { get; private set; } = string.Empty;
            public string[] AllowedOrigins { get; private set; } = Array.Empty<string>();

            /// <summary>
            /// Đọc cấu hình API, nếu thiếu file hoặc thiếu key thì dùng giá trị mặc định.
            /// </summary>
            public static ApiSettings Load()
            {
                ApiSettings settings = new ApiSettings();

                try
                {
                    string configPath = Path.Combine(AppContext.BaseDirectory, "Config", "appsettings.json");
                    if (!File.Exists(configPath))
                    {
                        return settings;
                    }

                    using FileStream stream = File.OpenRead(configPath);
                    using JsonDocument document = JsonDocument.Parse(stream);

                    if (!document.RootElement.TryGetProperty("ApiSettings", out JsonElement api))
                    {
                        return settings;
                    }

                    if (api.TryGetProperty("Enabled", out JsonElement enabled) && enabled.ValueKind == JsonValueKind.False)
                    {
                        settings.Enabled = false;
                    }

                    if (api.TryGetProperty("Url", out JsonElement url))
                    {
                        settings.Url = url.GetString() ?? settings.Url;
                    }

                    if (api.TryGetProperty("RequireApiKey", out JsonElement requireApiKey) && requireApiKey.ValueKind == JsonValueKind.True)
                    {
                        settings.RequireApiKey = true;
                    }

                    if (api.TryGetProperty("ApiKey", out JsonElement apiKey))
                    {
                        settings.ApiKey = apiKey.GetString() ?? settings.ApiKey;
                    }

                    if (api.TryGetProperty("AllowedOrigins", out JsonElement allowedOrigins) && allowedOrigins.ValueKind == JsonValueKind.Array)
                    {
                        List<string> origins = new List<string>();
                        foreach (JsonElement origin in allowedOrigins.EnumerateArray())
                        {
                            string value = origin.GetString();
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                origins.Add(value);
                            }
                        }

                        settings.AllowedOrigins = origins.ToArray();
                    }
                }
                catch
                {
                    return settings;
                }

                return settings;
            }
        }
    }
}
