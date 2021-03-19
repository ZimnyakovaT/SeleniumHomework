﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using NUnit.Framework;
using System.Drawing.Imaging;

namespace Homework1
{
    [TestFixture]
    [Parallelizable(scope: ParallelScope.All)]
    public class Test1 : TestBase
    {

       [SetUp]
        public override void start()
        {

            /*if (tlDriver.Value != null)
            {
                driver = tlDriver.Value;
                wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
                return;
            }*/
            
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-fullscreen");
            driver = new EventFiringWebDriver( new ChromeDriver(options));
            driver.FindingElement += (sender, e) => Console.WriteLine(e.FindMethod);
            driver.FindElementCompleted += (sender, e) => Console.WriteLine(e.FindMethod + " found");
            driver.ExceptionThrown += (sender, e) => Console.WriteLine(e.ThrownException);
            tlDriver.Value = driver;
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));

            
        }

        [Test, Order(1)]
 
        public void Test_1()
        {
            driver.Url = "http://www.google.com/";
            driver.FindElement(By.Name("q")).SendKeys("webdriver");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(wd => driver.FindElement(By.Name("btnK")).Displayed && driver.FindElement(By.Name("btnK")).Enabled);
            driver.FindElement(By.Name("btnK")).Click();
            // if (driver.FindElement(By.Name("btnK")).Displayed)
            // {
            //     driver.FindElement(By.Name("btnK")).Click();
            // }
            //   wait.Until(ExpectedConditions.TitleIs("webdriver - Поиск в Google"));
        }

    }

}

