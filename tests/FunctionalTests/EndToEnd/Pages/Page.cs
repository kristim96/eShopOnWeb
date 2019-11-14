using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopWeb.FunctionalTests.EndToEnd.Pages
{
    public abstract class Page
    {
        protected IWebDriver WebDriver { get; private set; }

        public string Title => WebDriver.Title;

        public string Url => WebDriver.Url;

        protected TPage NavigateTo<TPage>(By byLocator, Action<IWebElement> performAction = null)
            where TPage : Page, new()
        { 
            var action = performAction ?? (e => e.Click());
            action(WebDriver.FindElement(byLocator));

            return new TPage { WebDriver = WebDriver };
        } 

        internal static TPage NavigateToInitial<TPage>(IWebDriver webDriver, string startUpUrl)
             where TPage : Page, new()
        {
            if (webDriver == null) throw new ApplicationException("Please provide with an instance of web driver to proceed");
            if (string.IsNullOrWhiteSpace(startUpUrl)) throw new ApplicationException("Please provide with a start up url");

            webDriver.Navigate().GoToUrl(startUpUrl);

            return new TPage { WebDriver = webDriver };
        }

        protected TReturn ExecuteScriptAndReturn<TReturn>(string scriptToExecute)
        {
            var javascriptExecutor = (IJavaScriptExecutor)WebDriver;

            var untypedValue = javascriptExecutor.ExecuteScript($"return {scriptToExecute}");

            return (TReturn)Convert.ChangeType(untypedValue, typeof(TReturn));
        }
    }
}
