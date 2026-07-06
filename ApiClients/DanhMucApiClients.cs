using System.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.ApiClients
{
    public sealed class HangHoaApiClient
    {
        public DataTable GetAll() => ApiHttpClient.GetTable("api/hang-hoa");
        public void Them(HangHoa hangHoa) => ApiHttpClient.Post("api/hang-hoa", hangHoa);
        public void Sua(HangHoa hangHoa) => ApiHttpClient.Put($"api/hang-hoa/{hangHoa.MaHangHoa}", hangHoa);
        public void Xoa(int id) => ApiHttpClient.Delete($"api/hang-hoa/{id}");
    }

    public sealed class LoaiHangApiClient
    {
        public DataTable GetAll() => ApiHttpClient.GetTable("api/loai-hang");
        public void Them(LoaiHang loaiHang) => ApiHttpClient.Post("api/loai-hang", loaiHang);
        public void Sua(LoaiHang loaiHang) => ApiHttpClient.Put($"api/loai-hang/{loaiHang.MaLoaiHang}", loaiHang);
        public void Xoa(int id) => ApiHttpClient.Delete($"api/loai-hang/{id}");
    }

    public sealed class NhaCungCapApiClient
    {
        public DataTable GetAll() => ApiHttpClient.GetTable("api/nha-cung-cap");
        public void Them(NhaCungCap nhaCungCap) => ApiHttpClient.Post("api/nha-cung-cap", nhaCungCap);
        public void Sua(NhaCungCap nhaCungCap) => ApiHttpClient.Put($"api/nha-cung-cap/{nhaCungCap.MaNhaCungCap}", nhaCungCap);
        public void Xoa(int id) => ApiHttpClient.Delete($"api/nha-cung-cap/{id}");
    }

    public sealed class KhachHangApiClient
    {
        public DataTable GetAll() => ApiHttpClient.GetTable("api/khach-hang");
        public void Them(KhachHang khachHang) => ApiHttpClient.Post("api/khach-hang", khachHang);
        public void Sua(KhachHang khachHang) => ApiHttpClient.Put($"api/khach-hang/{khachHang.MaKhachHang}", khachHang);
        public void Xoa(int id) => ApiHttpClient.Delete($"api/khach-hang/{id}");
    }

    public sealed class NhanVienApiClient
    {
        public DataTable GetAll() => ApiHttpClient.GetTable("api/nhan-vien");
        public void Them(NhanVien nhanVien) => ApiHttpClient.Post("api/nhan-vien", nhanVien);
        public void Sua(NhanVien nhanVien) => ApiHttpClient.Put($"api/nhan-vien/{nhanVien.MaNhanVien}", nhanVien);
        public void Xoa(int id) => ApiHttpClient.Delete($"api/nhan-vien/{id}");
    }
}
