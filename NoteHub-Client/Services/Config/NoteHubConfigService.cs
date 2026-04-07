using bus.logic.Result;
using bus.logic.ApiService;

namespace NoteHub_Client.Services.Config
{
    public class NoteHubConfigService : INoteHubConfigService
    {
        const string serverUrl  = "SERVER_URL";
        const string localDb = "LOCAL_DB_PATH"; //default sqlite path
        public string LocalDb => localDb;
        public string ServerUrl
        {
            private get => Preferences.Get(serverUrl, "");
            set => Preferences.Set(serverUrl, value);
        }
        public HttpClient Client
        {
            get {
                return new HttpClient
                {
                    BaseAddress = new Uri(ServerUrl),
                    Timeout = TimeSpan.FromSeconds(5)
                };
            }
        }

    }
}
