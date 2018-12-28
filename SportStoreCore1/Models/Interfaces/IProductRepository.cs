using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStoreCore1.Models.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
}
