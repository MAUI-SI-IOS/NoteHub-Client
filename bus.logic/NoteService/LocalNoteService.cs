using bus.logic.Result;
using bus.logic.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.logic.NoteService
{
    public class LocalNoteService: INoteService
    {
        public LocalNoteService(string path) { }

        public Task<Result<Note, string>> CreateUpdateNote(long? id, string title, string note)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Note, string>> GetNoteByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<Note>, string>> SearchNote(string token)
        {
            throw new NotImplementedException();
        }
    }
}
