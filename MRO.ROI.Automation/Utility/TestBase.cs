using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using OpenQA.Selenium.Interactions;
using System.IO;
using System.Reflection;

namespace MRO.ROI.Automation
{

    [TestClass]

    public class TestBase : IDisposable
    {
        public static ThreadLocal<RemoteWebDriver> BaseWebDriver;
        public TestContext TestContext { get; set; }      


        public void Init(string testArea)
        {
            //string driverPath = @"C:\TestAutomation\MRO.AutomationTest.Solution\MRO.ROI.Test\Utilities";
            
            string driverPath = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..","..","Utilities\\chromedriver.exe"));

           BaseWebDriver = new ThreadLocal<RemoteWebDriver>(() =>
            {
                return new ChromeDriver(driverPath);
            });
         //   BaseWebDriver = new ChromeDriver(driverPath);
            string username = string.Empty;
            string password = string.Empty;
           // string webAppUrl = GetWebAppURL();
            //BaseWebDriver.Value.Navigate().GoToUrl(webAppUrl);

            // string driverPath = TestContext != null ? @"C:\TestAutomation\MRO.AutomationTest.Solution\MRO.ROI.Test\Utilities" : TestContext.Properties["driverPath"].ToString();            //Driver.Initialize(DriverType.IE, webAppUrl, driverPath);        
            // Driver.Initialize(DriverType.Chrome, webAppUrl, driverPath);

            Driver.Wait(TimeSpan.FromSeconds(2));
            BaseWebDriver.Value.Manage().Window.Maximize();

            //LoginMenuPage.GoToLoginMenuPage(BaseWebDriver,webAppUrl);
            switch (testArea)
            {
                case "ROIFacility":
                    GetCredentials("facility", out username, out password);
                    LoginPage.GoToROIFaclityLoginPage(BaseWebDriver.Value);
                    LoginPage.LoginAs(username).WithPassword(password).Login(BaseWebDriver.Value);
                    // Driver.WaitUntilDOMLoaded();
                    break;
                case "ROIAdmin":
                    GetCredentials("admin", out username, out password);
                    LoginPage.GoToROIAdminLoginPage(BaseWebDriver.Value);
                    LoginPage.LoginAs(username).WithPassword(password).Login(BaseWebDriver.Value);
                    //Driver.WaitUntilDOMLoaded(BaseWebDriver);
                    break;

                //TODO: Implement additional navigation and login pages.

                default:
                    break;
            }

        }


        public static string GetWebAppURL()
        {
            //return TestContext != null ?
            //    TestContext.Properties["webAppUrl"].ToString() :
            //    //@"https://hqqa01.mro.com/net4.0/Login/dev/login/LoginMenu.aspx";
            //    @"https://staging.mrocorp.com/net4.0/Login/dev/login/LoginMenu.aspx";
            string appurl =
                  @"https://staging.mrocorp.com/net4.0/Login/dev/login/LoginMenu.aspx";

            return appurl;
        }

        private void GetCredentials(string area, out string username, out string password)
        {
            if (TestContext != null)
            {
                // username = TestContext.Properties[area + "UserName"].ToString();
                // password = TestContext.Properties[area + "Password"].ToString();

                //username = TestContext.Properties[area + "cigniti-ssaligrama"].ToString();
                // password = TestContext.Properties[area + "cigniti@1359"].ToString();

                username = "cigniti-ssaligrama";
                password = "cigniti@1359";
            }
            else
            {
                switch (area)
                {
                    case "facility":
                        //username = "tmirza123";
                        //password = "Mihran2007$";
                        username = "akothuri";
                        password = "TestingCigniti";
                        break;
                    case "admin":
                        //username = "roi-tmirza";
                        //password = "Abdul2007$";
                        username = "cigniti-akothuri";
                        password = "TestingMRO@123";
                        break;

                    default:
                        username = "seleniumautomation";
                        password = "Testauto1$";
                        break;
                }
            }
        }

        public static void takeScreenShot()
        {
            Screenshot ss = ((ITakesScreenshot)BaseWebDriver.Value).GetScreenshot();
            //  string date = DateTime.Now.ToString("hh - mm - ss - tt");
            string date = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-tt");
            ss.SaveAsFile(Driver.projectPath + "screenshot" + date + ".png", OpenQA.Selenium.ScreenshotImageFormat.Jpeg);
            //Driver.logger.AddScreenCaptureFromPath("./screenshot" + date + ".png");
           // Driver.logger.Info("Snapshot below: " + Driver.logger.AddScreenCaptureFromPath("./screenshot" + date + ".png"));

        }

        public static void ScrollIntoView(string locatorKey, FindElementBy findElementBy)
        {
            IWebElement element;
            if (findElementBy == FindElementBy.Id)
            {
                element = BaseWebDriver.Value.FindElement(By.Id(locatorKey));
            }
            else if (findElementBy == FindElementBy.Xpath)
            {
                element = BaseWebDriver.Value.FindElement(By.XPath(locatorKey));
            }
            else
            {
                element = BaseWebDriver.Value.FindElement(By.TagName(locatorKey));
            }

            IWebDriver dr = BaseWebDriver.Value;
            IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void IsElementPresent(string locatorKey,int timeOut)
        {
            int i = BaseWebDriver.Value.FindElements(By.XPath(locatorKey)).Count;
            Console.WriteLine(i + locatorKey);
            int _timeElapsed = 0;
            while (i== 0 && _timeElapsed !=timeOut)
            {
                i = BaseWebDriver.Value.FindElements(By.XPath(locatorKey)).Count;
                Thread.Sleep(1000);
                _timeElapsed += 1000;
            }
            
        }

        public static void Select(string topLevelMenuText, string subMenuText)
        {
            //Driver.WaitUntilDOMLoaded();
            var topLevelMenu = BaseWebDriver.Value.FindElement(By.XPath($"//td[contains(text(),'{topLevelMenuText}')]"));
            Actions action = new Actions(BaseWebDriver.Value);
            action.Click(topLevelMenu).Build().Perform();
            //action.Perform();
            Driver.Wait(TimeSpan.FromSeconds(2));

            BaseWebDriver.Value.FindElement(By.XPath($"//td[contains(text(),'{subMenuText}')]")).Click();
        }


        public void Dispose()
        {
             BaseWebDriver.Value.Close();
             BaseWebDriver.Value.Quit();
            //Kill Driver here
            //TestContext instance will have the AssertCounts
            //But The Testcontext instance will have the result as Inconclusive.
        }
    }


}
