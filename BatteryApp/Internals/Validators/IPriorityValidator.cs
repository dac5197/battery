using BatteryApp.Models.PriorityModel;
using System.Collections.Generic;

namespace BatteryApp.Internals.Validators
{
    public interface IPriorityValidator
    {
        Dictionary<string, List<string>> Validate(List<Priority> priorities);
    }
}