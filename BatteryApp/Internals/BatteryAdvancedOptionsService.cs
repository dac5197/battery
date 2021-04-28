using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.PriorityModel;
using BatteryApp.Models.StatusModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public class BatteryAdvancedOptionsService : IBatteryAdvancedOptionsService
    {
        private readonly ICategoryService _categoryService;
        private readonly IPriorityService _priorityService;
        private readonly IStatusService _statusService;

        public BatteryAdvancedOptionsService(ICategoryService categoryService, IPriorityService priorityService, IStatusService statusService)
        {
            _categoryService = categoryService;
            _priorityService = priorityService;
            _statusService = statusService;
        }

        public async Task SaveAsync(Battery battery, List<Category> categories, List<Priority> priorities, List<Status> statuses)
        {
            await SaveCategoriesAsync(battery, categories);
            await SavePrioritiesAsync(battery, priorities);
            await SaveStatusesAsync(battery, statuses);
        }

        private async Task SaveCategoriesAsync(Battery battery, List<Category> categories)
        {
            foreach (var category in categories)
            {
                category.BatteryId = battery.Id;
                category.OwnerId = battery.OwnerId;

                await _categoryService.Add(category);
            }
        }

        private async Task SavePrioritiesAsync(Battery battery, List<Priority> priorities)
        {
            foreach (var priority in priorities)
            {
                priority.BatteryId = battery.Id;
                priority.OwnerId = battery.OwnerId;

                await _priorityService.Add(priority);
            }
        }

        private async Task SaveStatusesAsync(Battery battery, List<Status> statuses)
        {
            foreach (var status in statuses)
            {
                status.BatteryId = battery.Id;
                status.OwnerId = battery.OwnerId;

                await _statusService.Add(status);
            }
        }
    }
}
