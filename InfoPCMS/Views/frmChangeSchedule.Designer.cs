namespace InfoPCMS.Views
{
    partial class frmChangeSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangeSchedule));
            this.label5 = new System.Windows.Forms.Label();
            this.txtServiceType = new DevExpress.XtraEditors.TextEdit();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLocation = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCusName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.btnSaveServices = new DevExpress.XtraEditors.SimpleButton();
            this.txtPlanedDate = new DevExpress.XtraEditors.DateEdit();
            this.txtGenNo = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtServiceType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlanedDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlanedDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGenNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 103;
            this.label5.Text = "Plan Date";
            // 
            // txtServiceType
            // 
            this.txtServiceType.Enabled = false;
            this.txtServiceType.Location = new System.Drawing.Point(123, 129);
            this.txtServiceType.Name = "txtServiceType";
            this.txtServiceType.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtServiceType.Size = new System.Drawing.Size(147, 20);
            this.txtServiceType.TabIndex = 101;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 100;
            this.label9.Text = "Service Type";
            // 
            // txtLocation
            // 
            this.txtLocation.Enabled = false;
            this.txtLocation.Location = new System.Drawing.Point(123, 75);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(294, 20);
            this.txtLocation.TabIndex = 98;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(23, 78);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 97;
            this.labelControl1.Text = "Location";
            // 
            // txtCusName
            // 
            this.txtCusName.Enabled = false;
            this.txtCusName.Location = new System.Drawing.Point(123, 49);
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.Size = new System.Drawing.Size(330, 20);
            this.txtCusName.TabIndex = 96;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(23, 52);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(76, 13);
            this.labelControl11.TabIndex = 95;
            this.labelControl11.Text = "Customer Name";
            // 
            // btnSaveServices
            // 
            this.btnSaveServices.Image = global::InfoPCMS.Properties.Resources.Ribbon_Save_32x321;
            this.btnSaveServices.Location = new System.Drawing.Point(353, 198);
            this.btnSaveServices.Name = "btnSaveServices";
            this.btnSaveServices.Size = new System.Drawing.Size(100, 40);
            this.btnSaveServices.TabIndex = 99;
            this.btnSaveServices.Text = "Save";
            this.btnSaveServices.Click += new System.EventHandler(this.btnSaveServices_Click);
            // 
            // txtPlanedDate
            // 
            this.txtPlanedDate.EditValue = null;
            this.txtPlanedDate.Location = new System.Drawing.Point(123, 155);
            this.txtPlanedDate.Name = "txtPlanedDate";
            this.txtPlanedDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtPlanedDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPlanedDate.Size = new System.Drawing.Size(147, 20);
            this.txtPlanedDate.TabIndex = 113;
            // 
            // txtGenNo
            // 
            this.txtGenNo.Enabled = false;
            this.txtGenNo.Location = new System.Drawing.Point(123, 103);
            this.txtGenNo.Name = "txtGenNo";
            this.txtGenNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGenNo.Size = new System.Drawing.Size(147, 20);
            this.txtGenNo.TabIndex = 114;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 115;
            this.label1.Text = "Generator No";
            // 
            // frmChangeSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = global::InfoPCMS.Properties.Resources.bg33_2;
            this.ClientSize = new System.Drawing.Size(493, 253);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtGenNo);
            this.Controls.Add(this.txtPlanedDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtServiceType);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSaveServices);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtCusName);
            this.Controls.Add(this.labelControl11);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Metropolis";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "frmChangeSchedule";
            this.Text = "Change Schedule Date";
            this.Load += new System.EventHandler(this.frmChangeSchedule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtServiceType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlanedDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlanedDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGenNo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit txtServiceType;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.SimpleButton btnSaveServices;
        private DevExpress.XtraEditors.TextEdit txtLocation;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCusName;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.DateEdit txtPlanedDate;
        private DevExpress.XtraEditors.TextEdit txtGenNo;
        private System.Windows.Forms.Label label1;
    }
}