using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;

namespace MRO.ROI.Automation.Pages.Common
{
    public static class LoginMenuPage
    {
        public static void GoToLoginMenuPage(IWebDriver bDriver, string webAppURL)
        {
            DebugUtil.DebugMessage("Login Menu page");
            bDriver.Navigate().GoToUrl(webAppURL);
        }

        public static bool IsAtLoginMenuPage
        {
            get
            {
                var loginMenuLabel = Driver.Instance.FindElement(By.XPath("//td[contains(text(),'Choose a Login Page')]"));
                return loginMenuLabel.Text == "Choose a Login Page";
            }
        }
    }
}
