﻿using Microsoft.eShopWeb.FunctionalTests.EndToEnd.Configuration;
using Microsoft.eShopWeb.FunctionalTests.EndToEnd.Pages;
using System.Linq;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.EndToEnd.Step_1
{
    [Trait("Category","EndToEnd")]
    public class AddingToBasketAndCheckingOutTests : Specs
    {
        public AddingToBasketAndCheckingOutTests() : base(BrowserHost.Chrome) { }
        [Fact]
        public void Can_add_selected_item_basket()
        {
            //Arrange
            const int doNetBlackMugProductId = 2;
            var homePage = Browser.NavigateToInitial<HomePage>();

            //Act
            var result = homePage.AddToBasketByProductId(doNetBlackMugProductId);

            //Assert
            var actualPage = Assert.IsType<BasketPage>(result);
            Assert.EndsWith("/Basket", actualPage.Url);
            Assert.Equal("Basket - Microsoft.eShopOnWeb", actualPage.Title);
            Assert.Equal(1, actualPage.NumberOfItems );
            Assert.Equal(doNetBlackMugProductId, actualPage.Items.Single().Id);
            Assert.Equal(".NET Black & White Mug", actualPage.Items.Single().ProductName);
        }

    }
}
