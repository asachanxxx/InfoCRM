using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using InfoPCMS.Services;
using InfoPCMS.Views;

namespace InfoPCMS.Views
{
    public partial class frmApproval : DevExpress.XtraEditors.XtraForm
    {
        public frmApproval()
        {
            InitializeComponent();

            btnApprove.Enabled = false;
        }

        private void frmApproval_Load(object sender, EventArgs e)
        {
            PageLoad();

            // UPDATE InquiryStatus SET PendingApproval='Yes'

        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (lblInqNo.Text.Trim() != "00000000000")
            {
                string strInqNo = lblInqNo.Text.Trim();


                //MessageBox.Show("Approved Successfully to Forward.","Info");

                DataTable dtInqStatusTemp = InfoPCMS.db.executeSelectQuery("SELECT * FROM InquiryStatusTemp WHERE InquiryNumber = '" + strInqNo.Trim() + "' ");


                string strForwardedTo = dtInqStatusTemp.Rows[0]["ForwardUser"].ToString();
                string strForwardedBy = dtInqStatusTemp.Rows[0]["SiteResponsible"].ToString();
                string strForwardedRemarks = dtInqStatusTemp.Rows[0]["Remarks"].ToString();
                string strStatus = dtInqStatusTemp.Rows[0]["Status"].ToString();
                string strNoOfForwards = dtInqStatusTemp.Rows[0]["NoOfForwards"].ToString();


                new DatabaseService().executeUpdateQuery("UPDATE InquiryStatus SET ApprovedForChange = 'Yes' , ApprovedBy='" + GetEmpId(Views.frmHome.EmpName) + "' , ApprovedDate = GETDATE() WHERE InquiryNumber='" + lblChangeInqNo.Text.Trim() + "'");


                new DatabaseService().executeUpdateQuery("UPDATE InquiryStatus SET ");

                new DatabaseService().executeUpdateQuery("UPDATE Inquiry SET ");

                XtraMessageBox.Show("Approved Successfully to Forward.", "Information");

                PageLoad();
            }
            else
            {
                // MessageBox.Show("Please Select Pending Approval Inquiry.");

                XtraMessageBox.Show("Please Select Pending Approval Inquiry.", "Error");
            }
        }

        private void grvwPendingApproval_DoubleClick(object sender, EventArgs e)
        {
            string inqNo = grvwPendingApproval.GetFocusedDataRow()["InquiryNumber"].ToString();

            lblInqNo.Text = inqNo.Trim();

            InquiryService inquiry = new InquiryService();
            inquiry = inquiry.searchInquiry(inqNo);

            if (inquiry != null)
            {
                DataTable dtInqStatusTemp = InfoPCMS.db.executeSelectQuery("SELECT * FROM InquiryStatusTemp WHERE InquiryNumber = '" + inqNo.Trim() + "' ");




                lblInqNo.Text = inqNo.Trim();
                lblInqType.Text = inquiry.ProblemNature.ToString().Trim();
                lblInqDate.Text = inquiry.InquiryDate.ToString().Trim();
                lblInqCus.Text = grvwPendingApproval.GetFocusedDataRow()["CustomerName"].ToString();
                lblInqLoc.Text = inquiry.Location.ToString().Trim();
                lblInqTaken.Text = grvwPendingApproval.GetFocusedDataRow()["inquirytaken"].ToString();
                lblInqForBy.Text = grvwPendingApproval.GetFocusedDataRow()["siteresponsible"].ToString();
                lblInqJobAssignedTo.Text = grvwPendingApproval.GetFocusedDataRow()["JobAssignedTo"].ToString();

                if (dtInqStatusTemp.Rows.Count > 0)
                {

                    lblInqForTo.Text = GetEmpName(dtInqStatusTemp.Rows[0]["ForwardUser"].ToString());
                    lblInqNoOfFor.Text = dtInqStatusTemp.Rows[0]["NoOfForwards"].ToString();
                    lblInqRemarks.Text = dtInqStatusTemp.Rows[0]["Remarks"].ToString();
                }

               

                tabPendingApprove.SelectedTabPage = xtraTabPage2;

                xtraTabPage1.PageEnabled = false;
                xtraTabPage2.PageEnabled = true;
                xtraTabPage3.PageEnabled = false;

                btnApprove.Enabled = true;
                //btnViewDetails.Enabled = false;

            }


        }

