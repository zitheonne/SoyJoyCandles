using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SoyJoyCandles.Models
{
    public class SoyJoyCandlesContext : DbContext
    {
        public SoyJoyCandlesContext (DbContextOptions<SoyJoyCandlesContext> options)
            : base(options)
        {
        }

        public DbSet<SoyJoyCandles.Models.SoyJoyCandlesShop> SoyJoyCandlesShop { get; set; }
    }
}
