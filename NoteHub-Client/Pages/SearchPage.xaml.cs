namespace NoteHub_Client;

using NoteHub_Client.ViewModels;

public partial class SearchPage : ContentPage
{

    public SearchPage(SearchNoteViewModel viewModel)
	{
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}