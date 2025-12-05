using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using MiniMartPOS.Models;

namespace MiniMartPOS.Views
{
    public partial class frmReportDaily : Form
    {
        private PrintDocument printDoc;
        private DataTable dtTopProducts, dtCashiers;
        private decimal totalRevenue;
        private int totalOrders;

        public frmReportDaily()
        {
            InitializeComponent();
            this.Load += frmReportDaily_Load;

            printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDoc_PrintPage;
        }

        private void frmReportDaily_Load(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Today;

            btnToday.Click += btnToday_Click;
            dtpDate.ValueChanged += dtpDate_ValueChanged;
            btnPrint.Click += btnPrint_Click;

            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                DateTime date = dtpDate.Value.Date;
                DateTime from = date;
                DateTime to = date.AddDays(1).AddSeconds(-1);

                // Tổng doanh thu
                var revenueResult = BaseModel.ExecuteScalar(
                    "SELECT ISNULL(SUM(FinalAmount),0) FROM Orders WHERE OrderDate BETWEEN @from AND @to",
                    new[] { new SqlParameter("@from", from), new SqlParameter("@to", to) });
                totalRevenue = (revenueResult != DBNull.Value) ? Convert.ToDecimal(revenueResult) : 0;

                // Số hóa đơn
                var countResult = BaseModel.ExecuteScalar(
                    "SELECT COUNT(*) FROM Orders WHERE OrderDate BETWEEN @from AND @to",
                    new[] { new SqlParameter("@from", from), new SqlParameter("@to", to) });
                totalOrders = (countResult != DBNull.Value) ? Convert.ToInt32(countResult) : 0;

                // Top 10 sản phẩm
                string sqlTop = @"
                    SELECT TOP 10 p.ProductName, SUM(od.Quantity) AS TotalQty, SUM(od.LineTotal) AS TotalAmount
                    FROM OrderDetails od
                    JOIN Products p ON od.ProductID = p.ProductID
                    JOIN Orders o ON od.OrderID = o.OrderID
                    WHERE o.OrderDate BETWEEN @from AND @to
                    GROUP BY p.ProductName
                    ORDER BY TotalQty DESC";
                dtTopProducts = BaseModel.GetDataTable(sqlTop,
                    new[] { new SqlParameter("@from", from), new SqlParameter("@to", to) });

                // Thu ngân
                string sqlCashier = @"
                    SELECT u.FullName, COUNT(o.OrderID) AS SoHD, ISNULL(SUM(o.FinalAmount),0) AS DoanhThu
                    FROM Orders o
                    JOIN Users u ON o.UserID = u.UserID
                    WHERE o.OrderDate BETWEEN @from AND @to
                    GROUP BY u.FullName
                    ORDER BY DoanhThu DESC";
                dtCashiers = BaseModel.GetDataTable(sqlCashier,
                    new[] { new SqlParameter("@from", from), new SqlParameter("@to", to) });

                // Hiển thị
                lblDate.Text = $"BÁO CÁO DOANH THU NGÀY: {date:dd/MM/yyyy}";
                lblRevenue.Text = Helper.FormatMoney(totalRevenue);
                lblOrderCount.Text = totalOrders + " hóa đơn";
                lblAvgOrder.Text = totalOrders > 0 ? "Trung bình: " + Helper.FormatMoney(totalRevenue / totalOrders) : "Trung bình: 0 đ";

                dgvTopProducts.AutoGenerateColumns = true;
                dgvTopProducts.DataSource = dtTopProducts;

                dgvCashiers.AutoGenerateColumns = true;
                dgvCashiers.DataSource = dtCashiers;

                if (dgvTopProducts.Columns.Contains("TotalAmount"))
                    dgvTopProducts.Columns["TotalAmount"].DefaultCellStyle.Format = "#,##0 đ";

                if (dgvCashiers.Columns.Contains("DoanhThu"))
                    dgvCashiers.Columns["DoanhThu"].DefaultCellStyle.Format = "#,##0 đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load báo cáo: " + ex.Message);
            }
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Today;
            LoadReport();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintPreviewDialog preview = new PrintPreviewDialog();
                preview.Document = printDoc;
                preview.WindowState = FormWindowState.Maximized;
                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in: " + ex.Message);
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int startX = 50, startY = 50, offsetY = 0, lineHeight = 25;
            Font titleFont = new Font("Segoe UI", 16, FontStyle.Bold);
            Font headerFont = new Font("Segoe UI", 12, FontStyle.Bold);
            Font bodyFont = new Font("Segoe UI", 12);

            // Tiêu đề
            g.DrawString("BÁO CÁO DOANH THU NGÀY", titleFont, Brushes.Black, startX, startY + offsetY);
            offsetY += 40;
            g.DrawString(lblDate.Text, bodyFont, Brushes.Black, startX, startY + offsetY);
            offsetY += 40;

            g.DrawString("Tổng doanh thu: " + Helper.FormatMoney(totalRevenue), bodyFont, Brushes.Green, startX, startY + offsetY);
            offsetY += lineHeight;
            g.DrawString("Số hóa đơn: " + totalOrders, bodyFont, Brushes.Black, startX, startY + offsetY);
            offsetY += lineHeight;
            g.DrawString(lblAvgOrder.Text, bodyFont, Brushes.Black, startX, startY + offsetY);
            offsetY += 40;

            // Top sản phẩm
            g.DrawString("TOP 10 SẢN PHẨM BÁN CHẠY", headerFont, Brushes.Black, startX, startY + offsetY);
            offsetY += lineHeight;
            g.DrawString("Sản phẩm".PadRight(40) + "SL".PadRight(10) + "Doanh thu", bodyFont, Brushes.Black, startX, startY + offsetY);
            offsetY += lineHeight;

            foreach (DataRow row in dtTopProducts.Rows)
            {
                string line = $"{row["ProductName"],-40}{row["TotalQty"],-10}{Helper.FormatMoney(Convert.ToDecimal(row["TotalAmount"]))}";
                g.DrawString(line, bodyFont, Brushes.Black, startX, startY + offsetY);
                offsetY += lineHeight;
            }
            offsetY += 20;

            // Thu ngân
            g.DrawString("THU NGÂN", headerFont, Brushes.Black, startX, startY + offsetY);
            offsetY += lineHeight;
            g.DrawString("Thu ngân".PadRight(30) + "Số HD".PadRight(10) + "Doanh thu", bodyFont, Brushes.Black, startX, startY + offsetY);
            offsetY += lineHeight;

            foreach (DataRow row in dtCashiers.Rows)
            {
                string line = $"{row["FullName"],-30}{row["SoHD"],-10}{Helper.FormatMoney(Convert.ToDecimal(row["DoanhThu"]))}";
                g.DrawString(line, bodyFont, Brushes.Black, startX, startY + offsetY);
                offsetY += lineHeight;
            }
        }
    }
}
