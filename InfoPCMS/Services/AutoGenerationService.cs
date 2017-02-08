using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace InfoPCMS.Services
{
    class AutoGenerationService
    {


        SqlConnection connection;
        public string executeGetAutonoQuery(String maintype)
        {
            try
            {
                getConnection();

                // pass the type of the Auto Number needed . Eg. Inquiry Novitho,Inquiry GSP etc. ...
                string getmaintype = maintype;


                SqlCommand cmd = new SqlCommand("spGetAutoNo_ProInv", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();



                // input parm
                SqlParameter Mtype = cmd.Parameters.Add("@MainType", SqlDbType.VarChar, 50);
                Mtype.Value = getmaintype;

                // output parm
                SqlParameter Autono = cmd.Parameters.Add("@CURRCODE", SqlDbType.VarChar, 50);
                Autono.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                string getnewcode = Autono.Value.ToString();

                return getnewcode;




            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
            finally
            {

                connection.Close();
            }

        }


        public void getConnection()
        {

            try
            {
                connection = new SqlConnection("Data Source='" + InfoPCMS.configurations.DBSource.ToString() + "';Initial Catalog='" + InfoPCMS.configurations.DBName.ToString() + "';User ID='" + InfoPCMS.configurations.DBUsername.ToString() + "';Password='" + InfoPCMS.configurations.DBPassword.ToString() + "'");
                connection.Open();


            }
            catch (Exception e)
            {

                throw e;
            }

        }


        public void executesetAutonoQuery(String maintype)
        {

            try
            {
                getConnection();

                // pass the type of the Auto Number needed . Eg. Inquiry Novitho,Inquiry GSP etc. ...
                string getmaintype = maintype;


                SqlCommand cmd = new SqlCommand("spSetAutoNo_ProInv", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();



                // input parm
                SqlParameter FunctionType = cmd.Parameters.Add("@FunctionType", SqlDbType.NVarChar, 50);
                FunctionType.Value = maintype;


                SqlParameter FunctionSubType = cmd.Parameters.Add("@FunctionSubType", SqlDbType.NVarChar, 50);
                FunctionSubType.Value = "";


                SqlParameter Inquiryno = cmd.Parameters.Add("@Inquiryno", SqlDbType.NVarChar, 50);
                Inquiryno.Value = "";


                SqlParameter Quotno = cmd.Parameters.Add("@Quotno", SqlDbType.NVarChar, 50);
                Quotno.Value = "";



                SqlParameter Autono = cmd.Parameters.Add("@ISSUCCESS", SqlDbType.Bit, 1);
                Autono.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();






            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {

                connection.Close();
            }

        }


    }
}
