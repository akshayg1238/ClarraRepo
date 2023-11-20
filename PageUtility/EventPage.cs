using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAutomation.HelperUtility;
using WebAutomation.ConstantUtility;
using static System.Resources.ResXFileRef;
using System.Threading;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;

namespace WebAutomation.PageUtility
{
    class EventPage
    {
        //Define Objects
        IWebDriver driver;
        PageActions pageActions;

        public EventPage(IWebDriver _driver)
        {
            driver = _driver;
            pageActions = new PageActions(driver);
        }

        //Locators
        By eventNavigate = By.ClassName("sidenav-events");
        By newEvent = By.XPath("//button[contains(text(),'New Event')]");
        By appointment = By.XPath("//b[text()='Appointment']");
        By deadline = By.XPath("//b[text()='Deadline']");
        By task = By.XPath("//b[text()='Task']");


        By milestoneNo = By.XPath("//span[contains(text(),'No')]");
        By milestoneYes = By.XPath("//span[contains(text(),'Yes')]");
        By eventCode = By.XPath("//label[contains(text(),'Event Code')]/parent::div/select");
        By dateTextField = By.XPath("(//input[contains(@class,'datepicker')])[1]");
        By calendarIcon = By.XPath("(//div/mat-datepicker-toggle)[1]");
        By reminders = By.XPath("//div[@class='Form-Reminder col']//select[@formcontrolname=\"daysBeforeStart\"]");
        By recurringEvents = By.XPath("//label[contains(text(),'Recurring Event')]/parent::div/select");
        By location = By.XPath("//input[@placeholder='Location']");
        By description = By.XPath("//textarea[@placeholder='Event Description']");
        By notes = By.XPath("//textarea[@placeholder='Notes']");
        By matter = By.XPath("//input[@placeholder='Matter']");
        By matterText = By.XPath("//span[contains(text(),'Test')]");
        By assignedTo = By.XPath("//label[contains(text(),'Assigned')]/parent::div/mat-select/div");
        By dropDownSearchBar = By.Name("event-type-search");
        By dropDownFirstSearchResult = By.XPath("(//mat-option)[2]");
        By dropDownSearchSelectAll = By.XPath("(//mat-option)[1]");
        By followers = By.XPath("//label[contains(text(),'Followers')]/parent::div/mat-select/div");
        By status = By.XPath("//label[contains(text(),'Status')]/parent::div/select");
        By timeHh = By.Name("hours");
        By timeMm = By.Name("minutes");
        By timeAmorPm = By.XPath("//input[@name='minutes']/parent::div/select");
        By timeDeleteIcon = By.XPath("//i[contains(@class,'ddelete-icon')]");
        By durationHh = By.XPath("//select[@class='_col-6 form-control Margin-Between-Rows Events-Duration-Hours-Select ng-untouched ng-pristine ng-valid ng-star-inserted']");
        By durationMm = By.XPath("//select[@class='ml-2 _col-12 form-control ng-untouched ng-pristine ng-valid ng-star-inserted']");
        By noEnddate = By.XPath("//label[contains(text(),'No end date')]");
        By saveButton = By.XPath("//button[contains(@class, 'save-btn') and text()='Save']");
        By assignedToSelectAllCheckbox = By.XPath("//span[contains(text(),'Select All')]/preceding::mat-pseudo-checkbox[contains(@class,'mat-pseudo-checkbox-checked')]");
        By verifyName = By.XPath("(//div[contains(text(),'Test 1')])[last()]");
        By verifyTime = By.XPath("(//div[@class='fc-event-time'])[last()]");
        By eventTitle = By.XPath("(//div[@class='fc-event-title-container']/div)[last()]");

        //Date Calculator Locators
        By dateCalculator = By.XPath("//button[contains(text(),'Date Calculator')]");
        By startDateDateCalculator = By.XPath("//input[@formcontrolname='date']/parent::div");
        By calendarIconDateCalculator = By.XPath("//input[@formcontrolname='date']/parent::div/following-sibling::div");
        By todayDateCalculator = By.XPath("//div[contains(@class,'mat-calendar-body-today')]");
        By dateCalculatorOperation = By.XPath("//select[@formcontrolname='operation']");
        By dateCalculatorNumber = By.XPath("//input[@formcontrolname='number']");
        By dateCalculatorType = By.XPath("//select[@formcontrolname='type']");
        By dateCalculatorResult = By.XPath("//input[@formcontrolname='result']");

        public void fillRequiredFields(EventData eventData)
        {
            selectEventType(eventData);
            selectEventCode(eventData);
            selectDateFromCalendar(eventData);
            selectTime(eventData);
            selectDuration(eventData);
            selectRecurringEvents(eventData);
            enterDescription(eventData);
            enterMatter(eventData);
            selectAssignedTo(eventData);
            selectNoEndDate(eventData);
        }

