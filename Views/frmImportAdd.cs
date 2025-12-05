using MiniMartPOS.Controllers;
using MiniMartPOS.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace MiniMartPOS.Views
{
    public partial class frmImportAdd : Form
    {
        private int _receiptId = 0;

        public frmImportAdd()
        {
            InitializeComponent();
        }

        // =====================================================
        //  FORM LOAD
        // =====================================================
        private void frmImportAdd_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
            LoadProducts();
            CreateNewReceipt();

            lblUser.Text = "NV: " + CurrentUser.FullName;
            lblDate.Text = "Ngày: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        // =====================================================
        //  LOAD SUPPLIERS
        // =====================================================
        private void LoadSuppliers()
        {
            try
            {
                DataTable dt = BaseModel.GetDataTable("SELECT SupplierID, SupplierName FROM Suppliers ORDER BY SupplierName");

                DataRow empty = dt.NewRow();
                empty["SupplierID"] = DBNull.Value;
                empty["SupplierName"] = "-- Chọn nhà cung cấp --";
                dt.Rows.InsertAt(empty, 0);

                cboSupplier.DataSource = dt;
                cboSupplier.DisplayMember = "SupplierName";
                cboSupplier.ValueMember = "SupplierID";
                cboSupplier.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Helper.ShowError("Lỗi tải nhà cung cấp: " + ex.Message);
            }
        }

        // =====================================================
        //  LOAD PRODUCTS (for cboProduct)
        // =====================================================
        private void LoadProducts()
        {
            try
            {
                DataTable dt = BaseModel.GetDataTable("SELECT ProductID, ProductName, Barcode FROM Products WHERE IsActive = 1 ORDER BY ProductName");

                DataRow empty = dt.NewRow();
                empty["ProductID"] = DBNull.Value;
                empty["ProductName"] = "-- Chọn sản phẩm --";
                empty["Barcode"] = DBNull.Value;
                dt.Rows.InsertAt(empty, 0);

                cboProduct.DataSource = dt;
                cboProduct.DisplayMember = "ProductName";
                cboProduct.ValueMember = "ProductID";
                cboProduct.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Helper.ShowError("Lỗi tải sản phẩm: " + ex.Message);
            }
        }

        // =====================================================
        //  TẠO PHIẾU NHẬP MỚI
        // =====================================================
        private void CreateNewReceipt()
        {
            _receiptId = ImportController.CreateReceipt();

            if (_receiptId == 0)
            {
                MessageBox.Show("Không thể tạo phiếu nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            DataTable dt = BaseModel.GetDataTable("SELECT ReceiptNo FROM ImportReceipts WHERE ReceiptID = @id",
                new[] { new System.Data.SqlClient.SqlParameter("@id", _receiptId) });

            if (dt.Rows.Count > 0)
            {
                lblReceiptNo.Text = "Mã phiếu: " + dt.Rows[0]["ReceiptNo"].ToString();
            }

            LoadCart();
        }

        // =====================================================
        //  LOAD CART (ImportDetails)
        // =====================================================
        private void LoadCart()
        {
            string sql = @"
                SELECT d.ID AS DetailID, p.Barcode, p.ProductName, d.Quantity, d.UnitPrice,
                       (d.Quantity * d.UnitPrice) AS Total
                FROM ImportDetails d
                JOIN Products p ON d.ProductID = p.ProductID
                WHERE d.ReceiptID = @id
                ORDER BY d.ID DESC";

            dgvCart.DataSource = BaseModel.GetDataTable(sql, new[] {
                new System.Data.SqlClient.SqlParameter("@id", _receiptId)
            });

            if (dgvCart.Columns.Contains("DetailID"))
                dgvCart.Columns["DetailID"].Visible = false;

            UpdateTotalAmount();
        }

        // =====================================================
        //  CẬP NHẬT TỔNG TIỀN
        // =====================================================
        private void UpdateTotalAmount()
        {
            string sql = @"SELECT ISNULL(SUM(Quantity * UnitPrice), 0) FROM ImportDetails WHERE ReceiptID = @id";

            object result = BaseModel.ExecuteScalar(sql, new[] {
                new System.Data.SqlClient.SqlParameter("@id", _receiptId)
            });

            decimal total = Convert.ToDecimal(result);
            lblTotal.Text = "TỔNG TIỀN: " + total.ToString("N0") + " đ";
        }

        // =====================================================
        //  XỬ LÝ NHẬP BARCODE (ENTER)
        // =====================================================
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string barcode = txtBarcode.Text.Trim();

            if (string.IsNullOrEmpty(barcode))
            {
                Helper.ShowWarning("Vui lòng nhập barcode!");
                return;
            }

            AddProductToCart(barcode);

            txtBarcode.Clear();
            txtBarcode.Focus();
        }

        // =====================================================
        //  THÊM HÀNG HOÁ THEO BARCODE
        // =====================================================
        private void AddProductToCart(string barcode)
        {
            try
            {
                DataTable dt = BaseModel.GetDataTable(
                    "SELECT ProductID, ProductName, ImportPrice, Barcode FROM Products WHERE Barcode = @bc AND IsActive = 1",
                    new[] { new System.Data.SqlClient.SqlParameter("@bc", barcode) }
                );

                if (dt.Rows.Count == 0)
                {
                    Helper.ShowWarning("Không tìm thấy sản phẩm!");
                    return;
                }

                int productId = Convert.ToInt32(dt.Rows[0]["ProductID"]);
                decimal price = Convert.ToDecimal(dt.Rows[0]["ImportPrice"]);

                // Kiểm tra tồn tại
                DataTable existingCheck = BaseModel.GetDataTable(
                    "SELECT ID AS DetailID, Quantity FROM ImportDetails WHERE ReceiptID = @r AND ProductID = @p",
                    new[] {
                        new System.Data.SqlClient.SqlParameter("@r", _receiptId),
                        new System.Data.SqlClient.SqlParameter("@p", productId)
                    });

                if (existingCheck.Rows.Count > 0)
                {
                    int detailId = Convert.ToInt32(existingCheck.Rows[0]["DetailID"]);
                    int oldQty = Convert.ToInt32(existingCheck.Rows[0]["Quantity"]);

                    string updSql = "UPDATE ImportDetails SET Quantity = @q WHERE ID = @d";
                    BaseModel.Execute(updSql, new[] {
                        new System.Data.SqlClient.SqlParameter("@q", oldQty + 1),
                        new System.Data.SqlClient.SqlParameter("@d", detailId)
                    });
                }
                else
                {
                    bool ok = ImportDetail.Add(_receiptId, productId, 1, price);
                    if (!ok)
                    {
                        Helper.ShowError("Không thể thêm sản phẩm!");
                        return;
                    }
                }

                ImportController.UpdateTotal(_receiptId);
                LoadCart();
            }
            catch (Exception ex)
            {
                Helper.ShowError("Lỗi thêm hàng hóa: " + ex.Message);
            }
        }

        // =====================================================
        //  NÚT CHỌN SẢN PHẨM TỪ cboProduct
        // =====================================================
        private void btnChooseProduct_Click(object sender, EventArgs e)
        {
            if (cboProduct.SelectedIndex <= 0 || cboProduct.SelectedValue == null || cboProduct.SelectedValue == DBNull.Value)
            {
                Helper.ShowWarning("Vui lòng chọn sản phẩm!");
                return;
            }

            var row = (cboProduct.SelectedItem as DataRowView)?.Row;
            if (row == null)
            {
                Helper.ShowError("Không lấy được thông tin sản phẩm!");
                return;
            }

            string barcode = row["Barcode"] == DBNull.Value ? "" : row["Barcode"].ToString();
            if (string.IsNullOrEmpty(barcode))
            {
                int productId = Convert.ToInt32(row["ProductID"]);
                DataTable dtp = BaseModel.GetDataTable("SELECT ImportPrice FROM Products WHERE ProductID = @id",
                    new[] { new System.Data.SqlClient.SqlParameter("@id", productId) });
                if (dtp.Rows.Count == 0)
                {
                    Helper.ShowError("Không lấy được giá sản phẩm!");
                    return;
                }

                decimal price = Convert.ToDecimal(dtp.Rows[0]["ImportPrice"]);

                DataTable existingCheck = BaseModel.GetDataTable(
                    "SELECT ID AS DetailID, Quantity FROM ImportDetails WHERE ReceiptID = @r AND ProductID = @p",
                    new[] {
                        new System.Data.SqlClient.SqlParameter("@r", _receiptId),
                        new System.Data.SqlClient.SqlParameter("@p", productId)
                    });

                if (existingCheck.Rows.Count > 0)
                {
                    int detailId = Convert.ToInt32(existingCheck.Rows[0]["DetailID"]);
                    int oldQty = Convert.ToInt32(existingCheck.Rows[0]["Quantity"]);
                    string updSql = "UPDATE ImportDetails SET Quantity = @q WHERE ID = @d";
                    BaseModel.Execute(updSql, new[] {
                        new System.Data.SqlClient.SqlParameter("@q", oldQty + 1),
                        new System.Data.SqlClient.SqlParameter("@d", detailId)
                    });
                }
                else
                {
                    ImportDetail.Add(_receiptId, productId, 1, price);
                }

                ImportController.UpdateTotal(_receiptId);
                LoadCart();
                return;
            }

            AddProductToCart(barcode);
        }

        // =====================================================
        //  XOÁ DÒNG
        // =====================================================
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0)
            {
                Helper.ShowWarning("Vui lòng chọn dòng muốn xoá!");
                return;
            }

            var cell = dgvCart.SelectedRows[0].Cells["DetailID"];
            if (cell == null || cell.Value == null)
            {
                Helper.ShowError("Không xác định được dòng cần xóa!");
                return;
            }

            int detailId = Convert.ToInt32(cell.Value);

            string sql = "DELETE FROM ImportDetails WHERE ID = @id";
            BaseModel.Execute(sql, new[] {
                new System.Data.SqlClient.SqlParameter("@id", detailId)
            });

            ImportController.UpdateTotal(_receiptId);
            LoadCart();
        }

        // =====================================================
        //  LƯU PHIẾU NHẬP
        // =====================================================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count == 0)
            {
                Helper.ShowWarning("Chưa có hàng hóa!");
                return;
            }

            object supVal = cboSupplier.SelectedValue;
            var supParam = supVal == null || supVal == DBNull.Value ? (object)DBNull.Value : supVal;

            string sql = @"
                UPDATE ImportReceipts SET
                    SupplierID = @sup,
                    Note = @note
                WHERE ReceiptID = @id";

            BaseModel.Execute(sql, new[] {
                new System.Data.SqlClient.SqlParameter("@sup", supParam),
                new System.Data.SqlClient.SqlParameter("@note", txtNote.Text.Trim()),
                new System.Data.SqlClient.SqlParameter("@id", _receiptId)
            });

            ImportController.UpdateTotal(_receiptId);

            Helper.ShowSuccess("Đã lưu phiếu nhập thành công!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // =====================================================
        //  HỦY PHIẾU
        // =====================================================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hủy phiếu nhập này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                BaseModel.Execute("DELETE FROM ImportDetails WHERE ReceiptID = " + _receiptId);
                BaseModel.Execute("DELETE FROM ImportReceipts WHERE ReceiptID = " + _receiptId);
                this.Close();
            }
        }
    }
}
