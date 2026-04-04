using NoteHub_Client.ViewModels;

namespace NoteHub_Client;

public partial class WriteNotePage : ContentPage
{
	public WriteNotePage(WriteNoteViewModel viewmodel)
	{
		InitializeComponent();
		this.BindingContext = viewmodel;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is WriteNoteViewModel vm)
        {
            await vm.OnAppearing();
        }
    }
}