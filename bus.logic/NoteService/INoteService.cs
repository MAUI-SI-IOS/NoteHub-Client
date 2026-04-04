using bus.logic.ApiService.Directors;
using bus.logic.Result;
using bus.logic.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.logic.NoteService
{
    public interface INoteService
    {
        Task<Result<List<Note>, string>> SearchNote(string token);

        Task<Result<Note, string>> GetNoteByTitle(string title);
        Task<Result<Note, string>> CreateUpdateNote(long? id, string title, string note);
    }
}
