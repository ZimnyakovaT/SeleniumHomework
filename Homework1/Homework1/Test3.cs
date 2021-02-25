using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class Test3 : TestBase
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
            //options.AddArguments("start-fullscreen");
            driver = new ChromeDriver(options);
            tlDriver.Value = driver;
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));


        }

        [Test, Order(3)]

        public void Test_3()
        {
            driver.Url = "http://localhost/litecart/";

            var ducks = driver.FindElements(By.XPath($"//*[@class='product column shadow hover-light']"));
            var duckscount = ducks.Count();
            int stickercount = 0;
            foreach (var duck in ducks)
            { 
              var sticker = duck.FindElements(By.CssSelector("[class*=sticker]"));
                if (sticker.Count() == 1)
                {
                    stickercount++;
                }
                else
                    throw new Exception("Стикеров больше чем 1 на 1 товар ");
            }
            Assert.AreEqual(duckscount, stickercount);
        }
    }
}
