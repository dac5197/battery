using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.ChargeModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Views.Utils
{
    public interface IBreadcrumbManager
    {
        bool IsVisible { get; set; }
        List<BreadcrumbLink> Links { get; set; }

        event Action OnChange;

        void AddBatteryLink(int id);
        Task AddChargeLinkAsync(Charge charge);
        void AddLink(string url, string label);
        void ClearLinks();
        void Hide();
        Task InitializeAsync(Charge charge);
        void Show();
    }
}