using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace InfoPCMS.Services
{
    class CompanyService
    {

        private String companyname;
        private String vatno;
        private String svatno;

        public String SVATNo
        {
            get { return svatno; }
            set { svatno = value; }
        }


        public String VATNo
        {
            get { return vatno; }
            set { vatno = value; }
        }


        public String CompanyName
        {
            get { return companyname; }
            set { companyname = value; }
        }


        public void getCompanyInfo() {

            try
            {

                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Company");
                if (dt.Rows.Count > 0)
                {

                    CompanyName = dt.Rows[0]["CompanyName"].ToString();
                    VATNo = dt.Rows[0]["VATNo"].ToString();
                    SVATNo = dt.Rows[0]["SVATNo"].ToString();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        
        
        }


    }
}
