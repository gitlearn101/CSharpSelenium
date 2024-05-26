using System.Diagnostics;

namespace LearnCSharpSelenium
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Inside the setup method");
        }

        [Test]
        public void Test1()
        {
            Debug.WriteLine("Test1 from debug");
            Console.WriteLine("Test1 from console write");
            TestContext.Progress.WriteLine("Inside Test1");
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            TestContext.Progress.WriteLine("Inside Test2");
            Assert.Pass();
        }

        [TearDown]
        public void closeBrowser() { 
        
        
        
        }
    }
}