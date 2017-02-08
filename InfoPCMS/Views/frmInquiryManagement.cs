using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using InfoPCMS.Services;
using DevExpress.XtraTab;
using DevExpress.XtraGrid.Views.Grid;

namespace InfoPCMS.Views
{
    public partial class frmInquiryManagement : DevExpress.XtraEditors.XtraForm
    {
        String InqAccepted;
        int const_type;
        int addsearch;

        public bool IsNewCus;

        public frmInquiryManagement()
        {
            InitializeComponent();
            const_type = 1;

        }
        public frmInquiryManagement(String inquirynumber)
        {
            InitializeComponent();
            const_type = 2;
            setSurveyDetails(inquirynumber);
            tabInquiryManagement.SelectedTabPage = xtraTabPage3;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = true;


        }

        private void GetServerDate()
        {            
            try
            {
                DataTable dtCurDate = new DatabaseService().executeSelectQuery("select GETDATE()as sysdate");
                
                DateTime SysDate = Convert.ToDateTime(dtCurDate.Rows[0]["sysdate"].ToString());

               // MessageBox.Show(SysDate.ToString());


                

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
              
            }
        }




        private void frmInquiryManagement_Load(object sender, EventArgs e)
        {
            if (const_type == 1)
            {
                clearGetInquiry();
            }
            else if (const_type == 2)
            {
                btnSurvey.Text = "New Inspection";
                btnAddInquiry.Enabled = false;
                btnUpdate.Enabled = false;
            }
        }

