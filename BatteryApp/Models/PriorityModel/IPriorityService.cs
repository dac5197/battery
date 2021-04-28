using BatteryApp.Models.BatteryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.PriorityModel
{
    public interface IPriorityService
    {
        Task<Priority> Add(Priority priority);
        Task Delete(int id);
        Task<List<Priority>> Get();
        Task<List<Priority>> Get(Battery battery);
        Task<Priority> Get(int id);
        Task<List<Priority>> Get(string userId);
        Task<Priority> GetDefault(string userId);
        List<Priority> GetDefaultValues();
        Task<Priority> Update(Priority priority);
    }
}