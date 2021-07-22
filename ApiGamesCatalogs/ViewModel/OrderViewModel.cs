using ApiGamesCatalogs.Entities;
using ApiGamesCatalogs.InputModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.ViewModel
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public Client Client;

        public List<Guid> GamesId { get; set; }
    }
}
