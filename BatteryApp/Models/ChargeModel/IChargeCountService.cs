using BatteryApp.Models.BatteryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.ChargeModel
{
    public interface IChargeCountService
    {
        Task<int> GetActiveCount(Battery battery);
        Task<int> GetAllCount(Battery battery);
        Task<Dictionary<int, int>> GetActiveCountsByStatus(Battery battery);
        Task<List<ChargeChildrenCount>> GetChildrenCount(List<Charge> charges);
        Task<Dictionary<int, int>> GetInactiveCountsByStatus(Battery battery);
    }
}