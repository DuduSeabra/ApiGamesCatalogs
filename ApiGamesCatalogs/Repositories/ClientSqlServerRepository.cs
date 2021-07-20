using ApiGamesCatalogs.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Repositories
{
    public class ClientSqlServerRepository : IClientRepository
    {

        private readonly SqlConnection sqlConnection;

        public ClientSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task Delete(string username)
        {
            var command = $"delete from Clients where Username = '{username}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }

        public async Task insert(Client client)
        {
            var command = $"insert Clients (Username, Password) values ('{client.Username}', '{client.Password}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task<List<Client>> Obtain(int page, int quantity)
        {
            var clients = new List<Client>();

            var command = $"select * from Clients order by username offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                clients.Add(new Client
                {
                    Username = (string)sqlDataReader["Username"],
                    Password = (string)sqlDataReader["Password"],
                });
            }

            await sqlConnection.CloseAsync();

            return clients;
        }

        public async Task<Client> Obtain(string username)
        {
            Client client = null;

            var command = $"select * from Clients where Username = '{username}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                client = new Client
                {
                    Username = (string)sqlDataReader["Username"],
                    Password = (string)sqlDataReader["Password"],
                };
            }

            await sqlConnection.CloseAsync();

            return client;
        }
    }
}
