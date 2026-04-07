using bus.logic.models;
using SQLite;

namespace bus.logic.Data;

public class NoteEntity : INote
{
    [PrimaryKey, AutoIncrement]
    public long? Id { get; set; }
    [Column("title"), Unique]
    public string Title { get; set; }
    [Column("raw_content")]
    public string RawContent { get; set; }
    [Column("formatted_content")]
    public string FormattedContent { get; set; }

    public static NoteEntity FromNote(INote note) => new NoteEntity
        {
            Id = note.Id,
            Title = note.Title,
            RawContent = note.RawContent,
            FormattedContent = note.FormattedContent
        };


    // override GetHashCode and Equals to ensure notes are compared based on their content rather than reference
    public override int GetHashCode() => Id?.GetHashCode() ?? Title.GetHashCode();
    public override bool Equals(object? obj)
    {
        if (obj is NoteEntity other)
        {
            if (Id != null && other.Id != null)
                return Id == other.Id;
            else
                return Title == other.Title &&
                    RawContent == other.RawContent &&
                    FormattedContent == other.FormattedContent;
        }
        return false;
    }
}