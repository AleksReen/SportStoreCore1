﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportStoreCore1.Controllers;
using SportStoreCore1.Models;
using SportStoreCore1.Models.Interfaces;
using System.Collections.Generic;
using Xunit;


namespace SportStoreCore1.Tests
{
    public class AdminControllerTest
    {
        private Mock<IProductRepository> mock = new Mock<IProductRepository>();
        private Product[] productsTestData = new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                null
            };

        [Fact]
        public void Index_Contains_All_Products()
        {
            mock.Setup(o => o.Products).Returns(productsTestData);

            var target = new AdminController(mock.Object);

            var result = (target.Index() as ViewResult).ViewData.Model as Product [];

            Assert.True(4 == result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }

        [Fact]
        public void Can_Edit_Products()
        {
            mock.Setup(o => o.Products).Returns(productsTestData);

            var target = new AdminController(mock.Object);

            var listResult = new List<Product>();

            for (int i = 1; i <= productsTestData.Length; i++)
            {
                var result = (target.Edit(i) as ViewResult).ViewData.Model as Product;
                listResult.Add(result);
            }

            var results = listResult.ToArray();

            Assert.Equal(1, results[0].ProductID);
            Assert.Equal(2, results[1].ProductID);
            Assert.Equal(3, results[2].ProductID);
        }

        [Fact]
        public void Cannot_Edit_Nonexistent_Product()
        {
            mock.Setup(o => o.Products).Returns(productsTestData);

            var target = new AdminController(mock.Object);

            var result = (target.Edit(4) as ViewResult).ViewData.Model as Product;

            Assert.Null(result);
        }

        [Fact]
        public void Can_Delete_Valid_Products()
        {          
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(productsTestData);

            AdminController target = new AdminController(mock.Object);

            var result = target.Delete(2);

            mock.Verify(m => m.DeleteProduct(2));

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Can_Save_ValidChanges()
        {
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();

            var target = new AdminController(mock.Object) {
                TempData = tempData.Object
            };

            var testProduct = new Product() {
                Name = "Test"
            };

            var result = target.Edit(testProduct);

            mock.Verify(m => m.SaveProduct(testProduct));

            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);

        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();

            var target = new AdminController(mock.Object);
            target.ModelState.AddModelError("error", "error");

            var testProduct = new Product()
            {
                Name = "Test"
            };

            var result = target.Edit(testProduct);

            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());

            Assert.IsType<ViewResult>(result);
        }
    }
}
