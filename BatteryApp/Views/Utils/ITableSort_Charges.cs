using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.PriorityModel;
using BatteryApp.Models.StatusModel;
using System.Collections.Generic;

namespace BatteryApp.Views.Utils
{
    public interface ITableSort_Charges
    {
        string GetIcon(string columnName);
        void Reset();
        List<Charge> Sort(string columnName,
                          List<Charge> charges,
                          List<Priority> priorities = null,
                          List<Status> statuses = null,
                          List<ChargeChildrenCount> chargeChildrenCounts = null,
                          List<Category> categories = null);
    }
}