using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace MRO.ROI.Test
{
    [TestClass]
    public class UnitTest2
    {
        String test_url = "https://lambdatest.github.io/sample-todo-app/";
        String itemName = "Yey, Let's add it to list";


        [TestMethod]
        public void NavigateToDoApp()
        {
            IWebDriver driver;

            // Local Selenium WebDriver
            driver = new ChromeDriver(@"C:\TestAutomation\MRO.AutomationTest.Solution\MRO.ROI.Test\Utilities");

            driver.Manage().Window.Maximize();



            driver.Navigate().GoToUrl(test_url);



            driver.Manage().Window.Maximize();


            // Click on First Check box
            IWebElement firstCheckBox = driver.FindElement(By.Name("li1"));
            firstCheckBox.Click();

            Thread.Sleep(20000);

            // Click on Second Check box

            IWebElement secondCheckBox = driver.FindElement(By.Name("li2"));

            secondCheckBox.Click();



            // Enter Item name
            IWebElement textfield = driver.FindElement(By.Id("sampletodotext"));
            textfield.SendKeys(itemName);


            // Click on Add button

            IWebElement addButton = driver.FindElement(By.Id("addbutton"));

            addButton.Click();


            // Verified Added Item name

            IWebElement itemtext = driver.FindElement(By.XPath("/html/body/div/div/div/ul/li[6]/span"));
            String getText = itemtext.Text;
            Assert.IsTrue(itemName.Contains(getText));


            /* Perform wait to check the output in this MSTest tutorial for Selenium */
            //System.Threading.Thread.Sleep(4000);


            Console.WriteLine("LT_ToDo_Test Passed");

            driver.Quit();
        }
    }



}
