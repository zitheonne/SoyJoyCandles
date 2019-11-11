using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoyJoyCandles.Models;

namespace SoyJoyCandles.Models
{
    public class SoyJoyCandlesContext : DbContext
    {
        public SoyJoyCandlesContext (DbContextOptions<SoyJoyCandlesContext> options)
            : base(options)
        {
        }

        public DbSet<SoyJoyCandles.Models.SoyJoyCandlesShop> SoyJoyCandlesShop { get; set; }

        public DbSet<SoyJoyCandles.Models.CandleEntity> CandleEntity { get; set; }
    }
}
