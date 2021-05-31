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

        public async Task<List<Tag>> GetAllTagsAsync(Battery battery)
        {
            var tags = await _tagService.GetAsync(battery);
            return tags;
        }

        public async Task<List<Tag>> GetAllTagsAsync(int chargeId)
        {
            var relations = await _chargeTagRelationService.GetAllRelationsForChargeAsync(chargeId);
            var tags = await _tagService.GetAsync();
            var output = tags.Where(t => relations.Any(r => r.TagId == t.Id)).ToList();

            return output;
        }

        public async Task<List<Charge>> GetAllChargesAsync(Tag tag, Battery battery)
        {
            var relations = await _chargeTagRelationService.GetAllRelationsForTagAsync(tag.Id);
            var charges = await _chargeService.GetActiveAsync(battery);
            var output = charges.Where(c => relations.Any(r => r.ChargeId == c.Id)).ToList();

            return output;
        }

        public async Task<List<ChargeTagRelation>> GetAllRelationsForBattery(Battery battery)
        {
            List<ChargeTagRelation> relations = new();
            var tags = await GetAllTagsAsync(battery);

            foreach (var tag in tags)
            {
                var relation = await _chargeTagRelationService.GetAllRelationsForTagAsync(tag.Id);
                relations.AddRange(relation);
            }

            return relations;
        }

        public async Task AddTagFromChargeAsync(int chargeId, Tag tag)
        {
            var newTag = await _tagService.AddAsync(tag);
            await _chargeTagRelationService.AddAsync(chargeId, newTag.Id);
            await _noteService.AddTagHistoryNoteAsync(chargeId, tag);
            await _chargeService.SetUpdatedAsync(chargeId);
        }

        public async Task AddRelationshipAsync(int chargeId, Tag tag)
        {
            var relation = await _chargeTagRelationService.GetAsync(chargeId, tag.Id);

            if (relation is null)
            {
                await _chargeTagRelationService.AddAsync(chargeId, tag.Id);
                await _noteService.AddTagHistoryNoteAsync(chargeId, tag);
                await _chargeService.SetUpdatedAsync(chargeId);
            }
        }

        public async Task RemoveTagFromChargeAsync(int chargeId, Tag tag)
        {
            await _noteService.RemoveTagHistoryNoteAsync(chargeId, tag);
            await _chargeTagRelationService.DeleteAsync(chargeId, tag.Id);
            await _chargeService.SetUpdatedAsync(chargeId);
        }

        public async Task DeleteTagAndAllRelationshipsAsync(int tagId)
        {
            var relations = await _chargeTagRelationService.GetAllRelationsForTagAsync(tagId);

            foreach (var relation in relations)
            {
                await _chargeTagRelationService.DeleteAsync(relation.Id);
            }

            await _tagService.DeleteAsync(tagId);
        }

        public async Task DeleteAllRelationshipsForChargeAsync(int chargeId)
        {
            var relations = await _chargeTagRelationService.GetAllRelationsForChargeAsync(chargeId);

            foreach (var relation in relations)
            {
                await _chargeTagRelationService.DeleteAsync(relation.Id);
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

            var tags = await _tagService.GetAsync(battery);

            foreach (var tag in tags)
            {
                var relations = await _chargeTagRelationService.GetAllRelationsForTagAsync(tag.Id);

                counts.Add(tag.Id, relations.Count);
            }

            return counts;
        }

        public async Task<Dictionary<int, int>> CountAllChargeTagRelationshipsAsync(string userId)
        {
            Dictionary<int, int> counts = new();

            var tags = await _tagService.GetAsync(userId);

            foreach (var tag in tags)
            {
                var relations = await _chargeTagRelationService.GetAllRelationsForTagAsync(tag.Id);

                counts.Add(tag.Id, relations.Count);
            }

            return counts;
        }

        public async Task<Dictionary<int, int>> CountActiveChargeTagRelationshipsAsync(Battery battery)
        {
            Dictionary<int, int> counts = new();
            var tags = await _tagService.GetAsync(battery);

            var combinedCharges = await _chargeService.GetActiveParentsAndChildrenAsync(battery);

            foreach (var tag in tags)
            {
                var relations = await _chargeTagRelationService.GetAllRelationsForTagAsync(tag.Id);

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
            var tags = await _tagService.GetAsync(userId);

            var combinedCharges = await _chargeService.GetActiveParentsAndChildrenAsync(userId);

            foreach (var tag in tags)
            {
                var relations = await _chargeTagRelationService.GetAllRelationsForTagAsync(tag.Id);

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
