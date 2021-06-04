using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.CategoryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.ChargeModel
{
    public interface IChargeCountService
    {
        Task<int> GetActiveCountAsync(Battery battery);
        Task<int> GetAllCountAsync(Battery battery);
        Task<Dictionary<int, int>> GetActiveCountsByStatusAsync(Battery battery);
        Task<Dictionary<int, int>> GetCategoryCountAsync(List<Battery> batteries, List<Category> categories);
        Task<Dictionary<int, int>> GetCategoryCountAsync(Battery battery, List<Category> categories);
        Task<List<ChargeChildrenCount>> GetChildrenCountAsync(List<Charge> charges);
        Task<Dictionary<int, int>> GetInactiveCountsByStatusAsync(Battery battery);
    }
}