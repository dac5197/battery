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

        public async Task ToggleCompletedStatusAsync(Charge charge, bool isComplete)
        {
            if (isComplete)
            {
                var completedStatus = await _statusService.GetCompletedStatus();
                charge.StatusId = completedStatus.Id;
            }
            else
            {
                if (charge.ParentId is not null)
                {
                    var parent = await _chargeService.GetParent(charge);
                    charge.StatusId = parent.StatusId;
                }
                else
                {
                    var initialStatus = await _statusService.GetInitialStatus();
                    charge.StatusId = initialStatus.Id;
                }
            }

            await _chargeService.Update(charge);
        }
    }
}
