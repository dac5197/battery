using BatteryApp.Data;
using BatteryApp.Models.TagModel;
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

        public async Task<Note> AddEntityHistoryNote(PropertyValues oldValues, PropertyValues newValues)
        {
            List<HistoryJson> historyJsonList = new();

            List<string> IgnoreFieldList = new() { "Created", "Updated" };

            foreach (var property in newValues.Properties)
            {
                if (!IgnoreFieldList.Contains(property.Name))
                {
                    string oldPropValue;

                    if (oldValues is not null)
                    {
                        oldPropValue = oldValues[property]?.ToString();
                    }
                    else
                    {
                        oldPropValue = null;
                    }

                    string newPropValue = newValues[property]?.ToString(); //== null ? "" : newValues[property].ToString();

                    if (oldPropValue != newPropValue)
                    {
                        HistoryJson record = new(property.Name, oldPropValue, newPropValue);
                        historyJsonList.Add(record);
                    }
                }
                
            }

            Note note = new()
            {
                ChargeId = newValues.GetValue<int>("Id"),
                History = historyJsonList,
                OwnerId = newValues.GetValue<string>("OwnerId"),
                Timestamp = DateTime.UtcNow
            };

            note = await Add(note);

            return note;
        }

        public async Task<Note> AddTagHistoryNote(int chargeId, Tag tag)
        {
            

            List<HistoryJson> historyJsonList = new();
            HistoryJson record = new("Tag", null, tag.Name);
            historyJsonList.Add(record);

            Note note = new()
            {
                ChargeId = chargeId,
                History = historyJsonList,
                OwnerId = tag.OwnerId,
                Timestamp = DateTime.UtcNow
            };

            note = await Add(note);

            return note;
        }

        public async Task<Note> RemoveTagHistoryNote(int chargeId, Tag tag)
        {
            List<HistoryJson> historyJsonList = new();
            HistoryJson record = new("Tag", tag.Name, null);
            historyJsonList.Add(record);

            Note note = new()
            {
                ChargeId = chargeId,
                History = historyJsonList,
                OwnerId = tag.OwnerId,
                Timestamp = DateTime.UtcNow
            };

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
