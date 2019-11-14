using Microsoft.eShopWeb.FunctionalTests.EndToEnd.Pages;

namespace Microsoft.eShopWeb.FunctionalTests.EndToEnd.Step_2.Assertions
{
    public static class ShouldExtensions
    {
        public static BasketPageAssertions Should(this BasketPage page)
        {
            return new BasketPageAssertions(page);
        }

    }
}
