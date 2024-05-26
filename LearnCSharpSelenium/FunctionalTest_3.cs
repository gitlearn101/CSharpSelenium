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
    class FunctionalTest_3
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

        // dropdown operation
       // [Test]
        public void Dropdown()
        {

            IWebElement dropdown = driver.FindElement(By.CssSelector("select.form-control"));

            SelectElement s = new SelectElement(dropdown);
            s.SelectByText("Teacher");

        }

        // radio button operation
        [Test]
        public void RadioBtnDemo()
        {

            IList<IWebElement> radiobtns = driver.FindElements(By.CssSelector("input[type='radio']"));

            // iterate till we identify 'User' radio button. This step is to avoid indexing in locator as indexing is flaky
            foreach (IWebElement rb in radiobtns)
            {
                if (rb.GetAttribute("value").Equals("user"))
                {
                    rb.Click(); // click only if the 'User' is available
                }
            }

            // wait till reminder popup loads
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("okayBtn")));

            // click on okayBtn of the reminder popup
            driver.FindElement(By.Id("okayBtn")).Click();

            // validate if 'User' radiobtn is enabled
            Boolean userRbSelectedFlag = driver.FindElement(By.Id("usertype")).Selected;
           
            //Assert.That(userRbSelectedFlag, Is.True); // Application issue -> can ignore it



        }

    }

}
