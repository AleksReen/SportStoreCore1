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
    }
}
