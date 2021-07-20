using ApiGamesCatalogs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Repositories
{
    public interface IClientRepository : IDisposable
    {
        Task<List<Client>> Obtain(int page, int quantity);
        Task<Client> Obtain(string username);
        Task insert(Client client);
        Task Delete(string username);
    }
}
