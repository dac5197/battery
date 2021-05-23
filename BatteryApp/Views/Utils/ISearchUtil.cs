using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.ChargeModel;
using System.Collections.Generic;

namespace BatteryApp.Views.Utils
{
    public interface ISearchUtil
    {
        void Clear();
        void Initialize(List<Battery> batteries);
        void Initialize(List<Charge> charges);
        void Initialize(List<Battery> batteries, List<Charge> charges);
        List<Battery> GetBatteries();
        List<Charge> GetCharges();
        void SearchBatteries(string searchText);
        void SearchCharges(string searchText);
    }
}