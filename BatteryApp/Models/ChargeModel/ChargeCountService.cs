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

        public async Task<int> GetActiveCount(Battery battery)
        {
            var charges = await _chargeService.Get(battery);
            return charges.Where(x => x.BatteryId == battery.Id && (x.Completed is null || x.Completed >= DateTime.UtcNow.AddDays(-3))).ToList().Count;
        }

        public async Task<int> GetAllCount(Battery battery)
        {
            var charges = await _chargeService.Get(battery);
            return charges.ToList().Count;
        }

        public async Task<Dictionary<int, int>> GetActiveCountsByStatus(Battery battery)
        {
            Dictionary<int, int> counts = new();

            var charges = await _chargeService.Get(battery);
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

        public async Task<Dictionary<int, int>> GetInactiveCountsByStatus(Battery battery)
        {
            Dictionary<int, int> counts = new();

            var charges = await _chargeService.Get(battery);
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

        public async Task<List<ChargeChildrenCount>> GetChildrenCount(List<Charge> charges)
        {

            List<ChargeChildrenCount> chargeChildrenCounts = new();

            foreach (var charge in charges)
            {
                var children = await _chargeService.GetChildren(charge);
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
