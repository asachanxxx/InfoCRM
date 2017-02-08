using System;
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
    public partial class frmInquiryType : DevExpress.XtraEditors.XtraForm
    {
        public frmInquiryType()
        {
            InitializeComponent();

            clearAll();
        }

        private void clearAll()
        {

            InquiryTypeService inquiryType = new InquiryTypeService();
            gridInquiryType.DataSource = inquiryType.getAllInquiryType();
            txtInquiryType.Text = "";
            txtCompDays.Text = "";
            txtCompHours.Text = "";
            txtReminder1.Text = "";
            txtReminder2.Text = "";
            txtEscalation1.Text = "";
            txtEscalation2.Text = "";  
            
            //txtCompDaysNonAgree.Text = "";
           // txtCompHoursNonAgree.Text = "";
            btnAddInquiryType.Text = "Add Inquiry Type";
            btnUpdateInquryType.Enabled = false;
            btnAddInquiryType.Enabled = true;
            txtInquiryType.Enabled = true;

            DataTable dtCuscat = InfoPCMS.db.executeSelectQuery("SELECT * FROM CustomerCategory");
            cmbxCusCat.DataSource = dtCuscat;
            cmbxCusCat.DisplayMember = "CategoryName";
            cmbxCusCat.ValueMember = "Id";
            cmbxCusCat.SelectedIndex = -1;           
            

            tabInquiryType.SelectedTabPage = xtraTabPage1;
            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
        
        }

        private void btnAddInquiry_Click(object sender, System.EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("74"))
            {
                if (btnAddInquiryType.Text == "Add Inquiry Type")
                {
                    btnAddInquiryType.Text = "Save";
                    tabInquiryType.SelectedTabPage = xtraTabPage2;
                    xtraTabPage1.PageEnabled = false;
                    xtraTabPage2.PageEnabled = true;


                }
                else if (btnAddInquiryType.Text == "Save")
                {
                    if (string.IsNullOrEmpty(txtCompDays.Text.ToString()))
                    {

                        txtCompDays.Text = "0";
                    }
                    if (string.IsNullOrEmpty(txtCompHours.Text.ToString()))
                    {

                        txtCompHours.Text = "0";
                    }
                    //
                    

                    int intCompHours = (Convert.ToInt32(txtCompDays.Text.ToString()) * 24 + Convert.ToInt32(txtCompHours.Text.ToString()));

                   // int intCompHoursNonAgree = (Convert.ToInt32(txtCompDaysNonAgree.Text.ToString()) * 24 + Convert.ToInt32(txtCompHoursNonAgree.Text.ToString()));

                  //  string strShortCode = txtShortCode.Text.ToString().Trim();

                    InquiryTypeService inquiryType = new InquiryTypeService();
                    inquiryType.InquiryType = txtInquiryType.Text.Trim();
                    inquiryType.CusCastegory = cmbxCusCat.SelectedValue.ToString();                  
                    inquiryType.CompletionHours = intCompHours;
                    inquiryType.Reminder1 = Convert.ToInt32(txtReminder1.Text.ToString().Trim());
                    inquiryType.Reminder2 = Convert.ToInt32(txtReminder2.Text.ToString().Trim());
                    inquiryType.Escalation1= Convert.ToInt32(txtEscalation1.Text.ToString().Trim());
                    inquiryType.Escalation2 = Convert.ToInt32(txtEscalation2.Text.ToString().Trim());    

                    // inquiryType.CompletionHoursNonAgree = intCompHoursNonAgree;
                    //inquiryType.ShortCode = strShortCode;

                    Boolean result = inquiryType.addInquiryType();

                    if (result)
                    {
                        clearAll();

                        XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_ADDED_INFORMATION(), "Information");

                    }
                }
            
            
            }
        }

        private void btnClearInquiryType_Click(object sender, System.EventArgs e)
        {
            clearAll();
        }

        private void btnUpdateInquryType_Click(object sender, System.EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("75"))
            {
                 int intCompHours = 0;
                // int intCompHoursNonAgree = 0;

                if (string.IsNullOrEmpty(txtCompDays.Text.ToString()))
                {
                    intCompHours = 0;
                }
                else
                {
                  intCompHours = Convert.ToInt32(txtCompDays.Text.ToString()) * 24;
                }

                 if (string.IsNullOrEmpty(txtCompHours.Text.ToString()))
                {
                    intCompHours = intCompHours + 0; 
                }
                else
                {
                    intCompHours += Convert.ToInt32(txtCompHours.Text.ToString());
                }


                InquiryTypeService inquiryType = new InquiryTypeService();
                inquiryType.InquiryType = txtInquiryType.Text.Trim();
                inquiryType.CompletionHours = intCompHours;
                inquiryType.CusCastegory = cmbxCusCat.SelectedValue.ToString();               
                inquiryType.Reminder1 = Convert.ToInt32(txtReminder1.Text.ToString().Trim());
                inquiryType.Reminder2 = Convert.ToInt32(txtReminder2.Text.ToString().Trim());
                inquiryType.Escalation1 = Convert.ToInt32(txtEscalation1.Text.ToString().Trim());
                inquiryType.Escalation2 = Convert.ToInt32(txtEscalation2.Text.ToString().Trim());  

                //inquiryType.CompletionHoursNonAgree = intCompHoursNonAgree;

                Boolean result = inquiryType.updateInquiryType();
                if (result)
                {
                    clearAll();

                    XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_UPDATED_INFORMATION(), "Information");
                    
                }

            }
            else
            {
                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");

            }
        }

        private void selectInquiryType(object sender, EventArgs e)
        {
            int intCompHours;
           // int intCompHoursNonAgree;
            
            String InquiryType = gridView1.GetFocusedDataRow()["InquiryType"].ToString();
            String CustomerCategory = gridView1.GetFocusedDataRow()["CategoryName"].ToString();

            InquiryTypeService inquiryTypeService = new InquiryTypeService();
            inquiryTypeService.InquiryType= InquiryType;
            inquiryTypeService.CusCastegory = inquiryTypeService.GetCustomerCategoryByName(CustomerCategory);

            inquiryTypeService.GetInqTypeByName();
            txtInquiryType.Text = inquiryTypeService.InquiryType;
            cmbxCusCat.SelectedValue = inquiryTypeService.CusCastegory;
            txtReminder1.Text = inquiryTypeService.Reminder1.ToString();
            txtReminder2.Text = inquiryTypeService.Reminder2.ToString();

            txtEscalation1.Text = inquiryTypeService.Escalation1.ToString();
            txtEscalation2.Text = inquiryTypeService.Escalation2.ToString();     
            intCompHours = inquiryTypeService.CompletionHours;
           // intCompHoursNonAgree = inquiryTypeService.CompletionHoursNonAgree;

            if (intCompHours >= 24)
            {
                int days = intCompHours / 24; 
                int Hours = intCompHours % 24;

                txtCompDays.Text = days.ToString();
                txtCompHours.Text = Hours.ToString();
            }
            else
            {
                txtCompDays.Text = "0";
                txtCompHours.Text = inquiryTypeService.CompletionHours.ToString();
            }
           
           
            btnAddInquiryType.Enabled = false;
            btnUpdateInquryType.Enabled = true;
            txtInquiryType.Enabled = false;
            tabInquiryType.SelectedTabPage = xtraTabPage2;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = true;

        }




    }
}