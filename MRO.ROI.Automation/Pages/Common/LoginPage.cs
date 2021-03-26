using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages.Common
{
    public class LoginPage
    {
        public static bool IsAtLoginPage
        {
            get
            {
                var LoginPageLabel = Driver.Instance.FindElement(By.XPath("//td[contains(text(),'Please Login')]"));
                return LoginPageLabel.Text == "Please Login";
            }
        }

        public static bool IsAtTestFacility
        {
            get
            {
                var facilityLabel = Driver.Instance.FindElement(By.Id("mroheader_MROPageHead1_ctl00_lblSystem2"));
                return facilityLabel.Text == "ROI Test Facility";
            }
        }

        public static void GoToROIExternalRequesterPortal()
        {
            Driver.Instance.Navigate().GoToUrl(Driver.BaseAddress);
            Driver.Wait(TimeSpan.FromSeconds(2));
            var extRoiRequesterPortal = Driver.Instance.FindElement(By.XPath(PageElements.ROIRequesterPortal.extRoiRequesterPortal_xpath));
            extRoiRequesterPortal.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            var facilityCodeInput = Driver.Instance.FindElement(By.Id(PageElements.FacilityLoginPage.facilityCode_Id));
            facilityCodeInput.SendKeys("test");
            var okButton = Driver.Instance.FindElement(By.Id(PageElements.FacilityLoginPage.facilityOkButton));
            okButton.Click();
        }

        public static void GoToROIInternalRequesterPortal()
        {
            Driver.Instance.Navigate().GoToUrl(Driver.BaseAddress);
            Driver.Wait(TimeSpan.FromSeconds(2));
            var intRoiRequesterPortal = Driver.Instance.FindElement(By.XPath(PageElements.ROIRequesterPortal.intRoiRequesterPortal_xpath));
            intRoiRequesterPortal.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            var facilityCodeInput = Driver.Instance.FindElement(By.Id(PageElements.FacilityLoginPage.facilityCode_Id));
			//facilityCodeInput.SendKeys("test");
			facilityCodeInput.SendKeys("mroartf");
            var okButton = Driver.Instance.FindElement(By.Id(PageElements.FacilityLoginPage.facilityOkButton));
            okButton.Click();
        }

        public static void GoToROIFaclityLoginPage(IWebDriver bDriver)
        {
            DebugUtil.DebugMessage("ROI Facility Login page");
            var facilityLink = bDriver.FindElement(By.XPath(PageElements.FacilityLoginPage.facilityLink_Xpath));
            facilityLink.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

            var facilityCodeInput = bDriver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityCode_Id));
			//facilityCodeInput.SendKeys("test");
			facilityCodeInput.SendKeys("mroartf");

			var okButton = bDriver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityOkButton));
            okButton.Click();
        }

        //Added new method to log in to Iron Mountain.

        public static void GoToIronMountainROIFaclityLoginPage()
        {
            DebugUtil.DebugMessage("ROI Facility Login page");
            var facilityLink = Driver.Instance.FindElement(By.XPath(PageElements.FacilityLoginPage.facilityLink_Xpath));
            facilityLink.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

            var facilityCodeInput = Driver.Instance.FindElement(By.Id(PageElements.FacilityLoginPage.facilityCode_Id));
            facilityCodeInput.SendKeys("irmtest");

            var okButton = Driver.Instance.FindElement(By.Id(PageElements.FacilityLoginPage.facilityOkButton));
            okButton.Click();
        }

        public static void GoToROIAdminLoginPage(IWebDriver bDriver)
        {
            
            string baseAddress = "https://staging.mrocorp.com/net4.0/Login/dev/login/LoginMenu.aspx";


           bDriver.Navigate().GoToUrl(baseAddress);         
            Driver.Wait(TimeSpan.FromSeconds(2));

            var roiadminlink = bDriver.FindElement(By.XPath(PageElements.ROIAdminLoginPage.roiAdmin_xpath));
            //var roiadminlink = Driver.Instance.FindElement(By.XPath(PageElements.ROIAdminLoginPage.roiAdmin_xpath));
           // var roiadminlink = Driver.Instance.FindElement(By.XPath("//td[@id ='mroheader_Mropagehead1_Mromenus1_Mrotopmenu1_mnuMainMenu-menuItem006' and contains(text(),'ROI Admin')]"));
           
            roiadminlink.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
        }

        public static void GoToROIRequestorPortalLoginPage()
        {
            //TODO: Implement navigation to requestor portal
        }

        public static void GoToMROAdminLoginPage()
        {
            //TODO: Implement navigation to MRO Admin
        }

        public static void GoToROITrackerLoginPage()
        {
            //TODO: Implement navigation to ROI Tracker.
        }

        public static void GoToChartOnlineLoginPage()
        {
            //TODO: Implement navigation to Chart Online
        }

        public static void GoToFilingCabinetOnlinePage()
        {
            //TODO: Implement navigation to Filing Cabinet Online
        }

        public static void GoToParentBusinessAdminPage()
        {
            //TODO: Implement navigation to Parent Business Admin
        }

        public static LoginCommand LoginAs(string userName)
        {
            return new LoginCommand(userName);
        }

        public static void LogOut()
        {
            var LogOutBtn = Driver.Instance.FindElement(By.XPath(PageElements.FacilityLoginPage.facilityLogOutButton_Xpath));
            LogOutBtn.Click();

        }
    }

    public class LoginCommand
    {
        private readonly string userName;
        private string password;

        public LoginCommand(string userName)
        {
            this.userName = userName;
        }

        public LoginCommand WithPassword(string password)
        {
            this.password = password;
            return this;
        }

        public void Login(IWebDriver bDriver)
        {
            var userNameInput = bDriver.FindElement(By.Id("mrocontent_txtUserName"));
            userNameInput.Clear();
            userNameInput.SendKeys(userName);


            var passwordInput = bDriver.FindElement(By.Id("mrocontent_txtPassword"));
            passwordInput.SendKeys("");
            passwordInput.Clear();
            passwordInput.SendKeys(password);

            WebElementHelper.Click_Action("mrocontent_cmdLogin", FindElementBy.Id,bDriver);

            //var loginButton = Driver.Instance.FindElement(By.Id("mrocontent_cmdLogin"));
            //loginButton.Click();

            Driver.Wait(TimeSpan.FromSeconds(2));
        }

    }
}
