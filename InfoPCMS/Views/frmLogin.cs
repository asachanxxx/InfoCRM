using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using InfoPCMS.Services;
using System.Threading;
using InfoCrm.Tools;

namespace InfoPCMS.Views
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        Thread appThread;
        DateTime PubSdate;
        DateTime PubEdate;
        DateTime serverdate;
        public frmLogin()
        {
            InitializeComponent();
            this.AcceptButton = this.btnlogin;
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {

            try
            {
                if (CheckingLicense() == false)
                {

                    XtraMessageBox.Show("InfoPCMS License Key Expired. Please Contact System Administrators To Activate", Globals.MessageCaption,MessageBoxButtons.OK,MessageBoxIcon.Error);

                }
                else
                {

                    TimeSpan tsDiff = PubEdate - serverdate;
                    int theDays = tsDiff.Days;

                    if (theDays <= 30)
                    {

                        String expmessage = "InfoPCMS License Key Will Be Expired With In " + theDays.ToString() + " Days...";
                        XtraMessageBox.Show(expmessage, Globals.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                    String username = txtusername.Text;
                    String password = txtpassword.Text;

                    LoginService login = new LoginService();
                    if (login.validateLogin(username, password))
                    {

                        this.Close();

                        //frmHome frmHomePage = new frmHome();
                        frmHome.UsrName = txtusername.Text;
                        //frmHomePage.ShowDialog();


                        appThread = new Thread(LaunchNewForm);
                        appThread.SetApartmentState(ApartmentState.STA);
                        appThread.Start();
                        //this.LaunchNewForm();


                        frmHome.UsrName = txtusername.Text;

                        frmHome.EmpName = this.getEmpNameByUsername(txtusername.Text.Trim());

                        frmHome.EmpCat = this.getEmpCatByUsername(txtusername.Text.Trim());

                        if (chkBxOffline.Checked)
                        {
                            frmHome.isOffline = true;
                        }
                        else
                        {
                            frmHome.isOffline = false;
                        }

                    }
                    else
                    {

                        XtraMessageBox.Show(InfoPCMS.message.GET_LOGIN_ERROR(), "Login Error");

                    }

                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                XtraMessageBox.Show("Network Related Error occurred. this might happen because \n 1.Network connection problems \n 2.Database server availability problems \n 3.Wrong system configuration file ", Globals.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Network Related Error occurred. this might happen because \n 1.Network connection problems \n 2.Database server availability problems \n 3.Wrong system configuration file ", Globals.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Unknown Error occurred. for assistance Please contact ", Globals.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void LaunchNewForm()
        {

            Application.Run(new frmHome());
            
            frmHome.UsrName = txtusername.Text;
        }

        private String checkValidity() {

            try
            {
            LicenseService license = new LicenseService();
            String currentKey = license.getCurrentKey();
            String getDecrypt = license.Decrypt(currentKey);
            string getLastDate = getDecrypt.Substring(1, 1) + getDecrypt.Substring(16, 1);
            string Sdate = "20" + getDecrypt.Substring(8, 1) + getDecrypt.Substring(3, 1) + "-" + getDecrypt.Substring(5, 1) + getDecrypt.Substring(12, 1) + "-" + "01";
            string Edate = "20" + getDecrypt.Substring(13, 1) + getDecrypt.Substring(18, 1) + "-" + getDecrypt.Substring(10, 1) + getDecrypt.Substring(6, 1) + "-" + getLastDate.ToString();

            System.Diagnostics.Debug.Write(Sdate + "::" + Edate);
            return Sdate + "::" + Edate;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        public string getEmpNameByUsername(string userName)
        {
            string strEmpName = "";
            string strEmpID = "";

            DataTable dtEmpUser = new DatabaseService().executeSelectQuery("SELECT * FROM Users WHERE Username = '"+txtusername.Text.ToString().Trim()+"' ");

            if (dtEmpUser.Rows.Count > 0)
            {
                strEmpID = dtEmpUser.Rows[0]["EmpID"].ToString().Trim();
                
            }

            DataTable dtEmp = new DatabaseService().executeSelectQuery("SELECT * FROM Employee WHERE Id = '" + strEmpID.ToString().Trim() + "' ");
            if (dtEmp.Rows.Count > 0)
            {
                strEmpName = dtEmp.Rows[0]["EmployeeName"].ToString().Trim();

            }

            return strEmpName;

        }


        public string getEmpCatByUsername(string userName)
        {
            string strEmpCat = "";
            string strEmpID = "";

            DataTable dtEmpUser = new DatabaseService().executeSelectQuery("SELECT * FROM Users WHERE Username = '" + txtusername.Text.ToString().Trim() + "' ");

            if (dtEmpUser.Rows.Count > 0)
            {
                strEmpID = dtEmpUser.Rows[0]["EmpID"].ToString().Trim();

            }

            DataTable dtEmp = new DatabaseService().executeSelectQuery("SELECT * FROM Employee WHERE Id = '" + strEmpID.ToString().Trim() + "' ");
            if (dtEmp.Rows.Count > 0)
            {
                strEmpCat = dtEmp.Rows[0]["Category"].ToString().Trim();

            }

            return strEmpCat;

        }

        public bool CheckingLicense()
        {


            try
            {
                string getkeys = checkValidity();

                DataTable dtserverdate = InfoPCMS.db.executeSelectQuery("select GETDATE() as sysdate");

                DateTime SysDate = DateTime.Now;

                if(dtserverdate.Rows.Count>0){

                    SysDate = Convert.ToDateTime(dtserverdate.Rows[0]["sysdate"].ToString());
                }

                serverdate = SysDate;
                
                PubSdate = Convert.ToDateTime(getkeys.Substring(0, 10).ToString());                
                PubEdate = Convert.ToDateTime(getkeys.Substring(12, 10).ToString());
                

                // dtDate.Text = "2014-4-09"




                if (serverdate <= PubEdate & serverdate >= PubSdate)
                {
                    return true;


                }
                else
                {
                    return true; ;


                }

               
            }
            catch (Exception ex)
            {
                throw ex;
               // return false;

            }


        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

       
    }
}