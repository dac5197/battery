using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.ChargeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Views.Utils
{
    public class SearchUtil : ISearchUtil
    {
        private List<Battery> _batteries = new();
        private List<Battery> _searchedBatteries = new();
        private List<Charge> _charges = new();
        private List<Charge> _searchedCharges = new();
        private List<Charge> _children = new();
        private List<Charge> _searchedChildren = new();
        private Dictionary<int, List<Charge>> _chargeChildren = new();
        private Dictionary<int, List<Charge>> _searchedChargeChildren = new();
        //private string _searchText;

        public void Clear()
        {
            _searchedBatteries = new(_batteries);
            _searchedCharges = new(_charges);
            _searchedChargeChildren = new(_chargeChildren);
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

        public void Initialize(List<Battery> batteries, List<Charge> charges)
        {
            Initialize(batteries);
            Initialize(charges);
        }

        public void InitializeChildren(List<Charge> children)
        {
            _children = new(children);
            _searchedChildren = new(children);
            
        }

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

        public List<Charge> GetChildren()
        {
            return _searchedChildren;
        }

        public List<Charge> GetChildren(int parentId)
        {
            return _searchedChargeChildren[parentId];
        }

        public bool HasChildren(int parentId)
        {
            if (_searchedChargeChildren.ContainsKey(parentId))
            {
                return true;
            }

            return false;
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

        public void SearchChildren(string searchText)
        {
            //_searchedChildren = _children.Where(x => x.Title.ToLower().Contains(searchText.ToLower())).ToList();

            //foreach (var child in _searchedChildren)
            //{
            //    AddBatteryToResults(child);
            //    AddChargeToResults(child);
            //}

            _searchedChargeChildren = new();

            foreach (var kvp in _chargeChildren)
            {
                //_searchedChargeChildren[kvp.Key] = kvp.Value.Where(x => x.Title.ToLower().Contains(searchText.ToLower())).ToList();

                _searchedChargeChildren.Add(kvp.Key, kvp.Value.Where(x => x.Title.ToLower().Contains(searchText.ToLower())).ToList());

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
                _searchedCharges.Add(parent);

                AddBatteryToResults(parent.BatteryId);
            }
        }
    }
}
