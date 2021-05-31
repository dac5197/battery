using BatteryApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.NoteModel
{
    public class NoteTypeService : INoteTypeService
    {
        private readonly IDbContextFactory<AppDbContextFactory> _contextFactory;

        public NoteTypeService(IDbContextFactory<AppDbContextFactory> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<NoteType>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.NoteTypes.ToListAsync();
        }

        public async Task<NoteType> GetAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var type = await context.NoteTypes.FindAsync(id);
            return type;
        }

        public async Task<NoteType> GetAsync(string name)
        {
            using var context = _contextFactory.CreateDbContext();
            var types = await context.NoteTypes.ToListAsync();
            var type = types.Where(x => x.Name == name).FirstOrDefault();
            return type;
        }

        public async Task<NoteType> AddAsync(NoteType type)
        {
            using var context = _contextFactory.CreateDbContext();
            context.NoteTypes.Add(type);
            await context.SaveChangesAsync();
            return type;
        }

        public async Task<NoteType> UpdateAsync(NoteType type)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(type).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return type;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var type = await context.NoteTypes.FindAsync(id);
            context.NoteTypes.Remove(type);
            await context.SaveChangesAsync();
        }
    }
}
