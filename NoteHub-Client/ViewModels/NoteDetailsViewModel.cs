using bus.logic.service;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace NoteHub_Client.ViewModels
{
    public partial class NoteDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private string noteId = "";

        [ObservableProperty]
        private bool isReadMode = true;

        [ObservableProperty]
        private string noteTitle = "Sample Note Title";

        [ObservableProperty]
        private string noteContent = "Sample note content goes here. This is a placeholder for the note content.";

        [ObservableProperty]
        private ObservableCollection<string> tokens = new ObservableCollection<string>();

        public void SetStateFrom(Note note)
        {
            NoteId = note.id ?? NoteId;
            NoteTitle = note.title;
            NoteContent = note.RawContent;
        }

        public NoteDetailsViewModel()
        {
            // Example tokens for testing RenderedNoteContentView
            Tokens = new ObservableCollection<string> { "This", "is", "a", "sample", "note." };
        }

        public void ToggleReadMode()
        {
            IsReadMode = !IsReadMode;
        }
    }
}
