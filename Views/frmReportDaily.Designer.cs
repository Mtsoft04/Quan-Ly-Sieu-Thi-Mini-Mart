using System.Windows.Forms;
using System.Drawing;

namespace MiniMartPOS.Views
{
    partial class frmReportDaily
    {
        private DateTimePicker dtpDate;
        private Button btnToday;
        private Label lblDate, lblRevenue, lblOrderCount, lblAvgOrder;
        private DataGridView dgvTopProducts, dgvCashiers;
        private Button btnPrint;

        private void InitializeComponent()
        {
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnToday = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblRevenue = new System.Windows.Forms.Label();
            this.lblOrderCount = new System.Windows.Forms.Label();
            this.lblAvgOrder = new System.Windows.Forms.Label();
            this.dgvTopProducts = new System.Windows.Forms.DataGridView();
            this.dgvCashiers = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashiers)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(20, 32);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(340, 30);
            this.dtpDate.TabIndex = 1;
            // 
            // btnToday
            // 
            this.btnToday.Location = new System.Drawing.Point(366, 32);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(126, 30);
            this.btnToday.TabIndex = 2;
            this.btnToday.Text = "Hôm nay";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblDate.Location = new System.Drawing.Point(20, 70);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(0, 37);
            this.lblDate.TabIndex = 0;
            // 
            // lblRevenue
            // 
            this.lblRevenue.BackColor = System.Drawing.SystemColors.Window;
            this.lblRevenue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRevenue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRevenue.Location = new System.Drawing.Point(20, 120);
            this.lblRevenue.Name = "lblRevenue";
            this.lblRevenue.Size = new System.Drawing.Size(509, 60);
            this.lblRevenue.TabIndex = 3;
            // 
            // lblOrderCount
            // 
            this.lblOrderCount.BackColor = System.Drawing.SystemColors.Window;
            this.lblOrderCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderCount.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblOrderCount.Location = new System.Drawing.Point(20, 190);
            this.lblOrderCount.Name = "lblOrderCount";
            this.lblOrderCount.Size = new System.Drawing.Size(429, 30);
            this.lblOrderCount.TabIndex = 4;
            // 
            // lblAvgOrder
            // 
            this.lblAvgOrder.BackColor = System.Drawing.SystemColors.Window;
            this.lblAvgOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgOrder.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblAvgOrder.Location = new System.Drawing.Point(20, 230);
            this.lblAvgOrder.Name = "lblAvgOrder";
            this.lblAvgOrder.Size = new System.Drawing.Size(429, 30);
            this.lblAvgOrder.TabIndex = 5;
            // 
            // dgvTopProducts
            // 
            this.dgvTopProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopProducts.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvTopProducts.ColumnHeadersHeight = 29;
            this.dgvTopProducts.Location = new System.Drawing.Point(20, 280);
            this.dgvTopProducts.Name = "dgvTopProducts";
            this.dgvTopProducts.ReadOnly = true;
            this.dgvTopProducts.RowHeadersWidth = 51;
            this.dgvTopProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopProducts.Size = new System.Drawing.Size(500, 300);
            this.dgvTopProducts.TabIndex = 6;
            // 
            // dgvCashiers
            // 
            this.dgvCashiers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCashiers.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvCashiers.ColumnHeadersHeight = 29;
            this.dgvCashiers.Location = new System.Drawing.Point(540, 280);
            this.dgvCashiers.Name = "dgvCashiers";
            this.dgvCashiers.ReadOnly = true;
            this.dgvCashiers.RowHeadersWidth = 51;
            this.dgvCashiers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCashiers.Size = new System.Drawing.Size(500, 300);
            this.dgvCashiers.TabIndex = 7;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(900, 600);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(140, 50);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "In báo cáo";
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // frmReportDaily
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1060, 670);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.lblRevenue);
            this.Controls.Add(this.lblOrderCount);
            this.Controls.Add(this.lblAvgOrder);
            this.Controls.Add(this.dgvTopProducts);
            this.Controls.Add(this.dgvCashiers);
            this.Controls.Add(this.btnPrint);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmReportDaily";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo doanh thu ngày";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashiers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
