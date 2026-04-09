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
        Task<Result<List<INote>, NoteServiceException>> SearchNote(string token);

        Task<Result<List<INote>, NoteServiceException>> GetNoteByTitle(string title);
        Task<Result<INote, NoteServiceException>> CreateUpdateNote(long? id, string title, string note);

        bool IsOK { get; }

        Task<bool> Ping();
    }
}
