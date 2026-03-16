

namespace NoteHub_Client
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void OptionBtnClicked(object? sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true;
        }
    }
}
