using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using System.Data.SqlClient;
using InfoPCMS.Views;
using HayleysPowerEngineeringCRM.View;
using CrystalDecisions.CrystalReports.Engine;
using InfoPCMS.Services;
using InfoCrm.Tools;
 

namespace InfoPCMS.Views
{
    public partial class frmHome : DevExpress.XtraEditors.XtraForm
    {

        public static String UsrName = "";
        public static String EmpName = "";

        public static String EmpCat = "";

        public static bool isOffline;

        Thread appThread;

        

        public frmHome()
        {
            InitializeComponent();
        }



        //void timer_Tick(object sender, EventArgs e)
        //{
        //    Notification();
        //}

        private void Notification()
        {
            try
            {

                lblLoginAs.Text = frmHome.EmpName;

                //if (frmHome.UsrName == "admin")
                //{
                     if (frmHome.EmpCat == "LEVEL 3")
                {



                    DataTable dt = new DatabaseService().executeSelectQuery("SELECT Count(*) as co FROM  ServicesShedule where PlanDate < GETDATE() AND STATUS <> 'Complete'");
                    if (dt.Rows.Count > 0)
                    {
                        if (!(Convert.ToInt16(dt.Rows[0]["co"].ToString()) == 0))
                        {
                            labelControl2.Visible = true;
                            labelControl1.Visible = true;
                            labelControl2.Text = dt.Rows[0]["co"].ToString();
                        }
                        else
                        {
                            labelControl2.Visible = false;
                            labelControl1.Visible = false;
                        }

                    }

                    DataTable dt1 = new DatabaseService().executeSelectQuery("SELECT Count(*) as co FROM  ServicesShedule where Status='Up Comming' AND PlanDate BETWEEN GETDATE() AND DATEADD(day,30, GETDATE())");
                    if (dt1.Rows.Count > 0)
                    {
                        if (!(Convert.ToInt16(dt1.Rows[0]["co"].ToString()) == 0))
                        {
                            labelControl3.Visible = true;
                            labelControl4.Visible = true;                       
                            labelControl3.Text = dt1.Rows[0]["co"].ToString();
                        }
                        else {
                            labelControl4.Visible = false;
                            labelControl3.Visible = false;
                        }

                    }
                    DataTable dtPendingApproval = new DatabaseService().executeSelectQuery("SELECT Count(*) as co FROM  Inquiry where RequestForward='Yes'  AND ApprovedForward IS NULL");
                    if (dtPendingApproval.Rows.Count > 0)
                     {
                         if (!(Convert.ToInt16(dtPendingApproval.Rows[0]["co"].ToString()) == 0))
                         {
                             labelControl5.Visible = true;
                             labelControl6.Visible = true;
                             labelControl6.Text = dtPendingApproval.Rows[0]["co"].ToString();
                         }
                         else
                         {
                             labelControl5.Visible = false;
                             labelControl6.Visible = false;
                         }
                     
                     }

                    DataTable dtPendingApprovalDateChange = new DatabaseService().executeSelectQuery("SELECT Count(*) as co FROM  InqChangeCompDateTemp WHERE IsApproved IS NULL");
                    if (dtPendingApprovalDateChange.Rows.Count > 0)
                    {
                        if (!(Convert.ToInt16(dtPendingApprovalDateChange.Rows[0]["co"].ToString()) == 0))
                        {
                            labelControl5.Visible = true;
                            labelControl6.Visible = true;
                            //labelControl6.Text = dtPendingApproval.Rows[0]["co"].ToString();

                            if (Convert.ToInt32(labelControl6.Text) > 0)
                            {
                                // labelControl6.Text = "";

                                int intPendFor = Convert.ToInt32(labelControl6.Text.ToString());
                                int intPendChange = Convert.ToInt32(dtPendingApprovalDateChange.Rows[0]["co"].ToString());

                                labelControl6.Text = (intPendFor + intPendChange).ToString();
                            }
                            else
                            {
                                labelControl6.Text = dtPendingApprovalDateChange.Rows[0]["co"].ToString();
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(labelControl6.Text) > 0)
                            {
                                // labelControl6.Text = "";

                                int intPendFor = Convert.ToInt32(labelControl6.Text.ToString());
                              //  int intPendChange = Convert.ToInt32(dtPendingApprovalDateChange.Rows[0]["co"].ToString());

                               // labelControl6.Text = (intPendFor + intPendChange).ToString();

                                labelControl5.Visible = true;
                                labelControl6.Visible = true;
                            }
                            else
                            {
                                labelControl5.Visible = false;
                                labelControl6.Visible = false;
                            }




                            
                        }

                    }

                    //DataTable dtPendingApproval2 = new DatabaseService().executeSelectQuery("SELECT Count(*) as co FROM  InquiryStatus where PendingApproval='Yes' AND ApprovedForChange IS NULL");
                    //if (dtPendingApproval2.Rows.Count > 0)
                    //{
                    //    if (!(Convert.ToInt16(dtPendingApproval2.Rows[0]["co"].ToString()) == 0))
                    //    {
                    //        labelControl5.Visible = true;
                    //        labelControl6.Visible = true;
                    //        if (Convert.ToInt32(labelControl6.Text) > 0)
                    //        {
                    //            // labelControl6.Text = "";

                    //            int intPendFor = Convert.ToInt32(labelControl6.Text.ToString());
                    //            int intPendChange = Convert.ToInt32(dtPendingApproval2.Rows[0]["co"].ToString());

                    //            labelControl6.Text = (intPendFor + intPendChange).ToString();

                    //        }
                    //        else
                    //        {
                    //            labelControl6.Text = dtPendingApproval2.Rows[0]["co"].ToString();
                    //        }


                    //    }
                    //    else
                    //    {
                    //        labelControl5.Visible = false;
                    //        labelControl6.Visible = false;
                    //    }

                    //}

                }
                else {
                    DataTable dt = new DatabaseService().executeSelectQuery("SELECT Count(*) as co FROM  ServicesShedule where  PlanDate < GETDATE() and ResponsiblePerson='" +GetEmpId(EmpName) + "' AND STATUS <> 'Complete'");
                    if (dt.Rows.Count > 0)
                    {
                        if (!(Convert.ToInt16(dt.Rows[0]["co"].ToString()) == 0))
                        {
                            labelControl2.Visible = true;
                            labelControl1.Visible = true;
                            labelControl2.Text = dt.Rows[0]["co"].ToString();
                        }
                        else {
                            labelControl2.Visible = false;
                            labelControl1.Visible = false;
                        }

                    }

                    DataTable dt1 = new DatabaseService().executeSelectQuery("SELECT Count(*) as co FROM  ServicesShedule where Status='Up Comming' and ResponsiblePerson='" + GetEmpId(EmpName) + "' AND PlanDate BETWEEN GETDATE() AND DATEADD(day,30, GETDATE())");
                    if (dt1.Rows.Count > 0)
                    {
                        if (!(Convert.ToInt16(dt1.Rows[0]["co"].ToString()) == 0))
                        {
                          
                            labelControl3.Visible = true;
                            labelControl4.Visible = true;
                            labelControl3.Text = dt1.Rows[0]["co"].ToString();
                        }
                        else
                        {
                            labelControl4.Visible = false;
                            labelControl3.Visible = false;
                        }

                    }

                    DataTable dtuser = new DatabaseService().executeSelectQuery("select Id,Category from Employee  WHERE EmployeeName='" + Views.frmHome.EmpName + "'");

                    //if (dtuser.Rows[0]["Category"].ToString().Trim() == "LEVEL 3")
                    //{
                        DataTable dtPendingApproval = new DatabaseService().executeSelectQuery("SELECT Count(*) as co FROM  Inquiry where RequestForward='Yes' AND ApprovedForward IS NULL");
                        if (dtPendingApproval.Rows.Count > 0)
                        {
                            if (!(Convert.ToInt16(dtPendingApproval.Rows[0]["co"].ToString()) == 0))
                            {
                                labelControl5.Visible = true;
                                labelControl6.Visible = true;
                                labelControl6.Text = dtPendingApproval.Rows[0]["co"].ToString();
                            }
                            else
                            {
                                labelControl5.Visible = false;
                                labelControl6.Visible = false;
                            }

                        }

                        DataTable dtPendingApproval2 = new DatabaseService().executeSelectQuery("SELECT Count(*) as co FROM  InquiryStatus where PendingApproval='Yes' AND ApprovedForChange IS NULL");
                        if (dtPendingApproval2.Rows.Count > 0)
                        {
                            if (!(Convert.ToInt16(dtPendingApproval2.Rows[0]["co"].ToString()) == 0))
                            {
                                labelControl5.Visible = true;
                                labelControl6.Visible = true;
                                if (Convert.ToInt32(labelControl6.Text) > 0)
                                {
                                   // labelControl6.Text = "";

                                    int intPendFor = Convert.ToInt32(labelControl6.Text.ToString());
                                    int intPendChange =  Convert.ToInt32(dtPendingApproval2.Rows[0]["co"].ToString());

                                    labelControl6.Text = (intPendFor + intPendChange).ToString();

                                }
                                else
                                {
                                    labelControl6.Text = dtPendingApproval2.Rows[0]["co"].ToString();
                                }


                            }
                            else
                            {
                                labelControl5.Visible = false;
                                labelControl6.Visible = false;
                            }

                        }
                    //}
                }
                
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            //String welcometext = "Welcome : " + InfoPCMS.user.Username;
            //lblwelcome.Text = welcometext;
            // grpMain.Visible = true;

            

            try
            {
                Notification();
                this.StartPosition = FormStartPosition.CenterScreen;
                //System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
                //t.Interval = 10000; // specify interval time as you want
                //t.Tick += new EventHandler(timer_Tick);
                //t.Start();

            }
            catch (Exception ex)
            {

            }

        }


        private string GetEmpId(string strEmpName)
        {
            string strEmpId = "";
            DataTable dtEmployee = new DatabaseService().executeSelectQuery("select * from Employee  WHERE EmployeeName = '" + strEmpName + "'");


            if (dtEmployee.Rows.Count > 0)
            {

                strEmpId = dtEmployee.Rows[0]["Id"].ToString();
            }

            return strEmpId;
        }
               

        private void gbtCustomer_Click(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("6"))
            {
                InfoPCMS.currentfrm = new frmCustomer();
                InfoPCMS.currentfrm.Show();
            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");


            }

        }