        private void xtraTabPage3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grdVwPendingAppDateChange_DoubleClick(object sender, EventArgs e)
        {
            string inqNo = grdVwPendingAppDateChange.GetFocusedDataRow()["InquiryNumber"].ToString();

            DataTable dt1 = InfoPCMS.db.executeSelectQuery(" SELECT T0.*,(SELECT CustomerName FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer)AS CustomerName, "
               + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.RequestedBy)AS EmployeeName "
               + "  FROM HayleysPowerEngineeringCRM.dbo.InqChangeCompDateTemp T0 "
               + " WHERE T0.InquiryNumber = '" + inqNo + "'");
                       

            InquiryService inquiry = new InquiryService();
            inquiry = inquiry.searchInquiry(inqNo);

            if (inquiry != null)
            {

                string[] acDate = inquiry.InquiryDate.ToString().Trim().Split(' ');
                string[] dueDate = grdVwPendingAppDateChange.GetFocusedDataRow()["dueDate"].ToString().Split(' ');
                string[] ReqDate = grdVwPendingAppDateChange.GetFocusedDataRow()["ReqCompDate"].ToString().Split(' ');

                lblChangeInqNo.Text = inqNo.Trim();
                lblChangeType.Text = inquiry.ProblemNature.ToString().Trim();
                lblChangeActDate.Text = acDate[0];
                lblChangeActTime.Text = inquiry.InquiryTime.ToString().Trim();
                lblChangeCus.Text = dt1.Rows[0]["CustomerName"].ToString();
                lblChangeLoc.Text = inquiry.Location;
                lblChangeDueDate.Text = dueDate[0]; 
                lblChangeDueTime.Text = grdVwPendingAppDateChange.GetFocusedDataRow()["dueTime"].ToString();
                lblChangeReqDate.Text = ReqDate[0];
                lblChangeReqTime.Text = grdVwPendingAppDateChange.GetFocusedDataRow()["ReqCompTime"].ToString();
                lblChangeReqBy.Text = grdVwPendingAppDateChange.GetFocusedDataRow()["EmployeeName"].ToString();
                 lblChangeReqTime.Text = grdVwPendingAppDateChange.GetFocusedDataRow()["ReqCompTime"].ToString();
                 lblChangeRemark.Text = grdVwPendingAppDateChange.GetFocusedDataRow()["Remarks"].ToString();
                

                tabPendingApprove.SelectedTabPage = xtraTabPage3;
                xtraTabPage1.PageEnabled = false;
                xtraTabPage2.PageEnabled = false;
                xtraTabPage3.PageEnabled = true;

                btnApprove.Enabled = true;
                //btnViewDetails.Enabled = false;

            }

        }

        private void btnApprov2_Click(object sender, EventArgs e)
        {

            if (lblInqNo.Text.Trim() != "00000000000")
            {
                string strInqNo = lblInqNo.Text.Trim();


                //MessageBox.Show("Approved Successfully to Forward.","Info");

                DataTable dtInqStatusTemp = InfoPCMS.db.executeSelectQuery("SELECT * FROM InquiryStatusTemp WHERE InquiryNumber = '" + strInqNo.Trim() + "' ");


                string strForwardedTo = dtInqStatusTemp.Rows[0]["ForwardUser"].ToString();
                string strForwardedBy = dtInqStatusTemp.Rows[0]["SiteResponsible"].ToString();
                string strForwardedRemarks = dtInqStatusTemp.Rows[0]["Remarks"].ToString();
                string strStatus = dtInqStatusTemp.Rows[0]["Status"].ToString();
                string strNoOfForwards = dtInqStatusTemp.Rows[0]["NoOfForwards"].ToString();
                
                new DatabaseService().executeUpdateQuery("UPDATE InquiryStatus SET  SiteResponsible = '" + strForwardedTo + "' , Remarks='" + strForwardedRemarks + "' , oldUser = '" + strForwardedBy + "',NoOfForwards = '" + strNoOfForwards + "',Status='" + strStatus.Trim() + "' WHERE InquiryNumber='" + strInqNo.Trim() + "'");
                

                new DatabaseService().executeUpdateQuery("UPDATE Inquiry SET SiteResponsible = '" + strForwardedTo + "', ApprovedForward = 'Yes' , ApprovedForwardBy='" + GetEmpId(Views.frmHome.EmpName) + "' , ApprovedForwardDate = GETDATE() WHERE InquiryNumber='" + lblInqNo.Text.Trim() + "'");
                // MessageBox.Show("Approved Successfully to Forward.");

                new DatabaseService().executeUpdateQuery("DELETE  InquiryStatusTemp WHERE InquiryNumber='" + strInqNo.Trim() + "'");
                XtraMessageBox.Show("Approved Successfully to Forward.", "Information");

                if (!Views.frmHome.isOffline)
                {
                    SendMailtoForwaredUser();
                }

                ClearForInqDetails();
                PageLoad();


                

                

                
            }
            else
            {
                // MessageBox.Show("Please Select Pending Approval Inquiry.");

                XtraMessageBox.Show("Please Select Pending Approval Inquiry.", "Error");

            }
        }

