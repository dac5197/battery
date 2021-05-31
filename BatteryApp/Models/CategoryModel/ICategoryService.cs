using BatteryApp.Models.BatteryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.CategoryModel
{
    public interface ICategoryService
    {
        Task<Category> AddAsync(Category category);
        Task DeleteAsync(int id);
        Task<List<Category>> GetAsync();
        Task<Category> GetAsync(int id);
        Task<List<Category>> GetAsync(Battery battery);
        Task<List<Category>> GetAsync(string userId);
        Task<Category> GetByNameAsync(string name);
        Task<Category> GetDefaultChargeCategoryAsync(int batteryId);
        Task<Category> GetDefaultChildCategoryAsync(int batteryId);
        List<Category> GetDefaultValues();
        List<string> GetIconList();
        Task<Category> UpdateAsync(Category category);
    }
}