using bus.logic.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bus.logic.service;

namespace NoteHub_Client.Services.Config
{
    public interface INoteHubConfigService : ServerNoteService.ServerUrlObersvable
    {
        string? ServerUrl { get; set; }
        void Subscribe(Action<INoteHubConfigService> updater);
    }
}
