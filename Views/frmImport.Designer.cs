using System.Windows.Forms;

namespace MiniMartPOS.Views
{
    partial class frmImport
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView dgvImports;
        private Button btnAdd;
        private Button btnViewDetail;
        private Button btnRefresh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvImports = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnViewDetail = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImports)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvImports
            // 
            this.dgvImports.AllowUserToAddRows = false;
            this.dgvImports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvImports.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvImports.ColumnHeadersHeight = 29;
            this.dgvImports.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvImports.Location = new System.Drawing.Point(0, 0);
            this.dgvImports.MultiSelect = false;
            this.dgvImports.Name = "dgvImports";
            this.dgvImports.ReadOnly = true;
            this.dgvImports.RowHeadersWidth = 51;
            this.dgvImports.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvImports.Size = new System.Drawing.Size(950, 480);
            this.dgvImports.TabIndex = 3;
            this.dgvImports.DoubleClick += new System.EventHandler(this.dgvImports_DoubleClick);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(12, 490);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(178, 58);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Nhập hàng mới";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnViewDetail
            // 
            this.btnViewDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(62)))), ((int)(((byte)(49)))));
            this.btnViewDetail.ForeColor = System.Drawing.Color.White;
            this.btnViewDetail.Location = new System.Drawing.Point(196, 490);
            this.btnViewDetail.Name = "btnViewDetail";
            this.btnViewDetail.Size = new System.Drawing.Size(178, 58);
            this.btnViewDetail.TabIndex = 1;
            this.btnViewDetail.Text = "Xem chi tiết";
            this.btnViewDetail.UseVisualStyleBackColor = false;
            this.btnViewDetail.Click += new System.EventHandler(this.btnViewDetail_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(169)))), ((int)(((byte)(104)))));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(760, 486);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(178, 58);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmImport
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(950, 560);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnViewDetail);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvImports);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách phiếu nhập hàng";
            this.Load += new System.EventHandler(this.frmImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImports)).EndInit();
            this.ResumeLayout(false);

        }
    }
}