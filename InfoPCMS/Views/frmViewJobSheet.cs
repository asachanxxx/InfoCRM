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
using AcroPDFLib;
using AxAcroPDFLib;
using System.Resources;

namespace InfoPCMS.Views
{
    public partial class frmViewJobSheet : DevExpress.XtraEditors.XtraForm
    {
        public string JobSheetPath = "";


        public frmViewJobSheet()
        {
            InitializeComponent();
        }

        private void frmViewJobSheet_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PdfForm));
            //    AxAcroPDFLib.AxAcroPDF axAcroPdf = new AxAcroPDFLib.AxAcroPDF();

            //    ((System.ComponentModel.ISupportInitialize)(axAcroPdf)).BeginInit();

            //    axAcroPdf.Location = new Point(50, 50);
            //    axAcroPdf.Size = new Size(500, 500);
            //    axAcroPdf.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPdf.OcxState")));

            //    this.Controls.Add(axAcroPdf);
            //    ((System.ComponentModel.ISupportInitialize)(axAcroPdf)).EndInit();

            //    axAcroPdf.LoadFile("C:\\test.pdf");
            //    axAcroPdf.Show();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //} 

        }

        
    }
}