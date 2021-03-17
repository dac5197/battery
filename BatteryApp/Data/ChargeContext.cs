using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Data
{
    public class ChargeContext : DbContext
    {
        public ChargeContext(DbContextOptions<ChargeContext> options)
        : base(options)
        {
        }

        public DbSet<Charge> Charges { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
