using BatteryApp.Data;
using BatteryApp.Models.BatteryModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.ChargeModel
{
    public class ChargeService : IChargeService
    {
        private readonly IDbContextFactory<AppDbContextFactory> _contextFactory;

        public ChargeService(IDbContextFactory<AppDbContextFactory> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Charge>> Get()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Charges.ToListAsync();
        }

        public async Task<Charge> Get(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var charge = await context.Charges.FindAsync(id);
            return charge;
        }

        public async Task<List<Charge>> Get(Battery battery)
        {
            using var context = _contextFactory.CreateDbContext();
            var charges = await context.Charges.ToListAsync();
            return charges.Where(x => x.BatteryId == battery.Id).ToList();
        }

        public async Task<Charge> Add(Charge charge)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Charges.Add(charge);
            await context.SaveChangesAsync();
            return charge;
        }

        public async Task<Charge> Update(Charge charge)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(charge).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return charge;
        }

        public async Task Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var charge = await context.Charges.FindAsync(id);
            context.Charges.Remove(charge);
            await context.SaveChangesAsync();
        }

    }
}
