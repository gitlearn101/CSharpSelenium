using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;

namespace LearnCSharpSelenium
{
    class Locators_2
    {

#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        public IWebDriver driver;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method

        [SetUp]
        public void StartBrowser()
        {
            // Setup chromedriver using WebdriverManager
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            // Implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();

            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }

        [Test]
        public void LocatorsDemo()
        {

            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Name("password")).SendKeys("12345");

            // checkbox operation
            driver.FindElement(By.XPath("//input[@id='terms']")).Click();

            // sign-in btn
            IWebElement signInBtn = driver.FindElement(By.CssSelector("input[type='submit']"));
            signInBtn.Click();

            //Thread.Sleep(3000);
            // Explicitly wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(driver.FindElement(By.CssSelector("input[id='signInBtn']")),"Sign In"));


            String errorMsg = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine("errorMsg >> "+errorMsg);

            IWebElement blinkingText = driver.FindElement(By.XPath("//a[@class='blinkingText']"));
            TestContext.Progress.WriteLine("blinkingText >> " + blinkingText);
            String hrefAttribute = blinkingText.GetAttribute("href");

            String expectedUrl = "https://rahulshettyacademy.com/documents-request";

            //validate
            Assert.That(expectedUrl, Is.EqualTo(hrefAttribute));

            

        }

        [TearDown]
        public void CloseBrowser()
        {

             driver.Quit();

        }







    }
}
