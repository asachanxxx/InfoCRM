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
    public partial class frmEmployee : DevExpress.XtraEditors.XtraForm
    {
        public frmEmployee()
        {
            InitializeComponent();

            clearAll();
        }

        private void btnAddInquiry_Click(object sender, EventArgs e)
        {

            if(InfoPCMS.user.checkFunctionAuthentication("1")){

            if(btnAddEmployee.Text=="Add Employee"){
            
            btnAddEmployee.Text = "Save";
            tabEmployee.SelectedTabPage = xtraTabPage2;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = true;

            }
            else if (btnAddEmployee.Text == "Save")
            {

                if (ValidationEmployee())
                {
                    XtraMessageBox.Show(InfoPCMS.message.GET_EMPTY_FIELDS_ERROR(), "Error");
                }
                else
                {

                    EmployeeService employee = new EmployeeService();
                    employee.EmployeeName = txtEmployee.Text.Trim();
                    employee.Address = txtAddress.Text.Trim();
                    employee.Email = txtEmail.Text.Trim();
                    employee.ContactNo = txtContactNo.Text.Trim();
                    employee.JobArea = txtJobArea.Text.Trim();
                    employee.Department = txtDepartment.Text.Trim();
                    employee.Category = txtCategory.SelectedItem.ToString();
                    Boolean result = employee.addEmployee();
                    // Boolean result2 = employee.addUser();
                    if (result)//&& result2
                    {

                        clearAll();

                        XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_ADDED_INFORMATION(), "Information");


                    }
                }
            
            }

            }else{
            
            XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");
            
            
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void clearAll() {


            txtEmployee.Text = "";
            txtDepartment.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtContactNo.Text = "";
            txtJobArea.Text = "";
            txtCategory.SelectedIndex = 0;

            btnAddEmployee.Enabled = true;
            btnAddEmployee.Text = "Add Employee";
            btnUpdate.Enabled = false;
            btnInactive.Enabled = false;
            txtEmployee.Enabled = true;

            EmployeeService employees = new EmployeeService();
            DataTable dt = employees.getAllEmployeeDetails();
            gridItems.DataSource = dt;

            tabEmployee.SelectedTabPage = xtraTabPage1;
            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
        
        }

        private void selectEmployee(object sender, EventArgs e)
        {
            EmployeeService employee = new EmployeeService();
            employee.EmployeeName = gridView1.GetFocusedDataRow()["EmployeeName"].ToString();
            employee.getEmployeeDetailsByName();

            txtEmployee.Text = employee.EmployeeName.Trim();
            txtAddress.Text = employee.Address.Trim();
            txtEmail.Text = employee.Email.Trim();
            txtJobArea.Text = employee.JobArea.Trim();
            txtDepartment.Text = employee.Department.Trim();
            txtContactNo.Text = employee.ContactNo.Trim();
            txtCategory.SelectedItem = employee.Category;
            btnUpdate.Enabled = true;
            btnInactive.Enabled = true;
            btnAddEmployee.Enabled = false;
            txtEmployee.Enabled = false;
            tabEmployee.SelectedTabPage = xtraTabPage2;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (InfoPCMS.user.checkFunctionAuthentication("2"))
            {

                if (ValidationEmployee())
                {
                    XtraMessageBox.Show(InfoPCMS.message.GET_EMPTY_FIELDS_ERROR(), "Error");
                }
                else
                {

                    EmployeeService employee = new EmployeeService();
                    employee.EmployeeName = txtEmployee.Text.Trim();
                    employee.Address = txtAddress.Text.Trim();
                    employee.Email = txtEmail.Text.Trim();
                    employee.ContactNo = txtContactNo.Text.Trim();
                    employee.JobArea = txtJobArea.Text.Trim();
                    employee.Department = txtDepartment.Text.Trim();
                    employee.Category = txtCategory.SelectedItem.ToString();
                    Boolean result = employee.updateEmployee();
                    if (result)
                    {

                        clearAll();

                        XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_UPDATED_INFORMATION(), "Information");


                    }
                }

            }
            else {


                XtraMessageBox.Show(InfoPCMS.message.GET_NOT_AUTHORIZED_ERROR(), "Error");
            
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

        private void btnInactive_Click(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("2"))
            {

                EmployeeService employee = new EmployeeService();
                employee.EmployeeName = txtEmployee.Text.Trim();
                employee.Address = txtAddress.Text.Trim();
                employee.Email = txtEmail.Text.Trim();
                employee.ContactNo = txtContactNo.Text.Trim();
                employee.JobArea = txtJobArea.Text.Trim();
                employee.Department = txtDepartment.Text.Trim();
                employee.Category = txtCategory.SelectedItem.ToString();
                Boolean result = employee.inactiveEmployee();
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

        private void frmEmployee_Load(object sender, EventArgs e)
        {

        }


        private bool ValidationEmployee()
        {
            bool blValidation = false;

            if (string.IsNullOrEmpty(txtEmployee.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(txtCategory.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                blValidation = true;
            }
           
            return blValidation;
        }

       
    }
}