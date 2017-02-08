using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoCrm.Tools
{
    public class Globals
    {
        public static string MessageCaption { get; set; }

        public static string DocPath { get; set; }

        public static string DocPathJobs { get; set; }

        public static string CompanyName{ get; set; }
        public static void SetGlobals(string caption,string companyname)
        {
            MessageCaption = caption.ToUpper();
            CompanyName = companyname;
        } 

    }
}
