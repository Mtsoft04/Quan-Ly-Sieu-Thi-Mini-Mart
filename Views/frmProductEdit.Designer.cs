using System.Windows.Forms;

namespace MiniMartPOS.Views
{
    partial class frmProductEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Tất cả control
        private TextBox txtBarcode;
        private TextBox txtName;
        private TextBox txtUnit;
        private TextBox txtImportPrice;
        private TextBox txtSalePrice;
        private TextBox txtPromoPrice;
        private TextBox txtStock;
        private TextBox txtMinStock;
        private ComboBox cboCategory;
        private ComboBox cboSupplier;
        private DateTimePicker dtpPromoStart;
        private DateTimePicker dtpPromoEnd;
        private CheckBox chkPromo;
        private Button btnSave;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.txtImportPrice = new System.Windows.Forms.TextBox();
            this.txtSalePrice = new System.Windows.Forms.TextBox();
            this.txtPromoPrice = new System.Windows.Forms.TextBox();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.txtMinStock = new System.Windows.Forms.TextBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.cboSupplier = new System.Windows.Forms.ComboBox();
            this.dtpPromoStart = new System.Windows.Forms.DateTimePicker();
            this.dtpPromoEnd = new System.Windows.Forms.DateTimePicker();
            this.chkPromo = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(291, 33);
            this.txtBarcode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(476, 30);
            this.txtBarcode.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(291, 100);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(476, 30);
            this.txtName.TabIndex = 1;
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(291, 167);
            this.txtUnit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(476, 30);
            this.txtUnit.TabIndex = 2;
            // 
            // txtImportPrice
            // 
            this.txtImportPrice.Location = new System.Drawing.Point(291, 367);
            this.txtImportPrice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtImportPrice.Name = "txtImportPrice";
            this.txtImportPrice.Size = new System.Drawing.Size(476, 30);
            this.txtImportPrice.TabIndex = 5;
            // 
            // txtSalePrice
            // 
            this.txtSalePrice.Location = new System.Drawing.Point(291, 433);
            this.txtSalePrice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSalePrice.Name = "txtSalePrice";
            this.txtSalePrice.Size = new System.Drawing.Size(476, 30);
            this.txtSalePrice.TabIndex = 6;
            // 
            // txtPromoPrice
            // 
            this.txtPromoPrice.Location = new System.Drawing.Point(291, 500);
            this.txtPromoPrice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPromoPrice.Name = "txtPromoPrice";
            this.txtPromoPrice.Size = new System.Drawing.Size(476, 30);
            this.txtPromoPrice.TabIndex = 7;
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(291, 750);
            this.txtStock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(476, 30);
            this.txtStock.TabIndex = 11;
            // 
            // txtMinStock
            // 
            this.txtMinStock.Location = new System.Drawing.Point(291, 817);
            this.txtMinStock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMinStock.Name = "txtMinStock";
            this.txtMinStock.Size = new System.Drawing.Size(476, 30);
            this.txtMinStock.TabIndex = 12;
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(291, 233);
            this.cboCategory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(476, 33);
            this.cboCategory.TabIndex = 3;
            // 
            // cboSupplier
            // 
            this.cboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSupplier.FormattingEnabled = true;
            this.cboSupplier.Location = new System.Drawing.Point(291, 300);
            this.cboSupplier.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboSupplier.Name = "cboSupplier";
            this.cboSupplier.Size = new System.Drawing.Size(476, 33);
            this.cboSupplier.TabIndex = 4;
            // 
            // dtpPromoStart
            // 
            this.dtpPromoStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPromoStart.Location = new System.Drawing.Point(291, 617);
            this.dtpPromoStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpPromoStart.Name = "dtpPromoStart";
            this.dtpPromoStart.Size = new System.Drawing.Size(476, 30);
            this.dtpPromoStart.TabIndex = 9;
            // 
            // dtpPromoEnd
            // 
            this.dtpPromoEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPromoEnd.Location = new System.Drawing.Point(291, 683);
            this.dtpPromoEnd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpPromoEnd.Name = "dtpPromoEnd";
            this.dtpPromoEnd.Size = new System.Drawing.Size(476, 30);
            this.dtpPromoEnd.TabIndex = 10;
            // 
            // chkPromo
            // 
            this.chkPromo.AutoSize = true;
            this.chkPromo.Location = new System.Drawing.Point(291, 567);
            this.chkPromo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkPromo.Name = "chkPromo";
            this.chkPromo.Size = new System.Drawing.Size(165, 29);
            this.chkPromo.TabIndex = 8;
            this.chkPromo.Text = "Có khuyến mãi";
            this.chkPromo.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(291, 900);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(206, 75);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "LƯU";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(531, 900);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(206, 75);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "HỦY";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // frmProductEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(830, 1017);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.cboSupplier);
            this.Controls.Add(this.txtImportPrice);
            this.Controls.Add(this.txtSalePrice);
            this.Controls.Add(this.txtPromoPrice);
            this.Controls.Add(this.chkPromo);
            this.Controls.Add(this.dtpPromoStart);
            this.Controls.Add(this.dtpPromoEnd);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.txtMinStock);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmProductEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sản phẩm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}