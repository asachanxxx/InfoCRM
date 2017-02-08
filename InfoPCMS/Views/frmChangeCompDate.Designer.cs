namespace InfoPCMS.Views
{
    partial class frmChangeCompDate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangeCompDate));
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.btnSaveCustomer = new DevExpress.XtraEditors.SimpleButton();
            this.lblInqNo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.timeComp = new DevExpress.XtraEditors.TimeEdit();
            this.txtActualProblem = new DevExpress.XtraEditors.MemoEdit();
            this.dateComp = new DevExpress.XtraEditors.DateEdit();
            this.timeAccepted = new DevExpress.XtraEditors.TimeEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateAccepted = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.timeComp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActualProblem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateComp.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateComp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeAccepted.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAccepted.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAccepted.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 105;
            this.label5.Text = "Actual Problem";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 102;
            this.label9.Text = "Accepted Date";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(34, 197);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(117, 13);
            this.labelControl1.TabIndex = 99;
            this.labelControl1.Text = "Agreed Completion Date";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(34, 32);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(50, 13);
            this.labelControl11.TabIndex = 97;
            this.labelControl11.Text = "Inquiry No";
            // 
            // btnSaveCustomer
            // 
            this.btnSaveCustomer.Image = global::InfoPCMS.Properties.Resources.Ribbon_Save_32x321;
            this.btnSaveCustomer.Location = new System.Drawing.Point(325, 265);
            this.btnSaveCustomer.Name = "btnSaveCustomer";
            this.btnSaveCustomer.Size = new System.Drawing.Size(100, 40);
            this.btnSaveCustomer.TabIndex = 101;
            this.btnSaveCustomer.Text = "Save";
            // 
            // lblInqNo
            // 
            this.lblInqNo.Location = new System.Drawing.Point(182, 32);
            this.lblInqNo.Name = "lblInqNo";
            this.lblInqNo.Size = new System.Drawing.Size(50, 13);
            this.lblInqNo.TabIndex = 108;
            this.lblInqNo.Text = "Inquiry No";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(346, 197);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(22, 13);
            this.labelControl4.TabIndex = 110;
            this.labelControl4.Text = "Time";
            // 
            // timeComp
            // 
            this.timeComp.EditValue = new System.DateTime(2014, 5, 30, 0, 0, 0, 0);
            this.timeComp.Location = new System.Drawing.Point(386, 194);
            this.timeComp.Name = "timeComp";
            this.timeComp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeComp.Size = new System.Drawing.Size(95, 20);
            this.timeComp.TabIndex = 112;
            // 
            // txtActualProblem
            // 
            this.txtActualProblem.Location = new System.Drawing.Point(182, 89);
            this.txtActualProblem.Name = "txtActualProblem";
            this.txtActualProblem.Size = new System.Drawing.Size(377, 82);
            this.txtActualProblem.TabIndex = 111;
            // 
            // dateComp
            // 
            this.dateComp.EditValue = null;
            this.dateComp.Location = new System.Drawing.Point(182, 194);
            this.dateComp.Name = "dateComp";
            this.dateComp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateComp.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateComp.Size = new System.Drawing.Size(143, 20);
            this.dateComp.TabIndex = 113;
            // 
            // timeAccepted
            // 
            this.timeAccepted.EditValue = new System.DateTime(2014, 5, 30, 0, 0, 0, 0);
            this.timeAccepted.Location = new System.Drawing.Point(386, 59);
            this.timeAccepted.Name = "timeAccepted";
            this.timeAccepted.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeAccepted.Size = new System.Drawing.Size(95, 20);
            this.timeAccepted.TabIndex = 115;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(346, 62);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(22, 13);
            this.labelControl2.TabIndex = 114;
            this.labelControl2.Text = "Time";
            // 
            // dateAccepted
            // 
            this.dateAccepted.EditValue = null;
            this.dateAccepted.Location = new System.Drawing.Point(182, 59);
            this.dateAccepted.Name = "dateAccepted";
            this.dateAccepted.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateAccepted.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateAccepted.Size = new System.Drawing.Size(143, 20);
            this.dateAccepted.TabIndex = 116;
            // 
            // frmChangeCompDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 351);
            this.Controls.Add(this.dateAccepted);
            this.Controls.Add(this.timeAccepted);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.dateComp);
            this.Controls.Add(this.timeComp);
            this.Controls.Add(this.txtActualProblem);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.lblInqNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSaveCustomer);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl11);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChangeCompDate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Details";
            ((System.ComponentModel.ISupportInitialize)(this.timeComp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActualProblem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateComp.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateComp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeAccepted.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAccepted.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateAccepted.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.SimpleButton btnSaveCustomer;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl lblInqNo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TimeEdit timeComp;
        private DevExpress.XtraEditors.MemoEdit txtActualProblem;
        private DevExpress.XtraEditors.DateEdit dateComp;
        private DevExpress.XtraEditors.TimeEdit timeAccepted;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dateAccepted;
    }
}