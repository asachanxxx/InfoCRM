namespace InfoPCMS.Views
{
    partial class frmInquiryType
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
            this.tabInquiryType = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gridInquiryType = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.InquiryType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CompletionHours = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCompHours = new DevExpress.XtraEditors.TextEdit();
            this.txtCompDays = new DevExpress.XtraEditors.TextEdit();
            this.txtInquiryType = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClearInquiryType = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdateInquryType = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddInquiryType = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cmbxCusCat = new System.Windows.Forms.ComboBox();
            this.txtReminder1 = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtReminder2 = new DevExpress.XtraEditors.TextEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEscalation1 = new DevExpress.XtraEditors.TextEdit();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEscalation2 = new DevExpress.XtraEditors.TextEdit();
            this.label12 = new System.Windows.Forms.Label();
            this.CustomerCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tabInquiryType)).BeginInit();
            this.tabInquiryType.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInquiryType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompHours.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInquiryType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReminder1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReminder2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEscalation1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEscalation2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabInquiryType
            // 
            this.tabInquiryType.Location = new System.Drawing.Point(12, 12);
            this.tabInquiryType.Name = "tabInquiryType";
            this.tabInquiryType.SelectedTabPage = this.xtraTabPage1;
            this.tabInquiryType.Size = new System.Drawing.Size(640, 426);
            this.tabInquiryType.TabIndex = 62;
            this.tabInquiryType.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gridInquiryType);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(638, 401);
            this.xtraTabPage1.Text = "Inquiry Type";
            this.xtraTabPage1.DoubleClick += new System.EventHandler(this.selectInquiryType);
            // 
            // gridInquiryType
            // 
            this.gridInquiryType.Location = new System.Drawing.Point(16, 19);
            this.gridInquiryType.MainView = this.gridView1;
            this.gridInquiryType.Name = "gridInquiryType";
            this.gridInquiryType.Size = new System.Drawing.Size(619, 377);
            this.gridInquiryType.TabIndex = 0;
            this.gridInquiryType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridInquiryType.DoubleClick += new System.EventHandler(this.selectInquiryType);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.InquiryType,
            this.CustomerCategory,
            this.CompletionHours});
            this.gridView1.GridControl = this.gridInquiryType;
            this.gridView1.Name = "gridView1";
            this.gridView1.DoubleClick += new System.EventHandler(this.selectInquiryType);
            // 
            // InquiryType
            // 
            this.InquiryType.Caption = "Inquiry Type";
            this.InquiryType.FieldName = "InquiryType";
            this.InquiryType.Name = "InquiryType";
            this.InquiryType.Visible = true;
            this.InquiryType.VisibleIndex = 0;
            this.InquiryType.Width = 163;
            // 
            // CompletionHours
            // 
            this.CompletionHours.Caption = "Completion Hours";
            this.CompletionHours.FieldName = "CompletionHours";
            this.CompletionHours.Name = "CompletionHours";
            this.CompletionHours.Visible = true;
            this.CompletionHours.VisibleIndex = 2;
            this.CompletionHours.Width = 167;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.label11);
            this.xtraTabPage2.Controls.Add(this.txtEscalation2);
            this.xtraTabPage2.Controls.Add(this.label12);
            this.xtraTabPage2.Controls.Add(this.label9);
            this.xtraTabPage2.Controls.Add(this.txtEscalation1);
            this.xtraTabPage2.Controls.Add(this.label10);
            this.xtraTabPage2.Controls.Add(this.label7);
            this.xtraTabPage2.Controls.Add(this.txtReminder2);
            this.xtraTabPage2.Controls.Add(this.label8);
            this.xtraTabPage2.Controls.Add(this.label6);
            this.xtraTabPage2.Controls.Add(this.txtReminder1);
            this.xtraTabPage2.Controls.Add(this.label4);
            this.xtraTabPage2.Controls.Add(this.label19);
            this.xtraTabPage2.Controls.Add(this.cmbxCusCat);
            this.xtraTabPage2.Controls.Add(this.label3);
            this.xtraTabPage2.Controls.Add(this.label1);
            this.xtraTabPage2.Controls.Add(this.txtCompHours);
            this.xtraTabPage2.Controls.Add(this.txtCompDays);
            this.xtraTabPage2.Controls.Add(this.txtInquiryType);
            this.xtraTabPage2.Controls.Add(this.label5);
            this.xtraTabPage2.Controls.Add(this.label2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(638, 401);
            this.xtraTabPage2.Text = "Add/Edit Inquiry Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(376, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Hours";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(257, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Days";
            // 
            // txtCompHours
            // 
            this.txtCompHours.Location = new System.Drawing.Point(310, 107);
            this.txtCompHours.Name = "txtCompHours";
            this.txtCompHours.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCompHours.Size = new System.Drawing.Size(60, 20);
            this.txtCompHours.TabIndex = 5;
            // 
            // txtCompDays
            // 
            this.txtCompDays.Location = new System.Drawing.Point(191, 107);
            this.txtCompDays.Name = "txtCompDays";
            this.txtCompDays.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCompDays.Size = new System.Drawing.Size(60, 20);
            this.txtCompDays.TabIndex = 1;
            // 
            // txtInquiryType
            // 
            this.txtInquiryType.Location = new System.Drawing.Point(191, 30);
            this.txtInquiryType.Name = "txtInquiryType";
            this.txtInquiryType.Size = new System.Drawing.Size(227, 20);
            this.txtInquiryType.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Completion Days";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Inquiry Type";
            // 
            // btnClearInquiryType
            // 
            this.btnClearInquiryType.Image = global::InfoPCMS.Properties.Resources.cleanup;
            this.btnClearInquiryType.Location = new System.Drawing.Point(10, 110);
            this.btnClearInquiryType.Name = "btnClearInquiryType";
            this.btnClearInquiryType.Size = new System.Drawing.Size(150, 40);
            this.btnClearInquiryType.TabIndex = 66;
            this.btnClearInquiryType.Text = "Clear All";
            this.btnClearInquiryType.Click += new System.EventHandler(this.btnClearInquiryType_Click);
            // 
            // btnUpdateInquryType
            // 
            this.btnUpdateInquryType.Enabled = false;
            this.btnUpdateInquryType.Image = global::InfoPCMS.Properties.Resources.Update;
            this.btnUpdateInquryType.Location = new System.Drawing.Point(10, 60);
            this.btnUpdateInquryType.Name = "btnUpdateInquryType";
            this.btnUpdateInquryType.Size = new System.Drawing.Size(150, 40);
            this.btnUpdateInquryType.TabIndex = 65;
            this.btnUpdateInquryType.Text = "Update";
            this.btnUpdateInquryType.Click += new System.EventHandler(this.btnUpdateInquryType_Click);
            // 
            // btnAddInquiryType
            // 
            this.btnAddInquiryType.Image = global::InfoPCMS.Properties.Resources.info__2_;
            this.btnAddInquiryType.Location = new System.Drawing.Point(10, 10);
            this.btnAddInquiryType.Name = "btnAddInquiryType";
            this.btnAddInquiryType.Size = new System.Drawing.Size(150, 40);
            this.btnAddInquiryType.TabIndex = 69;
            this.btnAddInquiryType.Text = "Add Inquiry Type";
            this.btnAddInquiryType.Click += new System.EventHandler(this.btnAddInquiry_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnAddInquiryType);
            this.panelControl1.Controls.Add(this.pictureBox2);
            this.panelControl1.Controls.Add(this.btnClearInquiryType);
            this.panelControl1.Controls.Add(this.btnUpdateInquryType);
            this.panelControl1.Location = new System.Drawing.Point(658, 36);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(170, 401);
            this.panelControl1.TabIndex = 116;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::InfoPCMS.Properties.Resources.info_pcms;
            this.pictureBox2.Location = new System.Drawing.Point(34, 350);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(133, 46);
            this.pictureBox2.TabIndex = 115;
            this.pictureBox2.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(24, 70);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(101, 13);
            this.label19.TabIndex = 98;
            this.label19.Text = "Customer Category";
            // 
            // cmbxCusCat
            // 
            this.cmbxCusCat.FormattingEnabled = true;
            this.cmbxCusCat.Items.AddRange(new object[] {
            "Non VAT Customer",
            "VAT Only Customer",
            "VAT + NBT Customer",
            "SVAT Customer"});
            this.cmbxCusCat.Location = new System.Drawing.Point(191, 67);
            this.cmbxCusCat.Name = "cmbxCusCat";
            this.cmbxCusCat.Size = new System.Drawing.Size(143, 21);
            this.cmbxCusCat.TabIndex = 97;
            // 
            // txtReminder1
            // 
            this.txtReminder1.Location = new System.Drawing.Point(191, 146);
            this.txtReminder1.Name = "txtReminder1";
            this.txtReminder1.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReminder1.Size = new System.Drawing.Size(60, 20);
            this.txtReminder1.TabIndex = 99;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 100;
            this.label4.Text = "1st Reminder";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(257, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 13);
            this.label6.TabIndex = 101;
            this.label6.Text = "% (During what percentage?)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(257, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 13);
            this.label7.TabIndex = 104;
            this.label7.Text = "% (During what percentage?)";
            // 
            // txtReminder2
            // 
            this.txtReminder2.Location = new System.Drawing.Point(191, 185);
            this.txtReminder2.Name = "txtReminder2";
            this.txtReminder2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReminder2.Size = new System.Drawing.Size(60, 20);
            this.txtReminder2.TabIndex = 102;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 103;
            this.label8.Text = "2nd Reminder";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(257, 226);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(201, 13);
            this.label9.TabIndex = 107;
            this.label9.Text = "hrs (After how many hours of deadline?)";
            // 
            // txtEscalation1
            // 
            this.txtEscalation1.Location = new System.Drawing.Point(191, 223);
            this.txtEscalation1.Name = "txtEscalation1";
            this.txtEscalation1.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEscalation1.Size = new System.Drawing.Size(60, 20);
            this.txtEscalation1.TabIndex = 105;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 226);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 106;
            this.label10.Text = "Escalation 01";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(257, 267);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(224, 13);
            this.label11.TabIndex = 110;
            this.label11.Text = "hrs (After how many hours of escalation 01?)";
            // 
            // txtEscalation2
            // 
            this.txtEscalation2.Location = new System.Drawing.Point(191, 264);
            this.txtEscalation2.Name = "txtEscalation2";
            this.txtEscalation2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEscalation2.Size = new System.Drawing.Size(60, 20);
            this.txtEscalation2.TabIndex = 108;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 267);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 109;
            this.label12.Text = "Escalation 02";
            // 
            // CustomerCategory
            // 
            this.CustomerCategory.Caption = "Customer Category";
            this.CustomerCategory.FieldName = "CategoryName";
            this.CustomerCategory.Name = "CustomerCategory";
            this.CustomerCategory.Visible = true;
            this.CustomerCategory.VisibleIndex = 1;
            // 
            // frmInquiryType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = global::InfoPCMS.Properties.Resources.bg33_2;
            this.ClientSize = new System.Drawing.Size(839, 450);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.tabInquiryType);
            this.LookAndFeel.SkinName = "Metropolis";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "frmInquiryType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InfoCRM - InquiryType";
            ((System.ComponentModel.ISupportInitialize)(this.tabInquiryType)).EndInit();
            this.tabInquiryType.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridInquiryType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompHours.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInquiryType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReminder1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReminder2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEscalation1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEscalation2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabInquiryType;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraGrid.GridControl gridInquiryType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn InquiryType;
        private DevExpress.XtraGrid.Columns.GridColumn CompletionHours;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.TextEdit txtCompDays;
        private DevExpress.XtraEditors.TextEdit txtInquiryType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnClearInquiryType;
        private DevExpress.XtraEditors.SimpleButton btnUpdateInquryType;
        private DevExpress.XtraEditors.SimpleButton btnAddInquiryType;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtCompHours;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbxCusCat;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.TextEdit txtEscalation2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.TextEdit txtEscalation1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TextEdit txtReminder2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtReminder1;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraGrid.Columns.GridColumn CustomerCategory;
    }
}