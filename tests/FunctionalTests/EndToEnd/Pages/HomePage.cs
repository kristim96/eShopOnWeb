using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopWeb.FunctionalTests.EndToEnd.Pages
{
    public class HomePage : Page
    {
        public BasketPage AddToBasketByProductId(int productId)
        {
            return NavigateTo<BasketPage>(By.XPath($"//*[@type=\"hidden\"][@value={productId}]"), e => e.Submit());
        }

      
    }
}
