using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string DriverDirectory = "C:\\seleniumDriver";
        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _driver = new ChromeDriver(DriverDirectory);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }

        [TestInitialize]
        public void OpenPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/");
        }
        [TestMethod]
        public void TestMethod1()
        {
            String title = _driver.Title;
            Assert.AreEqual("app", title);   // Tester på om titlen på din hjemmeside er det samme som det du forventer

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)); // Programmet venter 10 sek på et svar, før end den failer
        }
    }
}
