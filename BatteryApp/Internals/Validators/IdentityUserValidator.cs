using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BatteryApp.Internals.Validators
{
    public class IdentityUserValidator : BaseValidator, IIdentityUserValidator
    {
        public Dictionary<string, List<string>> GetErrors()
        {
            return _errors;
        }

        public void Validate(string fieldName, string fieldValue, bool isEmail)
        {
            //ClearErrors();

            List<string> messages = new();

            messages.Add(Validate_NullOrWhiteSpace(fieldName, fieldValue));

            if (isEmail && !String.IsNullOrWhiteSpace(fieldValue))
            {
                messages.Add(Validate_EmailAddress(fieldName, fieldValue));
            }

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

        private static string Validate_EmailAddress(string fieldName, string fieldValue)
        {
            string tempMessage;

            try
            {
                MailAddress m = new MailAddress(fieldValue);
                tempMessage = "";
            }
            catch (FormatException)
            {
                tempMessage = "Email is invalid.  Please enter a valid email address.";
            }
            return tempMessage;
        }

    }
}
