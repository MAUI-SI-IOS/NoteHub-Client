using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;


namespace NoteHub_Client.Services
{
    public static class DisplayService
    {
        //service a construire snackbars
        public static async Task ShowSuccess(string message, VisualElement anchor = null)
        {
            var snackbar = Snackbar.Make(message, null, "OK", TimeSpan.FromSeconds(3), new SnackbarOptions
            {
                BackgroundColor = Colors.Green,
                TextColor = Colors.White
            }, anchor);
            await snackbar.Show();
        }

        public static async Task ShowFailure(string message, VisualElement anchor = null)
        {
            var snackbar = Snackbar.Make(message, null, "OK", TimeSpan.FromSeconds(3), new SnackbarOptions
            {
                BackgroundColor = Colors.Red,
                TextColor = Colors.White
            }, anchor);
            await snackbar.Show();
        }
        public static async Task ShowWarning(string message, VisualElement anchor=null)
        {
            var snackbar = Snackbar.Make(message, null, "OK", TimeSpan.FromSeconds(3), new SnackbarOptions
            {
                BackgroundColor = Colors.OrangeRed,
                TextColor = Colors.White
            }, anchor);
            await snackbar.Show();
        }
    }
}
