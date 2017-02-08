using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace InfoPCMS.Services
{
    class LoginService
    {

        protected DataTable dt;

        public Boolean validateLogin(String username, String password)
        {

           

         

            try
            {


              
                dt = InfoPCMS.db.executeSecuredLoginQuery(username, password);
                //if the username and password matches with useraccount table
                if (dt.Rows.Count > 0)
                {

                    //Load Username to this user object
                    InfoPCMS.user.StrUsername = dt.Rows[0]["Username"].ToString();
                    
                    //Get User Category
                    InfoPCMS.user.StrCategory = dt.Rows[0]["Category"].ToString();
                                  

                    //Add user display name
                    InfoPCMS.user.StrDisplayname = dt.Rows[0]["Displayname"].ToString();
                    
                    //Add user designation
                   // InfoPCMS.user.StrDesignation = dt.Rows[0]["Designation"].ToString();

                    //Add user signature path
                    //InfoPCMS.user.Signature = dt.Rows[0]["Signature"].ToString();

                    //Update last access time
                  //  DateTime now = DateTime.Now;
                   // InfoPCMS.db.executeUpdateQuery("update Users set Lastaccess = '" + now + "' where Username = '" + username + "'");

                    
                    return true;

                }


            }
            catch (Exception ex)
            {

                throw ex;

            }


            return false;


        }


    }
}
