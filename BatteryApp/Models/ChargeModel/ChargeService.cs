﻿using BatteryApp.Data;
using BatteryApp.Internals;
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
        private readonly IChargeLifecycle _chargeLifecycle;

        public ChargeService(IDbContextFactory<AppDbContextFactory> contextFactory, IChargeLifecycle chargeLifecycle)
        {
            _contextFactory = contextFactory;
            _chargeLifecycle = chargeLifecycle;
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

        public async Task<List<Charge>> GetChildren(Charge charge)
        {
            using var context = _contextFactory.CreateDbContext();
            var charges = await context.Charges.ToListAsync();
            var children = charges.Where(x => x.ParentId == charge.Id).ToList();
            return children;
        }

        public async Task<Charge> GetParent(Charge charge)
        {
            using var context = _contextFactory.CreateDbContext();
            var parent = await context.Charges.FindAsync(charge.ParentId);
            return parent;
        }

        public async Task<Charge> Add(Charge charge)
        {
            charge.Completed = await _chargeLifecycle.GetCompletedAsync(charge);

            using var context = _contextFactory.CreateDbContext();
            context.Charges.Add(charge);
            await context.SaveChangesAsync();
         
            return charge;
        }

        public async Task<Charge> Update(Charge charge)
        {
            charge.Completed = await _chargeLifecycle.GetCompletedAsync(charge);

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
