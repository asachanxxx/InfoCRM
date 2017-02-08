using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using InfoPCMS.Services;
using System.Globalization;
using System.Net.Mail;
using System.IO;
using System.Diagnostics;
using InfoPCMS.Services;
using InfoCrm.Tools;


namespace HayleysPowerEngineeringCRM.View
{


    public partial class FrmMyTask : DevExpress.XtraEditors.XtraForm
    {


        public FrmMyTask()
        {
            InitializeComponent();
            try
            {
                TableLoad();
                LoadDataJobService();

                // DataTable dt3 = new DatabaseService().executeSelectQuery("select * from Employee");
                //cmbBxJobDoneBy.DataSource = dt3;
                //cmbBxJobDoneBy.ValueMember = "EmployeeName";
                //cmbBxJobDoneBy.DisplayMember = "EmployeeName";

                xtraTabControl1.SelectedTabPageIndex = 01;

                xtraTabPage1.PageEnabled = true;
                xtraTabPage2.PageEnabled = false;
                xtraTabPage4.PageEnabled = false;
                xtraTabPage5.PageEnabled = false;
                xtraTabPage7.PageEnabled = false;
                xtraTabPage13.PageEnabled = false;

            }
            catch (Exception ex)
            {

            }
        }

        DataTable Dtgridaccepted = new DataTable();
        List<FileModel> FileList = new List<FileModel>();



        private void TableLoad()
        {
            try
            {
                DataTable dbuser = new DatabaseService().executeSelectQuery("select  Id,EmployeeName from Employee WHERE EmployeeName !='" + InfoPCMS.Views.frmHome.EmpName + "'");

                FileList = LoadFileList();

                cboUsers.DataSource = dbuser;
                cboUsers.DisplayMember = "EmployeeName";
                cboUsers.ValueMember = "EmployeeName";

                txtRemarks.Text = "";

                if (InfoPCMS.Views.frmHome.EmpCat == "LEVEL 3")
                {

                    DataTable dt = new DatabaseService().executeSelectQuery("SELECT i.InquiryNumber,i.JobEntryDate AS Date,i.JobEntryTime AS Time,(cast(cast(i.Date as date) as datetime) + cast(i.Time as time(7))) AS DateOld, "
                    + " c.CustomerName, c2.CategoryName AS CustomerCategory ,E1.EmployeeName AS InquiryTaken,i.InspectionDone,E2.EmployeeName AS SiteResponsible "
                    + " FROM HayleysPowerEngineeringCRM.dbo.Inquiry i "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer c on i.CustomerId = c.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory c2 on c.CustomerCategory = c2.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee E1 on i.InquiryTaken= E1.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee E2 on i.SiteResponsible= E2.Id "
                    + " WHERE i.Done != 'Yes'");
                    gridInquiryList.DataSource = dt;

                    Dtgridaccepted.Clear();

                    Dtgridaccepted = new DatabaseService().executeSelectQuery("SELECT (select Sum(CAST(Percentage as numeric(9))) FROM HayleysPowerEngineeringCRM.dbo.InquiryStatusDetails "
                    + " where InquiryNumber=T0.InquiryNumber) as percentage, "
                    + " T0.actual,T0.duedate,T0.dueTime, T0.InquiryNumber as  InquiryNumber,T1.JobEntryDate,T1.JobEntryTime, "
                    + " T2.CustomerName as Customer,T2.CustomerCategory,T5.CategoryName,T3.EmployeeName as InquiryTaken, "
                    + " T0.InspectionDone as InspectionDone,t4.EmployeeName as SiteResponsible, "
                    + " T0.NextAction as NextAction,T0.DateTime"
                    + " FROM HayleysPowerEngineeringCRM.dbo.InquiryStatus T0  "
                    + " INNER JOIN HayleysPowerEngineeringCRM.dbo.Inquiry T1  ON T0.InquiryNumber = T1.InquiryNumber "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T3 ON T0.InquiryTaken = T3.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T4 ON T1.SiteResponsible = T4.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T5 ON T2.CustomerCategory = T5.Id "
                    + " where T0.Status='Accept' ");

                    gridaccepted.DataSource = Dtgridaccepted;

                    DataTable Forward = new DatabaseService().executeSelectQuery("SELECT T0.InquiryNumber,T0.Date,T0.Time,"
                       + " (SELECT CustomerName FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer)as  Customer, "
                        + " (SELECT CustomerCategory FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer)as  CustomerCategory, "
                       + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.InquiryTaken) as  InquiryTaken,"
                       + " T0.InspectionDone as InspectionDone, "
                       + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T1.SiteResponsible) as  SiteResponsible  "
                       + " FROM HayleysPowerEngineeringCRM.dbo.InquiryStatus T0 INNER JOIN HayleysPowerEngineeringCRM.dbo.Inquiry T1 "
                        + "  ON T0.InquiryNumber = T1.InquiryNumber where T0.Status='Forward'");

                    tblForwarded.DataSource = Forward;

                    DataTable Completed = new DatabaseService().executeSelectQuery("SELECT T0.InquiryNumber,T0.Date,T0.Time,"
                         + " (SELECT CustomerName FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer)as  Customer,"
                          + " (SELECT CustomerCategory FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer)as  CustomerCategory, "
                         + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.InquiryTaken) as  InquiryTaken,"
                         + " T0.InspectionDone as InspectionDone, "
                         + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T1.SiteResponsible) as  SiteResponsible  "
                         + " FROM HayleysPowerEngineeringCRM.dbo.InquiryStatus T0 INNER JOIN HayleysPowerEngineeringCRM.dbo.Inquiry T1 "
                          + "  ON T0.InquiryNumber = T1.InquiryNumber where T0.Status='Completed'");


                    TblCompleted.DataSource = Completed;

                    DataTable UpComming = new DatabaseService().executeSelectQuery("SELECT* FROM ServiceShedule WHERE Status='UpComming'");

