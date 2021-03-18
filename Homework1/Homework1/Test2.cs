using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using NUnit.Framework;

namespace Homework1
{
    [TestFixture]
    [Parallelizable(scope: ParallelScope.All)]
    public class Test2 : TestBase
    {
        [SetUp]
        public override void start()
        {

           /* if (tlDriver.Value != null)
            {
                driver = tlDriver.Value;
                wait = new WebDriverWait(driver, new TimeSpan(0, 0, 60));
                return;
            }*/
            driver = new EventFiringWebDriver( new InternetExplorerDriver());
            driver.Manage().Window.FullScreen();
            tlDriver.Value = driver;
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 60));


        }
        [Test, Order(2)]

        public void Test_2()
        {
            driver.Url = "http://localhost/litecart/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            Cookie testCookie = driver.Manage().Cookies.GetCookieNamed("language_code");
            wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.ClassName("logotype"))));

            var menuItems = driver.FindElements(By.Id("app-"));
            for (var i=0; i< menuItems.Count; i++)
            {
                menuItems = driver.FindElements(By.Id("app-"));

                menuItems.ToArray()[i].Click(); //кликаем на пункты основного меню
                var menuItem = driver.FindElement(By.XPath($"//*[@id='app-'][{i + 1}]")); //заново находим локатор пункта меню
                wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector("h1")))); //ждем когда отобразятся вложенные пункты 
                var menusubItems = menuItem.FindElements(By.CssSelector("li")); //ищем вложенные 
                for  (var j=0; j< menusubItems.Count; j++)
                {
                    menuItem = driver.FindElement(By.XPath($"//*[@id='app-'][{i + 1}]")); //заново находим локатор пункта меню
                    menusubItems = menuItem.FindElements(By.CssSelector("li"));
                    wait.Until(ExpectedConditions.ElementToBeClickable(menusubItems.ToArray()[j])); //ждем загрузку вложенных
                    menusubItems.ToArray()[j].Click(); //кликаем по вложенному
                    wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector("h1"))));
                }
            }

        }


    }
}
