using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.BrowserUtility
{
    class FireFox
    {
        //initiating FireFox browser
        public IWebDriver initiateBrowser()
        {
            FirefoxOptions fireFoxOptions = new FirefoxOptions();
            IWebDriver driver = null;
            if (ConfigurationManager.AppSettings["OS"].Equals("WIN"))
            {
                driver = new FirefoxDriver(fireFoxOptions);
            }
            else if (ConfigurationManager.AppSettings["OS"].Equals("MAC"))
            {

            }
            return driver;
            
        }
    }
}
