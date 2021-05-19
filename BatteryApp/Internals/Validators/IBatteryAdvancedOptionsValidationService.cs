using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.PriorityModel;
using BatteryApp.Models.StatusModel;
using System.Collections.Generic;

namespace BatteryApp.Internals.Validators
{
    public interface IBatteryAdvancedOptionsValidator
    {
        Dictionary<string, List<string>> Validate(List<Category> categories, List<Priority> priorities, List<Status> statuses);
        Dictionary<string, List<string>> Validate(List<Category> categories);
        Dictionary<string, List<string>> Validate(List<Priority> priorities);

        public void ClearErrors();
    }
}