using System.Windows.Forms;

namespace MiniMartPOS.Views
{
    partial class frmPOS
    {
        private DataGridView dgvProducts;
        private DataGridView dgvCart;
        private TextBox txtSearch;
        private ComboBox cboCategory;
        private ComboBox cboSupplier;
        private Button btnSearch;
        private Button btnAddToCart;
        private Button btnRemove;
        private Button btnClear;
        private Button btnPay;
        private Label lblTotal;
        private Label lblItems;

        private void InitializeComponent()
        {
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.cboSupplier = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPay = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblItems = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(20, 20);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(361, 30);
            this.txtSearch.TabIndex = 0;
            // 
            // cboCategory
            // 
            this.cboCategory.Location = new System.Drawing.Point(405, 20);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(150, 33);
            this.cboCategory.TabIndex = 1;
            // 
            // cboSupplier
            // 
            this.cboSupplier.Location = new System.Drawing.Point(574, 22);
            this.cboSupplier.Name = "cboSupplier";
            this.cboSupplier.Size = new System.Drawing.Size(150, 33);
            this.cboSupplier.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(739, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(157, 35);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvProducts
            // 
            this.dgvProducts.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvProducts.ColumnHeadersHeight = 29;
            this.dgvProducts.Location = new System.Drawing.Point(20, 72);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersWidth = 51;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(876, 250);
            this.dgvProducts.TabIndex = 4;
            // 
            // dgvCart
            // 
            this.dgvCart.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCart.ColumnHeadersHeight = 29;
            this.dgvCart.Location = new System.Drawing.Point(20, 350);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersWidth = 51;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(876, 200);
            this.dgvCart.TabIndex = 6;
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(204)))), ((int)(((byte)(121)))));
            this.btnAddToCart.ForeColor = System.Drawing.Color.White;
            this.btnAddToCart.Location = new System.Drawing.Point(20, 647);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(171, 60);
            this.btnAddToCart.TabIndex = 5;
            this.btnAddToCart.Text = "Thêm vào giỏ";
            this.btnAddToCart.UseVisualStyleBackColor = false;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(62)))), ((int)(((byte)(49)))));
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(197, 647);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(171, 60);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Xóa dòng";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(169)))), ((int)(((byte)(104)))));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(384, 647);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(171, 60);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Xóa hết";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPay
            // 
            this.btnPay.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnPay.ForeColor = System.Drawing.Color.White;
            this.btnPay.Location = new System.Drawing.Point(574, 638);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(322, 69);
            this.btnPay.TabIndex = 9;
            this.btnPay.Text = "Thanh toán";
            this.btnPay.UseVisualStyleBackColor = false;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Location = new System.Drawing.Point(716, 553);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(180, 23);
            this.lblTotal.TabIndex = 10;
            // 
            // lblItems
            // 
            this.lblItems.Location = new System.Drawing.Point(446, 553);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(180, 23);
            this.lblItems.TabIndex = 11;
            // 
            // frmPOS
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(908, 729);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.cboSupplier);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.btnAddToCart);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblItems);
            this.Controls.Add(this.dgvCart);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmPOS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bán hàng - MiniMart POS";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
