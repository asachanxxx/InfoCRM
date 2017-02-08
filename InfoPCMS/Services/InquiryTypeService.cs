using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace InfoPCMS.Services
{
    class InquiryTypeService
    {
        private String inquiryType;       
        private int completionHours;
        private String cusCastegory;
        private int reminder1;
        private int reminder2;
        private int escalation1;
        private int escalation2;       

        
        public String InquiryType
        {
            get { return inquiryType; }
            set { inquiryType = value; }
        }

        public int CompletionHours
        {
            get { return completionHours; }
            set { completionHours = value; }
        }
        public String CusCastegory
        {
            get { return cusCastegory; }
            set { cusCastegory = value; }
        }

        public int Reminder1
        {
            get { return reminder1; }
            set { reminder1 = value; }
        }
        public int Reminder2
        {
            get { return reminder2; }
            set { reminder2 = value; }
        }
        public int Escalation1
        {
            get { return escalation1; }
            set { escalation1 = value; }
        }
        public int Escalation2
        {
            get { return escalation2; }
            set { escalation2 = value; }
        }   



        public DataTable getAllInquiryType()
        {
            try
            {

                DataTable dt = InfoPCMS.db.executeSelectQuery("select T0.*,T1.CategoryName from InquiryType T0 LEFT JOIN CustomerCategory T1 ON T0.CusCategory = T1.Id");
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;

            }


        }

        public Boolean addInquiryType()
        {

            try
            {
                var parameters = new Dictionary<String, String>() { { "InquiryType", InquiryType }, { "CompletionHours", CompletionHours.ToString() }, { "CusCategory", CusCastegory.ToString() }
                , { "Reminder1", Reminder1.ToString() } , { "Reminder2", Reminder2.ToString() } , { "Escalation1", Escalation1.ToString() } , { "Escalation2", Escalation2.ToString() } 
                };
                
                //var parameters = new Dictionary<String, String>() { { "InquiryType", InquiryType }, { "Perfix", Prefix.ToString() }, { "CompletionHours", CompletionHours.ToString() } };
                InfoPCMS.db.executeInsertOrUpdate("[spInsertInquiryType]", parameters);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return false;


        }

        public Boolean updateInquiryType()
        {

            try
            {

                var parameters = new Dictionary<String, String>() { { "InquiryType", InquiryType }, { "CompletionHours", CompletionHours.ToString() }, { "CusCategory", CusCastegory.ToString() }
                , { "Reminder1", Reminder1.ToString() } , { "Reminder2", Reminder2.ToString() } , { "Escalation1", Escalation1.ToString() } , { "Escalation2", Escalation2.ToString() } 
                }; 
                InfoPCMS.db.executeInsertOrUpdate("spUpdateInquiryType", parameters);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return false;


        }

        public void GetInqTypeByName()
        {

            try
            {

                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from InquiryType where InquiryType = '" + InquiryType + "' AND CusCategory = '"+CusCastegory+"'");

                if (dt.Rows.Count > 0)
                {
                    CompletionHours = Convert.ToInt32(dt.Rows[0]["CompletionHours"].ToString());
                    //CusCastegory = dt.Rows[0]["CusCategory"].ToString();
                    Reminder1 = Convert.ToInt32(dt.Rows[0]["Reminder1"].ToString());
                    Reminder2 = Convert.ToInt32(dt.Rows[0]["Reminder2"].ToString());
                    Escalation1 = Convert.ToInt32(dt.Rows[0]["Escalation1"].ToString());
                    Escalation2 = Convert.ToInt32(dt.Rows[0]["Escalation2"].ToString());
                    

                }

            }
            catch (Exception ex)
            {

                throw ex;

            }



        }

        public string GetCustomerCategoryByName(string strCusCastegory)
        {
            string strCusCat = "";

            try
            {

                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from CustomerCategory where CategoryName = '" + strCusCastegory + "'");


                if (dt.Rows.Count > 0)
                {
                    strCusCat = dt.Rows[0]["Id"].ToString();
                   
                }

                return strCusCat;

            }
            catch (Exception ex)
            {

                throw ex;

            }



        }
    }
}
