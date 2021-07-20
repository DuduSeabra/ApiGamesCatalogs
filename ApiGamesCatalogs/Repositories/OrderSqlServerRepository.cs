using ApiGamesCatalogs.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Repositories
{
    public class OrderSqlServerRepository : IOrderRepository
    {
        private readonly SqlConnection sqlConnection;

        public OrderSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task Delete(Guid id)
        {
            var command = $"delete from Orders where Id = '{id}'";

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

        public async Task insert(Order order)
        {
            var command = $"insert Orders (Id, Username) values ('{order.Id}', '{order.Username}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task<List<Order>> Obtain(int page, int quantity)
        {
            var orders = new List<Order>();

            var command = $"select * from Orders order by id offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                orders.Add(new Order
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Username = (string)sqlDataReader["Username"]
                });
            }

            await sqlConnection.CloseAsync();

            return orders;
        }

        public async Task<Order> Obtain(Guid id)
        {
            Order order = null;

            var command = $"select * from Orders where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                order = new Order
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Username = (string)sqlDataReader["Username"],
                };
            }

            await sqlConnection.CloseAsync();

            return order;
        }

        public async Task<List<Order>> Obtain(string username)
        {
            var orders = new List<Order>();

            var command = $"select * from Orders where Username = '{username}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                orders.Add(new Order
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Username = (string)sqlDataReader["Username"]
                });
            }

            await sqlConnection.CloseAsync();

            return orders;
        }
    }
}
