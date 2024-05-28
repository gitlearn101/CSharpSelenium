using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using CSharpSelFramework.utilities;

namespace CSharpSelFramework.tests
{
    public class EndToEndTest : Base
    {


        [Test]
        public void EndToEndFlow()
        {
            // expected product to be added to cart
            string[] expectedProducts = ["iphone X", "Blackberry"];

            // actual product list from cart screen
            string[] actualProducts = new string[expectedProducts.Length];

            // delivery location
            string deliveryCountry = "India";
            string deliveryCode = "IND";

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

                string actualProduct = product.FindElement(By.CssSelector("h4.card-title a ")).Text;

                TestContext.Progress.WriteLine("Actual product name in UI >> " + actualProduct);

                // if expectedProduct is available in UI the click on 'Add to Cart' button
                if (expectedProducts.Contains(actualProduct))
                {

                    product.FindElement(By.CssSelector("div.card-footer button")).Click();


                }

            }

            // Click on 'checkout' btn
            driver.FindElement(By.PartialLinkText("Checkout")).Click();

            // capture the products from cart screen
            IList<IWebElement> checkoutProducts = driver.FindElements(By.CssSelector("h4 a"));

            // retrieve text 
            for (int i = 0; i < checkoutProducts.Count; i++)
            {
                actualProducts[i] = checkoutProducts[i].Text;
            }

            // Assert two arrays
            Assert.That(actualProducts, Is.EqualTo(expectedProducts));

            // click on final checkout btn in cart screen
            driver.FindElement(By.CssSelector("button.btn-success")).Click();

            // select delivery location using auto-suggestion
            driver.FindElement(By.Id("country")).SendKeys(deliveryCode);
            Thread.Sleep(5000);

            WebDriverWait waitSuggestion = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            waitSuggestion.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));

            // click on relevant country from suggestion box
            driver.FindElement(By.LinkText(deliveryCountry)).Click();




            // select T&C checkbox
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("label[for*='checkbox2']")).Click();

            // Click 'Purchase' btn

            driver.FindElement(By.CssSelector("input.btn-lg")).Click();

            // validation the success message
            string successMsg = driver.FindElement(By.XPath("//div[@class='alert alert-success alert-dismissible']")).Text;
            TestContext.Progress.WriteLine("successMsg >> " + successMsg);
            StringAssert.Contains("Success", successMsg);


        }
    }
}