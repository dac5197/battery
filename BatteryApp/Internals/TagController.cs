﻿using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.TagModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public class TagController : ITagController
    {
        private readonly IChargeTagRelationService _chargeTagRelationService;
        private readonly ITagService _tagService;

        public TagController(IChargeTagRelationService chargeTagRelationService, ITagService tagService)
        {
            _chargeTagRelationService = chargeTagRelationService;
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
        }

        public async Task AddRelationshipAsync(int chargeId, int tagId)
        {
            var relation = await _chargeTagRelationService.Get(chargeId, tagId);

            if (relation is null)
            {
                await _chargeTagRelationService.Add(chargeId, tagId);
            }
        }

        public async Task RemoveTagFromChargeAsync(int chargeId, int tagId)
        {
            await _chargeTagRelationService.Delete(chargeId, tagId);
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
    }
}