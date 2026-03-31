using bus.logic.ApiService;
using bus.logic.Result;
using bus.logic.service;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Diagnostics;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace NoteHub_Client
{
    public partial class MainPage : ContentPage
    {
        NoteService service;
        long? id = null;
        public MainPage()
        {
            var client = new HttpClient { BaseAddress = new Uri("http://10.0.2.2:8080") };
            this.service = new NoteService(client);
            InitializeComponent();
        }

        private void OptionBtnClicked(object? sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true;
        }

        private async void SaveBtnClick(object? sender, EventArgs e)
        {
            //validation de string
            string title = TitleEditor.Text;
            string note  = NoteEditor.Text;
            if(String.IsNullOrEmpty(title) 
                || String.IsNullOrEmpty(note))
            {
                await this.ShowFail("Title or text can't be null");
            }

            //logic de la fonction
            try
            {
                var action = await this.service.CreateUpdateNote(id,title, note);
                action?.Match(
                   ok: async (note)  =>
                   {
                        this.id = note.Id; //from now on updates the note
                        NewNoteBtn.IsVisible = true;
                        await this.ShowSuccess("Note has been succesfully saved");
                   },
                   err:async (msg)=>
                   {
                       await this.ShowFail(msg);
                   }
                );
            }
            catch(Exception err)
            {
                Debug.WriteLine("[ADD]in catch:" + err.Message);
                await this.ShowFail(err.Message);
            }
        }
        private void NewNoteBtnClick(object? sender, EventArgs e)
        {
            this.id = null;
            TitleEditor.Text = "";
            NoteEditor.Text  = "";
            NewNoteBtn.IsVisible = false;
        }

        //snackbars
        private async Task ShowSuccess(string message)
        {
            var snackbar = Snackbar.Make(message, null, "OK", TimeSpan.FromSeconds(3), new SnackbarOptions
            {
                BackgroundColor = Colors.Green,
                TextColor = Colors.White
            }, ErrorText);
            await snackbar.Show();
        }

        private async Task ShowFail(string message)
        {
            var snackbar = Snackbar.Make(message, null, "OK", TimeSpan.FromSeconds(3), new SnackbarOptions
            {
                BackgroundColor = Colors.Red,
                TextColor = Colors.White
            }, ErrorText);
            await snackbar.Show();
        }
    }
}
