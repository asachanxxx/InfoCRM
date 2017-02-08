namespace InfoPCMS.Views
{
    partial class FrmInqNewCstomer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInqNewCstomer));
            this.txtCusName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.txtLocation = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtContactPerson = new DevExpress.XtraEditors.TextEdit();
            this.label9 = new System.Windows.Forms.Label();
            this.txtContactNo = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSaveCustomer = new DevExpress.XtraEditors.SimpleButton();
            this.cmbxNewCusServiceCen = new System.Windows.Forms.ComboBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactPerson.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCusName
            // 
            this.txtCusName.Location = new System.Drawing.Point(140, 30);
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.Size = new System.Drawing.Size(240, 20);
            this.txtCusName.TabIndex = 87;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(12, 33);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(46, 13);
            this.labelControl11.TabIndex = 86;
            this.labelControl11.Text = "Customer";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(140, 122);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(240, 20);
            this.txtLocation.TabIndex = 89;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 125);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 88;
            this.labelControl1.Text = "Location";
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Location = new System.Drawing.Point(140, 60);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(240, 20);
            this.txtContactPerson.TabIndex = 92;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 13);
            this.label9.TabIndex = 91;
            this.label9.Text = "Contact Person Name";
            // 
            // txtContactNo
            // 
            this.txtContactNo.Location = new System.Drawing.Point(140, 90);
            this.txtContactNo.Name = "txtContactNo";
            this.txtContactNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtContactNo.Size = new System.Drawing.Size(240, 20);
            this.txtContactNo.TabIndex = 93;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 94;
            this.label5.Text = "Contact No ";
            // 
            // btnSaveCustomer
            // 
            this.btnSaveCustomer.Image = global::InfoPCMS.Properties.Resources.Ribbon_Save_32x321;
            this.btnSaveCustomer.Location = new System.Drawing.Point(283, 193);
            this.btnSaveCustomer.Name = "btnSaveCustomer";
            this.btnSaveCustomer.Size = new System.Drawing.Size(100, 40);
            this.btnSaveCustomer.TabIndex = 90;
            this.btnSaveCustomer.Text = "Save";
            this.btnSaveCustomer.Click += new System.EventHandler(this.btnSaveCustomer_Click);
            // 
            // cmbxNewCusServiceCen
            // 
            this.cmbxNewCusServiceCen.FormattingEnabled = true;
            this.cmbxNewCusServiceCen.Location = new System.Drawing.Point(140, 153);
            this.cmbxNewCusServiceCen.Name = "cmbxNewCusServiceCen";
            this.cmbxNewCusServiceCen.Size = new System.Drawing.Size(143, 21);
            this.cmbxNewCusServiceCen.TabIndex = 95;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 156);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(71, 13);
            this.labelControl5.TabIndex = 96;
            this.labelControl5.Text = "Service Center";
            // 
            // FrmInqNewCstomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 245);
            this.Controls.Add(this.cmbxNewCusServiceCen);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtContactNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtContactPerson);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSaveCustomer);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtCusName);
            this.Controls.Add(this.labelControl11);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Metropolis";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmInqNewCstomer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Customer";
            this.Load += new System.EventHandler(this.FrmInqNewCstomer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCusName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactPerson.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactNo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtCusName;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.TextEdit txtLocation;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSaveCustomer;
        private DevExpress.XtraEditors.TextEdit txtContactPerson;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.TextEdit txtContactNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbxNewCusServiceCen;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}