using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.TagModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public interface ITagController
    {
        Task AddRelationshipAsync(int chargeId, int tagId);
        Task AddTagFromChargeAsync(int chargeId, Tag tag);
        Task DeleteAllRelationshipsForChargeAsync(int chargeId);
        Task DeleteTagAndAllRelationshipsAsync(int tagId);
        Task<List<Tag>> GetAllTagsForBatteryAsync(Battery battery);
        Task<List<Tag>> GetAllTagsForChargeAsync(int chargeId);
        Task RemoveTagFromChargeAsync(int chargeId, int tagId);
        Tag SetDefaults(Charge charge, Tag tag);
    }
}