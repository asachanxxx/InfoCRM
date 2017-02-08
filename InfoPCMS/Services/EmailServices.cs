using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Data.OleDb;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Web;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;


namespace InfoPCMS.Services
{
    class EmailServices
    {
        static bool mailSent = false;

        string strStmpServer = InfoPCMS.configurations.EmailSTMPServer.ToString();
        string strEmailUsername = InfoPCMS.configurations.SERVER_USERNAME.ToString();
        string strEmailPassword = InfoPCMS.configurations.EmailPassword.ToString();
        string strStmpServerPort = InfoPCMS.configurations.EmailSTMPServerPort.ToString();
        string mailFrom1 = InfoPCMS.configurations.EmailUsername.ToString();

        public void SendEmails(string mailFrom, string mailTo, string Subject, string Body)
        {
            mailFrom = mailFrom1;
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(strStmpServer);
                //SmtpClient SmtpServer = new SmtpClient("smtpout.secureserver.net");
                mail.From = new MailAddress(mailFrom);
                mail.To.Add(mailTo);
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;

                //SmtpServer.Port = 587;
                SmtpServer.Port = Convert.ToInt32(strStmpServerPort);


                //SmtpServer.EnableSsl = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(strEmailUsername, strEmailPassword);
                //SmtpServer.Credentials = CredentialCache.DefaultNetworkCredentials;
                //SmtpServer.UseDefaultCredentials = true;
                SmtpServer.EnableSsl = false;

               // SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                InfoCrm.Tools.Logger.Log("Email From : " + mailFrom + "  and Email TO : " + mailTo + " Username : " + strEmailUsername + "  and Password : " + strEmailPassword + "");


                SmtpServer.Send(mail);
                //MessageBox.Show("mail Send");

              



            }
            catch (Exception ex)
            {
               //MessageBox.Show(ex.ToString());
                InfoCrm.Tools.Logger.Log("Error : " + ex.Message + "  and Inner EX : " );

               XtraMessageBox.Show("Email Didn't Send", "Error");
               // throw ex;
            }
        
        
        }

        public void SendEmailsToMulti(string mailFrom, List<string> mailTo, string Subject, string Body)
        {

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(strStmpServer);
                //SmtpClient SmtpServer = new SmtpClient("smtpout.secureserver.net");
                mail.From = new MailAddress(mailFrom);
                //mail.To.Add(mailTo);

                foreach (string m in mailTo)
                {
                    mail.To.Add(m);
                }                               
                
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;

                SmtpServer.Port = Convert.ToInt32(strStmpServerPort);
                //SmtpServer.EnableSsl = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(strEmailUsername, strEmailPassword);
                //SmtpServer.Credentials = CredentialCache.DefaultNetworkCredentials;
                //SmtpServer.UseDefaultCredentials = true;
                SmtpServer.EnableSsl = true;

                // SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;


                SmtpServer.Send(mail);
                //MessageBox.Show("mail Send");






            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                XtraMessageBox.Show("Email Didn't Send", "Error");
               // throw ex;
            }


        }

        public void SendEmailsCC(string mailFrom, string mailTo, string mailCC, string Subject, string Body)
        {

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(strStmpServer);
                //SmtpClient SmtpServer = new SmtpClient("smtpout.secureserver.net");
                mail.From = new MailAddress(mailFrom);
                mail.To.Add(mailTo);
                mail.CC.Add(mailCC);
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;

                SmtpServer.Port = Convert.ToInt32(strStmpServerPort);
                //SmtpServer.EnableSsl = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(strEmailUsername, strEmailPassword);
                //SmtpServer.Credentials = CredentialCache.DefaultNetworkCredentials;
                //SmtpServer.UseDefaultCredentials = true;
                SmtpServer.EnableSsl = true;

                // SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;


                SmtpServer.Send(mail);
                //MessageBox.Show("mail Send");

                //XtraMessageBox.Show("Email Didn't Send", "Error");




            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                XtraMessageBox.Show("Email Didn't Send", "Error");
               // throw ex;
            }


        }

      

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;
            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }
    }
}
