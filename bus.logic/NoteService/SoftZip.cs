using bus.logic.ApiService;
using bus.logic.models;
using bus.logic.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus.logic.NoteService
{
    //where A is primordial and B is optional
    internal class SoftZip: INoteService
    {
        INoteService A; 
        INoteService B; 
        public SoftZip(INoteService a, INoteService b)
        {
            A = a;
            B = b;
        }
        public async Task<Result<Note, HttpException>> CreateUpdateNote(long? id, string title, string note)
        {
            var localResult = await A.CreateUpdateNote(id, title, note);  
            if (!localResult.IsSuccess) return localResult;

            return await B.CreateUpdateNote(id, title, note)
                .BindErrAsync(async (ex) =>
                {
                    if (ex.code < 10)
                    {
                        return localResult;
                    }
                    return Result<Note, HttpException>.Failure(ex);
                });
        }
        public async Task<Result<List<Note>, HttpException>> SearchNote(string token)
        {
            return await B.SearchNote(token)
            .BindErrAsync(async (ex) => 
            ex.code < 10 
                ? await A.SearchNote(token) 
                : Result<List<Note>, HttpException>.Failure(ex));

        }
        public async Task<Result<Note, HttpException>> GetNoteByTitle(string title)
        {
            return await B.GetNoteByTitle(title)
            .BindErrAsync(async (ex) => 
            ex.code < 10 
                ? await A.GetNoteByTitle(title) 
                : Result<Note, HttpException>.Failure(ex));
        }

        public async Task<bool> Ping() => await B.Ping();
    }
}
