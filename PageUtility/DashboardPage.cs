using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAutomation.HelperUtility;

namespace WebAutomation.PageUtility
{
    class DashboardPage
    {

        //Define Objects
        IWebDriver driver;
        PageActions pageActions;

        public DashboardPage(IWebDriver _driver)
        {
            driver = _driver;
            pageActions = new PageActions(driver);
        }

        //Locators
        private By myProfileIcon = By.Id("acc-photo");
        private By dashboardDropDown = By.XPath("(//mat-icon[text()='keyboard_arrow_down'])[1]"); 

        public Boolean isMyAccountIconDisplayed()
        {
            return pageActions.WaitForElementToBeDisplayed(myProfileIcon, 60);
        }

        public void clickOnDashboardDropdown()
        {
            pageActions.WaitForElementToBeDisplayed(dashboardDropDown,60);
            pageActions.clickButton(dashboardDropDown);
        }
    }
}