        private void btnAddInquiry_Click(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("15"))
            {

                if (btnAddInquiry.Text == "Add Inquiry")
                {
                    addsearch = 1;
                    btnUpdate.Enabled = false;
                    btnAddInquiry.Text = "Save";

                    ClearAddNewInq();
                    txtProblemNature.SelectedItem = -1;

                    inqNoGen();                  

                    txtInquiryTaken.SelectedValue = GetEmpId(frmHome.EmpName.ToString().Trim());
                    txtInquiryTaken.Enabled = false;

                    

                    tabInquiryManagement.SelectedTabPage = xtraTabPage2;
                    xtraTabPage1.PageEnabled = false;
                    xtraTabPage2.PageEnabled = true;
                    xtraTabPage3.PageEnabled = false;

                }
                else if (btnAddInquiry.Text == "Save")
                {

                    InquiryService inquiry = new InquiryService();

                    if (ValidationInquiryManagment())
                    {
                        XtraMessageBox.Show(InfoPCMS.message.GET_EMPTY_FIELDS_ERROR(), "Error");
                    }
                    else
                    {
                        
                        inquiry.InquiryNumber = txtInquiryNo.Text;

                        inquiry.CustomerID = txtCustomer.SelectedValue.ToString();
                        inquiry.StrCusName = GetCusName(txtCustomer.SelectedValue.ToString());
                        inquiry.InquiryDate = txtDate.EditValue.ToString();
                        inquiry.InquiryTime = txtTime.EditValue.ToString();
                        inquiry.ProblemNature = txtProblemNature.SelectedValue.ToString().Trim();
                        inquiry.ProblemSource = txtProblemSource.Text;
                        inquiry.InquiryTaken = txtInquiryTaken.SelectedValue.ToString();
                        // inquiry.InspectionDone = "";
                        // inquiry.InspectionDetails = txtInspectionDetails.Text;

                        inquiry.JobAssignedTo = txtSiteResponsible.SelectedValue.ToString();
                        inquiry.SiteResponsible = txtSiteResponsible.SelectedValue.ToString();


                        inquiry.JobEntryDate = txtJobEntryDate.EditValue.ToString();
                        inquiry.JobEntryTime = txtJobEntryTime.EditValue.ToString();

                        inquiry.StrJobEntryDateforEmail = txtJobEntryDate.Text;
                        inquiry.StrJobEntryTimeforEmail = txtJobEntryTime.Text;


                        inquiry.HowKnow = txtHowKnow.Text;
                        //inquiry.IfFailed = txtIfFailed.Text.Trim();
                        inquiry.FollowupDetails = txtFollowupDetails.Text;
                        inquiry.Recommendation = txtRecommendedBy.Text.Trim();
                        inquiry.StrCallerName = txtCallerName.Text;
                        inquiry.StrCallerTelNo = txtCallerPhone.Text;

                        inquiry.Location = txtLocation.SelectedValue.ToString().Trim();// GetSubLocationId(txtLocation.Text.Trim(), txtCustomer.SelectedValue.ToString());
                        inquiry.Priority = cboprio.Text.Trim();
                        inquiry.ExecutiveResponsible = "";// txtExecutiveresponsible.Text.ToString();

                        if (inquiry.InquiryType == "Repeat")
                        {

                            inquiry.PastInquiry = txtCallerPhone.Text;

                        }

                        if (chkDone.Checked)
                        {

                            inquiry.Done = "Yes";
                        }
                        else
                        {

                            inquiry.Done = "No";
                        }

                        Boolean result = inquiry.addInquiry();
                        if (result == true)
                        {

                           // new DatabaseService().executeUpdateQuery("INSERT INTO InqEscalationNotAccept VALUES('" + txtInquiryNo.Text + "','" + txtSiteResponsible.SelectedValue.ToString() + "','','')");


                            //Console.WriteLine("true");
                            DataTable dt = InfoPCMS.db.executeSelectQuery("SELECT i.InquiryNumber,i.Date,c.CustomerName,i.InquiryTaken,i.SiteResponsible,i.Done as Done ,i.active as active from Inquiry i inner join Customer c on i.CustomerId = c.Id and i.active='Active' and Done='No' order by i.InquiryNumber");
                            gridInquiryList.DataSource = dt;
                            string Type = "";


                            if (txtProblemNature.SelectedValue.ToString().Trim() == "Breakdown")
                            {

                                Type = "INQ-BRK";
                            }
                            else
                                if (txtProblemNature.SelectedValue.ToString().Trim() == "Service")
                                {
                                    Type = "INQ-SER";

                                }
                                else
                                    if (txtProblemNature.SelectedValue.ToString().Trim() == "Repairs")
                                    {
                                        Type = "INQ-REP";

                                    }
                                    else
                                        if (txtProblemNature.SelectedValue.ToString().Trim() == "Rental")
                                        {
                                            Type = "INQ-REN";

                                        }
                                        else
                                            if (txtProblemNature.SelectedValue.ToString().Trim() == "Installation")
                                            {
                                                Type = "INQ-INS";

                                            }
                                            else
                                                if (txtProblemNature.SelectedValue.ToString().Trim() == "Re-Do Job")
                                                {
                                                    Type = "INQ-RED";

                                                }
                            

                            DataTable dt1 = InfoPCMS.db.executeSelectQuery("select INCNUMBER,PREFIX from AutoNumberMaster  WHERE MAINTYPE='" + Type + "'");
                            if (dt1.Rows.Count > 0)
                            {
                                Console.WriteLine((dt1.Rows[0]["PREFIX"] + "000000" + ((Convert.ToInt16(dt1.Rows[0]["INCNUMBER"]) + 2))));
                                //  txtInquiryNo.Text = (dt1.Rows[0]["PREFIX"] + "000000" + (Convert.ToInt16(dt1.Rows[0]["INCNUMBER"]) + 1));
                                new DatabaseService().executeUpdateQuery("Update AutoNumberMaster SET INCNUMBER='" + (Convert.ToInt16(dt1.Rows[0]["INCNUMBER"]) + 1) + "',CURRCODE='" + (dt1.Rows[0]["PREFIX"] + "000000" + ((Convert.ToInt16(dt1.Rows[0]["INCNUMBER"]) + 2))) + "' WHERE MAINTYPE='" + Type + "'");


                            }
                            clearGetInquiry();
                            XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_ADDED_INFORMATION(), "Information");
                            //MessageBox.Show(InfoPCMS.message.GET_RECORD_ADDED_INFORMATION(), "Information");

                            if (!Views.frmHome.isOffline)
                            {
                                inquiry.SendEmail();
                            }
                        }

                    }

                }


            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }
            //
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

      
        private void clearGetInquiry()
        {
            
            InquiryService inquiry = new InquiryService();
            //DataTable dt0 = InfoPCMS.db.executeSelectQuery("select i.InquiryNumber,i.Date,c.CustomerName,e1.EmployeeName as inquirytaken,e2.EmployeeName as inspectiondone,e3.EmployeeName as siteresponsible , i.Done as Done ,i.active as active from Inquiry i inner join Employee e1 on i.InquiryTaken = e1.Id inner join Employee e2 on i.InspectionDone = e2.Id inner join Employee e3 on i.SiteResponsible = e3.Id inner join Customer c on i.CustomerId = c.Id and i.active='Active' and Done='No' order by i.InquiryNumber");

            DataTable dt0 = InfoPCMS.db.executeSelectQuery("SELECT i.InquiryNumber,i.Date,c.CustomerName,i.InquiryTaken,i.SiteResponsible,i.Done as Done ,i.active as active from Inquiry i inner join Customer c on i.CustomerId = c.Id and i.active='Active' and Done='No' order by i.InquiryNumber");

            DataTable dtInqList = new DataTable();

            DataTable dtuser = new DatabaseService().executeSelectQuery("select Id from Users  WHERE Username='" + Views.frmHome.UsrName + "'");
            DataTable dtEmployee = new DatabaseService().executeSelectQuery("select Id,EmployeeName from Employee  WHERE EmployeeName='" + Views.frmHome.EmpName + "'");



            if (Views.frmHome.EmpCat == "LEVEL 3")
            {

                //dtInqList = InfoPCMS.db.executeSelectQuery("SELECT i.InquiryNumber,i.Date,c.CustomerName,c2.CategoryName,E1.EmployeeName AS InquiryTaken"
                //     + " ,E2.EmployeeName AS SiteResponsible,i.Done as Done ,i.active as active "
                //     + "from HayleysPowerEngineeringCRM.dbo.Inquiry i "
                //     + "inner join HayleysPowerEngineeringCRM.dbo.Customer c on i.CustomerId = c.Id "
                //     + "inner join HayleysPowerEngineeringCRM.dbo.Employee E1 on i.InquiryTaken = E1.Id "
                //     + "inner join HayleysPowerEngineeringCRM.dbo.Employee E2 on i.SiteResponsible = E2.Id "
                //     + "inner join HayleysPowerEngineeringCRM.dbo.CustomerCategory c2 on c.CustomerCategory = c2.Id "
                //     + "and i.active='Active' and Done='No' order by i.InquiryNumber");

                dtInqList = InfoPCMS.db.executeSelectQuery(" SELECT i.InquiryNumber,i.JobEntryDate AS Date,i.JobEntryTime AS Time,c.CustomerName, "
                      + " (SELECT CategoryName FROM HayleysPowerEngineeringCRM.dbo.CustomerCategory WHERE Id = c.CustomerCategory) AS CategoryName, "
                      + " E1.EmployeeName AS InquiryTaken "
                      + "  ,E2.EmployeeName AS SiteResponsible,i.Done as Done ,i.active as active,i2.Status  "
                      + " from HayleysPowerEngineeringCRM.dbo.Inquiry i  "
                      + " LEFT join HayleysPowerEngineeringCRM.dbo.Customer c on i.CustomerId = c.Id  "
                      + " LEFT join HayleysPowerEngineeringCRM.dbo.Employee E1 on i.InquiryTaken = E1.Id  "
                      + " LEFT join HayleysPowerEngineeringCRM.dbo.Employee E2 on i.SiteResponsible = E2.Id  "
                      + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.InquiryStatus i2 on i.InquiryNumber = i2.InquiryNumber "
                      + " WHERE i.active='Active'  AND (i2.Status <> 'Complete' OR i2.Status IS NULL) order by i.JobEntryDate ");
                

            }
            else
            {
                //dtInqList = InfoPCMS.db.executeSelectQuery("SELECT i.InquiryNumber,i.Date,c.CustomerName,c2.CategoryName,E1.EmployeeName AS InquiryTaken"
                //        + " ,E2.EmployeeName AS SiteResponsible,i.Done as Done ,i.active as active "
                //        + "from HayleysPowerEngineeringCRM.dbo.Inquiry i "
                //        + "inner join HayleysPowerEngineeringCRM.dbo.Customer c on i.CustomerId = c.Id "
                //        + "inner join HayleysPowerEngineeringCRM.dbo.Employee E1 on i.InquiryTaken = E1.Id "
                //        + "inner join HayleysPowerEngineeringCRM.dbo.Employee E2 on i.SiteResponsible = E2.Id "
                //        + "inner join HayleysPowerEngineeringCRM.dbo.CustomerCategory c2 on c.CustomerCategory = c2.Id "
                //        + "and i.active='Active' and Done='No' order by i.InquiryNumber");
                if (dtuser.Rows.Count > 0)
                {

                    String Id = dtuser.Rows[0]["Id"].ToString();

                    String EmpName = dtEmployee.Rows[0]["EmployeeName"].ToString().Trim();
                    String EmpId = dtEmployee.Rows[0]["Id"].ToString().Trim();

                    dtInqList = InfoPCMS.db.executeSelectQuery(" SELECT i.InquiryNumber,i.JobEntryDate AS Date,i.JobEntryTime AS Time,c.CustomerName, "
                       + " (SELECT CategoryName FROM HayleysPowerEngineeringCRM.dbo.CustomerCategory WHERE Id = c.CustomerCategory) AS CategoryName, "
                       + " E1.EmployeeName AS InquiryTaken "
                       + "  ,E2.EmployeeName AS SiteResponsible,i.Done as Done ,i.active as active,i2.Status  "
                       + " from HayleysPowerEngineeringCRM.dbo.Inquiry i  "
                       + " LEFT join HayleysPowerEngineeringCRM.dbo.Customer c on i.CustomerId = c.Id  "
                       + " LEFT join HayleysPowerEngineeringCRM.dbo.Employee E1 on i.InquiryTaken = E1.Id  "
                       + " LEFT join HayleysPowerEngineeringCRM.dbo.Employee E2 on i.SiteResponsible = E2.Id  "
                       + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.InquiryStatus i2 on i.InquiryNumber = i2.InquiryNumber "
                       + " WHERE i.active='Active'  AND (i2.Status <> 'Complete' OR i2.Status IS NULL) AND i.InquiryTaken = '" + EmpId + "' order by i.JobEntryDate ");
                }
            }

            gridInquiryList.DataSource = dtInqList;

           // ClearAddNewInq();

            btnAddInquiry.Text = "Add Inquiry";
            btnSurvey.Text = "Inspection";

            inqNoGen();

            if (IsNewCus)
            {
                tabInquiryManagement.SelectedTabPage = xtraTabPage2;
                xtraTabPage1.PageEnabled = false;
                xtraTabPage2.PageEnabled = true;
                xtraTabPage3.PageEnabled = false;
            }
            else
            {

                tabInquiryManagement.SelectedTabPage = xtraTabPage1;
                xtraTabPage1.PageEnabled = true;
                xtraTabPage2.PageEnabled = false;
                xtraTabPage3.PageEnabled = false;
            }

         

        }

