using BatteryApp.Models.CategoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public class BatteryAdvancedOptionsValidationService : IBatteryAdvancedOptionsValidationService
    {
        private Dictionary<string, List<string>> _errors = new();
        private List<string> _messages = new();

        public Dictionary<string, List<string>> Validate(List<Category> categories)
        {
            _errors.Clear();
            _messages.Clear();

            ValidateInitialCategories(categories);

            return _errors;
        }

        private void ValidateInitialCategories(List<Category> categories)
        {
            ValidateCategories_EmptyList(categories);
            ValidateCategories_DefaultChargeCategory(categories);
            ValidateCategories_DefaultChildCategory(categories);

            _errors.Add("Categories", _messages);
        }

        private void ValidateCategories_EmptyList(List<Category> categories)
        {
            if (!categories.Any())
            {
                _messages.Add("List is empty.  Please enter a category or reset defaults.");
            }
        }

        private void ValidateCategories_DefaultChargeCategory(List<Category> categories)
        {
            List<Category> defaultCategories = new();

            foreach (var category in categories)
            {
                if (category.IsDefaultChargeCategory)
                {
                    defaultCategories.Add(category);
                }
            }

            if (defaultCategories.Count > 1)
            {
                //_errors.Add("Categories", new List<string>() { "Multiple default charge categories.  Please set one category to be the default." });
                _messages.Add("Multiple default charge categories.  Please set one category to be the default.");
            }

            if (defaultCategories.Count == 0)
            {
                _messages.Add("No categories set to be default charge category.  Please set one category to be the default.");
            }
        }

        private void ValidateCategories_DefaultChildCategory(List<Category> categories)
        {
            List<Category> defaultCategories = new();

            foreach (var category in categories)
            {
                if (category.IsDefaultChildCategory)
                {
                    defaultCategories.Add(category);
                }
            }

            if (defaultCategories.Count > 1)
            {
                _messages.Add("Multiple default child categories.  Please set one category to be the default.");
            }

            if (defaultCategories.Count == 0)
            {
                _messages.Add("No categories set to be default child category.  Please set one category to be the default.");
            }
        }

    }
}
