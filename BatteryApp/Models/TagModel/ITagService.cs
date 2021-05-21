using BatteryApp.Models.BatteryModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.TagModel
{
    public interface ITagService
    {
        Task<Tag> Add(Tag tag);
        Task Delete(int id);
        Task<List<Tag>> Get();
        Task<List<Tag>> Get(Battery battery);
        Task<List<Tag>> Get(string userId);
        Task<Tag> Get(int id);
        Task<Tag> Update(Tag tag);
    }
}