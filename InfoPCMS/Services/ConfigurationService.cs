using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoPCMS.Services
{
    class ConfigurationService
    {
        private String dbname;
        private String dbsource;
        private String dbusername;
        private String dbpassword;
        private String dbbackuppath;
        private String emailSTMPServer;
        private String emailSTMPServerPort;

        public String EmailSTMPServerPort
        {
            get { return emailSTMPServerPort; }
            set { emailSTMPServerPort = value; }
        }

        public string SERVER_USERNAME { get; set; }

        public String EmailSTMPServer
        {
            get { return emailSTMPServer; }
            set { emailSTMPServer = value; }
        }
        private String emailUsername;

        public String EmailUsername
        {
            get { return emailUsername; }
            set { emailUsername = value; }
        }
        private String emailPassword;

        public String EmailPassword
        {
            get { return emailPassword; }
            set { emailPassword = value; }
        }

       


        public String DBBackupPath
        {
            get { return dbbackuppath; }
            set { dbbackuppath = value; }
        }


        public String DBPassword
        {
            get { return dbpassword; }
            set { dbpassword = value; }
        }


        public String DBUsername
        {
            get { return dbusername; }
            set { dbusername = value; }
        }


        public String DBSource
        {
            get { return dbsource; }
            set { dbsource = value; }
        }


        public String DBName
        {
            get { return dbname; }
            set { dbname = value; }
        }


        public void setConfigurations() {

            DBSource = System.Configuration.ConfigurationSettings.AppSettings["DB_Datasource"].ToString().Trim();
            DBName = System.Configuration.ConfigurationSettings.AppSettings["DB_Name"].ToString().Trim();
            DBUsername = System.Configuration.ConfigurationSettings.AppSettings["DB_Username"].ToString().Trim();
            DBPassword = System.Configuration.ConfigurationSettings.AppSettings["DB_Password"].ToString().Trim();
            DBBackupPath = System.Configuration.ConfigurationSettings.AppSettings["DB_Backup_Path"].ToString().Trim();
            EmailSTMPServer = System.Configuration.ConfigurationSettings.AppSettings["STMP_SERVER"].ToString().Trim();
            EmailSTMPServerPort = System.Configuration.ConfigurationSettings.AppSettings["STMP_SERVER_PORT"].ToString().Trim();
            EmailUsername = System.Configuration.ConfigurationSettings.AppSettings["EMAIL_USERNAME"].ToString().Trim();
            EmailPassword = System.Configuration.ConfigurationSettings.AppSettings["EMAIL_PASSWARD"].ToString().Trim();
            SERVER_USERNAME = System.Configuration.ConfigurationSettings.AppSettings["SERVER_USERNAME"].ToString().Trim(); 
        }

    }
}
