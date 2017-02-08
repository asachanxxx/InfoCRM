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
    public partial class FrmInqNewCstomer : DevExpress.XtraEditors.XtraForm
    {
        public FrmInqNewCstomer()
        {
            InitializeComponent();
        }
        public String type = "";

        private void btnAddInquiry_Click(object sender, EventArgs e)
        {

        }



        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            CustomerService customer = new CustomerService();
            customer.StrCustomerName = txtCusName.Text.Trim();
            customer.StrContactNo = txtContactNo.Text.Trim();
            customer.StrLocation = txtLocation.Text.Trim();
            customer.StrAddress = "";
            customer.StrEmail = "";
            customer.StrTaxType = "";
            customer.StrVATNo = "";
            customer.StrSVATNo = "";
            customer.StrMobileNo = "";
            customer.StrFaxNo = "";
            customer.StrContactPerson = txtContactPerson.Text;
            customer.StrDesignation = "";
            customer.StrDistanceToServiceCenter = "";
            customer.StrDistanceFromColombo = "";
            customer.StrHiltopSite = "False";
            customer.StrServiceCenter = cmbxNewCusServiceCen.SelectedValue.ToString().Trim();
            customer.StrIsActive = "False";
            customer.StrIsTemp = "True";
            Boolean result = customer.AddCustomer();

            if (result)
            {
                DataTable dt = InfoPCMS.db.executeSelectQuery(String.Format("select * from Customer where CustomerName='{0}' ", txtCusName.Text));
                if (dt.Rows.Count > 0)
                {
                    int intCusId = Convert.ToInt32(dt.Rows[0]["Id"].ToString());

                    new DatabaseService().executeUpdateQuery(String.Format("INSERT INTO CustomerLocation VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')",
                        dt.Rows[0]["Id"], dt.Rows[0]["CustomerName"], dt.Rows[0]["Location"], dt.Rows[0]["Location"], dt.Rows[0]["ContactPerson"],
                    dt.Rows[0]["Designation"], dt.Rows[0]["Address"], dt.Rows[0]["ContactNo"],
                    dt.Rows[0]["FaxNo"], dt.Rows[0]["Email"], dt.Rows[0]["ServiceCenter"], dt.Rows[0]["DistanceToServiceCenter"],
                    dt.Rows[0]["DistanceFromColombo"], dt.Rows[0]["HiltopSite"], dt.Rows[0]["NonMotorable"]));

                    if (!Views.frmHome.isOffline)
                    {
                        SendEmailToMarketing();
                    }

                }


            }



            frmInquiryManagement inq = new frmInquiryManagement();
            inq.txtProblemNature.SelectedValue = type;
            inq.IsNewCus = true;
            inq.Show();


            DataTable dt1 = InfoPCMS.db.executeSelectQuery(String.Format("select * from Customer where CustomerName='{0}' ", txtCusName.Text));
            if (dt1.Rows.Count > 0)
            {
                inq.txtCustomer.SelectedValue = dt1.Rows[0]["Id"];


            }




            this.Hide();
        }

        private void FrmInqNewCstomer_Load(object sender, EventArgs e)
        {
            DataTable dt = InfoPCMS.db.executeSelectQuery("SELECT * FROM ServiceCenter");
            cmbxNewCusServiceCen.DataSource = dt;
            cmbxNewCusServiceCen.DisplayMember = "ServiceCenter";
            cmbxNewCusServiceCen.ValueMember = "ServiceCenter";
        }


        private void SendEmailToMarketing()
        {
            string strFromEmail = "";
            string strToEmail = "";
            List<string> strToEmails = new List<string>();
            List<string> strCCEmails = new List<string>();


            strFromEmail = GetEmail(GetEmpId(frmHome.EmpName.ToString().Trim()));

            // strFromEmail = GetE();
            strToEmails = GetMarketingEmails();
            StringBuilder strBuliderEmailSubject = new StringBuilder();
            strBuliderEmailSubject.Append("New Customer Introduced");


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
            strBuliderEmailBody.Append("<b><u><FONT COLOR=DodgerBlue>New</FONT></u></b> Customer Entered");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Customer : </b>" + txtCusName.Text.Trim() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Location : </b>" + txtLocation.Text.Trim() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Contact Person : </b>" + txtContactPerson.Text.Trim() + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Contact No : </b>" + txtContactNo.Text.Trim() + "");
            strBuliderEmailBody.Append("<br>");

            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("Regards,");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("" + frmHome.EmpName + "");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("<br>");

            strBuliderEmailBody.Append("<FONT COLOR=darkred>Auto generated email from CRM system</FONT>");
            strBuliderEmailBody.Append("<br>");
            strBuliderEmailBody.Append("</body>");
            strBuliderEmailBody.Append("</html>");


            EmailServices Email = new EmailServices();

            Email.SendEmailsToMulti(strFromEmail.Trim(), strToEmails, strBuliderEmailSubject.ToString(), strBuliderEmailBody.ToString());



        }

        public string GetEmail(string strEmpId)
        {
            string strEmail = "";

            DataTable dt;
            dt = new DatabaseService().executeSelectQuery("SELECT Email FROM Employee WHERE Id = '" + strEmpId + "'");
            if (dt.Rows.Count > 0)
            {

                strEmail = dt.Rows[0]["Email"].ToString();
            }

            return strEmail;

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

        private List<string> GetMarketingEmails()
        {
            List<string> strToEmails = new List<string>();

            DataTable dtEmployee = new DatabaseService().executeSelectQuery("select Email from Employee  WHERE Department='AMB'");

            // strToEmails.Add(dtEmployee.["Email"].ToString());
            if (dtEmployee.Rows.Count > 0)
            {

                for (int index = 0; index < dtEmployee.Rows.Count; index++)
                {
                    strToEmails.Add(dtEmployee.Rows[index]["Email"].ToString());
                }
            }



            return strToEmails;
        }

    }
}