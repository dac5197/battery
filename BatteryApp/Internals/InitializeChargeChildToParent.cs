using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.NoteModel;
using BatteryApp.Models.PriorityModel;
using BatteryApp.Models.StatusModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public class InitializeChargeChildToParent : IInitializeChargeChildToParent
    {
        private readonly ICategoryService _categoryService;
        private readonly IPriorityService _priorityService;
        private readonly IStatusService _statusService;

        public InitializeChargeChildToParent(ICategoryService categoryService, IPriorityService priorityService, IStatusService statusService)
        {
            _categoryService = categoryService;
            _priorityService = priorityService;
            _statusService = statusService;
        }

        public async Task<Charge> SetRelationshipAsync(Charge parent, Charge child)
        {
            // Set child charge fields from parent charge
            child.ParentId = parent.Id;
            child.OwnerId = parent.OwnerId;
            child.BatteryId = parent.BatteryId;
            // Get Child category ("spark") and assign it to child
            var childCategory = await _categoryService.GetDefaultChildCategoryAsync(parent.BatteryId);
            child.CategoryId = childCategory.Id;
            // Set child priority to default priority
            var defaultPriority = await _priorityService.GetDefaultAsync(parent.BatteryId);
            child.PriorityId = defaultPriority.Id;
            // Set child to initial status
            var initialStatus = await _statusService.GetInitialStatusAsync(parent.BatteryId);
            child.StatusId = initialStatus.Id;

            return child;
        }
    }
}
