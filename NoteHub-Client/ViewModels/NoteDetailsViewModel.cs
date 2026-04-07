using bus.logic.models;
using bus.logic.NoteService;
using bus.logic.Result;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NoteHub_Client.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NoteHub_Client.ViewModels
{
    public partial class NoteDetailsViewModel : ObservableObject
    {
        //Global Properties
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsUpdateMode))]
        private long? noteId = null;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsEditMode))]
        private bool isReadMode = true;
        public bool IsEditMode => !IsReadMode;      //with purpose of removing static inverse bool

        [ObservableProperty]
        private string noteTitle = string.Empty;

        [ObservableProperty]
        private string noteContent = string.Empty;

        [ObservableProperty]
        private ObservableCollection<string> tokens = new ObservableCollection<string>();

        public void SetStateFrom(INote note)
        {
            NoteId = note.Id;
            NoteTitle = note.Title;
            NoteContent = note.RawContent;
        }
        public void ToggleReadMode() => IsReadMode = !IsReadMode;

        INoteService service;
        public NoteDetailsViewModel(INoteService service)
        {
            this.service = service;
            IsReadMode = NoteId.HasValue; //if there is no note automaticly shows edit mode
        }


        //EditeMode Properties && Commands
        public bool IsUpdateMode => NoteId.HasValue; 
        //Save change to db
        [RelayCommand]
        public async Task Save(VisualElement anchor)
        {
            if (!ValidateInfo(NoteTitle, NoteContent))
            {
                await DisplayService.ShowSuccess("Invalid arguments Title and Content can't be null");
                return;
            }

            try //last line of defense
            {
                await this.service.CreateUpdateNote(NoteId, NoteTitle, NoteContent)
                .MatchAsync(
                   ok: async (note) =>
                   {
                       this.NoteId = note.Id;
                       await DisplayService.ShowSuccess("Note has been succesfully saved", anchor);
                   },

                   err: async (err) =>
                   {
                       Debug.WriteLine("[ERROR], " + err.msg);
                       await DisplayService.ShowFailure(err.msg, anchor);
                   }
                );
            }
            catch(Exception e)
            {
                Debug.WriteLine("[UNDIAGNOSED ERROR], " + e.Message);
                await DisplayService.ShowFailure("internal error, pls try again later", anchor);
            }
        }

        //Create a new note
        [RelayCommand]
        public void NewNoteCommand()
        {
            this.NoteId = null;
            this.NoteTitle = string.Empty;
            this.NoteContent = string.Empty;
            OnPropertyChanged(nameof(IsUpdateMode));
        }
        
        private bool ValidateInfo(string? noteTitle, string? noteContent)
        {
            return !String.IsNullOrEmpty(noteTitle) && !String.IsNullOrEmpty(noteContent);
        }
    }
}
