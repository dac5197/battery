using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.NoteModel
{
    public interface INoteTypeService
    {
        Task<NoteType> AddAsync(NoteType type);
        Task DeleteAsync(int id);
        Task<List<NoteType>> GetAsync();
        Task<NoteType> GetAsync(int id);
        Task<NoteType> GetAsync(string name);
        Task<NoteType> UpdateAsync(NoteType type);
    }
}