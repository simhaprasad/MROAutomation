using AutoIt;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using System;

namespace MRO.ROI.Automation.Pages.ROIFacility
{
    public static class FacilityRequestStatusPage
    {
        public static bool IsAtRequestStatusPage
        {
            get
            {
                //TODO: Logic to confirm.
                return true;
            }
        }

        public static bool IsRequestReleased
        {
            get
            {
                //TODO: Implement logic for request release confirmation
                return true;
            }
        }

        public static bool IsDocumentsDelivered
        {
            get
            {
                //TODO: Implement logic for request release confirmation
                return true;
            }
        }

        public static object Debug { get; private set; }

        public static void ScanPatientPagesupperhalf(string patientFirstName = null, string patientLastName = null)
        {
            if (!string.IsNullOrEmpty(patientFirstName) && !string.IsNullOrEmpty(patientLastName))
            {
                var win = AutoItX.WinWaitActive($"WebScan: Scan Medical Records for Release for {patientLastName}, {patientFirstName}", "", 10);
                AutoItX.WinActivate($"WebScan: Scan Medical Records for Release for {patientLastName}, {patientFirstName}", "");
            }

            WebElementHelper.ScrollIntoView("mrocontent_cmdScan_", FindElementBy.Id);
           // WebElementHelper.Click_Action(PageElements.FacilityRequestStatusPage.scanButton_Id, FindElementBy.Id);

        }

        public static void ScanPatientPages(string patientFirstName = null, string patientLastName = null, int nPatientPages = 2)
        {
            if (!string.IsNullOrEmpty(patientFirstName) && !string.IsNullOrEmpty(patientLastName))
            {
                var win = AutoItX.WinWaitActive($"WebScan: Scan Medical Records for Release for {patientLastName}, {patientFirstName}", "", 10);
                AutoItX.WinActivate($"WebScan: Scan Medical Records for Release for {patientLastName}, {patientFirstName}", "");
            }
            WebElementHelper.ScrollIntoView("mrocontent_cmdScan_", FindElementBy.Id);
           // WebElementHelper.Click_Action(PageElements.FacilityRequestStatusPage.scanButton_Id, FindElementBy.Id);

            ScannerUtil.ScanDocuments(nPatientPages);
            Driver.Wait(TimeSpan.FromSeconds(5));
            WebElementHelper.Click_Enter();
            Driver.Wait(TimeSpan.FromSeconds(5));
            //  WebElementHelper.Click_Enter();
            //  Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.logger.Pass("Successfully Scanned Patient Pages");

        }

