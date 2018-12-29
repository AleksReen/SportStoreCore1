using Moq;
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
                new Product () { Name = "P1", Price = 950m },
                new Product () { Name = "P2", Price = 150m },
                new Product () { Name = "P3", Price = 250m },
                new Product () { Name = "P4", Price = 150m },
                new Product () { Name = "P5", Price = 350m },
                new Product () { Name = "P6", Price = 450m },
                new Product () { Name = "P7", Price = 550m },
                new Product () { Name = "P8", Price = 650m },
            };

        private ProductsListViewModel InitialSetupCanPagination()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(ProductsTestList);

            var controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            var result = controller.List(2).ViewData.Model as ProductsListViewModel;

            return result;
        }

        [Fact]
        public void Can_Paginate_Test_View_Model_PagingInfo()
        {
            var result = InitialSetupCanPagination();

            var pagingInfo = result.PagingInfo;

            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(8, pagingInfo.TotalItems);
            Assert.Equal(3, pagingInfo.TotalPages);
        }

        [Fact]
        public void Can_Paginate()
        {
            var result = InitialSetupCanPagination();

            var prodArray = result.Products.ToArray();

            Assert.True(prodArray.Length == 3);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
            Assert.Equal("P6", prodArray[2].Name);
        }
    }
}
