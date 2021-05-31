using BatteryApp.Models.BatteryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.ChargeModel
{
    public interface IChargeCountService
    {
        Task<int> GetActiveCountAsync(Battery battery);
        Task<int> GetAllCountAsync(Battery battery);
        Task<Dictionary<int, int>> GetActiveCountsByStatusAsync(Battery battery);
        Task<List<ChargeChildrenCount>> GetChildrenCountAsync(List<Charge> charges);
        Task<Dictionary<int, int>> GetInactiveCountsByStatusAsync(Battery battery);
    }
}