using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NoteHub_Client.Services;
using NoteHub_Client.Services.Config;

namespace NoteHub_Client.ViewModels
{
    public partial class ServerSelectionViewModel : ObservableObject
    {
        private readonly INoteHubConfigService _config;

        [ObservableProperty]
        private string _serverUrl;

        public ServerSelectionViewModel(INoteHubConfigService config)
        {
            _config    = config;
            _serverUrl = _config.Client.ToString() ?? "";
        }


        [RelayCommand]
        public void SubmitServerUrl()
        {
            _config.ServerUrl = ServerUrl; 
        }
    }
}
