using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.ChargeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Views.Utils
{
    public class BreadcrumbManager : IBreadcrumbManager
    {
        private readonly IBatteryService _batteryService;
        private readonly IChargeService _chargeService;

        public BreadcrumbManager(IBatteryService batteryService, IChargeService chargeService)
        {
            _batteryService = batteryService;
            _chargeService = chargeService;
        }

        public bool IsVisible { get; set; } = false;

        public List<BreadcrumbLink> Links { get; set; } = new();

        public void ClearLinks()
        {
            Links?.Clear();
        }

        public void AddLink(string url, string label)
        {
            BreadcrumbLink link = new();
            link.Initialize(url, label);
            Links.Add(link);
        }

        public void Initialize(Charge charge)
        {
            ClearLinks();
            AddChargeLink(charge);
            AddBatteryLink(charge.BatteryId);

            Links.Reverse();
        }

        public async void AddChargeLink(Charge charge)
        {
            string url = $"/charge/{charge.Id}";
            string label = $"Charge{charge.Id}";

            AddLink(url, label);

            if (charge.ParentId is not null)
            {
                Charge parent = await _chargeService.Get((int)charge.ParentId);
                AddChargeLink(parent);
            }
        }

        public void AddBatteryLink(int id)
        {
            string url = $"/battery/{id}";
            string label = $"Battery{id}";

            AddLink(url, label);
        }

        public void Show()
        {
            IsVisible = true;
            NotifyStateChanged();
        }

        public void Hide()
        {
            IsVisible = false;
            NotifyStateChanged();
        }

        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
