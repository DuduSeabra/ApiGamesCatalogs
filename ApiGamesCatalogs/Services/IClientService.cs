using ApiGamesCatalogs.InputModel;
using ApiGamesCatalogs.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Services
{
    public interface IClientService
    {
        Task<List<ClientViewModel>> Obtain(int page, int quantity);
        Task<ClientViewModel> Obtain(string username);
        Task<ClientViewModel> Insert(ClientInputModel client);
        Task Remove(string username);
    }
}