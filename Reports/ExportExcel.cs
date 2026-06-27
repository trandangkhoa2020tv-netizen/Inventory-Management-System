using System;
using System.Data;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace QuanLyKhoHang.Reports
{
    /// <summary>
    /// Chức năng xuất dữ liệu dạng bảng ra file Excel (.xlsx).
    /// Các form nhập/xuất kho dùng lớp này để xuất chi tiết phiếu đang được chọn.
    /// </summary>
    public class ExportExcel
    {
        /// <summary>
        /// Nhận DataTable cần xuất và tên tiêu đề file.
        /// Người dùng chọn nơi lưu bằng SaveFileDialog, sau đó ClosedXML ghi file Excel.
        /// </summary>
        public static void ToExcel(DataTable dt, string titleHeader)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu để xuất Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                sfd.FileName = titleHeader + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

                // Chỉ xuất file khi người dùng thật sự bấm Save.
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            // Add(dt, sheetName) tự tạo tiêu đề cột từ DataTable.
                            var ws = wb.Worksheets.Add(dt, "Chi Tiet Phieu");
                            ws.Columns().AdjustToContents();
                            wb.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xuất file Excel: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
