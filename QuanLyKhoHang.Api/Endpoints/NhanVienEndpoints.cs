using QuanLyKhoHang.ApiServer.Services;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.ApiServer.Endpoints;

/// <summary>
/// Nhóm endpoint CRUD nhân viên.
/// </summary>
public static class NhanVienEndpoints
{
    /// <summary>
    /// Đăng ký route thêm, sửa, xóa, lấy danh sách nhân viên.
    /// </summary>
    public static IEndpointRouteBuilder MapNhanVienEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/nhan-vien", (INhanVienService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetAll())));

        app.MapGet("/api/v2/nhan-vien", (INhanVienService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetDtos())));

        app.MapPost("/api/nhan-vien", (NhanVien input, INhanVienService service) =>
            ApiResults.Safe(() => ApiResults.Created(service.Them(input))));

        app.MapPut("/api/nhan-vien/{id:int}", (int id, NhanVien input, INhanVienService service) =>
            ApiResults.Safe(() => ApiResults.Updated(service.Sua(id, input))));

        app.MapDelete("/api/nhan-vien/{id:int}", (int id, INhanVienService service) =>
            ApiResults.Safe(() => ApiResults.Deleted(service.Xoa(id))));

        return app;
    }
}
