using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals.Validators
{
    public class BaseValidator
    {
        protected Dictionary<string, List<string>> _errors = new();

        public void AddToErrors(string key, List<string> messages)
        {
            List<string> filteredMessages = messages.Where(x => !String.IsNullOrWhiteSpace(x)).ToList();

            if (filteredMessages.Any())
            {
                key = CheckDuplicateKey(key);
                _errors.Add(key, filteredMessages);
            }
        }

        public void ClearErrors()
        {
            _errors.Clear();
        }

        private string CheckDuplicateKey(string key)
        {
            int i = 2;

            string tempKey = new(key);

            while (_errors.ContainsKey(tempKey))
            {
                tempKey = $"{key} ({i})";
                i++;
            }

            return tempKey;
        }
    }
}
