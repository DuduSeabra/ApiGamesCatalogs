using ApiGamesCatalogs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Repositories
{
    public interface IOrderRepository : IDisposable
    {
        Task<List<Order>> Obtain(int page, int quantity);
        Task<Order> Obtain(Guid id);
        Task<List<Order>> Obtain(string username);
        Task insert(Order order);
        Task Delete(Guid id);
    }
}
