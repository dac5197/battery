using BatteryApp.Internals.Validators;
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
        private readonly ICategoryValidator _categoryValidator;
        private readonly IPriorityValidator _priorityValidator;

        public BatteryAdvancedOptionsValidationService(ICategoryValidator categoryValidator, IPriorityValidator priorityValidator)
        {
            _categoryValidator = categoryValidator;
            _priorityValidator = priorityValidator;
        }

        public Dictionary<string, List<string>> Validate(List<Category> categories, List<Priority> priorities, List<Status> statuses)
        {
            ClearErrors();

            AddToErrors(_categoryValidator.Validate(categories));
            AddToErrors(_priorityValidator.Validate(priorities));
            ValidateStatuses(statuses);

            return _errors;
        }

        public Dictionary<string, List<string>> Validate(List<Category> categories)
        {
            ClearErrors();

            AddToErrors(_categoryValidator.Validate(categories));

            return _errors;
        }

        public Dictionary<string, List<string>> Validate(List<Priority> priorities)
        {
            ClearErrors();

            AddToErrors(_priorityValidator.Validate(priorities));

            return _errors;
        }

        private void AddToErrors(Dictionary<string, List<string>> errorDict)
        {
            foreach (var item in errorDict)
            {
                _errors.Add(item.Key, item.Value);
            }
        }

        private void AddToErrors(string key, List<string> messages)
        {
            List<string> filteredMessages = messages.Where(x => !String.IsNullOrWhiteSpace(x)).ToList();

            if (filteredMessages.Any())
            {
                _errors.Add(key, filteredMessages);
            }
        }

        public void ClearErrors()
        {
            _errors.Clear();
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
