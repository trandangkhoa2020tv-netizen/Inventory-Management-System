using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
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

                WebApplication app = builder.Build();
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
            app.MapGet("/api/hang-hoa", () => Safe(() => OkTable(service.GetHangHoa())));
            app.MapPost("/api/hang-hoa", (HangHoa input) => Safe(() => ExecuteCreate(service.ThemHangHoa(input))));
            app.MapPut("/api/hang-hoa/{id:int}", (int id, HangHoa input) => Safe(() => ExecuteUpdate(service.SuaHangHoa(id, input))));
            app.MapDelete("/api/hang-hoa/{id:int}", (int id) => Safe(() => ExecuteDelete(service.XoaHangHoa(id))));

            // Nhóm API loại hàng: phục vụ phân loại hàng hóa.
            app.MapGet("/api/loai-hang", () => Safe(() => OkTable(service.GetLoaiHang())));
            app.MapPost("/api/loai-hang", (LoaiHang input) => Safe(() => ExecuteCreate(service.ThemLoaiHang(input))));
            app.MapPut("/api/loai-hang/{id:int}", (int id, LoaiHang input) => Safe(() => ExecuteUpdate(service.SuaLoaiHang(id, input))));
            app.MapDelete("/api/loai-hang/{id:int}", (int id) => Safe(() => ExecuteDelete(service.XoaLoaiHang(id))));

            // Nhóm API nhà cung cấp: phục vụ nghiệp vụ nhập kho.
            app.MapGet("/api/nha-cung-cap", () => Safe(() => OkTable(service.GetNhaCungCap())));
            app.MapPost("/api/nha-cung-cap", (NhaCungCap input) => Safe(() => ExecuteCreate(service.ThemNhaCungCap(input))));
            app.MapPut("/api/nha-cung-cap/{id:int}", (int id, NhaCungCap input) => Safe(() => ExecuteUpdate(service.SuaNhaCungCap(id, input))));
            app.MapDelete("/api/nha-cung-cap/{id:int}", (int id) => Safe(() => ExecuteDelete(service.XoaNhaCungCap(id))));

            // Nhóm API khách hàng: phục vụ nghiệp vụ xuất kho/bán hàng.
            app.MapGet("/api/khach-hang", () => Safe(() => OkTable(service.GetKhachHang())));
            app.MapPost("/api/khach-hang", (KhachHang input) => Safe(() => ExecuteCreate(service.ThemKhachHang(input))));
            app.MapPut("/api/khach-hang/{id:int}", (int id, KhachHang input) => Safe(() => ExecuteUpdate(service.SuaKhachHang(id, input))));
            app.MapDelete("/api/khach-hang/{id:int}", (int id) => Safe(() => ExecuteDelete(service.XoaKhachHang(id))));

            // Nhóm API nhân viên: phục vụ quản lý người lập phiếu và phân quyền.
            app.MapGet("/api/nhan-vien", () => Safe(() => OkTable(service.GetNhanVien())));
            app.MapPost("/api/nhan-vien", (NhanVien input) => Safe(() => ExecuteCreate(service.ThemNhanVien(input))));
            app.MapPut("/api/nhan-vien/{id:int}", (int id, NhanVien input) => Safe(() => ExecuteUpdate(service.SuaNhanVien(id, input))));
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
                return Results.Problem(title: "Loi xu ly API", detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
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
        private sealed class ApiSettings
        {
            public bool Enabled { get; private set; } = true;
            public string Url { get; private set; } = "http://localhost:5088";

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
