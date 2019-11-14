using Microsoft.eShopWeb.FunctionalTests.EndToEnd.ViewModels;
using Microsoft.eShopWeb.Web.Pages.Basket;
using Microsoft.eShopWeb.Web.ViewModels;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.eShopWeb.FunctionalTests.EndToEnd.Pages
{
    public class BasketPage : Page
    {
        private const string rowItemSelector = 
            "div.esh-catalog-items.row > article.esh-basket-items.row";

        private const string ExtractSerialisedBasketItem = @"JSON.stringify(
                                                                $('"+rowItemSelector+"')"+
                                                                    @".map(function(index,e){ 
                                                                        return { 
                                                                            ProductName: $(e).find('.col-xs-3').text(), 
                                                                            Id: $(e).find('.esh-basket-image').attr('src')
                                                                                    .substr(17).replace('.png','')
                                                                        } 
                                                                    }).toArray());";

        public int NumberOfItems =>
            WebDriver.FindElements(By.CssSelector(rowItemSelector)).Count();
 
        public BasketItemViewModel[] Items => 
            JsonConvert.DeserializeObject<BasketItemViewModel[]>(ExecuteScriptAndReturn<string>(ExtractSerialisedBasketItem));
    }
}