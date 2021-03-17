using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Data
{
    public class ChargeService : IChargeService
    {
        private readonly IDbContextFactory<ChargeContext> _contextFactory;

        public ChargeService(IDbContextFactory<ChargeContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Charge>> Get()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Charges.ToListAsync();
            }
        }

        public async Task<Charge> Get(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var charge = await context.Charges.FindAsync(id);
                return charge;
            }
        }

        public async Task<Charge> Add(Charge charge)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Charges.Add(charge);
                await context.SaveChangesAsync();
                return charge;
            }
        }

        public async Task<Charge> Update(Charge charge)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Entry(charge).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return charge;
            }
        }

        public async Task Delete(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var charge = await context.Charges.FindAsync(id);
                context.Charges.Remove(charge);
                await context.SaveChangesAsync();
            }
        }

    }
}
