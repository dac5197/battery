using BatteryApp.Models.ChargeModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public interface IChargeChildController
    {
        Task CompleteOpenChildrenAsync(Charge charge);
        Task<List<Charge>> GetOpenChildrenAsync(Charge charge);
        Task<bool> HasOpenChildrenAsync(Charge charge);
        Task ToggleCompletedStatusAsync(Charge charge, bool isComplete);
    }
}