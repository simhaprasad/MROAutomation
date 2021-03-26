using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using MRO.ROI.Automation;

namespace MRO.ROI.Test.SmokeTests.ROIAdmin
{
    [TestClass]
    public class ROIEmployeeStatusTest
    {
        //public ROIEmployeeStatusTest() : base(ROITestArea.ROIAdmin)
        //{

        //}

        [TestMethod]       
        public void ROI_Employee_Status()
        {
            try
            {
                using (var testInst = new TestBase())
                {
                    testInst.Init("ROIAdmin");

                }

                Driver.logger = Driver.extent.CreateTest("MRO ROI Employee Status Test");//step1
                                                                                         //base.Init();
                //TestBaseClass baseClass = new TestBaseClass();
                //baseClass.Init("ROIAdmin");

                string headerText = Driver.Instance.FindElement(By.XPath("//td[@id ='MasterHeaderText']")).Text;

                Assert.AreEqual(headerText, "Find a Request L2 RS");//step1 validation 

                MenuSelector.SelectRoiAdmin("Facilities", "Facility List",TestBase.BaseWebDriver.Value);//step2

                ROIAdminFacalitiesListPage.gotoROITestFacility();//step3

                Driver.Instance.FindElement(By.XPath("//td[@id ='mroheader_MROPageHead1_ctl01_ctl00_mnuMainMenu-menuItem012']")).Click();
                Driver.Instance.FindElement(By.XPath("//td[contains(text(), 'List All Users')]")).Click();//step4
                //step5
                Driver.Instance.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys("test");
                Driver.logger.Log(Status.Info, "First Name entered." + "test");
                Driver.Instance.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys("facility");
                Driver.logger.Log(Status.Info, "Last Name entered." + "facility");
                //step6

                Driver.Instance.FindElement(By.XPath("//a[text()='Facility, test']")).Click();
                bool isElementNotExists = false;
               
                Driver.Instance.FindElement(By.XPath("//input[@id ='mrocontent_bMROEmployee']")).Click();

                if (!WebElementHelper.IsElementPresent("//select[@id ='mrocontent_lstROIFacilityUserType']"))
                {
                    isElementNotExists = true;
                }

                //Assert.AreEqual(true, isElementNotExists);//step7 validation

               // Driver.Instance.FindElement(By.XPath("//input[@id ='mrocontent_bMROEmployee']")).Click();
                SelectElement oSelect = new SelectElement(Driver.Instance.FindElement(By.Id("mrocontent_lstROIFacilityUserType")));
                oSelect.SelectByText("Remote Services");

                SelectElement oSelect1 = new SelectElement(Driver.Instance.FindElement(By.Id("mrocontent_lstROIFacilityUserRemoteStatus")));
                oSelect1.SelectByText("");

               

                Driver.Instance.FindElement(By.XPath("//input[@id ='mrocontent_cmdUpdate']")).Click();//step7
                Driver.Wait(TimeSpan.FromSeconds(5));

                var wait = new WebDriverWait(Driver.Instance, new TimeSpan(0, 0, 55));
                IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

                //Store the alert text in a variable
                string text = alert.Text;
                Assert.AreEqual("Remote Status is a required field for this User Type!", text);
                //Press the OK button
                alert.Accept();

                SelectElement oSelect3 = new SelectElement(Driver.Instance.FindElement(By.Id("mrocontent_lstROIFacilityUserType")));//step8
                oSelect3.SelectByText("Floater");


                SelectElement oSelect4 = new SelectElement(Driver.Instance.FindElement(By.Id("mrocontent_lstROIFacilityUserRemoteStatus")));
                oSelect4.SelectByText("Remote Full-Time");

                Driver.Instance.FindElement(By.XPath("//input[@id ='mrocontent_cmdUpdate']")).Click();

                string statusMessage = Driver.Instance.FindElement(By.XPath("//span[@id = 'mrocontent_lblUserUpdated']")).Text;
                Assert.AreEqual("User Updated!", statusMessage);//step8 validation 


                SelectElement oSelect5 = new SelectElement(Driver.Instance.FindElement(By.Id("mrocontent_lstROIFacilityUserType")));
                oSelect5.SelectByText("Implementation");

                SelectElement oSelect6 = new SelectElement(Driver.Instance.FindElement(By.Id("mrocontent_lstROIFacilityUserRemoteStatus")));
                oSelect6.SelectByText("");

                string statusMessage1 = Driver.Instance.FindElement(By.XPath("//span[@id = 'mrocontent_lblUserUpdated']")).Text;
                Assert.AreEqual("User Updated!", statusMessage1);//step9 validation


                Driver.Instance.FindElement(By.XPath("//input[@id ='mrocontent_bMROEmployee']")).Click();//step10

                

                Driver.Instance.FindElement(By.XPath("//input[@id ='mrocontent_cmdUpdate']")).Click();

                if (!WebElementHelper.IsElementPresent("//select[@id ='mrocontent_lstROIFacilityUserType']"))
                {
                    isElementNotExists = true;//step10 validation
                }


                Driver.Instance.FindElement(By.XPath("//td[text()='Users']")).Click();
                Driver.Instance.FindElement(By.XPath("//td[contains(text(), 'List All Users')]")).Click();//step11

                string pageHeaderText = Driver.Instance.FindElement(By.XPath("//td[@id='MasterHeaderText']")).Text;
                Assert.AreEqual("User Listing", pageHeaderText);//step11 validation


                Driver.Instance.FindElement(By.XPath("//input[@id ='mrocontent_cmdAddUser']")).Click();//step12

                Driver.Instance.FindElement(By.XPath("//input[@id ='mrocontent_txtLogin']")).SendKeys("cignitestuser");////input[@id ='mrocontent_txtEmail']

                Driver.Instance.FindElement(By.XPath("//input[@id ='mrocontent_txtEmail']")).SendKeys("cignitiuser@testing.com");

                Driver.Instance.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys("cigniti");
                Driver.logger.Log(Status.Info, "First Name entered." + "cigniti");
                Driver.Instance.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys("user");
                Driver.logger.Log(Status.Info, "Last Name entered." + "user");

                Driver.Instance.FindElement(By.XPath("//input[@id ='mrocontent_bMROEmployee']")).Click();
                SelectElement oSelect7 = new SelectElement(Driver.Instance.FindElement(By.Id("mrocontent_lstROIFacilityUserType")));
                oSelect7.SelectByText("Field Staff");

                SelectElement oSelect8 = new SelectElement(Driver.Instance.FindElement(By.Id("mrocontent_lstROIFacilityUserRemoteStatus")));
                oSelect8.SelectByText("");

                Driver.Instance.FindElement(By.XPath("//input[@id ='mrocontent_cmdUpdate']")).Click();//step7
                Driver.Wait(TimeSpan.FromSeconds(5));

            
                IAlert alert1 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

                //Store the alert text in a variable
                string text1 = alert1.Text;                
                //Press the OK button
                alert.Accept();
                Assert.AreEqual("Remote Status is a required field for this User Type!", text1);


                // step13               

                //SelectElement oSelect10 = new SelectElement(Driver.Instance.FindElement(By.Id("mrocontent_lstROIFacilityUserRemoteStatus")));
                //oSelect10.SelectByText("On Site Full-Time");

                //Driver.Instance.FindElement(By.XPath("//input[@id ='mrocontent_cmdUpdate']")).Click();//step7
                //Driver.Wait(TimeSpan.FromSeconds(5));
            }
            catch (Exception ex)
            {
                Driver.logger.Log(Status.Fail, "Test failed with exception"); //Logging fail
                Driver.logger.Log(Status.Error, MarkupHelper.CreateTable(
                    new string[,]
                    {
                        {"Exception", ex.Message },
                        {"StackTrace", ex.StackTrace }
                    })); //Logging Error in a tabular format
                Assert.Fail(ex.Message);
            }
        }
    }

}

