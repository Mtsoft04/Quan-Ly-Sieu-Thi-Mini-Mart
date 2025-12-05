using MiniMartPOS.Controllers;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace MiniMartPOS.Views
{
    public partial class frmSupplierManager : Form
    {
        public frmSupplierManager()
        {
            InitializeComponent();

            // Gắn sự kiện tìm kiếm
            btnSearch.Click += btnSearch_Click;

            // Enter trong TextBox cũng tìm kiếm
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    LoadSuppliers(txtSearch.Text.Trim());
                }
            };
        }

        private void frmSupplierManager_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
        }

        private void LoadSuppliers(string keyword = "")
        {
            try
            {
                DataTable dt = SupplierController.GetAll();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    var filtered = dt.Select($"SupplierName LIKE '%{keyword.Replace("'", "''")}%'");
                    if (filtered.Length > 0)
                        dt = filtered.CopyToDataTable();
                    else
                        dt = dt.Clone(); // Không có kết quả vẫn hiển thị bảng trống
                }

                dgvSuppliers.DataSource = dt;

                if (dgvSuppliers.Columns["SupplierID"] != null)
                    dgvSuppliers.Columns["SupplierID"].Width = 80;
                if (dgvSuppliers.Columns["SupplierName"] != null)
                    dgvSuppliers.Columns["SupplierName"].HeaderText = "Tên nhà cung cấp";
                if (dgvSuppliers.Columns["Phone"] != null)
                    dgvSuppliers.Columns["Phone"].HeaderText = "Điện thoại";
                if (dgvSuppliers.Columns["Address"] != null)
                    dgvSuppliers.Columns["Address"].HeaderText = "Địa chỉ";
            }
            catch
            {
                dgvSuppliers.DataSource = SupplierController.GetAll();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSuppliers(txtSearch.Text.Trim());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var f = new frmSupplierEdit(0))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadSuppliers();
                    Helper.ShowSuccess("Thêm nhà cung cấp thành công!");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvSuppliers.CurrentRow.Cells["SupplierID"].Value);
            using (var f = new frmSupplierEdit(id))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadSuppliers();
                    Helper.ShowSuccess("Sửa thành công!");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvSuppliers.CurrentRow.Cells["SupplierID"].Value);
            string name = dgvSuppliers.CurrentRow.Cells["SupplierName"].Value.ToString();

            if (!Helper.AskYesNo($"Bạn có chắc muốn xóa nhà cung cấp \"{name}\"?\n\nCảnh báo: Các sản phẩm thuộc NCC này sẽ bị mất thông tin nhà cung cấp!"))
                return;

            // Xóa thực sự (hoặc UPDATE IsActive = 0 nếu muốn xóa mềm)
            string sql = "DELETE FROM Suppliers WHERE SupplierID = @id";
            int rows = BaseModel.Execute(sql, new[] { new SqlParameter("@id", id) });

            if (rows > 0)
            {
                Helper.ShowSuccess("Đã xóa nhà cung cấp!");
                LoadSuppliers();
            }
            else
            {
                Helper.ShowError("Xóa thất bại!");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadSuppliers();
        }
    }
}
