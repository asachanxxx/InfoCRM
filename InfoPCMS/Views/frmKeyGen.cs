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
    public partial class frmKeyGen : DevExpress.XtraEditors.XtraForm
    {
        public frmKeyGen()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LicenseService license = new LicenseService();

            string getEncrypt = license.Encrypt(txtKey.Text.Trim());
            txtKey.Text = getEncrypt;

        }
    }
}