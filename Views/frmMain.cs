using System;
using System.Windows.Forms;
using MiniMartPOS.Models;
using MiniMartPOS.Views;
using MiniMartPOS.Controllers;

namespace MiniMartPOS.Views
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.IsMdiContainer = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Chào mừng: {CurrentUser.FullName}";
            lblRole.Text = $"Quyền: {CurrentUser.RoleName}";
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            timer1.Start();

            // Ẩn chức năng Admin nếu không phải Admin
            btnUserManager.Visible = CurrentUser.IsAdmin;
            btnReport.Visible = CurrentUser.IsAdmin;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        // Mở form con (không trùng)
        private void OpenChildForm<T>() where T : Form, new()
        {
            // Xóa form cũ trong pnlContent nếu có
            pnlContent.Controls.Clear();

            var form = new T();
            form.TopLevel = false;           // Không phải form chính
            form.FormBorderStyle = FormBorderStyle.None; // bỏ border
            form.Dock = DockStyle.Fill;      // fill panel
            pnlContent.Controls.Add(form);   // thêm vào panel
            pnlContent.Tag = form;           // optional: lưu reference
            form.Show();
            form.BringToFront();             // đảm bảo nổi trên panel
        }


        private void btnPOS_Click(object sender, EventArgs e) => OpenChildForm<frmPOS>();
        private void btnProduct_Click(object sender, EventArgs e) => OpenChildForm<frmProductManager>();
        private void btnCategory_Click(object sender, EventArgs e) => OpenChildForm<frmCategoryManager>();
        private void btnSupplier_Click(object sender, EventArgs e) => OpenChildForm<frmSupplierManager>();
        private void btnImport_Click(object sender, EventArgs e) => OpenChildForm<frmImport>();
        private void btnOrderHistory_Click(object sender, EventArgs e) => OpenChildForm<frmOrderHistory>();
        private void btnReport_Click(object sender, EventArgs e) => OpenChildForm<frmReportDaily>();
        private void btnUserManager_Click(object sender, EventArgs e) => OpenChildForm<frmUserManager>();

        private void btnChangePass_Click(object sender, EventArgs e) => new frmChangePassword(CurrentUser.UserID, CurrentUser.Username).ShowDialog();

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (Helper.AskYesNo("Đăng xuất khỏi hệ thống?"))
            {
                CurrentUser.Clear();
                this.Close();
                new frmLogin().Show();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (Helper.AskYesNo("Thoát chương trình?"))
                Application.Exit();
        }
    }
}