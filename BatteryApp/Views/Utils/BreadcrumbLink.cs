using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.ChargeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Views.Utils
{
    public class BreadcrumbLink
    {
        public string Url { get; set; }
        public string Label { get; set; }

        public void Initialize(string url, string label)
        {
            Url = url;
            Label = label;
        }
    }
}
