using ApiGamesCatalogs.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Obtain(int page, int quantity);
        Task<Game> Obtain(Guid id);
        Task<List<Game>> Obtain(string name, string producer);
        Task insert(Game game);
        Task Update(Game game);
        Task Delete(Guid id);
    }
}
