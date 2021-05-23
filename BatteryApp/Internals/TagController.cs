using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.NoteModel;
using BatteryApp.Models.TagModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public class TagController : ITagController
    {
        private readonly IChargeService _chargeService;
        private readonly IChargeTagRelationService _chargeTagRelationService;
        private readonly INoteService _noteService;
        private readonly ITagService _tagService;

        public TagController(IChargeService chargeService, IChargeTagRelationService chargeTagRelationService, INoteService noteService, ITagService tagService)
        {
            _chargeService = chargeService;
            _chargeTagRelationService = chargeTagRelationService;
            _noteService = noteService;
            _tagService = tagService;
        }

        public async Task<List<Tag>> GetAllTagsForBatteryAsync(Battery battery)
        {
            var tags = await _tagService.Get(battery);
            return tags;
        }

        public async Task<List<Tag>> GetAllTagsForChargeAsync(int chargeId)
        {
            var relations = await _chargeTagRelationService.GetAllRelationsForCharge(chargeId);
            var tags = await _tagService.Get();
            var output = tags.Where(t => relations.Any(r => r.TagId == t.Id)).ToList();

            return output;
        }

        public async Task AddTagFromChargeAsync(int chargeId, Tag tag)
        {
            var newTag = await _tagService.Add(tag);
            await _chargeTagRelationService.Add(chargeId, newTag.Id);
            await _noteService.AddTagHistoryNote(chargeId, tag);
            await _chargeService.SetUpdated(chargeId);
        }

        public async Task AddRelationshipAsync(int chargeId, Tag tag)
        {
            var relation = await _chargeTagRelationService.Get(chargeId, tag.Id);

            if (relation is null)
            {
                await _chargeTagRelationService.Add(chargeId, tag.Id);
                await _noteService.AddTagHistoryNote(chargeId, tag);
                await _chargeService.SetUpdated(chargeId);
            }
        }

        public async Task RemoveTagFromChargeAsync(int chargeId, Tag tag)
        {
            await _noteService.RemoveTagHistoryNote(chargeId, tag);
            await _chargeTagRelationService.Delete(chargeId, tag.Id);
            await _chargeService.SetUpdated(chargeId);
        }

        public async Task DeleteTagAndAllRelationshipsAsync(int tagId)
        {
            var relations = await _chargeTagRelationService.GetAllRelationsForTag(tagId);

            foreach (var relation in relations)
            {
                await _chargeTagRelationService.Delete(relation.Id);
            }

            await _tagService.Delete(tagId);
        }

        public async Task DeleteAllRelationshipsForChargeAsync(int chargeId)
        {
            var relations = await _chargeTagRelationService.GetAllRelationsForCharge(chargeId);

            foreach (var relation in relations)
            {
                await _chargeTagRelationService.Delete(relation.Id);
            }
        }

        public Tag SetDefaults(Charge charge, Tag tag)
        {
            tag.BatteryId = charge.BatteryId;
            tag.OwnerId = charge.OwnerId;

            return tag;
        }

        public Tag SetDefaults(Battery battery, Tag tag)
        {
            tag.BatteryId = battery.Id;
            tag.OwnerId = battery.OwnerId;

            return tag;
        }

        public async Task<Dictionary<int, int>> CountAllChargeTagRelationshipsAsync(Battery battery)
        {
            Dictionary<int, int> counts = new();

            var tags = await _tagService.Get(battery);

            foreach (var tag in tags)
            {
                var relations = await _chargeTagRelationService.GetAllRelationsForTag(tag.Id);

                counts.Add(tag.Id, relations.Count);
            }

            return counts;
        }

        public async Task<Dictionary<int, int>> CountAllChargeTagRelationshipsAsync(string userId)
        {
            Dictionary<int, int> counts = new();

            var tags = await _tagService.Get(userId);

            foreach (var tag in tags)
            {
                var relations = await _chargeTagRelationService.GetAllRelationsForTag(tag.Id);

                counts.Add(tag.Id, relations.Count);
            }

            return counts;
        }

        public async Task<Dictionary<int, int>> CountActiveChargeTagRelationshipsAsync(Battery battery)
        {
            Dictionary<int, int> counts = new();
            var tags = await _tagService.Get(battery);

            var combinedCharges = await _chargeService.GetActiveParentsAndChildren(battery);

            foreach (var tag in tags)
            {
                var relations = await _chargeTagRelationService.GetAllRelationsForTag(tag.Id);

                var relationsCount = (from c in combinedCharges
                                      join r in relations on c.Id equals r.ChargeId
                                      select r
                                      ).Count();

                counts.Add(tag.Id, relationsCount);
            }

            return counts;
        }

        public async Task<Dictionary<int, int>> CountActiveChargeTagRelationshipsAsync(string userId)
        {
            Dictionary<int, int> counts = new();
            var tags = await _tagService.Get(userId);

            var combinedCharges = await _chargeService.GetActiveParentsAndChildren(userId);

            foreach (var tag in tags)
            {
                var relations = await _chargeTagRelationService.GetAllRelationsForTag(tag.Id);

                var relationsCount = (from c in combinedCharges
                                      join r in relations on c.Id equals r.ChargeId
                                      select r
                                      ).Count();

                counts.Add(tag.Id, relationsCount);
            }

            return counts;
        }
    }
}
