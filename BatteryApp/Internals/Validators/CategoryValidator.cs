using BatteryApp.Models.CategoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals.Validators
{
    public class CategoryValidator : BaseValidator, ICategoryValidator
    {
        public Dictionary<string, List<string>> Validate(List<Category> categories)
        {
            ClearErrors();

            List<string> messages = new();

            messages.Add(ValidateCategories_EmptyList(categories));
            messages.Add(ValidateCategories_DefaultChargeCategory(categories));
            messages.Add(ValidateCategories_DefaultChildCategory(categories));

            AddToErrors("Categories", messages);

            ValidateCategories_NameLength(categories);
            ValidateCategories_NameIsNullOrWhiteSpace(categories);
            ValidateCategories_IconIsNullOrWhiteSpace(categories);

            return _errors;
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

        private void ValidateCategories_NameLength(List<Category> categories)
        {

            List<string> tempMessages = new();

            string tempMessage = "Name is too long.  Please modify Name to be 10 characters or less.";
            tempMessages.Add(tempMessage);

            foreach (var category in categories)
            {
                if (category.Name?.Length > 10)
                {
                    string tempKey = $"Category Name '{category.Name}'";
                    AddToErrors(tempKey, tempMessages);
                }
            }
        }

        private void ValidateCategories_NameIsNullOrWhiteSpace(List<Category> categories)
        {

            List<string> tempMessages = new();

            string tempMessage = "Name is invalid.  Please re-enter Name to be 10 characters or less.";
            tempMessages.Add(tempMessage);

            foreach (var category in categories)
            {
                if (String.IsNullOrWhiteSpace(category.Name))
                {
                    string tempKey = $"Category Name '{category.Name}'";
                    AddToErrors(tempKey, tempMessages);
                }
            }
        }

        private void ValidateCategories_IconIsNullOrWhiteSpace(List<Category> categories)
        {

            List<string> tempMessages = new();

            string tempMessage = "Icon is invalid.  Please re-select an Icon.";
            tempMessages.Add(tempMessage);

            foreach (var category in categories)
            {
                if (String.IsNullOrWhiteSpace(category.Icon))
                {
                    string tempKey = $"Category Icon '{category.Name}'";
                    AddToErrors(tempKey, tempMessages);
                }
            }
        }
    }
}
