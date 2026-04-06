using bus.logic.ApiService;
using bus.logic.models;
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

        public Task<Result<Note, HttpException>> CreateUpdateNote(long? id, string title, string note)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Note, HttpException>> GetNoteByTitle(string title)
        {
            throw new NotImplementedException();
        }
        public Task<Result<List<Note>, HttpException>> SearchNote(string token)
        {
            throw new NotImplementedException();
        }
        public Task<bool> Ping()
        {
            throw new NotImplementedException();
        }
    }
}
