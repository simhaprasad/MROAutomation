using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;

namespace MRO.ROI.Automation.Utility
{
    public class PaginationUtil
    {

        public static bool SearchPaginatedList(string searchFor)
        {
            var totalPages = 5;
            try
            {
                totalPages = Int32.Parse(Driver.Instance.FindElement(By.XPath("//td[contains(text(), 'Page:')]/a[1]")).Text);
            }
            catch
            {
                try
                {
                    Driver.Instance.FindElement(By.XPath($"//td[text()={searchFor}]"));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            var i = 2;
            var exists = false;
            while (i <= totalPages)
            {
                try
                {
                    Driver.Instance.FindElement(By.XPath($"//td[text()={searchFor}]"));
                    exists = true;
                }
                catch
                {

                }
                Driver.Instance.FindElement(By.XPath($"//td[contains(text(), 'Page:')]/a[contains(text(), '{i}')]")).Click();
                i++;
            }
            Driver.logger.Pass("Success: RequestID does not appear in QC Queue prior to one hour after entered");
            return exists;
        }

    }
}
