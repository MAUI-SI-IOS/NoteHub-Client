using NoteHub_Client.ViewModels;

namespace NoteHub_Client;

public partial class ServerSelectionPage : ContentPage
{
	public ServerSelectionPage()
	{
		InitializeComponent();
		this.BindingContext = new ServerSelectionViewModel();
	}
}