        private void gbtEmployee_Click(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("3"))
            {

                InfoPCMS.currentfrm = new frmEmployee();
                InfoPCMS.currentfrm.Show();
            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }

        }           
                
      
        private void navCustomers_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("6"))
            {

                InfoPCMS.currentfrm = new frmCustomer();
                InfoPCMS.currentfrm.Show();
            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), Globals.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void navEmployee_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("3"))
            {

                InfoPCMS.currentfrm = new frmEmployee();
                InfoPCMS.currentfrm.Show();

            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }

        }

        

        private void navInquiry_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("17"))
            {
                InfoPCMS.currentfrm = new frmInquiryManagement();
                InfoPCMS.currentfrm.Show();
            }
            else
            {
                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), Globals.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
               
       
        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("65"))
            {


                frmReportViewer objRepviwer = new frmReportViewer();

                string ReportName = "";
                string rptTitle = "";

                string fltString = "";


                fltString = "";



                ReportName = "Customers.rpt";



                rptTitle = "All Customers";

                objRepviwer.rptPath = ReportName;
                objRepviwer.rptTitle = rptTitle;
                objRepviwer.selectionForumla = fltString;
                objRepviwer.reporttype = 1;

                //Date Range
                //objRepviwer.startdate = txtDate1.EditValue.ToString();

                //objRepviwer.startdate = txtDate1.Text;
                //objRepviwer.enddate = txtDate2.Text;
                //Date Range

                objRepviwer.paraRepName = "paraArrdate";
                objRepviwer.paraRepVale = "";


                objRepviwer.Show();

            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (InfoPCMS.user.checkFunctionAuthentication("66"))
            {
                frmReportViewer objRepviwer = new frmReportViewer();
                string ReportName = "";
                string rptTitle = "";
                string fltString = "";
                fltString = "";
                ReportName = "Employees.rpt";
                rptTitle = "All Employees";

                objRepviwer.rptPath = ReportName;
                objRepviwer.rptTitle = rptTitle;
                objRepviwer.selectionForumla = fltString;
                objRepviwer.reporttype = 1;

                //Date Range
                //objRepviwer.startdate = txtDate1.EditValue.ToString();

                //objRepviwer.startdate = txtDate1.Text;
                //objRepviwer.enddate = txtDate2.Text;
                //Date Range

                objRepviwer.paraRepName = "paraArrdate";
                objRepviwer.paraRepVale = "";


                objRepviwer.Show();
            }
            else
            {
                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }
        }
              

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getBackup();
        }

        private void getBackup()
        {
            DialogResult bt = MessageBox.Show("Do You Want To Take the DB Backup", "DB Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (bt == DialogResult.Yes)
            {
                try
                {
                    string passyear = DateTime.Now.Year.ToString();
                    string passmonth = DateTime.Now.Month.ToString();
                    string passday = DateTime.Now.Day.ToString();
                    string passHour = DateTime.Now.Hour.ToString();
                    string passmin = DateTime.Now.Minute.ToString();
                    string passsec = DateTime.Now.Second.ToString();


                    string BackupTime = passyear + "" + passmonth + "" + passday + " " + passHour + "" + passmin + "" + passsec;

                    string GetDBName = InfoPCMS.configurations.DBName;

                    string GetPath = InfoPCMS.configurations.DBBackupPath;
                    // Public DBBACKUPPATH As String = "TO DISK = N'D:\RMSDB_BACKUPS\"

                    string GetFilename = GetDBName + "_" + "Backup" + "_" + BackupTime + InfoPCMS.user.StrUsername + ".bak";

                    string fullfilepath = GetPath + GetFilename;
                    // Dim GetFilename As String = GetDBName + "_" + "Backup2.bak'"

                    // MessageBox.Show(fullfilepath);
                    // String passcommand = "BACKUP DATABASE" + " " + GetDBName + " " + GetPath + GetFilename;


                    String sCommand = "BACKUP DATABASE " + GetDBName + " TO DISK = N'" + fullfilepath + "' WITH COPY_ONLY";

                    // dynamic sCommand = passcommand;
                    InfoPCMS.db.executeUpdateQuery(sCommand);

                    XtraMessageBox.Show("DB Backup done Successfully", "DB Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        private void dashBoardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new FrmMainDashBoard().Show();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            new FrmMyTask().Show();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            new FrmServiceAgreenment().Show();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            new FrmGenerators().Show();
        }

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            new FrmGenerators().Show();
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            new FrmServiceAgreenment().Show();
        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            new FrmMyTask().Show();
        }

        private void switchUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new frmLogin().Show();


        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
           
      
        private void navBarItem1_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            new FrmMainDashBoard().Show();
        }

        private void navBarItem13_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            new FrmGenerators().Show();
        }

        private void navBarItem15_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            new FrmServiceAgreenment().Show();
        }

        private void navBarItem14_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            new FrmMyTask().Show();
        }

        private void navBarItem19_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.Hide();
            new frmLogin().Show();
        }

        private void navBarItem20_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.Close();
        }

        private void navBarItem21_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            new frmEmployee().Show();
        }

        private void navBarItem2_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmReportViewer objRepviwer = new frmReportViewer();
            ReportDocument cryRpt = new ReportDocument();
            objRepviwer.rptPath = "PendingInquiryRePerWise.rpt";
            //  cryRpt.Load("PendingInquiry.rpt");
            // objRepviwer.myCrystalReportViewer.ReportSource = cryRpt;
            objRepviwer.myCrystalReportViewer.Refresh();
            objRepviwer.Show();

        }

        private void navBarItem3_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmReportViewer objRepviwer = new frmReportViewer();
            ReportDocument cryRpt = new ReportDocument();
            objRepviwer.rptPath = "PendingInquiryCusWise.rpt";
            //  cryRpt.Load("PendingInquiry.rpt");
            // objRepviwer.myCrystalReportViewer.ReportSource = cryRpt;
            objRepviwer.myCrystalReportViewer.Refresh();
            objRepviwer.Show();
        }

        private void labelControl4_Click(object sender, EventArgs e)
        {
            frmJobServices mt = new frmJobServices();
            mt.Show();
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {
            frmJobServices mt = new frmJobServices();
            mt.Show();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            frmJobServices mt = new frmJobServices();
            mt.Show();
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {
            frmJobServices mt = new frmJobServices();
            mt.Show();
           // mt.xtraTabControl3.SelectedTabPageIndex = 0;
        }

        private void navBarItem17_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmReportViewer objRepviwer = new frmReportViewer();
            ReportDocument cryRpt = new ReportDocument();
            objRepviwer.rptPath = "UpCommingServiceShedule.rpt";
            //  cryRpt.Load("PendingInquiry.rpt");
            // objRepviwer.myCrystalReportViewer.ReportSource = cryRpt;
            objRepviwer.myCrystalReportViewer.Refresh();
            objRepviwer.Show();
        }

        private void navBarItem16_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
             frmReportViewer objRepviwer = new frmReportViewer();
            ReportDocument cryRpt = new ReportDocument();
            objRepviwer.rptPath = "DueServiceShedule.rpt";
            //  cryRpt.Load("PendingInquiry.rpt");
            // objRepviwer.myCrystalReportViewer.ReportSource = cryRpt;
            objRepviwer.myCrystalReportViewer.Refresh();
            objRepviwer.Show();
            
        }

        private void navBarItem22_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmReportViewer objRepviwer = new frmReportViewer();
            ReportDocument cryRpt = new ReportDocument();
            objRepviwer.rptPath = "CompletedServiceShedule.rpt";
            //  cryRpt.Load("PendingInquiry.rpt");
            // objRepviwer.myCrystalReportViewer.ReportSource = cryRpt;
            objRepviwer.myCrystalReportViewer.Refresh();
            objRepviwer.Show();
        }

        private void navBarItem23_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmReportViewer objRepviwer = new frmReportViewer();
            ReportDocument cryRpt = new ReportDocument();
            objRepviwer.rptPath = "OngoingServiceShedule.rpt";
            //  cryRpt.Load("PendingInquiry.rpt");
            // objRepviwer.myCrystalReportViewer.ReportSource = cryRpt;
            objRepviwer.myCrystalReportViewer.Refresh();
            objRepviwer.Show();

        }

        private void navBarItem24_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmReportViewer objRepviwer = new frmReportViewer();
            ReportDocument cryRpt = new ReportDocument();
            objRepviwer.rptPath = "OnGoingInquiry.rpt";
            //  cryRpt.Load("PendingInquiry.rpt");
            // objRepviwer.myCrystalReportViewer.ReportSource = cryRpt;
            objRepviwer.myCrystalReportViewer.Refresh();
            objRepviwer.Show();

        }

        private void navBarItem25_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            new FrmEscalation().Show();
        }

        private void navInquiryType_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //new frmInquiryType().Show();


            if (InfoPCMS.user.checkFunctionAuthentication("76"))
            {
                InfoPCMS.currentfrm = new frmInquiryType();
                InfoPCMS.currentfrm.Show();
            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }
        }

        private void navActionType_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("77"))
            {
                InfoPCMS.currentfrm = new frmActionType();
                InfoPCMS.currentfrm.Show();
            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }
        }

        private void navBarItem26_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("80"))
            {
                InfoPCMS.currentfrm = new frmJobServices();
                InfoPCMS.currentfrm.Show();
            }
            else
            {
                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), Globals.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gbtGenerator_DoubleClick(object sender, EventArgs e)
        {
            new FrmGenerators().Show();
        }

        private void labelControl6_Click(object sender, EventArgs e)
        {
            frmApproval mt = new frmApproval();
            mt.Show();
        }

        private void labelControl5_Click(object sender, EventArgs e)
        {
            frmApproval mt = new frmApproval();
            mt.Show();
        }

        private void labelControl7_Click(object sender, EventArgs e)
        {
            frmApproval mt = new frmApproval();
            mt.Show();
        }

        private void gbtSerAgreement_DoubleClick(object sender, EventArgs e)
        {
            new FrmServiceAgreenment().Show();
        }

       
        private void gbtInquirymgt_DoubleClick(object sender, EventArgs e)
        {

            if (InfoPCMS.user.checkFunctionAuthentication("17"))
            {
                InfoPCMS.currentfrm = new frmInquiryManagement();
                InfoPCMS.currentfrm.Show();
            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }
        }

        private void gbtMyTask_DoubleClick(object sender, EventArgs e)
        {
            new FrmMyTask().Show();
        }

        private void gbtJobService_DoubleClick(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("80"))
            {
                InfoPCMS.currentfrm = new frmJobServices();
                InfoPCMS.currentfrm.Show();
            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }
        }
               
        private void simpleButton5_DoubleClick(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("6"))
            {

                InfoPCMS.currentfrm = new frmCustomer();
                InfoPCMS.currentfrm.Show();
            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }
        }

        private void gbtEmployee_DoubleClick(object sender, EventArgs e)
        {
            new frmEmployee().Show();
        }

        private void simpleButton2_DoubleClick(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("76"))
            {
                InfoPCMS.currentfrm = new frmInquiryType();
                InfoPCMS.currentfrm.Show();
            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }
        }

        private void gbtProSales_DoubleClick(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("77"))
            {
                InfoPCMS.currentfrm = new frmActionType();
                InfoPCMS.currentfrm.Show();
            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }
        }
               
        private void navUsers_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("82"))
            {
                InfoPCMS.currentfrm = new frmUser();
                InfoPCMS.currentfrm.Show();
            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }
        }

        private void navMain_Click(object sender, EventArgs e)
        {

        }

        private void gbtGenerator_Click(object sender, EventArgs e)
        {
            new FrmGenerators().Show();
        }

        private void gbtSerAgreement_Click(object sender, EventArgs e)
        {
            new FrmServiceAgreenment().Show();
        }

        private void gbtInquirymgt_Click(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("17"))
            {
                InfoPCMS.currentfrm = new frmInquiryManagement();
                InfoPCMS.currentfrm.Show();
            }
            else
            {
                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), Globals.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gbtMyTask_Click(object sender, EventArgs e)
        {
            new FrmMyTask().Show();
        }

        private void gbtJobService_Click(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("80"))
            {
                InfoPCMS.currentfrm = new frmJobServices();
                InfoPCMS.currentfrm.Show();
            }
            else
            {
                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), Globals.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("6"))
            {

                InfoPCMS.currentfrm = new frmCustomer();
                InfoPCMS.currentfrm.Show();
            }
            else
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), Globals.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }





    }
}