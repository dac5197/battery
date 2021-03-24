using BatteryApp.Models.ChargeModel;
using System;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public interface IChargeLifecycle
    {
        Task<DateTime?> SetCompletedAsync(Charge charge);
        bool IsCompleted(Charge charge);
    }
}