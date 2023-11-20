using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutomation.HelperUtility
{
    class CommonUtils
    {

        public static void deleteFile(String filePath)
        {

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("File deleted successfully.");
            }
        }

        //create directory if not available
        public static void CreateIfMissing(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
        }

        public static String GetCurrentDate(string value)
        {
            String returnValue = "";
            if (value.ToLower().Equals("date"))
            {
                returnValue = DateTime.Now.ToShortDateString().ToString().Split('-')[0];              
            }
            else if (value.ToLower().Equals("month"))
            {
                returnValue = DateTime.Now.ToShortDateString().ToString().Split('-')[1];
            }
            else if (value.ToLower().Equals("year"))
            {
                returnValue = DateTime.Now.ToShortDateString().ToString().Split('-')[2];
            }
            return returnValue;
        }
    }
}
