using Microsoft.eShopWeb.FunctionalTests.EndToEnd.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace Microsoft.eShopWeb.FunctionalTests.EndToEnd.Configuration
{
    public class BrowserHost : IDisposable
    {
        private IWebDriver _webdriver;

        public string BaseUrl { get; private set; }


        private BrowserHost(IWebDriver webDriver)
        {
            _webdriver = webDriver;
            BaseUrl = Environment.GetEnvironmentVariable("URL") ?? "http://localhost:5106/";
            AppDomain.CurrentDomain.DomainUnload += CurrentDomainDomainUnload;
            _webdriver.Manage().Window.Maximize();
        }

        private void CurrentDomainDomainUnload(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.DomainUnload -= CurrentDomainDomainUnload;
            Dispose();
        }

        public TPage NavigateToInitial<TPage>(string relativeUrl = null)
          where TPage : Page, new()
        {

            var url = string.IsNullOrWhiteSpace(relativeUrl) ? BaseUrl : (BaseUrl + relativeUrl).Replace("//", "/");
            
            return Page.NavigateToInitial<TPage>(_webdriver, url);
        }

        public static BrowserHost Chrome()
        {
            var options = new ChromeOptions();
            options.AddArgument("test-type");

            var directory = Environment.GetEnvironmentVariable("ChromeWebDriver") ?? Directory.GetCurrentDirectory(); 

            return new BrowserHost(new ChromeDriver(directory, options));
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_webdriver != null)
                    {
                        _webdriver.Quit();
                        _webdriver.Dispose();
                        _webdriver = null;
                    }
                }
                disposedValue = true;
            }
        }


        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

    }
}
