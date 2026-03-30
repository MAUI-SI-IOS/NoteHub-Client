namespace NoteHub_Client.Views;

public class RenderedNoteContentView : ContentView
{
	public RenderedNoteContentView()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}
}