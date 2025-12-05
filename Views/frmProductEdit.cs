using MiniMartPOS.Controllers;
using MiniMartPOS.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace MiniMartPOS.Views
{
    public partial class frmProductEdit : Form
    {
        private int ProductId = 0;

        public frmProductEdit(int id = 0)
        {
            ProductId = id;
            InitializeComponent();

            // Gắn sự kiện
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            chkPromo.CheckedChanged += chkPromo_CheckedChanged;

            txtImportPrice.KeyPress += NumberOnly_KeyPress;
            txtSalePrice.KeyPress += NumberOnly_KeyPress;
            txtPromoPrice.KeyPress += NumberOnly_KeyPress;
            txtStock.KeyPress += NumberOnly_KeyPress;
            txtMinStock.KeyPress += NumberOnly_KeyPress;

            this.Load += frmProductEdit_Load;
        }

        private void frmProductEdit_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadSuppliers();

            if (ProductId > 0)
            {
                LoadProductFromDB();
                this.Text = "Sửa sản phẩm";
            }
            else
            {
                this.Text = "Thêm sản phẩm mới";
                txtUnit.Text = "Cái";
                chkPromo.Checked = false;
                dtpPromoStart.Enabled = dtpPromoEnd.Enabled = false;

                // Chọn dòng đầu tiên của danh mục nếu có
                if (cboCategory.Items.Count > 0)
                    cboCategory.SelectedIndex = 0;
                if (cboSupplier.Items.Count > 0)
                    cboSupplier.SelectedIndex = 0;
            }
        }

        private void LoadCategories()
        {
            var dt = CategoryController.GetAll();
            if (dt == null || dt.Rows.Count == 0)
            {
                Helper.ShowWarning("Chưa có danh mục nào! Vui lòng thêm trước.");
                cboCategory.DataSource = null;
                return;
            }
            cboCategory.DataSource = dt;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "CategoryID";
        }

        private void LoadSuppliers()
        {
            var dt = SupplierController.GetAll() ?? new DataTable();

            DataRow empty = dt.NewRow();
            empty["SupplierID"] = 0; // 0 = Không có nhà cung cấp
            empty["SupplierName"] = "-- Không có nhà cung cấp --";
            dt.Rows.InsertAt(empty, 0);

            cboSupplier.DataSource = dt;
            cboSupplier.DisplayMember = "SupplierName";
            cboSupplier.ValueMember = "SupplierID";
            cboSupplier.SelectedIndex = 0;
        }

        private void LoadProductFromDB()
        {
            try
            {
                string sql = "SELECT * FROM Products WHERE ProductID = @id";
                var dt = BaseModel.GetDataTable(sql, new[] { new SqlParameter("@id", ProductId) });
                if (dt.Rows.Count == 0)
                {
                    Helper.ShowError("Không tìm thấy sản phẩm!");
                    this.Close();
                    return;
                }

                DataRow r = dt.Rows[0];

                txtBarcode.Text = r["Barcode"]?.ToString() ?? "";
                txtName.Text = r["ProductName"]?.ToString() ?? "";
                txtUnit.Text = r["Unit"]?.ToString() ?? "Cái";
                txtImportPrice.Text = r["ImportPrice"]?.ToString() ?? "0";
                txtSalePrice.Text = r["SalePrice"]?.ToString() ?? "0";
                txtPromoPrice.Text = r["PromoPrice"] == DBNull.Value ? "" : r["PromoPrice"].ToString();
                txtStock.Text = r["Stock"]?.ToString() ?? "0";
                txtMinStock.Text = r["MinStock"]?.ToString() ?? "5";

                // Khuyến mãi
                bool hasPromo = r["PromoPrice"] != DBNull.Value && Convert.ToDecimal(r["PromoPrice"]) > 0;
                chkPromo.Checked = hasPromo;
                dtpPromoStart.Enabled = dtpPromoEnd.Enabled = hasPromo;
                if (r["PromoStart"] != DBNull.Value) dtpPromoStart.Value = (DateTime)r["PromoStart"];
                if (r["PromoEnd"] != DBNull.Value) dtpPromoEnd.Value = (DateTime)r["PromoEnd"];

                // ComboBox an toàn
                if (cboCategory.DataSource != null)
                    cboCategory.SelectedValue = r["CategoryID"];
                if (cboSupplier.DataSource != null)
                    cboSupplier.SelectedValue = r["SupplierID"] == DBNull.Value ? 0 : r["SupplierID"];
            }
            catch (Exception ex)
            {
                Helper.ShowError("Lỗi khi load sản phẩm: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate bắt buộc
                if (string.IsNullOrWhiteSpace(txtBarcode.Text))
                {
                    Helper.ShowWarning("Vui lòng nhập Barcode!");
                    txtBarcode.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    Helper.ShowWarning("Vui lòng nhập tên sản phẩm!");
                    txtName.Focus();
                    return;
                }
                if (!decimal.TryParse(txtSalePrice.Text, out decimal salePrice) || salePrice <= 0)
                {
                    Helper.ShowWarning("Giá bán không hợp lệ!");
                    txtSalePrice.Focus();
                    return;
                }

                if (!int.TryParse(txtStock.Text, out int stock)) stock = 0;
                if (!int.TryParse(txtMinStock.Text, out int minStock)) minStock = 5;
                decimal importPrice = decimal.TryParse(txtImportPrice.Text, out decimal ip) ? ip : 0m;
                decimal? promoPrice = string.IsNullOrWhiteSpace(txtPromoPrice.Text) ? (decimal?)null : decimal.Parse(txtPromoPrice.Text);

                // Lấy CategoryID an toàn
                int categoryId = 0;
                if (cboCategory.SelectedValue != null && int.TryParse(cboCategory.SelectedValue.ToString(), out int cId))
                    categoryId = cId;
                if (categoryId <= 0 || CategoryController.GetAll().Select("CategoryID=" + categoryId).Length == 0)
                {
                    Helper.ShowWarning("Vui lòng chọn danh mục hợp lệ!");
                    cboCategory.Focus();
                    return;
                }

                // SupplierID an toàn
                object supplierId = (cboSupplier.SelectedValue != null && int.TryParse(cboSupplier.SelectedValue.ToString(), out int sId) && sId > 0)
                    ? (object)sId
                    : (object)DBNull.Value;

                string sql = ProductId == 0 ?
                    @"INSERT INTO Products 
                    (Barcode, ProductName, CategoryID, SupplierID, Unit, ImportPrice, SalePrice, PromoPrice, PromoStart, PromoEnd, Stock, MinStock, IsActive)
                    VALUES (@b,@n,@c,@s,@u,@ip,@sp,@pp,@ps,@pe,@stock,@min,1)" :
                    @"UPDATE Products SET 
                    Barcode=@b, ProductName=@n, CategoryID=@c, SupplierID=@s, Unit=@u,
                    ImportPrice=@ip, SalePrice=@sp, PromoPrice=@pp, PromoStart=@ps, PromoEnd=@pe,
                    Stock=@stock, MinStock=@min
                    WHERE ProductID=@id";

                var parameters = new[]
                {
                    new SqlParameter("@b", txtBarcode.Text.Trim()),
                    new SqlParameter("@n", txtName.Text.Trim()),
                    new SqlParameter("@c", categoryId),
                    new SqlParameter("@s", supplierId),
                    new SqlParameter("@u", txtUnit.Text.Trim()),
                    new SqlParameter("@ip", importPrice),
                    new SqlParameter("@sp", salePrice),
                    new SqlParameter("@pp", promoPrice ?? (object)DBNull.Value),
                    new SqlParameter("@ps", dtpPromoStart.Enabled ? dtpPromoStart.Value.Date : (object)DBNull.Value),
                    new SqlParameter("@pe", dtpPromoEnd.Enabled ? dtpPromoEnd.Value.Date : (object)DBNull.Value),
                    new SqlParameter("@stock", stock),
                    new SqlParameter("@min", minStock),
                    new SqlParameter("@id", ProductId)
                };

                int result = BaseModel.Execute(sql, parameters);
                if (result > 0)
                {
                    Helper.ShowSuccess(ProductId == 0 ? "Thêm sản phẩm thành công!" : "Cập nhật sản phẩm thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    Helper.ShowError("Lưu sản phẩm thất bại!");
                }
            }
            catch (Exception ex)
            {
                Helper.ShowError("Lỗi khi lưu sản phẩm: " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        private void chkPromo_CheckedChanged(object sender, EventArgs e)
        {
            dtpPromoStart.Enabled = dtpPromoEnd.Enabled = chkPromo.Checked;
            if (!chkPromo.Checked) txtPromoPrice.Clear();
        }

        private void NumberOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != '.')
                e.Handled = true;
            if (e.KeyChar == '.' && (sender as TextBox)?.Text.Contains(".") == true)
                e.Handled = true;
        }
    }
}
