using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSelFramework.pageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver) { 

            this.driver = driver;
            PageFactory.InitElements(driver, this);
        
        }


        // driver.FindElement(By.CssSelector("input[type='submit']")).Click();

        // PageObject Factory
        [FindsBy(How = How.Id, Using = "username")]  // deconstruct as driver.findElement(By.Id("username"))
        private IWebElement username;

        [FindsBy(How = How.Name, Using = "password")]  
        private IWebElement password;

        [FindsBy(How = How.CssSelector, Using = "input[type='submit']")]
        private IWebElement signInButton;


        public ProductsPage validLogin(String user, String pass)
        {
            username.SendKeys(user);
            password.SendKeys(pass);
            signInButton.Click();

            return new ProductsPage(driver); // creating object for next page in this method itself to ensure driver continuity
        }


        public IWebElement getUsername()
        {
            return username;
        }


    }
}
