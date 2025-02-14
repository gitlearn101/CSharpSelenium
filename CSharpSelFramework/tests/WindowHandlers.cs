﻿using CSharpSelFramework.utilities;
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
    class WindowHandlers : Base
    {

        [TearDown]
        public void stopBrowser()
        {
            driver.Value.Quit();
        }

        [Test]
        public void windowHandlers()
        {
            // click on the blinking text to open a new tab
            driver.Value.FindElement(By.ClassName("blinkingText")).Click();


            Assert.That(driver.Value.WindowHandles.Count, Is.EqualTo(2));

            String childWindow = driver.Value.WindowHandles[1];
            String parentWindow = driver.Value.WindowHandles[0];

            // transfer driver control to childWindow
            driver.Value.SwitchTo().Window(childWindow);

            // get text from childWindow to prove that control is now moved to childWindow
            String textChildWindow = driver.Value.FindElement(By.CssSelector(".red")).Text;
            TestContext.Progress.WriteLine(textChildWindow);

            // capture the email from above text
            String[] splittedText = textChildWindow.Split("at");

            String[] trimmedText = splittedText[1].Trim().Split(" ");

            String email = trimmedText[0];

            TestContext.Progress.WriteLine("The email is >> "+email);

            // pass driver control back to parentWindow

            // method 1
            driver.Value.SwitchTo().Window(parentWindow);

            // method 2
            //driver.SwitchTo().

            // input email from above lines into email textbox
            driver.Value.FindElement(By.Id("username")).SendKeys(email);


        }
    }
}
