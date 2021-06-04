using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.TagModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BatteryApp.Views.Utils
{
    public class SearchUtil : ISearchUtil
    {
        private List<Battery> _batteries = new();
        private List<Battery> _searchedBatteries = new();
        private List<Charge> _charges = new();
        private List<Charge> _searchedCharges = new();
        private Dictionary<int, List<Charge>> _chargeChildren = new();
        private Dictionary<int, List<Charge>> _searchedChargeChildren = new();
        private List<ChargeTagRelation> _chargeTagRelations = new();
        private List<ChargeTagRelation> _searchedChargeTagRelations = new();
        private Category _searchCategory = new();
        private Tag _searchTag = new();

        public void Clear()
        {
            _searchedBatteries = new(_batteries);
            _searchedCharges = new(_charges);
            _searchedChargeChildren = new(_chargeChildren);
            _searchedChargeTagRelations = new(_chargeTagRelations);
            _searchCategory = new();
            _searchTag = new();
        }

        public void Initialize(Battery battery)
        {
            _batteries = new() { battery };
            _searchedBatteries = new() { battery };
        }

        public void Initialize(List<Battery> batteries)
        {
            _batteries = new(batteries);
            _searchedBatteries = new(batteries);
        }

        public void Initialize(List<Charge> charges)
        {
            _charges = new(charges.Where(x => x.ParentId is null).ToList());
            _searchedCharges = new(charges.Where(x => x.ParentId is null).ToList());
        }

        public void Initialize(List<ChargeTagRelation> relations)
        {
            _chargeTagRelations = new(relations);
            _searchedChargeTagRelations = new(relations);
        }

        public void Initialize(List<Battery> batteries, List<Charge> charges)
        {
            Initialize(batteries);
            Initialize(charges);
        }

        //public void InitializeChildren(List<Charge> children)
        //{
        //    throw NotImplementedException;
            
        //}

        public void AddChargeChildren(Charge parent, List<Charge> children)
        {
            if (_chargeChildren.ContainsKey(parent.Id))
            {
                _chargeChildren[parent.Id] = children;
            }
            else
            {
                _chargeChildren.Add(parent.Id, children);
            }

            if (_searchedChargeChildren.ContainsKey(parent.Id))
            {
                _searchedChargeChildren[parent.Id] = children;
            }
            else
            {
                _searchedChargeChildren.Add(parent.Id, children);
            }
        }

        public List<Battery> GetBatteries()
        {
            return _searchedBatteries;
        }

        public List<Charge> GetCharges()
        {
            return _searchedCharges;
        }

        public Dictionary<int, List<Charge>> GetChildren()
        {
            return _searchedChargeChildren;
        }

        public List<Charge> GetChildren(int parentId)
        {
            if (_searchedChargeChildren.ContainsKey(parentId))
            {
                return _searchedChargeChildren[parentId];
            }

            List<Charge> emptyList = new();
            return emptyList;
        }

        public Category GetCategory()
        {
            return _searchCategory;
        }

        public Tag GetTag()
        {
            return _searchTag;
        }

        public bool HasChildren(int parentId)
        {
            if (_searchedChargeChildren.ContainsKey(parentId))
            {
                return true;
            }

            return false;
        }

        public MarkupString HighlightSearchText(string searchText, string text)
        {
            if (String.IsNullOrWhiteSpace(searchText) || String.IsNullOrWhiteSpace(text))
            {
                return (MarkupString)text;
            }

            var rx = new Regex(searchText, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var highlightText = rx.Replace(text, "<span style=\"background-color: yellow\">$&</span>");

            return (MarkupString)highlightText;
        }

        public void Search(Category category)
        {
            _searchedCharges = new();
            _searchCategory = category;
            _searchTag = new();

            SearchCharges(category);
            SearchChildren(category);
        }

        public void Search(Tag tag)
        {
            _searchedCharges = new();
            _searchedChargeTagRelations = _chargeTagRelations.Where(x => x.TagId == tag.Id).ToList();
            _searchCategory = new();
            _searchTag = tag;

            SearchCharges(tag);
            SearchChildren(tag);
        }

        public void SearchBatteries(string searchText)
        {
            _searchedBatteries = _batteries.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList();
        }

        public void SearchCharges(string searchText)
        {
            _searchedCharges = _charges.Where(x => x.Title.ToLower().Contains(searchText.ToLower())).ToList();

            foreach (var charge in _searchedCharges)
            {
                AddBatteryToResults(charge.BatteryId);
            }
        }

        public void SearchCharges(Category category)
        {
            _searchedCharges = _charges.Where(x => x.CategoryId == category.Id).ToList();

            AddBatteryToResults(category.BatteryId);
        }

        public void SearchCharges(Tag tag)
        {
            foreach (var rel in _searchedChargeTagRelations)
            {
                var charge = _charges.Where(x => x.Id == rel.ChargeId).FirstOrDefault();

                if (charge is not null && !_searchedCharges.Any(x => x.Id == charge.Id))
                {
                    _searchedCharges.Add(charge);
                }
            }

            _searchedCharges.AddRange(_charges.Where(x => x.BatteryId != tag.BatteryId).ToList());
        }

        public void SearchChildren(string searchText)
        {
            foreach (var kvp in _chargeChildren)
            {
                _searchedChargeChildren[kvp.Key] = kvp.Value.Where(x => x.Title.ToLower().Contains(searchText.ToLower())).ToList();

                if (_searchedChargeChildren[kvp.Key].Any())
                {
                    AddChargeToResults(kvp.Key);
                }
            }
        }

        public void SearchChildren(Category category)
        {
            foreach (var kvp in _chargeChildren)
            {
                _searchedChargeChildren[kvp.Key] = kvp.Value.Where(x => x.CategoryId == category.Id).ToList();

                if (_searchedChargeChildren[kvp.Key].Any())
                {
                    AddChargeToResults(kvp.Key);
                }
            }
        }

        public void SearchChildren(Tag tag)
        {
            foreach (var kvp in _chargeChildren)
            {
                List<Charge> children = new();

                foreach (var rel in _searchedChargeTagRelations)
                {
                    var child = kvp.Value.Where(x => x.Id == rel.ChargeId).Where(x => x.ParentId is not null).FirstOrDefault();

                    if (child is not null && !children.Contains(child))
                    {
                        children.Add(child);
                    }
                }

                _searchedChargeChildren[kvp.Key] = new(children);

                if (_searchedChargeChildren[kvp.Key].Any())
                {
                    AddChargeToResults(kvp.Key);
                }
            }
        }

        private void AddBatteryToResults(int batteryId)
        {
            if (!_searchedBatteries.Any(x => x.Id == batteryId))
            {
                _searchedBatteries.Add(_batteries.Where(x => x.Id == batteryId).FirstOrDefault());
            }
        }

        private void AddChargeToResults(int parentId)
        {
            if (!_searchedCharges.Any(x => x.Id == parentId))
            {
                var parent = _charges.Where(x => x.Id == parentId).FirstOrDefault();

                if (parent is not null)
                {
                    _searchedCharges.Add(parent);
                    AddBatteryToResults(parent.BatteryId);
                }
            }
        }
    }
}
