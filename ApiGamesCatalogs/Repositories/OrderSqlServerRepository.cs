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
            var command = $"delete from OrdersItem where OrderId = '{id}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();

            command = $"delete from Orders where Id = '{id}'";

            await sqlConnection.OpenAsync();
            sqlCommand = new SqlCommand(command, sqlConnection);
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
            var games = new List<OrderItem>();


            var command = $"insert Orders (Id, Username) values ('{order.Id}', '{order.Username}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
            
            
            foreach(var i in order.GamesId)
            {
                var orderitem = new OrderItem();
                orderitem.Id = Guid.NewGuid();
                orderitem.OrderId = order.Id;
                orderitem.GameId = i;



                command = $"insert OrdersItem (Id, OrderId, GameId) values ('{orderitem.Id}', '{orderitem.OrderId}', '{orderitem.GameId}')";
                await sqlConnection.OpenAsync();
                sqlCommand = new SqlCommand(command, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                await sqlConnection.CloseAsync();
            }
        }

        public async Task<List<Order>> Obtain(int page, int quantity)
        {
            var orders = new List<Order>();
            var aux1 = new List<Guid>();

            var command = $"select * from Orders order by id offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                
                aux1.Add((Guid)sqlDataReader["Id"]);

                orders.Add(new Order
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Username = (string)sqlDataReader["Username"]
                });
            }

            await sqlConnection.CloseAsync();

            int count = 0;
            foreach(var i in aux1)
            {
                var games = new List<Guid>();

                await sqlConnection.OpenAsync();

                command = $"select * from OrdersItem where OrderId = '{i}' order by id offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";
                sqlCommand = new SqlCommand(command, sqlConnection);
                sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                while (sqlDataReader.Read())
                {
                    games.Add((Guid)sqlDataReader["GameId"]);
                }

                foreach(var a in orders)
                {
                    if (a.Id == i)
                    {
                        a.GamesId = games;
                    }
                }

                await sqlConnection.CloseAsync();
                
            }

            return orders;
        }

        public async Task<Order> Obtain(Guid id)
        {
            Order order = null;
            var aux = new List<Guid>();

            var command = $"select * from OrdersItem where OrderId = '{id}'";

            order = new Order
            {
                Id = id
                //Username = (string)sqlDataReader["Username"],
            };

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            

            while (sqlDataReader.Read())
            {
                aux.Add((Guid)sqlDataReader["Id"]);
            }

            //order.Username = (string)sqlDataReader["Username"];
            order.GamesId = aux;

            await sqlConnection.CloseAsync();

            await sqlConnection.OpenAsync();

            command = $"select * from Orders where Id = '{id}'";
            sqlCommand = new SqlCommand(command, sqlConnection);
            sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                var aux1 = (string)sqlDataReader["Username"];
                order.Username = aux1;
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
