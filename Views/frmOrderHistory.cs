using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MiniMartPOS.Models;
using MiniMartPOS.Controllers;

namespace MiniMartPOS.Views
{
    public partial class frmOrderHistory : Form
    {
        public frmOrderHistory()
        {
            InitializeComponent();
        }

        private void frmOrderHistory_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays(-7);
            dtpTo.Value = DateTime.Today;
            LoadOrders();
        }

        /// <summary>
        /// Load danh sách hóa đơn với tìm kiếm nâng cao
        /// </summary>
        /// <param name="keyword">OrderNo hoặc CashierName</param>
        private void LoadOrders(string keyword = "")
        {
            try
            {
                string sql = @"
                    SELECT o.OrderID, o.OrderNo, o.OrderDate, o.TotalAmount, o.Discount, o.FinalAmount,
                           o.PaymentMethod, u.FullName AS CashierName
                    FROM Orders o
                    LEFT JOIN Users u ON o.UserID = u.UserID
                    WHERE o.OrderDate BETWEEN @from AND @to
                    ORDER BY o.OrderDate DESC";

                var parameters = new[]
                {
                    new SqlParameter("@from", dtpFrom.Value.Date),
                    new SqlParameter("@to", dtpTo.Value.Date.AddDays(1).AddSeconds(-1))
                };

                DataTable dt = BaseModel.GetDataTable(sql, parameters);

                // Lọc theo từ khóa tìm kiếm nếu có
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    string safeKeyword = keyword.Replace("'", "''"); // tránh lỗi SQL injection
                    DataRow[] filtered = dt.Select(
                        $"OrderNo LIKE '%{safeKeyword}%' OR CashierName LIKE '%{safeKeyword}%'"
                    );

                    dt = filtered.Length > 0 ? filtered.CopyToDataTable() : dt.Clone();
                }

                dgvOrders.DataSource = dt;

                // Format cột
                if (dgvOrders.Columns.Contains("OrderID")) dgvOrders.Columns["OrderID"].Visible = false;
                if (dgvOrders.Columns.Contains("OrderNo")) dgvOrders.Columns["OrderNo"].HeaderText = "Số hóa đơn";
                if (dgvOrders.Columns.Contains("OrderDate"))
                {
                    dgvOrders.Columns["OrderDate"].HeaderText = "Ngày giờ";
                    dgvOrders.Columns["OrderDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }
                if (dgvOrders.Columns.Contains("TotalAmount"))
                {
                    dgvOrders.Columns["TotalAmount"].HeaderText = "Tổng tiền";
                    dgvOrders.Columns["TotalAmount"].DefaultCellStyle.Format = "#,##0 đ";
                    dgvOrders.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvOrders.Columns.Contains("Discount"))
                {
                    dgvOrders.Columns["Discount"].HeaderText = "Giảm giá";
                    dgvOrders.Columns["Discount"].DefaultCellStyle.Format = "#,##0 đ";
                    dgvOrders.Columns["Discount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvOrders.Columns.Contains("FinalAmount"))
                {
                    dgvOrders.Columns["FinalAmount"].HeaderText = "Khách trả";
                    dgvOrders.Columns["FinalAmount"].DefaultCellStyle.Format = "#,##0 đ";
                    dgvOrders.Columns["FinalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvOrders.Columns.Contains("PaymentMethod")) dgvOrders.Columns["PaymentMethod"].HeaderText = "Thanh toán";
                if (dgvOrders.Columns.Contains("CashierName")) dgvOrders.Columns["CashierName"].HeaderText = "Thu ngân";

                // Tổng doanh thu
                decimal totalRevenue = 0;
                foreach (DataRow row in dt.Rows)
                    totalRevenue += Convert.ToDecimal(row["FinalAmount"]);
                lblTotalRevenue.Text = $"Tổng doanh thu: {Helper.FormatMoney(totalRevenue)}";
            }
            catch (Exception ex)
            {
                Helper.ShowError("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadOrders(txtSearch.Text.Trim());
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            dtpFrom.Value = DateTime.Today.AddDays(-7);
            dtpTo.Value = DateTime.Today;
            LoadOrders();
        }

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow == null) return;

            try
            {
                int orderId = Convert.ToInt32(dgvOrders.CurrentRow.Cells["OrderID"].Value);
                string orderNo = dgvOrders.CurrentRow.Cells["OrderNo"].Value.ToString();

                string sqlDetail = @"
                    SELECT od.ProductID, p.ProductName, od.Quantity, od.UnitPrice, od.LineTotal
                    FROM OrderDetails od
                    JOIN Products p ON od.ProductID = p.ProductID
                    WHERE od.OrderID = @id";

                DataTable detail = BaseModel.GetDataTable(sqlDetail, new[] { new SqlParameter("@id", orderId) });

                if (detail == null || detail.Rows.Count == 0)
                {
                    Helper.ShowWarning("Hóa đơn chưa có sản phẩm nào!");
                    return;
                }

                var order = new Order
                {
                    OrderNo = orderNo,
                    OrderDate = Convert.ToDateTime(dgvOrders.CurrentRow.Cells["OrderDate"].Value),
                    TotalAmount = Convert.ToDecimal(dgvOrders.CurrentRow.Cells["TotalAmount"].Value),
                    Discount = Convert.ToDecimal(dgvOrders.CurrentRow.Cells["Discount"].Value),
                    FinalAmount = Convert.ToDecimal(dgvOrders.CurrentRow.Cells["FinalAmount"].Value)
                };

                PdfGenerator.CreateInvoice(order, detail);
            }
            catch (Exception ex)
            {
                Helper.ShowError("Lỗi khi in hóa đơn: " + ex.Message);
            }
        }

        private void dgvOrders_DoubleClick(object sender, EventArgs e)
        {
            btnViewDetail_Click(sender, e);
        }
    }
}
