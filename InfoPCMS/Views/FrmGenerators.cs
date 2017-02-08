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
    public partial class FrmGenerators : DevExpress.XtraEditors.XtraForm
    {
        public FrmGenerators()
        {
            InitializeComponent();
            try
            {
                LoadTable();
                DataTable dt2 = InfoPCMS.db.executeSelectQuery("select * from Customer WHERE IsActive = 'True'");
                // dt.Columns.Add("ConcatenatedField", typeof(string), "CustomerName + ' : ' + Address"); 

                cboCustomer.DataSource = dt2;
                cboCustomer.ValueMember = "CustomerName";
                cboCustomer.DisplayMember = "CustomerName";
              
                //DataTable dt5 = InfoPCMS.db.executeSelectQuery("select Id,Displayname from Users ");
                //cboExecutiveReposible.DataSource = dt5;
                //cboExecutiveReposible.ValueMember = "Displayname";
                //cboExecutiveReposible.DisplayMember = "Displayname";

                DataTable dt3 = InfoPCMS.db.executeSelectQuery("select * from Employee WHERE Category <> 'LEVEL 3'");
                cboExecutiveReposible.DataSource = dt3;
                cboExecutiveReposible.ValueMember = "EmployeeName";
                cboExecutiveReposible.DisplayMember = "EmployeeName";

                for (int year = 1950; year <= DateTime.UtcNow.Year; year++)
                {
                    txtyearofmanufature.Items.Add(year);
                    
                }

                txtyearofmanufature.SelectedIndex = -1;

            }
            catch (Exception ex) { 
            
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //DataTable dt = InfoPCMS.db.executeSelectQuery("select max(code) as code from GeneratorDetails");
            //     if (dt.Rows.Count > 0) {
            if (ValidationGenerator())
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_EMPTY_FIELDS_ERROR(), "Error");
            }
            else if (AlreadyEntered(GetCusId(cboCustomer.Text),GetSubLocationId(cboLocation.Text, GetCusId(cboCustomer.Text)),txtgennumber.Text))
            {

                XtraMessageBox.Show("Generator Details Already Entered", "Error");
            }
            else
            {
                new DatabaseService().executeUpdateQuery(String.Format("INSERT INTO GeneratorDetails VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}', "
                    +" '{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}',"
                    + " '{31}','{32}','{33}','{34}','{35}')", txtgennumber.Text, GetCusId(cboCustomer.Text), GetSubLocationId(cboLocation.Text, GetCusId(cboCustomer.Text)), txtplantNumber.Text, txtmanufactre.Text, txtcapacity.Text, 
                    txtyearofmanufature.Text, txtSetNumber.Text, txtordernumber.Text, txtEnginMake.Text, txtenginModel.Text, txtSerialNumber.Text, txtchassisId.Text, 
                    txtEnginSpecificNote.Text, txtalternatorMake.Text, txtalternatorModel.Text, txtalternatorSerialnumber.Text, txtAlternatorspecnote.Text, txtphase.Text,
                    txtoilUsed.Text, txtcontrolModule.Text, cbostartingMethod.Text, txtPmg.SelectedItem, txtavr.Text, txtgovernorCard.Text, txtSpecificFilterNumber.Text,
                    chbsyncronized.Checked, txtCompantOpenset.SelectedItem, txtregulerserviceInterval.Text, chbServiceAgreement.Checked, txtregulerservicingchargeamiount.Text,
                    txtAddressToSentInvoice.Text,GetEmpId(cboExecutiveReposible.Text), txtDeliveryDate.Text, cmbxGeneApplication.Text,txt_hourmeter.Text.Trim()));
                //    new DatabaseService().executeUpdateQuery("INSERT INTO GeneratorDetails VALUES('" + cboCustomer.Text + "','" + cboLocation.Text + "','" + txtContactPerson.Text + "','" + txtDesignation.Text + "','" + txttel.Text + "','" + txtfax.Text + "','" + txtsendinvoiceto.Text + "','" + txtattn.Text + "','" + txtemail.Text + "','" + txtenginmake.Text + "','" + txtenginmodel.Text + "','" + txtserialno.Text + "','" + txtavr.Text + "','" + txtkvr.Text + "','" + txtdistance.Text + "','" + txtoil.Text + "','" + txtlabortransport.Text + "','" + txtot1.Text + "','" + txttappet1.Text + "','" + txtot2.Text + "','" + txtvat.Text + "','" + (Convert.ToInt16(dt.Rows[0]["code"].ToString()) + 1) + "')");
                //MessageBox.Show("Record Saved Successfully");
                XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_ADDED_INFORMATION(), "Information");
                Clear();
                //}
            }
            
        }


        private bool AlreadyEntered(string strCustomer,string strLocation,string strGenNo)
        {
            bool blAlreadyEntered = false;

            DataTable dtGenDetails = InfoPCMS.db.executeSelectQuery("select * from HayleysPowerEngineeringCRM.dbo.GeneratorDetails WHERE GeneratorNumber = '"+strGenNo+"' AND Customer='"+strCustomer+"' AND location = '"+strLocation+"'");

            if (dtGenDetails.Rows.Count > 0)
            {

                blAlreadyEntered = true;
            }

            return blAlreadyEntered;
        
        }

        private bool ValidationGenerator()
        {
            bool blValidation = false;

            if (string.IsNullOrEmpty(cboCustomer.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(cboLocation.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(txtgennumber.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(txtcapacity.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(cboExecutiveReposible.Text.Trim()))
            {
                blValidation = true;
            }
            //else if (string.IsNullOrEmpty(txtDeliveryDate.Text.Trim()))
            //{
            //    blValidation = true;
            //}
            
            


            return blValidation;
        }

        private bool ValidationGeneratorHODetails()
        {
            bool blValidation = false;

            if (string.IsNullOrEmpty(dtchoCommenssiosioningDate.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(cboWarrantyPeriod.Text.Trim()))
            {
                blValidation = true;
            }
            else if (string.IsNullOrEmpty(txthowarranty.Text.Trim()))
            {
                blValidation = true;
            }
            
            return blValidation;
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            InquiryService inquiry = new InquiryService();
            inquiry.getSiteAddByCus(cboCustomer.Text.Trim());
            cboCustomer.Text = inquiry.ProblemSource;


            DataTable dt = InfoPCMS.db.executeSelectQuery(String.Format("select SubLocation from CustomerLocation WHERE CustomerName='{0}'", cboCustomer.Text.Trim()));
            cboLocation.DataSource = dt;
            cboLocation.DisplayMember = "SubLocation";
            cboLocation.ValueMember = "SubLocation";

        }

        public void Clear() {
            cboCustomer.SelectedIndex = 0;
            txtgennumber.Text ="";
            cboLocation.SelectedValue = "";
            txtgennumber.Text = "";
            txtplantNumber.Text = "";
            txtmanufactre.Text = "";
            txtcapacity.Text = "";
            txtyearofmanufature.Text = "";
            txtSetNumber.Text = "";
            txtordernumber.Text = "";
            txtEnginMake.Text = "";
            txtenginModel.Text = "";
            txtDeliveryDate.Text = "";
            txtSerialNumber.Text = "";
            txtchassisId.Text = "";
            txtEnginSpecificNote.Text = "";
            txtalternatorMake.Text = "";
            txtalternatorModel.Text = "";
            txtalternatorSerialnumber.Text = "";
            txtAlternatorspecnote.Text = "";
            txtphase.Text = "";
            txtoilUsed.Text = "";
            txtcontrolModule.Text = "";
            cbostartingMethod.SelectedIndex = 0;
            txtPmg.SelectedIndex =0;
            txtavr.Text = "";
            txtgovernorCard.Text = "";
            txtSpecificFilterNumber.Text = "";
            chbsyncronized.Checked =false;
            txtCompantOpenset.SelectedIndex = 0;
            txtregulerserviceInterval.Text = "";
            chbServiceAgreement.Checked = false;
            txtregulerservicingchargeamiount.Text = "";
            txtAddressToSentInvoice.SelectedIndex = 0;
            cboExecutiveReposible.SelectedIndex = 0;
            cboCustomer.Focus();
            cmbxGeneApplication.SelectedIndex = 0;
            LoadTable();

            btnUpdate.Enabled = false;


            tabPageAddGen.PageEnabled = false;
            tabPageGenDetails.PageEnabled = true;
            tabPageGenHODetails.PageEnabled = false;
            //xtraTabControl1.SelectedTabPageIndex = 1;
            xtraTabControl1.SelectedTabPageIndex = 0;
        }



        public void ClearGenDetails()
        {
            cboCustomer.Enabled = true;
            txtgennumber.Enabled = true;
            cboLocation.Enabled = true;


            cboCustomer.SelectedIndex = -1;
            txtgennumber.Text = "";
            cboLocation.SelectedValue = "";
            txtgennumber.Text = "";
            txtplantNumber.Text = "";
            txtmanufactre.Text = "";
            txtcapacity.Text = "";
            txtyearofmanufature.SelectedIndex = -1;
            txtSetNumber.Text = "";
            txtordernumber.Text = "";
            txtEnginMake.Text = "";
            txtenginModel.Text = "";
            txtDeliveryDate.Text = "";
            txtSerialNumber.Text = "";
            txtchassisId.Text = "";
            txtEnginSpecificNote.Text = "";
            txtalternatorMake.Text = "";
            txtalternatorModel.Text = "";
            txtalternatorSerialnumber.Text = "";
            txtAlternatorspecnote.Text = "";
            txtphase.Text = "";
            txtoilUsed.Text = "";
            txtcontrolModule.Text = "";
            cbostartingMethod.SelectedIndex = -1;
            txtPmg.SelectedIndex = -1;
            txtavr.Text = "";
            txtgovernorCard.Text = "";
            txtSpecificFilterNumber.Text = "";
            chbsyncronized.Checked = false;
            txtCompantOpenset.SelectedIndex = -1;
            txtregulerserviceInterval.Text = "";
            chbServiceAgreement.Checked = false;
            txtregulerservicingchargeamiount.Text = "";
            txtAddressToSentInvoice.SelectedIndex = -1;
            cboExecutiveReposible.SelectedIndex = -1;
            txtgennumber.Focus();
            cmbxGeneApplication.SelectedIndex = -1;
           
            btnUpdate.Enabled = false;

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

        private string GetSubLocationId(string strSubLocName, string strCusId)
        {
            string strSubLocId = "";
            DataTable dtEmployee = new DatabaseService().executeSelectQuery("select Id,SubLocation from CustomerLocation  WHERE SubLocation='" + strSubLocName.Trim() + "' AND CustomerId = '" + strCusId.Trim() + "'");


            if (dtEmployee.Rows.Count > 0)
            {

                strSubLocId = dtEmployee.Rows[0]["Id"].ToString();
            }

            return strSubLocId;
        }

        private void cboCustomer_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            InquiryService inquiry = new InquiryService();
            inquiry.getSiteAddByCus(cboCustomer.Text.Trim());



            DataTable dt = InfoPCMS.db.executeSelectQuery(String.Format("select SubLocation from CustomerLocation WHERE CustomerName='{0}'", cboCustomer.Text.Trim()));
            cboLocation.DataSource = dt;
            cboLocation.DisplayMember = "SubLocation";
            cboLocation.ValueMember = "SubLocation";

        }

        public void LoadTable()
        {
            try
            {
                //tblgen.DataSource = new DatabaseService().executeSelectQuery("SELECT* FROM GeneratorDetails");


                 tblgen.DataSource = new DatabaseService().executeSelectQuery("SELECT T1.CustomerName,T2.EmployeeName,T3.SubLocation,T0.* FROM HayleysPowerEngineeringCRM.dbo.GeneratorDetails T0 "
                + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Customer T1 ON T0.Customer = T1.Id "
                + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.Employee T2 ON T0.ExecutiveResponsible = T2.Id "
                + " LEFT JOIN HayleysPowerEngineeringCRM.dbo.CustomerLocation T3 ON T0.location = T3.Id ");
                    }
            catch (Exception ex)
            {

            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataTable dt = InfoPCMS.db.executeSelectQuery(String.Format("select * from GeneratorDetails where GeneratorNumber='{0}' and Customer='{1}' AND location ='{2}' ", gridView1.GetFocusedDataRow()["GeneratorNumber"], gridView1.GetFocusedDataRow()["Customer"], gridView1.GetFocusedDataRow()["location"]));
            if (dt.Rows.Count > 0) {

                cboCustomer.Enabled = false;
                txtgennumber.Enabled = false;
                cboLocation.Enabled = false;

                //string strCusName = "";
                //string strSubLoc = "";
                //string strEmpName = "";

                DataTable dtCus = InfoPCMS.db.executeSelectQuery("select * from Customer where Id = '"+dt.Rows[0]["Customer"].ToString().Trim()+"' ");
                DataTable dtEmp = InfoPCMS.db.executeSelectQuery("select * from Employee where Id = '" + dt.Rows[0]["ExecutiveResponsible"].ToString().Trim() + "' "); ;
                DataTable dtSubLoc = InfoPCMS.db.executeSelectQuery("select * from CustomerLocation where Id = '" + dt.Rows[0]["location"].ToString().Trim() + "' "); ;


                cboCustomer.SelectedValue = dtCus.Rows[0]["CustomerName"].ToString().Trim();
                txtgennumber.Text = dt.Rows[0]["GeneratorNumber"].ToString().Trim();
                cboLocation.SelectedValue = dtSubLoc.Rows[0]["SubLocation"].ToString().Trim();
                txtgennumber.Text = dt.Rows[0]["GeneratorNumber"].ToString().Trim();
                txtplantNumber.Text = dt.Rows[0]["plantNumber"].ToString().Trim();
                txtmanufactre.Text = dt.Rows[0]["manufacture"].ToString().Trim();
                txtcapacity.Text = dt.Rows[0]["capcity"].ToString().Trim();
                txtyearofmanufature.Text = dt.Rows[0]["yearofManufacture"].ToString().Trim();
                txtSetNumber.Text = dt.Rows[0]["setNumber"].ToString().Trim();
                txtordernumber.Text = dt.Rows[0]["orderNumber"].ToString().Trim();
                txtEnginMake.Text = dt.Rows[0]["enginMake"].ToString().Trim();
                txtenginModel.Text = dt.Rows[0]["enginModel"].ToString().Trim();
                txtSerialNumber.Text = dt.Rows[0]["EnginSerialNumber"].ToString().Trim();
                txtchassisId.Text = dt.Rows[0]["chassisId"].ToString().Trim();
                txtEnginSpecificNote.Text = dt.Rows[0]["enginSpecificNote"].ToString().Trim();
                txtalternatorMake.Text = dt.Rows[0]["alternatormake"].ToString().Trim();
                txtalternatorModel.Text = dt.Rows[0]["alternatormodel"].ToString().Trim();
                txtalternatorSerialnumber.Text = dt.Rows[0]["alternatorSerial"].ToString().Trim();
                txtAlternatorspecnote.Text = dt.Rows[0]["eltenatorSpecificNote"].ToString().Trim();
                txtphase.Text = dt.Rows[0]["phase"].ToString().Trim();
                txtoilUsed.Text = dt.Rows[0]["oilUsed"].ToString().Trim();
                txtcontrolModule.Text = dt.Rows[0]["controlModule"].ToString().Trim();
                cbostartingMethod.SelectedItem = dt.Rows[0]["startingMethord"].ToString().Trim();
                txtPmg.SelectedItem = dt.Rows[0]["pmg"].ToString().Trim();
                txtavr.Text = dt.Rows[0]["avr"].ToString().Trim();
                txtgovernorCard.Text = dt.Rows[0]["governorrCard"].ToString().Trim();
                txtSpecificFilterNumber.Text = dt.Rows[0]["specificFilterNumber"].ToString().Trim();
                chbsyncronized.Checked = Convert.ToBoolean(dt.Rows[0]["syncronized"].ToString().Trim());
                txtCompantOpenset.SelectedItem = dt.Rows[0]["companyOpenSet"].ToString().Trim();
                txtregulerserviceInterval.Text = dt.Rows[0]["regulerServiceInterval"].ToString().Trim();
                chbServiceAgreement.Checked = Convert.ToBoolean(dt.Rows[0]["ServiceAgreementSigned"].ToString().Trim());
                txtregulerservicingchargeamiount.Text = dt.Rows[0]["regulerServicingChargeAmount"].ToString().Trim();
                txtAddressToSentInvoice.SelectedItem = dt.Rows[0]["addresstoSendInvoice"].ToString().Trim();
                cboExecutiveReposible.SelectedValue = dtEmp.Rows[0]["EmployeeName"].ToString().Trim();
                txtDeliveryDate.Text = dt.Rows[0]["DeliveryDate"].ToString().Trim();
                cmbxGeneApplication.SelectedItem = dt.Rows[0]["GeneratorApplication"].ToString().Trim();

                tabPageAddGen.PageEnabled = true;
                tabPageGenDetails.PageEnabled = false;
                tabPageGenHODetails.PageEnabled = false;
                xtraTabControl1.SelectedTabPageIndex = 1;
                btnUpdate.Enabled = true;
                simpleButton1.Enabled = false;

            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            tabPageAddGen.PageEnabled = true;
            tabPageGenDetails.PageEnabled = false;
            tabPageGenHODetails.PageEnabled = false;

            xtraTabControl1.SelectedTabPageIndex = 1;

            ClearGenDetails();
        }

        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            string strSelecCus = gridView1.GetFocusedDataRow()["Customer"].ToString();
            string strSelecLoc = gridView1.GetFocusedDataRow()["Location"].ToString();

            DataTable dt = InfoPCMS.db.executeSelectQuery(String.Format("select * from GeneratorDetails where GeneratorNumber='{0}'AND Customer = '{1}' AND location = '{2}' ", gridView1.GetFocusedDataRow()["GeneratorNumber"], strSelecCus,strSelecLoc ));

            DataTable dtHandOver = InfoPCMS.db.executeSelectQuery(String.Format("select * from HandingOverDetails where GeneratorNumber='{0}' AND Customer = '{1}' AND Location = '{2}' ", gridView1.GetFocusedDataRow()["GeneratorNumber"],strSelecCus,strSelecLoc));
             if (dtHandOver.Rows.Count > 0)
             {
                 XtraMessageBox.Show("Generator Handing Over Details Already Entered","Information");
             }
             else
             {
                 

                 if (dt.Rows.Count > 0)
                 {

                     DataTable dtCus = InfoPCMS.db.executeSelectQuery("select * from Customer where Id = '" + dt.Rows[0]["Customer"].ToString().Trim() + "' ");
                    // DataTable dtEmp = InfoPCMS.db.executeSelectQuery("select * from Employee where Id = '" + dt.Rows[0]["ExecutiveResponsible"].ToString().Trim() + "' "); ;
                     DataTable dtSubLoc = InfoPCMS.db.executeSelectQuery("select * from CustomerLocation where Id = '" + dt.Rows[0]["location"].ToString().Trim() + "' "); ;


                     lblhoCutomer.Text = dtCus.Rows[0]["CustomerName"].ToString().Trim();
                     lblhoLocation.Text = dtSubLoc.Rows[0]["SubLocation"].ToString().Trim();
                     lblhoGeneratorNumber.Text = dt.Rows[0]["GeneratorNumber"].ToString().Trim();

                     dtchoCommenssiosioningDate.Text = "";
                     cboWarrantyPeriod.SelectedIndex = -1;
                     txthowarranty.Text = "";
                     txtSpecialCondion.Text = "";

                     tabPageAddGen.PageEnabled = false;
                     tabPageGenDetails.PageEnabled = false;
                     tabPageGenHODetails.PageEnabled = true;
                     xtraTabControl1.SelectedTabPageIndex = 3;

                     simpleButton4.Enabled = true;
                 }
             }
        }


        private void simpleButton4_Click(object sender, EventArgs e)
        {
            new DatabaseService().executeUpdateQuery(String.Format("INSERT INTO HandingOverDetails VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", lblhoGeneratorNumber.Text, GetCusId(lblhoCutomer.Text), GetSubLocationId(lblhoLocation.Text, GetCusId(lblhoCutomer.Text)), dtchoCommenssiosioningDate.Text, cboWarrantyPeriod.Text, txtSpecialCondion.Text, txthowarranty.Text));
           // MessageBox.Show("Record Saved Successfully");
            XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_ADDED_INFORMATION(), "Information");
            lblhoCutomer.Text = "";
            lblhoGeneratorNumber.Text = "";
            lblhoLocation.Text = "";
            dtchoCommenssiosioningDate.Text = "";
            cboWarrantyPeriod.SelectedIndex = 0;
            txthowarranty.Text = "";
            txtSpecialCondion.Text = "";
            dtchoCommenssiosioningDate.Focus();

            LoadTable();

            tabPageAddGen.PageEnabled = false;
            tabPageGenDetails.PageEnabled = true;
            tabPageGenHODetails.PageEnabled = false;
            xtraTabControl1.SelectedTabPageIndex = 0;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //lblhoCutomer.Text = "";
            //lblhoGeneratorNumber.Text = "";
            //lblhoLocation.Text="";
            //dtchoCommenssiosioningDate.Text = "";
            //cboWarrantyPeriod.SelectedIndex = 0;
            //txthowarranty.Text = "";
            //txtSpecialCondion.Text = "";
            //dtchoCommenssiosioningDate.Focus();

            Clear();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            //DataTable dt = InfoPCMS.db.executeSelectQuery(String.Format("select * from HandingOverDetails where GeneratorNumber='{0}' AND Customer = '{1}' AND Location = '{2}' ", gridView1.GetFocusedDataRow()["GeneratorNumber"], gridView1.GetFocusedDataRow()["Customer"],gridView1.GetFocusedDataRow()["Location"]));

            string strSelecCus = gridView1.GetFocusedDataRow()["Customer"].ToString();
            string strSelecLoc = gridView1.GetFocusedDataRow()["Location"].ToString();


            DataTable dt = InfoPCMS.db.executeSelectQuery(String.Format("select * from HandingOverDetails where GeneratorNumber='{0}'AND Customer = '{1}' AND location = '{2}' ", gridView1.GetFocusedDataRow()["GeneratorNumber"], strSelecCus, strSelecLoc));


            
            
            if (dt.Rows.Count > 0)
            {
                DataTable dtCus = InfoPCMS.db.executeSelectQuery("select * from Customer where Id = '" + dt.Rows[0]["Customer"].ToString().Trim() + "' ");
               // DataTable dtEmp = InfoPCMS.db.executeSelectQuery("select * from Employee where Id = '" + dt.Rows[0]["ExecutiveResponsible"].ToString().Trim() + "' "); ;
                DataTable dtSubLoc = InfoPCMS.db.executeSelectQuery("select * from CustomerLocation where Id = '" + dt.Rows[0]["location"].ToString().Trim() + "' "); ;




                lblhoCutomer.Text = dtCus.Rows[0]["CustomerName"].ToString().Trim();
                lblhoLocation.Text = dtSubLoc.Rows[0]["SubLocation"].ToString().Trim();
                lblhoGeneratorNumber.Text = dt.Rows[0]["GeneratorNumber"].ToString().Trim();
                dtchoCommenssiosioningDate.Text = dt.Rows[0]["CommissioningDate"].ToString().Trim();
                cboWarrantyPeriod.SelectedItem = dt.Rows[0]["WarrantyPeriod"].ToString().Trim();
                txthowarranty.Text = dt.Rows[0]["warranty"].ToString().Trim();
                txtSpecialCondion.Text = dt.Rows[0]["Special"].ToString().Trim();

                tabPageAddGen.PageEnabled = false;
                tabPageGenDetails.PageEnabled = false;
                tabPageGenHODetails.PageEnabled = true;
                xtraTabControl1.SelectedTabPageIndex = 3;

                btnUpdateHO.Enabled = true;
                simpleButton4.Enabled = false;
            }
            else
            {

                XtraMessageBox.Show("Generator Handing Over Details Not Entered Entered", "Information");
            }

        }

        private void FrmGenerators_Load(object sender, EventArgs e)
        {
            // xtraTabControl1.tabPageGenDetails(1).PageEnabled = True

            tabPageGenDetails.PageEnabled = true;
            tabPageGenHODetails.PageEnabled = false;
            tabPageAddGen.PageEnabled = false;

                  //  tabProforma.TabPages(0).PageEnabled = False
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //DataTable dt = InfoPCMS.db.executeSelectQuery("select max(code) as code from GeneratorDetails");
            //     if (dt.Rows.Count > 0) {
            if (ValidationGenerator())
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_EMPTY_FIELDS_ERROR(), "Error");
            }
            else
            {
                new DatabaseService().executeUpdateQuery(String.Format("UPDATE GeneratorDetails SET plantNumber='"+txtplantNumber.Text+"',manufacture='"+ txtmanufactre.Text+"' "
                    + " ,capcity= '"+txtcapacity.Text+"',yearofManufacture='"+ txtyearofmanufature.Text+"',setNumber='"+ txtSetNumber.Text+"',orderNumber='"+ txtordernumber.Text+"'," 
                    + " enginMake='"+ txtEnginMake.Text+"',enginModel='"+ txtenginModel.Text+"',EnginSerialNumber='"+ txtSerialNumber.Text+"',chassisId='"+ txtchassisId.Text+"',"
                    + " enginSpecificNote='"+ txtEnginSpecificNote.Text+"',alternatormake='"+ txtalternatorMake.Text+"',alternatormodel='"+ txtalternatorModel.Text+"',"
                    +"  alternatorSerial='"+ txtalternatorSerialnumber.Text+"',eltenatorSpecificNote='"+ txtAlternatorspecnote.Text+"',phase='"+ txtphase.Text+"'," 
                    +" oilUsed='"+ txtoilUsed.Text+"',controlModule='"+ txtcontrolModule.Text+"',startingMethord='"+ cbostartingMethod.Text+"',pmg='"+ txtPmg.SelectedItem+"',"
                    +" avr='"+ txtavr.Text+"',governorrCard='"+ txtgovernorCard.Text+"',specificFilterNumber='"+ txtSpecificFilterNumber.Text+"',"
                    +" syncronized='"+ chbsyncronized.Checked+"',companyOpenSet='"+ txtCompantOpenset.SelectedItem+"',regulerServiceInterval='"+ txtregulerserviceInterval.Text+"',"
                    +"  ServiceAgreementSigned='"+ chbServiceAgreement.Checked+"',regulerServicingChargeAmount='"+ txtregulerservicingchargeamiount.Text+"',"
                    +" addresstoSendInvoice='"+ txtAddressToSentInvoice.Text+"',ExecutiveResponsible='"+GetEmpId(cboExecutiveReposible.Text)+"',DeliveryDate='"+ txtDeliveryDate.Text+"',"
                    + " GeneratorApplication='" + cmbxGeneApplication.Text + "'  WHERE GeneratorNumber = '" + txtgennumber.Text + "' AND Customer = '" + GetCusId(cboCustomer.Text) + "'AND location = '" + GetSubLocationId(cboLocation.Text, GetCusId(cboCustomer.Text)) + "'"
                     + " AND HourM = '" + txt_hourmeter.Text.Trim() + "'"));
                //    new DatabaseService().executeUpdateQuery("INSERT INTO GeneratorDetails VALUES('" + cboCustomer.Text + "'+"','"+'" + cboLocation.Text + "'+"','"+'" + txtContactPerson.Text + "','" + txtDesignation.Text + "','" + txttel.Text + "','" + txtfax.Text + "','" + txtsendinvoiceto.Text + "','" + txtattn.Text + "','" + txtemail.Text + "','" + txtenginmake.Text + "','" + txtenginmodel.Text + "','" + txtserialno.Text + "','" + txtavr.Text + "','" + txtkvr.Text + "','" + txtdistance.Text + "','" + txtoil.Text + "','" + txtlabortransport.Text + "','" + txtot1.Text + "','" + txttappet1.Text + "','" + txtot2.Text + "','" + txtvat.Text + "','" + (Convert.ToInt16(dt.Rows[0]["code"].ToString()) + 1) + "')");
                //MessageBox.Show("Record Saved Successfully");
                XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_UPDATED_INFORMATION(), "Information");
                Clear();
                //}
            }
        }

        private void btnUpdateHO_Click(object sender, EventArgs e)
        {
            if (ValidationGeneratorHODetails())
            {

                XtraMessageBox.Show(InfoPCMS.message.GET_EMPTY_FIELDS_ERROR(), "Error");
            }
            else
            {
                new DatabaseService().executeUpdateQuery(String.Format("UPDATE HandingOverDetails SET CommissioningDate='" + dtchoCommenssiosioningDate.Text + "',WarrantyPeriod='" + cboWarrantyPeriod.Text + "',Special= '" + txtSpecialCondion.Text + "',warranty='" + txthowarranty.Text + "'  WHERE GeneratorNumber = '" + lblhoGeneratorNumber.Text.Trim() + "' AND Customer = '" +GetCusId(lblhoCutomer.Text.Trim()) + "'AND Location = '" + GetSubLocationId(lblhoLocation.Text.Trim(),GetCusId(lblhoCutomer.Text.Trim())) + "'"));
               
                
                
                //    new DatabaseService().executeUpdateQuery("INSERT INTO GeneratorDetails VALUES('" + cboCustomer.Text + "'+"','"+'" + cboLocation.Text + "'+"','"+'" + txtContactPerson.Text + "','" + txtDesignation.Text + "','" + txttel.Text + "','" + txtfax.Text + "','" + txtsendinvoiceto.Text + "','" + txtattn.Text + "','" + txtemail.Text + "','" + txtenginmake.Text + "','" + txtenginmodel.Text + "','" + txtserialno.Text + "','" + txtavr.Text + "','" + txtkvr.Text + "','" + txtdistance.Text + "','" + txtoil.Text + "','" + txtlabortransport.Text + "','" + txtot1.Text + "','" + txttappet1.Text + "','" + txtot2.Text + "','" + txtvat.Text + "','" + (Convert.ToInt16(dt.Rows[0]["code"].ToString()) + 1) + "')");
                //MessageBox.Show("Record Saved Successfully");
                XtraMessageBox.Show(InfoPCMS.message.GET_RECORD_UPDATED_INFORMATION(), "Information");
                Clear();
                //}
            }

        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {

        }

        private void btnCopyGenDetails_Click(object sender, EventArgs e)
        {
            string strSelCus = gridView1.GetFocusedDataRow()["Customer"].ToString().Trim();
            string strSelLoc = gridView1.GetFocusedDataRow()["Location"].ToString().Trim();
            string strSelGen = gridView1.GetFocusedDataRow()["GeneratorNumber"].ToString().Trim();

            DataTable dt = InfoPCMS.db.executeSelectQuery(String.Format("select * from GeneratorDetails where GeneratorNumber='{0}'AND Customer = '{1}' AND location = '{2}' ", strSelGen, strSelCus, strSelLoc));

            if (dt.Rows.Count > 0)
            {                               

                DataTable dtCus = InfoPCMS.db.executeSelectQuery("select * from Customer where Id = '" + dt.Rows[0]["Customer"].ToString().Trim() + "' ");
                DataTable dtEmp = InfoPCMS.db.executeSelectQuery("select * from Employee where Id = '" + dt.Rows[0]["ExecutiveResponsible"].ToString().Trim() + "' "); ;
                DataTable dtSubLoc = InfoPCMS.db.executeSelectQuery("select * from CustomerLocation where Id = '" + dt.Rows[0]["location"].ToString().Trim() + "' "); ;
                
               // cboCustomer.SelectedValue = dtCus.Rows[0]["CustomerName"].ToString().Trim();
               // txtgennumber.Text = dt.Rows[0]["GeneratorNumber"].ToString().Trim();
               // cboLocation.SelectedValue = dtSubLoc.Rows[0]["SubLocation"].ToString().Trim();
               // txtgennumber.Text = dt.Rows[0]["GeneratorNumber"].ToString().Trim();
                txtplantNumber.Text = dt.Rows[0]["plantNumber"].ToString().Trim();
                txtmanufactre.Text = dt.Rows[0]["manufacture"].ToString().Trim();
                txtcapacity.Text = dt.Rows[0]["capcity"].ToString().Trim();
                txtyearofmanufature.Text = dt.Rows[0]["yearofManufacture"].ToString().Trim();
                txtSetNumber.Text = dt.Rows[0]["setNumber"].ToString().Trim();
                txtordernumber.Text = dt.Rows[0]["orderNumber"].ToString().Trim();
                txtEnginMake.Text = dt.Rows[0]["enginMake"].ToString().Trim();
                txtenginModel.Text = dt.Rows[0]["enginModel"].ToString().Trim();
                txtSerialNumber.Text = dt.Rows[0]["EnginSerialNumber"].ToString().Trim();
                txtchassisId.Text = dt.Rows[0]["chassisId"].ToString().Trim();
                txtEnginSpecificNote.Text = dt.Rows[0]["enginSpecificNote"].ToString().Trim();
                txtalternatorMake.Text = dt.Rows[0]["alternatormake"].ToString().Trim();
                txtalternatorModel.Text = dt.Rows[0]["alternatormodel"].ToString().Trim();
                txtalternatorSerialnumber.Text = dt.Rows[0]["alternatorSerial"].ToString().Trim();
                txtAlternatorspecnote.Text = dt.Rows[0]["eltenatorSpecificNote"].ToString().Trim();
                txtphase.Text = dt.Rows[0]["phase"].ToString().Trim();
                txtoilUsed.Text = dt.Rows[0]["oilUsed"].ToString().Trim();
                txtcontrolModule.Text = dt.Rows[0]["controlModule"].ToString().Trim();
                cbostartingMethod.SelectedItem = dt.Rows[0]["startingMethord"].ToString().Trim();
                txtPmg.SelectedItem = dt.Rows[0]["pmg"].ToString().Trim();
                txtavr.Text = dt.Rows[0]["avr"].ToString().Trim();
                txtgovernorCard.Text = dt.Rows[0]["governorrCard"].ToString().Trim();
                txtSpecificFilterNumber.Text = dt.Rows[0]["specificFilterNumber"].ToString().Trim();
                chbsyncronized.Checked = Convert.ToBoolean(dt.Rows[0]["syncronized"].ToString().Trim());
                txtCompantOpenset.SelectedItem = dt.Rows[0]["companyOpenSet"].ToString().Trim();
                txtregulerserviceInterval.Text = dt.Rows[0]["regulerServiceInterval"].ToString().Trim();
                chbServiceAgreement.Checked = Convert.ToBoolean(dt.Rows[0]["ServiceAgreementSigned"].ToString().Trim());
                txtregulerservicingchargeamiount.Text = dt.Rows[0]["regulerServicingChargeAmount"].ToString().Trim();
                txtAddressToSentInvoice.SelectedItem = dt.Rows[0]["addresstoSendInvoice"].ToString().Trim();
                cboExecutiveReposible.SelectedValue = dtEmp.Rows[0]["EmployeeName"].ToString().Trim();
                txtDeliveryDate.Text = dt.Rows[0]["DeliveryDate"].ToString().Trim();
                cmbxGeneApplication.SelectedItem = dt.Rows[0]["GeneratorApplication"].ToString().Trim();
                txt_hourmeter.Text = dt.Rows[0]["HourM"].ToString().Trim();

                tabPageAddGen.PageEnabled = true;
                tabPageGenDetails.PageEnabled = false;
                tabPageGenHODetails.PageEnabled = false;
                xtraTabControl1.SelectedTabPageIndex = 1;

                simpleButton1.Enabled = true;
                btnUpdate.Enabled = false;

            }

        }
    }
}