using MiniMartPOS.Models;
using MiniMartPOS.Views;
using MiniMartPOS.Controllers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MiniMartPOS.Views
{
    public partial class frmSupplierEdit : Form
    {
        private int SupplierID = 0;

        public frmSupplierEdit(int id = 0)
        {
            SupplierID = id;
            InitializeComponent();
            this.Text = id == 0 ? "Thêm nhà cung cấp mới" : "Sửa nhà cung cấp";
        }

        private void frmSupplierEdit_Load(object sender, EventArgs e)
        {
            if (SupplierID > 0)
            {
                string sql = "SELECT * FROM Suppliers WHERE SupplierID = @id";
                var dt = BaseModel.GetDataTable(sql, new[] { new SqlParameter("@id", SupplierID) });
                if (dt.Rows.Count > 0)
                {
                    var r = dt.Rows[0];
                    txtName.Text = r["SupplierName"]?.ToString() ?? "";
                    txtPhone.Text = r["Phone"]?.ToString() ?? "";
                    txtAddress.Text = r["Address"]?.ToString() ?? "";
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                Helper.ShowWarning("Vui lòng nhập tên nhà cung cấp!");
                return;
            }

            string sql = SupplierID == 0
                ? "INSERT INTO Suppliers (SupplierName, Phone, Address) VALUES (@n, @p, @a)"
                : "UPDATE Suppliers SET SupplierName=@n, Phone=@p, Address=@a WHERE SupplierID=@id";

            var p = new[]
            {
                new SqlParameter("@n", txtName.Text.Trim()),
                new SqlParameter("@p", string.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim()),
                new SqlParameter("@a", string.IsNullOrWhiteSpace(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text.Trim()),
                new SqlParameter("@id", SupplierID)
            };

            if (BaseModel.Execute(sql, p) > 0)
            {
                Helper.ShowSuccess("Lưu thành công!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                Helper.ShowError("Lưu thất bại!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}