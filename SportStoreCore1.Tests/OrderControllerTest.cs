using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportStoreCore1.Components;
using SportStoreCore1.Controllers;
using SportStoreCore1.Models;
using SportStoreCore1.Models.Interfaces;
using SportStoreCore1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportStoreCore1.Tests
{
    public class OrderControllerTest
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            var order = new Order();

            var target = new OrderController(mock.Object, cart);

            var result = target.Checkout(order) as ViewResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()),Times.Never);

            Assert.True(string.IsNullOrEmpty(result.ViewName));

            Assert.False(result.ViewData.ModelState.IsValid);

        }

        [Fact]
        public void Cannot_Checkout_Invalid_Order()
        {
            var mock = new Mock<IOrderRepository>();

            var cart = new Cart();
            cart.AddOrUpdateCartLine(new Product(), 1);

            var target = new OrderController(mock.Object, cart);
            target.ModelState.AddModelError("error", "error");

            var result = target.Checkout(new Order()) as ViewResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.True(string.IsNullOrEmpty(result.ViewName));

            Assert.False(result.ViewData.ModelState.IsValid);

        }

        [Fact]
        public void Can_Check_And_Submit_Order()
        {
            var mock = new Mock<IOrderRepository>();

            var cart = new Cart();
            cart.AddOrUpdateCartLine(new Product(), 1);

            var target = new OrderController(mock.Object, cart);

            var result = target.Checkout(new Order()) as RedirectToActionResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            Assert.Equal("Completed", result.ActionName);

        }
    }
}
