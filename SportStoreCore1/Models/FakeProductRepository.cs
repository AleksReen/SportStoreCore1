using SportStoreCore1.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStoreCore1.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Products => new Product[]
        {
            new Product () { Name = "Board", Description = "lorems lorems lorems lorems lorems lorems", Price = 25M},
            new Product () { Name = "Kayak", Description = "lorems lorems lorems lorems lorems lorems", Price = 100M},
            new Product () { Name = "BattleShip", Description = "lorems lorems lorems lorems lorems lorems", Price = 150M}
        };
    }
}
