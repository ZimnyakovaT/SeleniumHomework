using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace Homework1
{
    [TestFixture]
    public class Homework1
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0,0,10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }


        [Test, Order(1)]
        public void Test1()
        {
            driver.Url = "http://www.google.com/";
            driver.FindElement(By.Name("q")).SendKeys("webdriver");
            wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Name("btnK"))));
            driver.FindElement(By.Name("btnK")).Click();
            wait.Until(ExpectedConditions.TitleIs("webdriver - Поиск в Google"));
        }

        [Test, Order(2)]
        public void Test2()
        {
            driver.Url = "http://localhost/litecart/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.ClassName("logotype"))));
        }


        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
