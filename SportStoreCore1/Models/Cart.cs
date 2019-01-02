using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStoreCore1.Models
{
    public class Cart
    {

        private List<CartLine> cartLines = new List<CartLine>();

        public virtual void AddOrUpdateCartLine(Product product, int quantity)
        {
            var cartLine = GetCartLine(product);

            if (cartLine == null)
            {
                cartLines.Add(CreateNewCartLine(product, quantity));
            }
            else
            {
                UpdateCurrentCartLineQuantity(cartLine, quantity);
            }
        }

        private void UpdateCurrentCartLineQuantity(CartLine cartLine, int quantity) => cartLine.Quantity += quantity;

        private CartLine CreateNewCartLine(Product product, int quantity) =>  
            new CartLine()
            {
                Product = product,
                Quantity = quantity
            };

        private CartLine GetCartLine(Product product) => 
            cartLines.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();

        public virtual void RemoveLine(Product product) =>
            cartLines.RemoveAll(p => product.ProductID == p.Product.ProductID);

        public virtual decimal CompleteTotalValue() =>
            cartLines.Sum(s => s.Product.Price * s.Quantity);

        public virtual void ClearCart() =>
            cartLines.Clear();

        public virtual IEnumerable<CartLine> Lines =>
            cartLines;

        public class CartLine
        {          
            public int CartLineId { get; set; }
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}
