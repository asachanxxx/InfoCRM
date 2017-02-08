using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HayleysPowerEngineeringCRM.View
{
    public partial class FrmMainDashBoard : DevExpress.XtraEditors.XtraForm
    {
        public FrmMainDashBoard()
        {
            InitializeComponent();
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void labelControl8_Click(object sender, EventArgs e)
        {

        }

        private void FrmMainDashBoard_Load(object sender, EventArgs e)
        {
            labelControl2.Text = InfoPCMS.Views.frmHome.UsrName;
        }
    }
}