using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;


namespace MRO.ROI.Automation.Selenium
{
    public class Driver
    {
        public static string createdDateTime = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-tt");
        public static ExtentReports extent = new ExtentReports();
        public static ExtentHtmlReporter extenthtml = GetHtmlReporter1();
        public static ExtentTest logger;
        public static string projectPath;
        public static string reportPath;
        public static IWebDriver Instance { get; set; }
        public static string TestCategory { get; set; }
        public static string BaseAddress { get; private set; }
        public static string DriverPath { get; private set; }

        public static void Initialize(DriverType driver, string baseAddress, string driverPath)
        {
            BaseAddress = baseAddress;
            DriverPath = driverPath;


            extent.AttachReporter(extenthtml);

            switch (driver)
            {
                case DriverType.Chrome:
                    Instance = new ChromeDriver(DriverPath);
                    break;
                case DriverType.IE:
                    var options = new InternetExplorerOptions
                    {
                        IgnoreZoomLevel = true,
                        EnableNativeEvents = true
                    };
                    Instance = new InternetExplorerDriver(DriverPath, options);

                    break;
                case DriverType.FireFox:
                    Instance = new FirefoxDriver(DriverPath);
                    break;
                default:
                    Instance = new InternetExplorerDriver(DriverPath);
                    break;
            }

          //  TurnOnWait();
        }

        public static object switchTo()
        {
            throw new NotImplementedException();
        }

        public static void Close()
        {
            Instance.Quit();
            extent.Flush();
        }
        public static IWebElement findElement(string xpath)
        {
            return Instance.FindElement(By.XPath(xpath));
        }
        public static int findElementsSize(string xpath)
        {
            return Instance.FindElements(By.XPath(xpath)).Count;
        }

        public static IList<IWebElement> findElements(string xpath)
        {
            return Instance.FindElements(By.XPath(xpath));
        }

        public static void Click(string xpath)
        {
            findElement(xpath).Click();
        }
        public static void Type(string xpath, string data)
        {
            findElement(xpath).SendKeys(data);
        }

        private static string GetDriverPath()
        {
            var outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var relativePath = @"..\..\Utilities";
            var driverPath = Path.GetFullPath(Path.Combine(outputDirectory, relativePath));

            return driverPath;
        }

        public static void Wait(TimeSpan timeSpan)
        {
            Thread.Sleep((int)(timeSpan.TotalSeconds * 1000));
        }

        public static void NoWait(Action action,IWebDriver bDriver)
        {
            TurnOffWait();
            action();
            TurnOnWait(bDriver);
        }

        public static void IncreaseOnWait()
        {
            Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(90);
        }

        public static void TurnOnWait(IWebDriver bDriver)
        {
            bDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);
        }

