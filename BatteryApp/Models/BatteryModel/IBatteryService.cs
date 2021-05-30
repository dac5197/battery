using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.BatteryModel
{
    public interface IBatteryService
    {
        Task<Battery> Add(Battery Battery);
        Task Delete(int id);
        Task<List<Battery>> Get();
        Task<List<Battery>> Get(string userId);
        Task<List<Battery>> GetActive(string userId);
        Task<Battery> Get(int id);
        Task<int> GetCount(string userId);
        Task<Battery> Update(Battery Battery);
    }
}