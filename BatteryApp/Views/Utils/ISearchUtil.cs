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
        void InitializeChildren(List<Charge> children);
        void AddChargeChildren(Charge parent, List<Charge> children);
        List<Battery> GetBatteries();
        List<Charge> GetCharges();
        List<Charge> GetChildren();
        List<Charge> GetChildren(int parentId);
        bool HasChildren(int parentId);
        void SearchBatteries(string searchText);
        void SearchCharges(string searchText);
        void SearchChildren(string searchText);
    }
}