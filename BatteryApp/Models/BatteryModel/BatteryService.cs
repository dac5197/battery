using BatteryApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.BatteryModel
{
    public class BatteryService : IBatteryService
    {
        private readonly IDbContextFactory<AppDbContextFactory> _contextFactory;

        public BatteryService(IDbContextFactory<AppDbContextFactory> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Battery>> Get()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Batteries.ToListAsync();
            }
        }

        public async Task<List<Battery>> Get(string userId)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var batteries = await context.Batteries.ToListAsync();
                return batteries.Where(x => x.OwnerId == userId).ToList();
            }
        }

        public async Task<Battery> Get(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var battery = await context.Batteries.FindAsync(id);
                return battery;
            }
        }

        public async Task<Battery> Add(Battery Battery)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Batteries.Add(Battery);
                await context.SaveChangesAsync();
                return Battery;
            }
        }

        public async Task<Battery> Update(Battery Battery)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Entry(Battery).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Battery;
            }
        }

        public async Task Delete(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var Battery = await context.Batteries.FindAsync(id);
                context.Batteries.Remove(Battery);
                await context.SaveChangesAsync();
            }
        }
    }
}
