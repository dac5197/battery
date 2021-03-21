﻿using BatteryApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.PriorityModel
{
    public class PriorityService : IPriorityService
    {
        private readonly IDbContextFactory<AppDbContextFactory> _contextFactory;

        public PriorityService(IDbContextFactory<AppDbContextFactory> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Priority>> Get()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Priorities.ToListAsync();
        }

        public async Task<List<Priority>> Get(string userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var priorities = await context.Priorities.ToListAsync();
            return priorities.Where(x => x.OwnerId == userId).ToList();
        }

        public async Task<Priority> Get(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var priority = await context.Priorities.FindAsync(id);
            return priority;
        }

        public async Task<Priority> Add(Priority priority)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Priorities.Add(priority);
            await context.SaveChangesAsync();
            return priority;
        }

        public async Task<Priority> Update(Priority priority)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(priority).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return priority;
        }

        public async Task Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var priority = await context.Priorities.FindAsync(id);
            context.Priorities.Remove(priority);
            await context.SaveChangesAsync();
        }
    }
}