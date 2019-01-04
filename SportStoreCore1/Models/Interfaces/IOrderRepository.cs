using System.Linq;

namespace SportStoreCore1.Models.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
