namespace bus.logic.models;

public interface INote
{
    long? Id { get; set; }
    string Title { get; set; }
    string RawContent { get; set; }
    string FormattedContent { get; set; }
}