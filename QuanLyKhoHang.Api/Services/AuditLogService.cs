using System.Text.Json;
using Npgsql;
using QuanLyKhoHang.Data;

namespace QuanLyKhoHang.ApiServer.Services;

/// <summary>
/// Ghi audit log cho cac thao tac lam thay doi du lieu.
/// </summary>
public sealed class AuditLogService
{
    private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

    public void Record(HttpContext context, string action, string tableName, int? recordId, object details)
    {
        try
        {
            string sql = @"INSERT INTO auditlog
                           (ten_taikhoan, vai_tro, hanh_dong, bang_du_lieu, ma_ban_ghi, noi_dung, ip_address)
                           VALUES (@username, @role, @action, @table, @recordId, @details, @ip)";

            NpgsqlParameter[] parameters =
            {
                new NpgsqlParameter("@username", GetUsername(context)),
                new NpgsqlParameter("@role", GetRole(context)),
                new NpgsqlParameter("@action", action),
                new NpgsqlParameter("@table", tableName),
                new NpgsqlParameter("@recordId", recordId.HasValue ? recordId.Value : DBNull.Value),
                new NpgsqlParameter("@details", SerializeDetails(details)),
                new NpgsqlParameter("@ip", context.Connection.RemoteIpAddress?.ToString() ?? string.Empty)
            };

            _dbHelper.ExecuteNonQuery(sql, parameters);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Khong ghi duoc audit log: " + ex.Message);
        }
    }

    private static string GetUsername(HttpContext context)
    {
        return context.User?.Identity?.Name
            ?? Convert.ToString(context.Items[ApiAuthorization.UserNameItemKey])
            ?? string.Empty;
    }

    private static string GetRole(HttpContext context)
    {
        return context.User?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value
            ?? Convert.ToString(context.Items[ApiAuthorization.UserRoleItemKey])
            ?? string.Empty;
    }

    private static string SerializeDetails(object details)
    {
        return JsonSerializer.Serialize(details, new JsonSerializerOptions
        {
            WriteIndented = false
        });
    }
}
