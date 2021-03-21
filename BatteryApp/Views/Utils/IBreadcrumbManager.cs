using System;
using System.Collections.Generic;

namespace BatteryApp.Views.Utils
{
    public interface IBreadcrumbManager
    {
        bool IsVisible { get; set; }
        List<BreadcrumbLink> Links { get; set; }

        event Action OnChange;

        void AddLink(BreadcrumbLink link);
        void ClearLinks();
        void Hide();
        void SetLinks(List<BreadcrumbLink> links);
        void Show();
    }
}