using BatteryApp.Data;
using BatteryApp.Models.ChargeModel;
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
        private readonly INoteTypeService _noteTypeService;

        public NoteService(IDbContextFactory<AppDbContextFactory> contextFactory, INoteTypeService noteTypeService)
        {
            _contextFactory = contextFactory;
            _noteTypeService = noteTypeService;
        }

        public async Task<List<Note>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Notes.ToListAsync();
        }

        public async Task<List<Note>> GetAllNotesForChargeAsync(int chargeId)
        {
            using var context = _contextFactory.CreateDbContext();
            var notes = await context.Notes.ToListAsync();
            return notes.Where(x => x.ChargeId == chargeId).ToList();
        }

        public async Task<Note> GetAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var note = await context.Notes.FindAsync(id);
            return note;
        }

        public async Task<Note> AddAsync(Note note)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Notes.Add(note);
            await context.SaveChangesAsync();
            return note;
        }

        public async Task<Note> AddNoteFromChargeAsync(Charge charge, string noteText)
        {
            NoteType noteType = await _noteTypeService.GetAsync("Note");

            Note note = new()
            {
                ChargeId = charge.Id,
                Description = noteText,
                NoteTypeId = noteType.Id,
                OwnerId = charge.OwnerId,
                Timestamp = DateTime.UtcNow
            };

            using var context = _contextFactory.CreateDbContext();
            context.Notes.Add(note);
            await context.SaveChangesAsync();
            return note;
        }

        public async Task<Note> AddEntityHistoryNoteAsync(PropertyValues oldValues, PropertyValues newValues)
        {
            List<HistoryJson> historyJsonList = new();
            List<string> IgnoreFieldList = new() { "Created", "Updated" };
            NoteType noteType = await _noteTypeService.GetAsync("History");

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
                NoteTypeId = noteType.Id,
                OwnerId = newValues.GetValue<string>("OwnerId"),
                Timestamp = DateTime.UtcNow
            };

            if (historyJsonList.Count > 0)
            {
                note = await AddAsync(note);
            }

            return note;
        }

        public async Task<Note> AddTagHistoryNoteAsync(int chargeId, Tag tag)
        {
            NoteType noteType = await _noteTypeService.GetAsync("Tag");
            List<HistoryJson> historyJsonList = new();

            HistoryJson record = new("Tag", null, tag.Name);
            historyJsonList.Add(record);

            Note note = new()
            {
                ChargeId = chargeId,
                History = historyJsonList,
                NoteTypeId = noteType.Id,
                OwnerId = tag.OwnerId,
                Timestamp = DateTime.UtcNow
            };

            note = await AddAsync(note);

            return note;
        }

        public async Task<Note> AddRelatedLinkHistoryNoteAsync(int chargeid, string desc, string linkType, string ownerId)
        {
            NoteType noteType = await _noteTypeService.GetAsync("Related");
            List<HistoryJson> historyJsonList = new();

            HistoryJson record = new(linkType, null, desc);
            historyJsonList.Add(record);

            Note note = new()
            {
                ChargeId = chargeid,
                History = historyJsonList,
                NoteTypeId = noteType.Id,
                OwnerId = ownerId,
                Timestamp = DateTime.UtcNow
            };

            note = await AddAsync(note);

            return note;
        }

        public async Task AddChildParentHistoryNoteAsync(Charge child, Charge parent)
        {
            // Add Child related link to Parent
            await AddRelatedLinkHistoryNoteAsync(parent.Id, $"{child.Id}: {child.Title}", "Child", child.OwnerId);
            // Add Parent related link to Child
            await AddRelatedLinkHistoryNoteAsync(child.Id, $"{parent.Id}: {parent.Title}", "Parent", child.OwnerId);
        }

        public async Task<Note> RemoveTagHistoryNoteAsync(int chargeId, Tag tag)
        {
            NoteType noteType = await _noteTypeService.GetAsync("Tag");
            List<HistoryJson> historyJsonList = new();
            HistoryJson record = new("Tag", tag.Name, null);
            historyJsonList.Add(record);

            Note note = new()
            {
                ChargeId = chargeId,
                History = historyJsonList,
                NoteTypeId = noteType.Id,
                OwnerId = tag.OwnerId,
                Timestamp = DateTime.UtcNow
            };

            note = await AddAsync(note);

            return note;
        }

        public async Task<Note> RemoveChildParentHistoryNoteAsync(Charge child)
        {
            NoteType noteType = await _noteTypeService.GetAsync("Related");
            List<HistoryJson> historyJsonList = new();

            HistoryJson record = new("Child", $"{child.Id}: {child.Title}", null);
            historyJsonList.Add(record);

            Note note = new()
            {
                ChargeId = (int)child.ParentId,
                History = historyJsonList,
                NoteTypeId = noteType.Id,
                OwnerId = child.OwnerId,
                Timestamp = DateTime.UtcNow
            };

            note = await AddAsync(note);

            return note;
        }

        public async Task<Note> UpdateAsync(Note note)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(note).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return note;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var note = await context.Notes.FindAsync(id);
            context.Notes.Remove(note);
            await context.SaveChangesAsync();
        }
    }
}
