using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.NoteModel
{
    public interface INoteService
    {
        Task<Note> Add(Note note);
        Task<Note> AddHistoryNote(PropertyValues oldValues, PropertyValues newValues);
        Task Delete(int id);
        Task<List<Note>> Get();
        Task<Note> Get(int id);
        Task<List<Note>> GetAllNotesForCharge(int chargeId)
        Task<Note> Update(Note note);
    }
}