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

            var ducks = driver.FindElements(By.XPath($"//*[@class='image-wrapper']"));
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

        

        public void login()
        {
            driver.Url = "http://localhost/litecart/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            Cookie testCookie = driver.Manage().Cookies.GetCookieNamed("language_code");
            wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.ClassName("logotype"))));
        }
        public void CheckSortAsc(List<string> countryNames)
        {
            var countryNamesArray = countryNames.ToArray();

            for (var i = 0; i < (countryNamesArray.Length - 1); i++)
            {
                if (countryNamesArray[i].CompareTo(countryNamesArray[i + 1]) > 0)
                    throw new Exception("ОШИБКА!!!!!!!!! ");
            }

        }
        [Test, Order(4)]
        public void Test_4()
        {
            login();
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector("h1"))));
            var countries = driver.FindElements(By.CssSelector(".row"));
            List<string> countryNames = new List<string>();

            //task 9 part 1.a
            foreach (var country in countries)
            {
                var t1 = country.FindElement(By.CssSelector("a")).GetAttribute("text");
                countryNames.Add(t1);
            }

            CheckSortAsc(countryNames);

            //task 9 part 1.b
            for (var j=0; j< countries.Count();j++)
            {
                var t1 = countries[j].FindElements(By.CssSelector("td")).ToArray();
                if (t1[5].GetAttribute("textContent") != "0")
                {
                    t1[4].FindElement(By.CssSelector("a")).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector("h1"))));
                    var states = driver.FindElements(By.CssSelector("#table-zones tr"));
                    var i = 0;
                    List<string> statesNames = new List<string>();
                    foreach (var state in states)
                    {
                        var t2 = state.FindElements(By.XPath($"//td"))[3].GetAttribute("textContent");
                        if (i != 0 && i != (states.Count - 1))
                            statesNames.Add(t2);
                        i++;
                    }
                    CheckSortAsc(statesNames);
                    driver.Navigate().Back();
                    wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector("h1"))));
                    countries = driver.FindElements(By.CssSelector(".row"));
                }
            }

        }

        [Test, Order(5)]
        //task 9 part 2
        public void Test_5()
        {
            login();
            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";
            wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector("h1"))));
            var countries = driver.FindElements(By.CssSelector(".row"));

            for (var j = 0; j < countries.Count(); j++)
            {
                List<string> geoZones = new List<string>();
                countries[j].FindElement(By.CssSelector("a")).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector("h1"))));
                
                var zones = driver.FindElement(By.Id("table-zones")).FindElements(By.CssSelector("tr"));
                foreach (var zone in zones)
                {
                    var tds = zone.FindElements(By.CssSelector("td"));
                    if(tds.Count > 2)
                    {
                        var options = tds[2]
                            .FindElement(By.CssSelector("select"))
                            .FindElements(By.CssSelector("option"));

                        foreach(var option in options)
                        {
                            if (option.Selected)
                                geoZones.Add(option.Text);

                        }
                    }
                }

                CheckSortAsc(geoZones);

                driver.Navigate().Back();
                countries = driver.FindElements(By.CssSelector(".row"));
            }
        }
    }
}
