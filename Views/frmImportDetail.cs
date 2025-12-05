using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MiniMartPOS.Views
{
    public partial class frmImportDetail : Form
    {
        private int ReceiptID;
        private string ReceiptNo;

        public frmImportDetail(int receiptId, string receiptNo)
        {
            ReceiptID = receiptId;
            ReceiptNo = receiptNo;
            InitializeComponent();
        }

        private void frmImportDetail_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = $"Chi tiết phiếu nhập: {ReceiptNo}";
                LoadReceiptInfo();
                LoadDetail();
            }
            catch (Exception ex)
            {
                Helper.ShowError("Lỗi khi tải chi tiết phiếu: " + ex.Message);
            }
        }

        private void LoadReceiptInfo()
        {
            string sql = @"
                SELECT ir.ReceiptNo, ir.ImportDate, ir.TotalAmount, ir.Note,
                       ISNULL(s.SupplierName, 'Không có') AS SupplierName,
                       ISNULL(u.FullName, '') AS UserName
                FROM ImportReceipts ir
                LEFT JOIN Suppliers s ON ir.SupplierID = s.SupplierID
                LEFT JOIN Users u ON ir.UserID = u.UserID
                WHERE ir.ReceiptID = @id";

            var dt = BaseModel.GetDataTable(sql, new[] { new SqlParameter("@id", ReceiptID) });
            if (dt.Rows.Count > 0)
            {
                var r = dt.Rows[0];

                lblReceiptNo.Text = "Số phiếu: " + (r["ReceiptNo"]?.ToString() ?? "");
                // ImportDate có thể null? theo DB mặc định GETDATE nên hiếm, nhưng ta vẫn guard
                if (r["ImportDate"] != DBNull.Value && DateTime.TryParse(r["ImportDate"].ToString(), out DateTime importDate))
                    lblDate.Text = "Ngày nhập: " + importDate.ToString("dd/MM/yyyy HH:mm");
                else
                    lblDate.Text = "Ngày nhập: -";

                lblSupplier.Text = "Nhà cung cấp: " + (r["SupplierName"]?.ToString() ?? "Không có");
                lblUser.Text = "Nhân viên: " + (r["UserName"]?.ToString() ?? "");
                decimal total = 0m;
                if (r["TotalAmount"] != DBNull.Value)
                    decimal.TryParse(r["TotalAmount"].ToString(), out total);
                lblTotal.Text = "TỔNG TIỀN: " + Helper.FormatMoney(total);
                txtNote.Text = r["Note"]?.ToString() ?? "";
            }
            else
            {
                lblReceiptNo.Text = "Số phiếu: (Không tìm thấy)";
                lblDate.Text = "Ngày nhập: -";
                lblSupplier.Text = "Nhà cung cấp: -";
                lblUser.Text = "Nhân viên: -";
                lblTotal.Text = "TỔNG TIỀN: 0 đ";
                txtNote.Text = "";
            }
        }

        private void LoadDetail()
        {
            // ORDER BY id.ID là hợp lệ vì DB bạn tạo cột ID trong ImportDetails.
            string sql = @"
                SELECT p.Barcode, p.ProductName, id.Quantity, id.UnitPrice, id.LineTotal
                FROM ImportDetails id
                JOIN Products p ON id.ProductID = p.ProductID
                WHERE id.ReceiptID = @id
                ORDER BY id.ID";

            var dt = BaseModel.GetDataTable(sql, new[] { new SqlParameter("@id", ReceiptID) });

            dgvDetail.DataSource = dt;

            // an toàn: kiểm tra tồn tại cột trước khi gán header/format
            if (dt.Columns.Contains("Barcode"))
                dgvDetail.Columns["Barcode"].HeaderText = "Mã vạch";
            if (dt.Columns.Contains("ProductName"))
                dgvDetail.Columns["ProductName"].HeaderText = "Tên sản phẩm";
            if (dt.Columns.Contains("Quantity"))
            {
                dgvDetail.Columns["Quantity"].HeaderText = "SL";
                dgvDetail.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dt.Columns.Contains("UnitPrice"))
            {
                dgvDetail.Columns["UnitPrice"].HeaderText = "Giá nhập";
                dgvDetail.Columns["UnitPrice"].DefaultCellStyle.Format = "#,##0 đ";
                dgvDetail.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dt.Columns.Contains("LineTotal"))
            {
                dgvDetail.Columns["LineTotal"].HeaderText = "Thành tiền";
                dgvDetail.Columns["LineTotal"].DefaultCellStyle.Format = "#,##0 đ";
                dgvDetail.Columns["LineTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Một vài cài đặt hiển thị
            dgvDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetail.MultiSelect = false;
            dgvDetail.ReadOnly = true;
            dgvDetail.AllowUserToAddRows = false;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}
