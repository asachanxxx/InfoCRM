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
    public partial class FrmServiceAgreenment : DevExpress.XtraEditors.XtraForm
    {
        public Boolean isschedulechange = false;
        public string agreementID = "";
        public string GenratorID = "";

        public FrmServiceAgreenment()
        {
            InitializeComponent();
            try
            {
                LoadPage();
                DataTable dt3 = InfoPCMS.db.executeSelectQuery("select * from Employee");
                //cboCompletedBy.DataSource = dt3;
                // cboCompletedBy.ValueMember = "Id";
                //cboCompletedBy.DisplayMember = "EmployeeName";

                DataTable dt6 = InfoPCMS.db.executeSelectQuery("select * from Customer");
                cboCustomer.DataSource = dt6;
                cboCustomer.ValueMember = "Id";
                cboCustomer.DisplayMember = "CustomerName";

                DataTable dt5 = InfoPCMS.db.executeSelectQuery("select * from GeneratorDetails");
                cboGeneratorId.DataSource = dt5;
                cboGeneratorId.ValueMember = "GeneratorNumber";
                cboGeneratorId.DisplayMember = "GeneratorNumber";

                DataTable dt7 = InfoPCMS.db.executeSelectQuery("select * from CustomerLocation");
                cboLocation.DataSource = dt7;
                cboLocation.ValueMember = "Id";
                cboLocation.DisplayMember = "SubLocation";
            }
            catch (Exception ex)
            {

            }
        }

        public void LoadPage()
        {
            DataTable dtPendingSerIns = new DatabaseService().executeSelectQuery("SELECT T0.AgreementId,T0.GeneratorId,T1.CustomerName,T2.SubLocation, "
                + " T0.CommercingDate,T0.NoofService,T0.Twentyforhorservice,T0.CostofMinor, "
                + " T0.NoofInspection,T0.material,T0.SpcRequirement,T0.expire, "
                + " T0.Status,T0.PendingService,T0.PendingInspection,T0.Value "
                + " FROM HayleysPowerEngineeringCRM.dbo.SeviceAgreement T0 "
                + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T1 ON T0.Customer = T1.Id "
                + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T2 ON T0.Location = T2.Id "
                + " WHERE T0.Status='Pending' order by T0.AgreementId");

            gridControl1.DataSource = dtPendingSerIns;

            DataTable dtCompSerIns = new DatabaseService().executeSelectQuery("SELECT T0.AgreementId,T0.GeneratorId,T1.CustomerName,T2.SubLocation, "
                + " T0.CommercingDate,T0.NoofService,T0.Twentyforhorservice,T0.CostofMinor, "
                + " T0.NoofInspection,T0.material,T0.SpcRequirement,T0.expire, "
                + " T0.Status,T0.PendingService,T0.PendingInspection,T0.Value "
                + " FROM HayleysPowerEngineeringCRM.dbo.SeviceAgreement T0 "
                + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T1 ON T0.Customer = T1.Id "
                + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T2 ON T0.Location = T2.Id "
                + " WHERE T0.Status='Completed'");

            gridControl2.DataSource = dtCompSerIns;

            tbControlScheduleServices.SelectedTabPageIndex = 0;
            tbPageServiceAgreements.PageEnabled = true;
            tbPageNewServiceAgree.PageEnabled = false;
            tbPageScheduleServices.PageEnabled = false;
            tbPageCompAgreements.PageEnabled = false;


        
        }  

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = InfoPCMS.db.executeSelectQuery("select max(AgreementId) as code from SeviceAgreement");
            string strCode = "";


            if (string.IsNullOrEmpty(dt.Rows[0]["code"].ToString()))
            {
                strCode = "1";
            }
            else
            {
                strCode = (Convert.ToInt16(dt.Rows[0]["code"].ToString()) + 1).ToString();

            }

            if (dt.Rows.Count > 0)
            {



                if (string.IsNullOrEmpty(cboGeneratorId.Text.Trim()) || string.IsNullOrEmpty(txtNofService.Text) || string.IsNullOrEmpty(txtNoofInspection.Text.Trim()) || string.IsNullOrEmpty(cboCustomer.Text) || string.IsNullOrEmpty(cboLocation.Text) || string.IsNullOrEmpty(dateExpire.Text.Trim()) || string.IsNullOrEmpty(dtcCommencingDate.Text.Trim()))
                {
                    XtraMessageBox.Show("Fill All Required Fileds", "Error");

                }

                else
                {

                    // new DatabaseService().executeUpdateQuery(String.Format("INSERT INTO SeviceAgreement VALUES('{0}','{1}','{2}',' {3}','{4}','{5}','{6}','{7}','{8}','{9}','Pending','{10}','{11}','{12}','{13}','{14}')", (Convert.ToInt16(dt.Rows[0]["code"].ToString()) + 1), cboGeneratorId.Text, dtcCommencingDate.Text, txtNofService.Text, chb24hourservice.Checked, chbcostofminor.Checked, txtNoofInspection.Text, chbcostofmaterial.Checked, txtspcRequirement.Text, dateExpire.Text, txtNofService.Text, txtNoofInspection.Text, cboCustomer.Text, cboLocation.Text, txtvalue.Text));
                    new DatabaseService().executeUpdateQuery(String.Format("INSERT INTO SeviceAgreement VALUES('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')", strCode, cboCustomer.SelectedValue.ToString(), cboLocation.SelectedValue.ToString(), cboGeneratorId.SelectedValue.ToString(), dtcCommencingDate.Text, txtNofService.Text, chb24hourservice.Checked, chbcostofminor.Checked, txtNoofInspection.Text, chbcostofmaterial.Checked, txtspcRequirement.Text, dateExpire.Text, "Pending", txtNofService.Text, txtNoofInspection.Text, txtvalue.Text.ToString()));
                    XtraMessageBox.Show("Record Saved Successfully", "Information");
                    //gridControl1.DataSource = new DatabaseService().executeSelectQuery("SELECT * FROM SeviceAgreement where Status='Pending'");

                    LoadPage();

                    Clear();
                    tbControlServiceAgreement.SelectedTabPageIndex = 0;
                    tbPageServiceAgreements.PageEnabled = true;
                    tbPageNewServiceAgree.PageEnabled = false;
                    tbPageScheduleServices.PageEnabled = false;
                    tbPageCompAgreements.PageEnabled = false;
                }
            }

        }

        public void Clear()
        {
            cboGeneratorId.SelectedIndex = -1;
            cboLocation.SelectedIndex = -1;
            cboCustomer.SelectedIndex = -1; ;
            txtvalue.Text = "";
            dtcCommencingDate.Text = "";
            txtNofService.Text = "";
            txtNoofInspection.Text = "";
            chb24hourservice.Checked = false;
            chbcostofmaterial.Checked = false;
            chbcostofminor.Checked = false;
            dateExpire.Text = "";
            txtspcRequirement.Text = "";

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataTable dt = InfoPCMS.db.executeSelectQuery("select* from SeviceAgreement WHERE AgreementId='" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                cboGeneratorId.SelectedValue = dt.Rows[0]["GeneratorId"].ToString();
                dtcCommencingDate.Text = dt.Rows[0]["CommercingDate"].ToString();
                txtNofService.Text = dt.Rows[0]["NoofService"].ToString();
                txtNoofInspection.Text = dt.Rows[0]["NoofInspection"].ToString();
                chb24hourservice.Checked = Convert.ToBoolean(dt.Rows[0]["Twentyforhorservice"].ToString());
                chbcostofmaterial.Checked = Convert.ToBoolean(dt.Rows[0]["material"].ToString());
                chbcostofminor.Checked = Convert.ToBoolean(dt.Rows[0]["CostofMinor"].ToString());
                txtspcRequirement.Text = dt.Rows[0]["SpcRequirement"].ToString();
                dateExpire.Text = dt.Rows[0]["expire"].ToString();
                cboLocation.SelectedValue = dt.Rows[0]["Location"].ToString();
                cboCustomer.SelectedValue = dt.Rows[0]["Customer"].ToString();
                txtvalue.Text = dt.Rows[0]["Value"].ToString();
                tbControlServiceAgreement.SelectedTabPageIndex = 1;
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            tbControlServiceAgreement.SelectedTabPageIndex = 1;
            tbPageServiceAgreements.PageEnabled = false;
            tbPageNewServiceAgree.PageEnabled = true;
            tbPageScheduleServices.PageEnabled = false;
            tbPageCompAgreements.PageEnabled = false;
            //Clearitem();
            Clear();


            dtcCommencingDate.Text = DateTime.Now.ToShortDateString();
            dateExpire.Text = DateTime.Now.ToShortDateString();

            simpleButton1.Enabled = true;

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            LoadPage();
            tbControlServiceAgreement.SelectedTabPageIndex = 3;
            tbPageServiceAgreements.PageEnabled = false;
            tbPageNewServiceAgree.PageEnabled = false;
            tbPageScheduleServices.PageEnabled = false;
            tbPageCompAgreements.PageEnabled = true;
            //gridControl2.DataSource = new DatabaseService().executeSelectQuery("SELECT* FROM SeviceAgreement WHERE Status='Completed'");

        }

        //private void Clearitem() {
        //    lblGenwratorId.Text = "";
        //    lblnumberofservice.Text = "";
        //    lblpendingservice.Text = "";
        //    cbotype.Text = "Service";
        //    dtccompltedate.Text = "";
        //    cboCompletedBy.Text = "";
        //    txtRemarks.Text = "";
        //    lblServiceCode.Text = "";
        //    lblnumberofinspection.Text = "";
        //    lblpendinginspection.Text = "";
        //    cbotype.Focus();

        //}

        //private void simpleButton6_Click(object sender, EventArgs e)
        //{
        //    new DatabaseService().executeSelectQuery("INSERT INTO ServiceItem VALUES('" + lblServiceCode.Text + "','" + cbotype.Text + "','" + dtccompltedate .Text+ "','" + cboCompletedBy.Text + "','" + txtRemarks.Text + "')");
        //    if (cbotype.Text.Equals("Service"))
        //    {
        //        string s = Convert.ToString((Convert.ToInt32(lblpendingservice.Text) - 1));
        //        new DatabaseService().executeSelectQuery("UPDATE SeviceAgreement SET PendingService='" + s + "' WHERE AgreementId='" + lblServiceCode.Text + "'");

        //    }
        //    else
        //    {
        //        string s = Convert.ToString((Convert.ToInt32(lblpendinginspection.Text) - 1));
        //        new DatabaseService().executeSelectQuery("UPDATE SeviceAgreement SET  PendingInspection='" +s + "' WHERE AgreementId='" + lblServiceCode.Text + "'");
        //    }
        //    MessageBox.Show("Record Updated");
        //    Clearitem();
        //    DataTable Dt2 = new DatabaseService().executeSelectQuery("SELECT Remarks, CompletedBy, CompletedDate, Type FROM ServiceItem where AgreementNumber='" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "'");
        //    tblServiceItem.DataSource = Dt2;
        //    gridControl1.DataSource = new DatabaseService().executeSelectQuery("SELECT* FROM SeviceAgreement WHERE Status='Pending'");
        //    xtraTabControl1.SelectedTabPageIndex = 0;
        //}

        //private void simpleButton5_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = new DatabaseService().executeSelectQuery("SELECT AgreementId, GeneratorId, CommercingDate, NoofService, Twentyforhorservice, CostofMinor, NoofInspection, material, SpcRequirement, expire, Status, PendingService, PendingInspection FROM SeviceAgreement where AgreementId='" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "'");
        //    if (!(dt.Rows.Count == 0)) {
        //        lblGenwratorId.Text = dt.Rows[0]["GeneratorId"].ToString();
        //        lblServiceCode.Text = dt.Rows[0]["AgreementId"].ToString();
        //        lblnumberofservice.Text = dt.Rows[0]["NoofService"].ToString();
        //        lblpendingservice.Text = dt.Rows[0]["PendingService"].ToString();
        //        lblnumberofinspection.Text = dt.Rows[0]["NoofInspection"].ToString();
        //        lblpendinginspection.Text = dt.Rows[0]["PendingInspection"].ToString();
        //        cbotype.Focus();
        //        DataTable Dt2 = new DatabaseService().executeSelectQuery("SELECT Remarks, CompletedBy, CompletedDate, Type FROM ServiceItem where AgreementNumber='" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "'");
        //        tblServiceItem.DataSource = Dt2;
        //        xtraTabControl1.SelectedTabPageIndex = 2;
        //    }
        //}

        private void gridView1_DoubleClick_1(object sender, EventArgs e)
        {
            //
            DataTable dt = new DatabaseService().executeSelectQuery("SELECT AgreementId,Customer, GeneratorId,Location, Value,CommercingDate, NoofService, Twentyforhorservice, CostofMinor, NoofInspection, material, SpcRequirement, expire, Status, PendingService, PendingInspection FROM SeviceAgreement where AgreementId='" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "'");
            if (!(dt.Rows.Count == 0))
            {
                cboGeneratorId.SelectedValue = dt.Rows[0]["GeneratorId"].ToString();
                dtcCommencingDate.Text = dt.Rows[0]["CommercingDate"].ToString();
                dateExpire.Text = dt.Rows[0]["expire"].ToString();
                txtNofService.Text = dt.Rows[0]["NoofService"].ToString();
                txtNoofInspection.Text = dt.Rows[0]["NoofInspection"].ToString();
                chb24hourservice.Checked = Convert.ToBoolean(dt.Rows[0]["Twentyforhorservice"].ToString());
                chbcostofminor.Checked = Convert.ToBoolean(dt.Rows[0]["CostofMinor"].ToString());
                chbcostofmaterial.Checked = Convert.ToBoolean(dt.Rows[0]["material"].ToString());
                txtspcRequirement.Text = dt.Rows[0]["SpcRequirement"].ToString();
                cboCustomer.SelectedValue = dt.Rows[0]["Customer"].ToString();
                cboLocation.SelectedValue = dt.Rows[0]["Location"].ToString();
                txtvalue.Text = dt.Rows[0]["Value"].ToString();


                tbControlServiceAgreement.SelectedTabPageIndex = 1;
                tbPageServiceAgreements.PageEnabled = false;
                tbPageNewServiceAgree.PageEnabled = true;
                tbPageScheduleServices.PageEnabled = false;
                tbPageCompAgreements.PageEnabled = false;

                simpleButton1.Enabled = false;
            }
        }

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {

        }

        //private void simpleButton7_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = new DatabaseService().executeSelectQuery("SELECT AgreementId, GeneratorId, CommercingDate, NoofService, Twentyforhorservice, CostofMinor, NoofInspection, material, SpcRequirement, expire, Status, PendingService, PendingInspection FROM SeviceAgreement where AgreementId='" + gridView3.GetFocusedDataRow()["AgreementId"].ToString() + "'");
        //    if (!(dt.Rows.Count == 0))
        //    {
        //        lblServiceCode.Text = dt.Rows[0]["AgreementId"].ToString();
        //        lblnumberofservice.Text = dt.Rows[0]["NoofService"].ToString();
        //        lblpendingservice.Text = dt.Rows[0]["PendingService"].ToString();
        //        lblnumberofinspection.Text = dt.Rows[0]["NoofInspection"].ToString();
        //        lblpendinginspection.Text = dt.Rows[0]["PendingInspection"].ToString();
        //        cbotype.Focus();
        //        DataTable Dt2 = new DatabaseService().executeSelectQuery("SELECT Remarks, CompletedBy, CompletedDate, Type FROM ServiceItem where AgreementNumber='" + gridView3.GetFocusedDataRow()["AgreementId"].ToString() + "'");
        //        tblServiceItem.DataSource = Dt2;
        //        xtraTabControl1.SelectedTabPageIndex = 2;
        //    }
        //}

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            DataTable dt = new DatabaseService().executeSelectQuery("SELECT AgreementId,Customer,Location,Value, GeneratorId, CommercingDate, NoofService, Twentyforhorservice, CostofMinor, NoofInspection, material, SpcRequirement, expire, Status, PendingService, PendingInspection FROM SeviceAgreement where AgreementId='" + gridView3.GetFocusedDataRow()["AgreementId"].ToString() + "'");
            if (!(dt.Rows.Count == 0))
            {
                cboGeneratorId.SelectedValue = dt.Rows[0]["GeneratorId"].ToString();
                dtcCommencingDate.Text = dt.Rows[0]["CommercingDate"].ToString();
                dateExpire.Text = dt.Rows[0]["expire"].ToString();
                txtNofService.Text = dt.Rows[0]["NoofService"].ToString();
                txtNoofInspection.Text = dt.Rows[0]["NoofInspection"].ToString();
                chb24hourservice.Checked = Convert.ToBoolean(dt.Rows[0]["Twentyforhorservice"].ToString());
                chbcostofminor.Checked = Convert.ToBoolean(dt.Rows[0]["CostofMinor"].ToString());
                chbcostofmaterial.Checked = Convert.ToBoolean(dt.Rows[0]["material"].ToString());
                txtspcRequirement.Text = dt.Rows[0]["SpcRequirement"].ToString();
                cboCustomer.SelectedValue = dt.Rows[0]["Customer"].ToString();
                cboLocation.SelectedValue = dt.Rows[0]["Location"].ToString();
                txtvalue.Text = dt.Rows[0]["Value"].ToString();

                tbControlServiceAgreement.SelectedTabPageIndex = 1;
                tbPageServiceAgreements.PageEnabled = false;
                tbPageNewServiceAgree.PageEnabled = true;
                tbPageScheduleServices.PageEnabled = false;
                tbPageCompAgreements.PageEnabled = false;

                simpleButton1.Enabled = false;
            }
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCustomer.SelectedIndex != -1)
            {
                DataTable dt7 = InfoPCMS.db.executeSelectQuery("select * from CustomerLocation where CustomerId='" + cboCustomer.SelectedValue.ToString().Trim() + "' ");
                cboLocation.DataSource = dt7;
                cboLocation.ValueMember = "Id";
                cboLocation.DisplayMember = "SubLocation";
            }
        }

        private void cboLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cboGeneratorId.Text = "";



            //string genDetails = "SELECT GEN.GeneratorNumber,GEN.capcity,(SELECT CommissioningDate FROM HayleysPowerEngineeringCRM.dbo.HandingOverDetails HO "
            //    + " WHERE location='" + cboLocation.Text.Trim() + "' and Customer='" + cboCustomer.Text.Trim() + "' AND GeneratorNumber = GEN.GeneratorNumber) AS CommissioningDate "
            //    + " FROM HayleysPowerEngineeringCRM.dbo.GeneratorDetails GEN  where GEN.location='" + cboLocation.Text.Trim() + "' and GEN.Customer='" + cboCustomer.Text.Trim() + "'";

            //DataTable dtGenDetails = InfoPCMS.db.executeSelectQuery(String.Format(genDetails));

            DataTable dtGenDetails1 = InfoPCMS.db.executeSelectQuery("select * from GeneratorDetails where Customer ='" + cboCustomer.SelectedValue.ToString() + "' AND location = '" + GetCusLocId(cboCustomer.SelectedValue.ToString(), cboLocation.Text) + "' ");



            cboGeneratorId.DataSource = dtGenDetails1;
            cboGeneratorId.ValueMember = "GeneratorNumber";
            cboGeneratorId.DisplayMember = "GeneratorNumber";

        }

        private void FrmServiceAgreenment_Load(object sender, EventArgs e)
        {
            rdo_service.Checked = true;

            //if (isschedulechange)
            //{
            //    DataTable dt = new DatabaseService().executeSelectQuery("SELECT AgreementId,Customer, GeneratorId,Location, CommercingDate, NoofService,  NoofInspection, expire, Status FROM SeviceAgreement where AgreementId='" + agreementID.Trim() + "'");
            //    if (!(dt.Rows.Count == 0))
            //    {
            //        txtSelCusName.Text = dt.Rows[0]["Customer"].ToString();
            //        txtSelLoc.Text = dt.Rows[0]["Location"].ToString();
            //        txtSelGenId.Text = dt.Rows[0]["GeneratorId"].ToString();
            //        txtSelComDate.Text = dt.Rows[0]["CommercingDate"].ToString();
            //        txtSelExpireDate.Text = dt.Rows[0]["expire"].ToString();
            //        txtSelNoSer.Text = dt.Rows[0]["NoofService"].ToString();
            //        txtSelNoInspec.Text = dt.Rows[0]["NoofInspection"].ToString();

            //        tbControlServiceAgreement.SelectedTabPageIndex = 2;

            //        gridLIstServices.DataSource = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule where  TYPE LIKE 'Service%' AND GeneratorId = '" + GenratorID + "'");

            //        gridlistInspection.DataSource = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule where  TYPE LIKE 'Inspec%'  AND GeneratorId = '" + GenratorID + "'");

            //    }

            //}
        }

        private void btnSheduleSer_Click(object sender, EventArgs e)
        {

        }


        private void btnCreateSerSchedule_Click(object sender, EventArgs e)
        {
            // DataTable dt = new DatabaseService().executeSelectQuery("SELECT AgreementId,Customer, GeneratorId,Location, CommercingDate, NoofService,  NoofInspection, expire, Status FROM SeviceAgreement where AgreementId='" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "'");

            DataTable dt = new DatabaseService().executeSelectQuery("SELECT T0.AgreementId,T0.GeneratorId,T0.Customer,T1.CustomerName,T0.Location,T2.SubLocation,"
                + " T0.CommercingDate,T0.NoofService,T0.Twentyforhorservice,T0.CostofMinor,"
                + " T0.NoofInspection,T0.material,T0.SpcRequirement,T0.expire, "
                + " T0.Status,T0.PendingService,T0.PendingInspection,T0.Value "
                + " FROM HayleysPowerEngineeringCRM.dbo.SeviceAgreement T0 "
                + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T1 ON T0.Customer = T1.Id "
                + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T2 ON T0.Location = T2.Id "
                + " WHERE T0.AgreementId='" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "'");

            if (!(dt.Rows.Count == 0))
            {
                lblSelAgreeId.Text = dt.Rows[0]["AgreementId"].ToString();
                txtSelCusName.Text = dt.Rows[0]["CustomerName"].ToString();
                lblSelCusId.Text = dt.Rows[0]["Customer"].ToString();
                txtSelLoc.Text = dt.Rows[0]["SubLocation"].ToString();
                lblSelLocId.Text = dt.Rows[0]["Location"].ToString();

                txtSelGenId.Text = dt.Rows[0]["GeneratorId"].ToString();
                txtSelComDate.Text = dt.Rows[0]["CommercingDate"].ToString();
                txtSelExpireDate.Text = dt.Rows[0]["expire"].ToString();
                txtSelNoSer.Text = dt.Rows[0]["NoofService"].ToString();
                txtSelNoInspec.Text = dt.Rows[0]["NoofInspection"].ToString();

                tbControlServiceAgreement.SelectedTabPageIndex = 2;
                tbPageServiceAgreements.PageEnabled = false;
                tbPageNewServiceAgree.PageEnabled = false;
                tbPageScheduleServices.PageEnabled = true;
                tbPageCompAgreements.PageEnabled = false;

                tbControlScheduleServices.SelectedTabPageIndex = 0;
                tbPageSubScheduleServices.PageEnabled = true;
                tbPageSubChangeSerSchedule.PageEnabled = false;

              //gridLIstServices.DataSource = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule where  TYPE LIKE 'Service%' AND GeneratorId = '" + dt.Rows[0]["GeneratorId"].ToString() + "' AND Customer = '" + dt.Rows[0]["Customer"].ToString() + "' AND Location = '" + dt.Rows[0]["Location"].ToString() + "'");

              //gridlistInspection.DataSource = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule where  TYPE LIKE 'Inspec%'  AND GeneratorId = '" + dt.Rows[0]["GeneratorId"].ToString() + "' AND Customer = '" + dt.Rows[0]["Customer"].ToString() + "' AND Location = '" + dt.Rows[0]["Location"].ToString() + "'");

              gridLIstServices.DataSource = new DatabaseService().executeSelectQuery("SELECT T0.*,T1.EmployeeName FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 "
                 + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 on T0.ResponsiblePerson = T1.Id "
                 + " where  T0.TYPE LIKE 'Service%' AND T0.GeneratorId = '" + dt.Rows[0]["GeneratorId"].ToString() + "' AND T0.Customer = '" + dt.Rows[0]["Customer"].ToString() + "' AND T0.Location = '" + dt.Rows[0]["Location"].ToString() + "' ");

              gridlistInspection.DataSource = new DatabaseService().executeSelectQuery("SELECT T0.*,T1.EmployeeName FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 "
                 + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 on T0.ResponsiblePerson = T1.Id "
                 + " where  T0.TYPE LIKE 'Inspec%' AND T0.GeneratorId = '" + dt.Rows[0]["GeneratorId"].ToString() + "' AND T0.Customer = '" + dt.Rows[0]["Customer"].ToString() + "' AND T0.Location = '" + dt.Rows[0]["Location"].ToString() + "' ");
                 


            }



        }

        private void btnGenSer_Click(object sender, EventArgs e)
        {
            double dbNoOfDates = (Convert.ToDateTime(txtSelExpireDate.Text) - Convert.ToDateTime(txtSelComDate.Text)).TotalDays;

            double dbDatesforOneSer = dbNoOfDates / (Convert.ToDouble(txtSelNoSer.Text) + 1);
            //double dbDatesforOneInsp = dbNoOfDates / (Convert.ToDouble(txtSelNoInspec.Text)+1);

            DataTable dtGenDetails = new DatabaseService().executeSelectQuery("SELECT GeneratorNumber,ExecutiveResponsible FROM GeneratorDetails where GeneratorNumber='" + txtSelGenId.Text + "' AND Customer = '" + GetCusId(txtSelCusName.Text.Trim()) + "' AND location = '" + GetCusLocId(GetCusId(txtSelCusName.Text.Trim()), txtSelLoc.Text.Trim()) + "'");

            string selAgreeID = gridView1.GetFocusedDataRow()["AgreementId"].ToString();


            if (!checkServiceSchduleCreated(gridView1.GetFocusedDataRow()["AgreementId"].ToString()))
            {
                for (int i = 1; i <= Convert.ToInt32(txtSelNoSer.Text); i++)
                {
                    DateTime dtServiceDate = Convert.ToDateTime(txtSelComDate.Text).AddDays(i * dbDatesforOneSer);
                    string strSerType = "Service " + i.ToString();

                    new DatabaseService().executeUpdateQuery("INSERT INTO ServicesShedule VALUES('" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "','" + dtGenDetails.Rows[0]["GeneratorNumber"].ToString() + "','" + GetCusId(txtSelCusName.Text) + "','" + GetCusLocId(GetCusId(txtSelCusName.Text), txtSelLoc.Text) + "','" + strSerType + "','" + dtServiceDate.ToString() + "','" + dtGenDetails.Rows[0]["ExecutiveResponsible"].ToString() + "','','','','','Up Comming')");
                    
                }

                XtraMessageBox.Show("Services Schedule Succesfully Created.", "Error");
            }
            else
            {

                XtraMessageBox.Show("Services Schedule already Created.", "Error");
            }

            //gridLIstServices.DataSource = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule where  TYPE LIKE 'Service%' AND AgreementId = '" + selAgreeID + "'");


            gridLIstServices.DataSource = new DatabaseService().executeSelectQuery("SELECT T0.*,(SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = ResponsiblePerson) AS EmployeeName "
            + " FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 where TYPE LIKE 'Service%'  AND AgreementId = '" + selAgreeID + "' ");



        }

        private bool checkServiceSchduleCreated(string strAgreementId)
        {
            Boolean blSchduleCreated = false;

            DataTable dtSchedulDetails = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule where AgreementID='" + strAgreementId + "' AND TYPE LIKE 'Service%'");
            if (dtSchedulDetails.Rows.Count > 0)
            {
                blSchduleCreated = true;

            }

            return blSchduleCreated;
        }

        private bool checkInsPectionSchduleCreated(string strAgreementId)
        {
            Boolean blSchduleCreated = false;

            DataTable dtSchedulDetails = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule where AgreementID='" + strAgreementId + "' AND TYPE LIKE 'Inspection%'");
            if (dtSchedulDetails.Rows.Count > 0)
            {
                blSchduleCreated = true;

            }

            return blSchduleCreated;
        }

        private void btnGenInspec_Click(object sender, EventArgs e)
        {
            double dbNoOfDates = (Convert.ToDateTime(txtSelExpireDate.Text) - Convert.ToDateTime(txtSelComDate.Text)).TotalDays;

            // double dbDatesforOneSer = dbNoOfDates / (Convert.ToDouble(txtSelNoSer.Text));
            double dbDatesforOneInsp = dbNoOfDates / (Convert.ToDouble(txtSelNoInspec.Text) + 1);

            //DataTable dtGenDetails = new DatabaseService().executeSelectQuery("SELECT GeneratorNumber,ExecutiveResponsible FROM GeneratorDetails where GeneratorNumber='" + txtSelGenID.Text + "' AND Customer = '" + txtSelCus.Text.Trim() + "' AND location = '" + txtSelLocation.Text.Trim() + "'");

            DataTable dtGenDetails = new DatabaseService().executeSelectQuery("SELECT GeneratorNumber,ExecutiveResponsible FROM GeneratorDetails where GeneratorNumber ='" + txtSelGenId.Text + "' AND Customer = '" + GetCusId(txtSelCusName.Text.Trim()) + "' AND location = '" + GetCusLocId(GetCusId(txtSelCusName.Text.Trim()), txtSelLoc.Text.Trim()) + "'");


            string selAgreeID = gridView1.GetFocusedDataRow()["AgreementId"].ToString();


            if (!checkInsPectionSchduleCreated(gridView1.GetFocusedDataRow()["AgreementId"].ToString()))
            {
                for (int i = 1; i <= Convert.ToInt32(txtSelNoInspec.Text); i++)
                {
                    DateTime dtServiceDate = Convert.ToDateTime(txtSelComDate.Text).AddDays(i * dbDatesforOneInsp);
                    string strSerType = "Inspection " + i.ToString();

                    //  new DatabaseService().executeUpdateQuery("INSERT INTO ServicesShedule VALUES('" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "','" + dtServiceDate.ToString() + "','','Up Comming','" + dtGenDetails.Rows[0]["ExecutiveResponsible"].ToString() + "','" + strSerType + "','" + txtSelGenID.Text + "','"+txtSelCus.Text+"','"+txtSelLocation.Text+"','')");

                    new DatabaseService().executeUpdateQuery("INSERT INTO ServicesShedule VALUES('" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "','" + dtGenDetails.Rows[0]["GeneratorNumber"].ToString() + "','" + GetCusId(txtSelCusName.Text) + "','" + GetCusLocId(GetCusId(txtSelCusName.Text), txtSelLoc.Text) + "','" + strSerType + "','" + dtServiceDate.ToString() + "','" + dtGenDetails.Rows[0]["ExecutiveResponsible"].ToString() + "','','','','','Up Comming')");
                    
                }

                XtraMessageBox.Show("Inspection Schedule Succesfully Created.", "Error");

            }
            else
            {
                XtraMessageBox.Show("Inspection Schedule already Created.", "Error");
            }
            
           // gridlistInspection.DataSource = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule where  TYPE LIKE 'Inspec%'  AND AgreementId = '" + selAgreeID + "' ");

            gridlistInspection.DataSource = new DatabaseService().executeSelectQuery("SELECT T0.*,(SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = ResponsiblePerson) AS EmployeeName "
            + " FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 where TYPE LIKE 'Inspec%'  AND AgreementId = '" + selAgreeID + "' ");



        }

        private void btnChange_Click(object sender, EventArgs e)
        {          

            string ServiceType = "";

            if (grdVwServies.SelectedRowsCount > 0)
            {
                ServiceType = grdVwServies.GetFocusedDataRow()["Type"].ToString();
            }
            else if (grdVwInspections.SelectedRowsCount > 0)
            {
                ServiceType = grdVwInspections.GetFocusedDataRow()["Type"].ToString();
            }

            DataTable dtSerDetails = new DatabaseService().executeSelectQuery("SELECT AgreementID,PlanDate,Location,Customer,GeneratorId,Type FROM ServicesShedule where AgreementID='" + lblSelAgreeId.Text + "' AND Type = '" + ServiceType + "' ");
                    


            txtSelChangeCus.Text = GetCusName(dtSerDetails.Rows[0]["Customer"].ToString());
            //txtPlanedDate.Text = dtSerDetails.Rows[0]["PlanDate"].ToString();

            String planDate = dtSerDetails.Rows[0]["PlanDate"].ToString().Trim();
            DateTime dtplanDate = Convert.ToDateTime(planDate);
            txtPlanedDate.EditValue = dtplanDate;
            txtSelChangeLoc.Text =  GetCusLocName(dtSerDetails.Rows[0]["Location"].ToString());
            txtSelChangeGenId.Text = dtSerDetails.Rows[0]["GeneratorId"].ToString();
            txtSelChangeSerType.Text = dtSerDetails.Rows[0]["Type"].ToString();


            tbControlScheduleServices.SelectedTabPageIndex = 1;


        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }


        private string GetCusLocId(string strcusId, string strSubLocName)
        {
            string strSubLocId = "";
            DataTable dtCusSbuLoc = new DatabaseService().executeSelectQuery("select * from CustomerLocation  WHERE CustomerId = '" + strcusId + "' AND SubLocation='" + strSubLocName.Trim() + "'");


            if (dtCusSbuLoc.Rows.Count > 0)
            {

                strSubLocId = dtCusSbuLoc.Rows[0]["Id"].ToString();
            }

            return strSubLocId;
        }

        private string GetCusLocName(string strSubLocId)
        {
            string strSubLocName= "";
            DataTable dtCusSbuLoc = new DatabaseService().executeSelectQuery("select * from CustomerLocation  WHERE Id ='" + strSubLocId.Trim() + "'");


            if (dtCusSbuLoc.Rows.Count > 0)
            {

                strSubLocName = dtCusSbuLoc.Rows[0]["SubLocation"].ToString();
            }

            return strSubLocName;
        }

        private string GetCusId(string strCusName)
        {
            string strCusId = "";
            DataTable dtCustomer = new DatabaseService().executeSelectQuery("select * from Customer  WHERE CustomerName = '" + strCusName + "'");


            if (dtCustomer.Rows.Count > 0)
            {

                strCusId = dtCustomer.Rows[0]["Id"].ToString();
            }

            return strCusId;
        }

        private string GetCusName(string strCusId)
        {
            string strCusName= "";
            DataTable dtCustomer = new DatabaseService().executeSelectQuery("select * from Customer  WHERE Id = '" + strCusId + "'");


            if (dtCustomer.Rows.Count > 0)
            {

                strCusName = dtCustomer.Rows[0]["CustomerName"].ToString();
            }

            return strCusName;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            new DatabaseService().executeSelectQuery("UPDATE SeviceAgreement SET  Status='Completed' WHERE AgreementId='" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "'");
            XtraMessageBox.Show("Agreement Closed Successfully", "Information");

            LoadPage();

            tbControlServiceAgreement.SelectedTabPageIndex = 4;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            InfoPCMS.db.executeUpdateQuery("update ServicesShedule set PlanDate = '" + txtPlanedDate.Text + "' where AgreementID='" + lblSelAgreeId.Text + "' AND Type = '" + txtSelChangeSerType.Text + "' ");
            XtraMessageBox.Show("Succesfully Update..");

             DataTable dt = new DatabaseService().executeSelectQuery("SELECT T0.AgreementId,T0.GeneratorId,T0.Customer,T1.CustomerName,T0.Location,T2.SubLocation,"
                + " T0.CommercingDate,T0.NoofService,T0.Twentyforhorservice,T0.CostofMinor,"
                + " T0.NoofInspection,T0.material,T0.SpcRequirement,T0.expire, "
                + " T0.Status,T0.PendingService,T0.PendingInspection,T0.Value "
                + " FROM HayleysPowerEngineeringCRM.dbo.SeviceAgreement T0 "
                + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T1 ON T0.Customer = T1.Id "
                + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T2 ON T0.Location = T2.Id "
                + " WHERE T0.AgreementId='" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "'");

             if (!(dt.Rows.Count == 0))
             {
                 lblSelAgreeId.Text = dt.Rows[0]["AgreementId"].ToString();
                 txtSelCusName.Text = dt.Rows[0]["CustomerName"].ToString();
                 txtSelLoc.Text = dt.Rows[0]["SubLocation"].ToString();
                 txtSelGenId.Text = dt.Rows[0]["GeneratorId"].ToString();
                 txtSelComDate.Text = dt.Rows[0]["CommercingDate"].ToString();
                 txtSelExpireDate.Text = dt.Rows[0]["expire"].ToString();
                 txtSelNoSer.Text = dt.Rows[0]["NoofService"].ToString();
                 txtSelNoInspec.Text = dt.Rows[0]["NoofInspection"].ToString();

                 tbControlServiceAgreement.SelectedTabPageIndex = 2;
                 tbPageServiceAgreements.PageEnabled = false;
                 tbPageNewServiceAgree.PageEnabled = false;
                 tbPageScheduleServices.PageEnabled = true;
                 tbPageCompAgreements.PageEnabled = false;

                 tbControlScheduleServices.SelectedTabPageIndex = 0;
                 tbPageSubScheduleServices.PageEnabled = true;
                 tbPageSubChangeSerSchedule.PageEnabled = false;

                 //gridLIstServices.DataSource = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule where  TYPE LIKE 'Service%' AND GeneratorId = '" + dt.Rows[0]["GeneratorId"].ToString() + "' AND Customer = '" + dt.Rows[0]["Customer"].ToString() + "' AND Location = '" + dt.Rows[0]["Location"].ToString() + "'");

                 gridLIstServices.DataSource = new DatabaseService().executeSelectQuery("SELECT T0.*,T1.EmployeeName FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 on T0.ResponsiblePerson = T1.Id "
                    + " where  T0.TYPE LIKE 'Service%' AND T0.GeneratorId = '" + dt.Rows[0]["GeneratorId"].ToString() + "' AND T0.Customer = '" + dt.Rows[0]["Customer"].ToString() + "' AND T0.Location = '" + dt.Rows[0]["Location"].ToString() + "' ");

                 gridlistInspection.DataSource = new DatabaseService().executeSelectQuery("SELECT T0.*,T1.EmployeeName FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 on T0.ResponsiblePerson = T1.Id "
                    + " where  T0.TYPE LIKE 'Inspec%' AND T0.GeneratorId = '" + dt.Rows[0]["GeneratorId"].ToString() + "' AND T0.Customer = '" + dt.Rows[0]["Customer"].ToString() + "' AND T0.Location = '" + dt.Rows[0]["Location"].ToString() + "' ");
                 
                 //gridlistInspection.DataSource = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule where  TYPE LIKE 'Inspec%'  AND GeneratorId = '" + dt.Rows[0]["GeneratorId"].ToString() + "' AND Customer = '" + dt.Rows[0]["Customer"].ToString() + "' AND Location = '" + dt.Rows[0]["Location"].ToString() + "'");
             }

           // tbControlServiceAgreement.SelectedTabPageIndex = 0;
           // LoadPage();

        }

        private void xtraTabPage3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSerShedule_Click(object sender, EventArgs e)
        {
            double dbNoOfDates = (Convert.ToDateTime(txtSelExpireDate.Text) - Convert.ToDateTime(txtSelComDate.Text)).TotalDays;
            rdo_service.Checked = true;
            rdo_inspection.Checked = false;

            double dbDatesforOneSer = dbNoOfDates / (Convert.ToDouble(txtSelNoSer.Text) + 1);
            //double dbDatesforOneInsp = dbNoOfDates / (Convert.ToDouble(txtSelNoInspec.Text)+1);

            DataTable dtGenDetails = new DatabaseService().executeSelectQuery("SELECT GeneratorNumber,ExecutiveResponsible FROM GeneratorDetails where GeneratorNumber='" + txtSelGenId.Text + "' AND Customer = '" + lblSelCusId.Text.Trim() + "' AND location = '" + lblSelLocId.Text.Trim() + "'");

            string selAgreeID = gridView1.GetFocusedDataRow()["AgreementId"].ToString();

            if (!checkServiceSchduleCreated(gridView1.GetFocusedDataRow()["AgreementId"].ToString()))
            {
                for (int i = 1; i <= Convert.ToInt32(txtSelNoSer.Text); i++)
                {
                    DateTime dtServiceDate = Convert.ToDateTime(txtSelComDate.Text).AddDays(i * dbDatesforOneSer);
                    string strSerType = "Service " + i.ToString();

                    new DatabaseService().executeUpdateQuery("INSERT INTO ServicesShedule VALUES('" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "','" + dtGenDetails.Rows[0]["GeneratorNumber"].ToString() + "','" + lblSelCusId.Text + "','" + lblSelLocId.Text + "','" + strSerType + "','" + dtServiceDate.ToString() + "','" + dtGenDetails.Rows[0]["ExecutiveResponsible"].ToString() + "','','','','','Up Comming')");

                }

                XtraMessageBox.Show("Services Schedule Succesfully Created.", "Error");
            }
            else
            {

                XtraMessageBox.Show("Services Schedule already Created.", "Error");
            }

            //gridLIstServices.DataSource = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule where  TYPE LIKE 'Service%' AND AgreementId = '" + selAgreeID + "'");


            gridLIstServices.DataSource = new DatabaseService().executeSelectQuery("SELECT T0.*,(SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = ResponsiblePerson) AS EmployeeName "
            + " FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 where TYPE LIKE 'Service%'  AND AgreementId = '" + selAgreeID + "' ");


        }

        private void btnInspecShedule_Click(object sender, EventArgs e)
        {
            double dbNoOfDates = (Convert.ToDateTime(txtSelExpireDate.Text) - Convert.ToDateTime(txtSelComDate.Text)).TotalDays;
            rdo_service.Checked = false;
            rdo_inspection.Checked = true;
            // double dbDatesforOneSer = dbNoOfDates / (Convert.ToDouble(txtSelNoSer.Text));
            double dbDatesforOneInsp = dbNoOfDates / (Convert.ToDouble(txtSelNoInspec.Text) + 1);

            //DataTable dtGenDetails = new DatabaseService().executeSelectQuery("SELECT GeneratorNumber,ExecutiveResponsible FROM GeneratorDetails where GeneratorNumber='" + txtSelGenID.Text + "' AND Customer = '" + txtSelCus.Text.Trim() + "' AND location = '" + txtSelLocation.Text.Trim() + "'");

           // DataTable dtGenDetails = new DatabaseService().executeSelectQuery("SELECT GeneratorNumber,ExecutiveResponsible FROM GeneratorDetails where GeneratorNumber ='" + txtSelGenId.Text + "' AND Customer = '" + GetCusId(txtSelCusName.Text.Trim()) + "' AND location = '" + GetCusLocId(GetCusId(txtSelCusName.Text.Trim()), txtSelLoc.Text.Trim()) + "'");
            DataTable dtGenDetails = new DatabaseService().executeSelectQuery("SELECT GeneratorNumber,ExecutiveResponsible FROM GeneratorDetails where GeneratorNumber='" + txtSelGenId.Text + "' AND Customer = '" + lblSelCusId.Text.Trim() + "' AND location = '" + lblSelLocId.Text.Trim() + "'");


            string selAgreeID = gridView1.GetFocusedDataRow()["AgreementId"].ToString();


            if (!checkInsPectionSchduleCreated(gridView1.GetFocusedDataRow()["AgreementId"].ToString()))
            {
                for (int i = 1; i <= Convert.ToInt32(txtSelNoInspec.Text); i++)
                {
                    DateTime dtServiceDate = Convert.ToDateTime(txtSelComDate.Text).AddDays(i * dbDatesforOneInsp);
                    string strSerType = "Inspection " + i.ToString();

                    //  new DatabaseService().executeUpdateQuery("INSERT INTO ServicesShedule VALUES('" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "','" + dtServiceDate.ToString() + "','','Up Comming','" + dtGenDetails.Rows[0]["ExecutiveResponsible"].ToString() + "','" + strSerType + "','" + txtSelGenID.Text + "','"+txtSelCus.Text+"','"+txtSelLocation.Text+"','')");

                   // new DatabaseService().executeUpdateQuery("INSERT INTO ServicesShedule VALUES('" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "','" + dtGenDetails.Rows[0]["GeneratorNumber"].ToString() + "','" + GetCusId(txtSelCusName.Text) + "','" + GetCusLocId(GetCusId(txtSelCusName.Text), txtSelLoc.Text) + "','" + strSerType + "','" + dtServiceDate.ToString() + "','" + dtGenDetails.Rows[0]["ExecutiveResponsible"].ToString() + "','','','','','Up Comming')");

                    new DatabaseService().executeUpdateQuery("INSERT INTO ServicesShedule VALUES('" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "','" + dtGenDetails.Rows[0]["GeneratorNumber"].ToString() + "','" + lblSelCusId.Text + "','" + lblSelLocId.Text + "','" + strSerType + "','" + dtServiceDate.ToString() + "','" + dtGenDetails.Rows[0]["ExecutiveResponsible"].ToString() + "','','','','','Up Comming')");




                }

                XtraMessageBox.Show("Inspection Schedule Succesfully Created.", "Error");

            }
            else
            {
                XtraMessageBox.Show("Inspection Schedule already Created.", "Error");
            }

            // gridlistInspection.DataSource = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule where  TYPE LIKE 'Inspec%'  AND AgreementId = '" + selAgreeID + "' ");

            gridlistInspection.DataSource = new DatabaseService().executeSelectQuery("SELECT T0.*,(SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = ResponsiblePerson) AS EmployeeName "
            + " FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 where TYPE LIKE 'Inspec%'  AND AgreementId = '" + selAgreeID + "' ");


        }

        private void btnChangeSerInspc_Click(object sender, EventArgs e)
        {
            string ServiceType = "";

            if (rdo_service.Checked)
            {
                ServiceType = grdVwServies.GetFocusedDataRow()["Type"].ToString();
            }
            if (rdo_inspection.Checked)
            {
                ServiceType = grdVwInspections.GetFocusedDataRow()["Type"].ToString();
            }

            //if (grdVwServies.SelectedRowsCount > 0)
            //{
            //    ServiceType = grdVwServies.GetFocusedDataRow()["Type"].ToString();
            //}
            //else if (grdVwInspections.SelectedRowsCount > 0)
            //{
            //    ServiceType = grdVwInspections.GetFocusedDataRow()["Type"].ToString();
            //}

            DataTable dtSerDetails = new DatabaseService().executeSelectQuery("SELECT AgreementID,PlanDate,Location,Customer,GeneratorId,Type FROM ServicesShedule where AgreementID='" + lblSelAgreeId.Text + "' AND Type = '" + ServiceType + "' ");



            txtSelChangeCus.Text = GetCusName(dtSerDetails.Rows[0]["Customer"].ToString());
            //txtPlanedDate.Text = dtSerDetails.Rows[0]["PlanDate"].ToString();

            String planDate = dtSerDetails.Rows[0]["PlanDate"].ToString().Trim();
            DateTime dtplanDate = Convert.ToDateTime(planDate);
            txtPlanedDate.EditValue = dtplanDate;


            txtSelChangeLoc.Text = GetCusLocName(dtSerDetails.Rows[0]["Location"].ToString());
            txtSelChangeGenId.Text = dtSerDetails.Rows[0]["GeneratorId"].ToString();
            txtSelChangeSerType.Text = dtSerDetails.Rows[0]["Type"].ToString();


            tbControlScheduleServices.SelectedTabPageIndex = 1;
            tbPageSubChangeSerSchedule.PageEnabled = true;
            tbPageSubScheduleServices.PageEnabled = false;
        }

        private void tbPageNewServiceAgree_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBackNewSerAgree_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void btnBackSerSchedule_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void btnBackChangeSer_Click(object sender, EventArgs e)
        {
            DataTable dt = new DatabaseService().executeSelectQuery("SELECT T0.AgreementId,T0.GeneratorId,T0.Customer,T1.CustomerName,T0.Location,T2.SubLocation,"
                + " T0.CommercingDate,T0.NoofService,T0.Twentyforhorservice,T0.CostofMinor,"
                + " T0.NoofInspection,T0.material,T0.SpcRequirement,T0.expire, "
                + " T0.Status,T0.PendingService,T0.PendingInspection,T0.Value "
                + " FROM HayleysPowerEngineeringCRM.dbo.SeviceAgreement T0 "
                + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T1 ON T0.Customer = T1.Id "
                + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T2 ON T0.Location = T2.Id "
                + " WHERE T0.AgreementId='" + gridView1.GetFocusedDataRow()["AgreementId"].ToString() + "'");

            if (!(dt.Rows.Count == 0))
            {
                lblSelAgreeId.Text = dt.Rows[0]["AgreementId"].ToString();
                txtSelCusName.Text = dt.Rows[0]["CustomerName"].ToString();
                txtSelLoc.Text = dt.Rows[0]["SubLocation"].ToString();
                txtSelGenId.Text = dt.Rows[0]["GeneratorId"].ToString();
                txtSelComDate.Text = dt.Rows[0]["CommercingDate"].ToString();
                txtSelExpireDate.Text = dt.Rows[0]["expire"].ToString();
                txtSelNoSer.Text = dt.Rows[0]["NoofService"].ToString();
                txtSelNoInspec.Text = dt.Rows[0]["NoofInspection"].ToString();

                tbControlServiceAgreement.SelectedTabPageIndex = 2;
                tbPageServiceAgreements.PageEnabled = false;
                tbPageNewServiceAgree.PageEnabled = false;
                tbPageScheduleServices.PageEnabled = true;
                tbPageCompAgreements.PageEnabled = false;

                tbControlScheduleServices.SelectedTabPageIndex = 0;
                tbPageSubScheduleServices.PageEnabled = true;
                tbPageSubChangeSerSchedule.PageEnabled = false;

                //gridLIstServices.DataSource = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule where  TYPE LIKE 'Service%' AND GeneratorId = '" + dt.Rows[0]["GeneratorId"].ToString() + "' AND Customer = '" + dt.Rows[0]["Customer"].ToString() + "' AND Location = '" + dt.Rows[0]["Location"].ToString() + "'");

                //gridlistInspection.DataSource = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule where  TYPE LIKE 'Inspec%'  AND GeneratorId = '" + dt.Rows[0]["GeneratorId"].ToString() + "' AND Customer = '" + dt.Rows[0]["Customer"].ToString() + "' AND Location = '" + dt.Rows[0]["Location"].ToString() + "'");

                gridLIstServices.DataSource = new DatabaseService().executeSelectQuery("SELECT T0.*,T1.EmployeeName FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 "
                + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 on T0.ResponsiblePerson = T1.Id "
                + " where  T0.TYPE LIKE 'Service%' AND T0.GeneratorId = '" + dt.Rows[0]["GeneratorId"].ToString() + "' AND T0.Customer = '" + dt.Rows[0]["Customer"].ToString() + "' AND T0.Location = '" + dt.Rows[0]["Location"].ToString() + "' ");

                gridlistInspection.DataSource = new DatabaseService().executeSelectQuery("SELECT T0.*,T1.EmployeeName FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 "
                   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 on T0.ResponsiblePerson = T1.Id "
                   + " where  T0.TYPE LIKE 'Inspec%' AND T0.GeneratorId = '" + dt.Rows[0]["GeneratorId"].ToString() + "' AND T0.Customer = '" + dt.Rows[0]["Customer"].ToString() + "' AND T0.Location = '" + dt.Rows[0]["Location"].ToString() + "' ");
                 


            }

        }        
        

        
    }
}