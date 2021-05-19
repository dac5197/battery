using BatteryApp.Models.StatusModel;
using System.Collections.Generic;

namespace BatteryApp.Internals.Validators
{
    public interface IStatusValidator
    {
        Dictionary<string, List<string>> Validate(List<Status> statuses);
    }
}