        public void selectCalculateDate(EventData eventData)
        {
            clickOnDateCalculator();
            clickCalendorIconInDateCalculator();
            clickOnTodayInCalendarCalculator();
            selectOperationInDateCalculator(eventData);
            enterNumberInDateCalculator(eventData);



        }

         public void navigateToEvent()
        {
            pageActions.WaitForElementToBeDisplayed(eventNavigate, 60);
            pageActions.clickButton(eventNavigate);

        }
        public void clickNewEventButton()
        {
            pageActions.WaitForElementToBeDisplayed(newEvent,60);
            pageActions.clickButton(newEvent);

        }
        public void selectEventType(EventData eventData)
        {
            string eventType = eventData.EventType.ToLower();
            Thread.Sleep(6000);
            switch (eventType)
            {
                case "appointment":
                    pageActions.WaitForElementToBeDisplayed(appointment, 60);
                    pageActions.clickButton(appointment);
                    break;
                case "deadline":
                    pageActions.WaitForElementToBeDisplayed(deadline, 60);
                    pageActions.clickButton(deadline);
                    break;
                case "task":
                    pageActions.WaitForElementToBeDisplayed(task, 60);
                    pageActions.clickButton(task);
                    break;
                default:
                    Console.WriteLine("USER TYPE is not supported");
                    break;
            }

        }

        public void selectMilestone(EventData eventData)
        {
            String milestone = eventData.Milestone.ToLower();
            switch (milestone)
            {
                case "yes":
                    pageActions.clickButton( milestoneYes);
                    break; 
                case "no":
                    pageActions.clickButton( milestoneNo);
                    break;
                    default:
                    Console.WriteLine("MILESTONE is not supported");
                    break;
            }

        }

        public void selectEventCode(EventData eventData)
        {
            pageActions.dropdownSelection(eventCode,eventData.EventCode, DropDownConstants.KEY_SELECT_BY_VISIBLE_TEXT);
        }

        public void clickOnDateCalculator()
        {
            pageActions.clickButton(dateCalculator);
        }

        public void enterStartDateInDateCalculator(EventData eventData)
        {
            pageActions.enterText(startDateDateCalculator, eventData.Date);
        }
        public void clickCalendorIconInDateCalculator()
        {
            pageActions.clickButton(calendarIconDateCalculator);
        }
        public void clickOnTodayInCalendarCalculator()
        {
            pageActions.clickButton(todayDateCalculator);
        }

        public void selectOperationInDateCalculator(EventData eventData)
        {
            pageActions.WaitForElementToBeDisplayed(dateCalculatorOperation, 60);
            pageActions.dropdownSelection(dateCalculatorOperation, eventData.Reminders, DropDownConstants.KEY_SELECT_BY_VALUE);

        }

        public void enterNumberInDateCalculator(EventData eventData)
        {
            pageActions.enterText(dateCalculatorNumber, eventData.Date);
        }

        public void selectDaysTypeInDateCalculator(EventData eventData)
        {
            pageActions.WaitForElementToBeDisplayed(dateCalculatorType, 60);
            pageActions.dropdownSelection(dateCalculatorType, eventData.Reminders, DropDownConstants.KEY_SELECT_BY_VALUE);

        }

