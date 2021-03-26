using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;


namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminFacalitiesListPage
    {
		public static void gotoROITestFacility()
		{
            
			//Driver.Instance.FindElement(By.XPath(PageElements.ROIAdminList.computericon_xpath)).Click();
			Driver.Instance.FindElement(By.Id("mrocontent_cbAlphaFilter")).Click();
			//Driver.Wait(TimeSpan.FromSeconds(5));
			//WebElementHelper.Click_Action(PageElements.ROIAdminList.computericon_xpath, FindElementBy.Xpath);
			WebElementHelper.ScrollIntoView("//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Login|1';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]", FindElementBy.Xpath);
			//Driver.Wait(TimeSpan.FromSeconds(5));
			//WebElementHelper.Click_Action("//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Login|1';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]", FindElementBy.Xpath,test);
        }

        public static string getRequestid()
        {
            return Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.requestid_id)).Text;

        }
        public static void roilookupidadmin(string rqsID)
        {
			Driver.MROLogInfo("Start Admin Request Status for " + rqsID);
			//Driver.Wait(TimeSpan.FromSeconds(5));
			Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminlookupid_id)).Click();
            //Driver.Wait(TimeSpan.FromSeconds(8));

            Driver.Instance.SwitchTo().Alert().SendKeys(rqsID);
            Driver.Instance.SwitchTo().Alert().Accept();
			Driver.MROLogInfo("Complete Admin Request Status for " + rqsID);
		}
		//factylookupid_xpath

		public static void faclookupidadmin(string rqsID)
        {
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.XPath(PageElements.ROIAdminList.factylookupid_xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));

            Driver.Instance.SwitchTo().Alert().SendKeys(rqsID);
            Driver.Instance.SwitchTo().Alert().Accept();
        }

        public static string invoiceAmounts()
        {
            return Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.roiadminvamt_id)).Text;

        }

        public static string balanceDue(string balancedue)
        {
            return Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.roiadminbaldue_id)).Text;


        }

        public static string adjustedBalance()
        {
            return Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.roiadmadjbal_id)).Text;

        }
        public static void drilltofacility()
        {
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.drilltofacility_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id("mrocontent_txtRequestRcvdDate")).SendKeys(DateTime.Now.ToShortDateString());
            WebElementHelper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id("mrocontent_cmdLogRequest")).Click();
        }

        public static void roilookupidadmin(object requestID)
        {
            throw new NotImplementedException();
        }

        public static void adminaddissue()
        {
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminaddissue_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminissue_id)).SendKeys("Authorization Not Dated");
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.admincomments_id)).SendKeys("Testing Add issues");
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminaddissubtn_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminviewaction_id)).Click();
            SelectElement oSelect = new SelectElement(Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminmroaction_id)));
            // oSelect.SelectByText("Patient"); Provider
            oSelect.SelectByText("WRONG PATIENT DOCUMENTS");
            Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminmronotes_id)).SendKeys("Testing MOR Notes");
            Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminaddactionbtn_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(3));
            Driver.Instance.FindElement(By.XPath(PageElements.ROIAdminList.adminrequestactionclose_xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            //Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.roidlogoutbtn_id)).Click();
        }

        public static void linkreqeusted()
        {
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id("mrocontent_cmdLinkRequest")).Click();
        }


        public static void gotoMROARTestFacility(IWebDriver bDriver)
        {
            bool result = bDriver.FindElement(By.Id("mrocontent_cbAlphaFilter")).Selected;
            if (result == false)
            {
                bDriver.FindElement(By.Id("mrocontent_cbAlphaFilter")).Click();
            }
            bDriver.FindElement(By.XPath("//*[@id='mrocontent_alphaFilter_lnkM']")).Click();
            bDriver.FindElement(By.XPath("//*[@id='mrocontent_tblFacilities']/tbody/tr[1]/td[3]/a[4]")).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            TestBase.ScrollIntoView("//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Login|537';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]", FindElementBy.Xpath);
                       
            Driver.Wait(TimeSpan.FromSeconds(5));
            WebElementHelper.Click_Action("//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Login|537';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]", FindElementBy.Xpath,bDriver);
        }

  
    
    }
}
