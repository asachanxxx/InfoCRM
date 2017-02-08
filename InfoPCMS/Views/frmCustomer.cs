using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using InfoPCMS.Services;
using System.Xml;

namespace InfoPCMS.Views
{
    public partial class frmCustomer : DevExpress.XtraEditors.XtraForm
    {
        public frmCustomer()
        {
            InitializeComponent();
            cmbxTaxType.SelectedIndex = 0;   
        }
        
        //.........................  
        private void frmCustomer_Load(object sender, EventArgs e)
        {   
            DataTable dt = InfoPCMS.db.executeSelectQuery("SELECT * FROM ServiceCenter");
            cmbxServiceCenter.DataSource = dt;
            cmbxServiceCenter.DisplayMember = "ServiceCenter";
            cmbxServiceCenter.ValueMember = "ServiceCenter";

            cmbxLocServiceCen.DataSource = dt;
            cmbxLocServiceCen.DisplayMember = "ServiceCenter";
            cmbxLocServiceCen.ValueMember = "ServiceCenter";
            
            cmbxServiceCenter.SelectedIndex = -1;

            DataTable dtCuscat = InfoPCMS.db.executeSelectQuery("SELECT * FROM CustomerCategory");
            cmbxCusCat.DataSource = dtCuscat;
            cmbxCusCat.DisplayMember = "CategoryName";
            cmbxCusCat.ValueMember = "Id";
            cmbxCusCat.SelectedIndex = -1;

            txtCusName.Enabled = false;

            cmbxTaxType.SelectedIndex = -1;

            clearAll();

            btnActiveCustomer.Visible = false;

            if (InfoPCMS.user.checkFunctionAuthentication("79"))
            {
                btnActiveCustomer.Visible = true;

            }

           // xtraTabPage2.PageEnabled = false;
           // xtraTabPage3.PageEnabled = false;

            //DataTable dtuser = new DatabaseService().executeSelectQuery("select Id,Category from Users  WHERE Username='" + InfoPCMS.user.+ "'");
            //if (dtuser.Rows.Count > 0)
            //{

            //    if (dtuser.Rows[0]["Category"].ToString().Trim() == "1")
            //    {
            //        btnActiveCustomer.Visible = true;

            //    }
            //}
        }               

