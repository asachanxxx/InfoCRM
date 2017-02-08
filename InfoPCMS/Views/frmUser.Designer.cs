namespace InfoPCMS.Views
{
    partial class frmUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUser));
            this.tabUsers = new DevExpress.XtraTab.XtraTabControl();
            this.tbPgUsers = new DevExpress.XtraTab.XtraTabPage();
            this.gridUsers = new DevExpress.XtraGrid.GridControl();
            this.gridVwUser = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tbPgAddUsers = new DevExpress.XtraTab.XtraTabPage();
            this.lblErrorAddUser = new System.Windows.Forms.Label();
            this.chkBxIsActive = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbUserCat = new System.Windows.Forms.ComboBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbxEmployee = new System.Windows.Forms.ComboBox();
            this.txtCfmPwd = new System.Windows.Forms.TextBox();
            this.lblConPswd = new DevExpress.XtraEditors.LabelControl();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.lblEmpID = new DevExpress.XtraEditors.LabelControl();
            this.lblUsername = new DevExpress.XtraEditors.LabelControl();
            this.lblPswrd = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnAddUsers = new DevExpress.XtraEditors.SimpleButton();
            this.btnChange = new DevExpress.XtraEditors.SimpleButton();
            this.tbPgChangePswd = new DevExpress.XtraTab.XtraTabPage();
            this.txtNewPswdChngPswd = new System.Windows.Forms.TextBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtCurPswdChngPswd = new System.Windows.Forms.TextBox();
            this.txtUsernameChngPswd = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtConPswdChngPswd = new System.Windows.Forms.TextBox();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSaveUser = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearUser = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnSaveChanges = new DevExpress.XtraEditors.SimpleButton();
            this.btnChngePswdClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnBackAddUsers = new DevExpress.XtraEditors.SimpleButton();
            this.btnBackChngePswd = new DevExpress.XtraEditors.SimpleButton();
            this.lblEmpName = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tabUsers)).BeginInit();
            this.tabUsers.SuspendLayout();
            this.tbPgUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVwUser)).BeginInit();
            this.tbPgAddUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tbPgChangePswd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsernameChngPswd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabUsers
            // 
            this.tabUsers.Location = new System.Drawing.Point(7, 9);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.SelectedTabPage = this.tbPgUsers;
            this.tabUsers.Size = new System.Drawing.Size(862, 452);
            this.tabUsers.TabIndex = 62;
            this.tabUsers.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tbPgUsers,
            this.tbPgAddUsers,
            this.tbPgChangePswd});
            // 
            // tbPgUsers
            // 
            this.tbPgUsers.Controls.Add(this.panelControl2);
            this.tbPgUsers.Controls.Add(this.gridUsers);
            this.tbPgUsers.Name = "tbPgUsers";
            this.tbPgUsers.Size = new System.Drawing.Size(860, 427);
            this.tbPgUsers.Text = "Users";
            // 
            // gridUsers
            // 
            this.gridUsers.Location = new System.Drawing.Point(15, 20);
            this.gridUsers.MainView = this.gridVwUser;
            this.gridUsers.Name = "gridUsers";
            this.gridUsers.Size = new System.Drawing.Size(655, 376);
            this.gridUsers.TabIndex = 2;
            this.gridUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVwUser});
            // 
            // gridVwUser
            // 
            this.gridVwUser.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserName,
            this.colEmployee,
            this.colStatus,
            this.colCat});
            this.gridVwUser.GridControl = this.gridUsers;
            this.gridVwUser.Name = "gridVwUser";
            this.gridVwUser.DoubleClick += new System.EventHandler(this.gridVwUser_DoubleClick);
            // 
            // colUserName
            // 
            this.colUserName.Caption = "Username";
            this.colUserName.FieldName = "Username";
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsColumn.AllowEdit = false;
            this.colUserName.OptionsColumn.ReadOnly = true;
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 0;
            this.colUserName.Width = 53;
            // 
            // colEmployee
            // 
            this.colEmployee.Caption = "Employee";
            this.colEmployee.FieldName = "EmployeeName";
            this.colEmployee.Name = "colEmployee";
            this.colEmployee.OptionsColumn.AllowEdit = false;
            this.colEmployee.OptionsColumn.ReadOnly = true;
            this.colEmployee.Visible = true;
            this.colEmployee.VisibleIndex = 1;
            this.colEmployee.Width = 79;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Status";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.OptionsColumn.ReadOnly = true;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 2;
            this.colStatus.Width = 81;
            // 
            // colCat
            // 
            this.colCat.Caption = "Category";
            this.colCat.FieldName = "Category";
            this.colCat.Name = "colCat";
            this.colCat.OptionsColumn.AllowEdit = false;
            this.colCat.OptionsColumn.ReadOnly = true;
            this.colCat.Visible = true;
            this.colCat.VisibleIndex = 3;
            this.colCat.Width = 87;
            // 
            // tbPgAddUsers
            // 
            this.tbPgAddUsers.Controls.Add(this.panelControl1);
            this.tbPgAddUsers.Controls.Add(this.lblErrorAddUser);
            this.tbPgAddUsers.Controls.Add(this.chkBxIsActive);
            this.tbPgAddUsers.Controls.Add(this.label4);
            this.tbPgAddUsers.Controls.Add(this.cmbUserCat);
            this.tbPgAddUsers.Controls.Add(this.labelControl2);
            this.tbPgAddUsers.Controls.Add(this.cmbxEmployee);
            this.tbPgAddUsers.Controls.Add(this.txtCfmPwd);
            this.tbPgAddUsers.Controls.Add(this.lblConPswd);
            this.tbPgAddUsers.Controls.Add(this.txtPwd);
            this.tbPgAddUsers.Controls.Add(this.txtUsername);
            this.tbPgAddUsers.Controls.Add(this.lblEmpID);
            this.tbPgAddUsers.Controls.Add(this.lblUsername);
            this.tbPgAddUsers.Controls.Add(this.lblPswrd);
            this.tbPgAddUsers.Name = "tbPgAddUsers";
            this.tbPgAddUsers.Size = new System.Drawing.Size(860, 427);
            this.tbPgAddUsers.Text = "Add Users";
            // 
            // lblErrorAddUser
            // 
            this.lblErrorAddUser.AutoSize = true;
            this.lblErrorAddUser.ForeColor = System.Drawing.Color.Red;
            this.lblErrorAddUser.Location = new System.Drawing.Point(147, 266);
            this.lblErrorAddUser.Name = "lblErrorAddUser";
            this.lblErrorAddUser.Size = new System.Drawing.Size(153, 13);
            this.lblErrorAddUser.TabIndex = 100;
            this.lblErrorAddUser.Text = "Both Passwords are not match";
            this.lblErrorAddUser.Visible = false;
            // 
            // chkBxIsActive
            // 
            this.chkBxIsActive.AutoSize = true;
            this.chkBxIsActive.Location = new System.Drawing.Point(150, 110);
            this.chkBxIsActive.Name = "chkBxIsActive";
            this.chkBxIsActive.Size = new System.Drawing.Size(15, 14);
            this.chkBxIsActive.TabIndex = 93;
            this.chkBxIsActive.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 99;
            this.label4.Text = "Is Active  :";
            // 
            // cmbUserCat
            // 
            this.cmbUserCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserCat.FormattingEnabled = true;
            this.cmbUserCat.Items.AddRange(new object[] {
            "LEVEL 1",
            "LEVEL 2",
            "LEVEL 3"});
            this.cmbUserCat.Location = new System.Drawing.Point(150, 70);
            this.cmbUserCat.Name = "cmbUserCat";
            this.cmbUserCat.Size = new System.Drawing.Size(184, 21);
            this.cmbUserCat.TabIndex = 92;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 70);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(55, 13);
            this.labelControl2.TabIndex = 98;
            this.labelControl2.Text = "Category  :";
            // 
            // cmbxEmployee
            // 
            this.cmbxEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxEmployee.FormattingEnabled = true;
            this.cmbxEmployee.Location = new System.Drawing.Point(150, 30);
            this.cmbxEmployee.Name = "cmbxEmployee";
            this.cmbxEmployee.Size = new System.Drawing.Size(184, 21);
            this.cmbxEmployee.TabIndex = 88;
            this.cmbxEmployee.SelectedIndexChanged += new System.EventHandler(this.cmbxEmployee_SelectedIndexChanged);
            // 
            // txtCfmPwd
            // 
            this.txtCfmPwd.Location = new System.Drawing.Point(150, 230);
            this.txtCfmPwd.Name = "txtCfmPwd";
            this.txtCfmPwd.PasswordChar = '*';
            this.txtCfmPwd.Size = new System.Drawing.Size(184, 21);
            this.txtCfmPwd.TabIndex = 91;
            // 
            // lblConPswd
            // 
            this.lblConPswd.Location = new System.Drawing.Point(30, 230);
            this.lblConPswd.Name = "lblConPswd";
            this.lblConPswd.Size = new System.Drawing.Size(96, 13);
            this.lblConPswd.TabIndex = 97;
            this.lblConPswd.Text = "Confirm Password  :";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(150, 190);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(184, 21);
            this.txtPwd.TabIndex = 90;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(150, 150);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(184, 20);
            this.txtUsername.TabIndex = 89;
            // 
            // lblEmpID
            // 
            this.lblEmpID.Location = new System.Drawing.Point(30, 30);
            this.lblEmpID.Name = "lblEmpID";
            this.lblEmpID.Size = new System.Drawing.Size(83, 13);
            this.lblEmpID.TabIndex = 96;
            this.lblEmpID.Text = "EmployeeName  :";
            // 
            // lblUsername
            // 
            this.lblUsername.Location = new System.Drawing.Point(30, 150);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 95;
            this.lblUsername.Text = "Username  :";
            // 
            // lblPswrd
            // 
            this.lblPswrd.Location = new System.Drawing.Point(30, 190);
            this.lblPswrd.Name = "lblPswrd";
            this.lblPswrd.Size = new System.Drawing.Size(56, 13);
            this.lblPswrd.TabIndex = 94;
            this.lblPswrd.Text = "Password  :";
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.Controls.Add(this.pictureBox2);
            this.panelControl2.Controls.Add(this.btnAddUsers);
            this.panelControl2.Controls.Add(this.btnChange);
            this.panelControl2.Location = new System.Drawing.Point(687, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(170, 423);
            this.panelControl2.TabIndex = 65;
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
            // btnAddUsers
            // 
            this.btnAddUsers.Image = global::InfoPCMS.Properties.Resources.UserIcon2;
            this.btnAddUsers.Location = new System.Drawing.Point(10, 10);
            this.btnAddUsers.Name = "btnAddUsers";
            this.btnAddUsers.Size = new System.Drawing.Size(150, 40);
            this.btnAddUsers.TabIndex = 0;
            this.btnAddUsers.Text = "Add Users";
            this.btnAddUsers.Click += new System.EventHandler(this.btnAddUsers_Click);
            // 
            // btnChange
            // 
            this.btnChange.Image = global::InfoPCMS.Properties.Resources.Update;
            this.btnChange.Location = new System.Drawing.Point(10, 62);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(150, 40);
            this.btnChange.TabIndex = 1;
            this.btnChange.Text = "Change Password";
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // tbPgChangePswd
            // 
            this.tbPgChangePswd.Controls.Add(this.lblEmpName);
            this.tbPgChangePswd.Controls.Add(this.panelControl3);
            this.tbPgChangePswd.Controls.Add(this.txtConPswdChngPswd);
            this.tbPgChangePswd.Controls.Add(this.labelControl6);
            this.tbPgChangePswd.Controls.Add(this.txtNewPswdChngPswd);
            this.tbPgChangePswd.Controls.Add(this.labelControl3);
            this.tbPgChangePswd.Controls.Add(this.txtCurPswdChngPswd);
            this.tbPgChangePswd.Controls.Add(this.txtUsernameChngPswd);
            this.tbPgChangePswd.Controls.Add(this.labelControl4);
            this.tbPgChangePswd.Controls.Add(this.labelControl5);
            this.tbPgChangePswd.Name = "tbPgChangePswd";
            this.tbPgChangePswd.Size = new System.Drawing.Size(860, 427);
            this.tbPgChangePswd.Text = "Change Password";
            // 
            // txtNewPswdChngPswd
            // 
            this.txtNewPswdChngPswd.Location = new System.Drawing.Point(150, 110);
            this.txtNewPswdChngPswd.Name = "txtNewPswdChngPswd";
            this.txtNewPswdChngPswd.PasswordChar = '*';
            this.txtNewPswdChngPswd.Size = new System.Drawing.Size(184, 21);
            this.txtNewPswdChngPswd.TabIndex = 103;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(30, 110);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(80, 13);
            this.labelControl3.TabIndex = 106;
            this.labelControl3.Text = "New Password  :";
            // 
            // txtCurPswdChngPswd
            // 
            this.txtCurPswdChngPswd.Location = new System.Drawing.Point(150, 70);
            this.txtCurPswdChngPswd.Name = "txtCurPswdChngPswd";
            this.txtCurPswdChngPswd.PasswordChar = '*';
            this.txtCurPswdChngPswd.Size = new System.Drawing.Size(184, 21);
            this.txtCurPswdChngPswd.TabIndex = 102;
            // 
            // txtUsernameChngPswd
            // 
            this.txtUsernameChngPswd.Enabled = false;
            this.txtUsernameChngPswd.Location = new System.Drawing.Point(150, 30);
            this.txtUsernameChngPswd.Name = "txtUsernameChngPswd";
            this.txtUsernameChngPswd.Size = new System.Drawing.Size(184, 20);
            this.txtUsernameChngPswd.TabIndex = 101;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(30, 70);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(96, 13);
            this.labelControl4.TabIndex = 105;
            this.labelControl4.Text = "Current Password  :";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(30, 30);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(62, 13);
            this.labelControl5.TabIndex = 104;
            this.labelControl5.Text = "User Name  :";
            // 
            // txtConPswdChngPswd
            // 
            this.txtConPswdChngPswd.Location = new System.Drawing.Point(150, 150);
            this.txtConPswdChngPswd.Name = "txtConPswdChngPswd";
            this.txtConPswdChngPswd.PasswordChar = '*';
            this.txtConPswdChngPswd.Size = new System.Drawing.Size(184, 21);
            this.txtConPswdChngPswd.TabIndex = 108;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(30, 150);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(96, 13);
            this.labelControl6.TabIndex = 109;
            this.labelControl6.Text = "Confirm Password  :";
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Controls.Add(this.btnBackAddUsers);
            this.panelControl1.Controls.Add(this.pictureBox1);
            this.panelControl1.Controls.Add(this.btnSaveUser);
            this.panelControl1.Controls.Add(this.btnClearUser);
            this.panelControl1.Location = new System.Drawing.Point(687, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(170, 421);
            this.panelControl1.TabIndex = 101;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::InfoPCMS.Properties.Resources.info_pcms;
            this.pictureBox1.Location = new System.Drawing.Point(32, 365);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 46);
            this.pictureBox1.TabIndex = 57;
            this.pictureBox1.TabStop = false;
            // 
            // btnSaveUser
            // 
            this.btnSaveUser.Image = global::InfoPCMS.Properties.Resources.Save_Icon;
            this.btnSaveUser.Location = new System.Drawing.Point(10, 10);
            this.btnSaveUser.Name = "btnSaveUser";
            this.btnSaveUser.Size = new System.Drawing.Size(150, 40);
            this.btnSaveUser.TabIndex = 0;
            this.btnSaveUser.Text = "Save";
            this.btnSaveUser.Click += new System.EventHandler(this.btnSaveUser_Click);
            // 
            // btnClearUser
            // 
            this.btnClearUser.Image = global::InfoPCMS.Properties.Resources.cleanup;
            this.btnClearUser.Location = new System.Drawing.Point(10, 60);
            this.btnClearUser.Name = "btnClearUser";
            this.btnClearUser.Size = new System.Drawing.Size(150, 40);
            this.btnClearUser.TabIndex = 2;
            this.btnClearUser.Text = "Clear All";
            this.btnClearUser.Click += new System.EventHandler(this.btnClearUser_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.panelControl3.Controls.Add(this.btnBackChngePswd);
            this.panelControl3.Controls.Add(this.pictureBox3);
            this.panelControl3.Controls.Add(this.btnSaveChanges);
            this.panelControl3.Controls.Add(this.btnChngePswdClear);
            this.panelControl3.Location = new System.Drawing.Point(687, 3);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(170, 421);
            this.panelControl3.TabIndex = 110;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::InfoPCMS.Properties.Resources.info_pcms;
            this.pictureBox3.Location = new System.Drawing.Point(32, 365);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(133, 46);
            this.pictureBox3.TabIndex = 57;
            this.pictureBox3.TabStop = false;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Image = global::InfoPCMS.Properties.Resources.Save_Icon;
            this.btnSaveChanges.Location = new System.Drawing.Point(10, 10);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(150, 40);
            this.btnSaveChanges.TabIndex = 0;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnChngePswdClear
            // 
            this.btnChngePswdClear.Image = global::InfoPCMS.Properties.Resources.cleanup;
            this.btnChngePswdClear.Location = new System.Drawing.Point(10, 60);
            this.btnChngePswdClear.Name = "btnChngePswdClear";
            this.btnChngePswdClear.Size = new System.Drawing.Size(150, 40);
            this.btnChngePswdClear.TabIndex = 2;
            this.btnChngePswdClear.Text = "Clear All";
            this.btnChngePswdClear.Click += new System.EventHandler(this.btnChngePswdClear_Click);
            // 
            // btnBackAddUsers
            // 
            this.btnBackAddUsers.Image = global::InfoPCMS.Properties.Resources.Back;
            this.btnBackAddUsers.Location = new System.Drawing.Point(10, 110);
            this.btnBackAddUsers.Name = "btnBackAddUsers";
            this.btnBackAddUsers.Size = new System.Drawing.Size(150, 40);
            this.btnBackAddUsers.TabIndex = 58;
            this.btnBackAddUsers.Text = "Back to List";
            this.btnBackAddUsers.Click += new System.EventHandler(this.btnBackAddUsers_Click);
            // 
            // btnBackChngePswd
            // 
            this.btnBackChngePswd.Image = global::InfoPCMS.Properties.Resources.Back;
            this.btnBackChngePswd.Location = new System.Drawing.Point(10, 110);
            this.btnBackChngePswd.Name = "btnBackChngePswd";
            this.btnBackChngePswd.Size = new System.Drawing.Size(150, 40);
            this.btnBackChngePswd.TabIndex = 59;
            this.btnBackChngePswd.Text = "Back to List";
            this.btnBackChngePswd.Click += new System.EventHandler(this.btnBackChngePswd_Click);
            // 
            // lblEmpName
            // 
            this.lblEmpName.Location = new System.Drawing.Point(349, 33);
            this.lblEmpName.Name = "lblEmpName";
            this.lblEmpName.Size = new System.Drawing.Size(62, 13);
            this.lblEmpName.TabIndex = 111;
            this.lblEmpName.Text = "User Name  :";
            // 
            // frmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = global::InfoPCMS.Properties.Resources.bg33_2;
            this.ClientSize = new System.Drawing.Size(881, 468);
            this.Controls.Add(this.tabUsers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Metropolis";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "frmUser";
            this.Text = "InfoCRMS - User Master";
            ((System.ComponentModel.ISupportInitialize)(this.tabUsers)).EndInit();
            this.tabUsers.ResumeLayout(false);
            this.tbPgUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVwUser)).EndInit();
            this.tbPgAddUsers.ResumeLayout(false);
            this.tbPgAddUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tbPgChangePswd.ResumeLayout(false);
            this.tbPgChangePswd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsernameChngPswd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabUsers;
        private DevExpress.XtraTab.XtraTabPage tbPgUsers;
        private DevExpress.XtraTab.XtraTabPage tbPgAddUsers;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private DevExpress.XtraEditors.SimpleButton btnAddUsers;
        private DevExpress.XtraEditors.SimpleButton btnChange;
        private System.Windows.Forms.Label lblErrorAddUser;
        private System.Windows.Forms.CheckBox chkBxIsActive;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbUserCat;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.ComboBox cmbxEmployee;
        private System.Windows.Forms.TextBox txtCfmPwd;
        private DevExpress.XtraEditors.LabelControl lblConPswd;
        private System.Windows.Forms.TextBox txtPwd;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.LabelControl lblEmpID;
        private DevExpress.XtraEditors.LabelControl lblUsername;
        private DevExpress.XtraEditors.LabelControl lblPswrd;
        private DevExpress.XtraGrid.GridControl gridUsers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVwUser;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployee;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCat;
        private DevExpress.XtraTab.XtraTabPage tbPgChangePswd;
        private System.Windows.Forms.TextBox txtNewPswdChngPswd;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.TextBox txtCurPswdChngPswd;
        private DevExpress.XtraEditors.TextEdit txtUsernameChngPswd;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.TextBox txtConPswdChngPswd;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.SimpleButton btnSaveUser;
        private DevExpress.XtraEditors.SimpleButton btnClearUser;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private DevExpress.XtraEditors.SimpleButton btnSaveChanges;
        private DevExpress.XtraEditors.SimpleButton btnChngePswdClear;
        private DevExpress.XtraEditors.SimpleButton btnBackAddUsers;
        private DevExpress.XtraEditors.SimpleButton btnBackChngePswd;
        private DevExpress.XtraEditors.LabelControl lblEmpName;
    }
}