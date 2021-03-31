using BatteryApp.Models.TagModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.NoteModel
{
    public interface INoteService
    {
        Task<Note> Add(Note note);
        Task<Note> AddEntityHistoryNote(PropertyValues oldValues, PropertyValues newValues);
        Task<Note> AddTagHistoryNote(int chargeId, Tag tag);
        Task Delete(int id);
        Task<List<Note>> Get();
        Task<Note> Get(int id);
        Task<List<Note>> GetAllNotesForCharge(int chargeId);
        Task<Note> RemoveTagHistoryNote(int chargeId, Tag tag);
        Task<Note> Update(Note note);
    }
}