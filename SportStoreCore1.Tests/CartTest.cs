using SportStoreCore1.Models;
using System.Linq;
using Xunit;

namespace SportStoreCore1.Tests
{
    public class CartTest
    {
        [Fact]
        public void Can_Add_CartLine()
        {
            var p1 = new Product() { ProductID = 1, Name = "P1" };
            var p2 = new Product() { ProductID = 2, Name = "P2" };

            var target = new Cart();

            target.AddOrUpdateCartLine(p1, 5);
            target.AddOrUpdateCartLine(p2, 3);

            var result = target.Lines.ToArray();

            Assert.Equal(2, result.Length);
            Assert.Equal("P1", result[0].Product.Name);
            Assert.Equal("P2", result[1].Product.Name);
            Assert.Equal(5, result[0].Quantity);
            Assert.Equal(3, result[1].Quantity);
        }

        [Fact]
        public void Can_Update_CartLine_Quantity()
        {
            var p1 = new Product() { ProductID = 1, Name = "P1" };
            var p2 = new Product() { ProductID = 2, Name = "P2" };

            var target = new Cart();

            target.AddOrUpdateCartLine(p1, 5);
            target.AddOrUpdateCartLine(p2, 3);
            target.AddOrUpdateCartLine(p1, 5);

            var result = target.Lines.ToArray();

            Assert.Equal(2, result.Length);
            Assert.Equal(10, result[0].Quantity);
            Assert.Equal(3, result[1].Quantity);
        }

        [Fact]
        public void Can_Remove_CartLine()
        {
            var p1 = new Product() { ProductID = 1, Name = "P1" };
            var p2 = new Product() { ProductID = 2, Name = "P2" };
            var p3 = new Product() { ProductID = 3, Name = "P3" };

            var target = new Cart();

            target.AddOrUpdateCartLine(p1, 5);
            target.AddOrUpdateCartLine(p2, 3);
            target.AddOrUpdateCartLine(p3, 5);

            target.RemoveLine(p2);

            var result = target.Lines.ToArray();

            Assert.Equal(2, result.Length);
            Assert.Equal("P1", result[0].Product.Name);
            Assert.Equal("P3", result[1].Product.Name);
        }

        [Fact]
        public void Can_Total_CartLines_Values()
        {
            var p1 = new Product() { ProductID = 1, Name = "P1", Price = 100 };
            var p2 = new Product() { ProductID = 2, Name = "P2", Price = 200 };
            var p3 = new Product() { ProductID = 3, Name = "P3", Price = 300 };

            var target = new Cart();

            target.AddOrUpdateCartLine(p1, 1);
            target.AddOrUpdateCartLine(p2, 1);
            target.AddOrUpdateCartLine(p3, 1);

            var result = target.CompleteTotalValue();

            Assert.Equal(600, result);
        }

        [Fact]
        public void Can_Clear_CartLines ()
        {
            var p1 = new Product() { ProductID = 1, Name = "P1", Price = 100 };
            var p2 = new Product() { ProductID = 2, Name = "P2", Price = 200 };
            var p3 = new Product() { ProductID = 3, Name = "P3", Price = 300 };

            var target = new Cart();

            target.AddOrUpdateCartLine(p1, 1);
            target.AddOrUpdateCartLine(p2, 1);
            target.AddOrUpdateCartLine(p3, 1);

            target.ClearCart();

            var result = target.Lines.ToArray();

            Assert.True(result.Count() == 0);
        }
    }
}
