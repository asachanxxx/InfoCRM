using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using InfoPCMS.Services;
using System.IO;
using System.Diagnostics;


namespace InfoPCMS.Views
{
    public partial class frmUser : DevExpress.XtraEditors.XtraForm
    {
        public frmUser()
        {
            InitializeComponent();

            LoadData();
        }

        public void LoadData()
        {

            DataTable dtEmployee = InfoPCMS.db.executeSelectQuery("select * from Employee ");
            DataTable dtUsers = InfoPCMS.db.executeSelectQuery("SELECT T0.Username,T1.EmployeeName,T0.Status,T0.Category FROM HayleysPowerEngineeringCRM.dbo.Users T0 "
            +" LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 ON T0.EmpID = t1.Id ");

            cmbxEmployee.DataSource = dtEmployee;
            cmbxEmployee.ValueMember = "Id";
            cmbxEmployee.DisplayMember = "EmployeeName";

            cmbxEmployee.SelectedIndex = -1;

            gridUsers.DataSource = dtUsers;

            tbPgUsers.PageEnabled = true;
            tbPgAddUsers.PageEnabled = false;
            tbPgChangePswd.PageEnabled = false;
            
            tabUsers.SelectedTabPageIndex = 0;



        }

        private void btnAddUsers_Click(object sender, EventArgs e)
        {
            tbPgUsers.PageEnabled = false;
            tbPgAddUsers.PageEnabled = true;
            tbPgChangePswd.PageEnabled = false;

            tabUsers.SelectedTabPageIndex = 1;

            btnSaveUser.Text = "Save";

            ClearAddUser();

            ChangeVisibilty(true);
           

        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            tbPgUsers.PageEnabled = false;
            tbPgAddUsers.PageEnabled = false;
            tbPgChangePswd.PageEnabled = true;

            tabUsers.SelectedTabPageIndex = 2;

            txtUsernameChngPswd.Text = frmHome.UsrName;
            lblEmpName.Text = frmHome.EmpName;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveUser_Click(object sender, EventArgs e)
        {
            string strSelEmployee = cmbxEmployee.SelectedValue.ToString().Trim();
            lblErrorAddUser.Visible = false;
            
              if(btnSaveUser.Text=="Save"){

                  UserService users = new UserService();

                  if (users.CheckUserNameExist(txtUsername.Text.Trim()))
                  {
                      txtUsername.Focus();
                      XtraMessageBox.Show("Username Already Exist.", "Information");
                  }
                  else{

                  users.StrUsername = txtUsername.Text.Trim();
                  users.StrPassword = txtPwd.Text.Trim();
                  users.StrEmpId = cmbxEmployee.SelectedValue.ToString();
                  users.StrDisplayname = users.GetEmpNameByEmpId(cmbxEmployee.SelectedValue.ToString());
                  users.StrCategory = cmbUserCat.SelectedItem.ToString();

                  if (chkBxIsActive.Checked)
                  {
                      users.StrStatus = "Active";
                  }
                  else
                  {
                      users.StrStatus = "Deactive";
                  }
                 
                  if (txtPwd.Text == txtCfmPwd.Text)
                  {
                      Boolean result = users.AddUser();
                      // Boolean result2 = employee.addUser();
                      if (result)//&& result2
                      {

                          ClearAddUser();

                          XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_ADDED_INFORMATION(), "Information");


                      }

                  }
                  else
                  {
                      lblErrorAddUser.Visible = true;
                      txtCfmPwd.Text = "";
                      txtCfmPwd.Focus();
                  
                  }
                      
                  }
                 
                  
              
              }
            else if 
                  (btnSaveUser.Text=="Update"){

                      UserService users = new UserService();
                      int UserId = users.GetUserIdByEmpId(strSelEmployee);
                      if (users.CheckUserNameExist(txtUsername.Text.Trim(), UserId.ToString()))
                      {
                          txtUsername.Focus();
                          XtraMessageBox.Show("Username Already Exist.", "Information");
                      }
                      else
                      {                        

                          users.IntId = UserId;
                          users.StrUsername = txtUsername.Text.Trim();
                          users.StrPassword = txtPwd.Text.Trim();
                          users.StrEmpId = cmbxEmployee.SelectedValue.ToString();
                          users.StrDisplayname = users.GetEmpNameByEmpId(cmbxEmployee.SelectedValue.ToString());
                          users.StrCategory = cmbUserCat.SelectedItem.ToString();

                          if (chkBxIsActive.Checked)
                          {
                              users.StrStatus = "Active";
                          }
                          else
                          {
                              users.StrStatus = "Deactive";
                          }

                          Boolean result = users.UpdateUsers();
                          // Boolean result2 = employee.addUser();
                          if (result)//&& result2
                          {

                              ClearAddUser();

                              XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_UPDATED_INFORMATION(), "Information");

                              LoadData();
                          }
                      }
              }       

            
            



        }

