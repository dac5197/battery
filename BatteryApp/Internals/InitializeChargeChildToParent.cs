using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.PriorityModel;
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

        public InitializeChargeChildToParent(ICategoryService categoryService, IPriorityService priorityService)
        {
            _categoryService = categoryService;
            _priorityService = priorityService;
        }

        public async Task<Charge> SetRelationshipAsync(Charge parent, Charge child)
        {
            child.ParentId = parent.Id;
            child.OwnerId = parent.OwnerId;
            child.BatteryId = parent.BatteryId;
            var childCategory = await _categoryService.GetByName("spark");
            child.CategoryId = childCategory.Id;
            var defaultPriority = await _priorityService.GetDefault(parent.OwnerId);
            child.PriorityId = defaultPriority.Id;

            return child;
        }
    }
}
