using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blackjack21Zhilin.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [NotMapped]
        public int Amount { get; set; }
        public ICollection<PlayCard> PlayCards { get; set; }
    }
}
