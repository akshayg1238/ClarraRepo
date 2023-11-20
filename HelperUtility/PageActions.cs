using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebAutomation.HelperUtility
{
    public class PageActions
    {

        //Define Objects
        IWebDriver driver;

        public PageActions(IWebDriver _driver)
        {
            driver = _driver;

        }


        public void clickButton(By byLocator)
        {
            IWebElement locator = driver.FindElement(byLocator);
            try
            {
                
                highlightText(locator);
                locator.Click();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public void enterText(By byLocator, String text)
        {
            IWebElement locator = driver.FindElement(byLocator);

            try
            {   
                    highlightText(locator);
                    locator.Clear();
                    locator.SendKeys(text);        
            }
            catch (Exception e)
            {
                Console.WriteLine(text + " field is not diplayed" + e);
            }
        }


        /**
	 * 
	 * @param element
	 */
        //highlightText on screen
        public void highlightText(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].setAttribute('style', 'background: yellow; border: 2px solid red;');", element);
        }

        //Vertical scroll
        public void scrollTo(String scrollValue)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("window.scrollBy(0," + scrollValue + ")", "");
        }

        //Click using Java Script Executor
        public void clickJavaScript(By byLocator)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", driver.FindElement(byLocator));
        }

        /**
    * 
    * @param byLocator
    * @param Index
    * @param selectionType
    */
        public void dropdownSelection<T>(By byLocator, T Index, String selectionType)
        {
            IWebElement locator = driver.FindElement(byLocator);
            try
            {
                if (locator.Displayed)
                {
                    SelectElement select = new SelectElement(locator);
                    switch (selectionType)
                    {
                        case DropDownConstants.KEY_SELECT_BY_INDEX:
                            select.SelectByIndex(Int32.Parse(Index.ToString()));
                            break;
                        case DropDownConstants.KEY_DESELECT_BY_INDEX:
                            select.DeselectByIndex(Int32.Parse(Index.ToString()));
                            break;
                        case DropDownConstants.KEY_SELECT_BY_VALUE:
                            select.SelectByValue(Convert.ToString(Index));
                            break;
                        case DropDownConstants.KEY_DESELECT_BY_VALUE:
                            select.DeselectByValue(Convert.ToString(Index));
                            break;
                        case DropDownConstants.KEY_SELECT_BY_VISIBLE_TEXT:
                            select.SelectByText(Convert.ToString(Index));
                            break;
                        case DropDownConstants.KEY_DESELECT_BY_VISIBLE_TEXT:
                            select.DeselectByText(Convert.ToString(Index));
                            break;
                        default:
                            throw new Exception("Selection Type mismatch..!!");

                    }
                    Console.WriteLine("dropdown item is selected.");
                }
                else
                {
                    Console.WriteLine(" dropdown item is not displayed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" method is throwing:- " + e);
            }
        }


        public Boolean WaitForElementToBeDisplayed( By locator, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(d =>
            {
                IWebElement element = d.FindElement(locator);
                if (element.Displayed)
                    highlightText(element);
                    return true;
            });
        }

        public Boolean WaitForElementToBeEnabled(By locator, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(d =>
            {
                IWebElement element = d.FindElement(locator);
                if (element.Enabled)
                    highlightText(element);
                return true;
                return false;
            });
        }

        public String getText(By byLocator)
        {
            IWebElement locator = driver.FindElement(byLocator);

            return locator.Text;
        }

        public String getInnerText(By byLocator)
        {
            IWebElement locator = driver.FindElement(byLocator);
            return locator.GetAttribute("innerText");
        }

        public void CaptureScreenshot(IWebDriver driver, String fileName)
        {
            ITakesScreenshot screen = driver as ITakesScreenshot;  // ITakesScreenshot is  an interface inside OpenQA.Selenium namespace
            Screenshot scrnst = screen.GetScreenshot();
            String projectDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
         //   String randomNumber = DateTime.Now.ToString("yyyyMMddHHmmss");
            projectDirectory = projectDirectory.Replace("\\bin\\Debug\\", "");
            String folderPath = projectDirectory + "/TestReports/Screenshot/";
            CommonUtils.CreateIfMissing(folderPath);
            if (fileName.Length > 120)
            {
                Console.WriteLine("fileName.length= " + fileName.Length);
                fileName = fileName.Substring(0, 100);
                Console.WriteLine(fileName);

            }
            string screenshot = projectDirectory + "/TestReports/Screenshot/" + fileName + ".png";
            scrnst.SaveAsFile(screenshot);
        }
        

    }
}
