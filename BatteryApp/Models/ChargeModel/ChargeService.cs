using BatteryApp.Data;
using BatteryApp.Internals;
using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.NoteModel;
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
        private readonly IChargeLifecycle _chargeLifecycle;
        private readonly INoteService _noteService;

        public ChargeService(IDbContextFactory<AppDbContextFactory> contextFactory, IChargeLifecycle chargeLifecycle, INoteService noteService)
        {
            _contextFactory = contextFactory;
            _chargeLifecycle = chargeLifecycle;
            _noteService = noteService;
        }

        public async Task<List<Charge>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Charges.ToListAsync();
        }

        public async Task<Charge> GetAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var charge = await context.Charges.FindAsync(id);
            return charge;
        }

        public async Task<List<Charge>> GetAsync(string userId)
        {
            var context = _contextFactory.CreateDbContext();
            var charges = await context.Charges.ToListAsync();
            return charges.Where(x => x.OwnerId == userId).ToList();
        }

        public async Task<List<Charge>> GetAsync(Battery battery)
        {
            using var context = _contextFactory.CreateDbContext();
            var charges = await context.Charges.ToListAsync();
            return charges.Where(x => x.BatteryId == battery.Id).ToList();
        }

        public async Task<List<Charge>> GetActiveAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var charges = await context.Charges.ToListAsync();
            return charges.Where(x => x.Completed is null || x.Completed >= DateTime.UtcNow.AddDays(-3)).ToList();
        }

        public async Task<List<Charge>> GetActiveAsync(Battery battery)
        {
            using var context = _contextFactory.CreateDbContext();
            var charges = await context.Charges.ToListAsync();
            return charges.Where(x => x.BatteryId == battery.Id && (x.Completed is null || x.Completed >= DateTime.UtcNow.AddDays(-3))).ToList();
        }

        public async Task<List<Charge>> GetActiveAsync(string userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var charges = await context.Charges.ToListAsync();
            return charges.Where(x => x.OwnerId == userId && (x.Completed is null || x.Completed >= DateTime.UtcNow.AddDays(-3))).ToList();
        }

        public async Task<List<Charge>> GetActiveParentsOnlyAsync(Battery battery)
        {
            using var context = _contextFactory.CreateDbContext();
            var charges = await context.Charges.ToListAsync();
            return charges.Where(x => x.BatteryId == battery.Id)
                          .Where(x => x.Completed is null || x.Completed >= DateTime.UtcNow.AddDays(-3))
                          .Where(x => x.ParentId is null)          
                          .ToList();
        }

        public async Task<List<Charge>> GetActiveParentsOnlyAsync(string userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var charges = await context.Charges.ToListAsync();
            return charges.Where(x => x.OwnerId == userId)
                          .Where(x => x.Completed is null || x.Completed >= DateTime.UtcNow.AddDays(-3))
                          .Where(x => x.ParentId is null)
                          .ToList();
        }

        public async Task<List<Charge>> GetActiveParentsAndChildrenAsync(Battery battery)
        {
            List<Charge> output = new();

            var parentCharges = await GetActiveParentsOnlyAsync(battery);

            foreach (var charge in parentCharges)
            {
                var childrenCharges = await GetChildrenAsync(charge);

                if (childrenCharges.Any())
                {
                    output.AddRange(childrenCharges);
                }
            }

            output.AddRange(parentCharges);

            return output;
        }

        public async Task<List<Charge>> GetActiveParentsAndChildrenAsync(string userId)
        {
            List<Charge> output = new();

            var parentCharges = await GetActiveParentsOnlyAsync(userId);

            foreach (var charge in parentCharges)
            {
                var childrenCharges = await GetChildrenAsync(charge);

                if (childrenCharges.Any())
                {
                    output.AddRange(childrenCharges);
                }
            }

            output.AddRange(parentCharges);

            return output;
        }

        public async Task<List<Charge>> GetChildrenAsync(Charge charge)
        {
            using var context = _contextFactory.CreateDbContext();
            var charges = await context.Charges.ToListAsync();
            var children = charges.Where(x => x.ParentId == charge.Id).ToList();
            return children;
        }

        public async Task<int> GetCountAsync(string userId)
        {
            var charges = await GetAsync(userId);
            return charges.Count;
        }

        public async Task<Charge> GetParentAsync(Charge charge)
        {
            using var context = _contextFactory.CreateDbContext();
            var parent = await context.Charges.FindAsync(charge.ParentId);
            return parent;
        }

        public async Task<Charge> AddAsync(Charge charge)
        {
            charge.Completed = await _chargeLifecycle.GetCompletedAsync(charge);

            using var context = _contextFactory.CreateDbContext();

            var oldValues = context.Entry(charge).GetDatabaseValues();
            var newValues = context.Entry(charge).CurrentValues;

            context.Charges.Add(charge);
            await context.SaveChangesAsync();
            
            await _noteService.AddEntityHistoryNoteAsync(oldValues, newValues);

            if (charge.ParentId is not null)
            {
                var parent = await GetAsync((int)charge.ParentId);
                await _noteService.AddChildParentHistoryNoteAsync(charge, parent);
            }

            return charge;
        }

        public async Task<Charge> UpdateAsync(Charge charge)
        {
            charge.Completed = await _chargeLifecycle.GetCompletedAsync(charge);

            using var context = _contextFactory.CreateDbContext();
            context.Entry(charge).State = EntityState.Modified;

            var oldValues = context.Entry(charge).GetDatabaseValues();
            var newValues = context.Entry(charge).CurrentValues;

            await _noteService.AddEntityHistoryNoteAsync(oldValues, newValues);

            await context.SaveChangesAsync();

            return charge;
        }

        public async Task<Charge> SetUpdatedAsync(int chargeId)
        {
            var charge = await GetAsync(chargeId);
            using var context = _contextFactory.CreateDbContext();
            context.Entry(charge).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return charge;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var charge = await context.Charges.FindAsync(id);
            context.Charges.Remove(charge);
            await context.SaveChangesAsync();
        }

    }
}
