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
        //private string _searchText;

        public void Clear()
        {
            _searchedBatteries = new(_batteries);
            _searchedCharges = new(_charges);
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

        public List<Battery> GetBatteries()
        {
            return _searchedBatteries;
        }

        public List<Charge> GetCharges()
        {
            return _searchedCharges;
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
                AddBatteryToResults(charge);
            }
        }

        private void AddBatteryToResults(Charge charge)
        {
            if (!_searchedBatteries.Any(x => x.Id == charge.BatteryId))
            {
                _searchedBatteries.Add(_batteries.Where(x => x.Id == charge.BatteryId).FirstOrDefault());
            }
        }


    }
}
