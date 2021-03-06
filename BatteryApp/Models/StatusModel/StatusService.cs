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

        public async Task<List<Status>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Statuses.ToListAsync();
        }

        public async Task<List<Status>> GetAsync(Battery battery)
        {
            using var context = _contextFactory.CreateDbContext();
            var statuses = await context.Statuses.ToListAsync();
            return statuses.Where(x => x.BatteryId == battery.Id).ToList();
        }

        public async Task<List<Status>> GetAsync(string userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var statuses = await context.Statuses.ToListAsync();
            return statuses.Where(x => x.OwnerId == userId).ToList();
        }

        public async Task<Status> GetAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var status = await context.Statuses.FindAsync(id);
            return status;
        }

        public async Task<Status> GetCompletedStatusAsync(int batteryId)
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

        public async Task<Status> GetInitialStatusAsync(int batteryId)
        {
            using var context = _contextFactory.CreateDbContext();
            var statuses = await context.Statuses.ToListAsync();
            var initialStatus = statuses.Where(x => x.BatteryId == batteryId)
                                        .OrderBy(x => x.Order)
                                        .FirstOrDefault();
            return initialStatus;
        }

        public async Task<Status> AddAsync(Status status)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Statuses.Add(status);
            await context.SaveChangesAsync();
            return status;
        }

        public async Task<Status> UpdateAsync(Status status)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(status).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return status;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
                       
            var status = await context.Statuses.FindAsync(id);

            await ReOrderStatusesAsync(status);

            context.Statuses.Remove(status);
            await context.SaveChangesAsync();
        }

        private async Task ReOrderStatusesAsync(Status status)
        {
            using var context = _contextFactory.CreateDbContext();
            var statuses = await context.Statuses.Where(x => x.BatteryId == status.BatteryId).ToListAsync();
            var statuseToBeReOrdered = statuses.Where(x => x.Order > status.Order).ToList();

            foreach (var statusToBeReOrdered in statuseToBeReOrdered)
            {
                statusToBeReOrdered.Order--;
                await UpdateAsync(statusToBeReOrdered);
            }
        }
    }
}


