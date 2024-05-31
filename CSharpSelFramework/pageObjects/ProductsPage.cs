using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;
using System.Runtime.CompilerServices;

namespace CSharpSelFramework.pageObjects
{
    public class ProductsPage
    {
        IWebDriver driver;
        By cardTitle = By.CssSelector(".card-title a");
        By AddToCartButton = By.CssSelector(".card-footer button");

        public ProductsPage(IWebDriver driver) { 
            
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        
        }

        [FindsBy(How =How.TagName, Using = "app-card")]
        private IList<IWebElement> cards;

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement checkout;


        public void waitForPageLoad()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(By.XPath("//a[@class='nav-link btn btn-primary']")));
        }


        public IList<IWebElement> getCards()
        {
            return cards;

        }


        public By getCardTitle()
        {
            return cardTitle;
        }

        public By addToCart()
        {
            return AddToCartButton;
        }

        public CheckoutPage ClickcheckoutBtn()
        {
            checkout.Click();
            return new CheckoutPage(driver);

        }

    }
}
