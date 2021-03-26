using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MRO.ROI.Test.Utilities
{
    [TestClass]
    public class ROITestBase
    {
        private readonly ROITestArea _testArea;
        public TestContext TestContext { get; set; }

        public ROITestBase(ROITestArea testArea)
        {
            _testArea = testArea;
        }
        //[TestInitialize]
        //public void browsersetup()
        //{
        //    string webAppUrl = GetWebAppURL();
        //    string driverPath = TestContext != null ? TestContext.Properties["driverPath"].ToString() : @"C:\WebDrivers";

        //    Driver.Initialize(DriverType.Chrome, webAppUrl, driverPath);
        //    Driver.MaximizeWindow();

        //}


        //[TestInitialize]
        public void Init() 
        {
           
            string username = string.Empty;
            string password = string.Empty;
            string webAppUrl = GetWebAppURL();
            string driverPath = TestContext != null ? TestContext.Properties["driverPath"].ToString() : @"C:\WebDrivers";
            // string driverPath = TestContext != null ? @"C:\TestAutomation\MRO.AutomationTest.Solution\MRO.ROI.Test\Utilities" : TestContext.Properties["driverPath"].ToString();
            //Driver.Initialize(DriverType.IE, webAppUrl, driverPath);        

            Driver.Initialize(DriverType.Chrome, webAppUrl, driverPath);
            Driver.MaximizeWindow();


            //LoginMenuPage.GoToLoginMenuPage();
            //    switch (_testArea)
            //    {
            //        case ROITestArea.ROIFacility:
            //            GetCredentials("facility", out username, out password);
            //            LoginPage.GoToROIFaclityLoginPage();
            //            LoginPage.LoginAs(username).WithPassword(password).Login(TestBase.BaseWebDriver.Value);
            //            Driver.WaitUntilDOMLoaded();
            //            break;
            //        case ROITestArea.ROIAdmin:
            //            GetCredentials("admin", out username, out password);
            //           // LoginPage.GoToROIAdminLoginPage();
            //            LoginPage.LoginAs(username).WithPassword(password).Login(TestBase.BaseWebDriver.Value);
            //            Driver.WaitUntilDOMLoaded();
            //            break;
            //        case ROITestArea.ROIExternalRequesterPortal:
            //            GetCredentials("rqrPortal", out username, out password);
            //            LoginPage.GoToROIExternalRequesterPortal();
            //            LoginPage.LoginAs(username).WithPassword(password).Login(TestBase.BaseWebDriver.Value);
            //            Driver.WaitUntilDOMLoaded();
            //            break;
            //        case ROITestArea.ROIInternalRequesterPortal:
            //            GetCredentials("intPortal", out username, out password);
            //            LoginPage.GoToROIInternalRequesterPortal();
            //            LoginPage.LoginAs(username).WithPassword(password).Login(TestBase.BaseWebDriver.Value);
            //            Driver.WaitUntilDOMLoaded();
            //            break;
            //        //adding for iron mountain
            //        case ROITestArea.IronMountainROIFacility:
            //            GetCredentials("ironmountain", out username, out password);
            //            LoginPage.GoToIronMountainROIFaclityLoginPage();
            //            LoginPage.LoginAs(username).WithPassword(password).Login(TestBase.BaseWebDriver.Value);
            //            Driver.WaitUntilDOMLoaded();
            //            break;

            //        //TODO: Implement additional navigation and login pages.

            //        default:
            //            break;
            //    }

            }


       

        private string GetWebAppURL()
        {
            //return TestContext != null ?
            //    TestContext.Properties["webAppUrl"].ToString() :
            //    //@"https://hqqa01.mro.com/net4.0/Login/dev/login/LoginMenu.aspx";
            //    @"https://staging.mrocorp.com/net4.0/Login/dev/login/LoginMenu.aspx";
            string appurl = TestContext != null ?
                  @"https://staging.mrocorp.com/net4.0/Login/dev/login/LoginMenu.aspx" : TestContext.Properties["webAppUrl"].ToString();

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
                        username = "ssaligrama";
                        password = "cigniti@1359";
                        break;
                    case "admin":
                        //username = "roi-tmirza";
                        //password = "Abdul2007$";
                        username = "seleniumautomation";
                        password = "Testauto1$";
                        break;
                    case "rqrPortal":
                        username = "seleniumautomation";
                        password = "Testmro2$";
                        break;
                    case "intPortal":
                        username = "int-seleniumautomation";
                        password = "Testmro02$";
                        break;
                    //Added Iron Mountain credentials to Log in
                    case "ironmountain":
                        username = "irmtmirza";
                        password = "Abdul2007$";
                        break;
                    default:
                        username = "seleniumautomation";
                        password = "Testauto1$";
                        break;
                }
            }
        }


        public static int getworkers()
        {
            int workersCount = 0;

            string path = @"C:\TestAutomation\MRO.AutomationTest.Solution\MRO.ROI.Test\Properties\AssemblyInfo.cs";
            if (File.Exists(path))
            {
                // Open the file to read from.
                string[] readText = File.ReadAllLines(path);
                var versionInfoLines = readText.Where(t => t.Contains("[assembly: Parallelize"));
                foreach (string item in versionInfoLines)
                {
                    string version = item.Substring(item.IndexOf('(') + 1);

                    string stringAfterChar = version.Substring(version.IndexOf("=") + 2);

                   string input = stringAfterChar.Substring(0, stringAfterChar.IndexOf(",") + 0);

                    workersCount = Convert.ToInt32(input);

                }
               
            }
            return workersCount;

        }
        [TestCleanup]
        public void Cleanup()
        {
            Driver.Close();
           
        }
    }
}
