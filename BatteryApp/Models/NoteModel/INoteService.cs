using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.TagModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.NoteModel
{
    public interface INoteService
    {
        Task<Note> AddAsync(Note note);
        Task AddChildParentHistoryNoteAsync(Charge child, Charge parent);
        Task<Note> AddEntityHistoryNoteAsync(PropertyValues oldValues, PropertyValues newValues);
        Task<Note> AddNoteFromChargeAsync(Charge charge, string noteText);
        Task<Note> AddRelatedLinkHistoryNoteAsync(int chargeid, string desc, string linkType, string ownerId);
        Task<Note> AddTagHistoryNoteAsync(int chargeId, Tag tag);
        Task DeleteAsync(int id);
        Task<List<Note>> GetAsync();
        Task<Note> GetAsync(int id);
        Task<List<Note>> GetAllNotesForChargeAsync(int chargeId);
        Task<Note> RemoveChildParentHistoryNoteAsync(Charge child);
        Task<Note> RemoveTagHistoryNoteAsync(int chargeId, Tag tag);
        Task<Note> UpdateAsync(Note note);
    }
}