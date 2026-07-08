using QuanLyKhoHang.ApiServer.Services;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.ApiServer.Endpoints;

/// <summary>
/// Nhóm endpoint CRUD nhà cung cấp.
/// </summary>
public static class NhaCungCapEndpoints
{
    /// <summary>
    /// Đăng ký route thêm, sửa, xóa, lấy danh sách nhà cung cấp.
    /// </summary>
    public static IEndpointRouteBuilder MapNhaCungCapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/nha-cung-cap", (INhaCungCapService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetAll())));

        app.MapGet("/api/v2/nha-cung-cap", (INhaCungCapService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetDtos())));

        app.MapPost("/api/nha-cung-cap", (NhaCungCap input, INhaCungCapService service) =>
            ApiResults.Safe(() => ApiResults.Created(service.Them(input))));

        app.MapPut("/api/nha-cung-cap/{id:int}", (int id, NhaCungCap input, INhaCungCapService service) =>
            ApiResults.Safe(() => ApiResults.Updated(service.Sua(id, input))));

        app.MapDelete("/api/nha-cung-cap/{id:int}", (int id, INhaCungCapService service) =>
            ApiResults.Safe(() => ApiResults.Deleted(service.Xoa(id))));

        return app;
    }
}
