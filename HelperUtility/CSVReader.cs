using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutomation.HelperUtility
{
    public class CSVReader
    {
        public List<string> keyCount;
        public int loopCount = 0;
        public Dictionary<String, Dictionary<String, String>> readCsvData(String pageName)
        {
            keyCount = new List<string>();
            String projectDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            projectDirectory = projectDirectory.Replace("\\bin\\Debug\\", "").Replace("net6.0-windows", "");

            String testSheet = "TestData.csv";

            var filePath = projectDirectory + @"\Resources\TestData\" + pageName + testSheet;

            var lines = File.ReadLines(filePath);
            String[] headers = null;

            var testDataMap = new Dictionary<String, Dictionary<String, String>>();
            String testCaseName = null;
            for (var i = 0; i < lines.Count(); i++)
            {
                if (i == 0)
                {
                    headers = lines.ElementAt(i).Split(',');
                }
                else
                {
                    var data = lines.ElementAt(i).Split(',');
                    var map = new Dictionary<String, String>();
                    testCaseName = data[1].ToString();
                    String methodName = data[2].ToString();
                    String executionStatus = data[3].ToString();
                   // if (executionStatus.Equals("Y"))
                   // {
                        for (int j = 1; j < data.Count(); j++)
                        {
                            String key = headers[j];
                            //+ "_" + i;
                            String value = data[j].ToString();
                            if (value.Equals("NA"))
                            {
                                value = "";
                            }
                            if (value.Equals("Space"))
                            {
                                value = "  ";
                            }

                            map.Add(key, value);
                            if (!keyCount.Contains(i.ToString()))
                            {
                                keyCount.Add(i.ToString());
                            }
                        }

                 //   }
                    testDataMap.Add(testCaseName.ToString(), map);
                }


            }

            return testDataMap;
        }


    }
}
