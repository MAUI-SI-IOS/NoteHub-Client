using bus.logic.service;
using NoteHub_Client.ViewModels;
using System.Diagnostics;

namespace NoteHub_Client.Pages;

public partial class NoteDetailsPage : ContentPage, IQueryAttributable
{
    public const string NOTE_TITLE_QUERY_KEY = "NOTE_TITLE_TO_SHOW";

    NoteDetailsViewModel _viewModel;

    public NoteDetailsPage()
    {
        InitializeComponent();
        _viewModel = new NoteDetailsViewModel();
        BindingContext = _viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey(NOTE_TITLE_QUERY_KEY) && query[NOTE_TITLE_QUERY_KEY] is Note noteFromQuery)
        {
            _viewModel.SetStateFrom(noteFromQuery);
            return;
        }
        Debug.WriteLine("Error applying query attributes");   
    }

    private void Switch_Toggled(object sender, ToggledEventArgs e) => _viewModel.ToggleReadMode();
}