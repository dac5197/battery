using BatteryApp.Models.ChargeModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Views.Utils
{
    public interface IChargeOpenChildrenModalHelper
    {
        Charge Charge { get; set; }
        bool HasChildren { get; }
        List<Charge> OpenChildren { get; set; }

        Task CompleteOpenChildrenAsync();
        Task SetChargeAndOpenChildrenAsync(Charge charge);
    }
}