using System;
using System.Windows.Forms;
using MiniMartPOS.Models;
using MiniMartPOS.Views;
using MiniMartPOS.Controllers;

namespace MiniMartPOS.Views
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                Helper.ShowWarning("Vui lòng nhập tài khoản và mật khẩu!");
                return;
            }

            User loginUser;
            if (User.Login(user, pass, out loginUser))
            {
                Helper.ShowSuccess($"Chào mừng {loginUser.FullName}!");
                this.Hide();
                new frmMain().Show();
            }
            else
            {
                Helper.ShowError("Sai tài khoản hoặc mật khẩu!");
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Text = "admin";
            txtPassword.Text = "123456";
            txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLogin.PerformClick();
        }
    }
}