using ApiGamesCatalogs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private static Dictionary<string, Client> clients = new Dictionary<string, Client>();

        public Task Delete(string username)
        {
            clients.Remove(username);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Close connection with the data base
        }

        public Task insert(Client client)
        {
            clients.Add(client.Username, client);
            return Task.CompletedTask;
        }

        public Task<List<Client>> Obtain(int page, int quantity)
        {
            return Task.FromResult(clients.Values.Skip((page - 1) * quantity).Take(quantity).ToList());
        }

        public Task<Client> Obtain(string username)
        {
            if (!clients.ContainsKey(username))
                return null;

            return Task.FromResult(clients[username]);
        }
    }
}
