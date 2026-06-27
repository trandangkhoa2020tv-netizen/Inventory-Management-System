using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace QuanLyKhoHang.Reports
{
    /// <summary>
    /// Chức năng xuất dữ liệu dạng bảng ra file PDF.
    /// Lớp này dùng iTextSharp để tạo tài liệu A4, tiêu đề và bảng chi tiết phiếu.
    /// </summary>
    public class ExportPdf
    {
        /// <summary>
        /// Nhận DataTable cần in và tiêu đề báo cáo.
        /// Dữ liệu được ghi ra file PDF tại vị trí người dùng chọn.
        /// </summary>
        public static void ToPdf(DataTable dt, string titleHeader)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất PDF!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF Document (*.pdf)|*.pdf";
                sfd.FileName = titleHeader.Replace(" ", "_") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

                // Chỉ tạo file khi người dùng xác nhận vị trí lưu.
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 15f, 15f, 20f, 20f);
                        iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(sfd.FileName, FileMode.Create));

                        document.Open();

                        // Dùng Arial để PDF hiển thị được tiếng Việt nếu dữ liệu có dấu.
                        string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Fonts", "Arial.ttf");
                        iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(fontPath, iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED);

                        iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.Blue);
                        iTextSharp.text.Font fontHeader = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.White);
                        iTextSharp.text.Font fontBody = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.Black);

                        // Tiêu đề báo cáo đặt ở giữa trang.
                        iTextSharp.text.Paragraph prgTitle = new iTextSharp.text.Paragraph(titleHeader.ToUpper(), fontTitle);
                        prgTitle.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                        prgTitle.SpacingAfter = 20;
                        document.Add(prgTitle);

                        // Tạo bảng có số cột bằng DataTable.
                        iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(dt.Columns.Count);
                        table.WidthPercentage = 100;

                        foreach (DataColumn column in dt.Columns)
                        {
                            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(column.ColumnName, fontHeader));
                            cell.BackgroundColor = iTextSharp.text.BaseColor.DarkGray;
                            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            cell.Padding = 6;
                            table.AddCell(cell);
                        }

                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (object cellValue in row.ItemArray)
                            {
                                string cellText = cellValue?.ToString() ?? string.Empty;
                                iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(cellText, fontBody));
                                cell.Padding = 5;
                                cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                table.AddCell(cell);
                            }
                        }

                        document.Add(table);
                        document.Close();

                        MessageBox.Show("Xuất file PDF thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xuất file PDF: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
