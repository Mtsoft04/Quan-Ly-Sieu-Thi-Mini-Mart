using System;
using System.Data;
using System.Windows.Forms;
using MiniMartPOS.Models;
using MiniMartPOS.Controllers;


namespace MiniMartPOS.Views
{
    public partial class frmImport : Form
    {
        public frmImport()
        {
            InitializeComponent();
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            LoadImportReceipts();
        }

        private void LoadImportReceipts()
        {
            string sql = @"
                SELECT ir.ReceiptID, ir.ReceiptNo, ir.ImportDate, ir.TotalAmount,
                       s.SupplierName, u.FullName AS UserName, ir.Note
                FROM ImportReceipts ir
                LEFT JOIN Suppliers s ON ir.SupplierID = s.SupplierID
                LEFT JOIN Users u ON ir.UserID = u.UserID
                ORDER BY ir.ImportDate DESC";

            DataTable dt = BaseModel.GetDataTable(sql);
            dgvImports.DataSource = dt;

            // Định dạng cột
            if (dgvImports.Columns["ReceiptID"] != null) dgvImports.Columns["ReceiptID"].Visible = false;
            if (dgvImports.Columns["ReceiptNo"] != null) dgvImports.Columns["ReceiptNo"].HeaderText = "Số phiếu";
            if (dgvImports.Columns["ImportDate"] != null)
            {
                dgvImports.Columns["ImportDate"].HeaderText = "Ngày nhập";
                dgvImports.Columns["ImportDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvImports.Columns["TotalAmount"] != null)
            {
                dgvImports.Columns["TotalAmount"].HeaderText = "Tổng tiền";
                dgvImports.Columns["TotalAmount"].DefaultCellStyle.Format = "#,##0 đ";
                dgvImports.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvImports.Columns["SupplierName"] != null) dgvImports.Columns["SupplierName"].HeaderText = "Nhà cung cấp";
            if (dgvImports.Columns["UserName"] != null) dgvImports.Columns["UserName"].HeaderText = "Nhân viên";
            if (dgvImports.Columns["Note"] != null) dgvImports.Columns["Note"].HeaderText = "Ghi chú";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var f = new frmImportAdd())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadImportReceipts();
                    Helper.ShowSuccess("Nhập hàng thành công!");
                }
            }
        }

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            if (dgvImports.CurrentRow == null) return;

            int receiptId = Convert.ToInt32(dgvImports.CurrentRow.Cells["ReceiptID"].Value);
            string receiptNo = dgvImports.CurrentRow.Cells["ReceiptNo"].Value.ToString();

            using (var f = new frmImportDetail(receiptId, receiptNo))
            {
                f.ShowDialog();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadImportReceipts();
        }

        private void dgvImports_DoubleClick(object sender, EventArgs e)
        {
            btnViewDetail_Click(sender, e);
        }
    }
}