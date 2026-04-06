namespace NoteHub_Client;
using bus.logic.models;
using NoteHub_Client.ViewModels;
using System.Diagnostics;



public partial class NoteDetailsPage : ContentPage, IQueryAttributable
{
    public const string NOTE_TITLE_QUERY_KEY = "NOTE_TITLE_TO_SHOW";

    NoteDetailsViewModel _viewModel;

    public NoteDetailsPage(NoteDetailsViewModel viewmodel)
    {
        InitializeComponent();
        _viewModel = viewmodel;
        this.BindingContext = _viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey(NOTE_TITLE_QUERY_KEY) && query[NOTE_TITLE_QUERY_KEY] is Note note)
        {
            Debug.WriteLine($"[TESTING] {note.Title}, {note.RawContent}");
            _viewModel.SetStateFrom(note);
            return;
        }
        Debug.WriteLine("Error applying query attributes");   
    }

}