using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.TagModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public interface ITagController
    {
        Task AddRelationshipAsync(int chargeId, Tag tag);
        Task AddTagFromChargeAsync(int chargeId, Tag tag);
        Task DeleteAllRelationshipsForChargeAsync(int chargeId);
        Task DeleteTagAndAllRelationshipsAsync(int tagId);
        Task<List<Tag>> GetAllTagsAsync(Battery battery);
        Task<List<Tag>> GetAllTagsAsync(int chargeId);
        Task<List<Charge>> GetAllChargesAsync(Tag tag, Battery battery);
        Task<List<ChargeTagRelation>> GetAllRelationsForBattery(Battery battery);
        Task RemoveTagFromChargeAsync(int chargeId, Tag tag);
        Tag SetDefaults(Charge charge, Tag tag);
        Tag SetDefaults(Battery battery, Tag tag);
        Task<Dictionary<int, int>> CountAllChargeTagRelationshipsAsync(Battery battery);
        Task<Dictionary<int, int>> CountAllChargeTagRelationshipsAsync(string userId);
        Task<Dictionary<int, int>> CountActiveChargeTagRelationshipsAsync(Battery battery);
        Task<Dictionary<int, int>> CountActiveChargeTagRelationshipsAsync(string userId);
    }
}