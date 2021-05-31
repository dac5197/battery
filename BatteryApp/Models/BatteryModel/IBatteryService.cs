using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.BatteryModel
{
    public interface IBatteryService
    {
        Task<Battery> AddAsync(Battery Battery);
        Task DeleteAsync(int id);
        Task<List<Battery>> GetAsync();
        Task<List<Battery>> GetAsync(string userId);
        Task<List<Battery>> GetActiveAsync(string userId);
        Task<Battery> GetAsync(int id);
        Task<int> GetCountAsync(string userId);
        Task<Battery> UpdateAsync(Battery Battery);
    }
}