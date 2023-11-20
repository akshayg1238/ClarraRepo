using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutomation.BrowserUtility
{
    class GoogleChrome
    {
        //initiating Chrome browser
        public IWebDriver initiateBrowser()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("start-maximized");
            IWebDriver driver = null;
            if (ConfigurationManager.AppSettings["OS"].Equals("WIN"))
            {

                driver = new ChromeDriver(chromeOptions);

            }
            else if (ConfigurationManager.AppSettings["OS"].Equals("MAC"))
            {


            }

            // var operatingSystem = ConfigurationManager.AppSettings["Browsers"];
            //  String projectDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            //  String downloadFilepath = projectDirectory + @"\Documents\";
            //  projectDirectory = projectDirectory.Replace("\\bin\\Debug\\", "");
            //  ChromeDriverService service = ChromeDriverService.CreateDefaultService(projectDirectory + @"\Drivers", "chromedriver.exe");
            //  var driver = new ChromeDriver(service, chromeOptions);
            return driver;


        }
    }
}
