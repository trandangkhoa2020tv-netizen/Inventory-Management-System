using System;
using System.Collections.Generic;
using System.Data;

namespace QuanLyKhoHang.Api
{
    /// <summary>
    /// Chuyển DataTable sang danh sách Dictionary để ASP.NET Core serialize ra JSON.
    /// Repository hiện trả về DataTable cho WinForms, nên API dùng lớp này làm cầu nối.
    /// </summary>
    public static class DataTableJson
    {
        /// <summary>
        /// Mỗi dòng DataRow được chuyển thành object JSON, tên cột là key.
        /// DBNull trong database được đổi thành null để JSON đúng nghĩa hơn.
        /// </summary>
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
}
