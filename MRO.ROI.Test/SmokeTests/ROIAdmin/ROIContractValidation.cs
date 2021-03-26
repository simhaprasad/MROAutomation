using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using MRO.ROI.Automation;


namespace MRO.ROI.Test.SmokeTests.ROIAdmin
{
    [TestClass]
    public class ROIContractValidation 
    {
       

        [TestMethod]
        [TestCategory(ROITestCategory.BuildVerification), TestCategory(ROITestCategory.Regression)]
        public void ROI_Contract_Validation_improvements()
        {
            TestBase baseClass = new TestBase();
            try
            {               
                baseClass.Init("ROIAdmin");

              //  Driver.logger = Driver.extent.CreateTest("Contract Validation improvements");
                //click on contract list
                MenuSelector.SelectRoiAdmin("Facilities", "Contract List",TestBase.BaseWebDriver.Value); 
                Driver.Wait(TimeSpan.FromSeconds(5));
                string headerText =TestBase.BaseWebDriver.Value.FindElement(By.XPath("//td[@id ='MasterHeaderText']")).Text;
                Assert.AreEqual(headerText, "Contract List");
                //Driver.logger.Log(Status.Pass, "Contract list Page loads Successfully");
                //click on Facillity dropdown
                SelectElement oSelect = new SelectElement(TestBase.BaseWebDriver.Value.FindElement(By.XPath("//select[@id='mrocontent_lstFac']")));
                oSelect.SelectByText("MRO eXpress TEST");
                Driver.Wait(TimeSpan.FromSeconds(5));
                //click on verify all contact button
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_cmdVerifyAll']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                string headingElement =TestBase.BaseWebDriver.Value.FindElement(By.XPath("//td[@id='MasterHeaderText']")).Text;
                Assert.AreEqual(headingElement, "Verify Active Contracts");
                //click on Return to contract list
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_btnReturn']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                //click on add contract button
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_cmdAdd']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Random random = new Random();
                int ID = random.Next(10, 2000);
                //adding first contract
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_txtName']")).SendKeys("TestCigniti"+ID.ToString());
                //calendar selection
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_tblContractProperties']/tbody/tr[3]/td[2]/a/img")).Click();
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='tblPopUpCalendar']/tbody/tr[3]/td[2]/a")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_tblContractProperties']/tbody/tr[3]/td[4]/a/img")).Click();
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='tblPopUpCalendar']/tbody/tr[7]/td[3]/a")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                //click on save changes button
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_cmdSave1']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                TestBase.takeScreenShot();
               // Driver.takeScreenShot();
                //Driver.logger.Log(Status.Pass, " First Contract Created Successfully");
                //click on Return to contract list
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_cmdReturn1']")).Click();
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_cmdAdd']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Random random1 = new Random();
                int ID1 = random.Next(10, 2000);
                //adding second contract
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_txtName']")).SendKeys("TestCigniti" + ID1.ToString());
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_tblContractProperties']/tbody/tr[3]/td[2]/a/img")).Click();
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='tblPopUpCalendar']/tbody/tr[3]/td[2]/a")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_tblContractProperties']/tbody/tr[3]/td[4]/a/img")).Click();
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='tblPopUpCalendar']/tbody/tr[7]/td[3]/a")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_cmdSave1']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                string contract1 =TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_txtName']")).GetAttribute("value");
                //Driver.takeScreenShot();
               // Driver.logger.Log(Status.Pass, " Second Contract Created Successfully");
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_cmdReturn1']")).Click();
                //click on verify facilities contract button
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//input[@id='mrocontent_cmdVerify']")).Click();
               // Driver.logger.Log(Status.Pass, "Active facility contracts Page load  successfully");
                
                //click on last 6 months dropdown
                SelectElement oSelect1 = new SelectElement(TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_lstLastSixMonths']")));
                oSelect1.SelectByIndex(1);
               // Driver.logger.Log(Status.Pass, "Contarct list is displayed for the selected month");
                TestBase.takeScreenShot();
                Driver.Wait(TimeSpan.FromSeconds(2));
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_dgReport_tblReport_imgSortSwitch']")).Click();
                string contractname =TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[2]/td[6]")).Text;
                Assert.AreEqual(contract1, contractname);
               // Driver.logger.Log(Status.Pass, "Created contracts are added Successfully and Contracts are duplicate");
                TestBase.takeScreenShot();
                string infomsg =TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[2]/td[12]")).Text;
                Assert.AreEqual("Multiple contracts for location", infomsg);
               TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_dgReport_tblReport_imgSortSwitch']")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                SelectElement oSelect2 = new SelectElement(TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_lstLastSixMonths']")));
                oSelect2.SelectByIndex(2);
                string previousmonthContracts =TestBase.BaseWebDriver.Value.FindElement(By.XPath("//*[@id='mrocontent_tblReport']/tbody/tr/td")).Text;
                Assert.AreEqual("No results found for this table!", previousmonthContracts);
                //Driver.logger.Log(Status.Pass, "No Contracts are available for selected month");
                TestBase.takeScreenShot();
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

