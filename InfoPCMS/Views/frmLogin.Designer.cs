namespace InfoPCMS.Views
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.btnlogin = new DevExpress.XtraEditors.SimpleButton();
            this.txtusername = new DevExpress.XtraEditors.TextEdit();
            this.txtpassword = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.chkBxOffline = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.txtusername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpassword.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnlogin
            // 
            this.btnlogin.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnlogin.Appearance.Options.UseFont = true;
            this.btnlogin.Location = new System.Drawing.Point(238, 126);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(130, 40);
            this.btnlogin.TabIndex = 2;
            this.btnlogin.Text = "Login";
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // txtusername
            // 
            this.txtusername.EditValue = "";
            this.txtusername.Location = new System.Drawing.Point(185, 38);
            this.txtusername.Name = "txtusername";
            this.txtusername.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusername.Properties.Appearance.Options.UseFont = true;
            this.txtusername.Size = new System.Drawing.Size(183, 24);
            this.txtusername.TabIndex = 0;
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(185, 73);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpassword.Properties.Appearance.Options.UseFont = true;
            this.txtpassword.Properties.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(183, 24);
            this.txtpassword.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Location = new System.Drawing.Point(101, 39);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 18);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Username";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Location = new System.Drawing.Point(101, 71);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(59, 18);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Password";
            // 
            // chkBxOffline
            // 
            this.chkBxOffline.AutoSize = true;
            this.chkBxOffline.BackColor = System.Drawing.Color.Transparent;
            this.chkBxOffline.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBxOffline.ForeColor = System.Drawing.Color.White;
            this.chkBxOffline.Location = new System.Drawing.Point(273, 102);
            this.chkBxOffline.Name = "chkBxOffline";
            this.chkBxOffline.Size = new System.Drawing.Size(94, 18);
            this.chkBxOffline.TabIndex = 5;
            this.chkBxOffline.Text = "Offline Login";
            this.chkBxOffline.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.chkBxOffline);
            this.panel1.Controls.Add(this.btnlogin);
            this.panel1.Controls.Add(this.txtusername);
            this.panel1.Controls.Add(this.txtpassword);
            this.panel1.Location = new System.Drawing.Point(309, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(546, 205);
            this.panel1.TabIndex = 6;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = global::InfoPCMS.Properties.Resources.Login_Page_Background_1;
            this.ClientSize = new System.Drawing.Size(885, 450);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Metropolis";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InfoCRM - Login";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtusername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpassword.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnlogin;
        private DevExpress.XtraEditors.TextEdit txtusername;
        private DevExpress.XtraEditors.TextEdit txtpassword;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.CheckBox chkBxOffline;
        private System.Windows.Forms.Panel panel1;
    }
}