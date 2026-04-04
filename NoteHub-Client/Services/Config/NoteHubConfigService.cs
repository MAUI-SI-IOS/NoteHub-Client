
using bus.logic.Result;

namespace NoteHub_Client.Services.Config
{
    public class NoteHubConfigService : INoteHubConfigService
    {
        const string serverUrl  = "SERVER_URL";
        const string localDb = "LOCAL_DB_PATH"; //default sqlite path
        public string ServerUrl
        {
            private get => Preferences.Get(serverUrl, "");
            set => Preferences.Set(serverUrl, value);
        }
        public Result<HttpClient, string> Client
        {
            get {

                if (string.IsNullOrWhiteSpace(ServerUrl))
                {
                    return Result<HttpClient, string>.Failure(localDb);
                }
                try
                {
                    var client = new HttpClient
                    {
                        BaseAddress = new Uri(ServerUrl),
                        Timeout = TimeSpan.FromSeconds(5)
                    };
                    return Result<HttpClient, string>.Success(client);
                }
                catch
                {
                    return Result<HttpClient, string>.Failure(localDb  );
                }
            }
        }
    }
}
