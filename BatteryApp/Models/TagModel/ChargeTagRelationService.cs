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

        public async Task<List<ChargeTagRelation>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.ChargeTagRelations.ToListAsync();
        }

        public async Task<ChargeTagRelation> GetAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var relation = await context.ChargeTagRelations.FindAsync(id);
            return relation;
        }

        public async Task<ChargeTagRelation> GetAsync(int chargeId, int tagId)
        {
            using var context = _contextFactory.CreateDbContext();
            var relation = await context.ChargeTagRelations.Where(x => x.ChargeId == chargeId && x.TagId == tagId).FirstOrDefaultAsync();
            return relation;
        }

        public async Task<List<ChargeTagRelation>> GetAllRelationsForChargeAsync(int chargeId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.ChargeTagRelations.Where(x => x.ChargeId == chargeId).ToListAsync();
        }

        public async Task<List<ChargeTagRelation>> GetAllRelationsForTagAsync(int tagId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.ChargeTagRelations.Where(x => x.TagId == tagId).ToListAsync();
        }

        public async Task<ChargeTagRelation> AddAsync(ChargeTagRelation relation)
        {
            using var context = _contextFactory.CreateDbContext();
            context.ChargeTagRelations.Add(relation);
            await context.SaveChangesAsync();
            return relation;
        }

        public async Task<ChargeTagRelation> AddAsync(int chargeId, int tagId)
        {
            using var context = _contextFactory.CreateDbContext();

            ChargeTagRelation relation = CreateChargeTagRelation(chargeId, tagId);

            context.ChargeTagRelations.Add(relation);
            await context.SaveChangesAsync();

            return relation;
        }

        public async Task<ChargeTagRelation> UpdateAsync(ChargeTagRelation relation)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(relation).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return relation;
        }

        public async Task<ChargeTagRelation> UpdateAsync(int chargeId, int tagId)
        {
            using var context = _contextFactory.CreateDbContext();

            ChargeTagRelation relation = await GetAsync(chargeId, tagId);

            context.Entry(relation).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return relation;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var relation = await context.ChargeTagRelations.FindAsync(id);
            context.ChargeTagRelations.Remove(relation);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int chargeId, int tagId)
        {
            using var context = _contextFactory.CreateDbContext();
            ChargeTagRelation relation = await GetAsync(chargeId, tagId);
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
