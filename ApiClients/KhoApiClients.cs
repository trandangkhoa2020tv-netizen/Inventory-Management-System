using System.Data;
using System.Text.Json.Serialization;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.ApiClients
{
    public sealed class PhieuNhapApiClient
    {
        public DataTable GetAllPhieuNhap() => ApiHttpClient.GetTable("api/phieu-nhap");
        public DataTable GetChiTietTheoMaPhieu(int id) => ApiHttpClient.GetTable($"api/phieu-nhap/{id}/chi-tiet");

        public void LuuPhieuNhap(PhieuNhap phieuNhap, List<ChiTietPhieuNhap> chiTietList)
        {
            ApiHttpClient.Post("api/phieu-nhap", new { phieuNhap, chiTietList });
        }
    }

    public sealed class PhieuXuatApiClient
    {
        public DataTable GetAllPhieuXuat() => ApiHttpClient.GetTable("api/phieu-xuat");
        public DataTable GetChiTietTheoMaPhieu(int id) => ApiHttpClient.GetTable($"api/phieu-xuat/{id}/chi-tiet");
        public DataTable GetThongTinPhieuXuat(int id) => ApiHttpClient.GetTable($"api/phieu-xuat/{id}/thong-tin");

        public void LuuPhieuXuat(PhieuXuat phieuXuat, List<ChiTietPhieuXuat> chiTietList)
        {
            ApiHttpClient.Post("api/phieu-xuat", new { phieuXuat, chiTietList });
        }
    }

    public sealed class AuthApiClient
    {
        public string CheckLogin(string username, string password)
        {
            try
            {
                LoginResponse response = ApiHttpClient.PostForJson<object, LoginResponse>(
                    "api/auth/login",
                    new { username, password });

                return response?.VaiTro ?? string.Empty;
            }
            catch (UnauthorizedAccessException)
            {
                return string.Empty;
            }
        }

        private sealed class LoginResponse
        {
            [JsonPropertyName("tenTaiKhoan")]
            public string TenTaiKhoan { get; set; }

            [JsonPropertyName("vaiTro")]
            public string VaiTro { get; set; }
        }
    }
}
