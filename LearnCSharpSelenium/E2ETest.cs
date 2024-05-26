// Perform e2e operation on ecomm portal

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace LearnCSharpSelenium
{
    class E2ETest
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
        public void EndToEndFlow()
        {
            // expected product to be added to cart
            String[] expectedProducts = ["iphone X", "Blackberry"];

            // login with valid credential
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Name("password")).SendKeys("learning");
            IWebElement signInBtn = driver.FindElement(By.CssSelector("input[type='submit']"));
            signInBtn.Click();

            // explicit wait for 'checkout' button to be displayed
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(By.XPath("//a[@class='nav-link btn btn-primary']")));

            // collects actual product details from UI
            IList<IWebElement> products = driver.FindElements(By.TagName("app-card"));

            foreach (IWebElement product in products)
            {

                String actualProduct = product.FindElement(By.CssSelector("h4.card-title a ")).Text;

                TestContext.Progress.WriteLine("Actual product name in UI >> " + actualProduct);

                // if expectedProduct is available in UI the click on 'Add to Cart' button
                if (expectedProducts.Contains(actualProduct))
                {

                    product.FindElement(By.CssSelector("div.card-footer button")).Click();


                }

            }

        }



    }
}
