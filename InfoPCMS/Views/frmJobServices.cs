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
using System.IO;
using System.Diagnostics;
using InfoCrm.Tools;


namespace InfoPCMS.Views
{
    public partial class frmJobServices : DevExpress.XtraEditors.XtraForm
    {

        List<FileModel> FileList = new List<FileModel>();

        public frmJobServices()
        {
            InitializeComponent();
            LoadData();

            DataTable dt3 = InfoPCMS.db.executeSelectQuery("select * from Employee");
            DataRow row = dt3.NewRow();
            row["EmployeeName"] = "------Please select Employee------";
            dt3.Rows.InsertAt(row, 0);          

            cmbBxJobDoneBy.DataSource = dt3;
            cmbBxJobDoneBy.ValueMember = "Id";
            cmbBxJobDoneBy.DisplayMember = "EmployeeName";

            DataTable dt4 = InfoPCMS.db.executeSelectQuery("select * from Employee");
            DataRow row2 = dt4.NewRow();
            row2["EmployeeName"] = "------Please select Employee------";
            dt4.Rows.InsertAt(row2, 0);
            cmbRepairDoneBy.DataSource = dt4;
            cmbRepairDoneBy.ValueMember = "Id";
            cmbRepairDoneBy.DisplayMember = "EmployeeName";

            DataTable dt5 = InfoPCMS.db.executeSelectQuery("select * from Employee");
            DataRow row3= dt5.NewRow();
            row3["EmployeeName"] = "------Please select Employee------";
            dt5.Rows.InsertAt(row3, 0);
            cmbRepairAssign.DataSource = dt5;
            cmbRepairAssign.ValueMember = "Id";
            cmbRepairAssign.DisplayMember = "EmployeeName";

            xtraTabControl3.SelectedTabPageIndex = 0;
            xtraTabPage10.PageEnabled = true;
            xtraTabPage11.PageEnabled = false;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = false;

        }

