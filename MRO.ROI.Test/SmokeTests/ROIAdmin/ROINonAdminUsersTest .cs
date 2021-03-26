using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

using MRO.ROI.Automation;

namespace MRO.ROI.Test.SmokeTests.ROIAdmin
{
    [TestClass]
    public class ROINonAdminUsersTest
    {
             

        [TestMethod]
        [TestCategory(ROITestCategory.BuildVerification), TestCategory(ROITestCategory.Regression)]
        public void ROI_Permissions_NonAdminUsers()
        {
            TestBase baseClass = new TestBase();
            try
            {               
                baseClass.Init("ROIAdmin");
                //Driver.logger = Driver.extent.CreateTest("Permissions to Create Non-Admin Users");
                string headerText = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//td[@id ='MasterHeaderText']")).Text;
                Assert.AreEqual(headerText, "Find a Request L2 RS");
                //Click on Facility list
                MenuSelector.SelectRoiAdmin("Facilities", "Facility List",TestBase.BaseWebDriver.Value);               
                Driver.Wait(TimeSpan.FromSeconds(5));
                ROIAdminFacalitiesListPage.gotoMROARTestFacility(TestBase.BaseWebDriver.Value);
                //click on users menu
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//td[@id ='mroheader_MROPageHead1_ctl01_ctl00_mnuMainMenu-menuItem014']")).Click();
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//td[contains(text(), 'List All Users')]")).Click();
                //Edit user info
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_txtLogin']")).SendKeys("akothuri");

                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_cmdRefresh']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                ////Driver.logger.Log(Status.Info, "Login in userid." + "akothuri");
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_dgUsers']/tbody/tr[2]/td[3]/a")).Click();
                //Driver.logger.Log(Status.Info, "Edit user info page load successfully.");
                bool result = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_bUserAdmin']")).Selected;
                if (result == true)
                {
                    TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_bUserAdmin']")).Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));

                }
                //Driver.logger.Log(Status.Info, "Can Administer users checkbox unchecked successfully");
                TestBase.takeScreenShot();
                //click on save changes button
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_cmdUpdate']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                string statusMessage = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//span[@id='mrocontent_lblUserUpdated']")).Text;
                Assert.AreEqual("User Updated!", statusMessage);
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_cmdCancel']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                string appUrl = TestBase.GetWebAppURL();
                IJavaScriptExecutor js = (IJavaScriptExecutor)TestBase.BaseWebDriver.Value;
                ((IJavaScriptExecutor)js).ExecuteScript("window.open('" + appUrl + "');");
                string tab1 = TestBase.BaseWebDriver.Value.WindowHandles[0];
                string tab2 = TestBase.BaseWebDriver.Value.WindowHandles[1];
                TestBase.BaseWebDriver.Value.SwitchTo().Window(tab2);
                LoginPage.GoToROIFaclityLoginPage(TestBase.BaseWebDriver.Value);
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_txtUserName']")).SendKeys("akothuri");
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_txtPassword']")).SendKeys("TestingCigniti");
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_cmdLogin']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));

                TestBase.IsElementPresent("(//td[contains(text(),'Users')])[1]",60000);
                
                //Driver.logger.Log(Status.Pass, "Users Menu Option is Not Visible");
                TestBase.takeScreenShot();
                //Driver.Wait(TimeSpan.FromSeconds(2));
                TestBase.BaseWebDriver.Value.SwitchTo().Window(tab1);
                Driver.Wait(TimeSpan.FromSeconds(2));
                TestBase.BaseWebDriver.Value.Navigate().GoToUrl(appUrl);
                Driver.Wait(TimeSpan.FromSeconds(2));
                LoginPage.GoToROIAdminLoginPage(TestBase.BaseWebDriver.Value);
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_txtUserName']")).SendKeys("cigniti-akothuri");
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_txtPassword']")).SendKeys("TestingMRO@123");
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_cmdLogin']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                MenuSelector.SelectRoiAdmin("Facilities", "Facility List", TestBase.BaseWebDriver.Value);

                Driver.Wait(TimeSpan.FromSeconds(5));
                ROIAdminFacalitiesListPage.gotoMROARTestFacility(TestBase.BaseWebDriver.Value);
                //click on users menu
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//td[@id ='mroheader_MROPageHead1_ctl01_ctl00_mnuMainMenu-menuItem014']")).Click();
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//td[contains(text(), 'List All Users')]")).Click();
                //Edit user info
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_txtLogin']")).SendKeys("akothuri");

                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_cmdRefresh']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                //Driver.logger.Log(Status.Info, "Login in userid." + "akothuri");
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_dgUsers']/tbody/tr[2]/td[3]/a")).Click();
                //Driver.logger.Log(Status.Info, "Edit user info page load successfully.");
                bool result1 = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_bUserAdmin']")).Selected;
                if (result == false)
                {
                    TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_bUserAdmin']")).Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));

                }
                //Driver.logger.Log(Status.Info, "Can Administer users checkbox checked successfully");
                TestBase.takeScreenShot();
                //click on save changes button
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_cmdUpdate']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));

                TestBase.BaseWebDriver.Value.SwitchTo().Window(tab2);
                TestBase.BaseWebDriver.Value.Navigate().Refresh();
                //Verification for users menu               
                TestBase.IsElementPresent("(//td[contains(text(),'Users')])[1]", 60000);
                //Driver.logger.Log(Status.Pass, "Users Menu Option is Not Visible");
                TestBase.takeScreenShot();
                baseClass.Dispose();
            }
            catch (Exception ex)
            {
                ////Driver.logger.Log(Status.Fail, "Test failed with exception"); //Logging fail
                ////Driver.logger.Log(Status.Error, MarkupHelper.CreateTable(
                //    new string[,]
                //    {
                //        {"Exception", ex.Message },
                //        {"StackTrace", ex.StackTrace }
                //    })); //Logging Error in a tabular format
                baseClass.Dispose();
                Assert.Fail(ex.Message);
            }
        }


    }

}