        private void gridCustomers_Click(object sender, EventArgs e)
        {
            tblCustomersLocation.DataSource = new DatabaseService().executeSelectQuery("SELECT CustomerId,CustomerName,Location,SubLocation,ContactNo,Address FROM CustomerLocation WHERE CustomerId='" + gridView1.GetFocusedDataRow()["Id"].ToString() + "' ");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (simpleButton1.Text == "Add Location")
            {
                simpleButton1.Text = "Save";
                tabCustomer.SelectedTabPage = xtraTabPage3;
                xtraTabPage1.PageEnabled = false;
                xtraTabPage2.PageEnabled = false;
                xtraTabPage3.PageEnabled = true;
              //  xtraTabPage2.PageEnabled = false;
               // xtraTabPage3.PageEnabled = true;
                // 
                // lblLocationCustomerId.Text=gridView1.GetFocusedDataRow()["Id"].ToString();
                //lblLocationContactPersonName.Text=gridView1.GetFocusedDataRow()["CustomerName"].ToString();
                int customerid =Convert.ToInt32( gridView1.GetFocusedDataRow()["Id"].ToString());

                clearAllLocation();

                CustomerService customer = new CustomerService();
                customer.IntCustomerId = customerid;
                customer.GetCustomerDetailsByID();
                lblLocationCustomerId.Text = customerid.ToString();
                lblLocationCusName.Text = customer.StrCustomerName;
                txtLocationMainLocation.Text = customer.StrLocation;
               

                txtLocationSubLocation.Enabled = true;
                txtLocationSubLocation.Focus();
            }
            else
            {
                simpleButton1.Text = "Add Location";

                if (ValidationCustomerLocation())
                {
                    XtraMessageBox.Show(InfoPCMS.message.GET_EMPTY_FIELDS_ERROR(), "Error");

                }
                else
                {

                    CustomerService customer = new CustomerService();

                    customer.IntCustomerId = Convert.ToInt32(lblLocationCustomerId.Text.ToString().Trim());
                    customer.StrCustomerName = lblLocationCusName.Text.Trim();
                    customer.StrContactPerson = txtLocContactPer.Text.Trim();
                    customer.StrDesignation = txtLocDesignation.Text.Trim();
                    customer.StrContactNo = txtLocationContactNumber.Text.Trim();                   
                    customer.StrAddress = txtLocationAddress.Text.Trim();
                    customer.StrEmail = txtLocEmail.Text.Trim();
                    customer.StrFaxNo = txtLocFax.Text.Trim();                  
                    customer.StrLocation = txtLocationMainLocation.Text.Trim();
                    customer.StrSubLocation = txtLocationSubLocation.Text.ToString();
                    customer.StrServiceCenter = cmbxLocServiceCen.SelectedValue.ToString().Trim();
                    customer.StrDistanceToServiceCenter = txtLocDistanceSer.Text.Trim();
                    customer.StrDistanceFromColombo = txtLocDistanceCol.Text.Trim();
                    customer.StrHiltopSite = Convert.ToString(chkLocHiltop.Checked);
                    customer.StrNonMotorable = txtLocationNonMotorable.Text.Trim();
                    

                    Boolean result = customer.AddCustomerLocation();

                    if (result)
                    {
                        clearAllLocation();
                        clearAll();
                       
                        tabCustomer.SelectedTabPage = xtraTabPage1;
                        xtraTabPage1.PageEnabled = true;
                        xtraTabPage2.PageEnabled = false;
                        xtraTabPage3.PageEnabled = false;
                    }
                   

                    XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_ADDED_INFORMATION(), "Information");


                    
                }
            }


        }

        private void tblCustomersLocation_DoubleClick(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            btnAddCustomer.Enabled = false;

            txtLocationSubLocation.Enabled = false;


            DataTable dt = new DatabaseService().executeSelectQuery(String.Format("SELECT* FROM CustomerLocation WHERE CustomerId='{0}' AND Location='{1}' AND SubLocation='{2}'", gridView2.GetFocusedDataRow()["CustomerId"], gridView2.GetFocusedDataRow()["Location"], gridView2.GetFocusedDataRow()["SubLocation"]));
            if (dt.Rows.Count > 0)
            {
                DataTable dtSerCenter = new DatabaseService().executeSelectQuery("select * from ServiceCenter  WHERE Id='" + dt.Rows[0]["ServiceCenter"].ToString().Trim() + "'");
                string ServiceCenter = dtSerCenter.Rows[0]["ServiceCenter"].ToString().Trim();


                lblLocationCustomerId.Text = dt.Rows[0]["CustomerId"].ToString();
                lblLocationCusName.Text = dt.Rows[0]["CustomerName"].ToString();
                txtLocationMainLocation.Text = dt.Rows[0]["Location"].ToString();
                txtLocationSubLocation.Text = dt.Rows[0]["SubLocation"].ToString();
                txtLocContactPer.Text = dt.Rows[0]["ContactPerson"].ToString();
                txtLocDesignation.Text = dt.Rows[0]["Designation"].ToString();
                txtLocationAddress.Text = dt.Rows[0]["Address"].ToString();
                txtLocationContactNumber.Text = dt.Rows[0]["ContactNo"].ToString();
                txtLocFax.Text = dt.Rows[0]["Fax"].ToString();
                txtLocEmail.Text = dt.Rows[0]["Email"].ToString();
                cmbxLocServiceCen.SelectedValue = ServiceCenter.ToString();
                txtLocDistanceSer.Text = dt.Rows[0]["DistancetoServiceCenter"].ToString();
                txtLocDistanceCol.Text = dt.Rows[0]["DistanceFromColombo"].ToString();
                chkLocHiltop.Checked = Convert.ToBoolean(dt.Rows[0]["HiltopSite"].ToString());
                txtLocationNonMotorable.Text = dt.Rows[0]["NonMotorable"].ToString();
                tabCustomer.SelectedTabPage = xtraTabPage3;
                xtraTabPage1.PageEnabled = false;
                xtraTabPage2.PageEnabled = false;
                xtraTabPage3.PageEnabled = true;

               // xtraTabPage2.PageEnabled = false;
            }


        }

