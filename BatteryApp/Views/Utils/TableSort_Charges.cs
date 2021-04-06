using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.PriorityModel;
using BatteryApp.Models.StatusModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Views.Utils
{
    public class TableSort_Charges : ITableSort_Charges
    {
        private bool _isSortedAscending;

        private string _currentSortColumn;

        public string GetIcon(string columnName)
        {
            if (_currentSortColumn != columnName)
            {
                return string.Empty;
            }

            if (_isSortedAscending )
            {
                return "oi-caret-top";
            }
            else
            {
                return "oi-caret-bottom";
            }
        }

        public void Reset()
        {
            _isSortedAscending = false;
            _currentSortColumn = "";
        }

        public List<Charge> Sort(string columnName,
                                 List<Charge> charges,
                                 List<Priority> priorities = null,
                                 List<Status> statuses = null,
                                 List<ChargeChildrenCount> chargeChildrenCounts = null,
                                 List<Category> categories = null)
        {
            if (columnName != _currentSortColumn)
            {
                charges = SortAscending(columnName, charges, priorities, statuses, chargeChildrenCounts, categories);
                _currentSortColumn = columnName;
                _isSortedAscending = true;
            }
            else
            {
                if (_isSortedAscending)
                {
                    charges = SortDescending(columnName, charges, priorities, statuses, chargeChildrenCounts, categories);
                }
                else
                {
                    charges = SortAscending(columnName, charges, priorities, statuses, chargeChildrenCounts, categories);
                }
                _isSortedAscending = !_isSortedAscending;
            }

            return charges;
        }

        private static List<Charge> SortAscending(string columnName,
                                                  List<Charge> charges,
                                                  List<Priority> priorities = null,
                                                  List<Status> statuses = null,
                                                  List<ChargeChildrenCount> chargeChildrenCounts = null,
                                                  List<Category> categories = null)
        {
            switch (columnName)
            {
                case "Category":
                    charges = (from charge in charges
                               join category in categories
                               on charge.CategoryId equals category.Id
                               orderby category.Name
                               select charge).ToList();
                    break;

                case "Children":
                    charges = (from charge in charges
                               join count in chargeChildrenCounts
                               on charge.Id equals count.ChargeId
                               orderby count.ChildrenCount
                               select charge).ToList();
                    break;

                case "Priority":
                    charges = (from charge in charges
                               join priority in priorities
                               on charge.PriorityId equals priority.Id
                               orderby priority.Severity
                               select charge).ToList();
                    break;

                case "Status":
                    charges = (from charge in charges
                               join status in statuses
                               on charge.StatusId equals status.Id
                               orderby status.Order
                               select charge).ToList();
                    break;

                default:
                    charges = charges.OrderBy(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
                    break;
            }

            return charges;
        }

        private static List<Charge> SortDescending(string columnName,
                                                   List<Charge> charges,
                                                   List<Priority> priorities = null,
                                                   List<Status> statuses = null,
                                                   List<ChargeChildrenCount> chargeChildrenCounts = null,
                                                   List<Category> categories = null)
        {
            switch (columnName)
            {
                case "Category":
                    charges = (from charge in charges
                               join category in categories
                               on charge.CategoryId equals category.Id
                               orderby category.Name descending
                               select charge).ToList();
                    break;

                case "Children":
                    charges = (from charge in charges
                               join count in chargeChildrenCounts
                               on charge.Id equals count.ChargeId
                               orderby count.ChildrenCount descending
                               select charge).ToList();
                    break;

                case "Priority":
                    charges = (from charge in charges
                               join priority in priorities
                               on charge.PriorityId equals priority.Id
                               orderby priority.Severity descending
                               select charge).ToList();
                    break;

                case "Status":
                    charges = (from charge in charges
                               join status in statuses
                               on charge.StatusId equals status.Id
                               orderby status.Order descending
                               select charge).ToList();
                    break;

                default:
                    charges = charges.OrderByDescending(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
                    break;
            }

            return charges;
        }
    }
}