                    DataTable Due = new DatabaseService().executeSelectQuery("SELECT* FROM ServiceShedule WHERE Status='Due'");

                }
                else
                {

                    DataTable dtuser = new DatabaseService().executeSelectQuery("select Id from Users  WHERE Username='" + InfoPCMS.Views.frmHome.UsrName + "'");
                    DataTable dtEmployee = new DatabaseService().executeSelectQuery("select Id,EmployeeName from Employee  WHERE EmployeeName='" + InfoPCMS.Views.frmHome.EmpName + "'");


                    if (dtuser.Rows.Count > 0)
                    {

                        String Id = dtuser.Rows[0]["Id"].ToString();

                        String EmpName = dtEmployee.Rows[0]["EmployeeName"].ToString().Trim();
                        String EmpId = dtEmployee.Rows[0]["Id"].ToString().Trim();

                        DataTable dt = new DatabaseService().executeSelectQuery("SELECT i.InquiryNumber,i.JobEntryDate AS Date,i.JobEntryTime AS Time,(cast(cast(i.Date as date) as datetime) + cast(i.Time as time(7))) AS DateOld, "
                    + " c.CustomerName, c2.CategoryName AS CustomerCategory ,E1.EmployeeName AS InquiryTaken,i.InspectionDone,E2.EmployeeName AS SiteResponsible "
                    + " FROM HayleysPowerEngineeringCRM.dbo.Inquiry i "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer c on i.CustomerId = c.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory c2 on c.CustomerCategory = c2.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee E1 on i.InquiryTaken= E1.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee E2 on i.SiteResponsible= E2.Id "
                    + " WHERE i.SiteResponsible = '" + EmpId + "' AND i.Done != 'Yes'");

                        gridInquiryList.DataSource = dt;
                        Dtgridaccepted.Clear();
                        Dtgridaccepted = new DatabaseService().executeSelectQuery("SELECT (select Sum(CAST(Percentage as numeric(9))) FROM HayleysPowerEngineeringCRM.dbo.InquiryStatusDetails "
                   + " where InquiryNumber=T0.InquiryNumber) as percentage, "
                   + " T0.actual,T0.duedate,T0.dueTime, T0.InquiryNumber as  InquiryNumber,T1.JobEntryDate,T1.JobEntryTime, "
                   + " T2.CustomerName as Customer,T2.CustomerCategory,T5.CategoryName,T3.EmployeeName as InquiryTaken, "
                   + " T0.InspectionDone as InspectionDone,t4.EmployeeName as SiteResponsible, "
                   + " T0.NextAction as NextAction,T0.DateTime "
                   + " FROM HayleysPowerEngineeringCRM.dbo.InquiryStatus T0  "
                   + " INNER JOIN HayleysPowerEngineeringCRM.dbo.Inquiry T1  ON T0.InquiryNumber = T1.InquiryNumber "
                   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
                   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T3 ON T0.InquiryTaken = T3.Id "
                   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T4 ON T1.SiteResponsible = T4.Id "
                   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T5 ON T2.CustomerCategory = T5.Id "
                   + " where T0.Status='Accept' AND T1.SiteResponsible = '" + EmpId + "' ");

                        gridaccepted.DataSource = Dtgridaccepted;

                        DataTable Forward = new DatabaseService().executeSelectQuery("SELECT T0.InquiryNumber,T0.Date,T0.Time,"
                          + " (SELECT CustomerName FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer)as  Customer,"
                           + " (SELECT CustomerCategory FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer)as  CustomerCategory, "
                          + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.InquiryTaken) as  InquiryTaken,"
                          + " T0.InspectionDone as InspectionDone, "
                          + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T1.SiteResponsible) as  SiteResponsible  "
                          + " FROM HayleysPowerEngineeringCRM.dbo.InquiryStatus T0 INNER JOIN HayleysPowerEngineeringCRM.dbo.Inquiry T1 "
                           + "  ON T0.InquiryNumber = T1.InquiryNumber where T0.Status='Forward' and T0.oldUser='" + EmpId + "'");

                        tblForwarded.DataSource = Forward;

                        DataTable Completed = new DatabaseService().executeSelectQuery("SELECT T0.InquiryNumber,T0.Date,T0.Time,"
                             + " (SELECT CustomerName FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer)as  Customer,"
                              + " (SELECT CustomerCategory FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer)as  CustomerCategory, "
                             + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.InquiryTaken) as  InquiryTaken,"
                             + " T0.InspectionDone as InspectionDone, "
                             + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T1.SiteResponsible) as  SiteResponsible  "
                             + " FROM HayleysPowerEngineeringCRM.dbo.InquiryStatus T0 INNER JOIN HayleysPowerEngineeringCRM.dbo.Inquiry T1 "
                              + "  ON T0.InquiryNumber = T1.InquiryNumber where T0.Status='Completed' AND T0.oldUser='" + EmpId + "'");

                        TblCompleted.DataSource = Completed;
                        DateTime todate = DateTime.Now.AddDays(14);

                        DataTable UpComming = new DatabaseService().executeSelectQuery("SELECT T0.*, "
                       + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.ResponsiblePerson) AS ResponsiblePersonName, "
                       + " (SELECT CustomerName FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer) AS CustomerName, "
                        + " (SELECT CustomerCategory FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer)as  CustomerCategory, "
                       + " (SELECT SubLocation FROM HayleysPowerEngineeringCRM.dbo.CustomerLocation  WHERE Id = T0.Location) AS SubLocation "
                       + " FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 where T0.Status='Up Comming' AND T0.ResponsiblePerson='" + EmpId + "' AND T0.PlanDate < '" + todate + "'");

                        gridUpComming.DataSource = UpComming;

                        DataTable Due = new DatabaseService().executeSelectQuery("SELECT T0.*, "
                         + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.ResponsiblePerson) AS ResponsiblePersonName, "
                         + " (SELECT CustomerName FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer) AS CustomerName, "
                          + " (SELECT CustomerCategory FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer)as  CustomerCategory, "
                         + " (SELECT SubLocation FROM HayleysPowerEngineeringCRM.dbo.CustomerLocation  WHERE Id = T0.Location) AS SubLocation "
                         + " FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 where T0.Status <> 'Completed' AND T0.PlanDate < GETDATE() AND T0.ResponsiblePerson='" + EmpId + "' ");

                        gridDueServices.DataSource = Due;


                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,Globals.MessageCaption,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }





        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount.ToString() == "0")
            {
                XtraMessageBox.Show("No Pending Inquiries.", "Information");
            }
            else
            {
                DataTable dtuser2 = new DatabaseService().executeSelectQuery("select Id from Users  WHERE Username='" + InfoPCMS.Views.frmHome.UsrName + "'");

                DataTable dtEmp2 = new DatabaseService().executeSelectQuery("select Id,EmployeeName from Employee  WHERE EmployeeName= '" + InfoPCMS.Views.frmHome.EmpName + "'");

                if (dtuser2.Rows.Count > 0)
                {

                    DataTable dtInqDetails = new DatabaseService().executeSelectQuery("select * from Inquiry  WHERE InquiryNumber= '" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "'");

                    DataTable dtCusDetails = new DatabaseService().executeSelectQuery("select * from Customer WHERE Id ='" + dtInqDetails.Rows[0]["CustomerId"].ToString() + "'");

                    //DateTime inqDate = Convert.ToDateTime(gridView1.GetFocusedDataRow()["Date"].ToString());
                    //DateTime inqTime = Convert.ToDateTime(gridView1.GetFocusedDataRow()["Time"].ToString());

                    //DateTime inqDateTime = inqDate.Date.Add(inqTime.TimeOfDay);


                    DateTime inqDate = Convert.ToDateTime(dtInqDetails.Rows[0]["JobEntryDate"].ToString());
                    DateTime inqTime = Convert.ToDateTime(dtInqDetails.Rows[0]["JobEntryTIme"].ToString());

                    DateTime inqDateTime = inqDate.Date.Add(inqTime.TimeOfDay);

                    // DateTime dtDueDate = getDueDate(gridView1.GetFocusedDataRow()["InquiryNumber"].ToString(), inqDateTime);


                    // DateTime dtDueDate = getDueDate(gridView1.GetFocusedDataRow()["InquiryNumber"].ToString(), DateTime.Now);

                    //  DateTime dtDueDateNew = GetDueDateTime(gridView1.GetFocusedDataRow()["InquiryNumber"].ToString(), inqDateTime);

                    DateTime dtDueDateNew = DoCalculation(gridView1.GetFocusedDataRow()["InquiryNumber"].ToString(), inqDateTime);
                    var dueDate = dtDueDateNew.Date;
                    string strDueDate = dueDate.ToString("yyyy-MM-dd");
                    // var dueDate = dtDueDate.TimeOfDay;

                    var dueTime = dtDueDateNew - dtDueDateNew.Date;
                    //string strDueTime = dueTime.ToString("HH:mm:ss");

                    DateTime dtReminder1 = GetReminder1(gridView1.GetFocusedDataRow()["InquiryNumber"].ToString());
                    var Reminder1Date = dtReminder1.Date;
                    string strReminder1Date = Reminder1Date.ToString("yyyy-MM-dd");
                    var Reminder1Time = dtReminder1 - dtReminder1.Date;

                    var Reminder2Date = new DateTime();
                    string strReminder2Date = "";
                    var Reminder2Time = new TimeSpan();

                    if (dtCusDetails.Rows[0]["CustomerCategory"].ToString() == "1")
                    {
                        DateTime dtReminder2 = GetReminder2(gridView1.GetFocusedDataRow()["InquiryNumber"].ToString());
                        Reminder2Date = dtReminder2.Date;
                        strReminder2Date = Reminder2Date.ToString("yyyy-MM-dd");
                        Reminder2Time = dtReminder2 - dtReminder2.Date;
                    }

                    DateTime dtEscalation1 = GetEscalation1(gridView1.GetFocusedDataRow()["InquiryNumber"].ToString());
                    var Escalation1Date = dtEscalation1.Date;
                    string strEscalation1Date = Escalation1Date.ToString("yyyy-MM-dd");
                    var Escalation1Time = dtEscalation1 - dtEscalation1.Date;

                    DateTime dtEscalation2 = GetEscalation2(gridView1.GetFocusedDataRow()["InquiryNumber"].ToString());
                    var Escalation2Date = dtEscalation2.Date;
                    string strEscalation2Date = Escalation2Date.ToString("yyyy-MM-dd");
                    var Escalation2Time = dtEscalation2 - dtEscalation2.Date;


                    string strCustomorId = dtInqDetails.Rows[0]["CustomerId"].ToString();// GetCusId(gridView1.GetFocusedDataRow()["CustomerName"].ToString());
                    string strInqTaken = GetEmpId(gridView1.GetFocusedDataRow()["inquirytaken"].ToString());
                    string strSiteResponsible = GetEmpId(gridView1.GetFocusedDataRow()["siteresponsible"].ToString());

                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string currentTime = DateTime.Now.ToString("HH:mm:ss");


                    DataTable dtInqStatus = new DatabaseService().executeSelectQuery("select * from InquiryStatus  WHERE InquiryNumber= '" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "'");

                    if (dtInqStatus.Rows.Count > 0)
                    {
                        // new DatabaseService().executeUpdateQuery("UPDATE InquiryStatus SET Date= '" + gridView1.GetFocusedDataRow()["Date"].ToString() + "',Time= '" + gridView1.GetFocusedDataRow()["Time"].ToString() + "',Status='Accept',duedate='" + dueDate.ToString() + "',dueTime='" + dueTime.ToString() + "' WHERE InquiryNumber= '" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "'");

                        new DatabaseService().executeUpdateQuery("UPDATE InquiryStatus SET Date= '" + currentDate.ToString() + "',Time= '" + currentTime.ToString() + "',Status='Accept',duedate='" + strDueDate.ToString() + "',dueTime='" + dueTime.ToString() + "' WHERE InquiryNumber= '" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "'");

                    }
                    else
                    {
                        //new DatabaseService().executeUpdateQuery("INSERT INTO InquiryStatus VALUES('" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "','" + gridView1.GetFocusedDataRow()["Date"].ToString() + "','" + gridView1.GetFocusedDataRow()["Time"].ToString() + "','" + strCustomorId.Trim() + "','" + strInqTaken + "','" + gridView1.GetFocusedDataRow()["inspectiondone"].ToString() + "','" + strSiteResponsible.Trim() + "','" + strSiteResponsible.Trim() + "','','" + txtRemarks.Text + "','Accept','" + dtEmp2.Rows[0]["Id"].ToString() + "','0','','" + dueDate.ToString() + "','" + dueTime.ToString() + "','','','0','','','','')");


                        new DatabaseService().executeUpdateQuery("INSERT INTO InquiryStatus VALUES('" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "','" + currentDate.ToString() + "','" + currentTime.ToString() + "','" + strCustomorId.Trim() + "','" + strInqTaken + "','" + gridView1.GetFocusedDataRow()["inspectiondone"].ToString() + "','" + strSiteResponsible.Trim() + "','" + strSiteResponsible.Trim() + "','','" + txtRemarks.Text + "','Accept','" + dtEmp2.Rows[0]["Id"].ToString() + "','0','','" + strDueDate.ToString() + "','" + dueTime.ToString() + "','','','0','','','','','')");

                    }

                    new DatabaseService().executeUpdateQuery("UPDATE Inquiry SET Done='Yes' WHERE InquiryNumber='" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "'");

                    //  new DatabaseService().executeUpdateQuery("INSERT INTO InquiryEscalations VALUES('" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "','" + strSiteResponsible.ToString() + "','" + currentDate.ToString() + "','" + currentTime.ToString() + "','" + strDueDate.ToString() + "','" + dueTime.ToString() + "','false','false','false')");

                    if (dtCusDetails.Rows[0]["CustomerCategory"].ToString() == "1")
                    {
                        new DatabaseService().executeUpdateQuery("INSERT INTO InqEscalation VALUES('" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "','" + strSiteResponsible.ToString() + "','" + currentDate.ToString() + "','" + currentTime.ToString() + "','" + strDueDate.ToString() + "','" + dueTime.ToString() + "','" + strReminder1Date.ToString() + "','" + Reminder1Time.ToString() + "','false','True','" + strReminder2Date.ToString() + "','" + Reminder2Time.ToString() + "','false','" + strEscalation1Date.ToString() + "','" + Escalation1Time.ToString() + "','false','" + strEscalation2Date.ToString() + "','" + Escalation2Time.ToString() + "','false')");
                    }
                    else
                    {
                        new DatabaseService().executeUpdateQuery("INSERT INTO InqEscalation VALUES('" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "','" + strSiteResponsible.ToString() + "','" + currentDate.ToString() + "','" + currentTime.ToString() + "','" + strDueDate.ToString() + "','" + dueTime.ToString() + "','" + strReminder1Date.ToString() + "','" + Reminder1Time.ToString() + "','false','false','','','','" + strEscalation1Date.ToString() + "','" + Escalation1Time.ToString() + "','false','" + strEscalation2Date.ToString() + "','" + Escalation2Time.ToString() + "','false')");

                    }


                    XtraMessageBox.Show("Inquiry Accepted Successfully", "Information");

                    DateTime firstEsc = calculationEscOne(gridView1.GetFocusedDataRow()["InquiryNumber"].ToString());

                    TableLoad();

                    xtraTabControl1.SelectedTabPageIndex = 1;



                    xtraTabPage1.PageEnabled = false;
                    xtraTabPage2.PageEnabled = true;
                    xtraTabPage4.PageEnabled = false;
                    xtraTabPage5.PageEnabled = false;
                    xtraTabPage7.PageEnabled = false;
                    xtraTabPage13.PageEnabled = false;

                    xtraTabControl2.SelectedTabPageIndex = 0;
                    xtraTabPage3.PageEnabled = true;
                    xtraTabPage6.PageEnabled = false;
                    tbPageChangeInqDetails.PageEnabled = false;

                    //lblType.Text = "d";
                }
            }
        }

        private DateTime GetReminder1(string strInqNo)
        {

            DateTime dtReminder1 = new DateTime();

            DataTable dtInqStatus = new DatabaseService().executeSelectQuery("select * from InquiryStatus  WHERE InquiryNumber= '" + strInqNo + "'");
            DataTable dtInqDetails = new DatabaseService().executeSelectQuery("select * from Inquiry  WHERE InquiryNumber= '" + strInqNo + "'");


            DateTime inqDate = Convert.ToDateTime(dtInqDetails.Rows[0]["JobEntryDate"].ToString());
            DateTime inqTime = Convert.ToDateTime(dtInqDetails.Rows[0]["JobEntryTIme"].ToString());

            DateTime inqDateTime = inqDate.Date.Add(inqTime.TimeOfDay);

            DataTable dtCusDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Customer WHERE Id = '" + dtInqDetails.Rows[0]["CustomerId"].ToString().Trim() + "' ");

            DataTable dtDueDate = new DataTable();
            DataTable dtInqTypeDetails = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryType WHERE InquiryType = '" + dtInqDetails.Rows[0]["ProblemNature"].ToString().Trim() + "' AND CusCategory = '" + dtCusDetails.Rows[0]["CustomerCategory"].ToString().Trim() + "'");

            int intReminder1Minites = 0;

            if (dtInqTypeDetails.Rows.Count > 0)
            {
                intReminder1Minites = (Convert.ToInt32(dtInqTypeDetails.Rows[0]["CompletionHours"].ToString()) * Convert.ToInt32(dtInqTypeDetails.Rows[0]["Reminder1"].ToString()) * 60) / 100;
            }

            if (inqDateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                // if the input date is a sunday, set the actual SLA start date to the following monday morning 7:00AM
                inqDateTime = inqDateTime.AddHours(24);
                inqDateTime = new DateTime(inqDateTime.Year, inqDateTime.Month, inqDateTime.Day, 7, 0, 0);
            }
            else if (inqDateTime.DayOfWeek == DayOfWeek.Saturday)
            {
                // if the input date is a saturday, set the actual SLA start date to the following monday morning 7:00AM
                inqDateTime = inqDate.AddHours(48);
                inqDateTime = new DateTime(inqDateTime.Year, inqDateTime.Month, inqDateTime.Day, 7, 0, 0);
            }

            dtReminder1 = inqDateTime;
            for (int i = 0; i < intReminder1Minites; i++)
            {
                dtReminder1 = dtReminder1.AddMinutes(1);

                // it is 5PM and time to go home
                if (dtReminder1.Hour >= 17)
                {
                    // if tomorrow is saturday
                    if (dtReminder1.AddDays(1).DayOfWeek == DayOfWeek.Saturday)
                    {
                        //add 48 hours to get us through the whole weekend
                        dtReminder1 = dtReminder1.AddHours(48);
                    }

                    // add 14 hours to get us to next morning
                    dtReminder1 = dtReminder1.AddHours(14);
                }
            }
            return dtReminder1;

        }

        private DateTime GetReminder2(string strInqNo)
        {

            DateTime dtReminder2 = new DateTime();

            DataTable dtInqStatus = new DatabaseService().executeSelectQuery("select * from InquiryStatus  WHERE InquiryNumber= '" + strInqNo + "'");
            DataTable dtInqDetails = new DatabaseService().executeSelectQuery("select * from Inquiry  WHERE InquiryNumber= '" + strInqNo + "'");


            DateTime inqDate = Convert.ToDateTime(dtInqDetails.Rows[0]["JobEntryDate"].ToString());
            DateTime inqTime = Convert.ToDateTime(dtInqDetails.Rows[0]["JobEntryTIme"].ToString());

            DateTime inqDateTime = inqDate.Date.Add(inqTime.TimeOfDay);

            DataTable dtCusDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Customer WHERE Id = '" + dtInqDetails.Rows[0]["CustomerId"].ToString().Trim() + "' ");

            DataTable dtDueDate = new DataTable();
            DataTable dtInqTypeDetails = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryType WHERE InquiryType = '" + dtInqDetails.Rows[0]["ProblemNature"].ToString().Trim() + "' AND CusCategory = '" + dtCusDetails.Rows[0]["CustomerCategory"].ToString().Trim() + "'");

            int intReminder1Minites = 0;

            if (dtInqTypeDetails.Rows.Count > 0)
            {
                intReminder1Minites = (Convert.ToInt32(dtInqTypeDetails.Rows[0]["CompletionHours"].ToString()) * Convert.ToInt32(dtInqTypeDetails.Rows[0]["Reminder2"].ToString()) * 60) / 100;
            }

            if (inqDateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                // if the input date is a sunday, set the actual SLA start date to the following monday morning 7:00AM
                inqDateTime = inqDateTime.AddHours(24);
                inqDateTime = new DateTime(inqDateTime.Year, inqDateTime.Month, inqDateTime.Day, 7, 0, 0);
            }
            else if (inqDateTime.DayOfWeek == DayOfWeek.Saturday)
            {
                // if the input date is a saturday, set the actual SLA start date to the following monday morning 7:00AM
                inqDateTime = inqDate.AddHours(48);
                inqDateTime = new DateTime(inqDateTime.Year, inqDateTime.Month, inqDateTime.Day, 7, 0, 0);
            }

            dtReminder2 = inqDateTime;
            for (int i = 0; i < intReminder1Minites; i++)
            {
                dtReminder2 = dtReminder2.AddMinutes(1);

                // it is 5PM and time to go home
                if (dtReminder2.Hour >= 17)
                {
                    // if tomorrow is saturday
                    if (dtReminder2.AddDays(1).DayOfWeek == DayOfWeek.Saturday)
                    {
                        //add 48 hours to get us through the whole weekend
                        dtReminder2 = dtReminder2.AddHours(48);
                    }

                    // add 14 hours to get us to next morning
                    dtReminder2 = dtReminder2.AddHours(14);
                }
            }
            return dtReminder2;

        }

        private DateTime GetEscalation1(string strInqNo)
        {

            DateTime dtEscalation1 = new DateTime();

            DataTable dtInqStatus = new DatabaseService().executeSelectQuery("select * from InquiryStatus  WHERE InquiryNumber= '" + strInqNo + "'");
            DataTable dtInqDetails = new DatabaseService().executeSelectQuery("select * from Inquiry  WHERE InquiryNumber= '" + strInqNo + "'");


            DateTime inqDate = Convert.ToDateTime(dtInqDetails.Rows[0]["JobEntryDate"].ToString());
            DateTime inqTime = Convert.ToDateTime(dtInqDetails.Rows[0]["JobEntryTIme"].ToString());

            DateTime inqDateTime = inqDate.Date.Add(inqTime.TimeOfDay);

            DateTime DueDateTime = DoCalculation(strInqNo, inqDateTime);


            DataTable dtCusDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Customer WHERE Id = '" + dtInqDetails.Rows[0]["CustomerId"].ToString().Trim() + "' ");

            DataTable dtDueDate = new DataTable();
            DataTable dtInqTypeDetails = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryType WHERE InquiryType = '" + dtInqDetails.Rows[0]["ProblemNature"].ToString().Trim() + "' AND CusCategory = '" + dtCusDetails.Rows[0]["CustomerCategory"].ToString().Trim() + "'");

            int intEscalation1Minites = 0;

            if (dtInqTypeDetails.Rows.Count > 0)
            {
                intEscalation1Minites = (Convert.ToInt32(dtInqTypeDetails.Rows[0]["Escalation1"].ToString())) * 60;
            }


            dtEscalation1 = DueDateTime;
            dtEscalation1 = dtEscalation1.AddMinutes(intEscalation1Minites);

            return dtEscalation1;

        }

        private DateTime GetEscalation2(string strInqNo)
        {

            DateTime dtEscalation2 = new DateTime();

            DataTable dtInqStatus = new DatabaseService().executeSelectQuery("select * from InquiryStatus  WHERE InquiryNumber= '" + strInqNo + "'");
            DataTable dtInqDetails = new DatabaseService().executeSelectQuery("select * from Inquiry  WHERE InquiryNumber= '" + strInqNo + "'");


            DateTime inqDate = Convert.ToDateTime(dtInqDetails.Rows[0]["JobEntryDate"].ToString());
            DateTime inqTime = Convert.ToDateTime(dtInqDetails.Rows[0]["JobEntryTIme"].ToString());

            DateTime inqDateTime = inqDate.Date.Add(inqTime.TimeOfDay);

            DateTime DueDateTime = DoCalculation(strInqNo, inqDateTime);

            DataTable dtCusDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Customer WHERE Id = '" + dtInqDetails.Rows[0]["CustomerId"].ToString().Trim() + "' ");

            DataTable dtDueDate = new DataTable();
            DataTable dtInqTypeDetails = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryType WHERE InquiryType = '" + dtInqDetails.Rows[0]["ProblemNature"].ToString().Trim() + "' AND CusCategory = '" + dtCusDetails.Rows[0]["CustomerCategory"].ToString().Trim() + "'");

            int intEscalation2Minites = 0;

            if (dtInqTypeDetails.Rows.Count > 0)
            {
                intEscalation2Minites = (Convert.ToInt32(dtInqTypeDetails.Rows[0]["Escalation2"].ToString())) * 60;
            }

            dtEscalation2 = DueDateTime;
            dtEscalation2 = dtEscalation2.AddMinutes(intEscalation2Minites);

            return dtEscalation2;

        }

        private DateTime calculationEscOne(string strInqNo)
        {

            DateTime dtFirstEsc = new DateTime();

            DataTable dtInqStatus = new DatabaseService().executeSelectQuery("select * from InquiryStatus  WHERE InquiryNumber= '" + strInqNo + "'");
            DataTable dtInqDetails = new DatabaseService().executeSelectQuery("select * from Inquiry  WHERE InquiryNumber= '" + strInqNo + "'");



            DateTime inqDate = Convert.ToDateTime(dtInqStatus.Rows[0]["Date"].ToString().Trim());
            DateTime inqTime = Convert.ToDateTime(dtInqStatus.Rows[0]["Time"].ToString().Trim());

            DateTime inqDateTime = inqDate.Date.Add(inqTime.TimeOfDay);

            DateTime dueDate = Convert.ToDateTime(dtInqStatus.Rows[0]["dueDate"].ToString().Trim());
            DateTime dueTime = Convert.ToDateTime(dtInqStatus.Rows[0]["dueTime"].ToString().Trim());

            DateTime dueDateTime = inqDate.Date.Add(inqTime.TimeOfDay);

            // DataTable dtSerAgreements = new DatabaseService().executeSelectQuery("SELECT * FROM SeviceAgreement WHERE Customer = '" + dtInqDetails.Rows[0]["CustomerId"].ToString().Trim() + "' AND Location = '" + dtInqDetails.Rows[0]["Location"].ToString().Trim() + "' AND GeneratorId = '" + dtInqDetails.Rows[0]["HowKnow"].ToString().Trim() + "'");

            DataTable dtDueDate = new DataTable();

            DataTable dtCompHours = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryType WHERE InquiryType = '" + dtInqDetails.Rows[0]["ProblemNature"].ToString().Trim() + "'");

            int intCompHours = 0;

            //if (dtSerAgreements.Rows.Count.ToString() == "0")
            //{
            //    intCompHours = Convert.ToInt32(dtCompHours.Rows[0]["CompletionHoursNonAgree"].ToString().Trim());
            //}
            //else
            //{
            intCompHours = Convert.ToInt32(dtCompHours.Rows[0]["CompletionHours"].ToString().Trim());

            //}

            int intFirstEsc = ((intCompHours * 25) / 100) * 60;

            dtFirstEsc = inqDateTime.AddMinutes(intFirstEsc);

            return dtFirstEsc;

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

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string InqNumber = gridView1.GetFocusedDataRow()["InquiryNumber"].ToString().Trim();

            if (gridView1.RowCount.ToString() == "0")
            {
                XtraMessageBox.Show("No Pending Inquiries.", "Information");
            }
            else
            {


                if (string.IsNullOrEmpty(txtRemarks.Text) || string.IsNullOrEmpty(cboUsers.Text))
                {
                    XtraMessageBox.Show("Cannot leave the required fields empty", "Error");
                }
                else
                {

                    DataTable dtuser = new DatabaseService().executeSelectQuery("select Id,Category from Users  WHERE Username='" + InfoPCMS.Views.frmHome.UsrName + "'");

                    DataTable dtEmp = new DatabaseService().executeSelectQuery("select Id,Category,EmployeeName from Employee  WHERE EmployeeName='" + InfoPCMS.Views.frmHome.EmpName + "'");

                    DataTable dtInqDetails = new DatabaseService().executeSelectQuery("select * from Inquiry  WHERE InquiryNumber='" + InqNumber + "' ");



                    if (dtuser.Rows.Count > 0)
                    {
                        // DataTable dtuser2 = new DatabaseService().executeSelectQuery("select Id,Category from Users  WHERE Username='" + cboUsers.Text + "'");

                        DataTable dtEmp2 = new DatabaseService().executeSelectQuery("select Id,Category,EmployeeName from Employee  WHERE EmployeeName='" + cboUsers.Text + "'");

                        if (dtEmp.Rows[0]["Category"].ToString().Trim() == "LEVEL 3")
                        {

                            if (dtEmp2.Rows.Count > 0)
                            {
                                int intNoOfForward = 0;

                                DataTable dtNoOfForward = new DatabaseService().executeSelectQuery("SELECT NoOfForwards FROM InquiryStatus WHERE InquiryNumber = '" + InqNumber + "'");

                                if (dtNoOfForward.Rows.Count > 0)
                                {
                                    intNoOfForward = Convert.ToInt32(dtNoOfForward.Rows[0]["NoOfForwards"].ToString().Trim());

                                    intNoOfForward = intNoOfForward + 1;

                                    // new DatabaseService().executeUpdateQuery("UPDATE InquiryStatus SET ForwardUser='" + cboUsers.Text.ToString() + "',Remarks='" + txtRemarks.Text.ToString() + "',oldUser='" + gridView1.GetFocusedDataRow()["SiteResponsible"].ToString() + "',NoOfForwards = '" + intNoOfForward.ToString() + "'  WHERE InquiryNumber='" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "'");

                                    new DatabaseService().executeUpdateQuery("UPDATE InquiryStatus SET ForwardUser='" + GetEmpId(cboUsers.Text.ToString()) + "',Remarks='" + txtRemarks.Text.ToString() + "',oldUser='" + GetEmpId(gridView1.GetFocusedDataRow()["SiteResponsible"].ToString()) + "',NoOfForwards = '" + intNoOfForward.ToString() + "'  WHERE InquiryNumber='" + InqNumber + "'");



                                }
                                else
                                {
                                    intNoOfForward = intNoOfForward + 1;

                                    //new DatabaseService().executeUpdateQuery("INSERT INTO InquiryStatus VALUES('" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "','" + gridView1.GetFocusedDataRow()["Date"].ToString() + "','" + gridView1.GetFocusedDataRow()["CustomerName"].ToString() + "','" + gridView1.GetFocusedDataRow()["inquirytaken"].ToString() + "','" + gridView1.GetFocusedDataRow()["inspectiondone"].ToString() + "','" + gridView1.GetFocusedDataRow()["siteresponsible"].ToString() + "','" + cboUsers.Text + "','" + txtRemarks.Text + "','Forward','" + gridView1.GetFocusedDataRow()["siteresponsible"].ToString() + "','0','','','','','" + intNoOfForward.ToString() + "','','','','')");

                                    new DatabaseService().executeUpdateQuery("INSERT INTO InquiryStatus VALUES('" + InqNumber + "','" + gridView1.GetFocusedDataRow()["Date"].ToString() + "','" + gridView1.GetFocusedDataRow()["Time"].ToString() + "','" + GetCusId(gridView1.GetFocusedDataRow()["CustomerName"].ToString()) + "','" + GetEmpId(gridView1.GetFocusedDataRow()["inquirytaken"].ToString()) + "','" + gridView1.GetFocusedDataRow()["inspectiondone"].ToString() + "','" + dtInqDetails.Rows[0]["jobAssignedTo"].ToString().Trim() + "','" + GetEmpId(gridView1.GetFocusedDataRow()["siteresponsible"].ToString()) + "','" + GetEmpId(cboUsers.Text) + "','" + txtRemarks.Text + "','Forward','" + GetEmpId(gridView1.GetFocusedDataRow()["siteresponsible"].ToString()) + "','0','','','','','','" + intNoOfForward.ToString() + "','','','','','')");
                                }
                                // new DatabaseService().executeUpdateQuery("UPDATE Inquiry SET SiteResponsible='" + dtEmp2.Rows[0]["EmployeeName"].ToString() + "' WHERE InquiryNumber='" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "'");

                                new DatabaseService().executeUpdateQuery("UPDATE Inquiry SET SiteResponsible='" + GetEmpId(dtEmp2.Rows[0]["EmployeeName"].ToString()) + "' WHERE InquiryNumber='" + InqNumber + "'");

                                XtraMessageBox.Show("Inquiry Forwarded Successfully", "Information");
                                // lblType.Text = "d";


                                if (!InfoPCMS.Views.frmHome.isOffline)
                                {
                                    SendEmailForward(InqNumber);
                                }

                                TableLoad();


                            }
                        }
                        else
                        {
                            if (this.CheckCanForward(InqNumber))
                            {
                                int intNoOfForward = 0;

                                DataTable dtNoOfForward = new DatabaseService().executeSelectQuery("SELECT NoOfForwards FROM InquiryStatus WHERE InquiryNumber = '" + InqNumber + "'");

                                if (dtNoOfForward.Rows.Count > 0)
                                {
                                    intNoOfForward = Convert.ToInt32(dtNoOfForward.Rows[0]["NoOfForwards"].ToString().Trim());

                                    intNoOfForward = intNoOfForward + 1;
                                }
                                else
                                {
                                    intNoOfForward = intNoOfForward + 1;
                                }

                                if (dtEmp2.Rows.Count > 0)
                                {
                                    // new DatabaseService().executeUpdateQuery("INSERT INTO InquiryStatus VALUES('" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "','" + gridView1.GetFocusedDataRow()["Date"].ToString() + "','" + gridView1.GetFocusedDataRow()["CustomerName"].ToString() + "','" + gridView1.GetFocusedDataRow()["inquirytaken"].ToString() + "','" + gridView1.GetFocusedDataRow()["inspectiondone"].ToString() + "','" + gridView1.GetFocusedDataRow()["siteresponsible"].ToString() + "','" + cboUsers.Text + "','" + txtRemarks.Text + "','Forward','" + dtEmp.Rows[0]["EmployeeName"].ToString() + "','0','','','','','" + intNoOfForward.ToString() + "','','','','')");
                                    DataRow drow = gridView1.GetFocusedDataRow();
                                    new DatabaseService().executeUpdateQuery("INSERT INTO InquiryStatus VALUES('" + InqNumber + "','" + gridView1.GetFocusedDataRow()["Date"].ToString() + "','" + gridView1.GetFocusedDataRow()["Time"].ToString() + "','" + GetCusId(gridView1.GetFocusedDataRow()["CustomerName"].ToString()) + "','" + GetEmpId(gridView1.GetFocusedDataRow()["inquirytaken"].ToString()) + "','" + gridView1.GetFocusedDataRow()["inspectiondone"].ToString() + "','" + dtInqDetails.Rows[0]["JobAssignedTo"].ToString() + "','" + GetEmpId(gridView1.GetFocusedDataRow()["siteresponsible"].ToString()) + "','" + GetEmpId(cboUsers.Text) + "','" + txtRemarks.Text + "','Forward','" + GetEmpId(dtEmp.Rows[0]["EmployeeName"].ToString()) + "','0','','','','','','" + intNoOfForward.ToString() + "','','','','','')");


                                    // new DatabaseService().executeUpdateQuery("UPDATE Inquiry SET SiteResponsible='" + dtEmp2.Rows[0]["EmployeeName"].ToString() + "' WHERE InquiryNumber='" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "'");

                                    new DatabaseService().executeUpdateQuery("UPDATE Inquiry SET SiteResponsible='" + GetEmpId(dtEmp2.Rows[0]["EmployeeName"].ToString()) + "' WHERE InquiryNumber='" + InqNumber + "'");

                                    XtraMessageBox.Show("Inquiry Forwarded Successfully", "Information");
                                    //lblType.Text = "d";

                                    if (!InfoPCMS.Views.frmHome.isOffline)
                                    {
                                        SendEmailForward(InqNumber);
                                    }

                                    TableLoad();




                                }

                            }
                            else
                            {
                                DataTable dtNoOfForward = new DatabaseService().executeSelectQuery("SELECT NoOfForwards FROM InquiryStatus WHERE InquiryNumber = '" + InqNumber + "'");

                                int intNoOfForward = 0;

                                if (dtNoOfForward.Rows.Count > 0)
                                {
                                    intNoOfForward = Convert.ToInt32(dtNoOfForward.Rows[0]["NoOfForwards"].ToString().Trim());

                                    intNoOfForward = intNoOfForward + 1;
                                }
                                else
                                {
                                    intNoOfForward = intNoOfForward + 1;
                                }


                                new DatabaseService().executeUpdateQuery("INSERT INTO InquiryStatusTemp VALUES('" + InqNumber + "','" + gridView1.GetFocusedDataRow()["Date"].ToString() + "','" + gridView1.GetFocusedDataRow()["Time"].ToString() + "','" + GetCusId(gridView1.GetFocusedDataRow()["CustomerName"].ToString()) + "','" + GetEmpId(gridView1.GetFocusedDataRow()["inquirytaken"].ToString()) + "','" + gridView1.GetFocusedDataRow()["inspectiondone"].ToString() + "','" + dtInqDetails.Rows[0]["JobAssignedTo"].ToString() + "','" + GetEmpId(gridView1.GetFocusedDataRow()["siteresponsible"].ToString()) + "','" + GetEmpId(cboUsers.Text) + "','" + txtRemarks.Text + "','Forward','" + GetEmpId(dtEmp.Rows[0]["EmployeeName"].ToString()) + "','0','','','','','','" + intNoOfForward.ToString() + "','','','','')");

                                if (intNoOfForward > 2)
                                {
                                    new DatabaseService().executeUpdateQuery("UPDATE Inquiry SET RequestForward ='Yes',ApprovedForward = NULL ,ApprovedForwardBy = NULL ,ApprovedForwardDate=NULL WHERE InquiryNumber='" + InqNumber + "'");
                                }
                                else
                                {


                                    new DatabaseService().executeUpdateQuery("UPDATE Inquiry SET RequestForward ='Yes' WHERE InquiryNumber='" + InqNumber + "'");

                                }
                                // new DatabaseService().executeUpdateQuery("UPDATE InquiryStatus SET ForwardUser='" + cboUsers.Text.ToString() + "',Remarks='" + txtRemarks.Text.ToString() + "',oldUser='" + dtEmp.Rows[0]["EmployeeName"].ToString() + "'  WHERE InquiryNumber='" + gridView1.GetFocusedDataRow()["InquiryNumber"].ToString() + "'");

                                XtraMessageBox.Show("This is a forwarded Inquiry. Send for Approval to Supervisor", "Information");

                                if (!InfoPCMS.Views.frmHome.isOffline)
                                {
                                    SendEmailMoreForwards(intNoOfForward, InqNumber);
                                }





                            }

                        }
                    }
                    //gridView1.GetFocusedDataRow()["InquiryNumber"].ToString();
                }
            }
        }

        private void SendEmailForward(string inqNo)
        {
            string strFromEmail = "";
            string strToEmail = "";
            List<string> strToEmails = new List<string>();
            List<string> strCCEmails = new List<string>();


            DataTable dtInquriyDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Inquiry WHERE InquiryNumber = '" + inqNo + "'");

            DataTable dtCusDetais = new DatabaseService().executeSelectQuery("SELECT * FROM Customer WHERE Id = '" + dtInquriyDetails.Rows[0]["CustomerId"].ToString().Trim() + "'");


            strFromEmail = "";//GetEmail(gridView1.GetFocusedDataRow()["SiteResponsible"].ToString().Trim());
            strToEmail = GetEmail(cboUsers.Text.Trim());
            StringBuilder strBuliderEmailSubject = new StringBuilder();
            strBuliderEmailSubject.Append("Inquiry Forwarded – Customer :" + dtCusDetais.Rows[0]["CustomerName"].ToString().Trim() + "  " + " -Inquiry No :" + inqNo + " ");


            StringBuilder strBuliderEmailBody = new StringBuilder();
            strBuliderEmailBody.Append("<head>");
            strBuliderEmailBody.Append("<title>");
            strBuliderEmailBody.Append(Guid.NewGuid().ToString());
            strBuliderEmailBody.Append("</title>");
            strBuliderEmailBody.Append("</head>");
            strBuliderEmailBody.Append("<body>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Dear " + cboUsers.Text.Trim() + ",");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Inquiry <b><u><FONT COLOR=DodgerBlue>Forwarded</FONT></u></b>, pending for your Acceptance,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Inquiry No : </b>" + inqNo + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Type : </b>" + dtInquriyDetails.Rows[0]["ProblemNature"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Customer : </b>" + dtCusDetais.Rows[0]["CustomerName"].ToString() + "");
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
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Forwarded By : </b>" + gridView1.GetFocusedDataRow()["SiteResponsible"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Remarks : </b>" + txtRemarks.Text.Trim() + "");

            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Job Entry Date & Time : </b>" + Convert.ToDateTime(dtInquriyDetails.Rows[0]["JobEntryDate"].ToString()).ToString("mm/dd/yyyy") + "&nbsp;&nbsp;" + dtInquriyDetails.Rows[0]["JobEntryTime"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Regards,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("" + InfoPCMS.Views.frmHome.EmpName + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");

            strBuliderEmailBody.Append("<FONT COLOR=darkred>Auto generated email from CRM system</FONT>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("</body>");
            strBuliderEmailBody.Append("</html>");

            //EmailServices Email = new EmailServices();
            //Email.SendEmails(strFromEmail.Trim(), strToEmail.Trim(), strBuliderEmailSubject.ToString(), strBuliderEmailBody.ToString());

            Email_Manager em = new Email_Manager();
            Boolean result = em.SendEmail(strToEmail.Trim(), strBuliderEmailSubject.ToString(), strBuliderEmailBody.ToString());

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

        public string GetEmail(string strEmpName)
        {
            string strEmail = "";

            DataTable dt;
            dt = new DatabaseService().executeSelectQuery("SELECT Email FROM Employee WHERE EmployeeName = '" + strEmpName + "'");
            if (dt.Rows.Count > 0)
            {

                strEmail = dt.Rows[0]["Email"].ToString();
            }

            return strEmail;

        }

        public string GetEmailL3()
        {
            string strEmail = "";

            DataTable dt;
            dt = new DatabaseService().executeSelectQuery("SELECT Email FROM Employee WHERE Category = 'LEVEL 3'");
            if (dt.Rows.Count > 0)
            {

                strEmail = dt.Rows[0]["Email"].ToString();
            }

            return strEmail;

        }

        public DateTime getDueDate(string inqNo, DateTime inqDate)
        {
            DateTime dueDate = DateTime.Now;

            DataTable dtInqDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Inquiry WHERE InquiryNumber = '" + inqNo + "'");



            //  DataTable dtSerAgreements = new DatabaseService().executeSelectQuery("SELECT * FROM SeviceAgreement WHERE Customer = '" + dtInqDetails.Rows[0]["CustomerId"].ToString().Trim() + "' AND Location = '" + dtInqDetails.Rows[0]["Location"].ToString().Trim() + "' AND GeneratorId = '" + dtInqDetails.Rows[0]["HowKnow"].ToString().Trim() + "'");

            DataTable dtDueDate = new DataTable();

            //if (dtSerAgreements.Rows.Count.ToString() == "0")
            //{
            //    dtDueDate = new DatabaseService().executeSelectQuery("SELECT DATEADD(hour,(SELECT CompletionHoursNonAgree FROM InquiryType WHERE InquiryType = (SELECT ProblemNature FROM dbo.Inquiry WHERE InquiryNumber = '" + inqNo + "')),'" + inqDate + "') AS DueDate FROM dbo.Inquiry WHERE InquiryNumber = '" + inqNo + "'");

            //}
            //else
            //{
            dtDueDate = new DatabaseService().executeSelectQuery("SELECT DATEADD(hour,(SELECT CompletionHours FROM InquiryType WHERE InquiryType = (SELECT ProblemNature FROM dbo.Inquiry WHERE InquiryNumber = '" + inqNo + "')),'" + inqDate + "') AS DueDate FROM dbo.Inquiry WHERE InquiryNumber = '" + inqNo + "'");


            //}


            if (dtDueDate.Rows.Count > 0)
            {
                dueDate = Convert.ToDateTime(dtDueDate.Rows[0]["DueDate"].ToString().Trim());


            }


            return dueDate;

        }


        public static DateTime GetDueDateTime(string inqNo, DateTime inqDate)
        {

            DataTable dtInqDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Inquiry WHERE InquiryNumber = '" + inqNo + "'");
            // DataTable dtSerAgreements = new DatabaseService().executeSelectQuery("SELECT * FROM SeviceAgreement WHERE Customer = '" + dtInqDetails.Rows[0]["CustomerId"].ToString().Trim() + "' AND Location = '" + dtInqDetails.Rows[0]["Location"].ToString().Trim() + "' AND GeneratorId = '" + dtInqDetails.Rows[0]["HowKnow"].ToString().Trim() + "'");
            DataTable dtDueDate = new DataTable();
            DataTable dtInqTypeDetails = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryType WHERE InquiryType = '" + dtInqDetails.Rows[0]["ProblemNature"].ToString().Trim() + "'");

            int intCompletionHours = 0;

            if (dtInqTypeDetails.Rows.Count > 0)
            {
                //if (dtSerAgreements.Rows.Count.ToString() == "0")
                //{
                //    intCompletionHours = Convert.ToInt32(dtInqTypeDetails.Rows[0]["CompletionHoursNonAgree"].ToString()) * 60;
                //}
                //else
                //{
                intCompletionHours = Convert.ToInt32(dtInqTypeDetails.Rows[0]["CompletionHours"].ToString()) * 60;
                //}
            }

            double days = (double)intCompletionHours / 540;
            DateTime dtInqDate = inqDate;
            DateTime dueDate = dtInqDate;
            while (days >= 1)
            {
                dueDate = dueDate.AddDays(1);
                if (dueDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    dueDate = dueDate.AddDays(2);
                }
                days--;
            }

            days = days * 540;

            dueDate = dueDate.AddMinutes(days);
            if (dueDate.Hour > 17)
            {
                dueDate = dueDate.AddHours(15);
            }
            if (dueDate.DayOfWeek == DayOfWeek.Saturday)
            {
                dueDate = dueDate.AddDays(2);
            }
            else if (dueDate.DayOfWeek == DayOfWeek.Sunday)
            {
                dueDate = dueDate.AddDays(1);
            }
            return dueDate;
        }



        private static DateTime DoCalculation(string inqNo, DateTime inqDate)
        {
            DataTable dtInqDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Inquiry WHERE InquiryNumber = '" + inqNo + "'");
            //DataTable dtSerAgreements = new DatabaseService().executeSelectQuery("SELECT * FROM SeviceAgreement WHERE Customer = '" + dtInqDetails.Rows[0]["CustomerId"].ToString().Trim() + "' AND Location = '" + dtInqDetails.Rows[0]["Location"].ToString().Trim() + "' AND GeneratorId = '" + dtInqDetails.Rows[0]["HowKnow"].ToString().Trim() + "'");
            DataTable dtCusDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Customer WHERE Id = '" + dtInqDetails.Rows[0]["CustomerId"].ToString().Trim() + "' ");

            DataTable dtDueDate = new DataTable();
            DataTable dtInqTypeDetails = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryType WHERE InquiryType = '" + dtInqDetails.Rows[0]["ProblemNature"].ToString().Trim() + "' AND CusCategory = '" + dtCusDetails.Rows[0]["CustomerCategory"].ToString().Trim() + "'");

            int intCompletionMinites = 0;

            if (dtInqTypeDetails.Rows.Count > 0)
            {
                intCompletionMinites = Convert.ToInt32(dtInqTypeDetails.Rows[0]["CompletionHours"].ToString()) * 60;
            }


            if (inqDate.DayOfWeek == DayOfWeek.Sunday)
            {
                // if the input date is a sunday, set the actual SLA start date to the following monday morning 7:00AM
                inqDate = inqDate.AddHours(24);
                inqDate = new DateTime(inqDate.Year, inqDate.Month, inqDate.Day, 7, 0, 0);
            }
            else if (inqDate.DayOfWeek == DayOfWeek.Saturday)
            {
                // if the input date is a saturday, set the actual SLA start date to the following monday morning 7:00AM
                inqDate = inqDate.AddHours(48);
                inqDate = new DateTime(inqDate.Year, inqDate.Month, inqDate.Day, 7, 0, 0);
            }

            DateTime resultDate = inqDate;
            for (int i = 0; i < intCompletionMinites; i++)
            {
                resultDate = resultDate.AddMinutes(1);

                // it is 5PM and time to go home
                if (resultDate.Hour >= 17)
                {
                    // if tomorrow is saturday
                    if (resultDate.AddDays(1).DayOfWeek == DayOfWeek.Saturday)
                    {
                        //add 48 hours to get us through the whole weekend
                        resultDate = resultDate.AddHours(48);
                    }

                    // add 14 hours to get us to next morning
                    resultDate = resultDate.AddHours(14);
                }
            }
            return resultDate;
        }


        public void SendEmailMoreForwards(int intNoofForwarded, string InqNO)
        {

            string strFromEmail = "";
            string strToEmail = "";
            List<string> strToEmails = new List<string>();
            List<string> strCCEmails = new List<string>();


            DataTable dtInquriyDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Inquiry WHERE InquiryNumber = '" + InqNO + "'");

            DataTable dtInquriyStatusDetails = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryStatus WHERE InquiryNumber = '" + InqNO + "'");


            DataTable dtCusDetais = new DatabaseService().executeSelectQuery("SELECT * FROM Customer WHERE Id = '" + dtInquriyDetails.Rows[0]["CustomerId"].ToString().Trim() + "'");


            strFromEmail = GetEmail(InfoPCMS.Views.frmHome.EmpName);
            strToEmail = GetEmailL3();
            StringBuilder strBuliderEmailSubject = new StringBuilder();
            strBuliderEmailSubject.Append("Request for Multiple Forward - Customer : " + dtCusDetais.Rows[0]["CustomerName"].ToString().Trim() + " " + "  - Inquiry No: " + InqNO + " ");


            StringBuilder strBuliderEmailBody = new StringBuilder();
            strBuliderEmailBody.Append("<head>");
            strBuliderEmailBody.Append("<title>");
            strBuliderEmailBody.Append(Guid.NewGuid().ToString());
            strBuliderEmailBody.Append("</title>");
            strBuliderEmailBody.Append("</head>");
            strBuliderEmailBody.Append("<body>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Dear Sir/Madam ,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Inquiry want <b><u><FONT COLOR=DodgerBlue>Multiple Forwards</FONT></u></b>, pending for your Approval,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Inquiry No : </b>" + InqNO + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Type : </b>" + dtInquriyDetails.Rows[0]["ProblemNature"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Customer : </b>" + dtCusDetais.Rows[0]["CustomerName"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Location : </b>" + dtInquriyDetails.Rows[0]["Location"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Caller Name : </b>" + dtInquriyDetails.Rows[0]["CallerName"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Caller Phone : </b>" + dtInquriyDetails.Rows[0]["CallerPhone"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Inquiry Details : </b>" + dtInquriyDetails.Rows[0]["FollowupDetails"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Priority : </b>" + dtInquriyDetails.Rows[0]["Priority"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Forwarded By : </b>" + InfoPCMS.Views.frmHome.EmpName + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Forwarded To : </b>" + cboUsers.Text.ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>No of Forwarded : </b>" + intNoofForwarded.ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Remarks : </b>" + txtRemarks.Text.Trim() + "");

            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Job Entry Date & Time : </b>" + dtInquriyDetails.Rows[0]["JobEntryDate"].ToString() + "&nbsp;&nbsp;" + dtInquriyDetails.Rows[0]["JobEntryTime"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Regards,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("" + InfoPCMS.Views.frmHome.EmpName + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");

            strBuliderEmailBody.Append("<FONT COLOR=darkred>Auto generated email from CRM system</FONT>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("</body>");
            strBuliderEmailBody.Append("</html>");


            EmailServices Email = new EmailServices();

            Email.SendEmails(strFromEmail.Trim(), strToEmail.Trim(), strBuliderEmailSubject.ToString(), strBuliderEmailBody.ToString());

            //  XtraMessageBox.Show("Notification Send for Approval to Supervisor","Information");
        }

        public bool CheckCanForward(string strInquiryNo)
        {
            bool canForward = false;

            DataTable dtCheckNoOfFwrds = new DatabaseService().executeSelectQuery("select NoOfForwards from InquiryStatus  WHERE InquiryNumber='" + strInquiryNo.ToString().Trim() + "'");

            // DataTable dtCheckApprovedForward = new DatabaseService().executeSelectQuery("select ApprovedForward from Inquiry  WHERE InquiryNumber='" + strInquiryNo.ToString().Trim() + "'");


            if (dtCheckNoOfFwrds.Rows.Count <= 0)
            {
                canForward = true;
            }
            else

                if (String.IsNullOrEmpty(dtCheckNoOfFwrds.Rows[0]["NoOfForwards"].ToString()))
                {
                    canForward = true;

                }
                else if (Convert.ToInt32(dtCheckNoOfFwrds.Rows[0]["NoOfForwards"].ToString()) < 1)
                {

                    canForward = true;
                }
            //else if (dtCheckApprovedForward.Rows[0]["ApprovedForward"].ToString().Trim() == "Yes")
            //{
            //    canForward = true;

            //}

            return canForward;

        }

        private void forwardInquiry()
        {

        }

        private void gridaccepted_DoubleClick(object sender, EventArgs e)
        {

        }

      

        private void SetSubActionTypes()
        {
            //try
            //{

            //    DataTable dt2 = new DatabaseService().executeSelectQuery("select * from ActionType");

            //    cboActionType.DataSource = dt2;
            //    cboActionType.ValueMember = "ActionType";
            //    cboActionType.DisplayMember = "ActionType";


            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}


            DataTable dtModifiedSubAcctions = new DatabaseService().executeSelectQuery("select SubActionType from InquiryActionTypeCheckList WHERE InquiryNo='" + lblInquiryNumber.Text.Trim() + "' AND ActionTypeID='" + this.getActionTypeID(lblInquiryType.Text.Trim()) + "' ");

            if (dtModifiedSubAcctions.Rows.Count > 0)
            {

                cmbxSubActionTypes.DataSource = dtModifiedSubAcctions;
                cmbxSubActionTypes.DisplayMember = "SubActionType";
                cmbxSubActionTypes.ValueMember = "SubActionType";

                btnRemoveSubAction.Enabled = false;
            }
            else
            {
                DataTable dtSubActions = new DatabaseService().executeSelectQuery("SELECT SubActionType,SeqNo FROM ActionTypeCheckList WHERE ActionType='" + lblInquiryType.Text.Trim() + "' ORDER BY SeqNo ");

                cmbxSubActionTypes.DataSource = dtSubActions;
                cmbxSubActionTypes.DisplayMember = "SubActionType";
                cmbxSubActionTypes.ValueMember = "SubActionType";
                if (dtSubActions.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSubActions.Rows.Count; i++)
                    {

                        new DatabaseService().executeUpdateQuery("INSERT INTO InquiryActionTypeCheckList VALUES('" + lblInquiryNumber.Text.ToString() + "','" + this.getActionTypeID(lblInquiryType.Text.Trim()) + "','" + lblInquiryType.Text.Trim() + "','" + dtSubActions.Rows[i]["SubActionType"].ToString() + "')");

                    }

                }
            }

        }

        private String GetInquiryType(string strInqNo)
        {
            string strInqType = "";

            DataTable dtInqDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Inquiry WHERE InquiryNumber = '" + strInqNo + "'");

            if (dtInqDetails.Rows.Count > 0)
            {
                strInqType = dtInqDetails.Rows[0]["ProblemNature"].ToString().Trim();
            }

            return strInqType;
        }

        private String GetInquiryActProb(string strInqNo)
        {
            string strInqActProb = "";

            DataTable dtInqDetails = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryStatus WHERE InquiryNumber = '" + strInqNo + "'");

            if (dtInqDetails.Rows.Count > 0)
            {
                strInqActProb = dtInqDetails.Rows[0]["actual"].ToString().Trim();
            }

            return strInqActProb;
        }

        //private void SetNextActionTypes()
        //{
        //    try
        //    {

        //        DataTable dt2 = new DatabaseService().executeSelectQuery("select * from ActionType");

        //        cmbxNextAction.DataSource = dt2;
        //        cmbxNextAction.ValueMember = "ActionType";
        //        cmbxNextAction.DisplayMember = "ActionType";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        private void simpleButton3_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(txt_hourmeter.Text.Trim()))
            {
                // MessageBox.Show("Please enter value to hour Meter",Globals.MessageCaption,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else {
                txt_hourmeter.Text = "0";
            }
          

            Boolean isSerInclude = false;

            if (lblInquiryType.Text.ToString() == "Repairs")
            {
                if (rdbtnSerInculdeYes.Checked)
                {
                    isSerInclude = true;
                }
                else
                {
                    isSerInclude = false;
                }
            }
            else
            {

            }

            Boolean isComplete = false;
            if (cmbxSubActionTypes.Text.Trim().Equals("Complete"))
            {
                isComplete = true;
            }
            new DatabaseService().executeUpdateQuery("INSERT INTO InquiryStatusDetails VALUES('" + lblInquiryType.Text.Trim() + "','" + cmbxSubActionTypes.Text.ToString() + "','" + txtactionDetails.Text + "','" + dateactiondate.Text + "','','" + isComplete + "','" + lblInquiryNumber.Text + "','" + txtCompPer.Text + "')");
            new DatabaseService().executeUpdateQuery("UPDATE InquiryStatus SET NextAction='" + txtNetAction.Text.ToString() + "',DateTime='" + (dateEdit2.Text + " " + timeEdit1.Text) + "' , IsServiceInclude = '" + isSerInclude.ToString().Trim() + "' WHERE Status='Accept' AND InquiryNumber='" + lblInquiryNumber.Text + "'");
            new DatabaseService().executeUpdateQuery("update Inquiry set HourM = " + txt_hourmeter.Text);
             

            if (isComplete)
            {
                new DatabaseService().executeUpdateQuery("UPDATE InquiryStatus SET Status='Completed',IsServiceInclude = '" + isSerInclude.ToString().Trim() + "' WHERE Status='Accept' AND InquiryNumber='" + lblInquiryNumber.Text + "'");
                TableLoad();
            }
            else
            {

            }

            SaveFileList(lblInquiryNumber.Text);


            // MessageBox.Show("Record Saved Successfully");
            XtraMessageBox.Show("Record Saved successfully", "Information");
            // Dtgridaccepted.Clear();
            TableLoad();
            // Dtgridaccepted = new DatabaseService().executeSelectQuery("SELECT (select   Sum(CAST(Percentage as numeric(9))) FROM InquiryStatusDetails where InquiryNumber=dbo.InquiryStatus.InquiryNumber) as percentage, InquiryStatus.InquiryNumber as  InquiryNumber,InquiryStatus.Dates as  date, InquiryStatus.Customer as  Customer, InquiryStatus.InquiryTaken as InquiryTaken, InquiryStatus.InspectionDone as InspectionDone, InquiryStatus.SiteResponsible as SiteResponsible, Inquiry.SiteResponsible  FROM InquiryStatus INNER JOIN Inquiry ON InquiryStatus.InquiryNumber = Inquiry.InquiryNumber where InquiryStatus.Status='Accept' ");
            //  gridaccepted.DataSource = Dtgridaccepted;
            DataTable Completed = new DatabaseService().executeSelectQuery("SELECT  InquiryNumber, ActionType,SubActionType,ActionDetails,ActionDate, Remarks, JobCompleted,Percentage FROM InquiryStatusDetails where InquiryNumber='" + lblInquiryNumber.Text + "'");
            tblupdate.DataSource = Completed;
            //lblInquiryNumber.Text = "";
            // cboActionType.SelectedIndex = 0;
            if (isComplete)
            {
                xtraTabControl1.SelectedTabPageIndex = 3;
                xtraTabPage1.PageEnabled = false;
                xtraTabPage2.PageEnabled = false;
                xtraTabPage4.PageEnabled = false;
                xtraTabPage5.PageEnabled = true;
                xtraTabPage7.PageEnabled = false;
                xtraTabPage13.PageEnabled = false;

                DataTable df = new DataTable();
                tblupdate.DataSource = df;
            }
            else
            {
                // xtraTabControl2.SelectedTabPageIndex = 0;
            }

            

            txtactionDetails.Text = "";
            dateactiondate.Text = "";
            // txtupdateremarks.Text = "";
            txtCompPer.Text = "0";
            txtNetAction.Text = "";
            dateEdit2.DateTime = System.DateTime.Now;
            timeEdit1.Time = System.DateTime.Now;
            // lblType.Text = "dd";


            //}
        }

       
        private bool CheckActionDateTimeMandatory(string strActionType)
        {
            bool blIsDateTimeMandatory = false;

            DataTable dtActionTyep = new DatabaseService().executeSelectQuery("SELECT  ActionType,IsDateTimeMandatory  FROM ActionType where ActionType='" + strActionType + "'");

            if (dtActionTyep.Rows.Count > 0)
            {
                if (dtActionTyep.Rows[0]["IsDateTimeMandatory"].ToString().Trim() == "True")
                {
                    blIsDateTimeMandatory = true;
                }

            }

            return blIsDateTimeMandatory;
        }

        private bool CheckActiontake(string InquiryName)
        {
            bool blActionDone = false;

            DataTable dtActionDone = new DatabaseService().executeSelectQuery("SELECT  InquiryNumber, ActionType, ActionDetails,ActionDate, Remarks, JobCompleted,Percentage FROM InquiryStatusDetails where InquiryNumber='" + lblInquiryNumber.Text + "'");

            if (dtActionDone.Rows.Count > 0)
            {
                blActionDone = true;

            }

            return blActionDone;


        }

        private void gridView4_DoubleClick(object sender, EventArgs e)
        {
            lblInquiryNumber.Text = gridView4.GetFocusedDataRow()["InquiryNumber"].ToString();
            xtraTabControl1.SelectedTabPageIndex = 1;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = true;
            xtraTabPage4.PageEnabled = false;
            xtraTabPage5.PageEnabled = false;
            xtraTabPage7.PageEnabled = false;
            xtraTabPage13.PageEnabled = false;


            xtraTabControl2.SelectedTabPageIndex = 1;
            xtraTabPage3.PageEnabled = false;
            xtraTabPage6.PageEnabled = true;
            tbPageChangeInqDetails.PageEnabled = false;
            //pnlRecRepairs.Enabled = false;
            // cboActionType.Focus();
            DataTable Completed = new DatabaseService().executeSelectQuery("SELECT  InquiryNumber, ActionType, ActionDetails,ActionDate, Remarks, JobCompleted FROM InquiryStatusDetails where InquiryNumber='" + lblInquiryNumber.Text + "'");
            tblupdate.DataSource = Completed;
        }
        String quotapproved;


        public void InquiryDetails(string InquiryNumber)
        {
            try
            {

                //DataTable dt = new DatabaseService().executeSelectQuery("select * from ServiceType");                
                //DataTable dt2 = new DatabaseService().executeSelectQuery("select * from Customer"); 
                //DataTable dt3 = new DatabaseService().executeSelectQuery("select * from Employee");
                //DataTable dt4 = new DatabaseService().executeSelectQuery("select * from Employee ");
                //DataTable dt5 = new DatabaseService().executeSelectQuery("select Id,Displayname from Users ");                            

                String inquiryno = InquiryNumber;

                InquiryService inquiry = new InquiryService();
                inquiry = inquiry.searchInquiry(inquiryno);
                quotapproved = inquiry.IsInquryAccepted(inquiryno);

                DataTable dt6 = new DatabaseService().executeSelectQuery("select * from InquiryStatus where InquiryNumber = '" + inquiryno + "' ");

                if (inquiry != null)
                {

                    txtInquiryNo.Text = inquiryno;
                    txtProblemNature.Text = inquiry.ProblemNature;


                    String inqdate = inquiry.InquiryDate;
                    DateTime inqdt = Convert.ToDateTime(inqdate);
                    txtDate.EditValue = inqdt;
                    txtTime.EditValue = inquiry.InquiryTime.ToString().Trim();

                    String jobEntrydate = inquiry.JobEntryDate;
                    DateTime dtjobEntrydate = Convert.ToDateTime(jobEntrydate);
                    txtJobEntryDate.EditValue = dtjobEntrydate;
                    txtJobEntryTime.EditValue = inquiry.JobEntryTime.ToString().Trim();
                    txtCustomer.Text = getCusName(inquiry.CustomerID);
                    txtLocation.Text = inquiry.Location;
                    txtGenNo.Text = inquiry.HowKnow;
                    txtProblemSource.Text = inquiry.ProblemSource.ToString().Trim();

                    if (dt6.Rows.Count > 0)
                    {
                        txtActualProbInqDetails.Text = dt6.Rows[0]["actual"].ToString().Trim();

                    }
                    else
                    {
                        txtActualProbInqDetails.Text = "";
                    }


                    txtInquiryTaken.Text = inquiry.GetEmpName(inquiry.InquiryTaken.ToString());
                    txtSiteResponsible.Text = inquiry.GetEmpName(inquiry.SiteResponsible);
                    txtCallerName.Text = inquiry.StrCallerName;
                    txtCallerPhone.Text = inquiry.StrCallerTelNo;

                    txtFollowupDetails.Text = inquiry.FollowupDetails;

                    cboprio.SelectedItem = inquiry.Priority;
                    txtExecutiveresponsible.Text = inquiry.ExecutiveResponsible;


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


                    xtraTabControl1.SelectedTabPage = xtraTabPage7;

                    xtraTabPage1.PageEnabled = false;
                    xtraTabPage2.PageEnabled = false;
                    xtraTabPage4.PageEnabled = false;
                    xtraTabPage5.PageEnabled = false;
                    xtraTabPage7.PageEnabled = true;
                    xtraTabPage13.PageEnabled = false;


                }
                else
                {



                }
            }
            catch (Exception ex)
            {

            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            if (gridView1.RowCount.ToString() == "0")
            {
                XtraMessageBox.Show("No Pending Inquiries.", "Information");
            }
            else
            {

                InquiryDetails(gridView1.GetFocusedDataRow()["InquiryNumber"].ToString());
                //selectInquiry();
            }
        }

        private void selectInquiry()
        {
            txtInquiryNo.Enabled = false;
            txtCustomer.Enabled = false;
            txtProblemNature.Enabled = false;
            txtLocation.Enabled = false;            //  txtInqType.Enabled = false;

            String inquiryno = gridView1.GetFocusedDataRow()["InquiryNumber"].ToString();

            InquiryService inquiry = new InquiryService();
            inquiry = inquiry.searchInquiry(inquiryno);
            quotapproved = inquiry.IsInquryAccepted(inquiryno);
            if (inquiry != null)
            {

                txtInquiryNo.Text = inquiryno;

                //txtDate.EditValue = InfoPCMS.conversion.convertDate(inquiry.InquiryDate);
                String inqdate = inquiry.InquiryDate;
                DateTime inqdt = Convert.ToDateTime(inqdate);
                txtDate.EditValue = inqdt;
                txtTime.EditValue = inquiry.InquiryTime.ToString().Trim();
                txtCustomer.Text = inquiry.StrCusName.ToString().Trim();
                txtGenNo.Text = inquiry.HowKnow;
                txtInquiryTaken.Text = inquiry.InquiryTaken;
                // txtInspectionDetails.Text = inquiry.InspectionDetails;
                txtProblemNature.Text = inquiry.ProblemNature;

                txtInquiryNo.Text = inquiryno;

                txtProblemSource.Text = inquiry.ProblemSource;
                txtSiteResponsible.Text = inquiry.SiteResponsible;
                // txtIfFailed.Text = inquiry.IfFailed.ToString().Trim();
                txtProblemSource.Text = inquiry.Recommendation.ToString().Trim();
                txtFollowupDetails.Text = inquiry.FollowupDetails;
                //txtInspectionDone.SelectedValue = inquiry.InspectionDone;
                txtLocation.Text = inquiry.Location;
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


                xtraTabControl1.SelectedTabPage = xtraTabPage7;

            }
            else
            {

                XtraMessageBox.Show("No such record exists", "Information");

            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (gridView4.RowCount.ToString() == "0")
            {
                XtraMessageBox.Show("No Completed Inquiries.", "Information");
            }
            else
            {
                InquiryDetails(gridView4.GetFocusedDataRow()["InquiryNumber"].ToString());
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (gridView3.RowCount.ToString() == "0")
            {
                XtraMessageBox.Show("No Forwarded Inquiries.", "Information");
            }
            else
            {
                InquiryDetails(gridView3.GetFocusedDataRow()["InquiryNumber"].ToString());
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount.ToString() == "0")
            {
                XtraMessageBox.Show("No Accepted Inquiries.", "Information");
            }
            else
            {
                InquiryDetails(gridView2.GetFocusedDataRow()["InquiryNumber"].ToString());
            }
        }

        private void gridView8_DoubleClick(object sender, EventArgs e)
        {
            //lblType.Text = "Service";
            // lblInquiryNumber.Text = gridView11.GetFocusedDataRow()["GeneratorNumber"].ToString();

            xtraTabControl1.SelectedTabPageIndex = 1;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = true;
            xtraTabPage4.PageEnabled = false;
            xtraTabPage5.PageEnabled = false;
            xtraTabPage7.PageEnabled = false;
            xtraTabPage13.PageEnabled = false;
            xtraTabControl2.SelectedTabPageIndex = 1;
            xtraTabPage3.PageEnabled = false;
            xtraTabPage6.PageEnabled = true;
            tbPageChangeInqDetails.PageEnabled = false;
            //pnlRecRepairs.Enabled = false;

            //gridView1.GetFocusedDataRow()["InquiryNumber"].ToString();


            //cboActionType.Focus();
            DataTable Completed = new DatabaseService().executeSelectQuery("SELECT  InquiryNumber, ActionType, ActionDetails,ActionDate, Remarks, JobCompleted FROM InquiryStatusDetails where InquiryNumber='" + lblInquiryNumber.Text + "'");
            tblupdate.DataSource = Completed;

        }

        //private void gridView11_DoubleClick(object sender, EventArgs e)
        //{
        //    //lblGeneratorNumber.Text = gridView11.GetFocusedDataRow()["GeneratorNumber"].ToString();
        //   // xtraTabControl3.SelectedTabPageIndex = 1;
        //   // cboserviceAction.Focus();
        //   // DataTable Completed = new DatabaseService().executeSelectQuery("SELECT* FROM ServiceStatusDetails WHERE GeneratorNumber='" + gridView11.GetFocusedDataRow()["GeneratorNumber"].ToString() + "'");
        //   // gridControl3.DataSource = Completed;
        //}

        //private void simpleButton9_Click(object sender, EventArgs e)
        //{
        //    Boolean IsChecked = false;
        //    if (cboserviceAction.SelectedItem.ToString().Equals("Job completed"))
        //    {
        //        IsChecked = true;
        //    }
        //    new DatabaseService().executeUpdateQuery("INSERT INTO ServiceStatusDetails VALUES('" + cboserviceAction.Text + "','" + memoEdit2.Text + "','" + dateEdit1.Text + "','','" + IsChecked + "','" + lblGeneratorNumber.Text + "')");
        //    if (IsChecked)
        //    {

        //        new DatabaseService().executeUpdateQuery("UPDATE ServiceShedule SET Status='Compelete', JobDoneDate='" + System.DateTime.Now + "' WHERE GeneratorNumber='" + lblGeneratorNumber.Text + "'");
        //    }
        //    MessageBox.Show("Record Saved Successfully");
        //    DataTable Completed = new DatabaseService().executeSelectQuery("SELECT* FROM ServiceStatusDetails WHERE GeneratorNumber='" + lblGeneratorNumber.Text + "'");
        //    gridControl3.DataSource = Completed;

        //    cboserviceAction.SelectedIndex = 0;
        //    memoEdit2.Text = "";
        //    dateEdit1.Text = "";

        //    cboserviceAction.Focus();
        //    TableLoad();
        //}

        //private void gridView9_DoubleClick(object sender, EventArgs e)
        //{
        //    lblGeneratorNumber.Text = gridView9.GetFocusedDataRow()["GeneratorNumber"].ToString();
        //    xtraTabControl3.SelectedTabPageIndex = 1;
        //    cboserviceAction.Focus();
        //    DataTable Completed = new DatabaseService().executeSelectQuery("SELECT* FROM ServiceStatusDetails WHERE GeneratorNumber='" + gridView9.GetFocusedDataRow()["GeneratorNumber"].ToString() + "'");
        //    gridControl3.DataSource = Completed;
        //}

        //private void simpleButton8_Click(object sender, EventArgs e)
        //{
        //    this.UpdateAcceptedTask();

        //}

        public void UpdateAcceptedTask()
        {
            for (int i = 0; i < Dtgridaccepted.Rows.Count; i++)
            {
                new DatabaseService().executeSelectQuery("UPDATE InquiryStatus SET actual='" + Dtgridaccepted.Rows[i]["actual"].ToString() + "', duedate='" + Dtgridaccepted.Rows[i]["duedate"].ToString() + "' where InquiryNumber='" + Dtgridaccepted.Rows[i]["InquiryNumber"].ToString() + "'");
            }
            Dtgridaccepted.Clear();

            //if (InfoPCMS.Views.frmHome.UsrName == "admin")
            //{
            if (InfoPCMS.Views.frmHome.EmpCat == "LEVEL 3")
            {

                //Dtgridaccepted = new DatabaseService().executeSelectQuery("SELECT (select   Sum(CAST(Percentage as numeric(9))) FROM InquiryStatusDetails where InquiryNumber=dbo.InquiryStatus.InquiryNumber) as percentage,actual,duedate, InquiryStatus.InquiryNumber as  InquiryNumber,InquiryStatus.Dates as  date, InquiryStatus.Customer as  Customer, InquiryStatus.InquiryTaken as InquiryTaken, InquiryStatus.InspectionDone as InspectionDone, InquiryStatus.SiteResponsible as SiteResponsible, Inquiry.SiteResponsible,InquiryStatus.NextAction as NextAction,InquiryStatus.DateTime as DateTime,DATEADD(hour,(SELECT CompletionHours FROM InquiryType WHERE InquiryType = dbo.Inquiry.ProblemNature),(cast(cast(Inquiry.Date as date) as datetime) + cast(Inquiry.Time as time(7)))) AS AgreedDate FROM InquiryStatus INNER JOIN Inquiry ON InquiryStatus.InquiryNumber = Inquiry.InquiryNumber where InquiryStatus.Status='Accept' ");

                Dtgridaccepted = new DatabaseService().executeSelectQuery("SELECT (select Sum(CAST(Percentage as numeric(9))) FROM HayleysPowerEngineeringCRM.dbo.InquiryStatusDetails "
                   + " where InquiryNumber=T0.InquiryNumber) as percentage, "
                   + " T0.actual,T0.duedate,T0.dueTime, T0.InquiryNumber as  InquiryNumber,T1.JobEntryDate,T1.JobEntryTime, "
                   + " T2.CustomerName as Customer,T2.CustomerCategory,T5.CategoryName,T3.EmployeeName as InquiryTaken, "
                   + " T0.InspectionDone as InspectionDone,t4.EmployeeName as SiteResponsible, "
                   + " T0.NextAction as NextAction,T0.DateTime  "
                   + " FROM HayleysPowerEngineeringCRM.dbo.InquiryStatus T0  "
                   + " INNER JOIN HayleysPowerEngineeringCRM.dbo.Inquiry T1  ON T0.InquiryNumber = T1.InquiryNumber "
                   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
                   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T3 ON T0.InquiryTaken = T3.Id "
                   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T4 ON T1.SiteResponsible = T4.Id "
                   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T5 ON T2.CustomerCategory = T5.Id "
                   + " where T0.Status='Accept' ");

                gridaccepted.DataSource = Dtgridaccepted;
            }
            else
            {
                DataTable dtuser = new DatabaseService().executeSelectQuery("select Id from Users  WHERE Username='" + InfoPCMS.Views.frmHome.UsrName + "'");
                //DataTable dtuser = new DatabaseService().executeSelectQuery("select Id from Users  WHERE Username='" + InfoPCMS.Views.frmHome.UsrName + "'");
                DataTable dtEmployee = new DatabaseService().executeSelectQuery("select Id,EmployeeName from Employee  WHERE EmployeeName='" + InfoPCMS.Views.frmHome.EmpName + "'");


                if (dtuser.Rows.Count > 0)
                {
                    String EmpName = dtEmployee.Rows[0]["EmployeeName"].ToString().Trim();
                    String EmpId = dtEmployee.Rows[0]["Id"].ToString().Trim();
                    String Id = dtuser.Rows[0]["Id"].ToString();

                    //Dtgridaccepted = new DatabaseService().executeSelectQuery("SELECT (select   Sum(CAST(Percentage as numeric(9))) FROM InquiryStatusDetails where InquiryNumber=dbo.InquiryStatus.InquiryNumber) as percentage,actual,duedate, InquiryStatus.InquiryNumber as  InquiryNumber,InquiryStatus.Dates as  date, InquiryStatus.Customer as  Customer, InquiryStatus.InquiryTaken as InquiryTaken, InquiryStatus.InspectionDone as InspectionDone, InquiryStatus.SiteResponsible as SiteResponsible, Inquiry.SiteResponsible,InquiryStatus.NextAction as NextAction,InquiryStatus.DateTime as DateTime,DATEADD(hour,(SELECT CompletionHours FROM InquiryType WHERE InquiryType = dbo.Inquiry.ProblemNature),(cast(cast(Inquiry.Date as date) as datetime) + cast(Inquiry.Time as time(7)))) AS AgreedDate FROM InquiryStatus INNER JOIN Inquiry ON InquiryStatus.InquiryNumber = Inquiry.InquiryNumber where InquiryStatus.Status='Accept' AND Inquiry.SiteResponsible = '" + Id + "'");
                    Dtgridaccepted = new DatabaseService().executeSelectQuery("SELECT (select Sum(CAST(Percentage as numeric(9))) FROM HayleysPowerEngineeringCRM.dbo.InquiryStatusDetails "
                   + " where InquiryNumber=T0.InquiryNumber) as percentage, "
                   + " T0.actual,T0.duedate,T0.dueTime, T0.InquiryNumber as  InquiryNumber,T1.JobEntryDate,T1.JobEntryTime, "
                   + " T2.CustomerName as Customer,T2.CustomerCategory,T5.CategoryName,T3.EmployeeName as InquiryTaken, "
                   + " T0.InspectionDone as InspectionDone,t4.EmployeeName as SiteResponsible, "
                   + " T0.NextAction as NextAction,T0.DateTime  "
                   + " FROM HayleysPowerEngineeringCRM.dbo.InquiryStatus T0  "
                   + " INNER JOIN HayleysPowerEngineeringCRM.dbo.Inquiry T1  ON T0.InquiryNumber = T1.InquiryNumber "
                   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
                   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T3 ON T0.InquiryTaken = T3.Id "
                   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T4 ON T1.SiteResponsible = T4.Id "
                   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T5 ON T2.CustomerCategory = T5.Id "
                   + " where T0.Status='Accept' AND T1.SiteResponsible = '" + EmpId + "' ");


                    gridaccepted.DataSource = Dtgridaccepted;
                }

            }

        }

        //private void gridView2_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        //{
        //    //GridView View = sender as GridView;
        //    //if (e.RowHandle >= 0)
        //    //{
        //    //    if (View.GetRowCellDisplayText(e.RowHandle, View.Columns["Date"]) == null || View.GetRowCellDisplayText(e.RowHandle, View.Columns["Date"]).Equals("") || View.GetRowCellDisplayText(e.RowHandle, View.Columns["duedate"]) == null || View.GetRowCellDisplayText(e.RowHandle, View.Columns["duedate"]).Equals(""))
        //    //    {
        //    //        Console.WriteLine("not in match");
        //    //    }
        //    //    else
        //    //    {
        //    //        DateTime sAcceptedDate = Convert.ToDateTime(View.GetRowCellDisplayText(e.RowHandle, View.Columns["Date"]));
        //    //        DateTime sAcceptedTime = Convert.ToDateTime(View.GetRowCellDisplayText(e.RowHandle, View.Columns["Time"]));

        //    //        DateTime AcceptedDateTime = sAcceptedDate.Date.Add(sAcceptedTime.TimeOfDay);

        //    //        DateTime sDuedDate = Convert.ToDateTime(View.GetRowCellDisplayText(e.RowHandle, View.Columns["duedate"]));
        //    //        DateTime sDuedTime = Convert.ToDateTime(View.GetRowCellDisplayText(e.RowHandle, View.Columns["dueTime"]));

        //    //        DateTime DueDateTime = sDuedDate.Date.Add(sDuedTime.TimeOfDay);
        //    //        string strDueDateTime = DueDateTime.ToString("MM/dd/yyyy");

        //    //        //DateTime DuedDate = Convert.ToDateTime(sDuedDate);
        //    //        DateTime today = System.DateTime.Now;

        //    //        TimeSpan t1 = DueDateTime - AcceptedDateTime;
        //    //        double deff1 = t1.TotalDays;
        //    //        TimeSpan t2 = DueDateTime - today;
        //    //        double deff2 = t2.TotalDays;
        //    //        double FinalDeff = deff1 - deff2;

        //    //        TimeSpan span = DueDateTime.Subtract(AcceptedDateTime);

        //    //        double total = span.Hours;

        //    //        TimeSpan span1 = DueDateTime - AcceptedDateTime;
        //    //        double totalMinutes = span1.TotalMinutes;

        //    //        double per90 = (double)((double)((double)90 / (double)100) * (double)deff1);
        //    //        double per75 = (double)((double)((double)75 / (double)100) * (double)deff1);
        //    //        double per50 = (double)((double)((double)50 / (double)100) * (double)deff1);
        //    //        double per25 = (double)((double)((double)25 / (double)100) * (double)deff1);

        //    //        if (FinalDeff >= per90)
        //    //        {
        //    //            Console.WriteLine("over 90");
        //    //            e.Appearance.BackColor = Color.Red;
        //    //            e.Appearance.BackColor2 = Color.SeaShell;
        //    //        }
        //    //        else if (FinalDeff >= per75 && per90 >= FinalDeff)
        //    //        {
        //    //            Console.WriteLine("per 90");
        //    //            e.Appearance.BackColor = Color.OrangeRed;
        //    //            e.Appearance.BackColor2 = Color.SeaShell;
        //    //        }
        //    //        else if (FinalDeff >= per50 && per75 >= FinalDeff)
        //    //        {
        //    //            Console.WriteLine("per 75");
        //    //            e.Appearance.BackColor = Color.Yellow;
        //    //            e.Appearance.BackColor2 = Color.SeaShell;
        //    //        }
        //    //        else if (FinalDeff >= per25 && per50 >= FinalDeff)
        //    //        {
        //    //            Console.WriteLine("per 50");
        //    //            e.Appearance.BackColor = Color.Lime;
        //    //            e.Appearance.BackColor2 = Color.SeaShell;
        //    //        }
        //    //        else if (FinalDeff >= 0 && (double)per25 >= FinalDeff)
        //    //        {
        //    //            Console.WriteLine("per 25");

        //    //        }


        //            //Console.WriteLine(deff1 + "  " + FinalDeff + "  " + deff2 + "  " + per25 + "  " + per50 + "   " + per75 + "  " + per90);

        //          //  Console.WriteLine("-------------------");
        //     //   }

        //    //}
        //}

        //private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        //{

        //}

        private void FrmMyTask_Load(object sender, EventArgs e)
        {
            dateEdit2.DateTime = System.DateTime.Now;
            timeEdit1.Time = System.DateTime.Now;
        }



        private void FrmMyTask_FormClosing(object sender, FormClosingEventArgs e)
        {
            //xtraTabControl3.SelectedTabPageIndex = 1;

            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                //bool blAllowClose = false;

                if (!this.CheckActiontake(lblInquiryNumber.Text.ToString().Trim()))
                {
                    ////blAllowClose = true;
                    e.Cancel = true;
                    XtraMessageBox.Show("Please take Action for Inquiry before closing.", "Information");
                }
                else
                {

                }
            }

        }

        private void cmbxSubActionTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtSubActionDetails = new DatabaseService().executeSelectQuery("select * from ActionTypeCheckList WHERE ActionType = '" + lblInquiryType.Text.Trim() + "' AND SubActionType =  '" + cmbxSubActionTypes.SelectedValue.ToString().Trim() + "' ");

            //txtRecRepairs.Text = "";
            //dtExpectedRecRepairs.Text = "";

            //if (cmbxSubActionTypes.SelectedValue.ToString().Trim() == "Complete")
            //{
            //    pnlRecRepairs.Enabled = true;

            //}
            //else
            //{
            //    pnlRecRepairs.Enabled = false;
            //}

            //txtCompPer.Text = dtSubActionDetails.Rows[0]["Percentage"].ToString().Trim();
        }

        private void btnAddSubAction_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DatabaseService().executeSelectQuery("select Id from InquiryActionTypeCheckList WHERE InquiryNo='" + lblInquiryNumber.Text.Trim() + "' AND ActionTypeID='" + this.getActionTypeID(cboActionType.SelectedValue.ToString().Trim()) + "' AND SubActionType = '" + cmbxSubActionTypes.SelectedValue.ToString().Trim() + "' ");
            //if (dt.Rows.Count > 0)
            //{
            //    MessageBox.Show("Sub Action Alraeday Added for Inquriy's Action");
            //}
            //else
            //{


            //    new DatabaseService().executeUpdateQuery("INSERT INTO InquiryActionTypeCheckList VALUES('" + lblInquiryNumber.Text.ToString() + "','" + this.getActionTypeID(cboActionType.SelectedValue.ToString().Trim()) + "','" + cboActionType.SelectedValue.ToString().Trim() + "','" + cmbxSubActionTypes.SelectedValue.ToString().Trim() + "')");

            //    //cmbxNextAction.Items.Add(cmbxSubActionTypes.SelectedValue.ToString().Trim());

            txtNetAction.Text = cmbxSubActionTypes.SelectedValue.ToString().Trim();

            //}

        }

        public string getCusName(string strCusID)
        {
            string strCusName = "";

            DataTable dt2 = new DatabaseService().executeSelectQuery("SELECT * FROM Customer WHERE Id = '" + strCusID.Trim() + "' ");
            if (dt2.Rows.Count > 0)
            {
                strCusName = dt2.Rows[0]["CustomerName"].ToString().Trim();
            }


            return strCusName;
        }

        public string getActionTypeID(string strActionType)
        {
            string subActionId = "";

            try
            {

                DataTable dt = new DatabaseService().executeSelectQuery("select Id from ActionType where ActionType ='" + strActionType + "'");
                if (dt.Rows.Count > 0)
                {

                    subActionId = dt.Rows[0]["Id"].ToString();

                }

            }
            catch (Exception ex)
            {


            }

            return subActionId;

        }

        private void btnRemoveSubAction_Click(object sender, EventArgs e)
        {
            DataTable dt = new DatabaseService().executeSelectQuery("select Id from InquiryActionTypeCheckList WHERE InquiryNo='" + lblInquiryNumber.Text.Trim() + "' AND ActionTypeID='" + this.getActionTypeID(lblInquiryType.Text.Trim()) + "' AND SubActionType = '" + cmbxSubActionTypes.SelectedValue.ToString().Trim() + "' ");
            if (dt.Rows.Count > 0)
            {
                new DatabaseService().executeUpdateQuery("DELETE FROM InquiryActionTypeCheckList WHERE InquiryNo='" + lblInquiryNumber.Text.Trim() + "' AND ActionTypeID='" + this.getActionTypeID(lblInquiryType.Text.Trim()) + "' AND SubActionType = '" + cmbxSubActionTypes.SelectedValue.ToString().Trim() + "' ");

                DataTable dtModifiedSubAcctions = new DatabaseService().executeSelectQuery("select SubActionType from InquiryActionTypeCheckList WHERE InquiryNo='" + lblInquiryNumber.Text.Trim() + "' AND ActionTypeID='" + this.getActionTypeID(lblInquiryType.Text.Trim()) + "' ");

                cmbxSubActionTypes.DataSource = dtModifiedSubAcctions;
                cmbxSubActionTypes.DisplayMember = "SubActionType";
                cmbxSubActionTypes.ValueMember = "SubActionType";

            }
            else
            {
                XtraMessageBox.Show("Sub Action Alraeday Removed for Inquriy's Action List", "Information");
            }
        }

        private void timeEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

      

        //private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

        //    DataTable dtGridAccepted = new DatabaseService().executeSelectQuery("SELECT (select   Sum(CAST(Percentage as numeric(9))) FROM InquiryStatusDetails where InquiryNumber=dbo.InquiryStatus.InquiryNumber) as percentage,actual,duedate, InquiryStatus.InquiryNumber as  InquiryNumber,InquiryStatus.Dates as  date, InquiryStatus.Customer as  Customer, InquiryStatus.InquiryTaken as InquiryTaken, InquiryStatus.InspectionDone as InspectionDone, InquiryStatus.SiteResponsible as SiteResponsible, Inquiry.SiteResponsible,InquiryStatus.NextAction as NextAction,InquiryStatus.DateTime as DateTime,DATEADD(hour,(SELECT CompletionHours FROM InquiryType WHERE InquiryType = dbo.Inquiry.ProblemNature),(cast(cast(Inquiry.Date as date) as datetime) + cast(Inquiry.Time as time(7)))) AS AgreedDate FROM InquiryStatus INNER JOIN Inquiry ON InquiryStatus.InquiryNumber = Inquiry.InquiryNumber where InquiryStatus.Status='Accept' ");

        //    DateTime dtDueDate = Convert.ToDateTime(gridView2.GetFocusedDataRow()["duedate"].ToString().Trim());

        //    // gridView2.GetFocusedDataRow()["InquiryNumber"].ToString()
        //    if (e.RowHandle >= 0)
        //    {
        //        if (e.Column.FieldName == "duedate")
        //        {
        //            DateTime strAgreeDate = Convert.ToDateTime(gridView2.GetFocusedDataRow()["duedate"].ToString());

        //            //if (!(InfoPCMS.Views.frmHome.UsrName == "admin"))
        //            //{
        //            if (!(InfoPCMS.Views.frmHome.EmpCat == "LEVEL 3"))
        //            {


        //                if (dtDueDate < strAgreeDate)
        //                {
        //                    //MessageBox.Show("Agreed Complete Date Shouldn't Larger than Due Date.Send for Supervisor Approval.");
        //                    XtraMessageBox.Show("Agreed Complete Date Larger than Due Date.Send Notification to Supervisor.", "Information");
        //                    // new DatabaseService().executeSelectQuery("UPDATE InquiryStatus SET PendingApproval='Yes', actual='" + gridView2.GetFocusedDataRow()["actual"].ToString() + "' where InquiryNumber='" + gridView2.GetFocusedDataRow()["InquiryNumber"].ToString() + "'");

        //                    SendMailToSupervisor();

        //                }
        //                else
        //                {


        //                    new DatabaseService().executeSelectQuery("UPDATE InquiryStatus SET actual='" + gridView2.GetFocusedDataRow()["actual"].ToString() + "', duedate='" + gridView2.GetFocusedDataRow()["duedate"].ToString() + "' where InquiryNumber='" + gridView2.GetFocusedDataRow()["InquiryNumber"].ToString() + "'");


        //                    lblInquiryNumber.Text = gridView2.GetFocusedDataRow()["InquiryNumber"].ToString();

        //                    xtraTabControl2.SelectedTabPageIndex = 1;

        //                    // this.SetActionTypes();


        //                    //cboActionType.Focus();



        //                    DataTable Completed = new DatabaseService().executeSelectQuery("SELECT  InquiryNumber, ActionType,SubActionType,ActionDetails,ActionDate, Remarks, JobCompleted,Percentage FROM InquiryStatusDetails where InquiryNumber='" + lblInquiryNumber.Text + "'");
        //                    tblupdate.DataSource = Completed;
        //                }
        //            }
        //            else
        //            {

        //                for (int i = 0; i < Dtgridaccepted.Rows.Count; i++)
        //                {
        //                    new DatabaseService().executeSelectQuery("UPDATE InquiryStatus SET actual='" + Dtgridaccepted.Rows[i]["actual"].ToString() + "', duedate='" + Dtgridaccepted.Rows[i]["duedate"].ToString() + "' where InquiryNumber='" + Dtgridaccepted.Rows[i]["InquiryNumber"].ToString() + "'");
        //                }
        //                this.UpdateAcceptedTask();

        //                lblInquiryNumber.Text = gridView2.GetFocusedDataRow()["InquiryNumber"].ToString();
        //                xtraTabControl2.SelectedTabPageIndex = 1;

        //                // this.SetActionTypes();




        //                //cboActionType.Focus();



        //                DataTable Completed = new DatabaseService().executeSelectQuery("SELECT  InquiryNumber, ActionType,SubActionType,ActionDetails,ActionDate, Remarks, JobCompleted,Percentage FROM InquiryStatusDetails where InquiryNumber='" + lblInquiryNumber.Text + "'");
        //                tblupdate.DataSource = Completed;
        //            }


        //        }
        //    }
        //}










        private void btnView_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(txtCompJobSheet.Text);
        }

        private void SendMailToSupervisor()
        {


        }


        private void xtraTabPage6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblInquiryNumber_Click(object sender, EventArgs e)
        {

        }

        private void gridaccepted_Click(object sender, EventArgs e)
        {

        }

        private void panelControl4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnChangeInqDetails_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount.ToString() == "0")
            {
                XtraMessageBox.Show("No Accepted Inquiries.", "Information");
            }
            else
            {

                DataTable dtInqStatus = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryStatus WHERE InquiryNumber='" + gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim() + "' ");

                DataTable dtInqChangeComp = new DatabaseService().executeSelectQuery("SELECT * FROM InqChangeCompDateTemp WHERE InquiryNumber='" + gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim() + "' ");

                string StatusReqChange = "";


                if (dtInqChangeComp.Rows.Count.ToString() == "0")
                {
                    StatusReqChange = "";
                }
                else
                {
                    StatusReqChange = dtInqChangeComp.Rows[0]["Status"].ToString().Trim();

                }
                string Status = "None";
                if (dtInqStatus.Rows.Count > 0)
                {
                    Status = dtInqStatus.Rows[0]["Status"].ToString().Trim();
                }


                if (Status == "Completed")
                {
                    XtraMessageBox.Show("Inquiry Alreday Accepted or Completed", "Information");
                }
                else if (StatusReqChange == "Pending Approval")
                {
                    XtraMessageBox.Show("Pending Supervisor Approval for Completed Date Change", "Error");
                }
                else
                {


                    lblInqNo.Text = gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim();

                    String AcceptDate = dtInqStatus.Rows[0]["Date"].ToString().Trim();
                    DateTime dtAcceptDate = Convert.ToDateTime(AcceptDate);
                    dateAccepted.EditValue = dtAcceptDate;
                    timeAccepted.EditValue = dtInqStatus.Rows[0]["Time"].ToString().Trim();


                    String dueDate = dtInqStatus.Rows[0]["dueDate"].ToString().Trim();
                    DateTime dtDueDate = Convert.ToDateTime(dueDate);
                    dateComp.EditValue = dtDueDate;
                    timeComp.EditValue = dtInqStatus.Rows[0]["dueTime"].ToString().Trim();







                    xtraTabControl2.SelectedTabPageIndex = 2;


                    xtraTabPage3.PageEnabled = false;
                    xtraTabPage6.PageEnabled = false;
                    tbPageChangeInqDetails.PageEnabled = true;


                }
                //String inqdate = inquiry.InquiryDate;
                //DateTime inqdt = Convert.ToDateTime(inqdate);
                //txtDate.EditValue = inqdt;
                //txtTime.EditValue = inquiry.InquiryTime.ToString().Trim();

            }
        }

        public int DifferenceMinites(DateTime startDate, DateTime endDate, Boolean excludeWeekends)
        {
            int count = 0;
            for (DateTime index = startDate; index < endDate; index = index.AddMinutes(1))
            {

                count++;


            }

            return count;
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {


            DataTable dtInqStatus = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryStatus WHERE InquiryNumber='" + lblInqNo.Text.ToString().Trim() + "' ");
            DataTable dtInqDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Inquiry WHERE InquiryNumber='" + lblInqNo.Text.ToString().Trim() + "' ");

            DataTable dtCusDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Customer WHERE Id = '" + dtInqDetails.Rows[0]["CustomerId"].ToString().Trim() + "' ");

            DataTable dtDueDate = new DataTable();
            DataTable dtInqTypeDetails = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryType WHERE InquiryType = '" + dtInqDetails.Rows[0]["ProblemNature"].ToString().Trim() + "' AND CusCategory = '" + dtCusDetails.Rows[0]["CustomerCategory"].ToString().Trim() + "'");


            DateTime JobEntryDate = Convert.ToDateTime(gridView2.GetFocusedDataRow()["JobEntryDate"].ToString());
            DateTime JobEntryTime = Convert.ToDateTime(gridView2.GetFocusedDataRow()["JobEntryTime"].ToString());
            DateTime JobEntryDateTime = JobEntryDate.Date.Add(JobEntryTime.TimeOfDay);


            DateTime agreedCompDate = Convert.ToDateTime(gridView2.GetFocusedDataRow()["dueDate"].ToString());
            DateTime agreedCompTime = Convert.ToDateTime(gridView2.GetFocusedDataRow()["dueTime"].ToString());
            DateTime agreedCompDateTime = agreedCompDate.Date.Add(agreedCompTime.TimeOfDay);

            if (string.IsNullOrEmpty(dateComp.EditValue.ToString()) || string.IsNullOrEmpty(timeComp.EditValue.ToString()))
            {

                XtraMessageBox.Show("Cannot leave the required fields empty", "Error");
            }
            else
            {
                //-----------------------------------------------------------------------------------

                DateTime NewagreedCompDate = Convert.ToDateTime(dateComp.EditValue.ToString());
                DateTime NewagreedCompTime = Convert.ToDateTime(timeComp.EditValue.ToString());
                DateTime NewagreedCompDateTime = NewagreedCompDate.Date.Add(NewagreedCompTime.TimeOfDay);

                int differenceMinites = this.DifferenceMinites(JobEntryDateTime, NewagreedCompDateTime, true);

                int firstReminder = 0;

                if (dtInqTypeDetails.Rows.Count > 0)
                {
                    firstReminder = (differenceMinites * Convert.ToInt32(dtInqTypeDetails.Rows[0]["Reminder1"].ToString())) / 100;
                }


                DateTime dtRemainder1 = JobEntryDateTime.AddMinutes(firstReminder);
                var Remainder1Date = dtRemainder1.Date;
                string strRemainder1Date = Remainder1Date.ToString("yyyy-MM-dd");
                var Remainder1Time = dtRemainder1 - dtRemainder1.Date;

                int SecondReminder = 0;
                if (dtInqTypeDetails.Rows.Count > 0)
                {

                    SecondReminder = (differenceMinites * Convert.ToInt32(dtInqTypeDetails.Rows[0]["Reminder2"].ToString())) / 100;
                }


                DateTime dtRemainder2 = JobEntryDateTime.AddMinutes(SecondReminder);
                var Remainder2Date = new DateTime();
                var Remainder2Time = new TimeSpan();


                if (dtInqTypeDetails.Rows[0]["CusCategory"].ToString() == "1")
                {
                    dtRemainder2 = JobEntryDateTime.AddMinutes(SecondReminder);
                    Remainder2Date = dtRemainder2.Date;
                    string strRemainder2Date = Remainder2Date.ToString("yyyy-MM-dd");
                    Remainder2Time = dtRemainder2 - dtRemainder2.Date;

                }

                if (dtInqTypeDetails.Rows.Count > 0)
                {
                    DateTime dtEsclation1 = NewagreedCompDateTime.AddHours(Convert.ToInt32(dtInqTypeDetails.Rows[0]["Escalation1"].ToString()));
                    var Esclation1Date = dtEsclation1.Date;
                    string strEsclation1Date = Esclation1Date.ToString("yyyy-MM-dd");
                    var Esclation1Time = dtEsclation1 - dtEsclation1.Date;

                    DateTime dtEsclation2 = NewagreedCompDateTime.AddHours(Convert.ToInt32(dtInqTypeDetails.Rows[0]["Escalation2"].ToString()));
                    var Esclation2Date = dtEsclation2.Date;
                    string strEsclation2Date = Esclation2Date.ToString("yyyy-MM-dd");
                    var Esclatio2Time = dtEsclation2 - dtEsclation2.Date;


                    //-----------------------------------------------------------------------------------


                    if (NewagreedCompDateTime > agreedCompDateTime)
                    {

                        //DataTable dtInqSatusDetails = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryStatus");


                        String dueDate = dtInqStatus.Rows[0]["dueDate"].ToString().Trim();
                        // DateTime dtDueDate = Convert.ToDateTime(dueDate);
                        // dateComp.EditValue = dtDueDate;
                        String dueTime = dtInqStatus.Rows[0]["dueTime"].ToString().Trim();

                        new DatabaseService().executeUpdateQuery("UPDATE InquiryStatus SET actual = '" + txtActualProblem.Text.Trim() + "' WHERE InquiryNumber= '" + lblInqNo.Text.ToString().Trim() + "'");

                        new DatabaseService().executeUpdateQuery("INSERT INTO InqChangeCompDateTemp VALUES('" + lblInqNo.Text.ToString().Trim() + "','" + dtInqDetails.Rows[0]["CustomerId"].ToString().Trim() + "','" + txtRemark.Text.Trim() + "','Pending Approval','" + txtActualProblem.Text.Trim() + "','" + agreedCompDate + "','" + agreedCompTime + "','" + dateComp.Text + "','" + timeComp.Text + "','" + GetEmpId(InfoPCMS.Views.frmHome.EmpName) + "',NULL,'','' )");

                        //  int difOldMin = this.DifferenceMinites(JobEntryDateTime, agreedCompDateTime, true);
                        // int difMin = this.DifferenceMinites(JobEntryDateTime, NewagreedCompDateTime, true);


                        if (!InfoPCMS.Views.frmHome.isOffline)
                        {

                            SendMailRequestApproval();

                        }
                        XtraMessageBox.Show("Agreed Complete Date Larger than Due Date.Send to Supervisor Approval.", "Information");

                        new DatabaseService().executeUpdateQuery("UPDATE InqEscalation SET dueDate = '" + NewagreedCompDate.ToString("yyyy-MM-dd") + "',dueTime = '" + NewagreedCompTime.ToString("yyyy-MM-dd") + "',Remainder1Date = '" + Remainder1Date + "', "
                           + "  Remainder1Time = '" + Remainder1Time + "',  Remainder2Date= '" + Remainder2Date + "',Remainder2Time  = '" + Remainder2Time + "',Escalation1Date = '" + Esclation1Date + "',Escalation1Time = '" + Esclation1Time + "', "
                           + " Escalation2Date = '" + Esclation2Date + "', Escalation2Time = '" + Esclatio2Time + "' WHERE InquiryNo= '" + lblInqNo.Text.ToString().Trim() + "'");


                    }
                    else
                    {


                        new DatabaseService().executeUpdateQuery("UPDATE InqEscalation SET dueDate = '" + NewagreedCompDate.ToString("yyyy-MM-dd") + "',dueTime = '" + NewagreedCompTime.ToString("H:mm") + "',Remainder1Date = '" + Remainder1Date.ToString("yyyy-MM-dd") + "', "
                            + "  Remainder1Time = '" + Remainder1Time + "',  Remainder2Date= '" + Remainder2Date.ToString("yyyy-MM-dd") + "',Remainder2Time  = '" + Remainder2Time + "',Escalation1Date = '" + Esclation1Date.ToString("yyyy-MM-dd") + "',Escalation1Time = '" + Esclation1Time + "', "
                            + " Escalation2Date = '" + Esclation2Date.ToString("yyyy-MM-dd") + "', Escalation2Time = '" + Esclatio2Time + "' WHERE InquiryNo= '" + lblInqNo.Text.ToString().Trim() + "'");

                        new DatabaseService().executeUpdateQuery("UPDATE InquiryStatus SET duedate='" + NewagreedCompDate.ToString("yyyy-MM-dd") + "',dueTime='" + NewagreedCompTime.ToString("H:MM") + "',actual = '" + txtActualProblem.Text.Trim() + "' WHERE InquiryNumber= '" + lblInqNo.Text.ToString().Trim() + "'");


                    }

                    if (InfoPCMS.Views.frmHome.EmpCat == "LEVEL 3")
                    {

                        Dtgridaccepted = new DatabaseService().executeSelectQuery("SELECT (select Sum(CAST(Percentage as numeric(9))) FROM HayleysPowerEngineeringCRM.dbo.InquiryStatusDetails "
                       + " where InquiryNumber=T0.InquiryNumber) as percentage, "
                       + " T0.actual,T0.duedate,T0.dueTime, T0.InquiryNumber as  InquiryNumber,T1.JobEntryDate,T1.JobEntryTime, "
                       + " T2.CustomerName as Customer,T2.CustomerCategory,T5.CategoryName,T3.EmployeeName as InquiryTaken, "
                       + " T0.InspectionDone as InspectionDone,t4.EmployeeName as SiteResponsible, "
                       + " T0.NextAction as NextAction,T0.DateTime  "
                       + " FROM HayleysPowerEngineeringCRM.dbo.InquiryStatus T0  "
                       + " INNER JOIN HayleysPowerEngineeringCRM.dbo.Inquiry T1  ON T0.InquiryNumber = T1.InquiryNumber "
                       + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
                       + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T3 ON T0.InquiryTaken = T3.Id "
                       + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T4 ON T1.SiteResponsible = T4.Id "
                       + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T5 ON T2.CustomerCategory = T5.Id "
                       + " where T0.Status='Accept' ");
                    }

                    else
                    {
                        Dtgridaccepted = new DatabaseService().executeSelectQuery("SELECT (select Sum(CAST(Percentage as numeric(9))) FROM HayleysPowerEngineeringCRM.dbo.InquiryStatusDetails "
                      + " where InquiryNumber=T0.InquiryNumber) as percentage, "
                      + " T0.actual,T0.duedate,T0.dueTime, T0.InquiryNumber as  InquiryNumber,T1.JobEntryDate,T1.JobEntryTime, "
                      + " T2.CustomerName as Customer,T2.CustomerCategory,T5.CategoryName,T3.EmployeeName as InquiryTaken, "
                      + " T0.InspectionDone as InspectionDone,t4.EmployeeName as SiteResponsible, "
                      + " T0.NextAction as NextAction,T0.DateTime  "
                      + " FROM HayleysPowerEngineeringCRM.dbo.InquiryStatus T0  "
                      + " INNER JOIN HayleysPowerEngineeringCRM.dbo.Inquiry T1  ON T0.InquiryNumber = T1.InquiryNumber "
                      + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
                      + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T3 ON T0.InquiryTaken = T3.Id "
                      + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T4 ON T1.SiteResponsible = T4.Id "
                      + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T5 ON T2.CustomerCategory = T5.Id "
                      + " where T0.Status='Accept' AND T1.SiteResponsible = '" + GetEmpId(InfoPCMS.Views.frmHome.EmpName) + "' ");

                    }

                    gridaccepted.DataSource = Dtgridaccepted;
                    xtraTabControl2.SelectedTabPageIndex = 0;

                    xtraTabPage3.PageEnabled = true;
                    xtraTabPage6.PageEnabled = false;
                    tbPageChangeInqDetails.PageEnabled = false;
                }
                else
                {
                    MessageBox.Show("Selected customer not assign to any category.");
                }
            }
        }

        private void SendMailRequestApproval()
        {
            string strFromEmail = "";
            string strToEmail = "";
            string strCCEmail = "";
            List<string> strToEmails = new List<string>();
            List<string> strCCEmails = new List<string>();


            DataTable dtInquriyDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Inquiry WHERE InquiryNumber = '" + lblInqNo.Text.ToString() + "'");
            DataTable dtInquriyStatusDetails = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryStatus WHERE InquiryNumber = '" + lblInqNo.Text.ToString() + "'");

            DataTable dtInquriyReqDetails = new DatabaseService().executeSelectQuery("SELECT * FROM InqChangeCompDateTemp WHERE InquiryNumber = '" + lblInqNo.Text.ToString() + "'");



            DataTable dtCusDetais = new DatabaseService().executeSelectQuery("SELECT * FROM Customer WHERE Id = '" + dtInquriyReqDetails.Rows[0]["Customer"].ToString() + "'");

            strFromEmail = GetEmail(InfoPCMS.Views.frmHome.EmpName);
            strToEmail = GetEmailL3();

            StringBuilder strBuliderEmailSubject = new StringBuilder();
            strBuliderEmailSubject.Append("Request Approval for Completion Date Change – Customer :" + getCusName(dtInquriyReqDetails.Rows[0]["Customer"].ToString()) + "  " + " -Inquiry No :" + lblInqNo.Text.Trim() + " ");


            StringBuilder strBuliderEmailBody = new StringBuilder();
            strBuliderEmailBody.Append("<head>");
            strBuliderEmailBody.Append("<title>");
            strBuliderEmailBody.Append(Guid.NewGuid().ToString());
            strBuliderEmailBody.Append("</title>");
            strBuliderEmailBody.Append("</head>");
            strBuliderEmailBody.Append("<body>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Dear Sir/Madam,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<b><u><FONT COLOR=DodgerBlue>Request Approval </FONT></u></b> for Completion Date Change,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Inquiry No : </b>" + lblInqNo.Text.Trim() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Type : </b>" + dtInquriyDetails.Rows[0]["ProblemNature"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Customer : </b>" + getCusName(dtInquriyReqDetails.Rows[0]["Customer"].ToString()) + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Location : </b>" + dtInquriyDetails.Rows[0]["Location"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Caller Name : </b>" + dtInquriyDetails.Rows[0]["CallerName"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Caller Phone : </b>" + dtInquriyDetails.Rows[0]["CallerPhone"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Inquiry Details : </b>" + dtInquriyDetails.Rows[0]["FollowupDetails"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Priority : </b>" + dtInquriyDetails.Rows[0]["Priority"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Job Entry Date & Time : </b>" + dtInquriyDetails.Rows[0]["JobEntryDate"].ToString() + "&nbsp;&nbsp;" + dtInquriyDetails.Rows[0]["JobEntryTime"].ToString() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Inquiry Acepted Date & Time : </b>" + dateAccepted.Text.Trim() + "&nbsp;&nbsp;" + timeAccepted.Text.ToString() + "");
            strBuliderEmailBody.Append("<br>");

            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Agreed Completion Date & Time : </b>" + dtInquriyStatusDetails.Rows[0]["dueDate"].ToString() + "&nbsp;&nbsp;" + dtInquriyStatusDetails.Rows[0]["dueTime"].ToString() + "");
            strBuliderEmailBody.Append("<br>");

            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Requested Completion Date & Time : </b>" + dateComp.Text.Trim() + "&nbsp;&nbsp;" + timeComp.Text.ToString() + "");
            strBuliderEmailBody.Append("<br>");

            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Remarks : </b>" + txtRemark.Text.Trim() + "");

            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Requested  By : </b>" + InfoPCMS.Views.frmHome.EmpName + "");
            strBuliderEmailBody.Append("<br>");



            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Regards,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("" + InfoPCMS.Views.frmHome.EmpName + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");

            strBuliderEmailBody.Append("<FONT COLOR=darkred>Auto generated email from CRM system</FONT>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("</body>");
            strBuliderEmailBody.Append("</html>");


            EmailServices Email = new EmailServices();

            Email.SendEmails(strFromEmail.Trim(), strToEmail.Trim(), strBuliderEmailSubject.ToString(), strBuliderEmailBody.ToString());



        }




        private void rdbtnSerInculdeYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnSerInculdeYes.Checked)
            {
                rdbtnSerInculdeNo.Checked = false;
            }
            else
            {
                rdbtnSerInculdeNo.Checked = true;
            }
        }

        private void rdbtnSerInculdeNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnSerInculdeNo.Checked)
            {
                rdbtnSerInculdeYes.Checked = false;
            }
            else
            {
                rdbtnSerInculdeYes.Checked = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            xtraTabControl2.SelectedTabPageIndex = 0;
            xtraTabPage3.PageEnabled = true;
            xtraTabPage6.PageEnabled = false;
            tbPageChangeInqDetails.PageEnabled = false;
        }

        private void btnViewForwarded_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 2;

            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage4.PageEnabled = true;
            xtraTabPage5.PageEnabled = false;
            xtraTabPage7.PageEnabled = false;
            xtraTabPage13.PageEnabled = false;
        }

        private void btnViewCompList_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 3;

            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage4.PageEnabled = false;
            xtraTabPage5.PageEnabled = true;
            xtraTabPage7.PageEnabled = false;
            xtraTabPage13.PageEnabled = false;
        }

        private void btnJobServices_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 6;

            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage4.PageEnabled = false;
            xtraTabPage5.PageEnabled = false;
            xtraTabPage7.PageEnabled = false;

            xtraTabPage13.PageEnabled = true;

            LoadDataJobService();

            DataTable dt3 = new DatabaseService().executeSelectQuery("select * from Employee");
            DataRow row = dt3.NewRow();
            row["EmployeeName"] = "------Please select Employee------";
            dt3.Rows.InsertAt(row, 0);

            cmbBxJobDoneBy.DataSource = dt3;
            cmbBxJobDoneBy.ValueMember = "Id";
            cmbBxJobDoneBy.DisplayMember = "EmployeeName";

            DataTable dt4 = new DatabaseService().executeSelectQuery("select * from Employee");
            DataRow row2 = dt4.NewRow();
            row2["EmployeeName"] = "------Please select Employee------";
            dt4.Rows.InsertAt(row2, 0);
            cmbRepairDoneBy.DataSource = dt4;
            cmbRepairDoneBy.ValueMember = "Id";
            cmbRepairDoneBy.DisplayMember = "EmployeeName";

            DataTable dt5 = new DatabaseService().executeSelectQuery("select * from Employee");
            DataRow row3 = dt5.NewRow();
            row3["EmployeeName"] = "------Please select Employee------";
            dt5.Rows.InsertAt(row3, 0);
            cmbRepairAssign.DataSource = dt5;
            cmbRepairAssign.ValueMember = "Id";
            cmbRepairAssign.DisplayMember = "EmployeeName";

            xtraTabControl4.SelectedTabPageIndex = 0;

            xtraTabPage14.PageEnabled = true;
            xtraTabPage15.PageEnabled = false;
            xtraTabPage16.PageEnabled = false;
            xtraTabPage17.PageEnabled = false;
            xtraTabPage18.PageEnabled = false;
        }

        private void btnBackInqDetails_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 0;

            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage4.PageEnabled = false;
            xtraTabPage5.PageEnabled = false;
            xtraTabPage7.PageEnabled = false;
            xtraTabPage13.PageEnabled = false;
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 0;

            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage4.PageEnabled = false;
            xtraTabPage5.PageEnabled = false;
            xtraTabPage7.PageEnabled = false;
            xtraTabPage13.PageEnabled = false;
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            xtraTabControl2.SelectedTabPageIndex = 0;

            xtraTabPage3.PageEnabled = true;
            xtraTabPage6.PageEnabled = false;
            tbPageChangeInqDetails.PageEnabled = false;
        }

        private void simpleButton8_Click_1(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 0;

            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage4.PageEnabled = false;
            xtraTabPage5.PageEnabled = false;
            xtraTabPage7.PageEnabled = false;
            xtraTabPage13.PageEnabled = false;
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 0;

            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage4.PageEnabled = false;
            xtraTabPage5.PageEnabled = false;
            xtraTabPage7.PageEnabled = false;
            xtraTabPage13.PageEnabled = false;
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 0;

            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage4.PageEnabled = false;
            xtraTabPage5.PageEnabled = false;
            xtraTabPage7.PageEnabled = false;
            xtraTabPage13.PageEnabled = false;
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 1;

            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = true;
            xtraTabPage4.PageEnabled = false;
            xtraTabPage5.PageEnabled = false;
            xtraTabPage7.PageEnabled = false;
            xtraTabPage13.PageEnabled = false;

            xtraTabControl2.SelectedTabPageIndex = 0;
            xtraTabPage3.PageEnabled = true;
            xtraTabPage6.PageEnabled = false;
            tbPageChangeInqDetails.PageEnabled = false;
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (View.GetRowCellDisplayText(e.RowHandle, View.Columns["CustomerCategory"]) == "Diamond")
                {
                    e.Appearance.BackColor = Color.Tomato;
                    // e.Appearance.BackColor2 = Color.SeaShell;
                }


            }
        }

        private void gridView2_RowStyle(object sender, RowStyleEventArgs e)
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

        private void btnUpdateMyTask_Click(object sender, EventArgs e)
        {
            DataTable dtInqChangeComp = new DatabaseService().executeSelectQuery("SELECT * FROM InqChangeCompDateTemp WHERE InquiryNumber='" + gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim() + "' ");

            string StatusReqChange = "";


            if (dtInqChangeComp.Rows.Count.ToString() == "0")
            {
                StatusReqChange = "";
            }
            else
            {
                StatusReqChange = dtInqChangeComp.Rows[0]["Status"].ToString().Trim();

            }


            if (StatusReqChange == "Pending Approval")
            {
                XtraMessageBox.Show("Pending Supervisor Approval for Completed Date Change", "Error");
            }
            else
            {
                if (!(InfoPCMS.Views.frmHome.EmpCat == "LEVEL 3"))
                {
                    lblInquiryNumber.Text = gridView2.GetFocusedDataRow()["InquiryNumber"].ToString();
                    lblInquiryType.Text = GetInquiryType(gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim());
                    txtAtcualProbUpdate.Text = GetInquiryActProb(gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim());



                    DataTable dtSelInq = new DatabaseService().executeSelectQuery("SELECT T0.*,T1.actual FROM HayleysPowerEngineeringCRM.dbo.Inquiry T0 "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.InquiryStatus T1 ON T0.InquiryNumber = T1.InquiryNumber "
                    + "WHERE T0.InquiryNumber = '" + gridView2.GetFocusedDataRow()["InquiryNumber"].ToString() + "' ");

                    lblSelCus.Text = getCusName(dtSelInq.Rows[0]["CustomerId"].ToString().Trim()).Trim();

                    txtActualProblem.Text = dtSelInq.Rows[0]["actual"].ToString().Trim();

                    if (GetInquiryType(gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim()) == "Repairs")
                    {
                        pnlSerInclude.Visible = true;

                        rdbtnSerInculdeNo.Checked = true;
                        rdbtnSerInculdeYes.Checked = false;

                        DataTable dtJobStart = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryStatusDetails WHERE InquiryNumber='" + gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim() + "' ");

                        if (dtJobStart.Rows.Count > 0)
                        {
                            pnlSerInclude.Enabled = false;

                            DataTable dtinqStatusIsSerIn = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryStatus WHERE InquiryNumber='" + gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim() + "' ");

                            string isSerInclude = dtinqStatusIsSerIn.Rows[0]["IsServiceInclude"].ToString().Trim();

                            if (isSerInclude == "True")
                            {
                                rdbtnSerInculdeYes.Checked = true;
                                //rdbtnSerInculdeNo.Checked = false;
                            }
                            else
                            {
                                rdbtnSerInculdeYes.Checked = false;
                                //rdbtnSerInculdeNo.Checked = true;
                            }

                        }


                    }
                    else
                    {
                        pnlSerInclude.Visible = false;
                    }


                    xtraTabControl2.SelectedTabPageIndex = 1;
                    xtraTabPage3.PageEnabled = false;
                    xtraTabPage6.PageEnabled = true;
                    tbPageChangeInqDetails.PageEnabled = false;
                    ///pnlRecRepairs.Enabled = false;

                    this.SetSubActionTypes();


                    DataTable Completed = new DatabaseService().executeSelectQuery("SELECT  InquiryNumber, ActionType,SubActionType,ActionDetails,ActionDate, Remarks, JobCompleted,Percentage FROM InquiryStatusDetails where InquiryNumber='" + lblInquiryNumber.Text + "'");
                    tblupdate.DataSource = Completed;
                    // }
                }
                else
                {

                    lblInquiryNumber.Text = gridView2.GetFocusedDataRow()["InquiryNumber"].ToString();

                    lblInquiryType.Text = GetInquiryType(gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim());

                    txtAtcualProbUpdate.Text = GetInquiryActProb(gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim());


                    xtraTabControl2.SelectedTabPageIndex = 1;
                    xtraTabPage3.PageEnabled = false;
                    xtraTabPage6.PageEnabled = true;
                    tbPageChangeInqDetails.PageEnabled = false;
                    //pnlRecRepairs.Enabled = false;
                    this.SetSubActionTypes();

                    // this.SetNextActionTypes();


                    //  cboActionType.Focus();
                    //cboActionType.SelectedIndex = -1;
                    //DataTable Completed = new DatabaseService().executeSelectQuery("SELECT  InquiryNumber, ActionType, ActionDetails,ActionDate, Remarks, JobCompleted,Percentage FROM InquiryStatusDetails where InquiryNumber='" + lblInquiryNumber.Text + "'");
                    //tblupdate.DataSource = Completed;


                    DataTable Completed = new DatabaseService().executeSelectQuery("SELECT  InquiryNumber, ActionType,SubActionType,ActionDetails,ActionDate, Remarks, JobCompleted,Percentage FROM InquiryStatusDetails where InquiryNumber='" + lblInquiryNumber.Text + "'");
                    tblupdate.DataSource = Completed;
                }


            }

        }


        //-----------------------------------------Job Services---------------------------------

        private void LoadDataJobService()
        {
            try
            {
                if (InfoPCMS.Views.frmHome.EmpCat == "LEVEL 3")
                {

                    DataTable UpComming = new DatabaseService().executeSelectQuery("SELECT T0.*, "
                        + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.ResponsiblePerson) AS ResponsiblePersonName, "
                        + " (SELECT CustomerName FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer) AS CustomerName, "
                        + " (SELECT SubLocation FROM HayleysPowerEngineeringCRM.dbo.CustomerLocation  WHERE Id = T0.Location) AS SubLocation "
                        + " FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 where T0.Status='Up Comming' AND T0.PlanDate BETWEEN GETDATE() AND DATEADD(day,90, GETDATE())  ORDER BY PlanDate");

                    gridUpComming.DataSource = UpComming;

                    DataTable Due = new DatabaseService().executeSelectQuery("SELECT T0.*, "
                        + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.ResponsiblePerson) AS ResponsiblePersonName, "
                        + " (SELECT CustomerName FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer) AS CustomerName, "
                        + " (SELECT SubLocation FROM HayleysPowerEngineeringCRM.dbo.CustomerLocation  WHERE Id = T0.Location) AS SubLocation "
                        + " FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 where T0.Status <> 'Completed' AND T0.PlanDate < GETDATE()  ORDER BY PlanDate");

                    gridDueServices.DataSource = Due;


                    DataTable CompletedServices = new DatabaseService().executeSelectQuery("SELECT T0.*, "
                     + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.ResponsiblePerson) AS ResponsiblePersonName, "
                     + " (SELECT CustomerName FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer) AS CustomerName, "
                     + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.JobDoneBy) AS JobDoneByName, "
                     + " (SELECT SubLocation FROM HayleysPowerEngineeringCRM.dbo.CustomerLocation  WHERE Id = T0.Location) AS SubLocation "
                     + " FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 where T0.Status='Completed'  ORDER BY PlanDate");
                    grdCompServices.DataSource = CompletedServices;


                    DataTable RecReparis = new DatabaseService().executeSelectQuery(" SELECT T0.*,T1.EmployeeName,T2.CustomerName,T3.SubLocation,T4.CategoryName FROM HayleysPowerEngineeringCRM.dbo.RecommendedRepairs T0 "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 ON T0.ResponsiblePerson = T1.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T3 ON T0.Location = T3.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T4 ON T0.CustomerCategory = T4.Id "
                    + " WHERE T0.Status <> 'Complete'ORDER BY T0.ExpectedDate ");
                    grdRecReparis.DataSource = RecReparis;


                    DataTable CompRecReparis = new DatabaseService().executeSelectQuery(" SELECT T0.*,T1.EmployeeName,T2.CustomerName,T3.SubLocation,T4.CategoryName,T5.EmployeeName As JobDoneByName FROM HayleysPowerEngineeringCRM.dbo.RecommendedRepairs T0 "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 ON T0.ResponsiblePerson = T1.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T3 ON T0.Location = T3.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T4 ON T0.CustomerCategory = T4.Id "
                     + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T5 ON T0.JobDoneBy = T1.Id "
                    + " WHERE T0.Status = 'Complete'  ORDER BY T0.ExpectedDate");
                    grdCompRecRep.DataSource = CompRecReparis;

                }
                else
                {

                    DataTable dtEmployee = new DatabaseService().executeSelectQuery("select Id from Employee  WHERE EmployeeName ='" + InfoPCMS.Views.frmHome.EmpName + "'");
                    if (dtEmployee.Rows.Count > 0)
                    {

                        DateTime todate = DateTime.Now.AddDays(14);


                        String EmpId = dtEmployee.Rows[0]["Id"].ToString();

                        DataTable UpComming = new DatabaseService().executeSelectQuery("SELECT T0.*, "
                        + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.ResponsiblePerson) AS ResponsiblePersonName, "
                        + " (SELECT CustomerName FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer) AS CustomerName, "
                        + " (SELECT SubLocation FROM HayleysPowerEngineeringCRM.dbo.CustomerLocation  WHERE Id = T0.Location) AS SubLocation "
                        + " FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 where T0.Status='Up Comming' AND T0.ResponsiblePerson='" + EmpId + "' AND T0.PlanDate BETWEEN GETDATE() AND DATEADD(day,90, GETDATE()) ORDER BY PlanDate");

                        gridUpComming.DataSource = UpComming;

                        DataTable Due = new DatabaseService().executeSelectQuery("SELECT T0.*, "
                         + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.ResponsiblePerson) AS ResponsiblePersonName, "
                         + " (SELECT CustomerName FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer) AS CustomerName, "
                         + " (SELECT SubLocation FROM HayleysPowerEngineeringCRM.dbo.CustomerLocation  WHERE Id = T0.Location) AS SubLocation "
                         + " FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 where T0.Status <> 'Completed' AND T0.PlanDate < GETDATE() AND T0.ResponsiblePerson='" + EmpId + "'  ORDER BY PlanDate ");

                        gridDueServices.DataSource = Due;

                        DataTable CompletedServices = new DatabaseService().executeSelectQuery("SELECT T0.*, "
                   + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.ResponsiblePerson) AS ResponsiblePersonName, "
                   + " (SELECT CustomerName FROM HayleysPowerEngineeringCRM.dbo.Customer WHERE Id = T0.Customer) AS CustomerName, "
                   + " (SELECT EmployeeName FROM HayleysPowerEngineeringCRM.dbo.Employee WHERE Id = T0.JobDoneBy) AS JobDoneByName, "
                   + " (SELECT SubLocation FROM HayleysPowerEngineeringCRM.dbo.CustomerLocation  WHERE Id = T0.Location) AS SubLocation "
                   + " FROM HayleysPowerEngineeringCRM.dbo.ServicesShedule T0 where T0.Status='Completed' AND T0.ResponsiblePerson='" + EmpId + "' ORDER BY PlanDate");



                        grdCompServices.DataSource = CompletedServices;


                        DataTable RecReparis = new DatabaseService().executeSelectQuery(" SELECT T0.*,T1.EmployeeName,T2.CustomerName,T3.SubLocation,T4.CategoryName FROM HayleysPowerEngineeringCRM.dbo.RecommendedRepairs T0 "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 ON T0.ResponsiblePerson = T1.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T3 ON T0.Location = T3.Id "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T4 ON T0.CustomerCategory = T4.Id "
                    + " WHERE T0.Status <> 'Complete' AND T0.ResponsiblePerson ='" + EmpId + "'  ORDER BY T0.ExpectedDate");
                        grdRecReparis.DataSource = RecReparis;

                        DataTable CompRecReparis = new DatabaseService().executeSelectQuery(" SELECT T0.*,T1.EmployeeName,T2.CustomerName,T3.SubLocation,T4.CategoryName,T5.EmployeeName As JobDoneByName FROM HayleysPowerEngineeringCRM.dbo.RecommendedRepairs T0 "
                 + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 ON T0.ResponsiblePerson = T1.Id "
                 + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
                 + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T3 ON T0.Location = T3.Id "
                 + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T4 ON T0.CustomerCategory = T4.Id "
                  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T5 ON T0.JobDoneBy = T1.Id "
                 + " WHERE T0.Status = 'Complete' AND T0.ResponsiblePerson ='" + EmpId + "'  ORDER BY T0.ExpectedDate");
                        grdCompRecRep.DataSource = CompRecReparis;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        //private string GetEmpId(string strEmpName)
        //{
        //    string strEmpId = "";
        //    DataTable dtEmployee = new DatabaseService().executeSelectQuery("select * from Employee  WHERE EmployeeName = '" + strEmpName + "'");


        //    if (dtEmployee.Rows.Count > 0)
        //    {

        //        strEmpId = dtEmployee.Rows[0]["Id"].ToString();
        //    }

        //    return strEmpId;
        //}

        //public UploadedFile CurrentFile
        //{
        //    get { return helper == null ? null : helper.CurrentFile; }
        //}

        private void gridVwUpcomming_DoubleClick(object sender, EventArgs e)
        {
            ClearUpdateSer();

            lblAgreementID.Text = gridVwUpcomming.GetFocusedDataRow()["AgreementID"].ToString();
            lblGeneratorNumber.Text = gridVwUpcomming.GetFocusedDataRow()["GeneratorId"].ToString();
            lblServiceName.Text = gridVwUpcomming.GetFocusedDataRow()["Type"].ToString();
            xtraTabControl4.SelectedTabPageIndex = 1;

            xtraTabPage14.PageEnabled = false;
            xtraTabPage15.PageEnabled = true;
            xtraTabPage16.PageEnabled = false;
            xtraTabPage17.PageEnabled = false;
            xtraTabPage18.PageEnabled = false;

            DataTable Completed = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule WHERE  AgreementID = '" + lblAgreementID.Text.Trim() + "' AND GeneratorId = '" + lblGeneratorNumber.Text.Trim() + "'");
            gridServiceActions.DataSource = Completed;
        }

        private void gridVwDueSer_DoubleClick(object sender, EventArgs e)
        {
            ClearUpdateSer();

            lblAgreementID.Text = gridVwDueSer.GetFocusedDataRow()["AgreementID"].ToString();
            lblGeneratorNumber.Text = gridVwDueSer.GetFocusedDataRow()["GeneratorId"].ToString();
            lblServiceName.Text = gridVwDueSer.GetFocusedDataRow()["Type"].ToString();

            xtraTabControl4.SelectedTabPageIndex = 1;

            xtraTabPage14.PageEnabled = false;
            xtraTabPage15.PageEnabled = true;
            xtraTabPage16.PageEnabled = false;
            xtraTabPage17.PageEnabled = false;
            xtraTabPage18.PageEnabled = false;

            DataTable Completed = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule WHERE  AgreementID = '" + lblAgreementID.Text.Trim() + "' AND GeneratorId = '" + lblGeneratorNumber.Text.Trim() + "'");
            gridServiceActions.DataSource = Completed;
        }

        private void gridDueServices_Click(object sender, EventArgs e)
        {

        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtJobSheet.Text = openFileDialog.FileName;
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            // Get file name.
            string name = saveFileDialog1.FileName;

            txtJobSheet.Text = name;
            // Write to the file name selected.
            // ... You can write the text from a TextBox instead of a string literal.
            File.WriteAllText(name, "test");
        }



        private void btnJobComplete_Click(object sender, EventArgs e)
        {
            DataTable SerAgreement = new DatabaseService().executeSelectQuery("SELECT * FROM SeviceAgreement WHERE  AgreementId = '" + lblAgreementID.Text.Trim() + "' AND GeneratorId = '" + lblGeneratorNumber.Text.Trim() + "'");

            string result = lblServiceName.Text.ToString().Substring(0, 3);

            int PendingSer = Convert.ToInt32(SerAgreement.Rows[0]["PendingService"].ToString());
            int PendingIns = Convert.ToInt32(SerAgreement.Rows[0]["PendingInspection"].ToString());

            if (result == "Ser")
            {
                PendingSer = PendingSer - 1;
            }
            else if (result == "Ins")
            {
                PendingIns = PendingIns - 1;
            }

            DataTable checkJobDone = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule WHERE  AgreementID = '" + lblAgreementID.Text.Trim() + "' AND Type = '" + lblServiceName.Text.Trim() + "' AND GeneratorId = '" + lblGeneratorNumber.Text.Trim() + "'");

            DataTable dtSerAgreement = new DatabaseService().executeSelectQuery("SELECT * FROM SeviceAgreement WHERE  AgreementID = '" + lblAgreementID.Text.Trim() + "'");

            DataTable dtGenDetails = new DatabaseService().executeSelectQuery("SELECT * FROM GeneratorDetails WHERE GeneratorNumber ='" + lblGeneratorNumber.Text.Trim() + "' AND Customer = '" + dtSerAgreement.Rows[0]["Customer"].ToString().Trim() + "' AND location = '" + dtSerAgreement.Rows[0]["Location"].ToString().Trim() + "'");

            DataTable dtCusCatDetails = new DatabaseService().executeSelectQuery("SELECT * FROM Customer WHERE Id = '" + dtSerAgreement.Rows[0]["Customer"].ToString().Trim() + "' ");


            if (checkJobDone.Rows.Count.ToString() == "0")
            {
                MessageBox.Show("No Relavent Services Shedule for Generator.");
            }
            else
            {
                if (checkJobDone.Rows[0]["Status"].ToString() == "Completed")
                {
                    MessageBox.Show("Services's Status Already Completed .");
                }
                else
                {

                    new DatabaseService().executeUpdateQuery("UPDATE ServicesShedule SET Status='Completed', JobDoneDate='" + dtJobDonedate.EditValue.ToString() + "',JobDoneBy = '" + GetEmpId(cmbBxJobDoneBy.Text.Trim()) + "',JobDetails = '" + txtJobDetails.Text.Trim() + "',JobSheetPath = '" + txtJobSheet.Text.Trim() + "' WHERE AgreementID = '" + lblAgreementID.Text.Trim() + "' AND Type = '" + lblServiceName.Text.Trim() + "' AND GeneratorId = '" + lblGeneratorNumber.Text.Trim() + "'");
                    new DatabaseService().executeUpdateQuery("UPDATE SeviceAgreement SET PendingService= '" + PendingSer.ToString() + "' , PendingInspection= '" + PendingIns.ToString() + "' WHERE AgreementId = '" + lblAgreementID.Text.Trim() + "' AND GeneratorId = '" + lblGeneratorNumber.Text.Trim() + "'");

                    if (PendingSer.ToString() == "0" && PendingIns.ToString() == "0")
                    {
                        new DatabaseService().executeUpdateQuery("UPDATE SeviceAgreement SET Status = 'Completed' , PendingInspection= '" + PendingIns.ToString() + "' WHERE AgreementId = '" + lblAgreementID.Text.Trim() + "' AND GeneratorId = '" + lblGeneratorNumber.Text.Trim() + "'");

                    }

                    string strRepAsign = "";

                    if (cmbRepairAssign.SelectedIndex == 0)
                    {
                        strRepAsign = dtGenDetails.Rows[0]["ExecutiveResponsible"].ToString().Trim();
                    }
                    else
                    {
                        strRepAsign = GetEmpId(cmbRepairAssign.Text.Trim());
                    }

                    string strJobDoneBy = "";
                    if (cmbBxJobDoneBy.SelectedIndex == 0)
                    {
                        strJobDoneBy = GetEmpId(InfoPCMS.Views.frmHome.EmpName); ;
                    }
                    else
                    {
                        strJobDoneBy = GetEmpId(cmbBxJobDoneBy.Text.Trim());
                    }


                    new DatabaseService().executeUpdateQuery("INSERT INTO RecommendedRepairs VALUES ('" + lblAgreementID.Text.Trim() + "','" + lblGeneratorNumber.Text.Trim() + "','" + dtSerAgreement.Rows[0]["Customer"].ToString().Trim() + "','" + dtCusCatDetails.Rows[0]["CustomerCategory"].ToString().Trim() + "','" + dtSerAgreement.Rows[0]["Location"].ToString().Trim() + "',"
                        + "'" + lblServiceName.Text.Trim() + "','" + strJobDoneBy.Trim() + "','" + dtJobDonedate.EditValue.ToString() + "','" + txtRecRepairs.Text.ToString() + "','" + dtExpectedRecRepairs.EditValue.ToString() + "',"
                        + "'" + strRepAsign + "','false','','','','Pending')");

                    DataTable RecommendedRepairs = new DatabaseService().executeSelectQuery("SELECT * FROM RecommendedRepairs WHERE  AgreementID = '" + lblAgreementID.Text.Trim() + "' AND GeneratorId = '" + lblGeneratorNumber.Text.Trim() + "' AND RelatedService ='" + lblServiceName.Text.Trim() + "' ");

                    if (dtCusCatDetails.Rows[0]["CustomerCategory"].ToString().Trim() == "1")
                    {
                        //new DatabaseService().executeUpdateQuery("INSERT INTO RecRepairsNotiDiamond VALUES ('" + RecommendedRepairs.Rows[0]["Id"].ToString() + "','" + lblAgreementID.Text.Trim() + "','" + lblGeneratorNumber.Text.Trim() + "','" + dtSerAgreement.Rows[0]["Customer"].ToString().Trim() + "','" + dtSerAgreement.Rows[0]["Location"].ToString().Trim() + "',"
                        //    + "'" + lblServiceName.Text.Trim() + "','" + strJobDoneBy.Trim() + "','" + dtJobDonedate.EditValue.ToString() + "','" + txtRecRepairs.Text.ToString() + "','" + dtExpectedRecRepairs.EditValue.ToString() + "',"
                        //    + "'" + strRepAsign + "','false','','','','Pending')");

                    }
                    else
                    {

                        new DatabaseService().executeUpdateQuery("INSERT INTO RecRepairsNotification VALUES ('" + RecommendedRepairs.Rows[0]["Id"].ToString() + "','" + lblAgreementID.Text.Trim() + "','" + lblGeneratorNumber.Text.Trim() + "','" + dtSerAgreement.Rows[0]["Customer"].ToString().Trim() + "','" + dtSerAgreement.Rows[0]["Location"].ToString().Trim() + "',"
                            + "'" + lblServiceName.Text.Trim() + "','" + strJobDoneBy.Trim() + "','" + dtJobDonedate.EditValue.ToString() + "','" + txtRecRepairs.Text.ToString() + "','" + dtExpectedRecRepairs.EditValue.ToString() + "',"
                            + "'" + strRepAsign + "','false','','','','Pending')");
                    }

                    MessageBox.Show("Services's Status Successfuly Updated.");

                    LoadDataJobService();
                }
            }

            DataTable Completed = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule WHERE  AgreementID = '" + lblAgreementID.Text.Trim() + "' AND GeneratorId = '" + lblGeneratorNumber.Text.Trim() + "'");
            gridServiceActions.DataSource = Completed;

            LoadDataJobService();

            xtraTabControl4.SelectedTabPageIndex = 0;

            xtraTabPage14.PageEnabled = true;
            xtraTabPage15.PageEnabled = false;
            xtraTabPage16.PageEnabled = false;
            xtraTabPage17.PageEnabled = false;
            xtraTabPage18.PageEnabled = false;

        }

        private void btnCompService_Click(object sender, EventArgs e)
        {
            ClearUpdateSer();

            if (gridVwUpcomming.SelectedRowsCount > 0)
            {
                lblAgreementID.Text = gridVwUpcomming.GetFocusedDataRow()["AgreementID"].ToString().Trim();
                lblGeneratorNumber.Text = gridVwUpcomming.GetFocusedDataRow()["GeneratorId"].ToString().Trim();
                lblServiceName.Text = gridVwUpcomming.GetFocusedDataRow()["Type"].ToString().Trim();

                DataTable Completed = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule WHERE  AgreementID = '" + gridVwUpcomming.GetFocusedDataRow()["AgreementID"].ToString().Trim() + "' AND GeneratorId = '" + gridVwUpcomming.GetFocusedDataRow()["GeneratorId"].ToString().Trim() + "'");
                gridServiceActions.DataSource = Completed;
                xtraTabControl4.SelectedTabPageIndex = 1;
                xtraTabPage14.PageEnabled = false;
                xtraTabPage15.PageEnabled = true;
                xtraTabPage16.PageEnabled = false;
                xtraTabPage17.PageEnabled = false;
                xtraTabPage18.PageEnabled = false;

            }
            else if (gridVwDueSer.SelectedRowsCount > 0)
            {
                lblAgreementID.Text = gridVwDueSer.GetFocusedDataRow()["AgreementID"].ToString().Trim();
                lblGeneratorNumber.Text = gridVwDueSer.GetFocusedDataRow()["GeneratorId"].ToString().Trim();
                lblServiceName.Text = gridVwDueSer.GetFocusedDataRow()["Type"].ToString().Trim();

                DataTable Completed = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule WHERE  AgreementID = '" + gridVwDueSer.GetFocusedDataRow()["AgreementID"].ToString().Trim() + "' AND GeneratorId = '" + gridVwDueSer.GetFocusedDataRow()["GeneratorId"].ToString().Trim() + "'");
                gridServiceActions.DataSource = Completed;
                xtraTabControl4.SelectedTabPageIndex = 1;

                xtraTabPage14.PageEnabled = false;
                xtraTabPage15.PageEnabled = true;
                xtraTabPage16.PageEnabled = false;
                xtraTabPage17.PageEnabled = false;
                xtraTabPage18.PageEnabled = false;

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //frmViewJobSheet viewSheet = new frmViewJobSheet();
            //viewSheet.AgreementID = lblAgreementID.Text;
            //viewSheet.GeneratorID = lblGeneratorNumber.Text;
            //viewSheet.ServiceType = lblServiceName.Text;
            //viewSheet.Show();

        }

        private void grdVwCompletedServices_DoubleClick(object sender, EventArgs e)
        {
            lblCompAgreeID.Text = grdVwCompletedServices.GetFocusedDataRow()["AgreementID"].ToString();
            lblCompGenNo.Text = grdVwCompletedServices.GetFocusedDataRow()["GeneratorId"].ToString();
            lblCompSerName.Text = grdVwCompletedServices.GetFocusedDataRow()["Type"].ToString();


            DataTable CompletedSer = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule WHERE  AgreementID = '" + lblCompAgreeID.Text.Trim() + "' AND GeneratorId = '" + lblCompGenNo.Text.Trim() + "' AND Type = '" + lblCompSerName.Text.Trim() + "' ");

            lblCompJobDoneBy.Text = CompletedSer.Rows[0]["JobDoneBy"].ToString();
            dtCompJobDoneDate.Text = CompletedSer.Rows[0]["JobDoneDate"].ToString();
            txtCompJobDetails.Text = CompletedSer.Rows[0]["JobDetails"].ToString();
            txtCompJobSheet.Text = CompletedSer.Rows[0]["JobSheetPath"].ToString();

            xtraTabControl4.SelectedTabPageIndex = 3;

            xtraTabPage14.PageEnabled = false;
            xtraTabPage15.PageEnabled = false;
            xtraTabPage16.PageEnabled = false;
            xtraTabPage17.PageEnabled = true;
            xtraTabPage18.PageEnabled = false;
        }

        private void btnViewJobSheet_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(txtCompJobSheet.Text);
        }

        private void xtraTabPage10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridServiceActions_Click(object sender, EventArgs e)
        {

        }

        private void btnViewServices_Click(object sender, EventArgs e)
        {
            lblCompAgreeID.Text = grdVwCompletedServices.GetFocusedDataRow()["AgreementID"].ToString();
            lblCompGenNo.Text = grdVwCompletedServices.GetFocusedDataRow()["GeneratorId"].ToString();
            lblCompSerName.Text = grdVwCompletedServices.GetFocusedDataRow()["Type"].ToString();


            DataTable CompletedSer = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule WHERE  AgreementID = '" + lblCompAgreeID.Text.Trim() + "' AND GeneratorId = '" + lblCompGenNo.Text.Trim() + "' AND Type = '" + lblCompSerName.Text.Trim() + "' ");

            lblCompJobDoneBy.Text = CompletedSer.Rows[0]["JobDoneBy"].ToString();
            dtCompJobDoneDate.Text = CompletedSer.Rows[0]["JobDoneDate"].ToString();
            txtCompJobDetails.Text = CompletedSer.Rows[0]["JobDetails"].ToString();
            txtCompJobSheet.Text = CompletedSer.Rows[0]["JobSheetPath"].ToString();

            xtraTabControl4.SelectedTabPageIndex = 3;

            xtraTabPage14.PageEnabled = false;
            xtraTabPage15.PageEnabled = false;
            xtraTabPage16.PageEnabled = false;
            xtraTabPage17.PageEnabled = true;
            xtraTabPage18.PageEnabled = false;
        }

        private void ClearUpdateSer()
        {

            cmbBxJobDoneBy.SelectedIndex = 0;
            dtJobDonedate.Text = "";
            txtJobDetails.Text = "";
            txtJobSheet.Text = "";
            txtRecRepairs.Text = "";
            dtExpectedRecRepairs.Text = "";
            cmbRepairAssign.SelectedIndex = 0;

        }

        private void ClearRecRep()
        {

            cmbRepairDoneBy.SelectedIndex = 0;
            dtRepairDoneDate.Text = "";
            txtRepairDetails.Text = "";
            lblRepAgreeId.Text = "";
            lblRepLocName.Text = "";
            lblRepCusName.Text = "";
            lblRepRelatedServ.Text = "";
            lblRepGenNo.Text = "";

        }

        private void btnViewCompSer_Click(object sender, EventArgs e)
        {
            xtraTabControl4.SelectedTabPageIndex = 2;

            xtraTabPage14.PageEnabled = false;
            xtraTabPage15.PageEnabled = false;
            xtraTabPage16.PageEnabled = true;
            xtraTabPage17.PageEnabled = false;
            xtraTabPage18.PageEnabled = false;
        }

        private void btnBackCompSerDeat_Click(object sender, EventArgs e)
        {
            xtraTabControl4.SelectedTabPageIndex = 2;

            xtraTabPage14.PageEnabled = false;
            xtraTabPage15.PageEnabled = false;
            xtraTabPage16.PageEnabled = true;
            xtraTabPage17.PageEnabled = false;
            xtraTabPage18.PageEnabled = false;
        }

        private void btnBackCompList_Click(object sender, EventArgs e)
        {
            xtraTabControl4.SelectedTabPageIndex = 0;

            xtraTabPage14.PageEnabled = true;
            xtraTabPage15.PageEnabled = false;
            xtraTabPage16.PageEnabled = false;
            xtraTabPage17.PageEnabled = false;
            xtraTabPage18.PageEnabled = false;
        }

        private void btnBackUpdateSer_Click(object sender, EventArgs e)
        {
            xtraTabControl4.SelectedTabPageIndex = 0;

            xtraTabPage14.PageEnabled = true;
            xtraTabPage15.PageEnabled = false;
            xtraTabPage16.PageEnabled = false;
            xtraTabPage17.PageEnabled = false;
            xtraTabPage18.PageEnabled = false;
        }

        private void btnRepComplete_Click(object sender, EventArgs e)
        {
            string strRepDoneBy = "";
            if (cmbRepairDoneBy.SelectedIndex == 0)
            {
                strRepDoneBy = GetEmpId(InfoPCMS.Views.frmHome.EmpName); ;
            }
            else
            {
                strRepDoneBy = GetEmpId(cmbRepairDoneBy.Text.Trim());
            }




            new DatabaseService().executeUpdateQuery("UPDATE RecommendedRepairs SET IsRepairDone = 'True',JobDoneDate='" + dtRepairDoneDate.EditValue.ToString() + "', "
              + " JobDoneBy='" + strRepDoneBy.Trim() + "',JobDetails='" + txtRepairDetails.Text.Trim() + "',Status='Complete' "
              + " WHERE AgreementID = '" + lblRepAgreeId.Text.Trim() + "' AND RelatedService = '" + lblRepRelatedServ.Text.Trim() + "' AND GeneratorID='" + lblRepGenNo.Text.Trim() + "' AND Customer='" + lblRepCusId.Text.Trim() + "' AND Location='" + lblRepLocId.Text.Trim() + "'");

            ClearRecRep();
            MessageBox.Show("Recomanded Repair's Status Successfuly Updated.");

            xtraTabControl4.SelectedTabPageIndex = 0;

            xtraTabPage14.PageEnabled = true;
            xtraTabPage15.PageEnabled = false;
            xtraTabPage16.PageEnabled = false;
            xtraTabPage17.PageEnabled = false;
            xtraTabPage18.PageEnabled = false;

            LoadDataJobService();
        }

        private void btnRepBack_Click(object sender, EventArgs e)
        {
            xtraTabControl4.SelectedTabPageIndex = 0;

            xtraTabPage14.PageEnabled = true;
            xtraTabPage15.PageEnabled = false;
            xtraTabPage16.PageEnabled = false;
            xtraTabPage17.PageEnabled = false;
            xtraTabPage18.PageEnabled = false;
        }

        private void btnRecRepStart_Click(object sender, EventArgs e)
        {
            ClearRecRep();

            lblRepAgreeId.Text = grdVwRecRep.GetFocusedDataRow()["AgreementID"].ToString();
            lblRepGenNo.Text = grdVwRecRep.GetFocusedDataRow()["GeneratorID"].ToString();

            DataTable AgreeDetails = new DatabaseService().executeSelectQuery("SELECT * FROM SeviceAgreement WHERE  AgreementId = '" + grdVwRecRep.GetFocusedDataRow()["AgreementID"].ToString() + "' ");

            lblRepCusId.Text = AgreeDetails.Rows[0]["Customer"].ToString().Trim();
            lblRepCusName.Text = grdVwRecRep.GetFocusedDataRow()["CustomerName"].ToString().Trim();
            lblRepLocId.Text = AgreeDetails.Rows[0]["Location"].ToString().Trim();
            lblRepLocName.Text = grdVwRecRep.GetFocusedDataRow()["SubLocation"].ToString().Trim();
            lblRepRelatedServ.Text = grdVwRecRep.GetFocusedDataRow()["RelatedService"].ToString().Trim();
            txtRepDetails.Text = grdVwRecRep.GetFocusedDataRow()["Details"].ToString().Trim();

            xtraTabControl4.SelectedTabPageIndex = 4;

            xtraTabPage14.PageEnabled = false;
            xtraTabPage15.PageEnabled = false;
            xtraTabPage16.PageEnabled = false;
            xtraTabPage17.PageEnabled = false;
            xtraTabPage18.PageEnabled = true;

            DataTable Completed = new DatabaseService().executeSelectQuery("SELECT T0.*,T1.EmployeeName,T2.CustomerName,T3.SubLocation,T4.CategoryName,T5.EmployeeName As JobDoneByName FROM HayleysPowerEngineeringCRM.dbo.RecommendedRepairs T0 "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 ON T0.ResponsiblePerson = T1.Id "
   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T5 ON T0.JobDoneBy = T5.Id "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T3 ON T0.Location = T3.Id "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T4 ON T0.CustomerCategory = T4.Id WHERE  T0.GeneratorID = '" + lblRepGenNo.Text.Trim() + "' AND T0.Customer='" + lblRepCusId.Text.Trim() + "' AND T0.Location ='" + lblRepLocId.Text.Trim() + "' AND T0.AgreementID = '" + lblRepAgreeId.Text.Trim() + "'");
            grdRecmonedReparis.DataSource = Completed;
        }



        private void grdVwRecRep_DoubleClick(object sender, EventArgs e)
        {
            ClearRecRep();

            lblRepAgreeId.Text = grdVwRecRep.GetFocusedDataRow()["AgreementID"].ToString();
            lblRepGenNo.Text = grdVwRecRep.GetFocusedDataRow()["GeneratorID"].ToString();

            DataTable AgreeDetails = new DatabaseService().executeSelectQuery("SELECT * FROM SeviceAgreement WHERE  AgreementId = '" + grdVwRecRep.GetFocusedDataRow()["AgreementID"].ToString() + "' ");

            lblRepCusId.Text = AgreeDetails.Rows[0]["Customer"].ToString().Trim();
            lblRepCusName.Text = grdVwRecRep.GetFocusedDataRow()["CustomerName"].ToString().Trim();
            lblRepLocId.Text = AgreeDetails.Rows[0]["Location"].ToString().Trim();
            lblRepLocName.Text = grdVwRecRep.GetFocusedDataRow()["SubLocation"].ToString().Trim();
            lblRepRelatedServ.Text = grdVwRecRep.GetFocusedDataRow()["RelatedService"].ToString().Trim();
            txtRepDetails.Text = grdVwRecRep.GetFocusedDataRow()["Details"].ToString().Trim();

            xtraTabControl4.SelectedTabPageIndex = 4;

            xtraTabPage14.PageEnabled = false;
            xtraTabPage15.PageEnabled = false;
            xtraTabPage16.PageEnabled = false;
            xtraTabPage17.PageEnabled = false;
            xtraTabPage18.PageEnabled = true;

            DataTable Completed = new DatabaseService().executeSelectQuery("SELECT T0.*,T1.EmployeeName,T2.CustomerName,T3.SubLocation,T4.CategoryName,T5.EmployeeName As JobDoneByName FROM HayleysPowerEngineeringCRM.dbo.RecommendedRepairs T0 "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 ON T0.ResponsiblePerson = T1.Id "
   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T5 ON T0.JobDoneBy = T5.Id "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T3 ON T0.Location = T3.Id "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T4 ON T0.CustomerCategory = T4.Id WHERE  T0.GeneratorID = '" + lblRepGenNo.Text.Trim() + "' AND T0.Customer='" + lblRepCusId.Text.Trim() + "' AND T0.Location ='" + lblRepLocId.Text.Trim() + "' AND T0.AgreementID = '" + lblRepAgreeId.Text.Trim() + "'");
            grdRecmonedReparis.DataSource = Completed;
        }

        private void simpleButton12_Click_1(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 0;

            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage4.PageEnabled = false;
            xtraTabPage5.PageEnabled = false;
            xtraTabPage7.PageEnabled = false;
            xtraTabPage13.PageEnabled = false;
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.hayleys.com");
                //SmtpClient SmtpServer = new SmtpClient("smtpout.secureserver.net");
                mail.From = new MailAddress("lasantha.kariyawasam@industrial.hayleys.com");
                mail.To.Add("janaka.madurasinghe@industrial.hayleys.com");
                mail.Subject = "Test";
                mail.Body = "Test Body";
                mail.IsBodyHtml = true;
                SmtpServer.Port = 20;

                SmtpServer.Credentials = new System.Net.NetworkCredential("lasantha.kariyawasam", "dilusha@1");
                SmtpServer.EnableSsl = true;

                // SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                InfoCrm.Tools.Logger.Log("Email From : " + "pe.crm@industrial.hayleys.com" + "  and Email TO : " + "lasantha.kariyawasam@industrial.hayleys.com" +
                    " Username : " + "hisl.crm" + "  and Password : " + "apple@123" + "");


                //SmtpServer.Send(mail);

                Email_Manager em = new Email_Manager();
                String Subject = "NEW Inquiry – Customer : " + "TesMail" + " " + " - Inquiry No : " + "Test Inquery No" + " ";
                Boolean result = em.SendEmail("powerservice@industrial.hayleys.com", "CRM testMail", "This e-mail is for testing purpose Please ignore it");
                if (result)
                {
                    // XtraMessageBox.Show("Email Send Successfully");
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());

                if (ex.InnerException != null)
                {
                    MessageBox.Show("Inner Exception " + ex.InnerException.Message.Trim());
                    InfoCrm.Tools.Logger.Log("Error : " + ex.Message + "  and Inner EX : " + ex.InnerException.Message.Trim());
                }
                else
                {
                    InfoCrm.Tools.Logger.Log("Error : " + ex.Message);
                }

                XtraMessageBox.Show("Email Didn't Send", "Error");
                // throw ex;
            }

        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //FileList.Clear();

                    foreach (var item in openFileDialog1.FileNames)
                    {
                        FileList.Add(new InfoPCMS.Services.FileModel()
                        {
                            FileName = item,
                            ShortFileName = Path.GetFileName(item)
                        });
                    }

                    UpdateDocuemtnListView();

                }
            }
            catch (Exception ex) { }
        }

        private void UpdateDocuemtnListView()
        {

            try
            {
                listView1.Items.Clear();

                foreach (var item in FileList)
                {
                    ListViewItem itm = new ListViewItem(item.ShortFileName, 1);
                    itm.Tag = item.FileName;
                    listView1.Items.Add(itm);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Globals.MessageCaption);
            }
        }

        private List<InfoPCMS.Services.FileModel> LoadFileList()
        {
            return FileList;
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    if (FileList.Count > 0)
                    {
                        FileModel model = new InfoPCMS.Services.FileModel();
                        model = FileList.Find(t => t.ShortFileName.ToUpper().Trim() == listView1.SelectedItems[0].Text.Trim().ToUpper());
                        FileList.Remove(model);

                        UpdateDocuemtnListView();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Please select a Document to remove", Globals.MessageCaption);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Globals.MessageCaption);
            }
        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            try
            {
                FileList.Clear();
                UpdateDocuemtnListView();
                //SaveFileList("INQ-BRK-16-00000029");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Globals.MessageCaption);
            }
        }

        private void SaveFileList(string inqueryNo)
        {
            try
            {
                if (FileList.Count > 0)
                {
                    string dirpath = "";
                    if (!Directory.Exists(Globals.DocPath))
                    {
                        dirpath = Path.Combine(Globals.DocPath, lblInquiryNumber.Text.Trim());
                        if (!Directory.Exists(Globals.DocPath))
                        {
                            Directory.CreateDirectory(dirpath);
                        }
                    }

                    foreach (var item in FileList)
                    {
                        string FileName = Path.Combine(dirpath, item.ShortFileName);
                        if (!File.Exists(FileName))
                        {
                            File.Copy(item.FileName, FileName, true);
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                XtraMessageBox.Show("You don't have the required permission on the Server to copy this documents", Globals.MessageCaption);
            }
            catch (Exception ex) {
                XtraMessageBox.Show("An I/O error has occurred. System did not copy the files", Globals.MessageCaption);
            }
        }

        private void GetExistingList(string inqnumber)
        {
           
            string dirpath = Path.Combine(Globals.DocPath, inqnumber);
            if (Directory.Exists(dirpath))
            {
                string[] arr = Directory.GetFiles(dirpath);
                foreach (var item in arr)
                {
                    FileModel fm = new InfoPCMS.Services.FileModel()
                    {
                        FileName = item,
                        ShortFileName = Path.GetFileName(item)
                    };
                    FileList.Add(fm);
                }
            }
        }

        private void gridaccepted_Click_1(object sender, EventArgs e)
        {

        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {

            //Comented by SUmi 07/10/2016

            //DataTable dtGridAccepted = new DatabaseService().executeSelectQuery("SELECT (select   Sum(CAST(Percentage as numeric(9))) FROM InquiryStatusDetails where InquiryNumber=dbo.InquiryStatus.InquiryNumber) as percentage,actual,duedate, InquiryStatus.InquiryNumber as  InquiryNumber,InquiryStatus.Dates as  date, InquiryStatus.Customer as  Customer, InquiryStatus.InquiryTaken as InquiryTaken, InquiryStatus.InspectionDone as InspectionDone, InquiryStatus.SiteResponsible as SiteResponsible, Inquiry.SiteResponsible,InquiryStatus.NextAction as NextAction,InquiryStatus.DateTime as DateTime,DATEADD(hour,(SELECT CompletionHours FROM InquiryType WHERE InquiryType = dbo.Inquiry.ProblemNature),(cast(cast(Inquiry.Date as date) as datetime) + cast(Inquiry.Time as time(7)))) AS AgreedDate FROM InquiryStatus INNER JOIN Inquiry ON InquiryStatus.InquiryNumber = Inquiry.InquiryNumber where InquiryStatus.Status='Accept' ");

            //DateTime dtDueDate = Convert.ToDateTime(dtGridAccepted.Rows[0]["duedate"].ToString().Trim());

            //DateTime strAgreeDate = Convert.ToDateTime(gridView2.GetFocusedDataRow()["duedate"].ToString());
            string inquiryNo = gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim();
            DataTable dtInqChangeComp = new DatabaseService().executeSelectQuery("SELECT * FROM InqChangeCompDateTemp WHERE InquiryNumber='" + gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim() + "' ");

            string StatusReqChange = "";
            FileList.Clear();
            GetExistingList(inquiryNo);
            UpdateDocuemtnListView();


            if (dtInqChangeComp.Rows.Count.ToString() == "0")
            {
                StatusReqChange = "";
            }
            else
            {
                StatusReqChange = dtInqChangeComp.Rows[0]["Status"].ToString().Trim();

            }


            if (StatusReqChange == "Pending Approval")
            {
                XtraMessageBox.Show("Pending Supervisor Approval for Completed Date Change", "Error");
            }
            else
            {
                if (!(InfoPCMS.Views.frmHome.EmpCat == "LEVEL 3"))
                {
                    lblInquiryNumber.Text = gridView2.GetFocusedDataRow()["InquiryNumber"].ToString();
                    lblInquiryType.Text = GetInquiryType(gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim());
                    txt_hourmeter.Text = "";
                    txtAtcualProbUpdate.Text = GetInquiryActProb(gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim());

                    DataTable dtSelInq = new DatabaseService().executeSelectQuery("SELECT T0.*,T1.actual FROM HayleysPowerEngineeringCRM.dbo.Inquiry T0 "
                    + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.InquiryStatus T1 ON T0.InquiryNumber = T1.InquiryNumber "
                    + "WHERE T0.InquiryNumber = '" + gridView2.GetFocusedDataRow()["InquiryNumber"].ToString() + "' ");

                    lblSelCus.Text = getCusName(dtSelInq.Rows[0]["CustomerId"].ToString().Trim()).Trim();

                    txtActualProblem.Text = dtSelInq.Rows[0]["actual"].ToString().Trim();

                    if (GetInquiryType(gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim()) == "Repairs")
                    {
                        pnlSerInclude.Visible = true;

                        rdbtnSerInculdeNo.Checked = true;
                        rdbtnSerInculdeYes.Checked = false;

                        DataTable dtJobStart = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryStatusDetails WHERE InquiryNumber='" + gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim() + "' ");

                        if (dtJobStart.Rows.Count > 0)
                        {
                            pnlSerInclude.Enabled = false;

                            DataTable dtinqStatusIsSerIn = new DatabaseService().executeSelectQuery("SELECT * FROM InquiryStatus WHERE InquiryNumber='" + gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim() + "' ");

                            string isSerInclude = dtinqStatusIsSerIn.Rows[0]["IsServiceInclude"].ToString().Trim();

                            if (isSerInclude == "True")
                            {
                                rdbtnSerInculdeYes.Checked = true;
                                //rdbtnSerInculdeNo.Checked = false;
                            }
                            else
                            {
                                rdbtnSerInculdeYes.Checked = false;
                                //rdbtnSerInculdeNo.Checked = true;
                            }

                        }


                    }
                    else
                    {
                        pnlSerInclude.Visible = false;
                    }


                    xtraTabControl2.SelectedTabPageIndex = 1;
                    xtraTabPage3.PageEnabled = false;
                    xtraTabPage6.PageEnabled = true;
                    tbPageChangeInqDetails.PageEnabled = false;
                    // pnlRecRepairs.Enabled = false;

                    this.SetSubActionTypes();


                    DataTable Completed = new DatabaseService().executeSelectQuery("SELECT  InquiryNumber, ActionType,SubActionType,ActionDetails,ActionDate, Remarks, JobCompleted,Percentage FROM InquiryStatusDetails where InquiryNumber='" + lblInquiryNumber.Text + "'");
                    tblupdate.DataSource = Completed;
                    // }
                }
                else
                {

                    lblInquiryNumber.Text = gridView2.GetFocusedDataRow()["InquiryNumber"].ToString();

                    lblInquiryType.Text = GetInquiryType(gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim());

                    txtAtcualProbUpdate.Text = GetInquiryActProb(gridView2.GetFocusedDataRow()["InquiryNumber"].ToString().Trim());



                    xtraTabControl2.SelectedTabPageIndex = 1;
                    xtraTabPage3.PageEnabled = false;
                    xtraTabPage6.PageEnabled = true;
                    tbPageChangeInqDetails.PageEnabled = false;
                    //pnlRecRepairs.Enabled = false;

                    this.SetSubActionTypes();

                    // this.SetNextActionTypes();


                    //  cboActionType.Focus();
                    //cboActionType.SelectedIndex = -1;
                    //DataTable Completed = new DatabaseService().executeSelectQuery("SELECT  InquiryNumber, ActionType, ActionDetails,ActionDate, Remarks, JobCompleted,Percentage FROM InquiryStatusDetails where InquiryNumber='" + lblInquiryNumber.Text + "'");
                    //tblupdate.DataSource = Completed;


                    DataTable Completed = new DatabaseService().executeSelectQuery("SELECT  InquiryNumber, ActionType,SubActionType,ActionDetails,ActionDate, Remarks, JobCompleted,Percentage FROM InquiryStatusDetails where InquiryNumber='" + lblInquiryNumber.Text + "'");
                    tblupdate.DataSource = Completed;
                }


            }



        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0) {

                System.Diagnostics.Process.Start(listView1.SelectedItems[0].Tag.ToString());
                
            }
        }

    





    }

}