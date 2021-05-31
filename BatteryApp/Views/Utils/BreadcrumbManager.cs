using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.ChargeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Views.Utils
{
    public class BreadcrumbManager : IBreadcrumbManager
    {
        private readonly IChargeService _chargeService;
        private readonly ICategoryService _categoryService;

        public BreadcrumbManager(IChargeService chargeService, ICategoryService categoryService)
        {
            _chargeService = chargeService;
            _categoryService = categoryService;
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

        public async Task InitializeAsync(Charge charge)
        {
            ClearLinks();
            await AddChargeLinkAsync(charge);
            AddBatteryLink(charge.BatteryId);

            Links.Reverse();
        }

        public async Task AddChargeLinkAsync(Charge charge)
        {
            Category category = await _categoryService.GetAsync(charge.CategoryId);

            string url = $"/charge/{charge.Id}";
            string label = $"{category.Name}{charge.Id}";

            AddLink(url, label);

            if (charge.ParentId is not null)
            {
                Charge parent = await _chargeService.GetAsync((int)charge.ParentId);
                await AddChargeLinkAsync(parent);
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
