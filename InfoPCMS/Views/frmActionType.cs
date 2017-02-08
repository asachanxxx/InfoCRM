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
    public partial class frmActionType : DevExpress.XtraEditors.XtraForm
    {
        public string subActionId = "";

        public frmActionType()
        {
            InitializeComponent();

            try
            {

                DataTable dt2 = InfoPCMS.db.executeSelectQuery("select * from ActionType");
                // dt.Columns.Add("ConcatenatedField", typeof(string), "CustomerName + ' : ' + Address"); 

                //cmbBxActionType.DataSource = dt2;
                //cmbBxActionType.ValueMember = "ActionType";
                //cmbBxActionType.DisplayMember = "ActionType";

                //cmbBxCheckList.DataSource = dt2;
                //cmbBxCheckList.ValueMember = "ActionType";
                //cmbBxCheckList.DisplayMember = "ActionType";
            }
            catch (Exception ex)
            {

            }
        }

        private void clearAll()
        {

            ActionTypeService actionType = new ActionTypeService();

            txtActionType.Text = "";
            txtDurationDays.Text = "";
            txtDurationHours.Text = "";
            //chkBxDateTimeMan.Checked = false;
            txtFirstReminder.Text = "";
            txtEscaltion1.Text = "";
            txtEscaltion2.Text = "";

            //cmbBxActionType.SelectedIndex = -1;
            txtActionTypeDesc.Text = "";
            //cmbBxCheckList.SelectedIndex = -1;
            txtActionTypeDesc.Text = "";
            //cmbBxCheckList.SelectedIndex = -1;
            txtCheckListDesc.Text = "";
            //txtCheckListDurationDays.Text = "";
            //txtCheckListDurationHours.Text = "";
            txtChecklistSeqNo.Text = "";
            txtCheckListEscPer.Text = "";

            btnAddActionType.Text = "Add Action Type";
            btnAddCheckList.Text = "Add Checklist";

            btnUpdate.Enabled = false;
            DataTable dt = actionType.getAllActionTypeDetails();
            gridActionType.DataSource = dt;
          //  tblActionTypeCheckList.DataSource = new DatabaseService().executeSelectQuery("SELECT ActionTypeID,ActionType,SubActionType,SeqNo FROM ActionTypeCheckList");
            tabActionType.SelectedTabPage = xtraTabPage1;
            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = false;


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void frmActionType_Load(object sender, EventArgs e)
        {
            clearAll();
        }

        private void btnAddActionType_Click(object sender, EventArgs e)
        {
            if (InfoPCMS.user.checkFunctionAuthentication("77"))
            {

                if (btnAddActionType.Text == "Add Action Type")
                {

                    btnAddActionType.Text = "Save";
                    tabActionType.SelectedTabPage = xtraTabPage2;
                    xtraTabPage1.PageEnabled = false;
                    xtraTabPage2.PageEnabled = true;
                    xtraTabPage3.PageEnabled = false;

                }
                else if (btnAddActionType.Text == "Save")
                {
                    ActionTypeService actionType = new ActionTypeService();
                    if (string.IsNullOrEmpty(txtDurationDays.Text.ToString()))
                    {

                        txtDurationDays.Text = "0";
                    }
                    if (string.IsNullOrEmpty(txtDurationHours.Text.ToString()))
                    {

                        txtDurationHours.Text = "0";
                    }

                    if (this.ValidationActionType())
                    {

                        XtraMessageBox.Show(InfoPCMS.message.GET_EMPTY_FIELDS_ERROR(), "Error");
                    }
                    else
                    {


                        int intDurations = (Convert.ToInt32(txtDurationDays.Text.ToString()) * 24 + Convert.ToInt32(txtDurationHours.Text.ToString()));

                        actionType.ActionType = txtActionType.Text.Trim();
                        actionType.Duration = intDurations;
                        actionType.IntFirstReminder = Convert.ToInt32(txtFirstReminder.Text.ToString().Trim());
                        actionType.Intescaltion1 = Convert.ToInt32(txtEscaltion1.Text.ToString().Trim());
                        actionType.Intescaltion2 = Convert.ToInt32(txtEscaltion2.Text.ToString().Trim());
                        //actionType.IsDateTimeMandatory = Convert.ToString(chkBxDateTimeMan.Checked);

                        Boolean result = actionType.addActionType();

                        if (result)
                        {
                            //DataTable dt = InfoPCMS.db.executeSelectQuery(String.Format("select * from ActionType where ActionType='{0}' ", txtActionType.Text));
                            //if (dt.Rows.Count > 0)
                            //{
                            //    new DatabaseService().executeUpdateQuery(String.Format("INSERT INTO CustomerLocation VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", dt.Rows[0]["Id"], dt.Rows[0]["CustomerName"], dt.Rows[0]["NbtChargeble"], dt.Rows[0]["NbtChargeble"], dt.Rows[0]["ContactNumber"], dt.Rows[0]["Address"], dt.Rows[0]["Company"], dt.Rows[0]["Designation"], dt.Rows[0]["Faxno"], dt.Rows[0]["Email"], dt.Rows[0]["ServiceCenter"], dt.Rows[0]["DistancetoServiceCenter"], dt.Rows[0]["DistanceFromColombo"], dt.Rows[0]["HiltopSite"], dt.Rows[0]["NonMotorable"]));
                            //}
                            clearAll();

                            XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_ADDED_INFORMATION(), "Information");

                            //txtActionTypeDesc.Text  ""
                            // cmbBxCheckList.DataSource = null;

                            // DataTable dt2 = InfoPCMS.db.executeSelectQuery("select * from ActionType");
                            // dt.Columns.Add("ConcatenatedField", typeof(string), "CustomerName + ' : ' + Address"); 

                            //cmbBxActionType.DataSource = dt2;
                            //cmbBxActionType.ValueMember = "ActionType";
                            //cmbBxActionType.DisplayMember = "ActionType";

                            //cmbBxCheckList.DataSource = dt2;
                            //cmbBxCheckList.ValueMember = "ActionType";
                            // cmbBxCheckList.DisplayMember = "ActionType";


                        }
                    }
                }
            }
        }

        private void btnAddCheckList_Click(object sender, EventArgs e)
        {
           // int intSubActionDurations = (Convert.ToInt32(txtCheckListDurationDays.Text.ToString()) * 24 + Convert.ToInt32(txtCheckListDurationHours.Text.ToString()));

            String ActionId;

            if (btnAddCheckList.Text == "Add Checklist")
            {
                btnAddCheckList.Text = "Save";
                tabActionType.SelectedTabPage = xtraTabPage3;
                xtraTabPage1.PageEnabled = false;
                xtraTabPage2.PageEnabled = false;
                xtraTabPage3.PageEnabled = true;

                ActionId = gridView1.GetFocusedDataRow()["Id"].ToString();
                String ActionType = gridView1.GetFocusedDataRow()["ActionType"].ToString();

                //cmbBxActionType.SelectedValue = ActionType.ToString();
                txtActionTypeDesc.Text = ActionType.ToString();
                txtActionTypeDesc.Enabled = false;


            }
           else if ( btnAddCheckList.Text == "Save")
                
            {
               

                //if (string.IsNullOrEmpty(txtCheckListDurationDays.Text.ToString()))
                //{

                //    txtCheckListDurationDays.Text = "0";
                //}
                //if (string.IsNullOrEmpty(txtCheckListDurationHours.Text.ToString()))
                //{

                //    txtCheckListDurationHours.Text = "0";
                //}


              //  int intSubActionDurations = (Convert.ToInt32(txtCheckListDurationDays.Text.ToString()) * 24 + Convert.ToInt32(txtCheckListDurationHours.Text.ToString()));

                if (this.ValidationCheckList())
                {

                    XtraMessageBox.Show(InfoPCMS.message.GET_EMPTY_FIELDS_ERROR(), "Error");
                }
                else
                {
                    new DatabaseService().executeUpdateQuery("INSERT INTO ActionTypeCheckList VALUES('" + this.getActionTypeID(txtActionTypeDesc.Text.ToString()) + "','" + txtActionTypeDesc.Text.ToString().Trim() + "','" + txtCheckListDesc.Text.ToString() + "','" + txtChecklistSeqNo.Text + "','" + txtCheckListEscPer.Text.Trim() + "')");
                   // MessageBox.Show("Record Saved Successfully");
                    XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_ADDED_INFORMATION(), "Information");

                    //cmbBxActionType.SelectedValue = -1;
                    txtCheckListDesc.Text = "";
                    txtActionTypeDesc.Text = "";
                    //cmbBxCheckList.SelectedValue = -1;
                    // txtCheckListDurationDays.Text = "";
                    // txtCheckListDurationHours.Text = "";
                    txtChecklistSeqNo.Text = "";
                    txtCheckListEscPer.Text = "";

                    tabActionType.SelectedTabPage = xtraTabPage1;
                    xtraTabPage1.PageEnabled = true;
                    xtraTabPage2.PageEnabled = false;
                    xtraTabPage3.PageEnabled = false;

                    clearAll();
                }
            
            }
        }
        
        public string getActionTypeID(string strActionType)
        {
            try
            {

                DataTable dt = InfoPCMS.db.executeSelectQuery("select Id from ActionType where ActionType ='" + strActionType + "'");
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void gridActionType_Click(object sender, EventArgs e)
        {
            tblActionTypeCheckList.DataSource = new DatabaseService().executeSelectQuery("SELECT SubActionType,SeqNo FROM ActionTypeCheckList WHERE ActionType='" + gridView1.GetFocusedDataRow()["ActionType"].ToString() + "' ORDER BY SeqNo ");
        
        }

        private void selectActionType(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            btnAddActionType.Enabled = false;
            txtActionType.Enabled = false;
            String ActionTypeid = gridView1.GetFocusedDataRow()["Id"].ToString();
            ActionTypeService actionType = new ActionTypeService();
            actionType.getActionTypeDetailsByID(ActionTypeid);

            txtActionType.Text = actionType.ActionType.ToString().Trim();

            int intDuration;

            intDuration = actionType.Duration;

            if (intDuration >= 24)
            {
                int days = intDuration / 24;
                int Hours = intDuration % 24;

                txtDurationDays.Text = days.ToString();
                txtDurationHours.Text = Hours.ToString();
            }
            else
            {
                txtDurationDays.Text = "0";
                txtDurationHours.Text = actionType.Duration.ToString();
            }

            txtFirstReminder.Text = actionType.IntFirstReminder.ToString();
            txtEscaltion1.Text = actionType.Intescaltion1.ToString();
            txtEscaltion2.Text = actionType.Intescaltion2.ToString();
            
            tabActionType.SelectedTabPage = xtraTabPage2;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = true;
            xtraTabPage3.PageEnabled = false;
        }

        private bool ValidationActionType()
        {
            bool blValidation = false;

            if (string.IsNullOrEmpty(txtActionType.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(txtDurationDays.Text.Trim()) && string.IsNullOrEmpty(txtDurationHours.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(txtFirstReminder.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(txtEscaltion1.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(txtEscaltion2.Text.Trim()))
            {
                blValidation = true;
            }
            return blValidation;
        }

        private bool ValidationCheckList()
        {
            bool blValidation = false;

            if (string.IsNullOrEmpty(txtCheckListDesc.Text.Trim()))
            {
                blValidation = true;
            }
           
            else if (string.IsNullOrEmpty(txtCheckListEscPer.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(txtChecklistSeqNo.Text.Trim()))
            {
                blValidation = true;
            }
          

            return blValidation;
        }
    }
}