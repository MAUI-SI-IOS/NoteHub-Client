using bus.logic.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteHub_Client.Services.Config
{
    public interface INoteHubConfigService
    {
        string LocalDb { get; }
        string ServerUrl { set; }
        HttpClient Client { get; }
    }
}
