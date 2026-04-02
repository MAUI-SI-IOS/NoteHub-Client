namespace NoteHub_Client;

using bus.logic.Result;
using bus.logic.service;
using NoteHub_Client.Pages;


public partial class SearchPage : ContentPage
{
    NoteService service;
    
    private CancellationTokenSource? _searchCancellation;
    public static readonly BindableProperty StatusMessageProperty =
        BindableProperty.Create(nameof(StatusMessage), typeof(string), typeof(SearchPage), "Commencez à taper...");

    public string StatusMessage
    {
        get => (string)GetValue(StatusMessageProperty);
        set => SetValue(StatusMessageProperty, value);
    }


    public SearchPage()
	{
        var client = new HttpClient { BaseAddress = new Uri("http://10.0.2.2:8080") };
        this.service = new NoteService(client);
        InitializeComponent();
	}
    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchTerm = e.NewTextValue;
        _searchCancellation?.Cancel();
        _searchCancellation = new CancellationTokenSource();
        var token = _searchCancellation.Token;

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            NotesListView.ItemsSource = null;
            return;
        }

        try
        {
            await Task.Delay(300, token);
            //search data from api
            var note = await service.SearchNote(searchTerm);
            if (note != null)
            {
                // depending on internal state do something
                note.Match(
                    ok:  (obj) => 
                    {
                        StatusMessage = "no item found";
                        NotesListView.ItemsSource = obj; 
                    },
                    err: (err) =>
                    {
                        StatusMessage = err.Message;
                        NotesListView.ItemsSource = null;
                    }
                );
            }
        }
        catch (OperationCanceledException) { }//do nothing
        catch (Exception ex) { StatusMessage = "Something went wrong. Please check your connection."; }
    }



    public async void OnNoteSelected(object? sender, SelectionChangedEventArgs e) 
	{
        var selectedNote = e.CurrentSelection.FirstOrDefault() as Note;
        
        if (selectedNote != null)
        {
            
            var navigationParameter = new Dictionary<string, object>
            {
                { NoteDetailsPage.NOTE_TITLE_QUERY_KEY, selectedNote }
            };

            await Shell.Current.GoToAsync(nameof(NoteDetailsPage), navigationParameter);
        }
    }
}