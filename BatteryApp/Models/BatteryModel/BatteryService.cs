using BatteryApp.Data;
using BatteryApp.Internals;
using BatteryApp.Models.ChargeModel;
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
        private readonly IChargeService _chargeService;
        private readonly IDeleteChargeController _deleteChargeController;

        public BatteryService(IDbContextFactory<AppDbContextFactory> contextFactory, IChargeService chargeService, IDeleteChargeController deleteChargeController)
        {
            _contextFactory = contextFactory;
            _chargeService = chargeService;
            _deleteChargeController = deleteChargeController;
        }

        public async Task<List<Battery>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Batteries.ToListAsync();
        }

        public async Task<List<Battery>> GetAsync(string userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var batteries = await context.Batteries.ToListAsync();
            return batteries.Where(x => x.OwnerId == userId).ToList();
        }

        public async Task<List<Battery>> GetActiveAsync(string userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var batteries = await context.Batteries.ToListAsync();
            return batteries.Where(x => x.OwnerId == userId)
                            .Where(x => x.IsActive == true)
                            .ToList();
        }

        public async Task<Battery> GetAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var battery = await context.Batteries.FindAsync(id);
            return battery;
        }

        public async Task<int> GetCountAsync(string userId)
        {
            var batteries = await GetAsync(userId);
            return batteries.Count;
        }

        public async Task<Battery> AddAsync(Battery battery)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Batteries.Add(battery);
            await context.SaveChangesAsync();
            return battery;
        }

        public async Task<Battery> UpdateAsync(Battery battery)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(battery).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return battery;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var battery = await context.Batteries.FindAsync(id);

            await DeleteAllChargesAndRelatedItemsAsync(battery);

            context.Batteries.Remove(battery);
            await context.SaveChangesAsync();
        }

        private async Task DeleteAllChargesAndRelatedItemsAsync(Battery battery)
        {
            var charges = await _chargeService.GetAsync(battery);

            foreach (var charge in charges)
            {
                await _deleteChargeController.DeleteChargeAndAllRelatedItemsAsync(charge);
            }
        }
    }
}
