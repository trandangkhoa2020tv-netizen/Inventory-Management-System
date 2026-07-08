using Microsoft.AspNetCore.Http.Json;
using QuanLyKhoHang.ApiServer;
using QuanLyKhoHang.ApiServer.Config;
using QuanLyKhoHang.ApiServer.Endpoints;
using QuanLyKhoHang.ApiServer.Services;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Repositories;

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Đọc cấu hình API từ appsettings.json và đặt URL backend sẽ lắng nghe.
ApiSettings apiSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>() ?? new ApiSettings();
JwtSettings jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();
builder.WebHost.UseUrls(apiSettings.Url);

    // Cho phép JSON request không phân biệt chữ hoa/thường ở tên property.
    builder.Services.Configure<JsonOptions>(options =>
    {
        options.SerializerOptions.PropertyNameCaseInsensitive = true;
    });

    // Bật Swagger/OpenAPI để test và xem tài liệu API trực quan tại /swagger.
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Cấu hình CORS để client được phép gọi API theo danh sách AllowedOrigins.
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

    // Đăng ký repository: tầng thao tác trực tiếp với database.
    builder.Services.AddScoped<HangHoaRepository>();
    builder.Services.AddScoped<LoaiHangRepository>();
    builder.Services.AddScoped<NhaCungCapRepository>();
    builder.Services.AddScoped<KhachHangRepository>();
    builder.Services.AddScoped<NhanVienRepository>();
    builder.Services.AddScoped<PhieuNhapRepository>();
    builder.Services.AddScoped<PhieuXuatRepository>();
    builder.Services.AddScoped<TaiKhoanRepository>();
    builder.Services.AddScoped<InventoryApiQueries>();

    // Đăng ký service: tầng xử lý nghiệp vụ và validate trước khi gọi repository.
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IHangHoaService, HangHoaService>();
    builder.Services.AddScoped<ILoaiHangService, LoaiHangService>();
    builder.Services.AddScoped<INhaCungCapService, NhaCungCapService>();
    builder.Services.AddScoped<IKhachHangService, KhachHangService>();
    builder.Services.AddScoped<INhanVienService, NhanVienService>();
    builder.Services.AddScoped<IKhoService, KhoService>();
builder.Services.AddScoped<IPhieuNhapService, PhieuNhapService>();
builder.Services.AddScoped<IPhieuXuatService, PhieuXuatService>();
builder.Services.AddSingleton(jwtSettings);
builder.Services.AddSingleton<JwtTokenService>();

    WebApplication app = builder.Build();

    // Đồng bộ sequence tự tăng để dữ liệu import sẵn không làm trùng khóa chính.
    try
    {
        DatabaseMaintenance.EnsureSerialSequences();
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine("Khong the dong bo sequence database khi khoi dong API: " + ex.Message);
    }

    app.UseCors("ApiCors");
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "QuanLyKhoHang API v1");
        options.RoutePrefix = "swagger";
    });

    // Nếu bật RequireApiKey, mọi request phải gửi key hợp lệ qua X-API-Key hoặc Authorization Bearer.
if (apiSettings.RequireApiKey)
{
        app.Use(async (context, next) =>
        {
            if (!ApiKeyValidator.HasValidApiKey(context.Request, apiSettings.ApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Khong co quyen truy cap API." });
                return;
            }

            await next();
    });
}

// Nếu bật JwtSettings.RequireJwt, các endpoint nghiệp vụ phải gửi Authorization: Bearer <token>.
if (jwtSettings.RequireJwt)
{
    app.Use(async (context, next) =>
    {
        if (!JwtTokenService.IsPublicPath(context.Request.Path))
        {
            JwtTokenService tokenService = context.RequestServices.GetRequiredService<JwtTokenService>();
            string token = JwtTokenService.GetBearerToken(context.Request);
            if (!tokenService.IsValid(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Token dang nhap khong hop le hoac da het han." });
                return;
            }
        }

        await next();
    });
}

// Đăng ký các nhóm route API theo từng chức năng.
    app.MapSystemEndpoints();
    app.MapAuthEndpoints();
    app.MapHangHoaEndpoints();
    app.MapLoaiHangEndpoints();
    app.MapNhaCungCapEndpoints();
    app.MapKhachHangEndpoints();
    app.MapNhanVienEndpoints();
    app.MapKhoEndpoints();
    app.MapPhieuNhapEndpoints();
    app.MapPhieuXuatEndpoints();

    // Nếu chạy API trực tiếp, tự mở WinForms để người dùng thấy giao diện desktop.
    app.Lifetime.ApplicationStarted.Register(DesktopClientLauncher.StartIfNeeded);

    app.Run();
