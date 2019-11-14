using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.EndToEnd
{
    [Trait("Category", "Brittle")]

    public class BrittleTests : IDisposable
    {
        private IWebDriver _browser;

        public BrittleTests()
        {
            var options = new ChromeOptions();
            options.AddArgument("test-type");

            var directory = Environment.GetEnvironmentVariable("ChromeWebDriver");
            //var directory = new ChromeDriver(Directory.GetCurrentDirectory(), options); 

            _browser = new ChromeDriver(directory, options);
            _browser.Manage().Window.Maximize();
        }

      
        [Fact (Skip = "Ignore")]
        public void LoggedIn_User_Can_buy_a_cup_of_T()
        {
            _browser.Navigate().GoToUrl("https://eshopwebapp9090.azurewebsites.net/");
            var type = new SelectElement(_browser.FindElement(By.Id("CatalogModel_TypesFilterApplied")));
            type.SelectByText("Mug");
            _browser.FindElement(By.CssSelector("body > div > section.esh-catalog-filters > div > form > input")).Click();

            _browser.FindElement(By.CssSelector("body > div > div > div.esh-catalog-items.row > div:nth-child(2) > form > input.esh-catalog-button")).Click();

            _browser.FindElement(By.CssSelector("body > div > div > form > div > div.row > section.esh-basket-item.col-xs-push-7.col-xs-4 > input")).Click();

            _browser.FindElement(By.Id("Input_Email")).SendKeys("demouser@microsoft.com");
            _browser.FindElement(By.Id("Input_Password")).SendKeys("Pass@word1");
            _browser.FindElement(By.CssSelector("body > div > div > div > div > section > form > div:nth-child(6) > button")).Click();

            _browser.FindElement(By.CssSelector("#logoutForm > section.esh-identity-section > div")).Click();
            _browser.FindElement(By.CssSelector("#logoutForm > section.esh-identity-drop > a:nth-child(1)")).Click();

            //this test will fail sometimes because the list of orders is not yet available.
            _browser.FindElement(By.CssSelector(".esh-orders-link")).Click();

            Assert.Equal(".NET Black & White Mug", _browser.FindElement(By.CssSelector(".esh-orders-detail-item--middle")).Text);
        }

        public void Dispose()
        {
            _browser.Quit();
            _browser.Dispose();
            _browser = null;
        }
    }
}
