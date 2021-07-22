using ApiGamesCatalogs.Entities;
using ApiGamesCatalogs.InputModel;
using ApiGamesCatalogs.Repositories;
using ApiGamesCatalogs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Services
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Dispose()
        {
            _orderRepository?.Dispose();
        }

        public async Task<OrderViewModel> Insert(OrderInputModel order)
        {
            var orderInsert = new Order
            {
                Id = Guid.NewGuid(),
                Username = order.Username,
                OrderId = order.GamesId
            };

            await _orderRepository.insert(orderInsert);

            return new OrderViewModel
            {
                Id = orderInsert.Id,
                Username = order.Username,
                OrderId = order.GamesId
            };
        }

        public async Task<List<OrderViewModel>> Obtain(int page, int quantity)
        {
            var orders = await _orderRepository.Obtain(page, quantity);

            return orders.Select(order => new OrderViewModel
            {
                Id = order.Id,
                Username = order.Username,
                OrderId = order.OrderId
            }).ToList();
        }

        public async Task<OrderViewModel> Obtain(Guid id)
        {
            var order = await _orderRepository.Obtain(id);

            if (order == null)
                return null;

            return new OrderViewModel
            {
                Id = order.Id,
                Username = order.Username,
                OrderId = order.OrderId
            };
        }

        public async Task Remove(Guid id)
        {
            /*var order = await _orderRepository.Obtain(id);

            if (order == null)
                throw new OrderNotRegisteredException();*/

            await _orderRepository.Delete(id);
        }
    }
}
