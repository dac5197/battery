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
        Task<List<Charge>> GetActiveParentsOnly(Battery battery);
        Task<int> GetAllCount(Battery battery);
        Task<List<Charge>> GetChildren(Charge charge);
        Task<List<ChargeChildrenCount>> GetChildrenCount(List<Charge> charges);
        Task<Charge> GetParent(Charge charge);
        Task<Charge> SetUpdated(int chargeId);
        Task<Charge> Update(Charge charge);
    }
}