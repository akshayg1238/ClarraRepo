using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.IE;

namespace WebApplication.BrowserUtility
{
    class Safari
    {
        //initiating Safari browser
        public IWebDriver initiateBrowser()
        {
            SafariOptions safariOptions = new SafariOptions();
            IWebDriver webDriver = null;
            if(ConfigurationManager.AppSettings["OS"].Equals("WIN"))
            {
                webDriver = new SafariDriver(safariOptions);
            }
            else if (ConfigurationManager.AppSettings["OS"].Equals("MAC"))
            {
                //webDriver = new SafariDriver();
            }
            return webDriver;
        }
    }
}
