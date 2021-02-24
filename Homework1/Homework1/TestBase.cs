using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using NUnit.Framework;

namespace Homework1
{
    public class TestBase
    {
        public static ThreadLocal<IWebDriver> tlDriver = new ThreadLocal<IWebDriver>();
        public IWebDriver driver;
        public WebDriverWait wait;

        public bool IsElementPresent(IWebDriver driver, By locator)
        {
            try
            {
                //driver.FindElement(locator);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                IWebElement element = wait.Until(ExpectedConditions.ElementExists(locator));
                return true;
            }
            catch (InvalidSelectorException ex)
            {
                throw ex;
            }
            catch (NoSuchElementException ex)
            {
                return false;
            }
        }
        public bool AreElementsPresent(IWebDriver driver, By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }

        [SetUp]
        public virtual void start()
        {
            //ChromeOptions options = new ChromeOptions();
            //options.AddArguments("start-fullscreen");
            //InternetExplorerOptions options = new InternetExplorerOptions();
            //options.RequireWindowFocus = false; //Возвращает или задает значение, показывающее, требуется ли фокусировку окна браузера перед взаимодействием с элементами.
            //options.BinaryLocation= @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            //options.AddArguments("start-maximized");
            if (tlDriver.Value != null)
            {
                driver = tlDriver.Value;
                wait = new WebDriverWait(driver, new TimeSpan(0,0,60));
                return;
            }
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60); //неявные ожидания
            tlDriver.Value = driver;
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 60));
            //Console.WriteLine(options);
            //driver = new FirefoxDriver();
            //driver = new InternetExplorerDriver
 
        }


        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
