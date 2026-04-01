using CommunityToolkit.Mvvm.ComponentModel;

namespace NoteHub_Client.ViewModels
{
    public partial class RenderedNoteViewModel: ObservableObject
    {
        [ObservableProperty]
        private IList<string> tokens = new List<string>() { "Hello", "World", "!" };
    }
}
