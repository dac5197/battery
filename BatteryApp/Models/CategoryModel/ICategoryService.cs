using BatteryApp.Models.BatteryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.CategoryModel
{
    public interface ICategoryService
    {
        Task<Category> Add(Category category);
        Task Delete(int id);
        Task<List<Category>> Get();
        Task<Category> Get(int id);
        Task<List<Category>> Get(Battery battery);
        Task<List<Category>> Get(string userId);
        Task<Category> GetByName(string name);
        Task<Category> GetDefaultChargeCategory(int batteryId);
        Task<Category> GetDefaultChildCategory(int batteryId);
        List<Category> GetDefaultValues();
        List<string> GetIconList();
        Task<Category> Update(Category category);
    }
}