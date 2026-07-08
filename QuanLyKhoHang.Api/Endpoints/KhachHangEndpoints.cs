using QuanLyKhoHang.ApiServer.Services;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.ApiServer.Endpoints;

/// <summary>
/// Nhóm endpoint CRUD khách hàng.
/// </summary>
public static class KhachHangEndpoints
{
    /// <summary>
    /// Đăng ký route thêm, sửa, xóa, lấy danh sách khách hàng.
    /// </summary>
    public static IEndpointRouteBuilder MapKhachHangEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/khach-hang", (IKhachHangService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetAll())));

        app.MapGet("/api/v2/khach-hang", (IKhachHangService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetDtos())));

        app.MapPost("/api/khach-hang", (KhachHang input, IKhachHangService service) =>
            ApiResults.Safe(() => ApiResults.Created(service.Them(input))));

        app.MapPut("/api/khach-hang/{id:int}", (int id, KhachHang input, IKhachHangService service) =>
            ApiResults.Safe(() => ApiResults.Updated(service.Sua(id, input))));

        app.MapDelete("/api/khach-hang/{id:int}", (int id, IKhachHangService service) =>
            ApiResults.Safe(() => ApiResults.Deleted(service.Xoa(id))));

        return app;
    }
}
