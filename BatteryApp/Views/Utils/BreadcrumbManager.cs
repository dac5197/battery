using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Views.Utils
{
    public class BreadcrumbManager : IBreadcrumbManager
    {
        public bool IsVisible { get; set; } = false;

        public List<BreadcrumbLink> Links { get; set; }

        public void ClearLinks()
        {
            Links?.Clear();
        }

        public void AddLink(BreadcrumbLink link)
        {
            Links.Add(link);
        }

        public void SetLinks(List<BreadcrumbLink> links)
        {
            Links = links;
            NotifyStateChanged();
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
