using BatteryApp.Models.BatteryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.ChargeModel
{
    public interface IChargeService
    {
        Task<Charge> Add(Charge charge);
        Task Delete(int id);
        Task<List<Charge>> Get();
        Task<Charge> Get(int id);
        Task<List<Charge>> Get(Battery battery);
        Task<List<Charge>> GetActive();
        Task<List<Charge>> GetActive(Battery battery);
        Task<List<Charge>> GetActive(string userId);
        Task<List<Charge>> GetActiveParentsOnly(Battery battery);
        Task<List<Charge>> GetActiveParentsOnly(string userId);
        Task<List<Charge>> GetChildren(Charge charge);
        Task<Charge> GetParent(Charge charge);
        Task<Charge> SetUpdated(int chargeId);
        Task<Charge> Update(Charge charge);
    }
}