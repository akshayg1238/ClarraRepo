using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.BrowserUtility;

namespace WebAutomation.BrowserUtility
{
    class ManageDriver
    {
        public IWebDriver initiateDriver()
        {
            String browserName = ConfigurationManager.AppSettings["BROWSER"];
          
            if (browserName.Equals("CHROME"))
            {
                // return driver instance of Chrome
                GoogleChrome chrome = new GoogleChrome();
                return chrome.initiateBrowser();
            }
            else if (browserName.Equals("EDGE"))
            {
                // return driver intance of Edge
                Edge Edge = new Edge();
                return Edge.initiateBrowser();
            }
            else if (browserName.Equals("SAFARI"))
            {
                // return driver intance of Safari
                Safari safari = new Safari();
                return safari.initiateBrowser();
            }
            else if (browserName.Equals("FIREFOX"))
            {
                // return driver intance of Firefox
                FireFox fireFox = new FireFox();
                return fireFox.initiateBrowser();
            }
            else
            {
                Console.WriteLine(browserName + " browser is not supported");
            }
            return null;

        }

        public void openApplication(IWebDriver driver)
        {
              String applicationURL = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["ENV"]];
              driver.Navigate().GoToUrl(applicationURL);

        }

    }
}
