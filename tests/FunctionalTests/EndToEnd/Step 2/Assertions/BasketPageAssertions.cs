using System;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.eShopWeb.FunctionalTests.EndToEnd.Pages;
using Microsoft.eShopWeb.FunctionalTests.EndToEnd.ViewModels;
using Microsoft.eShopWeb.Web.Pages.Basket;
using Microsoft.eShopWeb.Web.ViewModels;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.FunctionalTests.EndToEnd.Step_2.Assertions
{

    public class BasketPageAssertions : PageAssertions<BasketPage, BasketPageAssertions>
    {
        public BasketPageAssertions(BasketPage page) : base(page) { }

        public AndConstraint<BasketPageAssertions> OnlyHave(BasketItemViewModel basketItem)
        {
            var actualbasketItems = Subject.Items;

            Execute
               .Assertion
               .ForCondition(actualbasketItems.Single(a => AreEqual(a, basketItem)) != null)
               .FailWith($"\r\nExpected basket with : " +
               $"\r\n{Display(basketItem)}, \r\n but was {Display(actualbasketItems)}");

            return new AndConstraint<BasketPageAssertions>(this);
        }

        private bool AreEqual(BasketItemViewModel actualItem, BasketItemViewModel expectedItem)
        {
            return
                 actualItem.Id == expectedItem.Id &&
                string.Equals(actualItem.ProductName, expectedItem.ProductName, StringComparison.InvariantCultureIgnoreCase);
        }

        private string Display(params BasketItemViewModel[] items)
        {
            return items.Length == 0 ?
                    "empty" :
                    ":\r\n" + string.Join(":\r\n\t-----------------\r\n", items.Select(a => Display(a)));
        }

        private string Display(BasketItemViewModel item)

        {
            return "\t" + JsonConvert.SerializeObject(item,
                                                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
