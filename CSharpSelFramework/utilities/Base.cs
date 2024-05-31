

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using AventStack.ExtentReports;
using ICSharpCode.SharpZipLib.Zip;

#pragma warning disable CS0105 // Using directive appeared previously in this namespace
using AventStack.ExtentReports;
#pragma warning restore CS0105 // Using directive appeared previously in this namespace
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.DevTools.V85.HeadlessExperimental;
using System.Runtime.CompilerServices;


namespace CSharpSelFramework.utilities
{
    public class Base
    {
        public ExtentReports extent;
        public ExtentTest test;

        // defined for terminal cmd execution
        String browserName;

        /*
        // extent reports
        [OneTimeSetUp]
        public void Setup()
        {
            String workingDirectory = Environment.CurrentDirectory;
            String projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath = projectDirectory + "//index.html";

            var htmlReporter = new ExtentHtmlReporter(reportPath);

            extent = new ExtentReports();

            // need to debug below line
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local machine");
            extent.AddSystemInfo("Env", "QA");
        }
*/

        //#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        //   public IWebDriver driver;
        // #pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method


#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method


        [SetUp]
        public void StartBrowser()
        {
          
            
            // dynamicall grab test/method name for report purpose
          // test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            // configuration for terminal run
           browserName= TestContext.Parameters["browserName"];

            if (browserName == null)
            {

                // facing null pointer error. Will debug later
                browserName = ConfigurationManager.AppSettings["browser"];

            }
            initBrowser(browserName);

            //initBrowser("Chrome");

            // Implicit wait
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Value.Manage().Window.Maximize();

            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }



        public IWebDriver getDriver()  // as per OOPs protocol, we shouldn't call field directly into another class
        {                              // Better to call using Method 
            return driver.Value;
        }

        public void initBrowser(String browserName)
        {
            switch(browserName)
            {

                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;

                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;

            }
        }


        [TearDown]
        public void StopBrowser()
        {
           // Need to debug once htmlReporter is Fixed
            /*
            var status = TestContext.CurrentContext.Result.Outcome.Status;


            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";


            if(status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Fail("Test failed", captureScreenShot(driver.Value, fileName));
                test.Log(Status.Fail, " test failed with logtrace "+stackTrace);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {

            }

            extent.Flush();
            */
            driver.Value.Quit();
        }

        //MediaEntityModelProvider
        public AventStack.ExtentReports.Model.Media captureScreenShot(IWebDriver driver,  String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }

    }

    
}
