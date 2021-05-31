using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.PriorityModel;
using BatteryApp.Models.StatusModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public class ChargeLifecycle : IChargeLifecycle
    {
        private readonly ICategoryService _categoryService;
        private readonly IPriorityService _priorityService;
        private readonly IStatusService _statusService;

        public ChargeLifecycle(ICategoryService categoryService, IPriorityService priorityService, IStatusService statusService)
        {
            _categoryService = categoryService;
            _priorityService = priorityService;
            _statusService = statusService;
        }

        public async Task<DateTime?> GetCompletedAsync(Charge charge)
        {
            var completedStatus = await _statusService.GetCompletedStatus(charge.BatteryId);

            if (charge.StatusId == completedStatus.Id)
            {
                return DateTime.UtcNow;
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

        public async Task<Charge> SetDefaultValuesAsync(Battery battery, Charge charge)
        {
            charge.BatteryId = battery.Id;
            charge.OwnerId = battery.OwnerId;


            var initialStatus = await _statusService.GetInitialStatus(charge.BatteryId);
            charge.StatusId = initialStatus.Id;

            var category = await _categoryService.GetDefaultChargeCategoryAsync(battery.Id);
            charge.CategoryId = category.Id;

            var defaultPriority = await _priorityService.GetDefault(battery.Id);
            charge.PriorityId = defaultPriority.Id;

            return charge;
        }
    }
}
