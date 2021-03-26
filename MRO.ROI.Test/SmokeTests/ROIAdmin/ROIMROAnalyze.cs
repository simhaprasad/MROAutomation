using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Test.SmokeTests.ROIAdmin
{
    [TestClass]
    public class ROIMROAnalyze 
    {       

        [TestMethod]
        [TestCategory(ROITestCategory.BuildVerification), TestCategory(ROITestCategory.Regression)]
        public void ROI_MROAnalyze_TurnAround_Report()
        {
            TestBase baseClass = new TestBase();
            try
            {
                baseClass.Init("ROIAdmin");
                Driver.logger = Driver.extent.CreateTest("ROI MROAnaylze TurnAround Report");
                MenuSelector.SelectRoiAdmin("Facilities", "Facility List", TestBase.BaseWebDriver.Value);
                //ROIAdminFacalitiesListPage.gotoROITestFacility();
                ROIAdminFacalitiesListPage.gotoMROARTestFacility(TestBase.BaseWebDriver.Value);
                //Driver.logger.Log(Status.Info, "Logged as an ROITest Facility");
                Driver.Wait(TimeSpan.FromSeconds(5));
                TestBase.Select("MRO Analyze", "Turnaround Report");
                Driver.Wait(TimeSpan.FromSeconds(3));
                //Driver.logger.Log(Status.Info, "Turnaround Report page loaded successfully.");
                IWebElement frame = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//iframe[starts-with(@id,'rdFrame')]"));
                TestBase.BaseWebDriver.Value.SwitchTo().Frame(frame);
                IWebElement datePicker = TestBase.BaseWebDriver.Value.FindElement(By.XPath("(//span[@id='daterange'])[1]"));
                datePicker.Click();
                Driver.Wait(TimeSpan.FromSeconds(1));
                IWebElement datePickerSelectFromDate = TestBase.BaseWebDriver.Value.FindElement(By.XPath("((//table[@class='table-condensed'])[2]//tr//td[contains(text(),'1')])[2]"));
                datePickerSelectFromDate.Click();
                IWebElement datePickerSelectToDate = TestBase.BaseWebDriver.Value.FindElement(By.XPath("((//table[@class='table-condensed'])[1]//tr//td[contains(text(),'1')])[13]"));
                datePickerSelectToDate.Click();
                IWebElement btnApply = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//button[text()='Apply']"));
                btnApply.Click();
                var selectLocatin = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//select[@id='nLocationID']"));
                var selectLocations = new SelectElement(selectLocatin);
                selectLocations.SelectByText("Boston Proper");
                SelectElement selectLocation = new SelectElement(TestBase.BaseWebDriver.Value.FindElement(By.Id("nLocationID")));
                selectLocation.SelectByText("Boston Proper");
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='btn_submit']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                IWebElement turnAroundAllRequest = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//th[@id='colnDays_RcvdToShipped_minus_InvToPaid-TH']"));
                if (turnAroundAllRequest.Text.Contains("Received to Shipped") == true)
                {
                    //Driver.logger.Log(Status.Pass, "Received to shipped is visible under Turn Around all request");
                }

                IWebElement turnAroundPreBills = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//th[@id='colnDays_RcvdToShipped_minus_InvToPaid_Pre-TH']"));
                if (turnAroundPreBills.Text.Contains("Received to Shipped") == true)
                {
                    //Driver.logger.Log(Status.Pass, "Received to shipped is visible under Turn Around PreBills");
                }

                IWebElement turnAroundNonPreBills = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//th[@id='colnDays_RcvdToShipped_NonPre-TH']"));
                if (turnAroundNonPreBills.Text.Contains("Received to Shipped") == true)
                {
                    //Driver.logger.Log(Status.Pass, "Received to shipped is visible under Turn Around Non PreBills");
                }

                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//td[@id= 'colnDays_RcvdToShipped_minus_InvToPaid_Row1']//a")).Click();

                Driver.Wait(TimeSpan.FromSeconds(2));

                IWebElement frame1 = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//iframe[@title='sr_dtTurnaroundDrillDown_AllReqs']"));
                TestBase.BaseWebDriver.Value.SwitchTo().Frame(frame1);
                IWebElement turnAroundAllRequestPDF = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//span[@id= 'div_PDF_icon']"));
                if (turnAroundAllRequestPDF.Displayed == true)
                {
                    //Driver.logger.Log(Status.Pass, "PDF icon is displayed under Turn Around all request");
                }
                IWebElement turnAroundAllRequestExcel = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//span[@id= 'div_Excel_icon']"));
                if (turnAroundAllRequestExcel.Displayed == true)
                {
                    //Driver.logger.Log(Status.Pass, "Excel icon is displayed under Turn Around all request");
                }
                Driver.Wait(TimeSpan.FromSeconds(2));

                baseClass.Dispose();
                //TestBase.BaseWebDriver.Value.SwitchTo().Frame(frame);
                //TestBase.BaseWebDriver.Value.FindElement(By.XPath("//td[@id='cellAllReqs'][9]/a")).Click();
                //Driver.Wait(TimeSpan.FromSeconds(2));               s

                //TestBase.BaseWebDriver.Value.SwitchTo().Frame(frame1);                 
                //IWebElement turnAroundPreBillsPDF = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//span[@id= 'div_PDF_icon']"));
                //Assert.IsTrue(turnAroundPreBillsPDF.Displayed);
                //IWebElement turnAroundPreBillsExcel = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//span[@id= 'div_Excel_icon']"));
                //Assert.IsTrue(turnAroundPreBillsExcel.Displayed);                

                //TestBase.BaseWebDriver.Value.SwitchTo().Frame(frame);
                //LoginPage.LogOut();
                //Assert.IsTrue(LoginPage.IsAtLoginPage, "Failed to log out successfully.");
                //Driver.logger.Pass("Successfully logged out");


            }
            catch (Exception ex)
            {
                //Driver.logger.Log(Status.Fail, "Test failed with exception"); //Logging fail
                //Driver.logger.Log(Status.Error, MarkupHelper.CreateTable(
                //    new string[,]
                //    {
                //        {"Exception", ex.Message },
                //        {"StackTrace", ex.StackTrace }
                //    })); //Logging Error in a tabular format
                Assert.Fail(ex.Message);
            }
        }
    }
}
