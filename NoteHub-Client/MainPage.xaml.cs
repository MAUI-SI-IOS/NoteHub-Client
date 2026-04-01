

using CommunityToolkit.Mvvm.ComponentModel;
using NoteHub_Client.ViewModels;
using NoteHub_Client.Views;

namespace NoteHub_Client
{
    public partial class MainPage : ContentPage
    {
        ObservableObject viewModel;

        public IView NoteContentView = new RenderedNoteContentView();
        public MainPage()
        {
            InitializeComponent();
            viewModel = new RenderedNoteViewModel();

            BindingContext = viewModel;
        }

        private void OptionBtnClicked(object? sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true;
        }
    }
}
