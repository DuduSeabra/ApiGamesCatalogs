using ApiGamesCatalogs.InputModel;
using ApiGamesCatalogs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Services
{
    public interface IOrderService : IDisposable
    {
        Task<List<OrderViewModel>> Obtain(int page, int quantity);
        Task<OrderViewModel> Obtain(Guid id);
        Task<OrderViewModel> Insert(OrderInputModel order);
        Task Remove(Guid id);
    }
}