        public void selectTime(EventData eventData)
        {
            if (!eventData.TimeHh.ToString().Equals("N"))
            {
                pageActions.WaitForElementToBeDisplayed(timeHh, 60);
                pageActions.enterText(timeHh, eventData.TimeHh);
            }
            if (!eventData.TimeMm.ToString().Equals("N"))
            {
                pageActions.enterText(timeMm, eventData.TimeMm);
            }
            if (!eventData.TimeAmorPm.ToString().Equals("N"))
            {
                pageActions.WaitForElementToBeDisplayed(timeAmorPm, 60);
                pageActions.dropdownSelection(timeAmorPm, eventData.TimeAmorPm, DropDownConstants.KEY_SELECT_BY_VISIBLE_TEXT);
            }
            }
       public void selectReminders(EventData eventData)
        {
         pageActions.WaitForElementToBeDisplayed(reminders, 60);         
         pageActions.dropdownSelection(reminders, eventData.Reminders, DropDownConstants.KEY_SELECT_BY_VALUE);
        }
        public void selectRecurringEvents(EventData eventData)
        {
            pageActions.WaitForElementToBeDisplayed(recurringEvents, 60);
            pageActions.dropdownSelection(recurringEvents, eventData.RecurringEvent, DropDownConstants.KEY_SELECT_BY_VISIBLE_TEXT);
        }
        public void selectAssignedTo(EventData eventData)
        {
            pageActions.WaitForElementToBeDisplayed(assignedTo, 60);
           
            pageActions.clickButton(assignedTo);

            if (pageActions.WaitForElementToBeDisplayed(assignedToSelectAllCheckbox,30))
            {
                pageActions.clickButton(dropDownSearchSelectAll);
            }
            else
            {
                for (int i =0; i<=1;i++)
                {
                    pageActions.clickButton(dropDownSearchSelectAll);
                }
            }       
            pageActions.enterText(dropDownSearchBar,"Asya");
            pageActions.clickButton(dropDownFirstSearchResult);
            System.Windows.Forms.SendKeys.SendWait("{ESC}");

        }
        public void selectDuration(EventData eventData)
        {
            if (!eventData.DurationHh.ToString().Equals("N"))
            {
                pageActions.WaitForElementToBeDisplayed(durationHh, 60);
                pageActions.dropdownSelection(durationHh, eventData.DurationHh, DropDownConstants.KEY_SELECT_BY_VALUE);

            }
            if (!eventData.DurationMm.ToString().Equals("N"))
            {
                pageActions.WaitForElementToBeDisplayed(durationMm, 60);
                pageActions.dropdownSelection(durationMm, eventData.DurationMm, DropDownConstants.KEY_SELECT_BY_VALUE);
            }
            }
        public void selectFollowers(EventData eventData)
        {
            pageActions.WaitForElementToBeDisplayed(followers, 60);
            pageActions.clickButton(followers);
            pageActions.enterText(dropDownSearchBar, "Asya");
            pageActions.clickButton(dropDownFirstSearchResult);
            System.Windows.Forms.SendKeys.SendWait("{ESC}");
        }
        public void selectStatus(EventData eventData)
        {
            pageActions.WaitForElementToBeDisplayed(status, 60);
            pageActions.dropdownSelection(status, eventData.Status, DropDownConstants.KEY_SELECT_BY_VISIBLE_TEXT);
        }
        public void enterDescription(EventData eventData)
        {
            pageActions.WaitForElementToBeDisplayed(description, 60);
            pageActions.enterText(description, eventData.Description);
        }
        public void enterNotes(EventData eventData)
        {
            pageActions.WaitForElementToBeDisplayed(notes, 60);
            pageActions.enterText(notes, eventData.Notes);
        }
        public void enterMatter(EventData eventData)
        {
            pageActions.WaitForElementToBeDisplayed(matter, 60);
            pageActions.enterText(matter, eventData.Matter);
            pageActions.WaitForElementToBeDisplayed(matterText, 60);
            pageActions.clickButton(matterText);
        }
        public void selectLocation(EventData eventData)
        {
            pageActions.WaitForElementToBeDisplayed(location, 60);
            pageActions.enterText(location, eventData.Location);
        }
        public void enterDate(EventData eventData)
        {
            pageActions.WaitForElementToBeDisplayed(dateTextField,60);
            pageActions.enterText(dateTextField,eventData.Date);
        }

        public void selectNoEndDate(EventData eventData)
        {
            pageActions.WaitForElementToBeDisplayed(noEnddate, 60);
            pageActions.clickButton(noEnddate);
        }
        
        public void selectDateFromCalendar(EventData eventData)
        {
            if (!eventData.Date.ToUpper().ToString().Equals("N"))
            {
                pageActions.clickButton(calendarIcon);

                string date = eventData.Date.ToLower();
                if (date.Equals("currentdate") || date.Equals("today"))
                {
                    date = CommonUtils.GetCurrentDate("date");
                    if (date.Length == 2 && date[0] == '0')
                    {
                        date = date.Replace("0", "");
                    }
                    driver.FindElement(By.XPath("//div[text()=' " + date + " ']")).Click();
                }
            }
        }

            public void clickSave()
        {
            pageActions.clickButton(saveButton);
        }
        public Boolean verifyMatterText(string expectedName)
        {
            pageActions.WaitForElementToBeDisplayed(verifyName, 60);

            IWebElement name = driver.FindElement(By.XPath("(//div[contains(text(),'Test 1')])[last()]"));
            string actualName = name.Text;
            Console.WriteLine(actualName);
            return actualName.Contains(expectedName);
        }

        public String getEventTime()
        {
            pageActions.WaitForElementToBeDisplayed(verifyTime,60);
            return pageActions.getText(verifyTime);
        }

        public String getEventTitle()
        {
            pageActions.WaitForElementToBeDisplayed(eventTitle, 60);
            return pageActions.getInnerText(eventTitle);
        }

        // public Boolean verifyTimes(string expectedTime)
        // {
        //  pageActions.WaitForElementToBeDisplayed(verifyTime, 60);

        //  IWebElement time = driver.FindElement(By.XPath("(//div[@class=\"fc-event-time\"])[last()]"));
        //  string actualTime = time.Text;
        //  Console.WriteLine(actualTime);
        //  return actualTime.Contains(expectedTime);
        // }


    }
}