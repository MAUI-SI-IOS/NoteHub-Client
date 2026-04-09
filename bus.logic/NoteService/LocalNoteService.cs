using bus.logic.ApiService;
using bus.logic.models;
using bus.logic.Result;
using bus.logic.Data;

namespace bus.logic.NoteService
{
    public class LocalNoteService(
        NoteHubDatabaseAccess? access = null
    ): INoteService {

        public bool IsOK => true;

        private NoteHubDatabaseAccess dbAccess = access ?? new NoteHubDatabaseAccess();

        public async Task<Result<INote, NoteServiceException>> CreateUpdateNote(long? id, string title, string note)
        {
            try
            {

                var savedNote = await dbAccess.SaveNoteAsync(new NoteEntity
                {
                    Id = id,
                    Title = title,
                    RawContent = note,
                });


                return Result<INote, NoteServiceException>.Success(savedNote);

            }
            catch (Exception ex)
            {
                return Result<INote, NoteServiceException>.Failure(new NoteServiceException(
                    -1,
                    ex.Message
                ));
            }
        }

        public async Task<Result<List<INote>, NoteServiceException>> GetNoteByTitle(string title)
        {
            try
            {
                var result = await dbAccess.GetNoteListByTitle(title);

                return Result<List<INote>, NoteServiceException>.Success(result.Select(n => n as INote).ToList());
            }
            catch (Exception ex)
            {
                return Result<List<INote>, NoteServiceException>.Failure(new NoteServiceException(-1, ex.Message));
            }
        }
        public async Task<Result<List<INote>, NoteServiceException>> SearchNote(string token)
        {
            try
            {
                var resultByContent = await dbAccess.GetNoteListWithToken(token);
                var resultByTitle = await dbAccess.GetNoteListLikeWithTitleLike(token);

                var aggregatedResults = new HashSet<INote>();
                foreach(var note in resultByTitle)
                    _ = aggregatedResults.Add(note);
                foreach (var note in resultByContent)
                    _ = aggregatedResults.Add(note);


                return Result<List<INote>, NoteServiceException>.Success(aggregatedResults.ToList());
            }
            catch (Exception e)
            {
                return Result<List<INote>, NoteServiceException>.Failure(new NoteServiceException(-1, e.Message));
            }
        }

        public Task<bool> Ping() => Task.FromResult(true);
    }
}
