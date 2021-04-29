using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.PriorityModel;
using BatteryApp.Models.StatusModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public class BatteryAdvancedOptionsValidationService : IBatteryAdvancedOptionsValidationService
    {
        private Dictionary<string, List<string>> _errors = new();

        public Dictionary<string, List<string>> Validate(List<Category> categories, List<Priority> priorities, List<Status> statuses)
        {
            _errors.Clear();

            ValidateCategories(categories);
            ValidatePriorities(priorities);
            ValidateStatuses(statuses);

            return _errors;
        }

        private void AddToErrors(string key, List<string> messages)
        {
            List<string> filteredMessages = messages.Where(x => !String.IsNullOrWhiteSpace(x)).ToList();

            if (filteredMessages.Any())
            {
                _errors.Add(key, filteredMessages);
            }
        }

        private void ValidateCategories(List<Category> categories)
        {
            List<string> messages = new();

            messages.Add(ValidateCategories_EmptyList(categories));
            messages.Add(ValidateCategories_DefaultChargeCategory(categories));
            messages.Add(ValidateCategories_DefaultChildCategory(categories));

            AddToErrors("Categories", messages);
        }

        private static string ValidateCategories_EmptyList(List<Category> categories)
        {
            string tempMessage = "";

            if (!categories.Any())
            {
                tempMessage = "List is empty.  Please enter a Category or reset defaults.";
            }

            return tempMessage;
        }

        private static string ValidateCategories_DefaultChargeCategory(List<Category> categories)
        {
            List<Category> defaultCategories = new();
            
            string tempMessage = "";

            foreach (var category in categories)
            {
                if (category.IsDefaultChargeCategory)
                {
                    defaultCategories.Add(category);
                }
            }

            if (defaultCategories.Count > 1)
            {
                tempMessage = "Multiple default charge categories.  Please set one category to be the default.";
            }

            if (defaultCategories.Count == 0)
            {
                tempMessage = "No categories set to be default charge category.  Please set one category to be the default.";
            }

            return tempMessage;
        }

        private static string ValidateCategories_DefaultChildCategory(List<Category> categories)
        {
            List<Category> defaultCategories = new();

            string tempMessage = "";

            foreach (var category in categories)
            {
                if (category.IsDefaultChildCategory)
                {
                    defaultCategories.Add(category);
                }
            }

            if (defaultCategories.Count > 1)
            {
                tempMessage = "Multiple default child categories.  Please set one category to be the default.";
            }

            if (defaultCategories.Count == 0)
            {
                tempMessage = "No categories set to be default child category.  Please set one category to be the default.";
            }

            return tempMessage;
        }

        private void ValidatePriorities(List<Priority> priorities)
        {
            List<string> messages = new();

            messages.Add(ValidatePriorities_EmptyList(priorities));
            messages.Add(ValidatePriorities_DefaultPriority(priorities));
            messages.Add(ValidatePriorities_DuplicateSeverity(priorities));
            messages.Add(ValidatePriorities_NegativeSeverity(priorities));

            AddToErrors("Priorities", messages);
        }

        private static string ValidatePriorities_EmptyList(List<Priority> priorities)
        {
            string tempMessage = "";

            if (!priorities.Any())
            {
                tempMessage = "List is empty.  Please enter a priority or reset defaults.";
            }

            return tempMessage;
        }

        private static string ValidatePriorities_DefaultPriority(List<Priority> priorities)
        {
            List<Priority> defaultPriorities = new();

            string tempMessage = "";

            foreach (var priority in priorities)
            {
                if (priority.IsDefault)
                {
                    defaultPriorities.Add(priority);
                }
            }

            if (defaultPriorities.Count > 1)
            {
                tempMessage = "Multiple default priorities.  Please set one Priority to be the default.";
            }

            if (defaultPriorities.Count == 0)
            {
                tempMessage = "No priorities set to be default priority.  Please set one priority to be the default.";
            }

            return tempMessage;
        }

        private static string ValidatePriorities_DuplicateSeverity(List<Priority> priorities)
        {
            string tempMessage = "";

            var hasDuplicateSeverityPriorities = priorities.GroupBy(x => x.Severity).Any(g => g.Count() > 1);

            if (hasDuplicateSeverityPriorities)
            {
                tempMessage = "Duplicate Severity.  Please modify the severities to be unique.";
            }

            return tempMessage;
        }

        private static string ValidatePriorities_NegativeSeverity(List<Priority> priorities)
        {
            List<Priority> negativeSeverityPriorities = new();
            
            string tempMessage = "";

            foreach (var priority in priorities)
            {
                if (priority.Severity < 0)
                {
                    negativeSeverityPriorities.Add(priority);
                }
            }

            if (negativeSeverityPriorities.Count > 0)
            {
                tempMessage = "Negative Severity.  Please modify Severity value to be a positive integer.";
            }

            return tempMessage;
        }

        private void ValidateStatuses(List<Status> statuses)
        {
            List<string> messages = new();

            messages.Add(ValidateStatuses_EmptyList(statuses));
            messages.Add(ValidateStatuses_DuplicateOrder(statuses));
            messages.Add(ValidateStatuses_NegativeOrder(statuses));

            AddToErrors("Statuses", messages);
        }

        private static string ValidateStatuses_EmptyList(List<Status> statuses)
        {
            string tempMessage = "";

            if (!statuses.Any())
            {
                tempMessage = "List is empty.  Please enter a Status or reset defaults.";
            }

            return tempMessage;
        }

        private static string ValidateStatuses_DuplicateOrder(List<Status> statuses)
        {
            string tempMessage = "";

            var hasDuplicateSeverityStatuses = statuses.GroupBy(x => x.Order).Any(g => g.Count() > 1);

            if (hasDuplicateSeverityStatuses)
            {
                tempMessage = "Duplicate Order.  Please modify the orders to be unique.";
            }

            return tempMessage;
        }

        private static string ValidateStatuses_NegativeOrder(List<Status> statuses)
        {
            List<Status> negativeOrderStatuses = new();

            string tempMessage = "";

            foreach (var status in statuses)
            {
                if (status.Order < 0)
                {
                    negativeOrderStatuses.Add(status);
                }
            }

            if (negativeOrderStatuses.Count > 0)
            {
                tempMessage = "Negative Order.  Please modify Order value to be a positive integer.";
            }

            return tempMessage;
        }
    }
}
