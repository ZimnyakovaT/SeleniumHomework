using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using NUnit.Framework;
using System.Globalization;
using System.Threading;
using SeleniumExtras.WaitHelpers;

namespace Homework1.tests
{
    [TestFixture]
    [Parallelizable(scope: ParallelScope.All)]
    internal class AddAndRemoveProducts : TestBase
    {
        [Test, Order(3)]

        public void TestAddAndRemoveProducts()
        {
            app.mainPage.OpenMainPage();
            app.mainPage.GoToProduct();
            app.productPage.AddToCart();
            app.productPage.GoToMainPage();
            app.mainPage.GoToProduct();
            app.productPage.AddToCart();
            app.productPage.GoToMainPage();
            app.mainPage.GoToProduct();
            app.productPage.AddToCart();
            app.productPage.GoToCartPage();
            app.cartPage.RemoveAllCartItems();
        }


    }
}
