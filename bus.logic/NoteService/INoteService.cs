using bus.logic.ApiService;
using bus.logic.ApiService.Directors;
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
    public interface INoteService
    {
        Task<Result<List<Note>, HttpException>> SearchNote(string token);

        Task<Result<Note, HttpException>> GetNoteByTitle(string title);
        Task<Result<Note, HttpException>> CreateUpdateNote(long? id, string title, string note);

        Task<bool> Ping();
    }
}
