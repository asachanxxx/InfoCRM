using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InfoPCMS.Services {
    class Email_Manager {
        public Boolean SendEmail(string ToEmail,string subject,String MailBody) {

            //String url = "http://10.40.2.241:9093//api/peAPI/sendEmail?Body=" + MailBody + "&ToEmail=" + ToEmail + "&Subject=" + subject + "";

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.Method = "GET";
            //System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            //long length = 0;
            //try {
            //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
            //        length = response.ContentLength;
            //        Stream dataStream = response.GetResponseStream();
            //        StreamReader reader = new StreamReader(dataStream);
            //        string responseFromServer = reader.ReadToEnd();
            //        return Convert.ToBoolean(responseFromServer);
            //    }
            //} catch (Exception ex) {
            //    return false;
            //}

            Email_Mdl emodel = new Email_Mdl();
            emodel.MailBody = MailBody;
            emodel.ToEmail = ToEmail;
            emodel.subject = subject;
            
            string json = JsonConvert.SerializeObject(emodel);
            String url =  "http://10.40.2.241:9093/api/peAPI/sendEmail/";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(json);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream()) {
                dataStream.Write(byteArray,0,byteArray.Length);
            }
            long length = 0;
            try {

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                    length = response.ContentLength;
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    return Convert.ToBoolean(responseFromServer);
                }
            } catch (Exception ex) {
                return false;
            }
        }
     }

    public class Email_Mdl {
        public String MailBody { get; set; }
        public String ToEmail { get; set; }
        public String subject { get; set; }

    }
}
