
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;



namespace LearnCSharpSelenium
{
    class SeleniumFirst_1
    {
#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        public IWebDriver driver;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());

           driver  = new ChromeDriver();

            driver.Manage().Window.Maximize();

        }

        [Test]
        public void Test1() {

            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

            String title = driver.Title;

            TestContext.Progress.WriteLine("The actual title >>"+title);

            TestContext.Progress.WriteLine("The expected title >> "+driver.Title);
        
        }

        [TearDown]
        public void CloseBrowser()
        {

          // driver.Quit();

        }

    }
}
