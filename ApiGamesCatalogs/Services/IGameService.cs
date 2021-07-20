using ApiGamesCatalogs.InputModel;
using ApiGamesCatalogs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> Obtain(int page, int quantity);
        Task<GameViewModel> Obtain(Guid id);
        Task<GameViewModel> Insert(GameInputModel game);
        Task Update(Guid id, GameInputModel game);
        Task Update(Guid id, double price);
        Task Remove(Guid id);
    }
}
