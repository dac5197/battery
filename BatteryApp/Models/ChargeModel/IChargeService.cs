using BatteryApp.Models.BatteryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.ChargeModel
{
    public interface IChargeService
    {
        Task<Charge> AddAsync(Charge charge);
        Task DeleteAsync(int id);
        Task<List<Charge>> GetAsync();
        Task<Charge> GetAsync(int id);
        Task<List<Charge>> GetAsync(string userId);
        Task<List<Charge>> GetAsync(Battery battery);
        Task<List<Charge>> GetActiveAsync();
        Task<List<Charge>> GetActiveAsync(Battery battery);
        Task<List<Charge>> GetActiveAsync(string userId);
        Task<List<Charge>> GetActiveParentsOnlyAsync(Battery battery);
        Task<List<Charge>> GetActiveParentsOnlyAsync(string userId);
        Task<List<Charge>> GetActiveParentsAndChildrenAsync(Battery battery);
        Task<List<Charge>> GetActiveParentsAndChildrenAsync(string userId);
        Task<List<Charge>> GetChildrenAsync(Charge charge);
        Task<int> GetCountAsync(string userId);
        Task<Charge> GetParent(Charge charge);
        Task<Charge> SetUpdatedAsync(int chargeId);
        Task<Charge> UpdateAsync(Charge charge);
    }
}