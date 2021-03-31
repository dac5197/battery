using BatteryApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.NoteModel
{
    public class NoteService : INoteService
    {
        private readonly IDbContextFactory<AppDbContextFactory> _contextFactory;

        public NoteService(IDbContextFactory<AppDbContextFactory> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Note>> Get()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Notes.ToListAsync();
        }

        public async Task<List<Note>> GetAllNotesForCharge(int chargeId)
        {
            using var context = _contextFactory.CreateDbContext();
            var notes = await context.Notes.ToListAsync();
            return notes.Where(x => x.ChargeId == chargeId).ToList();
        }

        public async Task<Note> Get(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var note = await context.Notes.FindAsync(id);
            return note;
        }

        public async Task<Note> Add(Note note)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Notes.Add(note);
            await context.SaveChangesAsync();
            return note;
        }

        public async Task<Note> AddHistoryNote(PropertyValues oldValues, PropertyValues newValues)
        {
            Note note = new();

            note.ChargeId = newValues.GetValue<int>("Id");
            note.OwnerId = newValues.GetValue<string>("OwnerId");
            //note.OwnerId = "bca141ab-8848-4ce6-a77c-2d5e4412f4ba";
            note.Timestamp = DateTime.UtcNow;

            List<HistoryJson> historyJsonList = new();

            foreach (var property in oldValues.Properties)
            {
                string oldPropValue = oldValues[property]?.ToString(); //== null ? "" : oldValues[property].ToString();
                string newPropValue = newValues[property]?.ToString(); //== null ? "" : newValues[property].ToString();

                if (oldPropValue != newPropValue)
                {
                    HistoryJson record = new(property.Name, oldPropValue, newPropValue);
                    historyJsonList.Add(record);
                }
            }

            note.History = historyJsonList;

            note = await Add(note);

            return note;
        }

        public async Task<Note> Update(Note note)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(note).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return note;
        }

        public async Task Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var note = await context.Notes.FindAsync(id);
            context.Notes.Remove(note);
            await context.SaveChangesAsync();
        }
    }
}
