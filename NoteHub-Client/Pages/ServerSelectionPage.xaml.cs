using NoteHub_Client.ViewModels;

namespace NoteHub_Client;

public partial class ServerSelectionPage : ContentPage
{
	public ServerSelectionPage(ServerSelectionViewModel viewmodel)
	{
		InitializeComponent();
		this.BindingContext = viewmodel;
	}
}