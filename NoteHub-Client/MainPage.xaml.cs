using bus.logic.ApiService;
using bus.logic.service;
using bus.logic.Result;
using System.Diagnostics;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace NoteHub_Client
{
    public partial class MainPage : ContentPage
    {

        NoteService service;
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
            string note = NoteEditor.Text;
            if(String.IsNullOrEmpty(title) 
                || String.IsNullOrEmpty(note))
            {
                await this.ShowFail("Title or text can't be null");
            }

            //logic de la fonction
            try
            {
                var action = await this.service.AddNote(title, note);
                action?.Match(
                   ok: async (_)  =>
                   {
                       TitleEditor.Text = "";
                       NoteEditor.Text = "";
                       //snackbar
                       await this.ShowSuccess("Note has been succesfully added");
                   },
                   err:async (err)=>
                   {
                       Debug.WriteLine("[ADD]in result: " + err.Message);
                       await this.ShowFail(err.Message);
                   }
                );
            }
            catch(Exception err)
            {
                Debug.WriteLine("[ADD]in catch:" + err.Message);
                await this.ShowFail(err.Message);
            }
        }


        //snackbar for on succes
        private async Task ShowSuccess(string message)
        {
            var snackbar = Snackbar.Make(message, null, "OK", TimeSpan.FromSeconds(3), new SnackbarOptions
            {
                BackgroundColor = Colors.Green,
                TextColor = Colors.White
            });
            await snackbar.Show();
        }

        private async Task ShowFail(string message)
        {
            var snackbar = Snackbar.Make(message, null, "OK", TimeSpan.FromSeconds(3), new SnackbarOptions
            {
                BackgroundColor = Colors.Red,
                TextColor = Colors.White
            });
            await snackbar.Show();
        }
    }
}
