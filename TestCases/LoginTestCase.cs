using AventStack.ExtentReports.Model;
using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V116.Debugger;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using WebAutomation.HelperUtility;
using WebAutomation.BrowserUtility;
using WebAutomation.ConstantUtility;
using WebAutomation.HelperUtility;
using WebAutomation.PageUtility;
using NUnit.Framework.Legacy;
using WebAutomation.PageUtility;

namespace WebAutomation.TestCases
{
    [Parallelizable(ParallelScope.Fixtures)]
    class LoginTestCase : BaseFixture
    {
        //Initialize Objects
        CSVReader csvReader = new CSVReader();
        LoginData loginData = new LoginData();
        Dictionary<String, Dictionary<String, String>> testDataMap;
        LoginPage loginPage;
        DashboardPage dashboardPage;

        [SetUp]
        public void setupConfigurations()
        {       
                //read test data and store in testDataMap
                testDataMap = csvReader.readCsvData("Login");

                //set Test Data
                string methodName = TestContext.CurrentContext.Test.Name;
                setTestData(methodName);   

                loginPage = new LoginPage(driver);   
                dashboardPage = new DashboardPage(driver);
                test.AssignCategory("Login");
        }
        [Test]
        public void loginWithoutUsernameAndPassword()
        {
            
            loginPage.clickSignIn();
            ClassicAssert.IsTrue(loginPage.isEmailMandatoryErrorMessageDisplayed());
            ClassicAssert.IsTrue(loginPage.isPasswordMandatoryErrorMessageDisplayed());
        }
        [Test]
        public void loginWithBlankSpaceAsUsernameAndPassword()
        {
            loginPage.doLogin(loginData);
            ClassicAssert.IsTrue(loginPage.isEmailMandatoryErrorMessageDisplayed());
        }
        [Test]
        public void loginWithInvalidFormatAsUsername()
        {
            loginPage.doLogin(loginData);
            ClassicAssert.IsTrue(loginPage.isInvalidEmailErrorMessageDisplayed());
        }
        [Test]
        public void loginWithNonExistingUser()
        {
            loginPage.doLogin(loginData);
            ClassicAssert.IsTrue(loginPage.isinvalidPasswordErrorMessageDisplayed());
        }
        [Test]
        public void loginWithValidUsernameAndInvalidPassword()
        {
            loginPage.doLogin(loginData);
            ClassicAssert.IsTrue(loginPage.isinvalidAccountErrorMessageDisplayed());
        }
        [Test]
        public void loginWithValidUsernameAndPassword()
        {
            loginPage.doLogin(loginData);
            ClassicAssert.IsTrue(dashboardPage.isMyAccountIconDisplayed());
        }

        [Test]
        public void loginWithValidMicrosoftUsernameAndPassword()
        {
            loginPage.microsoftLogin(loginData);
        }


        public void setTestData(string methodName)
        {
            loginData.Username = testDataMap[methodName]["Username"];
            loginData.Password = testDataMap[methodName]["Password"];
        }
    }
}
