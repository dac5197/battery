using BatteryApp.Data;
using BatteryApp.Models.ChargeModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models
{
    public class BaseService
    {
        private readonly IDbContextFactory<AppDbContextFactory> _contextFactory;

        public BaseService(IDbContextFactory<AppDbContextFactory> contextFactory)
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

