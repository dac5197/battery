using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatteryApp.Models.NoteModel
{
    public interface INoteTypeService
    {
        Task<NoteType> Add(NoteType type);
        Task Delete(int id);
        Task<List<NoteType>> Get();
        Task<NoteType> Get(int id);
        Task<NoteType> Get(string name);
        Task<NoteType> Update(NoteType type);
    }
}