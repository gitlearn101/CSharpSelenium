// category test can be executed from VS cmd terminal

// 1. Navigate the Project folder such as cd CsharpSelFramework
// 2. dotnet test CsharpSelFramework.csproj --filter TestCategory=Regression --% -- TestRunParameters.Parameter(name=\"browserName\", value=\"Chrome\")



using CSharpSelFramework.pageObjects;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpSelFramework.utilities;

namespace CSharpSelFramework.tests
{
    public class CategoryTest : Base
    {

        [Test, TestCaseSource(nameof(AddTestData)), Category("Regression")]

        // [TestCase("rahulshettyacademy", "learning")]
        // [TestCase("rahulshetty", "learning")]
        [Parallelizable(ParallelScope.All)]
        public void EndToEndFlow(String username, String password)
        {


            // expected product to be added to cart
            string[] expectedProducts = ["iphone X", "Blackberry"];

            // actual product list from cart screen
            string[] actualProducts = new string[expectedProducts.Length];

            // delivery location
            string deliveryCountry = "India";
            string deliveryCode = "IND";

            // creating object of pageobject classes
            LoginPage loginPage = new LoginPage(getDriver());

            // login with valid credential
            ProductsPage productsPage = loginPage.validLogin(username, password);

            // explicit wait for 'checkout' button to be displayed
            productsPage.waitForPageLoad();

            // collects actual product details from UI
            IList<IWebElement> products = productsPage.getCards();


            foreach (IWebElement product in products)
            {
                // if expectedProduct is available in UI the click on 'Add to Cart' button
                if (expectedProducts.Contains(product.FindElement(productsPage.getCardTitle()).Text))
                {
                    product.FindElement(productsPage.addToCart()).Click();
                }

            }

            // Click on 'checkout' btn
            CheckoutPage checkoutPage = productsPage.ClickcheckoutBtn();


            // capture the products from cart screen
            IList<IWebElement> checkoutCards = checkoutPage.getCheckoutCards();

            // retrieve text 
            for (int i = 0; i < checkoutCards.Count; i++)
            {
                actualProducts[i] = checkoutCards[i].Text;
            }

            // Assert two arrays
            Assert.That(actualProducts, Is.EqualTo(expectedProducts));

            // click on final checkout btn in cart screen
            checkoutPage.checkout();

            // select delivery location using auto-suggestion
            driver.Value.FindElement(By.Id("country")).SendKeys(deliveryCode);
            Thread.Sleep(5000);

            WebDriverWait waitSuggestion = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(15));
            waitSuggestion.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));

            // click on relevant country from suggestion box
            driver.Value.FindElement(By.LinkText(deliveryCountry)).Click();

            // select T&C checkbox
            Thread.Sleep(2000);
            driver.Value.FindElement(By.CssSelector("label[for*='checkbox2']")).Click();

            // Click 'Purchase' btn
            driver.Value.FindElement(By.CssSelector("input.btn-lg")).Click();

            // validation the success message
            string successMsg = driver.Value.FindElement(By.XPath("//div[@class='alert alert-success alert-dismissible']")).Text;
            TestContext.Progress.WriteLine("successMsg >> " + successMsg);
            StringAssert.Contains("Success", successMsg);


        }

        public static IEnumerable<TestCaseData> AddTestData()
        {

            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"));
            yield return new TestCaseData("login1", "pwd2"); // hardcoded test data
                                                             // yield return new TestCaseData(getDataParser().extractData("username_invalid"), getDataParser().extractData("password_invalid"), getDataParser().extractDataArray("products"));
        }


        [Test, Category("Smoke")]
        public void LocatorsDemo()
        {

            driver.Value.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.Value.FindElement(By.Name("password")).SendKeys("12345");

            // checkbox operation
            driver.Value.FindElement(By.XPath("//input[@id='terms']")).Click();

            // sign-in btn
            IWebElement signInBtn = driver.Value.FindElement(By.CssSelector("input[type='submit']"));
            signInBtn.Click();

            //Thread.Sleep(3000);
            // Explicitly wait
            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(driver.Value.FindElement(By.CssSelector("input[id='signInBtn']")), "Sign In"));


            String errorMsg = driver.Value.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine("errorMsg >> " + errorMsg);

            IWebElement blinkingText = driver.Value.FindElement(By.XPath("//a[@class='blinkingText']"));
            TestContext.Progress.WriteLine("blinkingText >> " + blinkingText);
            String hrefAttribute = blinkingText.GetAttribute("href");

            String expectedUrl = "https://rahulshettyacademy.com/documents-request";

            //validate
            Assert.That(expectedUrl, Is.EqualTo(hrefAttribute));



        }



    }
}
