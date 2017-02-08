using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace InfoPCMS.Services
{
   public class DatabaseService
    {
        static SqlConnection connection;

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

        public DataTable executeSelectQuery(String sql)
        {
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand(@sql, connection);
                cmd.CommandType = CommandType.Text;
                
                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
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

        public DataTable executeSecuredLoginQuery(String username,String password)
        {

            try
            {
                getConnection();

                SqlCommand cmd = new SqlCommand("select * from Users where Username =@user and Password=@pass", connection);
                cmd.Parameters.Add("@user", username);
                cmd.Parameters.Add("@pass", password);

                cmd.CommandType = CommandType.Text;


                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;

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
        
        public void executeUpdateQuery(String sql)
        {
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand(sql, connection);
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

        public void executeInsertOrUpdate(String sql, Dictionary<String, String> parameters = null)
        {
            getConnection();

            try
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue("@" + param.Key, param.Value);
                    }
                }
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

        public DateTime GetServerDateTime()
        {
            DateTime dtSystemDateTime = new DateTime();
            try
            {
                DataTable dtCurDate = new DatabaseService().executeSelectQuery("select GETDATE()as sysdate");

                if (dtCurDate.Rows.Count > 0)
                {

                    dtSystemDateTime = Convert.ToDateTime(dtCurDate.Rows[0]["sysdate"].ToString());
                }

                // MessageBox.Show(SysDate.ToString());

                return dtSystemDateTime;


            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
