

namespace NoteHub_Client;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent();
	}

	public async void OnNoteSelected(object? sender, SelectionChangedEventArgs e) 
	{
        var selectedNote = e.CurrentSelection.FirstOrDefault();//need to cast in Note

        if (selectedNote != null)
        {
            
            var navigationParameter = new Dictionary<string, object>
            {
                { "Note", selectedNote }
            };

            await Shell.Current.GoToAsync(nameof(NoteDetailPage), navigationParameter);
        }
    }
}