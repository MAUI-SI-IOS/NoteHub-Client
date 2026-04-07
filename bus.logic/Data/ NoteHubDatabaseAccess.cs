using System.Diagnostics;
using bus.logic.models;
using SQLite;

namespace bus.logic.Data;

public class NoteHubDatabaseAccess
{
    private SQLiteAsyncConnection? dbConnection;

    private async Task Init()
    {
        if (dbConnection != null)
            return;
        dbConnection = new SQLiteAsyncConnection(DBConfig.DatabasePath, DBConfig.flags);
        await dbConnection.CreateTableAsync<NoteEntity>();
    }

    public async Task<NoteEntity> GetNoteAsyncByTitle(string title)
    {
        await Init();

        Debug.Assert(dbConnection != null, nameof(dbConnection) + " != null");
        var result = await dbConnection.Table<NoteEntity>()
            .Where(x => x.Title == title)
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<List<NoteEntity>> GetNoteListLikeWithTitleLike(string title)
    {
        await Init();

        Debug.Assert(dbConnection != null, nameof(dbConnection) + " != null");
        var result = await dbConnection.Table<NoteEntity>()
            .Where(x => x.Title.Contains(title))
            .ToListAsync();

        return result;
    }

    public async Task<NoteEntity> GetNoteAsync(long id)
    {
        await Init();
        Debug.Assert(dbConnection != null, nameof(dbConnection) + " != null");
        var result = await dbConnection.Table<NoteEntity>()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        return result;
    }

    public async Task<NoteEntity> SaveNoteAsync(INote note)
    {
        await Init();
        Debug.Assert(dbConnection != null, nameof(dbConnection) + " != null");
        var noteEntity = NoteEntity.FromNote(note);
        if (noteEntity.Id != null)
            _ = await dbConnection.UpdateAsync(noteEntity);
        else
            _ = await dbConnection.InsertAsync(noteEntity);

            return await dbConnection.GetAsync<NoteEntity>(n => note.Title == n.Title && note.RawContent == n.RawContent);
    }

    public async Task<List<NoteEntity>> GetNoteListWithToken(string token)
    {
        await Init();
        Debug.Assert(dbConnection != null, nameof(dbConnection) + " != null");
        var result = await dbConnection.Table<NoteEntity>()
            .Where(x => x.RawContent.Contains(token))
            .ToListAsync();
        return result;
    }

    public async Task<int> DeleteNoteAsync(INote note)
    {
        await Init();
        Debug.Assert(dbConnection != null, nameof(dbConnection) + " != null");
        var noteEntity = NoteEntity.FromNote(note);
        return await dbConnection.DeleteAsync(noteEntity);
    }
}