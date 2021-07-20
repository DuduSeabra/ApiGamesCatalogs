using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Game name must be between 3 and 100 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Producer name must be between 3 and 100 characters")]
        public string Producer { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Game price must be between 3 and 100 dollars")]
        public double Price { get; set; }
    }
}
