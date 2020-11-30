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
        private static readonly string DriverDirectory = "C:\\chromedriver";
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
        public void LoginTitle()
        {
            String title = _driver.Title;
            Assert.AreEqual("app", title);   // Tester på om titlen på din hjemmeside er det samme som det du forventer

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)); // Programmet venter 10 sek på et svar, før end den failer
        }

        [TestMethod]
        public void Login()
        {
            IWebElement email = _driver.FindElement(By.Id("email"));
            IWebElement password = _driver.FindElement(By.Name("password"));
            email.SendKeys("string");
            password.SendKeys("string");

            IWebElement buttonElement = _driver.FindElement(By.Id("login"));
            Assert.AreEqual("Login", buttonElement.Text);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)); // Programmet venter 10 sek på et svar, før end den failer
        }

       [TestMethod]
       public void CreateUser() 
        {
            IWebElement opretUser = _driver.FindElement(By.Name("Opret Bruger"));
            IWebElement email = _driver.FindElement(By.Id("email"));
            email.SendKeys("string");
            IWebElement password = _driver.FindElement(By.Id("password"));
            password.SendKeys("string");
            IWebElement firstname = _driver.FindElement(By.Id("name"));
            firstname.SendKeys("string");
            IWebElement lastname = _driver.FindElement(By.Id("lastname"));
            lastname.SendKeys("string");
            IWebElement phone = _driver.FindElement(By.Id("phone"));
            phone.SendKeys("5555555");
            IWebElement addUser = _driver.FindElement(By.Name("Opret Bruger"));

        }
    }
}

