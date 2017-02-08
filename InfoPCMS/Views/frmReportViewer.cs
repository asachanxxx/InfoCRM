using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CrystalDecisions.CrystalReports.Engine;
using  CrystalDecisions.Shared;

namespace InfoPCMS.Views
{
    public partial class frmReportViewer : DevExpress.XtraEditors.XtraForm
    {


   public string  rptPath  = "";
    private ReportDocument northwindCustomersReport;
    public string selectionForumla = "";
    public string rptTitle  = "";
   public string paraRepName = "";
    public string paraRepVale  = "";
   public string paraRepName2  = "";
   public string paraRepVale2 = "";
    public int reporttype;

        //Date Range
    public string startdate = null;
    public string enddate = null;
        //Date Range

        //REPRINT VALUE
    public string reprint = null;

        //USER DETAILS
    public string currentusername = null;
    public string designation = null;
    public string signature = null;

        //COMPANY TAX NO
    public string taxno = null;


        public frmReportViewer()
        {
            InitializeComponent();
        }

        private void frmReportViewer_Load(object sender, EventArgs e)
        {

            ConfigureCrystalReports();

        }


        public void ConfigureCrystalReports()
        {



            string ServerName = InfoPCMS.configurations.DBSource;
               
                northwindCustomersReport = new ReportDocument();
              


            string reportPath = String.Format(@"{0}\Reports\{1}", Application.StartupPath, rptPath);


              
                northwindCustomersReport.Load(reportPath);
                if(startdate!=null&&enddate!=null){


                    ParameterFields paramFields = new ParameterFields();
                    ParameterField pfItemYr = new ParameterField();
                    pfItemYr.ParameterFieldName = "startdate"; //startdate is Crystal Report Parameter name.
                    ParameterDiscreteValue dcItemYr = new ParameterDiscreteValue();
                    dcItemYr.Value = startdate;
                    pfItemYr.CurrentValues.Add(dcItemYr);

                    paramFields.Add(pfItemYr);

                    ParameterField pfItemYr2 = new ParameterField();
                    pfItemYr2.ParameterFieldName = "enddate"; //enddate is Crystal Report Parameter name.
                    ParameterDiscreteValue dcItemYr2 = new ParameterDiscreteValue();
                    dcItemYr2.Value = enddate;
                    pfItemYr2.CurrentValues.Add(dcItemYr2);

                    paramFields.Add(pfItemYr2);

                    myCrystalReportViewer.ParameterFieldInfo = paramFields;
                                       
                
                }

                

                if (currentusername != null && designation != null)
                {

                    ParameterFields paramFields = new ParameterFields();
                    ParameterField pfItemYr = new ParameterField();
                    pfItemYr.ParameterFieldName = "currentuser"; //startdate is Crystal Report Parameter name.
                    ParameterDiscreteValue dcItemYr = new ParameterDiscreteValue();
                    dcItemYr.Value = currentusername;
                    pfItemYr.CurrentValues.Add(dcItemYr);

                    paramFields.Add(pfItemYr);


                    
                    ParameterField pfItemYr3 = new ParameterField();
                    pfItemYr3.ParameterFieldName = "designation"; //startdate is Crystal Report Parameter name.
                    ParameterDiscreteValue dcItemYr3 = new ParameterDiscreteValue();
                    dcItemYr3.Value = designation;
                    pfItemYr3.CurrentValues.Add(dcItemYr3);

                    paramFields.Add(pfItemYr3);
                    

                    if (reprint != null)
                    {

                        
                        ParameterField pfItemYr2 = new ParameterField();
                        pfItemYr2.ParameterFieldName = "reprint"; //startdate is Crystal Report Parameter name.
                        ParameterDiscreteValue dcItemYr2 = new ParameterDiscreteValue();
                        dcItemYr2.Value = reprint;
                        pfItemYr2.CurrentValues.Add(dcItemYr2);

                        paramFields.Add(pfItemYr2);
                      

                    }

                    if (taxno != null)
                    {


                        ParameterField pfItemYr2 = new ParameterField();
                        pfItemYr2.ParameterFieldName = "taxno"; //startdate is Crystal Report Parameter name.
                        ParameterDiscreteValue dcItemYr2 = new ParameterDiscreteValue();
                        dcItemYr2.Value = taxno;
                        pfItemYr2.CurrentValues.Add(dcItemYr2);

                        paramFields.Add(pfItemYr2);


                    }

                    myCrystalReportViewer.ParameterFieldInfo = paramFields;
                
                
                }



                ConnectionInfo myConnectionInfo = new ConnectionInfo();
                myConnectionInfo.ServerName = ServerName;


                myConnectionInfo.DatabaseName = InfoPCMS.configurations.DBName.ToString();

                myConnectionInfo.UserID = InfoPCMS.configurations.DBUsername.ToString();
                myConnectionInfo.Password = InfoPCMS.configurations.DBPassword.ToString();



                myCrystalReportViewer.SelectionFormula = selectionForumla;
                northwindCustomersReport.SummaryInfo.ReportTitle = rptTitle;
                myCrystalReportViewer.ReportSource = northwindCustomersReport;
                myConnectionInfo.IntegratedSecurity = true;
                northwindCustomersReport.SetDatabaseLogon(InfoPCMS.configurations.DBUsername.ToString(), InfoPCMS.configurations.DBPassword.ToString());
                
                northwindCustomersReport.Refresh();
               
                     
              
                


        }

        private void myCrystalReportViewer_Load(object sender, EventArgs e)
        {
            ConfigureCrystalReports();
        }

        private void myCrystalReportViewer_Load_1(object sender, EventArgs e)
        {
            ConfigureCrystalReports();
        }


       


    }
}