using BatteryApp.Models.ChargeModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public interface IChargeChildController
    {
        Task CompleteOpenChildren(Charge charge);
        Task<List<Charge>> GetOpenChildren(Charge charge);
        Task<bool> HasOpenChildren(Charge charge);
        Task ToggleCompletedStatusAsync(Charge charge, bool isComplete);
    }
}