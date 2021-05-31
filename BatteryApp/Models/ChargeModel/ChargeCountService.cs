using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.StatusModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.ChargeModel
{
    public class ChargeCountService : IChargeCountService
    {
        private readonly IChargeService _chargeService;
        private readonly IStatusService _statusService;

        public ChargeCountService(IChargeService chargeService, IStatusService statusService)
        {
            _chargeService = chargeService;
            _statusService = statusService;
        }

        public async Task<int> GetActiveCountAsync(Battery battery)
        {
            var charges = await _chargeService.GetAsync(battery);
            return charges.Where(x => x.BatteryId == battery.Id && (x.Completed is null || x.Completed >= DateTime.UtcNow.AddDays(-3))).ToList().Count;
        }

        public async Task<int> GetAllCountAsync(Battery battery)
        {
            var charges = await _chargeService.GetAsync(battery);
            return charges.ToList().Count;
        }

        public async Task<Dictionary<int, int>> GetActiveCountsByStatusAsync(Battery battery)
        {
            Dictionary<int, int> counts = new();

            var charges = await _chargeService.GetAsync(battery);
            var statuses = await _statusService.Get(battery);

            foreach (var status in statuses)
            {
                counts.Add(
                            status.Id,
                            charges.Where(x => x.StatusId == status.Id)
                                   .Where(x => x.ParentId is null)
                                   .Where(x => x.Completed is null || x.Completed >= DateTime.UtcNow.AddDays(-3))
                                   .ToList()
                                   .Count
                          );
            }

            return counts;
        }

        public async Task<Dictionary<int, int>> GetInactiveCountsByStatusAsync(Battery battery)
        {
            Dictionary<int, int> counts = new();

            var charges = await _chargeService.GetAsync(battery);
            var statuses = await _statusService.Get(battery);

            foreach (var status in statuses)
            {
                counts.Add(
                            status.Id,
                            charges.Where(x => x.StatusId == status.Id)
                                   .Where(x => x.ParentId is null)
                                   .Where(x => x.Completed is not null && x.Completed < DateTime.UtcNow.AddDays(-3))
                                   .ToList()
                                   .Count
                          );
            }

            return counts;
        }

        public async Task<List<ChargeChildrenCount>> GetChildrenCountAsync(List<Charge> charges)
        {

            List<ChargeChildrenCount> chargeChildrenCounts = new();

            foreach (var charge in charges)
            {
                var children = await _chargeService.GetChildrenAsync(charge);
                ChargeChildrenCount chargeChildrenCount = new()
                {
                    ChargeId = charge.Id,
                    ChildrenCount = children.Count
                };
                chargeChildrenCounts.Add(chargeChildrenCount);
            }

            return chargeChildrenCounts;
        }
    }
}
