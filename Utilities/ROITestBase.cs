using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using System;

namespace MRO.ROI.Test.Utilities
{
    [TestClass]
    public class ROITestBase
    {
        private readonly ROITestArea _testArea;
        public static ExtentReports extent;
        public static ExtentTest logger;
        public ROITestBase(ROITestArea testArea)
        {
            _testArea = testArea;
        }

        [TestInitialize]
        public void Init()
        {
            Driver.Initialize(DriverType.IE, GetRequestedTestCategory());
            extent = new ExtentReports();
            extent.AttachReporter(GetHtmlReporter());

            LoginMenuPage.GoToLoginMenuPage();

            switch (_testArea)
            {
                case ROITestArea.ROIFacility:
                    LoginPage.GoToROIFaclityLoginPage();
                    LoginPage.LoginAs("seleniumautomation").WithPassword("Testauto1$").Login();
                    break;
                case ROITestArea.ROIAdmin:
                    LoginPage.GoToROIAdminLoginPage();
                    LoginPage.LoginAs("seleniumautomation").WithPassword("Testauto1$").Login();
                    break;
                case ROITestArea.ROIExternalRequesterPortal:
                    LoginPage.GoToROIExternalRequesterPortal();
                    LoginPage.LoginAs("seleniumautomation").WithPassword("Testauto1$").Login();
                    break;
                case ROITestArea.ROIInternalRequesterPortal:
                    LoginPage.GoToROIInternalRequesterPortal();
                    LoginPage.LoginAs("int-seleniumautomation").WithPassword("Testauto1$").Login();
                    break;

                //TODO: Implement additional navigation and login pages.

                default:
                    break;
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Close();
            if (logger != null)
            {
                logger.Log(Status.Pass, "Web Driver closed successfully.");
                extent.Flush();
            }
        }

        private string GetRequestedTestCategory()
        {
            string[] arguments = Environment.GetCommandLineArgs();
            string testCategory = string.Empty;
            foreach (string arg in arguments)
            {
                if (arg.Contains("TestCategory"))
                {
                    var strArray = arg.Split('=');
                    if (strArray.Length == 2)
                    {
                        testCategory = strArray[0];
                    }
                }
            }
            return testCategory;
        }

        private static ExtentHtmlReporter GetHtmlReporter()
        {
            string createdDateTime = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-tt");
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath + "Reports\\" + createdDateTime + "\\";
            Console.Write(projectPath);


            string reportPath = projectPath + @"MyOwnReport.html";

            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.Config.Encoding = "UTF-8";
            htmlReporter.Config.Theme = Theme.Standard;
            htmlReporter.Config.DocumentTitle = "UI Test Automation Report";
            htmlReporter.Config.ReportName = "UI Test Automation";
            htmlReporter.Config.EnableTimeline = true;
            htmlReporter.Config.CSS = "canvas {height: 250px;}";

            return htmlReporter;
        }
    }
}
