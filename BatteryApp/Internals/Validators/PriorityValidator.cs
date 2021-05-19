using BatteryApp.Models.PriorityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals.Validators
{
    public class PriorityValidator : BaseValidator, IPriorityValidator
    {
        public Dictionary<string, List<string>> Validate(List<Priority> priorities)
        {
            ClearErrors();

            List<string> messages = new();

            messages.Add(ValidatePriorities_EmptyList(priorities));
            messages.Add(ValidatePriorities_DefaultPriority(priorities));
            messages.Add(ValidatePriorities_DuplicateSeverity(priorities));
            messages.Add(ValidatePriorities_NegativeSeverity(priorities));

            AddToErrors("Priorities", messages);

            ValidatePriorities_NameLength(priorities);
            ValidatePriorities_NameIsNullOrWhiteSpace(priorities);

            return _errors;
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

        private void ValidatePriorities_NameLength(List<Priority> priorities)
        {

            List<string> tempMessages = new();

            string tempMessage = "Name is too long.  Please modify Name to be 10 characters or less.";
            tempMessages.Add(tempMessage);

            foreach (var priority in priorities)
            {
                if (priority.Name?.Length > 10)
                {
                    string tempKey = $"Priority '{priority.DisplayName}'";
                    AddToErrors(tempKey, tempMessages);
                }
            }
        }

        private void ValidatePriorities_NameIsNullOrWhiteSpace(List<Priority> priorities)
        {

            List<string> tempMessages = new();

            string tempMessage = "Name is invalid.  Please re-enter Name to be 10 characters or less.";
            tempMessages.Add(tempMessage);

            foreach (var priority in priorities)
            {
                if (String.IsNullOrWhiteSpace(priority.Name))
                {
                    string tempKey = $"Priority '{priority.DisplayName}'";
                    AddToErrors(tempKey, tempMessages);
                }
            }
        }
    }
}
