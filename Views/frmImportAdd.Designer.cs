using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniMartPOS.Views
{
    partial class frmImportAdd
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblReceiptNo;
        private Label lblUser;
        private Label lblDate;
        private ComboBox cboSupplier;
        private ComboBox cboProduct;
        private Button btnChooseProduct;
        private TextBox txtNote;
        private TextBox txtBarcode;
        private DataGridView dgvCart;
        private Label lblTotal;
        private Button btnSave;
        private Button btnCancel;
        private Button btnRemove;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblReceiptNo = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.cboSupplier = new System.Windows.Forms.ComboBox();
            this.cboProduct = new System.Windows.Forms.ComboBox();
            this.btnChooseProduct = new System.Windows.Forms.Button();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReceiptNo
            // 
            this.lblReceiptNo.AutoSize = true;
            this.lblReceiptNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiptNo.Location = new System.Drawing.Point(12, 9);
            this.lblReceiptNo.Name = "lblReceiptNo";
            this.lblReceiptNo.Size = new System.Drawing.Size(0, 25);
            this.lblReceiptNo.TabIndex = 0;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(345, 14);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(0, 25);
            this.lblUser.TabIndex = 1;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(592, 14);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(0, 25);
            this.lblDate.TabIndex = 2;
            // 
            // cboSupplier
            // 
            this.cboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSupplier.FormattingEnabled = true;
            this.cboSupplier.Location = new System.Drawing.Point(12, 54);
            this.cboSupplier.Name = "cboSupplier";
            this.cboSupplier.Size = new System.Drawing.Size(280, 33);
            this.cboSupplier.TabIndex = 3;
            // 
            // cboProduct
            // 
            this.cboProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProduct.FormattingEnabled = true;
            this.cboProduct.Location = new System.Drawing.Point(307, 55);
            this.cboProduct.Name = "cboProduct";
            this.cboProduct.Size = new System.Drawing.Size(360, 33);
            this.cboProduct.TabIndex = 4;
            // 
            // btnChooseProduct
            // 
            this.btnChooseProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(204)))), ((int)(((byte)(121)))));
            this.btnChooseProduct.ForeColor = System.Drawing.Color.White;
            this.btnChooseProduct.Location = new System.Drawing.Point(672, 56);
            this.btnChooseProduct.Name = "btnChooseProduct";
            this.btnChooseProduct.Size = new System.Drawing.Size(114, 32);
            this.btnChooseProduct.TabIndex = 5;
            this.btnChooseProduct.Text = "Chọn SP";
            this.btnChooseProduct.UseVisualStyleBackColor = false;
            this.btnChooseProduct.Click += new System.EventHandler(this.btnChooseProduct_Click);
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(792, 58);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(140, 30);
            this.txtNote.TabIndex = 6;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtBarcode.Location = new System.Drawing.Point(12, 109);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(920, 39);
            this.txtBarcode.TabIndex = 7;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // dgvCart
            // 
            this.dgvCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCart.ColumnHeadersHeight = 29;
            this.dgvCart.Location = new System.Drawing.Point(12, 154);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersWidth = 51;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(920, 360);
            this.dgvCart.TabIndex = 8;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(5, 546);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(300, 45);
            this.lblTotal.TabIndex = 9;
            this.lblTotal.Text = "TỔNG TIỀN: 0 đ";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(656, 524);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 66);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "LƯU PHIẾU (F10)";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(62)))), ((int)(((byte)(49)))));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(812, 525);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 63);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(169)))), ((int)(((byte)(104)))));
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(507, 525);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(143, 66);
            this.btnRemove.TabIndex = 12;
            this.btnRemove.Text = "Xóa dòng";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // frmImportAdd
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(960, 600);
            this.Controls.Add(this.lblReceiptNo);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.cboSupplier);
            this.Controls.Add(this.cboProduct);
            this.Controls.Add(this.btnChooseProduct);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRemove);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmImportAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo phiếu nhập mới";
            this.Load += new System.EventHandler(this.frmImportAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