        private void LoadData()
        {
            try
            {
                if (Views.frmHome.EmpCat == "LEVEL 3")
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
                    +" LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 ON T0.ResponsiblePerson = T1.Id " 
                    +" LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
                    +" LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T3 ON T0.Location = T3.Id "
                    +" LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T4 ON T0.CustomerCategory = T4.Id "
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

                    DataTable dtEmployee = new DatabaseService().executeSelectQuery("select Id from Employee  WHERE EmployeeName ='" + Views.frmHome.EmpName + "'");
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

        //public UploadedFile CurrentFile
        //{
        //    get { return helper == null ? null : helper.CurrentFile; }
        //}

        private void gridVwUpcomming_DoubleClick(object sender, EventArgs e)
        {
            ClearUpdateSer();
            RefreshFileList();
            lblAgreementID.Text = gridVwUpcomming.GetFocusedDataRow()["AgreementID"].ToString();
            lblGeneratorNumber.Text = gridVwUpcomming.GetFocusedDataRow()["GeneratorId"].ToString();
            lblServiceName.Text = gridVwUpcomming.GetFocusedDataRow()["Type"].ToString();
            xtraTabControl3.SelectedTabPageIndex = 1;
            xtraTabPage10.PageEnabled = false;
            xtraTabPage11.PageEnabled = true;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = false;

            DataTable Completed = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule WHERE  AgreementID = '" + lblAgreementID.Text.Trim() + "' AND GeneratorId = '" + lblGeneratorNumber.Text.Trim() + "'");
            gridServiceActions.DataSource = Completed;
        }

        private void gridVwDueSer_DoubleClick(object sender, EventArgs e)
        {
            ClearUpdateSer();
            RefreshFileList();
            lblAgreementID.Text = gridVwDueSer.GetFocusedDataRow()["AgreementID"].ToString();
            lblGeneratorNumber.Text = gridVwDueSer.GetFocusedDataRow()["GeneratorId"].ToString();
            lblServiceName.Text = gridVwDueSer.GetFocusedDataRow()["Type"].ToString();
            xtraTabControl3.SelectedTabPageIndex = 1;
            xtraTabPage10.PageEnabled = false;
            xtraTabPage11.PageEnabled = true;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = false;

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

            try
            {

                DataTable SerAgreement = new DatabaseService().executeSelectQuery("SELECT * FROM SeviceAgreement WHERE  AgreementId = '" + lblAgreementID.Text.Trim() + "' AND GeneratorId = '" + lblGeneratorNumber.Text.Trim() + "'");

                string result = lblServiceName.Text.ToString().Substring(0, 3);
                int PendingSer = 0;
                int PendingIns = 0;
                if (SerAgreement.Rows.Count>0)
                {
                    PendingSer = Convert.ToInt32(SerAgreement.Rows[0]["PendingService"].ToString());
                    PendingIns = Convert.ToInt32(SerAgreement.Rows[0]["PendingInspection"].ToString());
                }
                //if (SerAgreement.Rows[0]["PendingInspection"] != null)
                //{
                    
                //}
                
                

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
                            strJobDoneBy = GetEmpId(Views.frmHome.EmpName); ;
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

                        SaveFileList(lblAgreementID.Text);

                        MessageBox.Show("Services's Status Successfuly Updated.");
                    }
                }

                DataTable Completed = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule WHERE  AgreementID = '" + lblAgreementID.Text.Trim() + "' AND GeneratorId = '" + lblGeneratorNumber.Text.Trim() + "'");
                gridServiceActions.DataSource = Completed;

                LoadData();

                xtraTabControl3.SelectedTabPageIndex = 0;
                xtraTabPage10.PageEnabled = true;
                xtraTabPage11.PageEnabled = false;
                xtraTabPage1.PageEnabled = false;
                xtraTabPage2.PageEnabled = false;
                xtraTabPage3.PageEnabled = false;
            }
            catch (System.Data.SqlClient.SqlException ex) {
                MessageBox.Show("Please select a record first",Globals.MessageCaption,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
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

            RefreshFileList();
            DataTable CompletedSer = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule WHERE  AgreementID = '" + lblCompAgreeID.Text.Trim() + "' AND GeneratorId = '" + lblCompGenNo.Text.Trim() + "' AND Type = '" + lblCompSerName.Text.Trim() + "' ");

            lblCompJobDoneBy.Text = CompletedSer.Rows[0]["JobDoneBy"].ToString();
            dtCompJobDoneDate.Text = CompletedSer.Rows[0]["JobDoneDate"].ToString();
            txtCompJobDetails.Text = CompletedSer.Rows[0]["JobDetails"].ToString();
            txtCompJobSheet.Text = CompletedSer.Rows[0]["JobSheetPath"].ToString();

            xtraTabControl3.SelectedTabPageIndex = 3;
            xtraTabPage10.PageEnabled = false;
            xtraTabPage11.PageEnabled = false;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = true;
            xtraTabPage3.PageEnabled = false;
        }

        private void btnView_Click(object sender, EventArgs e)
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

            xtraTabControl3.SelectedTabPageIndex = 3;
            xtraTabPage10.PageEnabled = false;
            xtraTabPage11.PageEnabled = false;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = true;
            xtraTabPage3.PageEnabled = false;
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
            xtraTabControl3.SelectedTabPageIndex = 2;

            xtraTabPage10.PageEnabled = false;
            xtraTabPage11.PageEnabled = false;
            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = false;
        }

        private void btnBackCompSerDeat_Click(object sender, EventArgs e)
        {
            xtraTabControl3.SelectedTabPageIndex = 2;

            xtraTabPage10.PageEnabled = false;
            xtraTabPage11.PageEnabled = false;
            xtraTabPage1.PageEnabled = true;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = false;
        }

        private void btnBackCompList_Click(object sender, EventArgs e)
        {
            xtraTabControl3.SelectedTabPageIndex = 0;

            xtraTabPage10.PageEnabled = true;
            xtraTabPage11.PageEnabled = false;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = false;
        }

        private void btnBackUpdateSer_Click(object sender, EventArgs e)
        {
            xtraTabControl3.SelectedTabPageIndex = 0;

            xtraTabPage10.PageEnabled = true;
            xtraTabPage11.PageEnabled = false;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = false;
        }

        private void btnRepComplete_Click(object sender, EventArgs e)
        {
            string strRepDoneBy = "";
            if (cmbRepairDoneBy.SelectedIndex == 0)
            {
                strRepDoneBy = GetEmpId(Views.frmHome.EmpName); ;
            }
            else
            {
                strRepDoneBy = GetEmpId(cmbRepairDoneBy.Text.Trim());
            }


        new DatabaseService().executeUpdateQuery("UPDATE RecommendedRepairs SET IsRepairDone = 'True',JobDoneDate='"+dtRepairDoneDate.EditValue.ToString()+"', "
          + " JobDoneBy='" + strRepDoneBy.Trim()+ "',JobDetails='" + txtRepairDetails.Text.Trim() + "',Status='Complete' "
          + " WHERE AgreementID = '" + lblRepAgreeId.Text.Trim() + "' AND RelatedService = '" + lblRepRelatedServ.Text.Trim() + "' AND GeneratorID='" + lblRepGenNo.Text.Trim() + "' AND Customer='" + lblRepCusId.Text.Trim() + "' AND Location='" + lblRepLocId.Text.Trim() + "'");

        ClearRecRep();
        MessageBox.Show("Recomanded Repair's Status Successfuly Updated.");

        xtraTabControl3.SelectedTabPageIndex = 0;

        xtraTabPage10.PageEnabled = true;
        xtraTabPage11.PageEnabled = false;
        xtraTabPage1.PageEnabled = false;
        xtraTabPage2.PageEnabled = false;
        xtraTabPage3.PageEnabled = false;

        LoadData();
        }

        private void btnRepBack_Click(object sender, EventArgs e)
        {
            xtraTabControl3.SelectedTabPageIndex = 0;

            xtraTabPage10.PageEnabled = true;
            xtraTabPage11.PageEnabled = false;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = false;
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

            xtraTabControl3.SelectedTabPageIndex = 4;

            xtraTabPage10.PageEnabled = false;
            xtraTabPage11.PageEnabled = false;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = true;

            DataTable Completed = new DatabaseService().executeSelectQuery("SELECT T0.*,T1.EmployeeName,T2.CustomerName,T3.SubLocation,T4.CategoryName,T5.EmployeeName As JobDoneByName FROM HayleysPowerEngineeringCRM.dbo.RecommendedRepairs T0 "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 ON T0.ResponsiblePerson = T1.Id "
   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T5 ON T0.JobDoneBy = T5.Id "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T3 ON T0.Location = T3.Id "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T4 ON T0.CustomerCategory = T4.Id WHERE  T0.GeneratorID = '" + lblRepGenNo.Text.Trim() + "' AND T0.Customer='" + lblRepCusId.Text.Trim() + "' AND T0.Location ='" + lblRepLocId.Text.Trim() + "' AND T0.AgreementID = '" + lblRepAgreeId.Text.Trim() + "'");
            grdRecmonedReparis.DataSource = Completed;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {            

           



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

            xtraTabControl3.SelectedTabPageIndex = 4;

            xtraTabPage10.PageEnabled = false;
            xtraTabPage11.PageEnabled = false;
            xtraTabPage1.PageEnabled = false;
            xtraTabPage2.PageEnabled = false;
            xtraTabPage3.PageEnabled = true;
            
            DataTable Completed = new DatabaseService().executeSelectQuery("SELECT T0.*,T1.EmployeeName,T2.CustomerName,T3.SubLocation,T4.CategoryName,T5.EmployeeName As JobDoneByName FROM HayleysPowerEngineeringCRM.dbo.RecommendedRepairs T0 "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T1 ON T0.ResponsiblePerson = T1.Id "
   + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T5 ON T0.JobDoneBy = T5.Id "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T2 ON T0.Customer = T2.Id "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T3 ON T0.Location = T3.Id "
  + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerCategory T4 ON T0.CustomerCategory = T4.Id WHERE  T0.GeneratorID = '" + lblRepGenNo.Text.Trim() + "' AND T0.Customer='" + lblRepCusId.Text.Trim() + "' AND T0.Location ='" + lblRepLocId.Text.Trim() + "' AND T0.AgreementID = '" + lblRepAgreeId.Text.Trim() + "'");
            grdRecmonedReparis.DataSource = Completed;
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
                        FileList.Add(new FileModel()
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

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    if (FileList.Count > 0)
                    {
                        FileModel model = new FileModel();
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

        private void SaveFileList(string agreementno)
        {
            try
            {
                if (FileList.Count > 0)
                {
                    string dirpath = "";
                    if (Directory.Exists(Globals.DocPathJobs))
                    {
                        dirpath = Path.Combine(Globals.DocPathJobs, agreementno.Trim());
                        if (!Directory.Exists(dirpath))
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
            catch (Exception ex)
            {
                XtraMessageBox.Show("An I/O error has occurred. System did not copy the files", Globals.MessageCaption);
            }
        }


        private void RefreshFileList()
        {
            FileList.Clear();
            GetExistingList(lblAgreementID.Text);
            UpdateDocuemtnListView();
        }

        private void btnCompService_Click(object sender, EventArgs e)
        {
            ClearUpdateSer();

            if (gridVwUpcomming.SelectedRowsCount > 0)
            {
                lblAgreementID.Text = gridVwUpcomming.GetFocusedDataRow()["AgreementID"].ToString().Trim();
                lblGeneratorNumber.Text = gridVwUpcomming.GetFocusedDataRow()["GeneratorId"].ToString().Trim();
                lblServiceName.Text = gridVwUpcomming.GetFocusedDataRow()["Type"].ToString().Trim();

                RefreshFileList();


                DataTable Completed = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule WHERE  AgreementID = '" + gridVwUpcomming.GetFocusedDataRow()["AgreementID"].ToString().Trim() + "' AND GeneratorId = '" + gridVwUpcomming.GetFocusedDataRow()["GeneratorId"].ToString().Trim() + "'");
                gridServiceActions.DataSource = Completed;
                xtraTabControl3.SelectedTabPageIndex = 1;
                xtraTabPage10.PageEnabled = false;
                xtraTabPage11.PageEnabled = true;
                xtraTabPage1.PageEnabled = false;
                xtraTabPage2.PageEnabled = false;
                xtraTabPage3.PageEnabled = false;

            }
            else if (gridVwDueSer.SelectedRowsCount > 0)
            {
                lblAgreementID.Text = gridVwDueSer.GetFocusedDataRow()["AgreementID"].ToString().Trim();
                lblGeneratorNumber.Text = gridVwDueSer.GetFocusedDataRow()["GeneratorId"].ToString().Trim();
                lblServiceName.Text = gridVwDueSer.GetFocusedDataRow()["Type"].ToString().Trim();

                DataTable Completed = new DatabaseService().executeSelectQuery("SELECT * FROM ServicesShedule WHERE  AgreementID = '" + gridVwDueSer.GetFocusedDataRow()["AgreementID"].ToString().Trim() + "' AND GeneratorId = '" + gridVwDueSer.GetFocusedDataRow()["GeneratorId"].ToString().Trim() + "'");
                gridServiceActions.DataSource = Completed;
                xtraTabControl3.SelectedTabPageIndex = 1;
                xtraTabPage10.PageEnabled = false;
                xtraTabPage11.PageEnabled = true;
                xtraTabPage1.PageEnabled = false;
                xtraTabPage2.PageEnabled = false;
                xtraTabPage3.PageEnabled = false;


            }

        }

        private void GetExistingList(string inqnumber)
        {
            try
            {
                string dirpath = Path.Combine(Globals.DocPathJobs, inqnumber);
                if (Directory.Exists(dirpath))
                {
                    string[] arr = Directory.GetFiles(dirpath);
                    foreach (var item in arr)
                    {
                        FileModel fm = new FileModel()
                        {
                            FileName = item,
                            ShortFileName = Path.GetFileName(item)
                        };
                        FileList.Add(fm);
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileList("12");
        }






    }

}