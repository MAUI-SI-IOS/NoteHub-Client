using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NoteHub_Client.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NoteHub_Client.ViewModels
{
    public partial class ServerSelectionViewModel : ObservableObject
    {
        private readonly INoteHubConfigService _configService = new NoteHubConfigService();

        [ObservableProperty]
        private string _serverUrl;

        public ServerSelectionViewModel()
        {
            _serverUrl = _configService.ServerConnectionUrl ?? "";
        }


        [RelayCommand]
        public void SubmitServerUrl()
        {
            _configService.ServerConnectionUrl = _serverUrl;
        }
    }
}
