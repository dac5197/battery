using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.TagModel;
using System.Collections.Generic;

namespace BatteryApp.Views.Utils
{
    public interface ISearchUtil
    {
        void Clear();
        void Initialize(List<Battery> batteries);
        void Initialize(List<Charge> charges);
        void Initialize(List<Battery> batteries, List<Charge> charges);
        void Initialize(List<ChargeTagRelation> relations);
        void AddChargeChildren(Charge parent, List<Charge> children);
        List<Battery> GetBatteries();
        List<Charge> GetCharges();
        Dictionary<int, List<Charge>> GetChildren();
        List<Charge> GetChildren(int parentId);
        Tag GetTag();
        bool HasChildren(int parentId);
        void SearchBatteries(string searchText);
        void SearchCharges(string searchText);
        void SearchCharges(Tag tag);
        void SearchChildren(string searchText);
    }
}