        private void ClearAddNewInq()
        {
            gridSurveys.DataSource = null;
            txtCallerPhone.Visible = true;
            chkDone.Checked = false;
            try
            {

                DataTable dt = InfoPCMS.db.executeSelectQuery("SELECT distinct(InquiryType) FROM InquiryType");
                txtProblemNature.DataSource = dt;
                txtProblemNature.DisplayMember = "InquiryType";
                txtProblemNature.ValueMember = "InquiryType";
                txtProblemNature.Enabled = true;

            }
            catch (Exception ex)
            {

                throw ex;

            }

           

            DataTable dt2 = InfoPCMS.db.executeSelectQuery("SELECT Id,CustomerName FROM Customer WHERE IsActive = 'True' OR IsTemp = 'True' ORDER BY CustomerName");
            txtCustomer.DataSource = dt2;

            //foreach (DataRow row in dt2.Rows)
            //{
            //    txtCustomer.Items.Add( row["CustomerName"].ToString());

            //}
            //txtCustomer.Items.Insert(0, "----------Select Customer---------");
            txtCustomer.ValueMember = "Id";
            txtCustomer.DisplayMember = "CustomerName";
           
            txtCustomer.SelectedIndex = -1;

            txtCustomer.Enabled = true;
            txtLocation.Enabled = true;


            DataTable dt3 = InfoPCMS.db.executeSelectQuery("select * from Employee WHERE Category != 'LEVEL 3'");
            DataTable dt4 = InfoPCMS.db.executeSelectQuery("select * from Employee ");
            DataTable dt5 = InfoPCMS.db.executeSelectQuery("select Id,Displayname from Users ");
           // DataTable dt6 = InfoPCMS.db.executeSelectQuery("select * from InquiryType ");

            DataTable df = new DataTable();
            gridControl1.DataSource = df;

            txtInquiryTaken.DataSource = dt4;
            txtInquiryTaken.ValueMember = "Id";
            txtInquiryTaken.DisplayMember = "EmployeeName";
            
            txtInspectionDone.DataSource = dt4;
            txtInspectionDone.ValueMember = "Id";
            txtInspectionDone.DisplayMember = "EmployeeName";

            txtExecutiveresponsible.DataSource = dt4;
            txtExecutiveresponsible.ValueMember = "Id";
            txtExecutiveresponsible.DisplayMember = "EmployeeName";

            txtSiteResponsible.DataSource = dt3;
            txtSiteResponsible.ValueMember = "Id";
            txtSiteResponsible.DisplayMember = "EmployeeName";

            //txtProblemNature.DataSource = dt6;
            //txtProblemNature.ValueMember = "InquiryType";
            //txtProblemNature.DisplayMember = "InquiryType";

            txtInquiryNo.Text = "";
            txtDate.EditValue = DateTime.Now;
            txtTime.EditValue = DateTime.Now;
            txtInspectionDetails.Text = "";
            txtHowKnow.Text = "";
            txtProblemNature.SelectedIndex = 0;
            txtProblemSource.Text = "";
            txtCallerName.Text = "";
            txtCallerPhone.Text = "";

            // txtIfFailed.Text = "";
            txtRecommendedBy.Text = "";
            if (txtInquiryTaken.Items.Count > 0)
            {
                txtInquiryTaken.SelectedIndex = 0;
            }
            if (txtInspectionDone.Items.Count > 0)
            {
                txtInspectionDone.SelectedIndex = 0;
            }
            if (txtSiteResponsible.Items.Count > 0)
            {
                txtSiteResponsible.SelectedIndex = 0;
            }
            txtFollowupDetails.Text = "";                       

            txtCallerName.Enabled = true;
            txtCallerName.Text = "";                      
            btnAddInquiry.Enabled = true;
            btnUpdate.Enabled = false;
            btnSurvey.Enabled = false;
            txtInquiryNo.Enabled = true;
            txtProblemNature.Enabled = true;
            txtRecommendedBy.Text = "";
            txtProblemSource.Text = "";
            txtInspectionDetails.Text = "";
            txtFollowupDetails.Text = "";

            DateTime ServerDateTime = InfoPCMS.db.GetServerDateTime();
            txtTime.EditValue = ServerDateTime.TimeOfDay.ToString();
            txtDate.EditValue = ServerDateTime.Date.ToString("MM/dd/yyyy");
            txtJobEntryDate.EditValue = ServerDateTime.Date.ToString("MM/dd/yyyy");
            txtJobEntryTime.EditValue = ServerDateTime.TimeOfDay.ToString();
            txtLocation.Text = "";

        
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearGetInquiry();
        }

