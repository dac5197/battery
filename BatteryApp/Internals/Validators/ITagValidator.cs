using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.TagModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Internals.Validators
{
    public interface ITagValidator
    {
        void ClearErrors();
        Task<Dictionary<string, List<string>>> ValidateAsync(Tag tag, Battery battery);
    }
}