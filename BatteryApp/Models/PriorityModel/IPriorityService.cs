using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.PriorityModel
{
    public interface IPriorityService
    {
        Task<Priority> Add(Priority priority);
        Task Delete(int id);
        Task<List<Priority>> Get();
        Task<Priority> Get(int id);
        Task<List<Priority>> Get(string userId);
        Task<Priority> GetDefault(string userId);
        Task<Priority> Update(Priority priority);
    }
}