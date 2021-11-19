using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace OnlineShoppingSystem.Models
{
    public partial class AdminLogin
    {
        public string AdminId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string AdminPassword { get; set; }
    }
}
