using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using WindowsInput;
using WindowsInput.Native;


namespace MRO.ROI.Automation.Selenium
{
    public static class WebElementHelper
    {
        private static object window;

        /// <summary>
        /// Perform click action on the web element
        /// </summary>
        /// <param name="webElementName"></param>
        /// <param name="findElementBy"></param>
        public static void Click_Action(string webElementName, FindElementBy findElementBy,IWebDriver bDriver)
        {
            IWebElement webElement;

            switch (findElementBy)
            {
                case FindElementBy.Id:
                    webElement = bDriver.FindElement(By.Id(webElementName));
                    break;

                case FindElementBy.Xpath:
                    webElement = bDriver.FindElement(By.XPath(webElementName));
                    break;
                default:
                    throw new Exception("Invalid element type");
            }

            Actions action = new Actions(bDriver);
            action.Click(webElement).Build().Perform();
        }

        /// <summary>
        /// Simulates keyboard return/enter key press
        /// </summary>
        public static void Click_Enter()
        {
            InputSimulator simulator = new InputSimulator();
            simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
        }

        public static void ScrollIntoView(string locatorKey, FindElementBy findElementBy)
        {
			IWebElement element;
			if (findElementBy == FindElementBy.Id)
			{
				element = Driver.Instance.FindElement(By.Id(locatorKey));
			}
			else if(findElementBy == FindElementBy.Xpath)
			{
				element = Driver.Instance.FindElement(By.XPath(locatorKey));
			}
			else
			{
				element = Driver.Instance.FindElement(By.TagName(locatorKey));
			}
             
            IWebDriver dr = Driver.Instance;
            IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void Click_Action()
        {
            throw new NotImplementedException();
        }

        public static void ScrollIntoView1()
        {
            //IWebElement element = Driver.Instance.FindElement(By.Id(locatorKey));
            IWebDriver dr = Driver.Instance;
            IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
            js.ExecuteScript("window.scrollBy(0,950);");

        }
        public static bool IsElementPresent(string locatorKey)
        {
            int i = Driver.Instance.FindElements(By.XPath(locatorKey)).Count;
            Console.WriteLine(i + locatorKey);
            if (i > 0)
            {
                return true;
            }
            return false;
        }
        public static bool IsElementMissing(By elementLocator, int timeout = 2)
        {
            bool bRet = false;
            try
            {
                Driver.TurnOffWait();
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(timeout));
                wait.Until(x => x.FindElement(elementLocator));
            }
            catch (Exception)
            {
                //Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                bRet = true;
            }
            //Driver.TurnOnWait();
            return bRet;
        }
        public static IWebElement WaitUntilElementExists(By elementLocator, int timeout = 30)
        {
            try
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(timeout));
                return wait.Until(x => x.FindElement(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }

        public static void AcceptAlert()
        {
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.Instance.SwitchTo().Alert().Accept();
        }
    }

}