        public static void TurnOffWait()
        {
            Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        public static void MaximizeWindow()
        {
            Instance.Manage().Window.Maximize();        
        
        }

        private static ExtentHtmlReporter GetHtmlReporter1()
        {

            projectPath = @"C:\\ChonSite\\Reports\\" + createdDateTime + "\\";
            reportPath = @"C:\ChonSite\Reports\" + createdDateTime + @"\MyOwnReport.html";

            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.Config.Encoding = "UTF-8";
            htmlReporter.Config.Theme = Theme.Standard;
            htmlReporter.Config.DocumentTitle = "UI Test Automation Report";
            htmlReporter.Config.ReportName = "UI Test Automation";
            htmlReporter.Config.EnableTimeline = true;
            htmlReporter.Config.CSS = "canvas {height: 250px;}";

            return htmlReporter;
        }
        public static void takeScreenShot()
        {
            Screenshot ss = ((ITakesScreenshot)Driver.Instance).GetScreenshot();
            //  string date = DateTime.Now.ToString("hh - mm - ss - tt");
            string date = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-tt");
            ss.SaveAsFile(Driver.projectPath + "screenshot" + date + ".png", OpenQA.Selenium.ScreenshotImageFormat.Jpeg);
            //Driver.logger.AddScreenCaptureFromPath("./screenshot" + date + ".png");
            Driver.logger.Info("Snapshot below: " + Driver.logger.AddScreenCaptureFromPath("./screenshot" + date + ".png"));

        }
        internal static object SwitchTo()
        {
            throw new NotImplementedException();
        }

        public static void WaitUntilDOMLoaded(IWebDriver bDriver)
        {
            IncreaseOnWait();
            var wait = new WebDriverWait(Driver.Instance, new TimeSpan(0, 0, 55));
            wait.Until(x => Driver.Instance.Scripts().ExecuteScript("return document.readyState").Equals("complete"));
            TurnOnWait(bDriver);
        }

        public static void MROLog(Status status, string details)
        {
            Driver.logger.Log(status, details);
            Utility.DebugUtil.DebugMessage("MROLog: " + details);
        }

        public static void MROLogInfo(string details)
        {
            MROLog(Status.Info, details);
        }

        public static void MROSelectRoiAdmin(string topLevelMenuText, string subMenuText)
        {
            //Driver.WaitUntilDOMLoaded();
            Console.Write($"//td[contains(text(),'{topLevelMenuText}')]");
            var wait = new WebDriverWait(Driver.Instance, new TimeSpan(0, 0, 55));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//table[@smenuname='mnuROIAdmin']//td[contains(text(),'{topLevelMenuText}')]")));

            var topLevelMenu = Driver.Instance.FindElement(By.XPath($"//table[@smenuname='mnuROIAdmin']//td[contains(text(),'{topLevelMenuText}')]"));
            Actions action = new Actions(Driver.Instance);
            action.Click(topLevelMenu).Build().Perform();
            //action.Perform();
            //Driver.Wait(TimeSpan.FromSeconds(2));

            Driver.Instance.FindElement(By.XPath($"//td[contains(text(),'{subMenuText}')]")).Click();
        }

        public static void SetVersion(string sVersion)
        {
            MROSelectRoiAdmin("Profile", "QA Test");

            Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminQAVersion)).Clear();
            Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminQAVersion)).SendKeys(sVersion);

            Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminQAUpdateVersion)).Click();
        }


        public static void ExecuteValidateSP(string sSP, string sMessage, int nRequestID1, int nRequestID2)
        {
            Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminQATestSP)).Clear();
            Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminQATestSP)).SendKeys(sSP);
            Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminQATestExecute)).Click();

            string sResults = Driver.Instance.FindElement(By.Id(PageElements.ROIAdminList.adminQATestResults)).Text;
            Driver.MROLogInfo(sMessage + " Validate requests: " + nRequestID1 + " & " + nRequestID2);
            Driver.MROLogInfo(sMessage + " " + sSP + " Validate results: " + sResults);
            if (sResults != "GOOD")
            {
                Driver.MROLog(Status.Fail, sMessage + " " + sSP + " Validate failed with result: " + sResults); //Logging fail
                                                                                                                //Assert.Fail(sMessage + " " + sSP + " Validate failed with result: " + sResults);
            }
        }



    }

    public static class JSExecute
    {
        public static IJavaScriptExecutor Scripts(this IWebDriver driver)
        {
            return (IJavaScriptExecutor)driver;
        }
    }

    public enum DriverType
    {
        Chrome,
        FireFox,
        IE
    }

    public enum FindElementBy
    {
        Id,
        Xpath,
        Tagname
        //TODO Optional: Add required types
    }

    public static class ROITestCategory
    {
        public const string BuildVerification = "BVT";
        public const string Regression = "REGRESSION";
    }

    public enum ROITestArea
    {
        ChartOnline,
        FilingCabinetOnline,
        MROAdmin,
        ParentBusinessAdmin,
        PatientPortal,
        ROIAdmin,
        ROIFacility,
        IronMountainROIFacility,
        ROIExternalRequesterPortal,
        ROIInternalRequesterPortal,
        ROITracker,
        MRO_ROI_Rate_Test,
        Reg_Outstanding_SubpoenasTest
    }






}
