using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using MiniMartPOS.Controllers;

namespace MiniMartPOS.Views
{
    public partial class frmChangePassword : Form
    {
        private int UserId;
        private string Username;

        public frmChangePassword(int userId, string username)
        {
            UserId = userId;
            Username = username;
            InitializeComponent();
            this.Text = $"Đổi mật khẩu cho: {username}";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNewPass.Text != txtConfirm.Text)
            {
                Helper.ShowError("Mật khẩu xác nhận không khớp!");
                return;
            }
            if (txtNewPass.Text.Length < 3)
            {
                Helper.ShowError("Mật khẩu quá ngắn!");
                return;
            }

            string sql = "UPDATE Users SET PasswordHash = @pass WHERE UserID = @id";
            int rows = BaseModel.Execute(sql, new[]
            {
                new SqlParameter("@pass", txtNewPass.Text),
                new SqlParameter("@id", UserId)
            });

            if (rows > 0)
            {
                Helper.ShowSuccess("Đổi mật khẩu thành công!");
                this.Close();
            }
            else
            {
                Helper.ShowError("Đổi mật khẩu thất bại!");
            }
        }
    }
}