using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.PriorityModel;
using BatteryApp.Models.StatusModel;
using System.Collections.Generic;

namespace BatteryApp.Internals
{
    public interface IBatteryAdvancedOptionsValidationService
    {
        Dictionary<string, List<string>> Validate(List<Category> categories, List<Priority> priorities, List<Status> statuses);
        Dictionary<string, List<string>> Validate(List<Category> categories);
    }
}