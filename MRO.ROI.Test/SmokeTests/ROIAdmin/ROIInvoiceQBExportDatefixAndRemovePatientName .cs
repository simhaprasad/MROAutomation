using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MRO.ROI.Test.SmokeTests.ROIAdmin
{
    [TestClass]
    public class ROIInvoiceQBExportDatefixAndRemovePatientName
    {
        

        [TestMethod]
        [TestCategory(ROITestCategory.BuildVerification), TestCategory(ROITestCategory.Regression)]

        public void ROI_Invoice_QBExport()
        {
            TestBase baseClass = new TestBase();
            try
            {
                baseClass.Init("ROIAdmin");

                Driver.logger = Driver.extent.CreateTest("ROI Invoice QB Export Date fix And Remove Patient Name Test");
                string headerText = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//td[@id ='MasterHeaderText']")).Text;

                Assert.AreEqual(headerText, "Find a Request L2 RS");

                //click on finacial tab
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//td[@id='mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem006']")).Click();

                //click on Statements/Invoices
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//td[@id='mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem006-subMenu-menuItem007']")).Click();

                //click roi invoic
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//td[@id='mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem006-subMenu-menuItem007-subMenu-menuItem002']")).Click();

                //select Month from dropdown
                var MonthOption = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//select[@id='mrocontent_ddlMonth']"));
                var selectMonthElement = new SelectElement(MonthOption);

                selectMonthElement.SelectByText("May");
                //For data stability,delete month record

                Thread.Sleep(1000);
                TestBase.BaseWebDriver.Value.Navigate().Refresh();

                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_txtBxYear']")).SendKeys("2020");

               

                var FacilityOption = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//select[@id='mrocontent_ddlFacility']"));
                var selectFacilityElement = new SelectElement(FacilityOption);

                selectFacilityElement.SelectByText("Curahealth");

                //Select contract

                var ContractOption = TestBase.BaseWebDriver.Value.FindElement(By.XPath("//select[@id='mrocontent_ddlContract']"));
                var selectContractElement = new SelectElement(ContractOption);

                selectContractElement.SelectByText("883 - Curahealth");


                //Click on show invoice
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_btnShow']")).Click();

                //click on order statements button
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_btnDeferredStatement']")).Click();

                //click on return to ROI invoice
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//a[@id='mrocontent_lnkReturn']")).Click();

                //click on saved export
                TestBase.BaseWebDriver.Value.FindElement(By.XPath("//a[@id='mrocontent_lnkSavedExports']")).Click();

                Thread.Sleep(30000);
                //click on report link
                TestBase.BaseWebDriver.Value.FindElement(By.XPath(" //td[contains(text(),'ROIInvoice_2021Jan_20210322_221616.zip')]")).Click();
                //string filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"C:\Users\E006201\TestAutomation\MRO.AutomationTest.Solution";

                //unzipping the file


                UnzipFiles.UnzipFile();

                //Excel validation is pending

                baseClass.Dispose();
                


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
               
                baseClass.Dispose();
                Assert.Fail(ex.Message);

            }
        }
    }
}
