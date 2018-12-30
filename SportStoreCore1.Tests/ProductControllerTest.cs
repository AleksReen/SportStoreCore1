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
    public class ProductControllerTest
    {
        public Product[] ProductsTestList { get; set; } = new Product[]{
                new Product () { Name = "P1", Price = 950m, Category = "cat1" },
                new Product () { Name = "P2", Price = 150m, Category = "cat2" },
                new Product () { Name = "P3", Price = 250m, Category = "cat3" },
                new Product () { Name = "P4", Price = 150m, Category = "cat2" },
                new Product () { Name = "P5", Price = 350m, Category = "cat1" },
                new Product () { Name = "P6", Price = 450m, Category = "cat1" },
                new Product () { Name = "P7", Price = 550m, Category = "cat1" },
                new Product () { Name = "P8", Price = 650m, Category = "cat1" },
            };

        private ProductsListViewModel InitialSetupCanPagination(string category, int page)
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(ProductsTestList);

            var controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            var result = controller.List(category, page).ViewData.Model as ProductsListViewModel;

            return result;
        }

        [Fact]
        public  void Indicate_Selected_Category()
        {
            var selectedCategory = "cat2";
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(ProductsTestList);

            var navMenu = new NavigationMenuViewComponent(mock.Object);

            navMenu.ViewComponentContext = new ViewComponentContext()
            {
                ViewContext = new ViewContext()
                {
                    RouteData = new RouteData()
                }
            };

            navMenu.RouteData.Values["category"] = selectedCategory;

            var result = (navMenu.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"] as string;

            Assert.Equal(selectedCategory, result);


        }

        [Fact]
        public void Can_Select_Category()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(ProductsTestList);

            var navMenu = new NavigationMenuViewComponent(mock.Object);

            var result = (navMenu.Invoke() as ViewViewComponentResult).ViewData.Model as IEnumerable<string>;

            Assert.True(Enumerable.SequenceEqual(new string[] { "cat1", "cat2", "cat3", }, result));

        }

        [Fact]
        public void Can_Filter_Category()
        {
            var result = InitialSetupCanPagination("cat2", 1);
            var products = result.Products.ToArray();
            var category = result.CurrentCategory;

            Assert.True(products.Length == 2);
            Assert.Equal("cat2", products[0].Category);
            Assert.Equal("cat2", products[1].Category);
            Assert.Equal("cat2", category);
        }

        [Fact]
        public void Can_Paginate_Test_View_Model_PagingInfo()
        {
            var result = InitialSetupCanPagination(null, 2);

            var pagingInfo = result.PagingInfo;

            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(8, pagingInfo.TotalItems);
            Assert.Equal(3, pagingInfo.TotalPages);
        }

        [Fact]
        public void Can_Paginate()
        {
            var result = InitialSetupCanPagination(null, 2);

            var prodArray = result.Products.ToArray();

            Assert.True(prodArray.Length == 3);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
            Assert.Equal("P6", prodArray[2].Name);
        }
    }
}
