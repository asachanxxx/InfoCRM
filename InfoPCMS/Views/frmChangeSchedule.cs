using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using InfoPCMS.Services;

namespace InfoPCMS.Views
{
    public partial class frmChangeSchedule : DevExpress.XtraEditors.XtraForm
    {

        public String AgreementID = "";
        public String ServiceType = "";

        public frmChangeSchedule()
        {
            InitializeComponent();
        }

        private void frmChangeSchedule_Load(object sender, EventArgs e)
        {
            DataTable dtSerDetails = new DatabaseService().executeSelectQuery("SELECT AgreementID,PlanDate,Location,Customer,GeneratorId,Type FROM ServicesShedule where AgreementID='" + AgreementID + "' AND Type = '" + ServiceType + "' ");


            txtCusName.Text = dtSerDetails.Rows[0]["Customer"].ToString();
            txtPlanedDate.Text = dtSerDetails.Rows[0]["PlanDate"].ToString();
            txtLocation.Text = dtSerDetails.Rows[0]["Location"].ToString();
            txtGenNo.Text = dtSerDetails.Rows[0]["GeneratorId"].ToString();
            txtServiceType.Text = dtSerDetails.Rows[0]["Type"].ToString();
        }

       

        private void btnSaveServices_Click(object sender, EventArgs e)
        {
            InfoPCMS.db.executeUpdateQuery("update ServicesShedule set PlanDate = '" + txtPlanedDate.Text + "' where AgreementID='" + AgreementID + "' AND Type = '" + ServiceType + "' ");
            XtraMessageBox.Show("Succesfully Update..");


            FrmServiceAgreenment serAgreement = new FrmServiceAgreenment();
            serAgreement.isschedulechange = true;
            serAgreement.agreementID = AgreementID;
            serAgreement.GenratorID = txtGenNo.Text;
            serAgreement.Show();  

            this.Close();


        }

        
    }
}