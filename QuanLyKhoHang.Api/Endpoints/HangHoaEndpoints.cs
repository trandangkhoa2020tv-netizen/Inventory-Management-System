using QuanLyKhoHang.ApiServer.Services;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.ApiServer.Endpoints;

/// <summary>
/// Nhóm endpoint CRUD hàng hóa.
/// </summary>
public static class HangHoaEndpoints
{
    /// <summary>
    /// Đăng ký route thêm, sửa, xóa, lấy danh sách hàng hóa.
    /// </summary>
    public static IEndpointRouteBuilder MapHangHoaEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/hang-hoa", (IHangHoaService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetAll())));

        app.MapGet("/api/v2/hang-hoa", (IHangHoaService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetDtos())));

        app.MapPost("/api/hang-hoa", (HangHoa input, IHangHoaService service) =>
            ApiResults.Safe(() => ApiResults.Created(service.Them(input))));

        app.MapPut("/api/hang-hoa/{id:int}", (int id, HangHoa input, IHangHoaService service) =>
            ApiResults.Safe(() => ApiResults.Updated(service.Sua(id, input))));

        app.MapDelete("/api/hang-hoa/{id:int}", (int id, IHangHoaService service) =>
            ApiResults.Safe(() => ApiResults.Deleted(service.Xoa(id))));

        return app;
    }
}
