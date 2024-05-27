using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Interactions;

namespace LearnCSharpSelenium
{
    class AlertActionAutosuggestion
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

            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";

        }

        [TearDown]
        public void StopBrowser()
        {
            driver.Quit();
        }

        [Test]
        public void AlertDemo()
        {
            String inputName = "restapi";

            // generating alertbox
            driver.FindElement(By.CssSelector("input#name")).SendKeys(inputName);
            driver.FindElement(By.CssSelector("input#confirmbtn")).Click();

            // alertbox operation
            String alertText = driver.SwitchTo().Alert().Text;

            // accept alert
            driver.SwitchTo().Alert().Accept();

            // Assert alertbox content
            StringAssert.Contains(inputName, alertText);


        }

        [Test]
        public void AutoSuggestionDemo()
        {
            // input country name in the textbox
            String inputCountryCode = "IND";
            String expectedCountry = "India";

            driver.FindElement(By.CssSelector("input#autocomplete")).SendKeys(inputCountryCode);
            Thread.Sleep(2000);

            // capture the dynamic options and put into IList
            IList<IWebElement> options = driver.FindElements(By.CssSelector("li.ui-menu-item div"));

            foreach(IWebElement element in options)
            {
                // if one of the options is INDIA then select it
                if(element.Text.Equals(expectedCountry))
                {
                    element.Click();   
                }
            }

            // For dynamically passed value we have to use getAtrribute()
            String actualCountryText =driver.FindElement(By.CssSelector("input#autocomplete")).GetAttribute("value");

            // Assert
            StringAssert.Contains(expectedCountry, actualCountryText);

        }

        [Test]
        public void ActionDemo() {

            driver.Url = "https://rahulshettyacademy.com/";

            Thread.Sleep(5000);
            Actions act = new Actions(driver);

            act.MoveToElement(driver.FindElement(By.CssSelector("a.dropdown-toggle"))).Perform();

        }

        [Test]
        public void DragAndDropDemo()
        {
            driver.Url = "https://demoqa.com/droppable";

            Thread.Sleep(5000);
            Actions act = new Actions(driver);

            IWebElement source = driver.FindElement(By.Id("draggable"));
            IWebElement dest = driver.FindElement(By.Id("droppable"));

            act.DragAndDrop(source, dest).Perform();

            String droppedText = driver.FindElement(By.Id("droppable")).GetAttribute("p");

            // Assert
            //Assert.That(droppedText, Is.EqualTo("Dropped!"));

        }

        // sample portal is down
       [Test]
        public void frameDemo()
        {
            driver.Url = "https://selectorshub.com/xpath-practice-page/";

           // driver.FindElement(By.XPath("//div[@aria-label='Close']")).Click();
            
            // frame webelement
            IWebElement frameElement = driver.FindElement(By.CssSelector("div#tablepress-1_info"));

            // JavaScript Executor to scroll into the view of frame
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", frameElement);

            Thread.Sleep(1000);
            driver.SwitchTo().Frame("coming google");

            Thread.Sleep(1000);

            driver.FindElement(By.CssSelector("div#i5")).Click();  


        }

    }

}
