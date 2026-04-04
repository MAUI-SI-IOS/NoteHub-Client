using bus.logic.Result;
using bus.logic.service;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using bus.logic.NoteService;


namespace NoteHub_Client.ViewModels
{
    public partial class WriteNoteViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsUpdating))]
        long? id = null;
        [ObservableProperty]
        string title = string.Empty;
        [ObservableProperty]
        string content = string.Empty;
        bool IsUpdating => Id != null;


        //service
        readonly INoteService service;
        public WriteNoteViewModel(INoteService service)
        {
            this.service = service;
        }
        [RelayCommand]
        public async void OnSaveClicked(VisualElement anchor)
        {
            //logic de la fonction
            try
            {
                var action = await this.service.CreateUpdateNote(Id, Title, Content);
                action?.Match(
                   ok: async (note) =>
                   {
                       this.Id = note.Id; //from now on updates the note
                       await this.ShowSuccess("Note has been succesfully saved", anchor);
                   },

                   err: async (msg) => await this.ShowFail(msg, anchor)                  
                );
            }
            catch (Exception err)
            {
                Debug.WriteLine("[ADD]in catch:" + err.Message);
                await this.ShowFail(err.Message, anchor);
            }
        }

        [RelayCommand]
        public void OnNewNoteClicked()
        {
            this.Id      = null;
            this.Title   = string.Empty;
            this.Content = string.Empty;
        }

        [RelayCommand]
        public async Task OnAppearing()
        {
            if (service is LocalNoteService)
            {
                var snackbar = Snackbar.Make(
                    message: "No connection found, offline mode",
                    duration: TimeSpan.FromSeconds(3),
                    visualOptions: new SnackbarOptions { BackgroundColor = Colors.Red });

                await snackbar.Show();
            }
        }

        //snackbars
        internal async Task ShowSuccess(string message, VisualElement anchor)
        {
            var snackbar = Snackbar.Make(message, null, "OK", TimeSpan.FromSeconds(3), new SnackbarOptions
            {
                BackgroundColor = Colors.Green,
                TextColor = Colors.White
            }, anchor);
            await snackbar.Show();
        }

        private async Task ShowFail(string message, VisualElement anchor)
        {
            var snackbar = Snackbar.Make(message, null, "OK", TimeSpan.FromSeconds(3), new SnackbarOptions
            {
                BackgroundColor = Colors.Red,
                TextColor = Colors.White
            }, anchor);
            await snackbar.Show();
        }
    }
}
