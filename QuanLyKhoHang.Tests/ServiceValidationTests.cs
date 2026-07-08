using QuanLyKhoHang.ApiServer.DTOs;
using QuanLyKhoHang.ApiServer.Services;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.Tests;

public sealed class ServiceValidationTests
{
    [Fact]
    public void AuthService_ShouldRejectEmptyLoginRequest()
    {
        AuthService service = new AuthService(new TaiKhoanRepository());

        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            service.CheckLogin(new LoginRequest
            {
                Username = "",
                Password = ""
            }));

        Assert.Contains("Vui long nhap ten tai khoan", ex.Message);
    }

    [Fact]
    public void HangHoaService_ShouldRejectMissingName()
    {
        HangHoaService service = new HangHoaService(new HangHoaRepository());

        ApiValidationException ex = Assert.Throws<ApiValidationException>(() =>
            service.Them(new HangHoa
            {
                TenHangHoa = "",
                MaLoaiHang = 1,
                MaNhaCungCap = 1,
                GiaNhap = 1000,
                GiaBan = 1500,
                SoLuongTon = 10,
                DonViTinh = "Cai"
            }));

        Assert.Contains(ex.Errors, error => error.Contains("tenHangHoa"));
    }

    [Fact]
    public void HangHoaService_ShouldRejectNegativeStock()
    {
        HangHoaService service = new HangHoaService(new HangHoaRepository());

        ApiValidationException ex = Assert.Throws<ApiValidationException>(() =>
            service.Them(new HangHoa
            {
                TenHangHoa = "Laptop",
                MaLoaiHang = 1,
                MaNhaCungCap = 1,
                GiaNhap = 1000,
                GiaBan = 1500,
                SoLuongTon = -1,
                DonViTinh = "Cai"
            }));

        Assert.Contains(ex.Errors, error => error.Contains("soLuongTon"));
    }

    [Fact]
    public void PhieuNhapService_ShouldRejectEmptyDetails()
    {
        PhieuNhapService service = new PhieuNhapService(new PhieuNhapRepository());

        ApiValidationException ex = Assert.Throws<ApiValidationException>(() =>
            service.LuuPhieuNhap(new LuuPhieuNhapRequest
            {
                PhieuNhap = new PhieuNhap
                {
                    MaNhaCungCap = 1,
                    MaNhanVien = 1,
                    TongTien = 1000
                },
                ChiTietList = new List<ChiTietPhieuNhap>()
            }));

        Assert.Contains(ex.Errors, error => error.Contains("it nhat mot mat hang"));
    }

    [Fact]
    public void PhieuXuatService_ShouldRejectEmptyDetails()
    {
        PhieuXuatService service = new PhieuXuatService(new PhieuXuatRepository());

        ApiValidationException ex = Assert.Throws<ApiValidationException>(() =>
            service.LuuPhieuXuat(new LuuPhieuXuatRequest
            {
                PhieuXuat = new PhieuXuat
                {
                    MaKhachHang = 1,
                    MaNhanVien = 1,
                    TongTien = 1000
                },
                ChiTietList = new List<ChiTietPhieuXuat>()
            }));

        Assert.Contains(ex.Errors, error => error.Contains("it nhat mot mat hang"));
    }
}
