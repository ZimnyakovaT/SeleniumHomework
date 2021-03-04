﻿using System;
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
using System.Globalization;


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
            for (var j = 0; j < countries.Count(); j++)
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
                    if (tds.Count > 2)
                    {
                        var options = tds[2]
                            .FindElement(By.CssSelector("select"))
                            .FindElements(By.CssSelector("option"));

                        foreach (var option in options)
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

        [Test, Order(6)]
        public void Test_6()
        {
            driver.Url = "http://localhost/litecart/";
            var ducks = driver.FindElements(By.CssSelector("li a.link"));
            for (var j = 0; j < ducks.Count(); j++)
            {
                var duckname = ducks[j].GetProperty("title").ToString();
                var allprice = ducks[j].FindElement(By.CssSelector(".price-wrapper")).GetProperty("innerText").ToString();
                ducks[j].Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector("h1"))));
                var header = driver.FindElement(By.CssSelector("h1")).GetProperty("textContent");
                var allprice_ = driver.FindElement(By.CssSelector(".price-wrapper")).GetProperty("innerText").ToString();
                Assert.AreEqual(duckname, header); //task 10.a
                Assert.AreEqual(allprice, allprice_); //task 10.б
                driver.Navigate().Back();
                ducks = driver.FindElements(By.CssSelector("li a.link"));
            }

        }

        private string[] rgb(string regularpricecolor)
        {
            var t1 = regularpricecolor.Replace("rgba(", "")
                   .Replace(")", "")
                   .Replace(" ", "")
                   .Split(',');
            if (t1.Length != 4)
                throw new Exception("Не удалось извлечь RGB-значение из цвета!");
            return t1;
        }


        [Test, Order(7)]
        public void Test_7()
        {
            driver.Url = "http://localhost/litecart/";
            var ducks = driver.FindElements(By.CssSelector("li s.regular-price"));
            for (var j = 0; j < ducks.Count(); j++)
            {
                var regularpricecolor = ducks[j].GetCssValue("color");
                var textdecoration = ducks[j].GetCssValue("text-decoration-line");
                var regularsize = ducks[j].GetCssValue("font-size").Replace("px", "").Trim();
                var regularsize_ = Convert.ToDecimal(regularsize, new CultureInfo("en-US"));

                var saleprice = driver.FindElement(By.CssSelector(".campaign-price"));
                var salepricecolor = saleprice.GetCssValue("color");

                var t1 = rgb(regularpricecolor); //parse grey
                var t2 = t1[0] == t1[1] && t1[1]==t1[2];
                t1 = rgb(salepricecolor); //parse red
                var t3 = t1[1] == t1[2];

                var salepricebold = saleprice.GetCssValue("font-weight");
                var salepricesize =saleprice.GetCssValue("font-size").Replace("px", "").Trim();
                var salepricesize_= Convert.ToDecimal(salepricesize,new CultureInfo("en-US"));

                Assert.AreEqual(t2, true);//task 10.в
                Assert.AreEqual(textdecoration, "line-through");//task 10.в
                Assert.AreEqual(t3, true);//task 10.г
                Assert.AreEqual(salepricebold, "700");//task 10.г
                if (regularsize_ < salepricesize_)
                {
                    Assert.IsTrue(true); //task 10.д
                }
                else throw new Exception("Акционная цена не крупнее обычной!");

                ducks[j].Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector("h1"))));

                var regularpricecolor_ = driver.FindElement(By.CssSelector(".regular-price")).GetCssValue("color");
                var textdecoration_ = driver.FindElement(By.CssSelector(".regular-price")).GetCssValue("text-decoration-line");
                var salepricecolor_ = driver.FindElement(By.CssSelector(".campaign-price")).GetCssValue("color");
                var salepricebold_ = driver.FindElement(By.CssSelector(".campaign-price")).GetCssValue("font-weight");
                var regularsizenew = driver.FindElement(By.CssSelector(".regular-price")).GetCssValue("font-size").Replace("px", "").Trim();
                var regularsizenew_ = Convert.ToDecimal(regularsizenew, new CultureInfo("en-US"));
                var salepricesizenew = driver.FindElement(By.CssSelector(".campaign-price")).GetCssValue("font-size").Replace("px", "").Trim();
                var salepricesizenew_ = Convert.ToDecimal(salepricesizenew, new CultureInfo("en-US"));

                t1 = rgb(regularpricecolor_);
                t2 = t1[0] == t1[1] && t1[1] == t1[2];
                t1 = rgb(salepricecolor_);
                t3 = t1[1] == t1[2];

                Assert.AreEqual(t2, true);//task 10.в
                Assert.AreEqual(textdecoration_, "line-through");//task 10.в
                Assert.AreEqual(t3, true);//task 10.г
                Assert.AreEqual(salepricebold_, "700");//task 10.г
                if (regularsizenew_ < salepricesizenew_)
                {
                    Assert.IsTrue(true); //task 10.д
                }
                else throw new Exception("Акционная цена не крупнее обычной!");
                driver.Navigate().Back();
                ducks = driver.FindElements(By.CssSelector("li s.regular-price"));

            }
        }

        [Test, Order(8)]
        public void Test_8()
        {
            driver.Url = "http://localhost/litecart/";
            var newuser = driver.FindElement(By.XPath($"//*[@id='box-account-login']//a"));
            newuser.Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector("h1"))));

            RegistrationUser();
            UserLogout();
            UserLogin();
            UserLogout();

        }

        /// <summary>
        /// Рандомные юзеры
        /// </summary>
        /// <returns></returns>
        public string RandomUser()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        private string email;
        private string password;
        /// <summary>
        /// заполняем обязательные поля
        /// </summary>
        public void RegistrationUser()
        {
            
            var UserData = driver.FindElement(By.Name("firstname"));
            UserData.SendKeys(RandomUser());
            UserData = driver.FindElement(By.Name("lastname"));
            UserData.SendKeys(RandomUser());
            UserData = driver.FindElement(By.Name("address1"));
            UserData.SendKeys(RandomUser());
            UserData = driver.FindElement(By.Name("postcode"));
            UserData.SendKeys("660118");
            UserData = driver.FindElement(By.Name("city"));
            UserData.SendKeys(RandomUser());
            UserData = driver.FindElement(By.Name("email"));
            email = RandomUser() + "@yandex.ru";
            UserData.SendKeys(email);
            UserData = driver.FindElement(By.Name("phone"));
            UserData.SendKeys("9232880000");
            UserData = driver.FindElement(By.Name("password"));
            password = RandomUser();
            UserData.SendKeys(password);
            UserData = driver.FindElement(By.Name("confirmed_password"));
            UserData.SendKeys(password);
            UserData = driver.FindElement(By.Name("create_account"));
            UserData.Click();
        }

        /// <summary>
        /// User Logout
        /// </summary>
        public void UserLogout()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Id("box-account"))));
            var logout = driver.FindElements(By.CssSelector("[id=box-account] a"))[3];
            logout.Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector("[id=box-account-login] a"))));
        }

        /// <summary>
        /// User Login
        /// </summary>
        public void UserLogin()
        {
            var logindata = driver.FindElement(By.CssSelector("input[type=text]"));
            logindata.SendKeys(email);
            logindata = driver.FindElement(By.CssSelector("input[type=password]"));
            logindata.SendKeys(password);
            logindata = driver.FindElement(By.Name("login"));
            logindata.Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Id("box-account"))));
        }

    }
}
