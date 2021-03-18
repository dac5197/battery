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
        Task<Charge> Update(Charge charge);
    }
}