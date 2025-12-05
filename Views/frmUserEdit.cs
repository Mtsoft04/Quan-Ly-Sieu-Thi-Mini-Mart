using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MiniMartPOS.Views
{
    public partial class frmUserEdit : Form
    {
        private int UserID = 0;

        public frmUserEdit(int id = 0)
        {
            UserID = id;
            InitializeComponent();

            // BẮT BUỘC GẮN SỰ KIỆN NÚT VÀ LOAD TRONG CONSTRUCTOR
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            this.Load += frmUserEdit_Load;
        }

        private void frmUserEdit_Load(object sender, EventArgs e)
        {
            LoadRoles(); // Load Role TRƯỚC

            if (UserID > 0)
            {
                LoadUserData();
                txtPassword.Enabled = false;
                txtPassword.Text = "******";
                this.Text = "Sửa nhân viên";
            }
            else
            {
                ClearFields();
                txtPassword.Enabled = true;
                this.Text = "Thêm nhân viên mới";
            }
        }

        private void LoadRoles()
        {
            try
            {
                string sql = "SELECT RoleID, RoleName FROM Roles ORDER BY RoleID";
                DataTable dt = BaseModel.GetDataTable(sql);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có quyền nào!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cboRole.DataSource = null;
                cboRole.Items.Clear();

                cboRole.DataSource = dt;
                cboRole.DisplayMember = "RoleName";
                cboRole.ValueMember = "RoleID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải quyền: " + ex.Message);
            }
        }

        private void LoadUserData()
        {
            string sql = "SELECT * FROM Users WHERE UserID = @id";
            var dt = BaseModel.GetDataTable(sql, new[] { new SqlParameter("@id", UserID) });

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy nhân viên!");
                this.Close();
                return;
            }

            DataRow r = dt.Rows[0];
            txtUsername.Text = r["Username"]?.ToString() ?? "";
            txtFullName.Text = r["FullName"]?.ToString() ?? "";
            txtPhone.Text = r["Phone"]?.ToString() ?? "";
            cboRole.SelectedValue = r["RoleID"];
        }

        private void ClearFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtFullName.Clear();
            txtPhone.Clear();
            cboRole.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (UserID == 0 && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!");
                return;
            }

            if (cboRole.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn quyền!");
                return;
            }

            string sql = UserID == 0 ?
                @"INSERT INTO Users (Username, PasswordHash, FullName, Phone, RoleID, IsActive)
                  VALUES (@u, @p, @f, @ph, @r, 1)" :
                @"UPDATE Users SET Username=@u, FullName=@f, Phone=@ph, RoleID=@r WHERE UserID=@id";

            var p = UserID == 0 ?
                new[] {
                    new SqlParameter("@u", txtUsername.Text.Trim()),
                    new SqlParameter("@p", txtPassword.Text),
                    new SqlParameter("@f", txtFullName.Text.Trim()),
                    new SqlParameter("@ph", string.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim()),
                    new SqlParameter("@r", cboRole.SelectedValue)
                } :
                new[] {
                    new SqlParameter("@u", txtUsername.Text.Trim()),
                    new SqlParameter("@f", txtFullName.Text.Trim()),
                    new SqlParameter("@ph", string.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim()),
                    new SqlParameter("@r", cboRole.SelectedValue),
                    new SqlParameter("@id", UserID)
                };

            try
            {
                if (BaseModel.Execute(sql, p) > 0)
                {
                    MessageBox.Show("Lưu thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}