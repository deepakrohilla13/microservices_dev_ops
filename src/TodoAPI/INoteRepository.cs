using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotes();
        Task<Note> GetNote(string id);
        Task AddNote(Note item);
        Task<bool> RemoveNote(string id);
        Task<bool> UpdateNote(string id, string body);
        Task<bool> UpdateNoteDocument(string id, string body);
        Task<bool> RemoveAllNotes();
    }
}
