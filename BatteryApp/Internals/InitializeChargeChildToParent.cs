using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.NoteModel;
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
        private readonly INoteService _noteService;
        private readonly IPriorityService _priorityService;

        public InitializeChargeChildToParent(ICategoryService categoryService, INoteService noteService, IPriorityService priorityService)
        {
            _categoryService = categoryService;
            _noteService = noteService;
            _priorityService = priorityService;
        }

        public async Task<Charge> SetRelationshipAsync(Charge parent, Charge child)
        {
            // Set child charge fields from parent charge
            child.ParentId = parent.Id;
            child.OwnerId = parent.OwnerId;
            child.BatteryId = parent.BatteryId;
            // Get Child category ("spark") and assign it to child
            var childCategory = await _categoryService.GetByName("spark");
            child.CategoryId = childCategory.Id;
            // Set child priority to default priority
            var defaultPriority = await _priorityService.GetDefault(parent.OwnerId);
            child.PriorityId = defaultPriority.Id;

            return child;
        }
    }
}
