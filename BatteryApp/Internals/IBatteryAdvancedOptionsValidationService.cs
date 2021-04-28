using BatteryApp.Models.CategoryModel;
using System.Collections.Generic;

namespace BatteryApp.Internals
{
    public interface IBatteryAdvancedOptionsValidationService
    {
        Dictionary<string, List<string>> Validate(List<Category> categories);
    }
}