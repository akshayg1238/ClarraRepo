using NUnit.Framework.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using System.Threading;
using WebAutomation.HelperUtility;
using System.Reflection;
using WebAutomation.BrowserUtility;

namespace WebAutomation.HelperUtility
{
    public class BaseFixture
    {
        public static ExtentReports extent;
        public ExtentTest test;
        ManageDriver manageDriver = new ManageDriver();
        public IWebDriver driver;
        public PageActions pageActions;

        [OneTimeSetUp]
        public void Setup()
        {
           
            String path = System.AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("\\bin\\Debug\\", "") + "\\TestReports";
            CommonUtils.deleteFile(path + "\\extent.json");
            ExtentSparkReporter spark = new ExtentSparkReporter(path+"\\testReport.html");
            ExtentJsonFormatter json = new ExtentJsonFormatter(path+"\\extent.json");
            extent = new ExtentReports();     
            extent.CreateDomainFromJsonArchive(path + "\\extent.json");
            extent.AttachReporter(json, spark);

        }

        [OneTimeTearDown]
        protected void TearDown()
        {
            
            extent.Flush();
            
        }

        [SetUp]
        public void BeforeTest()
        {
            //initiate browser
            driver = manageDriver.initiateDriver();

            //open Application
            manageDriver.openApplication(driver);

            //set report test case name
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            pageActions = new PageActions(driver);

        }

      [TearDown]
        public void AfterTest()
        {
           
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            String projectDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            projectDirectory = projectDirectory.Replace("\\bin\\Debug\\", "");
            String folderPath = projectDirectory + "/TestReports/Screenshot/";

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    pageActions.CaptureScreenshot(driver, TestContext.CurrentContext.Test.Name);
                    test.Fail(MediaEntityBuilder.CreateScreenCaptureFromPath(folderPath+ TestContext.CurrentContext.Test.Name+".png").Build());
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }
            
            test.Log(logstatus, "Test ended with " + logstatus + stacktrace);

         // driver.Close();
         // driver.Quit();
        }
    }
}
