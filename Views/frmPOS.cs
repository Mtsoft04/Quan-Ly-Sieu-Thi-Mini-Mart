using MiniMartPOS.Controllers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MiniMartPOS.Views
{
    public partial class frmPOS : Form
    {
        private DataTable cart = new DataTable();
        private DataTable products = new DataTable();

        public frmPOS()
        {
            InitializeComponent();
            InitCart();
            LoadCategories();
            LoadSuppliers();
            LoadProducts();
        }

        #region --- Khởi tạo giỏ hàng ---
        private void InitCart()
        {
            cart.Columns.Add("ProductID", typeof(int));
            cart.Columns.Add("ProductName", typeof(string));
            cart.Columns.Add("Quantity", typeof(int));
            cart.Columns.Add("UnitPrice", typeof(decimal));
            cart.Columns.Add("LineTotal", typeof(decimal));

            dgvCart.DataSource = cart;

            dgvCart.Columns["ProductID"].Visible = false;
            dgvCart.Columns["ProductName"].Width = 250;
            dgvCart.Columns["Quantity"].Width = 80;
            dgvCart.Columns["UnitPrice"].Width = 120;
            dgvCart.Columns["LineTotal"].Width = 120;
        }
        #endregion

        #region --- Load sản phẩm ---
        private void LoadProducts(string keyword = "", int categoryId = 0, int supplierId = 0)
        {
            try
            {
                products = ProductController.GetAll();

                if (!string.IsNullOrWhiteSpace(keyword) && products.Rows.Count > 0)
                    products = products.Select($"ProductName LIKE '%{keyword.Replace("'", "''")}%'")
                                       .CopyToDataTable();

                if (categoryId > 0 && products.Columns.Contains("CategoryID"))
                    products = products.Select($"CategoryID = {categoryId}").CopyToDataTable();

                if (supplierId > 0 && products.Columns.Contains("SupplierID"))
                    products = products.Select($"SupplierID = {supplierId}").CopyToDataTable();

                dgvProducts.DataSource = products;

                if (products.Columns.Contains("ProductID"))
                    dgvProducts.Columns["ProductID"].Visible = false;
                if (products.Columns.Contains("ProductName"))
                    dgvProducts.Columns["ProductName"].Width = 250;
                if (products.Columns.Contains("SalePrice"))
                    dgvProducts.Columns["SalePrice"].HeaderText = "Đơn giá";
            }
            catch
            {
                dgvProducts.DataSource = new DataTable();
            }
        }
        #endregion

        #region --- Load Category & Supplier ---
        private void LoadCategories()
        {
            var dt = CategoryController.GetAll();
            cboCategory.DataSource = dt;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "CategoryID";
            cboCategory.SelectedIndex = -1;
        }

        private void LoadSuppliers()
        {
            var dt = SupplierController.GetAll();
            cboSupplier.DataSource = dt;
            cboSupplier.DisplayMember = "SupplierName";
            cboSupplier.ValueMember = "SupplierID";
            cboSupplier.SelectedIndex = -1;
        }
        #endregion

        #region --- Nút Tìm kiếm ---
        private void btnSearch_Click(object sender, EventArgs e)
        {
            int categoryId = cboCategory.SelectedValue is int c ? c : 0;
            int supplierId = cboSupplier.SelectedValue is int s ? s : 0;
            LoadProducts(txtSearch.Text.Trim(), categoryId, supplierId);
        }
        #endregion

        #region --- Thêm sản phẩm vào giỏ ---
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;

            int productId = dgvProducts.CurrentRow.Cells["ProductID"].Value != DBNull.Value
                            ? Convert.ToInt32(dgvProducts.CurrentRow.Cells["ProductID"].Value)
                            : 0;

            string name = dgvProducts.CurrentRow.Cells["ProductName"].Value?.ToString() ?? "";
            decimal price = dgvProducts.CurrentRow.Cells["SalePrice"].Value != DBNull.Value
                            ? Convert.ToDecimal(dgvProducts.CurrentRow.Cells["SalePrice"].Value)
                            : 0;

            if (productId <= 0) return;

            var row = cart.Rows.Cast<DataRow>().FirstOrDefault(r => (int)r["ProductID"] == productId);
            if (row != null)
            {
                row["Quantity"] = (int)row["Quantity"] + 1;
                row["LineTotal"] = (int)row["Quantity"] * price;
            }
            else
            {
                cart.Rows.Add(productId, name, 1, price, price);
            }

            UpdateTotal();
        }
        #endregion

        #region --- Xóa / Xóa hết giỏ ---
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.CurrentRow != null)
            {
                cart.Rows.RemoveAt(dgvCart.CurrentRow.Index);
                UpdateTotal();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (cart.Rows.Count > 0 &&
                MessageBox.Show("Xóa toàn bộ giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                cart.Clear();
                UpdateTotal();
            }
        }
        #endregion

        #region --- Tổng tiền ---
        private void UpdateTotal()
        {
            decimal total = 0;
            foreach (DataRow r in cart.Rows)
            {
                object val = r["LineTotal"];
                total += val == DBNull.Value ? 0 : Convert.ToDecimal(val);
            }

            lblTotal.Text = $"Tổng tiền: {total:N0} đ";
            lblItems.Text = $"Số món: {cart.Rows.Count}";
        }
        #endregion

        #region --- Thanh toán ---
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (cart == null || cart.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống!");
                return;
            }

            bool ok = SaleController.Checkout(cart);
            if (ok)
            {
                cart.Clear();
                UpdateTotal();
            }
        }
        #endregion
    }
}
