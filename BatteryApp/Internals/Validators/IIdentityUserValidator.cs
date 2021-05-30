using System.Collections.Generic;

namespace BatteryApp.Internals.Validators
{
    public interface IIdentityUserValidator
    {
        void ClearErrors();
        Dictionary<string, List<string>> GetErrors();
        void Validate(string fieldName, string fieldValue, bool isEmail);
    }
}