        private void SendMailtoForwaredUser()
        {
            string strFromEmail = "";
            string strToEmail = "";
            string strCCEmail = "";
            List<string> strToEmails = new List<string>();
            List<string> strCCEmails = new List<string>();


            DataTable dtInquriyDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Inquiry WHERE InquiryNumber = '" + lblInqNo.Text.ToString() + "'");

            DataTable dtCusDetais = new DatabaseService().executeSelectQuery("SELECT * FROM Customer WHERE Id = '" + GetCusId(lblInqCus.Text.Trim()) + "'");


            strFromEmail = GetEmail(GetEmpId(Views.frmHome.EmpName));
            strToEmail = GetEmail(GetEmpId(lblInqForTo.Text.Trim()));
            strCCEmail = GetEmail(GetEmpId(lblInqForBy.Text.Trim()));
            StringBuilder strBuliderEmailSubject = new StringBuilder();
            strBuliderEmailSubject.Append("Inquiry Forward Approved – Customer :" + lblInqCus.Text.Trim() + "  " + " -Inquiry No :" + lblInqNo.Text.Trim() + " ");


            StringBuilder strBuliderEmailBody = new StringBuilder();
            strBuliderEmailBody.Append("<head>");
            strBuliderEmailBody.Append("<title>");
            strBuliderEmailBody.Append(Guid.NewGuid().ToString());
            strBuliderEmailBody.Append("</title>");
            strBuliderEmailBody.Append("</head>");
            strBuliderEmailBody.Append("<body>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Dear " + lblInqForTo.Text.Trim() + ",");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Inquiry <b><u><FONT COLOR=DodgerBlue>Forwarded</FONT></u></b>, pending for your Acceptance,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Inquiry No : </b>" + lblInqNo.Text.Trim() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Type : </b>" + lblInqType.Text.Trim() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Customer : </b>" + lblInqCus.Text.Trim() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Location : </b>" + GetSubLocationName(dtInquriyDetails.Rows[0]["Location"].ToString()) + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Caller Name : </b>" + dtInquriyDetails.Rows[0]["CallerName"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Caller Phone : </b>" + dtInquriyDetails.Rows[0]["CallerPhone"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Inquiry Details : </b>" + dtInquriyDetails.Rows[0]["FollowupDetails"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Priority : </b>" + dtInquriyDetails.Rows[0]["Priority"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Job Entry Date & Time : </b>" + Convert.ToDateTime(dtInquriyDetails.Rows[0]["JobEntryDate"].ToString()).ToString("mm/dd/yyyy") + "&nbsp;&nbsp;" + dtInquriyDetails.Rows[0]["JobEntryTime"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Forwarded By : </b>" + lblInqForBy.Text.Trim() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Remarks : </b>" + lblInqRemarks.Text.Trim() + "");

            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Forward Approved By : </b>" + Views.frmHome.EmpName + "");
            strBuliderEmailBody.Append("<br>");

            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Forward Approved Date & Time : </b>" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "");
            strBuliderEmailBody.Append("<br>");

           
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Regards,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("" + Views.frmHome.EmpName + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");

            strBuliderEmailBody.Append("<FONT COLOR=darkred>Auto generated email from CRM system</FONT>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("</body>");
            strBuliderEmailBody.Append("</html>");


            EmailServices Email = new EmailServices();

            Email.SendEmailsCC(strFromEmail.Trim(), strToEmail.Trim(),strCCEmail.Trim(), strBuliderEmailSubject.ToString(), strBuliderEmailBody.ToString());
   

        
        }
        private string GetSubLocationName(string strSubLocId)
        {
            string strSubLocName = "";
            DataTable dtSubLocations = new DatabaseService().executeSelectQuery("select Id,SubLocation from CustomerLocation  WHERE Id='" + strSubLocId.Trim() + "'");


            if (dtSubLocations.Rows.Count > 0)
            {

                strSubLocName = dtSubLocations.Rows[0]["SubLocation"].ToString();
            }

            return strSubLocName;
        }
        private void SendMailtoApproved()
        {
            string strFromEmail = "";
            string strToEmail = "";
            string strCCEmail = "";
            List<string> strToEmails = new List<string>();
            List<string> strCCEmails = new List<string>();


            DataTable dtInquriyDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Inquiry WHERE InquiryNumber = '" + lblChangeInqNo.Text.ToString() + "'");

            DataTable dtCusDetais = new DatabaseService().executeSelectQuery("SELECT * FROM Customer WHERE Id = '" + GetCusId(lblChangeCus.Text.Trim()) + "'");

            string[] acDate = lblChangeActDate.Text.ToString().Trim().Split(' ');
           // string[] dueDate = grdVwPendingAppDateChange.GetFocusedDataRow()["ReqCompDate"].ToString().Split(' ');
            string[] ReqDate = lblChangeReqDate.Text.ToString().Trim().Split(' ');


            strFromEmail = GetEmail(GetEmpId(Views.frmHome.EmpName));
            strToEmail = GetEmail(dtInquriyDetails.Rows[0]["SiteResponsible"].ToString());
            //strCCEmail = GetEmail(GetEmpId(lblInqForBy.Text.Trim()));
            StringBuilder strBuliderEmailSubject = new StringBuilder();
            strBuliderEmailSubject.Append("Inquiry Completion Date Change Approved – Customer :" + lblChangeCus.Text.Trim() + "  " + " -Inquiry No :" + lblChangeInqNo.Text.Trim() + " ");


            StringBuilder strBuliderEmailBody = new StringBuilder();
            strBuliderEmailBody.Append("<head>");
            strBuliderEmailBody.Append("<title>");
            strBuliderEmailBody.Append(Guid.NewGuid().ToString());
            strBuliderEmailBody.Append("</title>");
            strBuliderEmailBody.Append("</head>");
            strBuliderEmailBody.Append("<body>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Dear " + lblChangeReqBy.Text.Trim() + ",");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Inquiry Completion Date Change <b><u><FONT COLOR=DodgerBlue>Approved</FONT></u></b>,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Inquiry No : </b>" + lblChangeInqNo.Text.Trim() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Type : </b>" + lblChangeType.Text.Trim() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Customer : </b>" + lblChangeCus.Text.Trim() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Location : </b>" + GetSubLocationName( dtInquriyDetails.Rows[0]["Location"].ToString()) + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Caller Name : </b>" + dtInquriyDetails.Rows[0]["CallerName"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Caller Phone : </b>" + dtInquriyDetails.Rows[0]["CallerPhone"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Inquiry Details : </b>" + dtInquriyDetails.Rows[0]["FollowupDetails"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Priority : </b>" + dtInquriyDetails.Rows[0]["Priority"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Accepted  Date & Time : </b>" + acDate[0] + "&nbsp;&nbsp;" + lblChangeActTime.Text.ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>New Completion  Date & Time : </b>" + ReqDate[0] + "&nbsp;&nbsp;" + lblChangeReqTime.Text.ToString() + "");
            strBuliderEmailBody.Append("<br>");  

            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Regards,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("" + Views.frmHome.EmpName + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");

            strBuliderEmailBody.Append("<FONT COLOR=darkred>Auto generated email from CRM system</FONT>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("</body>");
            strBuliderEmailBody.Append("</html>");


            EmailServices Email = new EmailServices();

            Email.SendEmails(strFromEmail.Trim(), strToEmail.Trim(), strBuliderEmailSubject.ToString(), strBuliderEmailBody.ToString());



        }

        public string GetEmail(string strEmpName)
        {
            string strEmail = "";

            DataTable dt;
            dt = new DatabaseService().executeSelectQuery("SELECT Email FROM Employee WHERE Id = '" + strEmpName + "'");
            if (dt.Rows.Count > 0)
            {

                strEmail = dt.Rows[0]["Email"].ToString();
            }

            return strEmail;

        }

        private void PageLoad()
        {
            DataTable dt0 = InfoPCMS.db.executeSelectQuery("SELECT i.InquiryNumber,i.Date,c.CustomerName,e1.EmployeeName as inquirytaken,"
                + " e3.EmployeeName as siteresponsible ,e2.EmployeeName as JobAssignedTo,i.Done as Done ,i.active as active FROM HayleysPowerEngineeringCRM.dbo.Inquiry i "
                + " INNER JOIN HayleysPowerEngineeringCRM.dbo.Employee e1 on i.InquiryTaken = e1.Id  "
                + " INNER JOIN HayleysPowerEngineeringCRM.dbo.Customer c on i.CustomerId = c.Id "
                + " INNER JOIN HayleysPowerEngineeringCRM.dbo.Employee e3 on i.SiteResponsible = e3.Id "
                + " INNER JOIN HayleysPowerEngineeringCRM.dbo.Employee e2 on i.JobAssignedTo = e2.Id "
                + " WHERE i.active='Active' and i.Done='No' AND i.RequestForward ='Yes' AND i.ApprovedForward IS NULL ORDER BY i.InquiryNumber ");
            

                gridPendingApproval.DataSource = dt0;

            //DataTable dt1 = InfoPCMS.db.executeSelectQuery("SELECT T1.InquiryNumber,T2.Date,T1.Customer,T1.actual,T1.SiteResponsible FROM InquiryStatus T1 LEFT JOIN Inquiry T2 ON T1.InquiryNumber = T2.InquiryNumber WHERE PendingApproval = 'Yes' AND ApprovedForChange IS NULL");
            
                DataTable dt1 = InfoPCMS.db.executeSelectQuery(" SELECT T0.*,(SELECT CustomerName FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer)AS CustomerName, "
                +" (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.RequestedBy)AS EmployeeName "
                +"  FROM HayleysPowerEngineeringCRM.dbo.InqChangeCompDateTemp T0 "
                +" WHERE T0.IsApproved IS NULL" );
            
            
            grdPendingAppDateChange.DataSource = dt1;

            tabPendingApprove.SelectedTabPage = xtraTabPage1;
            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = false;

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void xtraTabPage2_Paint(object sender, PaintEventArgs e)
        {

        }

        private string GetEmpName(string strEmpId)
        {
            string strEmpName = "";
            DataTable dtEmployee = new DatabaseService().executeSelectQuery("select Id,EmployeeName from Employee  WHERE Id='" + strEmpId.Trim() + "'");


            if (dtEmployee.Rows.Count > 0)
            {

                strEmpName = dtEmployee.Rows[0]["EmployeeName"].ToString();
            }

            return strEmpName;
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

        private void ClearForInqDetails()
        {
            lblInqNo.Text = "";
            lblInqType.Text = "";
            lblInqDate.Text = "";
            lblInqCus.Text = "";
            lblInqLoc.Text = "";
            lblInqTaken.Text = "";
            lblInqForBy.Text = "";
            lblInqJobAssignedTo.Text = "";
            lblInqForTo.Text = "";
            lblInqNoOfFor.Text = "";
            btnApprov2.Enabled = false;
        }

        private void ClearChangeInqDetails()
        {
            lblChangeInqNo.Text = "";
            lblChangeLoc.Text = "";
            lblChangeCus.Text = "";
            lblChangeActDate.Text = "";
            lblChangeActTime.Text = "";
            lblChangeDueDate.Text = "";
            lblChangeDueTime.Text = "";
            lblChangeRemark.Text = "";
            lblChangeReqBy.Text = "";
            lblChangeReqDate.Text = "";
            lblChangeReqTime.Text = "";

            btnApprove.Enabled = false;
        }

        private void btnBackFor_Click(object sender, EventArgs e)
        {
            tabPendingApprove.SelectedTabPage = xtraTabPage1;
            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = false;
        }

        private void btnBackChange_Click(object sender, EventArgs e)
        {
            tabPendingApprove.SelectedTabPage = xtraTabPage1;
            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = false;
        }

        private void btnApprove_Click_1(object sender, EventArgs e)
        {
            if (lblChangeInqNo.Text.Trim() != "00000000000")
            {
                string strInqNo = lblChangeInqNo.Text.Trim();

                DataTable dtInqChangeReq = InfoPCMS.db.executeSelectQuery("SELECT * FROM InqChangeCompDateTemp WHERE InquiryNumber='" + strInqNo.Trim() + "'");

                new DatabaseService().executeUpdateQuery("UPDATE InqChangeCompDateTemp SET Status='Approved',IsApproved = 'True' , ApprovedBy='" + GetEmpId(Views.frmHome.EmpName) + "' , ApprovedDate = GETDATE() WHERE InquiryNumber='" + strInqNo.Trim() + "'");

                new DatabaseService().executeUpdateQuery("UPDATE InquiryStatus SET duedate='" + dtInqChangeReq.Rows[0]["ReqCompDate"].ToString() + "',dueTime='" + dtInqChangeReq.Rows[0]["ReqCompTime"].ToString() + "',actual = '" + dtInqChangeReq.Rows[0]["actual"].ToString() + "' WHERE InquiryNumber= '" + strInqNo.Trim() + "'");

                XtraMessageBox.Show("Approved Request for Completion Date Change.", "Information");

                ClearChangeInqDetails();
                PageLoad();

                if (!Views.frmHome.isOffline)
                {
                    SendMailtoApproved();
                }

              
            }
        }
    }
}