        private void btnClearUser_Click(object sender, EventArgs e)
        {
            ClearAddUser();
           
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            UserService users = new UserService();

            int UserId = users.GetUserIdByUsename(txtUsernameChngPswd.Text.Trim());
            string curPswd = users.GetPasswordByUsename(txtUsernameChngPswd.Text.Trim());

            if (curPswd != txtCurPswdChngPswd.Text.Trim())
            {
                XtraMessageBox.Show("Invalid Current Passward", "Information");
                txtCurPswdChngPswd.Focus();

            }
            else if (txtNewPswdChngPswd.Text != txtConPswdChngPswd.Text)
            {

                XtraMessageBox.Show("Password Confirmation is incorrect ", "Information");
                txtConPswdChngPswd.Focus();

            }
            else
            {


                new DatabaseService().executeUpdateQuery("Update Users SET Password='" + txtNewPswdChngPswd.Text.Trim() + "' WHERE Id ='" + UserId.ToString() + "'");

                XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_UPDATED_INFORMATION(), "Information");
                ClearChangePswd();
                LoadData();
            
            }
        }

        private void btnChngePswdClear_Click(object sender, EventArgs e)
        {

        }

        private void btnBackAddUsers_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnBackChngePswd_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbxEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {

            string strSelEmployee = "";
            if (cmbxEmployee.SelectedIndex != -1)
            {
                cmbUserCat.SelectedIndex = -1;
                txtUsername.Text = "";
                chkBxIsActive.Checked = false;
                txtPwd.Text = "";
                txtCfmPwd.Text = "";

                btnSaveUser.Text = "Save";

                strSelEmployee = cmbxEmployee.SelectedValue.ToString().Trim();
            }

            UserService users = new UserService();
            if (users.CheckUserAccountExist(strSelEmployee))
            {
                users.GetUserDetailsByEmpId(strSelEmployee);

                cmbUserCat.SelectedItem = users.StrCategory;
                txtUsername.Text = users.StrUsername;
                if (users.StrStatus == "Active")
                {
                    chkBxIsActive.Checked = true;
                }

                btnSaveUser.Text = "Update";

                ChangeVisibilty(false);

                cmbxEmployee.Enabled = true;
            }
            else
            {
                ChangeVisibilty(true);
            
            }
            
            


        }

        private void gridVwUser_DoubleClick(object sender, EventArgs e)
        {
            UserService user = new UserService();

            int intUserId = user.GetUserIdByUsename(gridVwUser.GetFocusedDataRow()["Username"].ToString());
            user.GetUserDetailsById(intUserId.ToString());
            cmbxEmployee.SelectedValue = user.StrEmpId;
            cmbUserCat.SelectedItem = user.StrCategory;
            txtUsername.Text = user.StrUsername;
            if (user.StrStatus == "Active")
            {
                chkBxIsActive.Checked = true;
            }

            tbPgUsers.PageEnabled = false;
            tbPgAddUsers.PageEnabled = true;
            tbPgChangePswd.PageEnabled = false;

            tabUsers.SelectedTabPageIndex = 1;

            btnSaveUser.Text = "Update";

            ChangeVisibilty(false);

        }

        private void ClearAddUser()
        {
            cmbxEmployee.SelectedIndex = -1;
            cmbUserCat.SelectedIndex = -1;
            txtUsername.Text = "";
            chkBxIsActive.Checked = false;
            txtPwd.Text = "";
            txtCfmPwd.Text = "";
            

        }

        private void ClearChangePswd()
        {
            
            txtCurPswdChngPswd.Text = "";
            txtNewPswdChngPswd.Text = "";
            txtConPswdChngPswd.Text = "";  


        }

        private void ChangeVisibilty(bool Visibility)
        {
            cmbxEmployee.Enabled = Visibility;
            lblPswrd.Visible = Visibility;
            txtPwd.Visible = Visibility;
            lblConPswd.Visible = Visibility;
            txtCfmPwd.Visible = Visibility;
           // lblErrorAddUser.Visible = Visibility;
        }


    }
}