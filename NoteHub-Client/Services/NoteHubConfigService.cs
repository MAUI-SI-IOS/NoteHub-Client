using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteHub_Client.Services
{
    public class NoteHubConfigService : INoteHubConfigService
    {
        const string SERVER_URL_PREFERENCE_KEY = "nothub_servel_url";
        public string? ServerConnectionUrl
        {
            get => Preferences.Get(SERVER_URL_PREFERENCE_KEY, null);

            set
            {
                if (value != null)
                    Preferences.Set(SERVER_URL_PREFERENCE_KEY, value);
                else
                    Preferences.Remove(SERVER_URL_PREFERENCE_KEY);
            }
        }
    }
}
