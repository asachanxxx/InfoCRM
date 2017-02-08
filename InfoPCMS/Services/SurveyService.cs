using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoPCMS.Services
{
    class SurveyService
    {
        private String Id;
        private String details;
        private String img1;
        private String img2;
        private String inquirynumber;

        public String InquiryNumber
        {
            get { return inquirynumber; }
            set { inquirynumber = value; }
        }


        public String Img2
        {
            get { return img2; }
            set { img2 = value; }
        }


        public String Img1
        {
            get { return img1; }
            set { img1 = value; }
        }


        public String Details
        {
            get { return details; }
            set { details = value; }
        }


       

        public String ID
        {
            get { return Id; }
            set { Id = value; }
        }

        public Boolean addSurvey() {

            try {
                var parameters = new Dictionary<String, String>() { { "InquiryNumber", InquiryNumber }, { "Details", Details }, { "Img1", Img1 }, { "Img2", Img2 } };
                //InfoPCMS.db.executeUpdateQuery("insert into Survey values(GETDATE(),'" + InquiryNumber + "','" + Details + "','" + Img1 + "','" + Img2 + "')");
                InfoPCMS.db.executeInsertOrUpdate("spInsertSurvey", parameters);
                return true;
            }catch(Exception ex){

                throw ex;
            }
            return false;
        
        }


        public Boolean updateSurvey() {

            try {
                var parameters = new Dictionary<String, String>() { { "InquiryNumber", InquiryNumber }, { "Details", Details }, { "Img1", Img1 }, { "Img2", Img2 },{"ID",ID} };
                //InfoPCMS.db.executeUpdateQuery("update Survey set DateAdded=GETDATE(), Details='"+Details+"',img1='"+Img1+"',img2='"+Img2+"' where Id='"+ID+"'");
                InfoPCMS.db.executeInsertOrUpdate("spUpdateSurvey", parameters);
                return true;
            }
            catch (Exception ex) {

                throw ex;
            }
            return false;
        
        }

        public Boolean deleteSurvey() {

            try {
                var parameters = new Dictionary<String, String>() { { "Id", ID }};
                InfoPCMS.db.executeInsertOrUpdate("spDeleteSurvey",parameters);
                return true;
            }
            catch (Exception ex) {

                throw ex;
            }
            return false;
        
        }


    }
}
