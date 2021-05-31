using BatteryApp.Data;
using BatteryApp.Models.BatteryModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<List<Priority>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Priorities.ToListAsync();
        }

        public async Task<List<Priority>> GetAsync(Battery battery)
        {
            using var context = _contextFactory.CreateDbContext();
            var priorities = await context.Priorities.ToListAsync();
            return priorities.Where(x => x.BatteryId == battery.Id).ToList();
        }

        public async Task<List<Priority>> GetAsync(string userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var priorities = await context.Priorities.ToListAsync();
            return priorities.Where(x => x.OwnerId == userId).ToList();
        }

        public async Task<Priority> GetAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var priority = await context.Priorities.FindAsync(id);
            return priority;
        }

        public async Task<Priority> GetDefaultAsync(int batteryId)
        {
            using var context = _contextFactory.CreateDbContext();
            var priorities = await context.Priorities.ToListAsync();
            var priority = priorities.Where(x => x.BatteryId == batteryId && x.IsDefault == true).FirstOrDefault();
            return priority;
        }

        public List<Priority> GetDefaultValues()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\data\\batterydefaults.json");
            var json = File.ReadAllText(file);
            dynamic jsonObject = JsonConvert.DeserializeObject(json);

            List<Priority> priorities = new();

            foreach (var item in jsonObject["Priorities"])
            {
                Priority priority = new()
                {
                    Name = item["Name"],
                    Severity = item["Severity"],
                    BgColor = item["BgColor"],
                    FontColor = item["FontColor"],
                    IsDefault = item["IsDefault"],
                };

                priorities.Add(priority);
            }

            return priorities;
        }

        public async Task<Priority> AddAsync(Priority priority)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Priorities.Add(priority);
            await context.SaveChangesAsync();
            return priority;
        }

        public async Task<Priority> UpdateAsync(Priority priority)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(priority).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return priority;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var priority = await context.Priorities.FindAsync(id);
            context.Priorities.Remove(priority);
            await context.SaveChangesAsync();
        }
    }
}
