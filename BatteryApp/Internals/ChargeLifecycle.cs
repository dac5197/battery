using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.StatusModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public class ChargeLifecycle : IChargeLifecycle
    {
        private readonly IStatusService _statusService;

        public ChargeLifecycle(IStatusService statusService)
        {
            _statusService = statusService;
        }

        public async Task<DateTime?> GetCompletedAsync(Charge charge)
        {
            var completedStatus = await _statusService.GetCompletedStatus();

            if (charge.StatusId == completedStatus.Id)
            {
                return DateTime.Now;
            }

            return null;
        }

        public bool IsCompleted(Charge charge)
        {
            if (charge.Completed is not null)
            {
                return true;
            }

            return false;
        }

       
    }
}
