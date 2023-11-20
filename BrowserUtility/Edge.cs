using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;

namespace WebAutomation.BrowserUtility
{
    class Edge
    {
        //initiating Edge browser
        public IWebDriver initiateBrowser()
        {
            EdgeOptions internetExplorerOptions = new EdgeOptions();
            //internetExplorerOptions.
            IWebDriver driver = null;
            if (ConfigurationManager.AppSettings["OS"].Equals("WIN"))
            {
                driver = new EdgeDriver(internetExplorerOptions);
            }
            else if (ConfigurationManager.AppSettings["OS"].Equals("MAC"))
            {


            }
            return driver;
        }
    }
}
