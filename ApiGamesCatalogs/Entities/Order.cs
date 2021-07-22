using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
        public Client Client { get; set; }

        public List<Guid> GamesId { get; set; }
        public OrderItem OrderItem { get; set; }

    }
}
