using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using NUnit.Framework;

namespace Homework1
{
    [TestFixture]
    [Parallelizable(scope: ParallelScope.All)]
    public class Test1 : TestBase
    {

        [SetUp]
        public override void start()
        {
            
            if (tlDriver.Value != null)
            {
                driver = tlDriver.Value;
                wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
                return;
            }
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-fullscreen");
            driver = new ChromeDriver(options);
            tlDriver.Value = driver;
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));

            
        }

        [Test, Order(1)]
 
        public void Test_1()
        {
            driver.Url = "http://www.google.com/";
            driver.FindElement(By.Name("q")).SendKeys("webdriver");
            wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Name("btnK"))));
            driver.FindElement(By.Name("btnK")).Click();
            wait.Until(ExpectedConditions.TitleIs("webdriver - Поиск в Google"));
        }

    }
}
