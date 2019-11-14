using Microsoft.eShopWeb.FunctionalTests.EndToEnd.Configuration;
using Microsoft.eShopWeb.FunctionalTests.EndToEnd.Pages;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.EndToEnd.Step_1
{
    [Trait("Category","EndToEnd")]
    public class AddingToBasketAndCheckingOutTests : Specs
    {
        public AddingToBasketAndCheckingOutTests() : base(BrowserHost.Chrome) { }
        // a comment
        [Fact]
        public void Can_add_selected_item_basket()
        {
            //Arrange
            var homePage = Browser.NavigateToInitial<HomePage>("https://eshopwebappdemo-demo.azurewebsites.net/");

            //Act
            var result = homePage.AddToBasketByProductId(2);

            //Assert
            var actualPage = Assert.IsType<BasketPage>(result);
            Assert.EndsWith("/Basket", actualPage.Url);
            Assert.Equal("Basket - Microsoft.eShopOnWeb", actualPage.Title);
            Assert.Equal(1, actualPage.NumberOfItems );
        }

    }
}
