using BatteryApp.Data;
using BatteryApp.Models.BatteryModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<List<Status>> Get(Battery battery)
        {
            using var context = _contextFactory.CreateDbContext();
            var statuses = await context.Statuses.ToListAsync();
            return statuses.Where(x => x.BatteryId == battery.Id).ToList();
        }

        public async Task<Status> Get(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var status = await context.Statuses.FindAsync(id);
            return status;
        }

        public async Task<Status> GetCompletedStatus(int batteryId)
        {
            using var context = _contextFactory.CreateDbContext();
            var statuses = await context.Statuses.ToListAsync();
            var completedStatus = statuses.Where(x => x.BatteryId == batteryId)
                                          .OrderByDescending(x => x.Order)
                                          .FirstOrDefault();
            return completedStatus;
        }

        public List<Status> GetDefaultValues()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\data\\batterydefaults.json");
            var json = File.ReadAllText(file);
            dynamic jsonObject = JsonConvert.DeserializeObject(json);

            List<Status> statuses = new();

            foreach (var item in jsonObject["Statuses"])
            {
                Status status = new()
                {
                    Name = item["Name"],
                    Order = item["Order"],
                    BgColor = item["BgColor"],
                    FontColor = item["FontColor"],
                };

                statuses.Add(status);
            }

            return statuses;
        }

        public async Task<Status> GetInitialStatus(int batteryId)
        {
            using var context = _contextFactory.CreateDbContext();
            var statuses = await context.Statuses.ToListAsync();
            var initialStatus = statuses.Where(x => x.BatteryId == batteryId)
                                        .OrderBy(x => x.Order)
                                        .FirstOrDefault();
            return initialStatus;
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


