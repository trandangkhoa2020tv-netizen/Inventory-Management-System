using System;
using System.Data;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace QuanLyKhoHang.Reports
{
    public class ExportExcel
    {
        public static void ToExcel(DataTable dt, string titleHeader)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có mặt hàng nào trong danh sách để xuất Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                sfd.FileName = titleHeader + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                
                // BƯỚC QUAN TRỌNG: Chỉ chạy khi bấm nút "Save"
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var ws = wb.Worksheets.Add(dt, "Chi Tiết Phiếu");
                            ws.Columns().AdjustToContents(); 
                            wb.SaveAs(sfd.FileName); // Ghi file thực tế
                        }
                        
                        // THÔNG BÁO PHẢI NẰM TRONG NGOẶC NHỌN CỦA IF DIALOGRESULT.OK
                        MessageBox.Show("Xuất hóa đơn Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xuất file Excel: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // Nếu lọt ra ngoài này (người dùng bấm Cancel), hàm sẽ kết thúc êm ru, không chạy lệnh hiện MessageBox bên trên!
            }
        }
    }
}