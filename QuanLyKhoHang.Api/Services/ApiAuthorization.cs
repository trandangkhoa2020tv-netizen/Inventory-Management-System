using System.Security.Claims;

namespace QuanLyKhoHang.ApiServer.Services;

/// <summary>
/// Helper phan quyen don gian dua tren role trong JWT.
/// </summary>
public static class ApiAuthorization
{
    public const string UserNameItemKey = "ApiUserName";
    public const string UserRoleItemKey = "ApiUserRole";

    public static void SetAuthenticatedUser(HttpContext context, string username, string role)
    {
        context.Items[UserNameItemKey] = username;
        context.Items[UserRoleItemKey] = role;

        ClaimsIdentity identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        }, "Jwt");

        context.User = new ClaimsPrincipal(identity);
    }

    public static IResult RequireAdmin(HttpContext context)
    {
        return RequireRole(context, "Admin");
    }

    public static IResult RequireRole(HttpContext context, params string[] allowedRoles)
    {
        string role = GetRole(context);
        if (string.IsNullOrWhiteSpace(role))
        {
            return Results.Unauthorized();
        }

        foreach (string allowedRole in allowedRoles)
        {
            if (string.Equals(role, allowedRole, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
        }

        return Results.StatusCode(StatusCodes.Status403Forbidden);
    }

    private static string GetRole(HttpContext context)
    {
        if (context.User?.Identity?.IsAuthenticated == true)
        {
            string role = context.User.FindFirstValue(ClaimTypes.Role);
            if (!string.IsNullOrWhiteSpace(role))
            {
                return role;
            }
        }

        return context.Items.TryGetValue(UserRoleItemKey, out object value)
            ? Convert.ToString(value) ?? string.Empty
            : string.Empty;
    }
}
