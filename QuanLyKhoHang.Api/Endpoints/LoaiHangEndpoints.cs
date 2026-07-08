using QuanLyKhoHang.ApiServer.Services;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.ApiServer.Endpoints;

/// <summary>
/// Nhóm endpoint CRUD loại hàng.
/// </summary>
public static class LoaiHangEndpoints
{
    /// <summary>
    /// Đăng ký route thêm, sửa, xóa, lấy danh sách loại hàng.
    /// </summary>
    public static IEndpointRouteBuilder MapLoaiHangEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/loai-hang", (ILoaiHangService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetAll())));

        app.MapGet("/api/v2/loai-hang", (ILoaiHangService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetDtos())));

        app.MapPost("/api/loai-hang", (LoaiHang input, ILoaiHangService service) =>
            ApiResults.Safe(() => ApiResults.Created(service.Them(input))));

        app.MapPut("/api/loai-hang/{id:int}", (int id, LoaiHang input, ILoaiHangService service) =>
            ApiResults.Safe(() => ApiResults.Updated(service.Sua(id, input))));

        app.MapDelete("/api/loai-hang/{id:int}", (int id, ILoaiHangService service) =>
            ApiResults.Safe(() => ApiResults.Deleted(service.Xoa(id))));

        return app;
    }
}
