using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using System.Collections;

namespace LearnCSharpSelenium
{
    class SortWebtable
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

            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";

        }

        [Test]
        public void SortTable()
        {

            ArrayList alA = new ArrayList();
            ArrayList alB = new ArrayList();


            // select '20' from pagesize dropdown
            SelectElement pageSizeDropdown = new SelectElement(driver.FindElement(By.Id("page-menu")));
            pageSizeDropdown.SelectByValue("20");

            // step 1 : consolidate all veg name in arraylist A

            IList<IWebElement> vegProducts = driver.FindElements(By.XPath("//tr/td[1]"));

            foreach (IWebElement vegProduct in vegProducts)
            {
                alA.Add(vegProduct.Text);

            }

            // step 2 : sort arraylist
            alA.Sort();

            foreach (String ele in alA)
            {
                TestContext.Progress.WriteLine("After sorting : " + ele);
            }



            // step 3 : click coln name 
            driver.FindElement(By.CssSelector("th[aria-label*='fruit']")).Click();
            //tip - regular exp using css >> th[aria-label*='fruit']
            //tip - regular exp using xpath >> //th[contains(@aria-label,'fruit')]


            // step 4 : Get all veg name into arraylist B

            IList<IWebElement> SortedVegProducts = driver.FindElements(By.XPath("//tr/td[1]"));

            foreach (IWebElement vegProduct in SortedVegProducts)
            {
                alB.Add(vegProduct.Text);

            }



            // Step 5 : compare arraylist A and B 

            Assert.That(alA, Is.EqualTo(expected: alB));
        }



    }
}
