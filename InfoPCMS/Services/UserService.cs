using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace InfoPCMS.Services
{
    class UserService
    {

        private int intId;

        public int IntId
        {
            get { return intId; }
            set { intId = value; }
        }

        private string strUsername;

        public string StrUsername
        {
            get { return strUsername; }
            set { strUsername = value; }
        }
        private string strDisplayname;

        public string StrDisplayname
        {
            get { return strDisplayname; }
            set { strDisplayname = value; }
        }
        private string strStatus;

        public string StrStatus
        {
            get { return strStatus; }
            set { strStatus = value; }
        }
        private string strCategory;

        public string StrCategory
        {
            get { return strCategory; }
            set { strCategory = value; }
        }
        private string strPassword;

        public string StrPassword
        {
            get { return strPassword; }
            set { strPassword = value; }
        }
        private string strEmpId;

        public string StrEmpId
        {
            get { return strEmpId; }
            set { strEmpId = value; }
        }




        public Boolean checkFunctionAuthentication(String functionid)
        {

            try
            {
                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from FunctionToCategory where FunctionID = '" + functionid + "' and CategoryID = '" + StrCategory + "' and Status='ACTIVE'");
                if (dt.Rows.Count > 0)
                {

                    return true;

                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

            return false;


        }

        public Boolean AddUser()
        {

            try
            {

                var parameters = new Dictionary<String, String>() 
                { { "Username", StrUsername }, 
                { "Password", StrPassword }, 
                { "Displayname", StrDisplayname }, 
                { "EmpId", StrEmpId }, 
                 { "Status", StrStatus },
                  { "Category", StrCategory }
                };



                InfoPCMS.db.executeInsertOrUpdate("spInsertUser", parameters);
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;

        }

        public Boolean UpdateUsers()
        {
            try
            {
                var parameters = new Dictionary<String, String>()  { 
                { "Id", IntId.ToString() }, 
                { "Username", StrUsername },               
                { "Displayname", StrDisplayname }, 
                { "EmpId", StrEmpId }, 
                 { "Status", StrStatus },
                  { "Category", StrCategory }
                };

                InfoPCMS.db.executeInsertOrUpdate("spUpdateUsers", parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;

        }

        public void GetUserDetailsById(string Id)
        {
            try
            {
                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Users where Id = '" + Id + "'");
                if (dt.Rows.Count > 0)
                {
                    IntId = Convert.ToInt32(Id);
                    StrUsername = dt.Rows[0]["Username"].ToString();
                    StrPassword = dt.Rows[0]["Password"].ToString();
                    StrDisplayname = dt.Rows[0]["Displayname"].ToString();
                    StrEmpId = dt.Rows[0]["EmpID"].ToString();
                    StrStatus = dt.Rows[0]["Status"].ToString();
                    StrCategory = dt.Rows[0]["Category"].ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;

            }


        }

        public int GetUserIdByUsename(string strUsername)
        {
            int id = 0;
            try
            {


                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Users where Username = '" + strUsername + "'");
                if (dt.Rows.Count > 0)
                {

                    id = Convert.ToInt32(dt.Rows[0]["Id"].ToString());

                }

            }
            catch (Exception ex)
            {

                throw ex;

            }

            return id;


        }

        public string GetPasswordByUsename(string strUsername)
        {
            string strPswd = "";
            try
            {


                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Users where Username = '" + strUsername + "'");
                if (dt.Rows.Count > 0)
                {

                    strPswd = dt.Rows[0]["Password"].ToString();

                }

            }
            catch (Exception ex)
            {

                throw ex;

            }

            return strPswd;


        }

        public int GetUserIdByEmpId(string strEmpID)
        {
            int id = 0;
            try
            {
                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Users where EmpID = '" + strEmpID + "'");
                if (dt.Rows.Count > 0)
                {

                    id = Convert.ToInt32(dt.Rows[0]["Id"].ToString());

                }

            }
            catch (Exception ex)
            {

                throw ex;

            }

            return id;


        }

        public string GetEmpNameByEmpId(string strEmpID)
        {
            string strEmpName = "";
            try
            {
                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Employee where Id = '" + strEmpID + "'");
                if (dt.Rows.Count > 0)
                {

                    strEmpName = dt.Rows[0]["EmployeeName"].ToString().Trim();

                }

            }
            catch (Exception ex)
            {

                throw ex;

            }

            return strEmpName;


        }

        public void GetUserDetailsByEmpId(string EmpId) {
            try
            {
                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Users where EmpID = '" + EmpId + "'");
                if (dt.Rows.Count > 0)
                {
                    IntId = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    StrUsername = dt.Rows[0]["Username"].ToString();
                    StrPassword = dt.Rows[0]["Password"].ToString();
                    StrDisplayname = dt.Rows[0]["Displayname"].ToString();
                    StrEmpId = dt.Rows[0]["EmpID"].ToString();
                    StrStatus = dt.Rows[0]["Status"].ToString();
                    StrCategory = dt.Rows[0]["Category"].ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;

            }
        
        }         
        public bool CheckUserAccountExist(string EmpId)
        {
                bool blActExist = false;

            try
            {
                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Users where EmpID = '" + EmpId + "'");
                if (dt.Rows.Count > 0)
                {
                    blActExist = true;
                }

            }
            catch (Exception ex)
            {

                throw ex;

            }

                return blActExist;

        }

        public bool CheckUserNameExist(string strUsername)
        {
            bool blActExist = false;

            try
            {
                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Users where Username = '" + strUsername + "'");
                if (dt.Rows.Count > 0)
                {
                    blActExist = true;
                }

            }
            catch (Exception ex)
            {

                throw ex;

            }

            return blActExist;

        }

        public bool CheckUserNameExist(string strUsername,string strUserId)
        {
            bool blActExist = false;

            try
            {
                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Users where Username = '" + strUsername + "' AND Id <> '" + strUserId + "'");
                if (dt.Rows.Count > 0)
                {
                    blActExist = true;
                }

            }
            catch (Exception ex)
            {

                throw ex;

            }

            return blActExist;

        }
    }
}
