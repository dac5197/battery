using BatteryApp.Models.StatusModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals.Validators
{
    public class StatusValidator : BaseValidator, IStatusValidator
    {
        public Dictionary<string, List<string>> Validate(List<Status> statuses)
        {
            ClearErrors();

            List<string> messages = new();

            messages.Add(ValidateStatuses_EmptyList(statuses));
            messages.Add(ValidateStatuses_DuplicateOrder(statuses));
            messages.Add(ValidateStatuses_NegativeOrder(statuses));

            AddToErrors("Statuses", messages);

            ValidateStatuses_NameLength(statuses);
            ValidateStatuses_NameIsNullOrWhiteSpace(statuses);

            return _errors;
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

        private void ValidateStatuses_NameLength(List<Status> statuses)
        {

            List<string> tempMessages = new();

            string tempMessage = "Name is too long.  Please modify Name to be 20 characters or less.";
            tempMessages.Add(tempMessage);

            foreach (var status in statuses)
            {
                if (status.Name?.Length > 20)
                {
                    string tempKey = $"Status '{status.Name}'";
                    AddToErrors(tempKey, tempMessages);
                }
            }
        }

        private void ValidateStatuses_NameIsNullOrWhiteSpace(List<Status> statuses)
        {

            List<string> tempMessages = new();

            string tempMessage = "Name is invalid.  Please re-enter Name to be 20 characters or less.";
            tempMessages.Add(tempMessage);

            foreach (var status in statuses)
            {
                if (String.IsNullOrWhiteSpace(status.Name))
                {
                    string tempKey = $"Priority '{status.Name}'";
                    AddToErrors(tempKey, tempMessages);
                }
            }
        }
    }
}
