using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackjack21Zhilin.Models
{
    public class CurrentViewModel
    {       
        public List<PlayCard> PlayerHand { get; set; }
        public List<PlayCard> PlayCards { get; set; }
        public List<PlayCard> DealerHand { get; set; }
        public Player Player { get; set; }
        public int PlayerValue { get; set; }
        public int DealerValue { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public int Ties { get; set; }
        public int Bet { get; set; }
        public string Message { get; set; }
        public bool DisablePlayQuit { get; set; }
        public bool DisableHitStand { get; set; }
    }
}
