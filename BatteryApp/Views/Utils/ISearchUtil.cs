using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.TagModel;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BatteryApp.Views.Utils
{
    public interface ISearchUtil
    {
        void Clear();

        void Initialize(Battery battery);
        void Initialize(List<Battery> batteries);
        void Initialize(List<Charge> charges);
        void Initialize(List<Battery> batteries, List<Charge> charges);
        void Initialize(List<ChargeTagRelation> relations);
        void AddChargeChildren(Charge parent, List<Charge> children);
        List<Battery> GetBatteries();
        List<Charge> GetCharges();
        Dictionary<int, List<Charge>> GetChildren();
        List<Charge> GetChildren(int parentId);
        Category GetCategory();
        Tag GetTag();
        bool HasChildren(int parentId);
        MarkupString HighlightSearchText(string searchText, string text);
        void Search(Category category);
        void Search(Tag tag);
        void SearchBatteries(string searchText);
        void SearchCharges(string searchText);
        void SearchCharges(Category category);
        void SearchCharges(Tag tag);
        void SearchChildren(string searchText);
        void SearchChildren(Tag tag);
    }
}