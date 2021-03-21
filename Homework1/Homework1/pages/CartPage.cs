using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;

namespace Homework1.pages
{
    internal class CartPage : Page
    {
        internal CartPage(IWebDriver driver) : base(driver) { }
        public void RemoveAllCartItems()
        {
            wait.Until(wd => driver.FindElement(By.CssSelector("[name=confirm_order]")).Displayed);
            //  wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[name=confirm_order]")));

            //костыль, чтоб не крутились картинки
            wait.Until(wd => driver.FindElement(By.CssSelector(".shortcuts li a")).Displayed && driver.FindElement(By.CssSelector(".shortcuts li a")).Enabled);
            //  wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".shortcuts li a")));
            var inacts = driver.FindElements(By.CssSelector(".shortcuts li a"));
            foreach (var inact in inacts)
            {
                inact.Click();
                Thread.Sleep(500);
            }

            inacts[0].Click();

            wait.Until(wd => driver.FindElement(By.CssSelector("[name=remove_cart_item]")).Displayed);
            //  wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[name=remove_cart_item]")));

            var x1 = driver.FindElements(By.CssSelector(".sku"));
            int x2 = Int32.Parse(x1.Count().ToString());

            for (int i = 0; i < (x2 - 1); i++)
            {
                var table = driver.FindElement(By.CssSelector(".dataTable"));
                var t1 = driver.FindElements(By.Name("remove_cart_item"));
                driver.FindElements(By.Name("remove_cart_item"))[0].Click();
                wait.Until(ExpectedConditions.StalenessOf(table));
            }

        }

    }
}
