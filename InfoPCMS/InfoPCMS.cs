//--- Author : Dilukshan Mahendra | Software Engineer, Infocraft Ltd. ---//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using InfoPCMS.Services;
using InfoPCMS.Views;
using InfoCrm.Tools;

namespace InfoPCMS
{
    class InfoPCMS
    {
        public static DatabaseService db;
        public static UserService user;
        public static MessageService message;
        public static ConversionService conversion;
        public static ConfigurationService configurations;
        public static CompanyService company;
        public static Form currentfrm;


        static void Main() {

         //   Application.Run(new frmKeyGen());
            //company = new CompanyService();
            // company.getCompanyInfo();
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                DevExpress.Skins.SkinManager.EnableFormSkins();
                DevExpress.UserSkins.BonusSkins.Register();
                UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

                Globals.SetGlobals("Infocraft CRM Notifications","InfoCraft Ltd.");
                Globals.DocPath = System.Configuration.ConfigurationManager.AppSettings["DocPath"];
                Globals.DocPathJobs = System.Configuration.ConfigurationManager.AppSettings["DocPathJobs"];
                if (!System.IO.Directory.Exists(Globals.DocPath.Trim()))
                {
                    MessageBox.Show("Cannot find the document saved path. Please add following key to configuration file " + "\n  <add key=\"DocPath\" value=\"<Network path>\"/>" , Globals.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                db = new DatabaseService();
                user = new UserService();
                message = new MessageService();
                conversion = new ConversionService();
                configurations = new ConfigurationService();

                configurations.setConfigurations();

                //check the db connection
                DatabaseService dbs =  new DatabaseService();
                dbs.getConnection();
                dbs.executeSelectQuery("select * from Customer where CustomerName is null ");

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
               
                MessageBox.Show("Network Related Error occurred. this might happen because \n 1.Network connection problems \n 2.Database server availability problems \n 3.Wrong system configuration file ", Globals.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown Error occurred. for assistance Please contact " + Globals.CompanyName + ".  ", Globals.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
           

           Application.Run(new frmLogin());

          
        
        }

    }
}
