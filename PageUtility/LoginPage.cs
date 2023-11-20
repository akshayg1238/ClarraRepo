using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V116.DOM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebAutomation.ConstantUtility;
using WebAutomation.HelperUtility;

namespace WebAutomation.PageUtility
{
    class LoginPage
    {
        //Define Objects
        IWebDriver driver;
        PageActions pageActions;

        public LoginPage(IWebDriver _driver)
        {
            driver = _driver;
            pageActions = new PageActions(driver);
        }

        //Initialize WebElements
        private By username = By.Id("email");
        private By password = By.Id("password");
        private By signIn = By.Id("next");
        private By signInLogo = By.XPath("//h1[@role='heading' or text()='Sign in']");
        private By companyLogo = By.ClassName("companyLogo");
        private By emailMandatoryErrorMessage = By.XPath("//p[text()='Please enter your Email Address']");
        private By passwordMandatoryErrorMessage = By.XPath("//p[text()='Please enter your password']");
        private By invalidEmailErrorMessage = By.XPath("//p[text()='Please enter a valid email address.']");
        private By invalidPasswordErrorMessage = By.XPath("//p[text()=\"We can't seem to find your account\"]");
        private By invalidAccountErrorMessage = By.XPath("//p[text()='Your password is incorrect']");

        //Microsoft Social bannerLogo
        private By companyBannerLogo = By.Id("bannerLogo");
        private By microsoftButton = By.Id("Microsoft");
        private By microsoftUsername = By.Name("loginfmt");
        private By microsoftPassword = By.Name("passwd");
        private By microsoftNextButton = By.XPath("//input[@type='submit']");

        //Google Locators

        public void doLogin(LoginData loginData)
        {
            enterUserName(loginData);
            enterPassword(loginData);
            clickSignIn();
        }

        public void setCredentials(LoginData loginData)
        {
            if (ConfigurationManager.AppSettings["USERTYPE"].Equals("LOCAL"))
            {
                loginData.Username = ConfigurationManager.AppSettings["LOCALUSERNAME"];
                loginData.Password = ConfigurationManager.AppSettings["LOCALPASSWORD"];
            }else if (ConfigurationManager.AppSettings["USERTYPE"].Equals("MICROSOFT"))
            {
                loginData.Username = ConfigurationManager.AppSettings["MICROSOFTUSERNAME"];
                loginData.Password = ConfigurationManager.AppSettings["MICROSOFTPASSWORD"];
            }
            else if (ConfigurationManager.AppSettings["USERTYPE"].Equals("GOOGLE"))
            {
                loginData.Username = ConfigurationManager.AppSettings["GOOGLEUSERNAME"];
                loginData.Password = ConfigurationManager.AppSettings["GOOGLEPASSWORD"];
            }
            else
            {
                Console.WriteLine("NOT A VALID USER TYPE");
            }

        }

        public void microsoftLogin(LoginData loginData)
        {
            clickMicrosoftSocialButton();
            enterMicrosoftUsername(loginData);
            clickNext();
            enterMicrosoftPassword(loginData);
            clickNext();        
        }


        public void enterUserName(LoginData loginData)
        {
            if (pageActions.WaitForElementToBeDisplayed(signInLogo,30) )
            {
                pageActions.WaitForElementToBeDisplayed(companyLogo, 30);
                pageActions.enterText(username, loginData.Username);
            }
            
        }

        public void enterPassword(LoginData loginData)
        {
            pageActions.enterText(password, loginData.Password);
        }

        public void clickSignIn()
        {
            pageActions.WaitForElementToBeDisplayed(signIn,60);
            pageActions.clickButton(signIn);
        }

        public void enterMicrosoftUsername(LoginData loginData)
        {
            if (pageActions.WaitForElementToBeDisplayed(companyBannerLogo, 30))
            {
                pageActions.enterText(microsoftUsername, loginData.Username);
            }
        }

        public void enterMicrosoftPassword(LoginData loginData)
        {
            Thread.Sleep(5000);
            pageActions.enterText(microsoftPassword, loginData.Password);
        }

        public void clickMicrosoftSocialButton()
        {
            pageActions.WaitForElementToBeDisplayed(companyLogo, 60);
            pageActions.clickButton(microsoftButton);
        }

        public void clickNext()
        {
            pageActions.WaitForElementToBeDisplayed(microsoftNextButton,30);
            pageActions.clickButton(microsoftNextButton);

        }

        public Boolean isEmailMandatoryErrorMessageDisplayed()
        {
            return pageActions.WaitForElementToBeDisplayed(emailMandatoryErrorMessage,60);
        }

        public Boolean isPasswordMandatoryErrorMessageDisplayed()
        {
            return pageActions.WaitForElementToBeDisplayed(passwordMandatoryErrorMessage, 60);
        }

        public Boolean isInvalidEmailErrorMessageDisplayed()
        {
            return pageActions.WaitForElementToBeDisplayed(invalidEmailErrorMessage, 60);
        }
        public Boolean isinvalidPasswordErrorMessageDisplayed()
        {
            return pageActions.WaitForElementToBeDisplayed(invalidPasswordErrorMessage, 60);
        }
        public Boolean isinvalidAccountErrorMessageDisplayed()
        {
            return pageActions.WaitForElementToBeDisplayed(invalidAccountErrorMessage, 60);
        }
    }
}
