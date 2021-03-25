using BatteryApp.Models.ChargeModel;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public interface IChargeChildController
    {
        Task ToggleCompletedStatusAsync(Charge charge, bool isComplete);
    }
}