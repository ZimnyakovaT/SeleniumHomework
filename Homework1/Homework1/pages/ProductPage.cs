using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;

namespace Homework1.pages
{
    internal class ProductPage : Page
    {
        internal ProductPage(IWebDriver driver) : base(driver) { }
        private int counter_;

        public void AddToCart()
        {
            wait.Until(wd => driver.FindElement(By.CssSelector("h1")).Displayed && driver.FindElement(By.CssSelector("h1")).Enabled);
            //wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector("h1"))));

            //check  counter of products
            var counter = driver.FindElements(By.CssSelector(".quantity"))[0].GetAttribute("textContent");
            counter_ = Int32.Parse(counter);

            //if size is visible
            var isselectpresent = driver.FindElements(By.CssSelector("[name='options[Size]'"));
            if (isselectpresent.Count > 0)
            {
                var select = driver.FindElement(By.CssSelector("[name='options[Size]'"));
                select.Click();
                select = select.FindElements(By.CssSelector("option"))[2];
                select.Click();
            }

            //add to cart
            driver.FindElement(By.Name("add_cart_product")).Click();
            counter_++;
            wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.CssSelector(".quantity")), (counter_.ToString())));
        }

        public void GoToMainPage()
        {
            //goto main page
            driver.Navigate().Back();
        }

        public void GoToCartPage()
        {
            driver.FindElements(By.CssSelector(".link"))[0].Click();
        }
    }
}
