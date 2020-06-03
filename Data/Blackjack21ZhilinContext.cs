using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blackjack21Zhilin.Models;

namespace Blackjack21Zhilin.Data
{
    public class Blackjack21ZhilinContext : DbContext
    {
        public Blackjack21ZhilinContext (DbContextOptions<Blackjack21ZhilinContext> options)
            : base(options)
        {
        }

        public DbSet<Blackjack21Zhilin.Models.Player> Player { get; set; }

        public DbSet<Blackjack21Zhilin.Models.PlayCard> PlayCard { get; set; }

    }
}
