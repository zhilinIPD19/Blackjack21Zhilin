using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blackjack21Zhilin.Models
{
    public class PlayCard
    {
        public int PlayCardId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Value { get; set; }
        [Required]
        public string PhotoPath { get; set; }
        public bool IsDistributed { get; set; }
        [NotMapped]
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
   
}
