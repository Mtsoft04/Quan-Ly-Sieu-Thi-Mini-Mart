using MiniMartPOS.Models;
using MiniMartPOS.Views;
using MiniMartPOS.Controllers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MiniMartPOS.Views
{
    public partial class frmUserManager : Form
    {
        public frmUserManager()
        {
            InitializeComponent();

            btnSearch.Click += btnSearch_Click;
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    LoadUsers(txtSearch.Text.Trim());
                }
            };
        }

        private void frmUserManager_Load(object sender, EventArgs e)
        {
            LoadUsers();
            cboRole.DataSource = BaseModel.GetDataTable("SELECT RoleID, RoleName FROM Roles");
            cboRole.DisplayMember = "RoleName";
            cboRole.ValueMember = "RoleID";
        }

        private void LoadUsers(string keyword = "")
        {
            string sql = @"
        SELECT u.UserID, u.Username, u.FullName, u.Phone, r.RoleName, u.IsActive
        FROM Users u
        JOIN Roles r ON u.RoleID = r.RoleID
        ORDER BY u.Username";

            try
            {
                DataTable dt = BaseModel.GetDataTable(sql);

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    var filtered = dt.Select(
                        $"Username LIKE '%{keyword.Replace("'", "''")}%' OR FullName LIKE '%{keyword.Replace("'", "''")}%'");
                    dt = filtered.Length > 0 ? filtered.CopyToDataTable() : dt.Clone();
                }

                dgvUsers.DataSource = dt;

                dgvUsers.Columns["UserID"].Visible = false;
                dgvUsers.Columns["Username"].HeaderText = "Tài khoản";
                dgvUsers.Columns["FullName"].HeaderText = "Họ tên";
                dgvUsers.Columns["Phone"].HeaderText = "Điện thoại";
                dgvUsers.Columns["RoleName"].HeaderText = "Quyền";
                dgvUsers.Columns["IsActive"].HeaderText = "Trạng thái";
                dgvUsers.Columns["IsActive"].Width = 80;
            }
            catch
            {
                // fallback an toàn
                dgvUsers.DataSource = BaseModel.GetDataTable(sql);
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadUsers(txtSearch.Text.Trim());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var f = new frmUserEdit(0))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadUsers();
                    Helper.ShowSuccess("Thêm nhân viên thành công!");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvUsers.CurrentRow.Cells["UserID"].Value);
            using (var f = new frmUserEdit(id))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadUsers();
                    Helper.ShowSuccess("Cập nhật thành công!");
                }
            }
        }

        private void btnToggleActive_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvUsers.CurrentRow.Cells["UserID"].Value);
            bool active = (bool)dgvUsers.CurrentRow.Cells["IsActive"].Value;

            string sql = "UPDATE Users SET IsActive = @active WHERE UserID = @id";
            BaseModel.Execute(sql, new[]
            {
                new SqlParameter("@active", !active),
                new SqlParameter("@id", id)
            });

            LoadUsers();
            Helper.ShowSuccess(active ? "Đã khóa tài khoản!" : "Đã mở khóa tài khoản!");
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvUsers.CurrentRow.Cells["UserID"].Value);
            string username = dgvUsers.CurrentRow.Cells["Username"].Value.ToString();

            using (var f = new frmChangePassword(id, username))
            {
                f.ShowDialog();
            }
        }
    }
}
