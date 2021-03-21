using BatteryApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.StatusModel
{
    public class StatusService : IStatusService
    {
        private readonly IDbContextFactory<AppDbContextFactory> _contextFactory;

        public StatusService(IDbContextFactory<AppDbContextFactory> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Status>> Get()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Statuses.ToListAsync();
        }

        public async Task<Status> Get(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var status = await context.Statuses.FindAsync(id);
            return status;
        }

        public async Task<Status> Add(Status status)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Statuses.Add(status);
            await context.SaveChangesAsync();
            return status;
        }

        public async Task<Status> Update(Status status)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(status).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return status;
        }

        public async Task Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var status = await context.Statuses.FindAsync(id);
            context.Statuses.Remove(status);
            await context.SaveChangesAsync();
        }

    }
}


