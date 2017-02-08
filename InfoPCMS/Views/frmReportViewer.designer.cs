namespace InfoPCMS.Views
{
    partial class frmReportViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportViewer));
            this.myCrystalReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // myCrystalReportViewer
            // 
            this.myCrystalReportViewer.ActiveViewIndex = -1;
            this.myCrystalReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myCrystalReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.myCrystalReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myCrystalReportViewer.Location = new System.Drawing.Point(0, 0);
            this.myCrystalReportViewer.Name = "myCrystalReportViewer";
            this.myCrystalReportViewer.Size = new System.Drawing.Size(734, 367);
            this.myCrystalReportViewer.TabIndex = 0;
            this.myCrystalReportViewer.Load += new System.EventHandler(this.myCrystalReportViewer_Load_1);
            // 
            // frmReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 367);
            this.Controls.Add(this.myCrystalReportViewer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Metropolis";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "frmReportViewer";
            this.Text = "InfoInfoCRM - Report Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReportViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer myCrystalReportViewer;

    }
}