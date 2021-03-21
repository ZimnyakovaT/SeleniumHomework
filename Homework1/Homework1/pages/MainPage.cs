using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1.pages
{
    internal class MainPage : Page
    {
        internal MainPage(IWebDriver driver) : base(driver) { }

        public void GoToProduct()
        {
            wait.Until(wd => driver.FindElement(By.CssSelector(".general-0")).Displayed && driver.FindElement(By.CssSelector(".general-0")).Enabled);
            //  wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector(".general-0"))));
            driver.FindElements(By.CssSelector("li a.link"))[0].Click();
        }

        public void OpenMainPage()
        {
            driver.Url = "http://localhost/litecart/";
            wait.Until(wd => driver.FindElement(By.CssSelector(".general-0")).Displayed && driver.FindElement(By.CssSelector(".general-0")).Enabled);
        }

    }
}
