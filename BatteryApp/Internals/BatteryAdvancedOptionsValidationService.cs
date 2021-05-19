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
        private readonly IStatusValidator _statusValidator;

        public BatteryAdvancedOptionsValidationService(ICategoryValidator categoryValidator, IPriorityValidator priorityValidator, IStatusValidator statusValidator)
        {
            _categoryValidator = categoryValidator;
            _priorityValidator = priorityValidator;
            _statusValidator = statusValidator;
        }

        public Dictionary<string, List<string>> Validate(List<Category> categories, List<Priority> priorities, List<Status> statuses)
        {
            ClearErrors();

            AddToErrors(_categoryValidator.Validate(categories));
            AddToErrors(_priorityValidator.Validate(priorities));
            AddToErrors(_statusValidator.Validate(statuses));

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

        public Dictionary<string, List<string>> Validate(List<Status> statuses)
        {
            ClearErrors();

            AddToErrors(_statusValidator.Validate(statuses));

            return _errors;
        }

        private void AddToErrors(Dictionary<string, List<string>> errorDict)
        {
            foreach (var item in errorDict)
            {
                _errors.Add(item.Key, item.Value);
            }
        }

        public void ClearErrors()
        {
            _errors.Clear();
        }
    }
}
