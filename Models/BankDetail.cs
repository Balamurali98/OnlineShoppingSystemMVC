using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OnlineShoppingSystem.Models
{
    public partial class BankDetail
    {
        public int BillId { get; set; }
        public string CardHoldername { get; set; }
        [NotMapped]
        [Required]
        [RegularExpression(@"[0-9]{16}", ErrorMessage = "Please enter Valid Card number")]
        public string CardNumber { get; set; }
        public string Cardtye { get; set; }
        public string Cvv { get; set; }
        public decimal? Balance { get; set; }
    }
}
