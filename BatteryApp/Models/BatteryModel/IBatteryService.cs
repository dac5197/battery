using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.BatteryModel
{
    public interface IBatteryService
    {
        Task<Battery> Add(Battery Battery);
        Task Delete(int id);
        Task<List<Battery>> Get();
        Task<Battery> Get(int id);
        Task<Battery> Update(Battery Battery);
    }
}