using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAutomation.PageUtility;
using WebAutomation.ConstantUtility;
using WebAutomation.HelperUtility;
using WebAutomation.PageUtility;
using System.Threading;
using NUnit.Framework.Legacy;
using System.Windows.Forms;

namespace WebAutomation.TestCases
{
     class EventTestCase : BaseFixture
    {
        //Initialize Objects
        CSVReader csvReader = new CSVReader();
        Dictionary<String, Dictionary<String, String>> testDataMap;
        LoginPage loginPage;
        DashboardPage dashboardPage;
        EventPage eventPage;
        LoginData loginData = new LoginData();
        EventData eventData = new EventData();

        [SetUp]
        public void setupConfigurations()
        {
            //read test data and store in testDataMap
            testDataMap = csvReader.readCsvData("Event");
            test.AssignCategory("Event");

            //set Test Data
            string methodName = TestContext.CurrentContext.Test.Name;
            
            loginPage = new LoginPage(driver);
            loginPage.setCredentials(loginData);
            loginPage.doLogin(loginData);

            dashboardPage = new DashboardPage(driver);
            dashboardPage.isMyAccountIconDisplayed();

            eventPage = new EventPage(driver);
            eventData.EventType = testDataMap[methodName]["EventType"];
            eventData.Milestone = testDataMap[methodName]["Milestone"];
            eventData.EventCode = testDataMap[methodName]["EventCode"];
            eventData.Date = testDataMap[methodName]["Date"];
            eventData.Reminders = testDataMap[methodName]["Reminders"];
            eventData.RecurringEvent = testDataMap[methodName]["RecurringEvent"];
            eventData.Location = testDataMap[methodName]["Location"];
            eventData.Description = testDataMap[methodName]["Description"];
            eventData.Notes = testDataMap[methodName]["Notes"];
            eventData.Matter = testDataMap[methodName]["Matter"];
            eventData.AssignedTo = testDataMap[methodName]["AssignedTo"];
            eventData.Followers = testDataMap[methodName]["Followers"];
            eventData.Status = testDataMap[methodName]["Status"];
            eventData.TimeHh = testDataMap[methodName]["TimeHh"];
            eventData.TimeMm = testDataMap[methodName]["TimeMm"];
            eventData.TimeAmorPm = testDataMap[methodName]["TimeAmorPm"];
            eventData.DurationHh = testDataMap[methodName]["DurationHh"];
            eventData.DurationMm = testDataMap[methodName]["DurationMm"];
            eventData.Operation = testDataMap[methodName]["Operation"];


        }

        [Test]
        public void CreateAnAppointmentEventAndValidateIfEventIsCreatedSucessfully()
        {
            Console.WriteLine("************");

            eventPage.navigateToEvent();
            eventPage.clickNewEventButton();
            eventPage.selectEventType(eventData);
            eventPage.fillRequiredFields(eventData);
            //eventPage.clickSave();
            //ClassicAssert.IsTrue(eventPage.verifyMatterText(eventData.Matter));
          //  ClassicAssert.IsTrue(eventPage.verifyTimes(eventData.Date));
            // eventPage.enterDate(eventData);

        }
        [Test]
        public void CreateAnDeadlineEventAndValidateIfEventIsCreatedSucessfully()
        {
             eventPage.navigateToEvent();
             eventPage.clickNewEventButton();        
             eventPage.fillRequiredFields(eventData);
             eventPage.clickSave();
             ClassicAssert.IsTrue(eventPage.getEventTime().Contains(eventData.TimeHh +":"+ eventData.TimeMm));
             ClassicAssert.IsTrue(eventPage.getEventTitle().Contains(eventData.Description));
             ClassicAssert.IsTrue(eventPage.getEventTitle().Contains(eventData.Matter));
        }
        [Test]
        public void CreateAnTaskEventAndValidateIfEventIsCreatedSucessfully()
        {
            eventPage.navigateToEvent();
            eventPage.clickNewEventButton();
            eventPage.fillRequiredFields(eventData);
            eventPage.clickSave();
            ClassicAssert.IsTrue(eventPage.getEventTitle().Contains(eventData.Description));
            ClassicAssert.IsTrue(eventPage.getEventTitle().Contains(eventData.Matter));
        }

        [Test]
        public void CreateAnAllDayDeadlineEvent(){

            eventPage.navigateToEvent();
            eventPage.clickNewEventButton();
            eventPage.fillRequiredFields(eventData);
            eventPage.clickSave();
            ClassicAssert.IsTrue(eventPage.getEventTitle().Contains(eventData.Description));
            ClassicAssert.IsTrue(eventPage.getEventTitle().Contains(eventData.Matter));
        }

        [Test]
        public void VerifyDateCalculatorAddRegulardays()
        {

            eventPage.navigateToEvent();
            eventPage.clickNewEventButton();
         //   eventPage.fillRequiredFields(eventData);
            eventPage.selectCalculateDate(eventData);

        }


    }
}
