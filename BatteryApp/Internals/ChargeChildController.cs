using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.StatusModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public class ChargeChildController : IChargeChildController
    {
        private readonly IChargeService _chargeService;
        private readonly IStatusService _statusService;

        public ChargeChildController(IChargeService chargeService, IStatusService statusService)
        {
            _chargeService = chargeService;
            _statusService = statusService;
        }

        public async Task CompleteOpenChildrenAsync(Charge charge)
        {
            var openChildren = await GetOpenChildrenAsync(charge);
            var completedStatus = await _statusService.GetCompletedStatusAsync(charge.BatteryId);

            foreach (var child in openChildren)
            {
                child.StatusId = completedStatus.Id;
                await _chargeService.UpdateAsync(child);
            }
        }

        public async Task<List<Charge>> GetOpenChildrenAsync(Charge charge)
        {
            var children = await _chargeService.GetChildrenAsync(charge);
            var completedStatus = await _statusService.GetCompletedStatusAsync(charge.BatteryId);

            return children.Where(x => x.StatusId != completedStatus.Id).ToList();
        }

        public async Task<bool> HasOpenChildrenAsync(Charge charge)
        {
            var openChildren = await GetOpenChildrenAsync(charge);

            if (openChildren.Count > 0)
            {
                return true;
            }

            return false;
        }

        public async Task ToggleCompletedStatusAsync(Charge charge, bool isComplete)
        {
            if (isComplete)
            {
                var completedStatus = await _statusService.GetCompletedStatusAsync(charge.BatteryId);
                charge.StatusId = completedStatus.Id;
            }
            else
            {
                if (charge.ParentId is not null)
                {
                    var parent = await _chargeService.GetParentAsync(charge);
                    charge.StatusId = parent.StatusId;
                }
                else
                {
                    var initialStatus = await _statusService.GetInitialStatusAsync(charge.BatteryId);
                    charge.StatusId = initialStatus.Id;
                }
            }

            await _chargeService.UpdateAsync(charge);
        }

       
    }
}
