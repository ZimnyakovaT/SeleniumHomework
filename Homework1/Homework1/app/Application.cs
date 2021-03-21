using Homework1.pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1.app
{
    public class Application
    {
        private IWebDriver driver;

        internal ProductPage productPage;
        internal MainPage mainPage;
        internal CartPage cartPage;

        public Application()
        {
            driver = new ChromeDriver();
            productPage = new ProductPage(driver);
            mainPage = new MainPage(driver);
            cartPage = new CartPage(driver);
        }
            public void Quit()
        {
            foreach (LogEntry l in driver.Manage().Logs.GetLog(LogType.Browser))
                {
                   Console.WriteLine(l);
                }
                driver.Quit();
        }
    }
}
