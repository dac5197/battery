using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.PriorityModel;
using BatteryApp.Models.StatusModel;
using System.Collections.Generic;

namespace BatteryApp.Views.Utils
{
    public interface ITableSort_Charges
    {
        string GetIcon(string columnName);
        List<Charge> Sort(string columnName, List<Charge> charges, List<Priority> priorities = null, List<Status> statuses = null);
    }
}