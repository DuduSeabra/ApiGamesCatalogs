using ApiGamesCatalogs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.InputModel
{
    public class OrderInputModel
    {
        public string Username { get; set; }

        public List<Game> Games { get; set; }
        public Game Game;
    }
}
