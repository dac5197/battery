using BatteryApp.Models.BatteryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.TagModel
{
    public interface ITagService
    {
        Task<Tag> AddAsync(Tag tag);
        Task DeleteAsync(int id);
        Task<List<Tag>> GetAsync();
        Task<List<Tag>> GetAsync(Battery battery);
        Task<List<Tag>> GetAsync(string userId);
        Task<Tag> GetAsync(int id);
        Task<Tag> UpdateAsync(Tag tag);
    }
}