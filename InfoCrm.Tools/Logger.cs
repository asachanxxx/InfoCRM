using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InfoCrm.Tools
{
    public class Logger
    {

        public static void Log(string message)
        {
            try
            {
                string filename = "InfoCRM_Log.txt";

                if (!File.Exists(filename))
                {
                    FileStream fs = File.Create(filename);
                    fs.Close();
                }

                StreamWriter sr = new StreamWriter(filename, true);
                sr.WriteLine(DateTime.Now.ToString() + "  :   " +  message);
                sr.Close();
            }
            catch (Exception ex) {
            
            }
        }

    }
}