        public static void ReleaseRequest()
        {
            Driver.Instance.SwitchTo().Frame("radWndPrompt");
            Driver.Instance.FindElement(By.Id("rbYes")).Click();
            //Driver.Wait(TimeSpan.FromSeconds(10)); //helga script
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.btnRelease_Id)).Click();
            //Driver.Wait(TimeSpan.FromSeconds(2));
            WebElementHelper.Click_Enter();
            //Driver.Wait(TimeSpan.FromSeconds(2));
            DebugUtil.DebugMessage("Request released");
            Driver.Wait(TimeSpan.FromSeconds(10)); //CHANGES FOR HELGA SCRIPT DELTE THIS LINE LATER
        }

        public static void ReleaseRequest1()
        {
            // this method created for only MultiplePagaesScanPages script.
            // btnRelease_Id not working
            Driver.Instance.SwitchTo().Frame("radWndPrompt");
            Driver.Instance.FindElement(By.Id("rbYes")).Click();
            Driver.Wait(TimeSpan.FromSeconds(10)); //helga script
                                                   // Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.btnRelease_Id)).Click();
            WebElementHelper.Click_Enter();
            Driver.Wait(TimeSpan.FromSeconds(5));
            WebElementHelper.Click_Enter();
            Driver.Wait(TimeSpan.FromSeconds(15));
            DebugUtil.DebugMessage("Request released");
            Driver.Wait(TimeSpan.FromSeconds(10)); //CHANGES FOR HELGA SCRIPT DELTE THIS LINE LATER
        }

        public static void DeliverMedicalRecordOnsite()

        {
            //  Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.deliverfaxosdnow_id)).Click();
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.requesterFax_Id)).Click();
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.requesterFax_Id)).SendKeys("555-555-5555");
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.storedOnPaper_Id)).SendKeys("2");
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.storedElectronically_Id)).SendKeys("2");
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.createInvoiceButton_Id)).Click();

            string balanceDue = Driver.Instance.FindElement(By.XPath(PageElements.FacilityRequestStatusPage.balanceDue_Xpath)).Text;
            Driver.Wait(TimeSpan.FromSeconds(5));

            Driver.Instance.FindElement(By.XPath(PageElements.FacilityRequestStatusPage.cashCheck_Xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));

            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.amount_Id)).SendKeys(balanceDue);
            Driver.Instance.FindElement(By.XPath(PageElements.FacilityRequestStatusPage.amountUncheck_Xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.organizationtext_id)).SendKeys("Dr.MROTest");
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.XPath(PageElements.FacilityRequestStatusPage.sendFax_Xpath)).Click();
            Driver.logger.Pass("Successfully Send The Fax");
            //  Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.Wait(TimeSpan.FromSeconds(2));
            WebElementHelper.Click_Enter();
        }


        public static void DeliverMedicalRecordOnsite1()

        {
            //  Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.deliverfaxosdnow_id)).Click();
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.organizationtext_id)).SendKeys("Dr.MROTest");
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.requesterFax_Id)).Click();
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.requesterFax_Id)).SendKeys("555-555-5555");
            Driver.Instance.FindElement(By.Id("mrocontent_rbChargeByCD")).Click();
            Driver.Instance.FindElement(By.Id("mrocontent_txtBxNumCDs")).SendKeys("1");
            Driver.Instance.FindElement(By.Id("mrocontent_txtBxCDCost")).SendKeys("10.00");
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.createInvoiceButton_Id)).Click();

            string balanceDue = Driver.Instance.FindElement(By.XPath(PageElements.FacilityRequestStatusPage.balanceDue_Xpath)).Text;
            Driver.Wait(TimeSpan.FromSeconds(5));

            Driver.Instance.FindElement(By.Id("mrocontent_rbCreditCard")).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));

            Driver.Instance.FindElement(By.Id("mrocontent_custCCPayment_txtBxPayment")).SendKeys(balanceDue);
            //Driver.Instance.FindElement(By.XPath(PageElements.FacilityRequestStatusPage.amountUncheck_Xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id("mrocontent_custCCPayment_btnEnterCCInfo")).Click();


            //Driver.Instance.FindElement(By.XPath(PageElements.FacilityRequestStatusPage.sendFax_Xpath)).Click();
            //Driver.logger.Pass("Successfully Send The Fax");
            //  Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.Wait(TimeSpan.FromSeconds(2));
            //   WebElementHelper.Click_Enter();
        }

        public static void IronMountainDeliverMedicalRecordOnsite()

        {
            //  Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.deliverfaxosdnow_id)).Click();
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.requesterFax_Id)).Click();
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.requesterFax_Id)).SendKeys("555-555-5555");
            //   Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.storedOnPaper_Id)).SendKeys("2");
            //    Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.storedElectronically_Id)).SendKeys("2");
            Driver.Instance.FindElement(By.Id("mrocontent_rbChargeByCD")).Click();
            Driver.Wait(TimeSpan.FromSeconds(3));
            Driver.Instance.FindElement(By.Id("mrocontent_txtBxNumCDs")).SendKeys("1");
            Driver.Instance.FindElement(By.Id("mrocontent_txtBxCDCost")).SendKeys("10");

            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.createInvoiceButton_Id)).Click();

            string balanceDue = Driver.Instance.FindElement(By.XPath(PageElements.FacilityRequestStatusPage.balanceDue_Xpath)).Text;
            Driver.Wait(TimeSpan.FromSeconds(5));

            Driver.Instance.FindElement(By.XPath(PageElements.FacilityRequestStatusPage.cashCheck_Xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));

            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.amount_Id)).SendKeys(balanceDue);
            Driver.Instance.FindElement(By.XPath(PageElements.FacilityRequestStatusPage.amountUncheck_Xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id("mrocontent_cmdDeliver")).Click();

            // Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.organizationtext_id)).SendKeys("Dr.MROTest");
            Driver.Wait(TimeSpan.FromSeconds(5));
            //   Driver.Instance.FindElement(By.XPath(PageElements.FacilityRequestStatusPage.sendFax_Xpath)).Click();
            Driver.logger.Pass("Successfully Submitted Delivery Records.");
            //  Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.Wait(TimeSpan.FromSeconds(2));
            WebElementHelper.Click_Enter();
        }

        public static void ReqPreAuthorizationBtn()
        {
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.estpapstored_id)).SendKeys("10");
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.estelcstored_id)).SendKeys("10");
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.reqpreauthorizationbtn_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            //    string actmessage = Driver.Instance.FindElement(By.Id(PageElements.FacilityRequestStatusPage.actionmessage_id)).Text;
            //   Console.WriteLine(actmessage);
            Driver.Wait(TimeSpan.FromSeconds(5));

        }
        public static void SendEnterKey()
        {
            //  Debug.WriteLine("Accept the scan");
            Driver.Wait(TimeSpan.FromSeconds(10));
            WindowsInput.InputSimulator simulator = new InputSimulator();
            simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            //  Driver.Wait(TimeSpan.FromSeconds(5));
            //       VK_ENTER);
            //    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);

            Driver.Wait(TimeSpan.FromSeconds(15));

            //      simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A);

        }
        public static void acceptalert1()
        {
            Driver.Instance.SwitchTo().Alert().Accept();
        }

		public static void closeRadNotification()
		{
			try
			{
				string mainWindowHandle = Driver.Instance.CurrentWindowHandle;
				Driver.Instance.SwitchTo().Frame("radWndPrompt");
				Driver.Instance.FindElement(By.Id("btnNo")).Click();
				Driver.Instance.SwitchTo().Window(mainWindowHandle);
			}
			catch
			{
				//Do nothing
			}
		}

    }

    internal class InputSimulator : WindowsInput.InputSimulator
    {
    }
}
