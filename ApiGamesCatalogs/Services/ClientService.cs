using ApiGamesCatalogs.Controllers.V1;
using ApiGamesCatalogs.Entities;
using ApiGamesCatalogs.Exceptions;
using ApiGamesCatalogs.InputModel;
using ApiGamesCatalogs.Repositories;
using ApiGamesCatalogs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public void Dispose()
        {
            _clientRepository?.Dispose();
        }

        public async Task<ClientViewModel> Insert(ClientInputModel client)
        {

            var clientEntity = await _clientRepository.Obtain(client.Username);

            if (clientEntity != null)
                throw new ClientAlreadyRegisteredException();

            var clientInsert = new Client
            {
                Username = client.Username,
                Password = client.Password,
            };

            await _clientRepository.insert(clientInsert);

            return new ClientViewModel
            {
                Username = clientInsert.Username,
                Password = client.Password
            };
        }

        public async Task<List<ClientViewModel>> Obtain(int page, int quantity)
        {
            var clients = await _clientRepository.Obtain(page, quantity);

            return clients.Select(client => new ClientViewModel
            {
                Username = client.Username,
                Password = client.Password
            }).ToList();
        }

        public async Task<ClientViewModel> Obtain(string username)
        {
            var client = await _clientRepository.Obtain(username);

            if (client == null)
                return null;

            return new ClientViewModel
            {
                Username = client.Username,
                Password = client.Password
            };
        }

        public async Task Remove(string username)
        {
            var client = await _clientRepository.Obtain(username);

            if (client == null)
                throw new ClientNotRegisteredException();

            await _clientRepository.Delete(username);
        }
    }
}
