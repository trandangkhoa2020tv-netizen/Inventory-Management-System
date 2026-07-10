using QuanLyKhoHang.ApiServer.Config;
using QuanLyKhoHang.ApiServer.Services;

namespace QuanLyKhoHang.Tests;

public sealed class JwtTokenServiceTests
{
    [Fact]
    public void CreateToken_ShouldReturnTokenThatCanBeValidated()
    {
        JwtTokenService service = CreateService();

        string token = service.CreateToken("admin", "Admin", out DateTime expiresAt);

        Assert.False(string.IsNullOrWhiteSpace(token));
        Assert.True(expiresAt > DateTime.UtcNow);
        Assert.True(service.IsValid(token));
    }

    [Fact]
    public void TryReadUser_ShouldReturnUsernameAndRole()
    {
        JwtTokenService service = CreateService();

        string token = service.CreateToken("admin", "Admin", out _);

        Assert.True(service.TryReadUser(token, out string username, out string role));
        Assert.Equal("admin", username);
        Assert.Equal("Admin", role);
    }

    [Fact]
    public void IsValid_ShouldRejectInvalidToken()
    {
        JwtTokenService service = CreateService();

        Assert.False(service.IsValid("invalid.token.value"));
    }

    private static JwtTokenService CreateService()
    {
        return new JwtTokenService(new JwtSettings
        {
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            SecretKey = "Unit-Test-Secret-Key-For-Jwt-Validation",
            ExpirationMinutes = 30
        });
    }
}
