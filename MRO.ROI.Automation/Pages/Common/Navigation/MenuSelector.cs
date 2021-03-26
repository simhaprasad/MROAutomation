using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Common.Navigation
{
    public class MenuSelector
    {
        public static void Select(string topLevelMenuText, string subMenuText)
        {
            //Driver.WaitUntilDOMLoaded();
            var topLevelMenu = Driver.Instance.FindElement(By.XPath($"//td[contains(text(),'{topLevelMenuText}')]"));
            Actions action = new Actions(Driver.Instance);
            action.Click(topLevelMenu).Build().Perform();
            //action.Perform();
            Driver.Wait(TimeSpan.FromSeconds(2));

            Driver.Instance.FindElement(By.XPath($"//td[contains(text(),'{subMenuText}')]")).Click();
        }

        public static void SelectRoiAdmin(string topLevelMenuText, string subMenuText, IWebDriver bdriver)
        {
            //Driver.WaitUntilDOMLoaded();
            Console.Write($"//td[contains(text(),'{topLevelMenuText}')]");
            var wait = new WebDriverWait(bdriver, new TimeSpan(0, 0, 55));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//table[@smenuname='mnuROIAdmin']//td[contains(text(),'{topLevelMenuText}')]")));

            var topLevelMenu = bdriver.FindElement(By.XPath($"//table[@smenuname='mnuROIAdmin']//td[contains(text(),'{topLevelMenuText}')]"));
            Actions action = new Actions(bdriver);
            action.Click(topLevelMenu).Build().Perform();
            //action.Perform();
            Driver.Wait(TimeSpan.FromSeconds(2));

            bdriver.FindElement(By.XPath($"//td[contains(text(),'{subMenuText}')]")).Click();
        }


    }
    
    public class FacMenuSelector
    {
        public static void Select(string topLevelMenuText, string subMenuText)
        {
            //Driver.WaitUntilDOMLoaded();
            var wait = new WebDriverWait(Driver.Instance, new TimeSpan(0, 0, 55));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//td[contains(text(),'{topLevelMenuText}')]")));

            var topLevelMenu = Driver.Instance.FindElement(By.XPath($"//td[contains(text(),'{topLevelMenuText}')]"));
            Actions action = new Actions(Driver.Instance);
            action.Click(topLevelMenu).Build().Perform();
            //action.Perform();
            Driver.Wait(TimeSpan.FromSeconds(2));

            Driver.Instance.FindElement(By.XPath($"//td[contains(text(),'{subMenuText}')]")).Click();
        }
    }
}
