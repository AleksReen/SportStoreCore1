using Microsoft.EntityFrameworkCore;
using SportStoreCore1.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStoreCore1.Models
{
    public class EFOrderReposotory : IOrderRepository
    {
        private ApplicationDBContext context;

        public EFOrderReposotory(ApplicationDBContext con)
        {
            context = con;
        }

        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderId == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
