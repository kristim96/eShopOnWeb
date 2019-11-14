using Microsoft.eShopWeb.FunctionalTests.EndToEnd.Configuration;
using Microsoft.eShopWeb.FunctionalTests.EndToEnd.Pages;
using Microsoft.eShopWeb.FunctionalTests.EndToEnd.Step_2.Assertions;
using Microsoft.eShopWeb.Web.Pages.Basket;
using System;
using TestStack.BDDfy;
using Xunit;


namespace Microsoft.eShopWeb.FunctionalTests.EndToEnd.Step_3
{
    [Trait("Category","EndToEnd")]
    [Story(
       AsA = "As an Customer",
       IWant = "I want to add an item to the basket",
       SoThat = "So that I can place an online order")]
    public class AddingItemsToBasket : IDisposable
    {
        private HomePage _homePage;
        private BasketItemViewModel _expectedDotNetBlackAndWhiteMug;
        private BasketPage _actualPage;

        public BrowserHost Browser { get; private set; }

        public AddingItemsToBasket()
        {
            Browser = BrowserHost.Chrome();
        }

        [Fact]
        public void Can_add_selected_item_basket()
        {
            this.BDDfy();
        }

        void Given_the_Basket_Is_Empty()
        {
            _homePage = Browser.NavigateToInitial<HomePage>("https://eshopwebapp9090.azurewebsites.net/");
        }

        void AndGiven_the_DotNet_Black_And_White_Mug_IsSelected()
        {
            //Arrange
            _expectedDotNetBlackAndWhiteMug = new BasketItemViewModel { Id = 2, ProductName = ".NET Black & White Mug" };
        }

        void When_adding_the_selected_item_to_the_basket()
        {
           _actualPage = _homePage.AddToBasketByProductId(_expectedDotNetBlackAndWhiteMug.Id);
        }

        void Then_Should_Be_Redirected_To_The_Basket_Content()
        {
            _actualPage
               .Should()
               .Be<BasketPage>(withExpectedTitle: "Basket - Microsoft.eShopOnWeb")
                              .And
                              .HaveUrlEndingWith("/Basket");
        }

        void AndThen_the_Basket_Should_Contain_DotNet_Black_And_White_Mug_IsSelected()
        {
            _actualPage
                .Should()
                .OnlyHave(_expectedDotNetBlackAndWhiteMug);
        }

        public void Dispose()
        {
            Browser.Dispose();
            Browser = null;
        }
    }

}
