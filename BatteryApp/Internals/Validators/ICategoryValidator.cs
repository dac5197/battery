using BatteryApp.Models.CategoryModel;
using System.Collections.Generic;

namespace BatteryApp.Internals.Validators
{
    public interface ICategoryValidator
    {
        Dictionary<string, List<string>> Validate(List<Category> categories);
    }
}