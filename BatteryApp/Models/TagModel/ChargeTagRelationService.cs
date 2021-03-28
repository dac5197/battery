using BatteryApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.TagModel
{
    public class ChargeTagRelationService : IChargeTagRelationService
    {
        private readonly IDbContextFactory<AppDbContextFactory> _contextFactory;

        public ChargeTagRelationService(IDbContextFactory<AppDbContextFactory> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<ChargeTagRelation>> Get()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.ChargeTagRelations.ToListAsync();
        }

        public async Task<ChargeTagRelation> Get(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var relation = await context.ChargeTagRelations.FindAsync(id);
            return relation;
        }

        public async Task<ChargeTagRelation> Get(int chargeId, int tagId)
        {
            using var context = _contextFactory.CreateDbContext();
            var relation = await context.ChargeTagRelations.Where(x => x.ChargeId == chargeId && x.TagId == tagId).FirstOrDefaultAsync();
            return relation;
        }

        public async Task<List<ChargeTagRelation>> GetAllRelationsForCharge(int chargeId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.ChargeTagRelations.Where(x => x.ChargeId == chargeId).ToListAsync();
        }

        public async Task<List<ChargeTagRelation>> GetAllRelationsForTag(int tagId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.ChargeTagRelations.Where(x => x.TagId == tagId).ToListAsync();
        }

        public async Task<ChargeTagRelation> Add(ChargeTagRelation relation)
        {
            using var context = _contextFactory.CreateDbContext();
            context.ChargeTagRelations.Add(relation);
            await context.SaveChangesAsync();
            return relation;
        }

        public async Task<ChargeTagRelation> Add(int chargeId, int tagId)
        {
            using var context = _contextFactory.CreateDbContext();

            ChargeTagRelation relation = CreateChargeTagRelation(chargeId, tagId);

            context.ChargeTagRelations.Add(relation);
            await context.SaveChangesAsync();

            return relation;
        }

        public async Task<ChargeTagRelation> Update(ChargeTagRelation relation)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(relation).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return relation;
        }

        public async Task<ChargeTagRelation> Update(int chargeId, int tagId)
        {
            using var context = _contextFactory.CreateDbContext();

            ChargeTagRelation relation = await Get(chargeId, tagId);

            context.Entry(relation).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return relation;
        }

        public async Task Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var relation = await context.ChargeTagRelations.FindAsync(id);
            context.ChargeTagRelations.Remove(relation);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int chargeId, int tagId)
        {
            using var context = _contextFactory.CreateDbContext();
            ChargeTagRelation relation = await Get(chargeId, tagId);
            context.ChargeTagRelations.Remove(relation);
            await context.SaveChangesAsync();
        }

        private static ChargeTagRelation CreateChargeTagRelation(int chargeId, int tagId)
        {
            ChargeTagRelation relation = new()
            {
                ChargeId = chargeId,
                TagId = tagId
            };
            return relation;
        }
    }
}
