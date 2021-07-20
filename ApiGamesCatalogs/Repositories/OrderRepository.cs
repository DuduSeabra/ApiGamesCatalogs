using ApiGamesCatalogs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static Dictionary<Guid, Order> orders = new Dictionary<Guid, Order>();

        public Task Delete(Guid id)
        {
            orders.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Close connection with the data base
        }

        public Task insert(Order order)
        {
            orders.Add(order.Id, order);
            return Task.CompletedTask;
        }

        public Task<List<Order>> Obtain(int page, int quantity)
        {
            return Task.FromResult(orders.Values.Skip((page - 1) * quantity).Take(quantity).ToList());
        }

        public Task<Order> Obtain(Guid id)
        {
            if (!orders.ContainsKey(id))
                return null;

            return Task.FromResult(orders[id]);
        }

        public Task<List<Order>> Obtain(string username)
        {
            return Task.FromResult(orders.Values.Where(order => order.Username.Equals(username)).ToList());
        }
    }
}
