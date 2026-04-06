namespace NoteHub_Client;

using NoteHub_Client.ViewModels;



public partial class ServerSelectionPage : ContentPage
{
	public ServerSelectionPage(ServerSelectionViewModel viewmodel)
	{
		InitializeComponent();
		this.BindingContext = viewmodel;
	}
}