        private void btnSearchInquiry_Click(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("16"))
            {
                if (InqAccepted != "Yes")
                {

                    if (!String.IsNullOrEmpty(txtInquiryNo.Text) && !String.IsNullOrEmpty(txtDate.Text) && !String.IsNullOrEmpty(txtCustomer.Text))
                    {

                        InquiryService inquiry = new InquiryService();
                        inquiry.InquiryNumber = txtInquiryNo.Text;
                        inquiry.CustomerID = txtCustomer.SelectedValue.ToString();
                        inquiry.InquiryDate = txtDate.EditValue.ToString();
                        inquiry.InquiryTime = txtTime.EditValue.ToString();
                        inquiry.ProblemNature = txtProblemNature.SelectedValue.ToString().Trim();
                        inquiry.ProblemSource = txtProblemSource.Text;
                        inquiry.JobAssignedTo = txtSiteResponsible.SelectedValue.ToString();
                        inquiry.SiteResponsible = txtSiteResponsible.SelectedValue.ToString();
                        //DataTable dtuser = new DatabaseService().executeSelectQuery("SELECT Id FROM Users WHERE Username='" + txtSiteResponsible.SelectedValue.ToString() + "'");
                        //if (dtuser.Rows.Count > 0)
                        //{
                        //    inquiry.SiteResponsible = dtuser.Rows[0]["id"].ToString();
                        //}
                        inquiry.InquiryTaken = txtInquiryTaken.SelectedValue.ToString();
                        //inquiry.InspectionDone = "";
                        //inquiry.InspectionDetails = txtInspectionDetails.Text;

                        inquiry.JobEntryDate = txtJobEntryDate.EditValue.ToString();
                        inquiry.JobEntryTime = txtJobEntryTime.EditValue.ToString();

                        inquiry.HowKnow = txtHowKnow.Text;
                        //inquiry.IfFailed = txtIfFailed.Text.Trim();
                        inquiry.FollowupDetails = txtFollowupDetails.Text;
                        inquiry.Recommendation = txtRecommendedBy.Text.Trim();
                        inquiry.StrCallerName = txtCallerName.Text.Trim();
                        inquiry.StrCallerTelNo = txtCallerPhone.Text.Trim();

                        if (chkDone.Checked)
                        {

                            inquiry.Done = "Yes";
                        }
                        else
                        {

                            inquiry.Done = "No";
                        }


                        Boolean result = inquiry.updateInquiry();
                        if (result == true)
                        {

                            clearGetInquiry();
                            // MessageBox.Show(InfoPCMS.message.GET_RECORD_UPDATED_INFORMATION(), "Information");
                            XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_UPDATED_INFORMATION(), "Information");
                        }
                    }
                    else
                    {

                        XtraMessageBox.Show(InfoPCMS.message.GET_EMPTY_FIELDS_ERROR(), "Error");

                    }



                }
                else
                {

                    XtraMessageBox.Show("The Quotation of this Inquiry Is Already Authorized", "Error");

                }

            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }
            //
        }

        private void selectInquiry(object sender, EventArgs e)
        {
            addsearch = 2;
            btnAddInquiry.Enabled = false;
            btnUpdate.Enabled = true;
            txtInquiryNo.Enabled = false;
            btnSurvey.Enabled = true;
            txtProblemNature.Enabled = false;

            txtCustomer.Enabled = false;

            txtLocation.Enabled = false;            //  txtInqType.Enabled = false;

            String inquiryno = gridView1.GetFocusedDataRow()["InquiryNumber"].ToString();


            InquiryService inquiry = new InquiryService();
            inquiry = inquiry.searchInquiry(inquiryno);
            InqAccepted = inquiry.IsInquryAccepted(inquiryno);
            if (inquiry != null)
            {

                txtInquiryNo.Text = inquiryno;

                //txtDate.EditValue = InfoPCMS.conversion.convertDate(inquiry.InquiryDate);
                String inqdate = inquiry.InquiryDate;
                DateTime inqdt = Convert.ToDateTime(inqdate);
                txtDate.EditValue = inqdt;
                txtTime.EditValue = inquiry.InquiryTime.ToString().Trim();


                DataTable dtCustomer = new DatabaseService().executeSelectQuery("select * from Customer  WHERE Id='" + inquiry.CustomerID.ToString().Trim() + "'");
                string StrCustomer = dtCustomer.Rows[0]["CustomerName"].ToString().Trim();

                DataTable dtInqTaken = new DatabaseService().executeSelectQuery("select * from Employee  WHERE Id='" + inquiry.InquiryTaken.ToString().Trim() + "'");
                string StrInqTaken = dtInqTaken.Rows[0]["EmployeeName"].ToString().Trim();

                DataTable dtJobAssign = new DatabaseService().executeSelectQuery("select * from Employee  WHERE Id='" + inquiry.SiteResponsible.ToString().Trim() + "'");
                string StrjobbAssign = dtJobAssign.Rows[0]["EmployeeName"].ToString().Trim();



                txtCustomer.SelectedValue = inquiry.CustomerID.ToString().Trim();
                txtHowKnow.Text = inquiry.HowKnow;
                txtInquiryTaken.SelectedValue = inquiry.InquiryTaken.ToString().Trim();
                // txtInspectionDetails.Text = inquiry.InspectionDetails;
                txtProblemNature.SelectedValue = inquiry.ProblemNature;

                txtInquiryNo.Text = inquiryno;

                txtProblemSource.Text = inquiry.ProblemSource;
                txtSiteResponsible.SelectedValue = inquiry.SiteResponsible.ToString().Trim();
                // txtIfFailed.Text = inquiry.IfFailed.ToString().Trim();
                txtRecommendedBy.Text = inquiry.Recommendation.ToString().Trim();
                txtFollowupDetails.Text = inquiry.FollowupDetails;
                // txtInspectionDone.SelectedValue = inquiry.InspectionDone;
                txtLocation.SelectedValue = inquiry.Location;
                cboprio.SelectedItem = inquiry.Priority;
                txtExecutiveresponsible.Text = inquiry.ExecutiveResponsible;

                txtCallerName.Text = inquiry.StrCallerName;
                txtCallerPhone.Text = inquiry.StrCallerTelNo;

                String strJobEntrydate = inquiry.JobEntryDate;
                DateTime jobEntrydate = Convert.ToDateTime(strJobEntrydate);

                txtJobEntryDate.EditValue = jobEntrydate;
                txtJobEntryTime.EditValue = inquiry.JobEntryTime.ToString().Trim();



                if (inquiry.Done == "Yes")
                {

                    chkDone.Checked = true;

                }
                else
                {

                    chkDone.Checked = false;

                }

                //txtInqType.SelectedItem = inquiry.InquiryType;
                //if (txtInqType.SelectedItem == "Repeat")
                //{
                //    txtPastInquiry.SelectedValue = inquiry.PastInquiry;
                //    txtPastInquiry.Enabled = false;
                //}
                //else {

                //    txtPastInquiry.Visible = false;

                //}


                tabInquiryManagement.SelectedTabPage = xtraTabPage2;
                xtraTabPage1.PageEnabled = false;
                xtraTabPage2.PageEnabled = true;
                xtraTabPage3.PageEnabled = false;

            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NO_SUCH_RECORD_INFORMATION(), "Information");

            }
        }

        private void tabInquiryManagement_Click(object sender, EventArgs e)
        {

        }

        private void setSurveyDetails(String inqno)
        {


            try
            {
                txtSurveyInquiry.Text = inqno;
                // DataTable dtsurveys = InfoPCMS.db.executeSelectQuery("select e.EmployeeName as Surveyor from Inquiry i inner join Employee e on e.EmployeeId = i.InspectionDone where i.InquiryNumber = '" + inqno.Trim() + "'");
                // DataTable dt = InfoPCMS.db.executeSelectQuery("select e.EmployeeName as Surveyor from Inquiry i inner join Employee e on e.EmployeeId = i.InspectionDone where i.InquiryNumber = '" + inqno + "'");
                DataTable dtsurveytable = InfoPCMS.db.executeSelectQuery("select * from Survey where InquiryNumber = '" + inqno + "'");

                //String surveyor = dtsurveys.Rows[0]["Surveyor"].ToString();


                gridSurveys.DataSource = dtsurveytable;


            }
            catch (Exception ex) { throw ex; }

        }

        private void btnSurvey_Click(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("71"))
            {

                if (btnSurvey.Text == "Inspection")
                {
                    String inqno = txtInquiryNo.Text;

                    txtSurveyInquiry.Text = txtInquiryNo.Text;




                    btnUpdate.Enabled = false;
                    btnAddInquiry.Enabled = false;
                    btnSurvey.Text = "New Inspection";


                    setSurveyDetails(txtInquiryNo.Text);

                    tabInquiryManagement.SelectedTabPage = xtraTabPage3;
                    xtraTabPage1.PageEnabled = false;
                    xtraTabPage2.PageEnabled = false;
                    xtraTabPage3.PageEnabled = true;




                }
                else if (btnSurvey.Text == "New Inspection")
                {

                    //frmSurvey survey = new frmSurvey(txtSurveyInquiry.Text, "Add");
                    //survey.Show();


                }

            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }
        }

        private void selectSurvey(object sender, EventArgs e)
        {

            String surveyid = gridView2.GetFocusedDataRow()["Id"].ToString();

            //frmSurvey survey = new frmSurvey(surveyid, txtSurveyInquiry.Text, "Update");
            //survey.Show();
        }

        private void txtProblemNature_SelectedIndexChanged(object sender, EventArgs e)
        {

            // if(addsearch==1){
            inqNoGen();
            // }

        }

        private void inqNoGen()
        {
            try
            {
                txtInquiryNo.Text = "";

                if (txtProblemNature.SelectedValue.ToString().Trim() == "Service")
                {
                    AutoGenerationService generate1 = new AutoGenerationService();
                    txtInquiryNo.Text = generate1.executeGetAutonoQuery("INQ-SER");
                }
                else if (txtProblemNature.SelectedValue.ToString().Trim() == "Breakdown")
                {
                    AutoGenerationService generate = new AutoGenerationService();
                    txtInquiryNo.Text = generate.executeGetAutonoQuery("INQ-BRK");
                }
                else if (txtProblemNature.SelectedValue.ToString().Trim() == "Repairs")
                {
                    AutoGenerationService generate = new AutoGenerationService();
                    txtInquiryNo.Text = generate.executeGetAutonoQuery("INQ-REP");
                }
                else if (txtProblemNature.SelectedValue.ToString().Trim() == "Rental")
                {
                    AutoGenerationService generate = new AutoGenerationService();
                    txtInquiryNo.Text = generate.executeGetAutonoQuery("INQ-REN");
                }
                else if (txtProblemNature.SelectedValue.ToString().Trim() == "Installation")
                {
                    AutoGenerationService generate = new AutoGenerationService();
                    txtInquiryNo.Text = generate.executeGetAutonoQuery("INQ-INS");
                }

                else if (txtProblemNature.SelectedValue.ToString().Trim() == "Re-Do Job")
                {

                    AutoGenerationService generate = new AutoGenerationService();

                    txtInquiryNo.Text = generate.executeGetAutonoQuery("INQ-RED");

                }
                //

                //if (txtProblemNature.SelectedValue.ToString().Trim() == "Quotation")
                //{
                //    AutoGenerationService generate1 = new AutoGenerationService();
                //    txtInquiryNo.Text = generate1.executeGetAutonoQuery("INQ-QTN");
                //}
                //else if (txtProblemNature.SelectedValue.ToString().Trim() == "Inspection")
                //{
                //    AutoGenerationService generate = new AutoGenerationService();
                //    txtInquiryNo.Text = generate.executeGetAutonoQuery("INQ-INS");
                //}
                //else if (txtProblemNature.SelectedValue.ToString().Trim() == "Service")
                //{
                //    AutoGenerationService generate = new AutoGenerationService();
                //    txtInquiryNo.Text = generate.executeGetAutonoQuery("INQ-SER");
                //}
                //else if (txtProblemNature.SelectedValue.ToString().Trim() == "Major Repair")
                //{
                //    AutoGenerationService generate = new AutoGenerationService();
                //    txtInquiryNo.Text = generate.executeGetAutonoQuery("INQ-MJR");
                //}
                //else if (txtProblemNature.SelectedValue.ToString().Trim() == "Sound Proof")
                //{
                //    AutoGenerationService generate = new AutoGenerationService();
                //    txtInquiryNo.Text = generate.executeGetAutonoQuery("INQ-SPF");
                //}
                //else if (txtProblemNature.SelectedValue.ToString().Trim() == "Installation")
                //{
                //    AutoGenerationService generate = new AutoGenerationService();
                //    txtInquiryNo.Text = generate.executeGetAutonoQuery("INQ-ITL");
                //}
                //else if (txtProblemNature.SelectedValue.ToString().Trim() == "Renting")
                //{

                //    AutoGenerationService generate = new AutoGenerationService();

                //    txtInquiryNo.Text = generate.executeGetAutonoQuery("INQ-RTG");

                //}
                //else if (txtProblemNature.SelectedValue.ToString().Trim() == "Other")
                //{

                //    AutoGenerationService generate = new AutoGenerationService();

                //    txtInquiryNo.Text = generate.executeGetAutonoQuery("INQ-OTR");

                //}
                //else if (txtProblemNature.SelectedValue.ToString().Trim() == "BreakDown")
                //{

                //    AutoGenerationService generate = new AutoGenerationService();

                //    txtInquiryNo.Text = generate.executeGetAutonoQuery("INQ-BRK");

                //}



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }

        private void deleteSurvey(object sender, MouseEventArgs e)
        {
            String surveyid = gridView2.GetFocusedDataRow()["Id"].ToString();
            if (e.Button == MouseButtons.Right)
            {
                DialogResult result1 = XtraMessageBox.Show("Are you sure you want to delete this record?",
          "Delete Record",
          MessageBoxButtons.YesNo);

                if (result1 == DialogResult.Yes)
                {

                    SurveyService survey = new SurveyService();
                    survey.ID = surveyid;
                    Boolean result = survey.deleteSurvey();
                    if (result)
                    {

                        XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_DELETED_INFORMATION(), "Information");
                        clearGetInquiry();




                    }

                }

            }
        }

        private void inquiryTypeCheck()
        {

            //if (txtInqType.SelectedItem == "New")
            //{

            //DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Inquiry");
            //if (dt.Rows.Count > 0)
            //{

            //    txtPastInquiry.DataSource = dt;
            //    txtPastInquiry.ValueMember = "InquiryNumber";
            //    txtPastInquiry.DisplayMember = "InquiryNumber";



            //}

            //txtPastInquiry.Enabled = false;
            //}
            //else 
            //if (txtInqType.SelectedItem == "Repeat")
            //{
            //    DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Inquiry");
            //    if(dt.Rows.Count>0){

            //        txtPastInquiry.DataSource = dt;
            //        txtPastInquiry.ValueMember = "InquiryNumber";
            //        txtPastInquiry.DisplayMember = "InquiryNumber";



            //    }
            //    txtPastInquiry.Enabled = true;

            //    try
            //    {
            //        txtPastInquiry.SelectedIndex = 0;

            //        if (!String.IsNullOrEmpty(txtPastInquiry.SelectedValue.ToString()))
            //        {
            //            InquiryService inquiry = new InquiryService();
            //            inquiry.InquiryNumber = txtPastInquiry.SelectedValue.ToString();
            //            inquiry.getInquiryDetailsByInqNo();
            //            txtRecommendedBy.Text = inquiry.Recommendation;
            //            txtProblemSource.Text = inquiry.ProblemSource;
            //            txtInspectionDetails.Text = inquiry.InspectionDetails;
            //            txtFollowupDetails.Text = inquiry.FollowupDetails;
            //            txtCustomer.SelectedValue = inquiry.CustomerID;
            //            txtProblemNature.SelectedValue = inquiry.ProblemNature;
            //            txtHowKnow.SelectedItem = inquiry.HowKnow;
            //            txtInquiryTaken.SelectedValue = inquiry.InquiryTaken;
            //            txtInspectionDone.SelectedValue = inquiry.InspectionDone;
            //            txtSiteResponsible.SelectedValue = inquiry.SiteResponsible;
            //        }
            //    }catch(ArgumentOutOfRangeException aex){

            //        txtInqType.Text = "";
            //        XtraMessageBox.Show("Oops! No Past Inquiries", "Error");


            //    }



            //}



        }

        private void loadPastInquiryDetails()
        {

            InquiryService inquiry = new InquiryService();
            inquiry.InquiryNumber = txtCallerPhone.Text;
            inquiry.getInquiryDetailsByInqNo();
            txtRecommendedBy.Text = inquiry.Recommendation;
            txtProblemSource.Text = inquiry.ProblemSource;
            txtInspectionDetails.Text = inquiry.InspectionDetails;
            txtFollowupDetails.Text = inquiry.FollowupDetails;


            if (btnAddInquiry.Text == "Save")
            {

                txtCustomer.SelectedValue = inquiry.CustomerID;
                txtProblemNature.SelectedValue = inquiry.ProblemNature;
                txtHowKnow.Text = inquiry.HowKnow;
                txtInquiryTaken.SelectedValue = inquiry.InquiryTaken;
                txtInspectionDone.SelectedValue = inquiry.InspectionDone;
                txtSiteResponsible.SelectedValue = inquiry.SiteResponsible;

            }


        }

        private void txtCustomer_SelectedIndexChanged_1(object sender, EventArgs e)
        { 
            if (txtCustomer.SelectedIndex != -1)
            {              
                DataTable dt = InfoPCMS.db.executeSelectQuery(String.Format("select * from CustomerLocation WHERE CustomerId='{0}'", txtCustomer.SelectedValue.ToString().Trim()));
                txtLocation.DataSource = dt;
                txtLocation.DisplayMember = "SubLocation";
                txtLocation.ValueMember = "Id";               
            } 
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtInquiryNo.Text == "")
            {
                XtraMessageBox.Show("Please Select Inquiry", "Error");
            }
            else
            {
                InquiryService iqs = new InquiryService();
                iqs.InquiryNumber = txtInquiryNo.Text;
                Boolean bo = iqs.inactiveInquiry("Inactive");
                if (bo == true)
                {
                    XtraMessageBox.Show("Inquiry Inactive Successfully", "Information");
                    clearGetInquiry();
                }
                else
                {
                    XtraMessageBox.Show("Oops ! Inquiry Inactive Fail", "Error");
                }
            }
        }

        private void txtLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = InfoPCMS.db.executeSelectQuery(String.Format("SELECT Address FROM CustomerLocation where CustomerId='{0}' and  Id='{1}'", txtCustomer.SelectedValue.ToString().Trim(), txtLocation.SelectedValue.ToString().Trim()));
            if (dt.Rows.Count > 0)
            {
                txtProblemSource.Text = dt.Rows[0]["Address"].ToString();
            }

            string strSelCus = txtCustomer.SelectedValue.ToString().Trim();//GetCusId(txtCustomer.Text.Trim());
            string strSelLoc = txtLocation.SelectedValue.ToString().Trim();//GetSubLocationId(txtLocation.Text.Trim(),strSelCus);

            string genDetails = "SELECT GEN.GeneratorNumber,GEN.capcity,(SELECT CommissioningDate FROM HayleysPowerEngineeringCRM.dbo.HandingOverDetails HO "
                + " WHERE location='" + strSelLoc + "' and Customer='" + strSelCus + "' AND GeneratorNumber = GEN.GeneratorNumber) AS CommissioningDate "
                + " FROM HayleysPowerEngineeringCRM.dbo.GeneratorDetails GEN  where GEN.location='" + strSelLoc + "' and GEN.Customer='" + strSelCus + "'";

            DataTable dtGenDetails = InfoPCMS.db.executeSelectQuery(String.Format(genDetails));

            gridControl1.DataSource = dtGenDetails;            
        }

         private string GetCusId(string strCusName)
        {
            string strCusId = "";
            DataTable dtCus = new DatabaseService().executeSelectQuery("select Id,CustomerName from Customer  WHERE CustomerName='" + strCusName.Trim() + "'");

            if (dtCus.Rows.Count > 0)
            {

                strCusId = dtCus.Rows[0]["Id"].ToString();
            }


            return strCusId;
        }

         private string GetCusName(string strCusId)
         {
             string strCusName = "";
             DataTable dtCus = new DatabaseService().executeSelectQuery("select Id,CustomerName from Customer  WHERE Id='" + strCusId.Trim() + "'");

             if (dtCus.Rows.Count > 0)
             {

                 strCusName = dtCus.Rows[0]["CustomerName"].ToString();
             }


             return strCusName;
         }

        private string GetEmpId(string strEmpName)
        {
            string strEmpId = "";
            DataTable dtEmployee = new DatabaseService().executeSelectQuery("select Id,EmployeeName from Employee  WHERE EmployeeName='" + strEmpName.Trim() + "'");


            if (dtEmployee.Rows.Count > 0)
            {

                strEmpId = dtEmployee.Rows[0]["Id"].ToString();
            }

            return strEmpId;
        }

        private string GetSubLocationId(string strSubLocName, string strCusId)
        {
            string strSubLocId = "";
            DataTable dtEmployee = new DatabaseService().executeSelectQuery("select Id,SubLocation from CustomerLocation  WHERE SubLocation='" + strSubLocName.Trim() + "' AND CustomerId = '" + strCusId.Trim() + "'");


            if (dtEmployee.Rows.Count > 0)
            {

                strSubLocId = dtEmployee.Rows[0]["Id"].ToString();
            }

            return strSubLocId;
        }        

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            txtHowKnow.Text = gridView3.GetFocusedDataRow()["GeneratorNumber"].ToString();
            DataTable dt = InfoPCMS.db.executeSelectQuery(String.Format("SELECT * FROM GeneratorDetails where GeneratorNumber='{0}' AND Customer = '{1}' AND location = '{2}'", gridView3.GetFocusedDataRow()["GeneratorNumber"].ToString(), txtCustomer.SelectedValue.ToString(), txtLocation.SelectedValue.ToString().Trim()));//GetSubLocationId(txtLocation.SelectedValue.ToString().Trim(), txtCustomer.SelectedValue.ToString().Trim())
            if (dt.Rows.Count > 0)
            {
                DataTable dtEmp = InfoPCMS.db.executeSelectQuery("select * from Employee where Id = '" + dt.Rows[0]["ExecutiveResponsible"].ToString().Trim() + "' "); ;


                txtSiteResponsible.SelectedValue = dt.Rows[0]["ExecutiveResponsible"].ToString();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            new FrmServiceAgreenment().Show();
        }

        private void xtraTabPage2_Paint(object sender, PaintEventArgs e)
        {

        }

        //private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //}

        public void AutoSearch(System.Windows.Forms.ComboBox xcb, System.Windows.Forms.KeyPressEventArgs e, bool blnLimitToList)
        {
            string strFindStr = "";

            if (e.KeyChar == (char)8)
            {
                if (xcb.SelectionStart <= 1)
                {
                    xcb.Text = "";
                    return;
                }

                if (xcb.SelectionLength == 0)
                {
                    strFindStr = xcb.Text.Substring(0, xcb.Text.Length - 1);
                }
                else
                {
                    strFindStr = xcb.Text.Substring(0, xcb.SelectionStart - 1);
                }
            }
            else
            {
                if (xcb.SelectionLength == 0)
                {
                    strFindStr = xcb.Text + e.KeyChar;
                }
                else
                {
                    strFindStr = xcb.Text.Substring(0, xcb.SelectionStart) + e.KeyChar;
                }

                int intIdx = -1;
                // Search the string in the ComboBox list.  

                intIdx = xcb.FindString(strFindStr);
                if (intIdx != -1)
                {
                    xcb.SelectedText = "";
                    xcb.SelectedIndex = intIdx;
                    xcb.SelectionStart = strFindStr.Length;
                    xcb.SelectionLength = xcb.Text.Length;
                    e.Handled = true;
                }
                else
                {
                    e.Handled = blnLimitToList;

                }
            }
        }

        private void txtCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            AutoSearch(txtCustomer, e, false);
        }

        private void lblpastinq_Click(object sender, EventArgs e)
        {

        }

        private void txtJobEntryTime_EditValueChanged(object sender, EventArgs e)
        {

        }

        private bool ValidationInquiryManagment()
        {
            bool blValidation = false;

            if (string.IsNullOrEmpty(txtInquiryNo.Text.Trim()))
            {
                blValidation = true;
                lblReqInqNo.Visible = true;
            }
            else if (string.IsNullOrEmpty(txtProblemNature.Text.Trim()))
            {
                blValidation = true;
                lblReqInqType.Visible = true;
            }
            else if (string.IsNullOrEmpty(txtCustomer.Text.Trim()))
            {
                blValidation = true;
                lblReqCus.Visible = true;
            }
            else if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                blValidation = true;
                lblReqLoc.Visible = true;
            }
            else if (string.IsNullOrEmpty(txtInquiryTaken.Text.Trim()))
            {
                blValidation = true;
                lblReqInqTaken.Visible = true;

            }
            else if (string.IsNullOrEmpty(txtSiteResponsible.Text.Trim()))
            {
                blValidation = true;
                lblReqJobAssing.Visible = true;
            }
            else if (string.IsNullOrEmpty(txtJobEntryDate.Text.Trim()))
            {
                blValidation = true;
                lblReqJobEnDate.Visible = true;
            }
            else if (string.IsNullOrEmpty(txtJobEntryTime.Text.Trim()))
            {
                blValidation = true;
                lblReqJobEnTime.Visible = true;
            }
            else if (string.IsNullOrEmpty(txtCallerName.Text.Trim()))
            {
                blValidation = true;
                lblReqCallerName.Visible = true;
            }
            else if (string.IsNullOrEmpty(txtCallerPhone.Text.Trim()))
            {
                blValidation = true;
                lblReqCallerPhone.Visible = true;
            }
            else if (string.IsNullOrEmpty(txtFollowupDetails.Text.Trim()))
            {
                blValidation = true;
                lblReqInqDetails.Visible = true;
            }
            else if (string.IsNullOrEmpty(cboprio.Text.Trim()))
            {
                blValidation = true;
                lblReqPriority.Visible = true;
            }

            return blValidation;
        }

        private void txtCallerName_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnAddNewCus_Click(object sender, EventArgs e)
        {
            FrmInqNewCstomer f = new FrmInqNewCstomer();
            f.type = txtProblemNature.Text;
            f.Show();
            this.Hide();
        }

        private void gridInquiryList_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (View.GetRowCellDisplayText(e.RowHandle, View.Columns["CategoryName"]) == "Diamond")
                {
                    e.Appearance.BackColor = Color.Tomato;
                   // e.Appearance.BackColor2 = Color.SeaShell;
                }
                
                   
            }
        }

    }
}