using SportStoreCore1.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStoreCore1.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDBContext context;

        public EFProductRepository(ApplicationDBContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Product> Products => context.Products;

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                var prod = context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);

                if (prod != null)
                {
                    prod.Name = product.Name;
                    prod.Category = product.Category;
                    prod.Description = product.Description;
                    prod.Price = product.Price;
                }
            }
            context.SaveChanges();
        }
    }
}
