using System.Data;

namespace QuanLyKhoHang.ApiServer;

public static class DataTableJson
{
    public static List<Dictionary<string, object>> ToRows(DataTable table)
    {
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        foreach (DataRow row in table.Rows)
        {
            Dictionary<string, object> item = new Dictionary<string, object>();
            foreach (DataColumn column in table.Columns)
            {
                object value = row[column];
                item[column.ColumnName] = value == DBNull.Value ? null : value;
            }

            rows.Add(item);
        }

        return rows;
    }
}
