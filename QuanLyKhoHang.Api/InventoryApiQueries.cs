using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;

namespace QuanLyKhoHang.ApiServer;

public sealed class InventoryApiQueries
{
    private readonly DatabaseHelper _db = new DatabaseHelper();

    public DataTable GetTonKhoThap(int soLuongToiDa)
    {
        const string sql = @"SELECT ma_hanghoa AS ""Ma Hang"", ten_hanghoa AS ""Ten Hang Hoa"",
                                    so_luong_ton AS ""Ton Kho"", don_vi_tinh AS ""DVT""
                             FROM hanghoa
                             WHERE so_luong_ton <= @soLuongToiDa
                             ORDER BY so_luong_ton ASC, ten_hanghoa ASC";
        return _db.ExecuteQuery(sql, new[] { new NpgsqlParameter("@soLuongToiDa", soLuongToiDa) });
    }
}
