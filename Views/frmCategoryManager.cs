using MiniMartPOS.Controllers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MiniMartPOS.Views
{
    public partial class frmCategoryManager : Form
    {
        public frmCategoryManager()
        {
            InitializeComponent();

            // Gắn sự kiện nút tìm kiếm
            btnSearch.Click += btnSearch_Click;

            // Nhấn Enter trong TextBox tìm kiếm cũng search
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    LoadCategories(txtSearch.Text.Trim());
                }
            };
        }

        private void frmCategoryManager_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        /// <summary>
        /// Load danh sách danh mục, có thể lọc theo keyword
        /// </summary>
        private void LoadCategories(string keyword = "")
        {
            try
            {
                var dt = CategoryController.GetAll();
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    dt = dt.Select($"CategoryName LIKE '%{keyword.Replace("'", "''")}%'").CopyToDataTable();
                }

                dgvCategories.DataSource = dt;
                dgvCategories.Columns["CategoryID"].Width = 80;
                dgvCategories.Columns["CategoryName"].HeaderText = "Tên danh mục";
                dgvCategories.Columns["IsActive"].Visible = false;
            }
            catch
            {
                // Nếu filter ra không có bản ghi, dt.Select sẽ lỗi, fallback về toàn bộ
                dgvCategories.DataSource = CategoryController.GetAll();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadCategories(txtSearch.Text.Trim());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox(
                "Nhập tên danh mục mới:", "Thêm danh mục", "");

            if (string.IsNullOrWhiteSpace(name)) return;

            if (CategoryController.Add(name))
            {
                Helper.ShowSuccess("Thêm danh mục thành công!");
                LoadCategories();
            }
            else
            {
                Helper.ShowError("Thêm thất bại! Tên có thể đã tồn tại.");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvCategories.CurrentRow.Cells["CategoryID"].Value);
            string oldName = dgvCategories.CurrentRow.Cells["CategoryName"].Value.ToString();

            string newName = Microsoft.VisualBasic.Interaction.InputBox(
                "Sửa tên danh mục:", "Sửa danh mục", oldName);

            if (string.IsNullOrWhiteSpace(newName) || newName == oldName) return;

            if (CategoryController.Update(id, newName))
            {
                Helper.ShowSuccess("Sửa thành công!");
                LoadCategories();
            }
            else
            {
                Helper.ShowError("Sửa thất bại!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvCategories.CurrentRow.Cells["CategoryID"].Value);
            string name = dgvCategories.CurrentRow.Cells["CategoryName"].Value.ToString();

            if (!Helper.AskYesNo($"Xóa danh mục \"{name}\"?\n\nCảnh báo: Các sản phẩm thuộc danh mục này sẽ bị lỗi hiển thị!"))
                return;

            // Xóa mềm: chỉ ẩn đi
            string sql = "UPDATE Categories SET IsActive = 0 WHERE CategoryID = @id";
            if (BaseModel.Execute(sql, new[] { new SqlParameter("@id", id) }) > 0)
            {
                Helper.ShowSuccess("Đã ẩn danh mục!");
                LoadCategories();
            }
            else
            {
                Helper.ShowError("Xóa thất bại!");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadCategories();
        }
    }
}
