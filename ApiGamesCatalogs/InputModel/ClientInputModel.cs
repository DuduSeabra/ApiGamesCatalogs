using System;
using System.ComponentModel.DataAnnotations;

namespace ApiGamesCatalogs.InputModel
{
    public class ClientInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters")]
        public string Username { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Password must be between 8 and 100 characters")]
        public string Password { get; set; }
    }
}