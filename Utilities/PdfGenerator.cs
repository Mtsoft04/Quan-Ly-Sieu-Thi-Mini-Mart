using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Data;
using System.IO;

public static class PdfGenerator
{
    public static void CreateInvoice(Order order, DataTable details, string savePath = null)
    {
        // Kiểm tra dữ liệu an toàn
        if (order == null)
        {
            Helper.ShowWarning("Không có thông tin hóa đơn để in!");
            return;
        }

        if (details == null || details.Rows.Count == 0)
        {
            Helper.ShowWarning("Không có sản phẩm trong hóa đơn!");
            return;
        }

        if (string.IsNullOrEmpty(savePath))
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            savePath = Path.Combine(desktop, $"HoaDon_{order.OrderNo ?? "Unknown"}.pdf");
        }

        try
        {
            using (var writer = new PdfWriter(savePath))
            using (var pdf = new PdfDocument(writer))
            using (var document = new Document(pdf))
            {
                // Tiêu đề cửa hàng
                document.Add(new Paragraph("MINI MART BÁN LẺ")
                    .SetFontSize(20).SetBold().SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph("ĐC: 11 Đường Đồng Tiến, Xã An Khánh, TP.Hà Nội - ĐT: 0982.899.620")
                    .SetFontSize(10).SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph($"HÓA ĐƠN BÁN HÀNG: {order.OrderNo ?? "HD???"}")
                    .SetFontSize(14).SetBold().SetTextAlignment(TextAlignment.CENTER));

                string cashierName = CurrentUser.FullName ?? "Thu ngân chưa xác định";
                DateTime orderDate = order.OrderDate == default ? DateTime.Now : order.OrderDate;

                document.Add(new Paragraph($"Ngày: {orderDate:dd/MM/yyyy HH:mm} | Thu ngân: {cashierName}")
                    .SetFontSize(11).SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph(new string('=', 50)));

                // Bảng sản phẩm
                Table table = new Table(new float[] { 25, 5, 10, 10 }).UseAllAvailableWidth();
                table.AddHeaderCell("Tên sản phẩm").SetBold();
                table.AddHeaderCell("SL").SetBold();
                table.AddHeaderCell("Đơn giá").SetBold();
                table.AddHeaderCell("Thành tiền").SetBold();

                decimal total = 0;
                foreach (DataRow row in details.Rows)
                {
                    string name = row.Table.Columns.Contains("ProductName") ? row["ProductName"].ToString() : "Unknown";
                    int qty = row.Table.Columns.Contains("Quantity") && row["Quantity"] != DBNull.Value ? Convert.ToInt32(row["Quantity"]) : 0;
                    decimal price = row.Table.Columns.Contains("UnitPrice") && row["UnitPrice"] != DBNull.Value ? Convert.ToDecimal(row["UnitPrice"]) : 0;
                    decimal amount = qty * price;
                    total += amount;

                    table.AddCell(name.Length > 25 ? name.Substring(0, 22) + "..." : name);
                    table.AddCell(qty.ToString()).SetTextAlignment(TextAlignment.CENTER);
                    table.AddCell(price.ToString("#,##0")).SetTextAlignment(TextAlignment.RIGHT);
                    table.AddCell(amount.ToString("#,##0")).SetTextAlignment(TextAlignment.RIGHT);
                }
                document.Add(table);

                document.Add(new Paragraph(new string('-', 50)));
                document.Add(new Paragraph($"Tổng cộng: {Helper.FormatMoney(total)}").SetFontSize(12).SetBold());

                if (order.Discount > 0)
                    document.Add(new Paragraph($"Giảm giá: {Helper.FormatMoney(order.Discount)}").SetBold());

                document.Add(new Paragraph($"Khách trả: {Helper.FormatMoney(order.FinalAmount)}")
                    .SetFontSize(16).SetBold());

                document.Add(new Paragraph(new string('=', 50)));
                document.Add(new Paragraph("CẢM ƠN QUÝ KHÁCH - HẸN GẶP LẠI!")
                    .SetFontSize(14).SetBold().SetTextAlignment(TextAlignment.CENTER));

                // Mở PDF tự động
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(savePath) { UseShellExecute = true });
                }
                catch
                {
                    Helper.ShowWarning("Đã tạo hóa đơn nhưng không mở được file PDF!");
                }
            }
        }
        catch (Exception ex)
        {
            Helper.ShowError("Lỗi khi tạo hóa đơn PDF: " + ex.Message);
        }
    }
}
