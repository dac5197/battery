using BatteryApp.Models.BatteryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.PriorityModel
{
    public interface IPriorityService
    {
        Task<Priority> AddAsync(Priority priority);
        Task DeleteAsync(int id);
        Task<List<Priority>> GetAsync();
        Task<List<Priority>> GetAsync(Battery battery);
        Task<Priority> GetAsync(int id);
        Task<List<Priority>> GetAsync(string userId);
        Task<Priority> GetDefaultAsync(int batteryId);
        List<Priority> GetDefaultValues();
        Task<Priority> UpdateAsync(Priority priority);
    }
}