        private void btnAddInquiry_Click(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("4"))
            {
                txtCusName.Enabled = true;
                if (btnAddCustomer.Text == "Add Customer")
                {

                    btnAddCustomer.Text = "Save";
                    tabCustomer.SelectedTabPage = xtraTabPage2;
                    xtraTabPage1.PageEnabled = false;
                    xtraTabPage2.PageEnabled = true;
                    xtraTabPage3.PageEnabled = false;

                   // xtraTabPage2.PageEnabled = true;
                   // xtraTabPage3.PageEnabled = false;

                    lblCusId.Visible = false;
                    lblCusIdLbl.Visible = false;

                    lblCusId.Text = "";
                }
                else if (btnAddCustomer.Text == "Save")
                {

                    if (ValidationCustomer())
                    {

                        XtraMessageBox.Show(InfoPCMS.message.GET_EMPTY_FIELDS_ERROR(), "Error");

                    }
                    else
                    {
                        CustomerService customer = new CustomerService();

                        customer.StrCustomerName = txtCusName.Text.Trim();
                        customer.StrCustomerCategory =cmbxCusCat.SelectedValue.ToString();
                        customer.StrContactPerson = txtContactPerson.Text.Trim();
                        customer.StrDesignation = txtDesignation.Text.Trim();
                        customer.StrContactNo = txtContactNo.Text.Trim();
                        customer.StrMobileNo = txtMobiletNo.Text.Trim();
                        customer.StrAddress = txtAddress.Text.Trim();
                        customer.StrEmail = txtEmail.Text.Trim();
                        customer.StrFaxNo = txtFax.Text.Trim();
                        customer.StrTaxType = cmbxTaxType.SelectedItem.ToString();
                        customer.StrVATNo = txtVATNo.Text.Trim();
                        customer.StrSVATNo = txtSVATNo.Text.Trim();
                        customer.StrLocation = txtLocation.Text.Trim();
                        customer.StrServiceCenter = cmbxServiceCenter.SelectedValue.ToString().Trim();
                        customer.StrDistanceToServiceCenter = txtDistanceToServiceCenter.Text.Trim();
                        customer.StrDistanceFromColombo = txtDistanceFromColombo.Text.Trim();
                        customer.StrHiltopSite = Convert.ToString(chbHiltop.Checked);
                        customer.StrNonMotorable = txtNonMotorable.Text.Trim();
                        customer.StrIsActive = "False";
                        customer.StrIsTemp = "False";
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
                            }


                        }
                        clearAll();

                        XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_ADDED_INFORMATION(), "Information");
                    }
                }

            }
            else
            {
                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (InfoPCMS.user.checkFunctionAuthentication("5"))
            {
                if (tabCustomer.SelectedTabPage.Equals(xtraTabPage2))                    
                {

                    if (string.IsNullOrEmpty(cmbxTaxType.Text.Trim()))
                    {
                        errorProvider1.SetError(cmbxTaxType, "Please Select a value");
                        return;

                    }
                    

                    if (ValidationCustomer())
                    {

                        XtraMessageBox.Show(InfoPCMS.message.GET_EMPTY_FIELDS_ERROR(), "Error");

                    }
                    else
                    {

                        CustomerService customer = new CustomerService();

                        customer.StrCustomerName = txtCusName.Text.Trim();
                        customer.StrCustomerCategory = cmbxCusCat.SelectedValue.ToString().Trim();
                        customer.StrContactPerson = txtContactPerson.Text.Trim();
                        customer.StrDesignation = txtDesignation.Text.Trim();
                        customer.StrContactNo = txtContactNo.Text.Trim();
                        customer.StrMobileNo = txtMobiletNo.Text.Trim();
                        customer.StrAddress = txtAddress.Text.Trim();
                        customer.StrEmail = txtEmail.Text.Trim();
                        customer.StrFaxNo = txtFax.Text.Trim();
                        customer.StrTaxType = cmbxTaxType.SelectedItem.ToString();
                        customer.StrVATNo = txtVATNo.Text.Trim();
                        customer.StrSVATNo = txtSVATNo.Text.Trim();
                        customer.StrLocation = txtLocation.Text.Trim();
                        customer.StrServiceCenter = cmbxServiceCenter.SelectedValue.ToString().Trim();
                        customer.StrDistanceToServiceCenter = txtDistanceToServiceCenter.Text.Trim();
                        customer.StrDistanceFromColombo = txtDistanceFromColombo.Text.Trim();
                        customer.StrHiltopSite = Convert.ToString(chbHiltop.Checked);
                        customer.StrNonMotorable = txtNonMotorable.Text.Trim();

                        customer.IntCustomerId = Convert.ToInt32(lblCusId.Text.ToString().Trim());

                        DataTable dt = InfoPCMS.db.executeSelectQuery(String.Format("select * from Customer where Id='{0}' ", lblCusId.Text.ToString().Trim()));
                        if (dt.Rows.Count > 0)
                        {
                            //int intCusId = Convert.ToInt32(dt.Rows[0]["Id"].ToString());

                            customer.StrIsActive = dt.Rows[0]["IsActive"].ToString();
                            customer.StrIsTemp = dt.Rows[0]["IsTemp"].ToString();
                        }

                        // Boolean result = customer.addCustomer();
                        Boolean result = customer.UpdateCustomer();
                        if (result)
                        {
                            clearAll();
                            XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_UPDATED_INFORMATION(), "Information");
                        }
                    }
                }

                else if (tabCustomer.SelectedTabPage.Equals(xtraTabPage3))
                {
                    //XtraMessageBox.Show("Select CusLoc", "Error");

                    //Update Customer Location;

                    CustomerService customer = new CustomerService();

                    customer.IntCustomerId = Convert.ToInt32(lblLocationCustomerId.Text.ToString().Trim());
                    customer.StrCustomerName = lblLocationCusName.Text.Trim();
                    customer.StrLocation = txtLocationMainLocation.Text.Trim();
                    customer.StrSubLocation = txtLocationSubLocation.Text.Trim();
                    customer.StrContactPerson = txtLocContactPer.Text.Trim();
                    customer.StrDesignation = txtLocDesignation.Text.Trim();
                    customer.StrContactNo = txtLocationContactNumber.Text.Trim();                    
                    customer.StrAddress = txtLocationAddress.Text.Trim();
                    customer.StrEmail = txtLocEmail.Text.Trim();
                    customer.StrFaxNo = txtLocFax.Text.Trim();
                    customer.StrServiceCenter = cmbxLocServiceCen.SelectedValue.ToString().Trim();
                    customer.StrDistanceToServiceCenter = txtLocDistanceSer.Text.Trim();
                    customer.StrDistanceFromColombo = txtLocDistanceCol.Text.Trim();
                    customer.StrHiltopSite = Convert.ToString(chkLocHiltop.Checked);
                    customer.StrNonMotorable = txtLocationNonMotorable.Text.Trim();

                    Boolean result = customer.UpdateCustomerLocation();
                    if (result)
                    {
                        clearAll();
                        XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_UPDATED_INFORMATION(), "Information");
                    }
                   
                }

            }
            else
            {
                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");
            }
        }

        private void btnActiveCustomer_Click(object sender, EventArgs e)
        {
            CustomerService customer = new CustomerService();

            customer.IntCustomerId = Convert.ToInt32(lblCusId.Text.ToString().Trim());
            customer.StrCustomerName = txtCusName.Text.Trim();
            customer.StrContactNo = txtContactNo.Text.Trim();
            customer.StrAddress = txtAddress.Text.Trim();
            customer.StrEmail = txtEmail.Text.Trim();

            customer.StrTaxType = cmbxTaxType.SelectedItem.ToString();
            customer.StrVATNo = txtVATNo.Text;
            customer.StrSVATNo = txtSVATNo.Text;

            customer.StrMobileNo = txtMobiletNo.Text.Trim();
            customer.StrFaxNo = txtFax.Text.Trim();
            customer.StrLocation = txtLocation.Text.Trim();
            customer.StrContactPerson = txtContactPerson.Text;
            customer.StrDesignation = txtDesignation.Text;
            customer.StrDistanceToServiceCenter = txtDistanceToServiceCenter.Text;
            customer.StrDistanceFromColombo = txtDistanceFromColombo.Text;
            customer.StrHiltopSite = Convert.ToString(chbHiltop.Checked);
            customer.StrServiceCenter = cmbxServiceCenter.SelectedValue.ToString();
            customer.StrNonMotorable = txtNonMotorable.Text.Trim();
            customer.StrIsActive = "True";
            customer.StrIsTemp = "True";
            // Boolean result = customer.addCustomer();


            Boolean result = customer.UpdateCustomer();
            if (result)
            {
                clearAll();
                XtraMessageBox.Show(InfoPCMS.message.GET_CUSTOMER_ACTIVE(), "Information");
            }


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void tabCustomer_Click(object sender, EventArgs e)
        {

        }

        //..................
        private void clearAll()
        {

            txtCusName.Text = "";
            txtContactPerson.Text = "";
            txtDesignation.Text = "";
            txtContactNo.Text = "";
            txtAddress.Text = "";
            txtMobiletNo.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtDistanceFromColombo.Text = "";
            txtDistanceToServiceCenter.Text = "";
            cmbxTaxType.SelectedIndex = -1;
            txtVATNo.Text = "";
            txtSVATNo.Text = "";
            txtNonMotorable.Text = "";
            cmbxServiceCenter.SelectedIndex = -1;
            txtLocation.Text = "";
            chkLocHiltop.Checked = false;

            txtCusName.Enabled = true;
            btnUpdate.Enabled = false;
            btnAddCustomer.Enabled = true;
            btnAddCustomer.Text = "Add Customer";
            simpleButton1.Text = "Add Location";

            btnActiveCustomer.Enabled = false;

            CustomerService customers = new CustomerService();
            DataTable dt = customers.GetAllCustomerDetails();
            gridCustomers.DataSource = dt;

            tblCustomersLocation.DataSource = new DatabaseService().executeSelectQuery("SELECT CustomerId,CustomerName,Location,SubLocation,ContactNo,Address FROM CustomerLocation");

            tabCustomer.SelectedTabPage = xtraTabPage1;
            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = false;


        }

        private void clearAllLocation()
        {

            lblLocationCusName.Text = "";
            lblLocationCustomerId.Text = "";
            txtLocationAddress.Text = "";
            txtLocationContactNumber.Text = "";
            txtLocationMainLocation.Text = "";
            txtLocationSubLocation.Text = "";
            txtLocationSubLocation.Text = "";
            txtLocContactPer.Text = "";
            txtLocDesignation.Text = "";
            txtLocationContactNumber.Text = "";
            txtLocFax.Text = "";
            txtLocEmail.Text = "";
            cmbxLocServiceCen.SelectedValue = 0;
            txtLocationNonMotorable.Text = "";
            txtLocDistanceSer.Text = "";
            txtLocDistanceCol.Text = "";
            chkLocHiltop.Checked = false;


        }

        private void selectCustomer(object sender, EventArgs e)
        {
            txtCusName.Enabled = false;
            if (CheckCusIsActive(gridView1.GetFocusedDataRow()["Id"].ToString()))
            {
                btnActiveCustomer.Enabled = false;
            }
            else
            {
                btnActiveCustomer.Enabled = true;

            }

            //btnActiveCustomer.Enabled = true;
            btnUpdate.Enabled = true;
            btnAddCustomer.Enabled = false;
            // txtCusName.Enabled = false;
            int customerid = Convert.ToInt32(gridView1.GetFocusedDataRow()["Id"].ToString());


            CustomerService customer = new CustomerService();
            customer.IntCustomerId = customerid;
            customer.GetCustomerDetailsByID();

            DataTable dtSerCenter = new DatabaseService().executeSelectQuery("select * from ServiceCenter  WHERE Id='" + customer.StrServiceCenter.Trim() + "'");
            string ServiceCenter = dtSerCenter.Rows[0]["ServiceCenter"].ToString().Trim();

            lblCusIdLbl.Visible = true;
            lblCusId.Visible = true;

            lblCusId.Text = customerid.ToString();
            txtCusName.Text = customer.StrCustomerName.Trim();
            cmbxCusCat.SelectedValue = customer.StrCustomerCategory;
            txtAddress.Text = customer.StrAddress.Trim();
            txtEmail.Text = customer.StrEmail.Trim();
            txtContactNo.Text = customer.StrContactNo.Trim();
            cmbxTaxType.SelectedItem = customer.StrTaxType;
            txtVATNo.Text = customer.StrVATNo;
            txtSVATNo.Text = customer.StrSVATNo;
            txtMobiletNo.Text = customer.StrMobileNo;
            txtFax.Text = customer.StrFaxNo;
            txtLocation.Text = customer.StrLocation;
            txtContactPerson.Text = customer.StrContactPerson;
            txtDesignation.Text = customer.StrDesignation;
            cmbxServiceCenter.SelectedValue = ServiceCenter.Trim();
            txtDistanceToServiceCenter.Text = customer.StrDistanceToServiceCenter;
            txtDistanceFromColombo.Text = customer.StrDistanceFromColombo;
            chbHiltop.Checked = Convert.ToBoolean(customer.StrHiltopSite);
            txtNonMotorable.Text = customer.StrNonMotorable;
            tabCustomer.SelectedTabPage = xtraTabPage2;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = true;
            xtraTabPage3.PageEnabled = false;

           // xtraTabPage3.PageEnabled = false;

            txtCusName.Focus();
        }

        private bool ValidationCustomer()
        {
            bool blValidation = false;

            if (string.IsNullOrEmpty(txtCusName.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                blValidation = true;
            }
            else if ((cmbxServiceCenter.SelectedItem == null) || string.IsNullOrEmpty(cmbxServiceCenter.SelectedItem.ToString()))
            {
                blValidation = true;
            }


            return blValidation;
        }

        private bool ValidationCustomerLocation()
        {
            bool blValidation = false;

            if (string.IsNullOrEmpty(txtLocationMainLocation.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(txtLocationSubLocation.Text.Trim()))
            {
                blValidation = true;
            }
            else if ((cmbxLocServiceCen.SelectedItem == null) || string.IsNullOrEmpty(cmbxLocServiceCen.SelectedItem.ToString()))
            {
                blValidation = true;
            }


            return blValidation;
        }

        private bool CheckCusIsActive(string strCusID)
        {
            bool blIsCusActive = false;

            DataTable dtCustomer = new DatabaseService().executeSelectQuery("SELECT Id,IsActive FROM Customer WHERE Id = '" + strCusID + "' ");

            if (dtCustomer.Rows.Count > 0)
            {
                if (dtCustomer.Rows[0]["IsActive"].ToString() == "True")
                {

                    blIsCusActive = true;
                }

            }
            return blIsCusActive;

        }

        private void onlyAllowNumerics(object sender, KeyPressEventArgs e)
        {

            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.')
            {

            }
            else
            {

                e.Handled = e.KeyChar != (char)Keys.Back;
            }

        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {

            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

            if (txtEmail.Text.Length > 0)
            {

                if (!rEMail.IsMatch(txtEmail.Text))
                {

                    XtraMessageBox.Show("Enter Valid Email Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtEmail.SelectAll();

                    e.Cancel = true;

                }

            }
        }

        private void lblLocationCusName_Click(object sender, EventArgs e)
        {

        }




    }
}