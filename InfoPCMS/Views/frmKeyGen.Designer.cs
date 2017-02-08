namespace InfoPCMS.Views
{
    partial class frmKeyGen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKeyGen));
            this.txtKey = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(31, 41);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(328, 20);
            this.txtKey.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter New Key";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::InfoPCMS.Properties.Resources.add_icon;
            this.btnSave.Location = new System.Drawing.Point(192, 67);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(167, 33);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Get Encrypted Key";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.txtKey);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(390, 122);
            this.panelControl1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::InfoPCMS.Properties.Resources.info_pcms;
            this.pictureBox1.Location = new System.Drawing.Point(281, 140);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(121, 29);
            this.pictureBox1.TabIndex = 57;
            this.pictureBox1.TabStop = false;
            // 
            // frmKeyGen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = global::InfoPCMS.Properties.Resources.bg33_2;
            this.ClientSize = new System.Drawing.Size(412, 175);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Metropolis";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "frmKeyGen";
            this.Text = "InfoCRM - KeyGen";
            ((System.ComponentModel.ISupportInitialize)(this.txtKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtKey;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}