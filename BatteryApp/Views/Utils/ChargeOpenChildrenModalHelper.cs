using BatteryApp.Internals;
using BatteryApp.Models.ChargeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Views.Utils
{
    public class ChargeOpenChildrenModalHelper : IChargeOpenChildrenModalHelper
    {
        private readonly IChargeChildController _chargeChildController;

        public ChargeOpenChildrenModalHelper(IChargeChildController chargeChildController)
        {
            _chargeChildController = chargeChildController;
        }

        public Charge Charge { get; set; } = new();

        public List<Charge> OpenChildren { get; set; } = new();

        public bool HasChildren
        {
            get
            {
                if (OpenChildren.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task CompleteOpenChildrenAsync()
        {
            await _chargeChildController.CompleteOpenChildrenAsync(Charge);
        }

        public async Task SetChargeAndOpenChildrenAsync(Charge charge)
        {
            Charge = charge;
            OpenChildren = await _chargeChildController.GetOpenChildrenAsync(charge);
        }

    }
}
