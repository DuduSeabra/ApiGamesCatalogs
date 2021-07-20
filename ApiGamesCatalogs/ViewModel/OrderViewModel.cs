using ApiGamesCatalogs.Entities;
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
        public List<Game> Games { get; set; }
    }
}
