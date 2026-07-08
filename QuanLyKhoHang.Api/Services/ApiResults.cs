using Microsoft.AspNetCore.Http;

namespace QuanLyKhoHang.ApiServer.Services;

/// <summary>
/// Lớp tiện ích chuẩn hóa response và xử lý lỗi chung cho các endpoint API.
/// </summary>
public static class ApiResults
{
    /// <summary>
    /// Bọc xử lý endpoint trong try/catch để API luôn trả lỗi dạng JSON thống nhất.
    /// </summary>
    public static IResult Safe(Func<IResult> action)
    {
        try
        {
            return action();
        }
        catch (ApiValidationException ex)
        {
            return Results.BadRequest(new { message = ex.Message, errors = ex.Errors });
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

    /// <summary>
    /// Chuyển DataTable từ repository thành danh sách object JSON và trả HTTP 200.
    /// </summary>
    public static IResult OkTable(System.Data.DataTable table)
    {
        return Results.Ok(DataTableJson.ToRows(table));
    }

    /// <summary>
    /// Trả kết quả chuẩn cho thao tác thêm mới dữ liệu.
    /// </summary>
    public static IResult Created(int affectedRows)
    {
        return Results.Ok(new { message = "Da them du lieu.", affectedRows });
    }

    /// <summary>
    /// Trả kết quả chuẩn cho thao tác cập nhật, bao gồm trường hợp không tìm thấy bản ghi.
    /// </summary>
    public static IResult Updated(int affectedRows)
    {
        return affectedRows == 0
            ? Results.NotFound(new { message = "Khong tim thay du lieu can cap nhat." })
            : Results.Ok(new { message = "Da cap nhat du lieu.", affectedRows });
    }

    /// <summary>
    /// Trả kết quả chuẩn cho thao tác xóa, bao gồm trường hợp không tìm thấy bản ghi.
    /// </summary>
    public static IResult Deleted(int affectedRows)
    {
        return affectedRows == 0
            ? Results.NotFound(new { message = "Khong tim thay du lieu can xoa." })
            : Results.Ok(new { message = "Da xoa du lieu.", affectedRows });
    }
}
