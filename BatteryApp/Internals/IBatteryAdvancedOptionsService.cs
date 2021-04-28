using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.PriorityModel;
using BatteryApp.Models.StatusModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public interface IBatteryAdvancedOptionsService
    {
        Task SaveAsync(Battery battery, List<Category> categories, List<Priority> priorities, List<Status> statuses);
    }
}