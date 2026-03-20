using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteHub_Client.Services
{
    public interface INoteHubConfigService
    {
        string? ServerConnectionUrl { get; set;  }
    }
}
