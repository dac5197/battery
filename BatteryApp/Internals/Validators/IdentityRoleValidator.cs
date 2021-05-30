using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals.Validators
{
    public class IdentityRoleValidator : BaseValidator, IIdentityRoleValidator
    {
        public Dictionary<string, List<string>> GetErrors()
        {
            return _errors;
        }

        public void Validate(string fieldName, string fieldValue)
        {
            List<string> messages = new();

            messages.Add(Validate_NullOrWhiteSpace(fieldName, fieldValue));

            AddToErrors(fieldName, messages);
        }

        private static string Validate_NullOrWhiteSpace(string fieldName, string fieldValue)
        {
            string tempMessage = "";

            if (String.IsNullOrWhiteSpace(fieldValue))
            {
                tempMessage = $"Value is empty.  Please enter a {fieldName}.";
            }

            return tempMessage;
        }
    }
}
