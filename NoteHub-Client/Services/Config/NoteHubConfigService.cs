using bus.logic.Result;
using bus.logic.ApiService;
using bus.logic.service;

namespace NoteHub_Client.Services.Config
{
    public class NoteHubConfigService : INoteHubConfigService
    {
        const string SERVER_URL_KEY  = "SERVER_URL";
        
        private readonly List<Action<INoteHubConfigService>> _subsList = new();
        public string? ServerUrl
        {
            get
            {
                var savedUrl = Preferences.Get(SERVER_URL_KEY, null);
                return string.IsNullOrWhiteSpace(savedUrl) ? null : savedUrl;
            }
            set
            {
                Preferences.Set(SERVER_URL_KEY, value); 
                foreach (var updater in _subsList)
                {
                    updater(this);
                }
            }
        }

        public void Subscribe(Action<ServerNoteService.ServerUrlObersvable> updater) => _subsList.Add(updater);


        public void Subscribe(Action<INoteHubConfigService> updater) => _subsList.Add(updater);
    }
}
