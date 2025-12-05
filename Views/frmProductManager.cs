using MiniMartPOS.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MiniMartPOS.Controllers;

namespace MiniMartPOS.Views
{
    public partial class frmProductManager : Form
    {
        public frmProductManager()
        {
            InitializeComponent();

            // Gắn sự kiện nút tìm kiếm
            btnSearch.Click += btnSearch_Click;

            // Nhấn Enter trong TextBox cũng tìm kiếm
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    LoadData();
                }
            };
        }

        private void frmProductManager_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// Load danh sách sản phẩm theo từ khóa
        /// </summary>
        private void LoadData()
        {
            try
            {
                dgvProducts.DataSource = ProductController.Search(txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                Helper.ShowError("Lỗi khi tải dữ liệu sản phẩm: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new frmProductEdit(0))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvProducts.CurrentRow.Cells["ProductID"].Value);
            using (var frm = new frmProductEdit(id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;
            if (!Helper.AskYesNo("Xóa sản phẩm này?")) return;

            try
            {
                int id = Convert.ToInt32(dgvProducts.CurrentRow.Cells["ProductID"].Value);
                string sql = "UPDATE Products SET IsActive = 0 WHERE ProductID = @id";
                BaseModel.Execute(sql, new[] { new SqlParameter("@id", id) });

                LoadData();
                Helper.ShowSuccess("Đã xóa (ẩn) sản phẩm!");
            }
            catch (Exception ex)
            {
                Helper.ShowError("Lỗi khi xóa sản phẩm: " + ex.Message);
            }
        }
    }
}
