using BatteryApp.Data;
using BatteryApp.Models.BatteryModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.TagModel
{
    public class TagService : ITagService
    {
        private readonly IDbContextFactory<AppDbContextFactory> _contextFactory;

        public TagService(IDbContextFactory<AppDbContextFactory> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Tag>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Tags.ToListAsync();
        }

        public async Task<List<Tag>> GetAsync(Battery battery)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Tags.Where(x => x.BatteryId == battery.Id).ToListAsync();
        }

        public async Task<List<Tag>> GetAsync(string userId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Tags.Where(x => x.OwnerId == userId).ToListAsync();
        }

        public async Task<Tag> GetAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var tag = await context.Tags.FindAsync(id);
            return tag;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Tags.Add(tag);
            await context.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(tag).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return tag;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var tag = await context.Tags.FindAsync(id);
            context.Tags.Remove(tag);
            await context.SaveChangesAsync();
        }
    }
}
