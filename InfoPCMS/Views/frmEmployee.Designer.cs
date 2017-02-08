namespace InfoPCMS.Views
{
    partial class frmEmployee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmployee));
            this.tabEmployee = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gridItems = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.EmpName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Email = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Department = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Category = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ContactNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.txtCategory = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtJobArea = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAddress = new DevExpress.XtraEditors.MemoEdit();
            this.txtDepartment = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.txtContactNo = new DevExpress.XtraEditors.TextEdit();
            this.txtEmployee = new DevExpress.XtraEditors.TextEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddEmployee = new DevExpress.XtraEditors.SimpleButton();
            this.btnInactive = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.tabEmployee)).BeginInit();
            this.tabEmployee.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtJobArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabEmployee
            // 
            this.tabEmployee.Location = new System.Drawing.Point(8, 10);
            this.tabEmployee.Name = "tabEmployee";
            this.tabEmployee.SelectedTabPage = this.xtraTabPage1;
            this.tabEmployee.Size = new System.Drawing.Size(578, 452);
            this.tabEmployee.TabIndex = 61;
            this.tabEmployee.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gridItems);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(576, 427);
            this.xtraTabPage1.Text = "Employees";
            // 
            // gridItems
            // 
            this.gridItems.Location = new System.Drawing.Point(3, 20);
            this.gridItems.MainView = this.gridView1;
            this.gridItems.Name = "gridItems";
            this.gridItems.Size = new System.Drawing.Size(569, 403);
            this.gridItems.TabIndex = 0;
            this.gridItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridItems.DoubleClick += new System.EventHandler(this.selectEmployee);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.EmpName,
            this.Email,
            this.Department,
            this.Category,
            this.ContactNo});
            this.gridView1.GridControl = this.gridItems;
            this.gridView1.Name = "gridView1";
            // 
            // EmpName
            // 
            this.EmpName.Caption = "Employee";
            this.EmpName.FieldName = "EmployeeName";
            this.EmpName.Name = "EmpName";
            this.EmpName.Visible = true;
            this.EmpName.VisibleIndex = 0;
            this.EmpName.Width = 190;
            // 
            // Email
            // 
            this.Email.Caption = "Email";
            this.Email.FieldName = "Email";
            this.Email.Name = "Email";
            this.Email.Visible = true;
            this.Email.VisibleIndex = 4;
            this.Email.Width = 163;
            // 
            // Department
            // 
            this.Department.Caption = "Department";
            this.Department.FieldName = "Department";
            this.Department.Name = "Department";
            this.Department.Visible = true;
            this.Department.VisibleIndex = 2;
            this.Department.Width = 158;
            // 
            // Category
            // 
            this.Category.Caption = "Category";
            this.Category.FieldName = "Category";
            this.Category.Name = "Category";
            this.Category.Visible = true;
            this.Category.VisibleIndex = 3;
            this.Category.Width = 158;
            // 
            // ContactNo
            // 
            this.ContactNo.Caption = "Contact No";
            this.ContactNo.FieldName = "ContactNumber";
            this.ContactNo.Name = "ContactNo";
            this.ContactNo.Visible = true;
            this.ContactNo.VisibleIndex = 1;
            this.ContactNo.Width = 158;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.txtCategory);
            this.xtraTabPage2.Controls.Add(this.label1);
            this.xtraTabPage2.Controls.Add(this.txtJobArea);
            this.xtraTabPage2.Controls.Add(this.label6);
            this.xtraTabPage2.Controls.Add(this.txtAddress);
            this.xtraTabPage2.Controls.Add(this.txtDepartment);
            this.xtraTabPage2.Controls.Add(this.label4);
            this.xtraTabPage2.Controls.Add(this.txtEmail);
            this.xtraTabPage2.Controls.Add(this.txtContactNo);
            this.xtraTabPage2.Controls.Add(this.txtEmployee);
            this.xtraTabPage2.Controls.Add(this.label8);
            this.xtraTabPage2.Controls.Add(this.label5);
            this.xtraTabPage2.Controls.Add(this.label3);
            this.xtraTabPage2.Controls.Add(this.label2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(576, 427);
            this.xtraTabPage2.Text = "Add/Edit Employee";
            // 
            // txtCategory
            // 
            this.txtCategory.FormattingEnabled = true;
            this.txtCategory.Items.AddRange(new object[] {
            "LEVEL 1",
            "LEVEL 2",
            "LEVEL 3",
            "LEVEL 4"});
            this.txtCategory.Location = new System.Drawing.Point(120, 62);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(143, 21);
            this.txtCategory.TabIndex = 61;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 60;
            this.label1.Text = "Category";
            // 
            // txtJobArea
            // 
            this.txtJobArea.Location = new System.Drawing.Point(120, 293);
            this.txtJobArea.Name = "txtJobArea";
            this.txtJobArea.Size = new System.Drawing.Size(143, 20);
            this.txtJobArea.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 296);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 59;
            this.label6.Text = "Job Area";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(120, 136);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(227, 63);
            this.txtAddress.TabIndex = 2;
            // 
            // txtDepartment
            // 
            this.txtDepartment.Location = new System.Drawing.Point(120, 255);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(143, 20);
            this.txtDepartment.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 258);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 56;
            this.label4.Text = "Department";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(120, 216);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(227, 20);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmail_Validating);
            // 
            // txtContactNo
            // 
            this.txtContactNo.Location = new System.Drawing.Point(120, 101);
            this.txtContactNo.Name = "txtContactNo";
            this.txtContactNo.Size = new System.Drawing.Size(143, 20);
            this.txtContactNo.TabIndex = 1;
            // 
            // txtEmployee
            // 
            this.txtEmployee.Location = new System.Drawing.Point(120, 24);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.Size = new System.Drawing.Size(227, 20);
            this.txtEmployee.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 219);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Email";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Contact No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Employee Name";
            // 
            // btnClear
            // 
            this.btnClear.Image = global::InfoPCMS.Properties.Resources.cleanup;
            this.btnClear.Location = new System.Drawing.Point(10, 160);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(150, 40);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear All";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Image = global::InfoPCMS.Properties.Resources.Update;
            this.btnUpdate.Location = new System.Drawing.Point(10, 60);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(150, 40);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAddEmployee
            // 
            this.btnAddEmployee.Image = global::InfoPCMS.Properties.Resources.AddUser;
            this.btnAddEmployee.Location = new System.Drawing.Point(10, 10);
            this.btnAddEmployee.Name = "btnAddEmployee";
            this.btnAddEmployee.Size = new System.Drawing.Size(150, 40);
            this.btnAddEmployee.TabIndex = 0;
            this.btnAddEmployee.Text = "Add Employee";
            this.btnAddEmployee.Click += new System.EventHandler(this.btnAddInquiry_Click);
            // 
            // btnInactive
            // 
            this.btnInactive.Image = global::InfoPCMS.Properties.Resources.Inactive_Icon;
            this.btnInactive.Location = new System.Drawing.Point(10, 111);
            this.btnInactive.Name = "btnInactive";
            this.btnInactive.Size = new System.Drawing.Size(150, 40);
            this.btnInactive.TabIndex = 63;
            this.btnInactive.Text = "Inactive";
            this.btnInactive.Click += new System.EventHandler(this.btnInactive_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.pictureBox2);
            this.panelControl2.Controls.Add(this.btnInactive);
            this.panelControl2.Controls.Add(this.btnAddEmployee);
            this.panelControl2.Controls.Add(this.btnClear);
            this.panelControl2.Controls.Add(this.btnUpdate);
            this.panelControl2.Location = new System.Drawing.Point(592, 34);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(170, 428);
            this.panelControl2.TabIndex = 64;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::InfoPCMS.Properties.Resources.info_pcms;
            this.pictureBox2.Location = new System.Drawing.Point(32, 365);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(133, 46);
            this.pictureBox2.TabIndex = 57;
            this.pictureBox2.TabStop = false;
            // 
            // frmEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = global::InfoPCMS.Properties.Resources.bg33_2;
            this.ClientSize = new System.Drawing.Size(784, 469);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.tabEmployee);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Metropolis";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "frmEmployee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InfoCRM - Employee";
            this.Load += new System.EventHandler(this.frmEmployee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabEmployee)).EndInit();
            this.tabEmployee.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtJobArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabEmployee;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraGrid.GridControl gridItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.MemoEdit txtAddress;
        private DevExpress.XtraEditors.TextEdit txtDepartment;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.TextEdit txtContactNo;
        private DevExpress.XtraEditors.TextEdit txtEmployee;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.SimpleButton btnAddEmployee;
        private DevExpress.XtraEditors.TextEdit txtJobArea;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraGrid.Columns.GridColumn EmpName;
        private DevExpress.XtraGrid.Columns.GridColumn Email;
        private DevExpress.XtraGrid.Columns.GridColumn Department;
        private DevExpress.XtraGrid.Columns.GridColumn Category;
        private DevExpress.XtraGrid.Columns.GridColumn ContactNo;
        private System.Windows.Forms.ComboBox txtCategory;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnInactive;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.PictureBox pictureBox2;

    }
}