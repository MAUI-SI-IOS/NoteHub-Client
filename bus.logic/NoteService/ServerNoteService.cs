using bus.logic.ApiService;
using bus.logic.ApiService.Directors;
using bus.logic.models;
using bus.logic.NoteService;
using bus.logic.Result;
using System.Diagnostics;
using System.Text.Json;

namespace bus.logic.service
{
    public class ServerNoteService: INoteService {
        public bool IsOK => _client.BaseAddress != null;

        private HttpClient _client = new()
        {
            Timeout = TimeSpan.FromSeconds(5),
        };
        JsonSerializerOptions _options;
        public ServerNoteService(ServerNoteService.ServerUrlObersvable serverUrlObservable) 
        {
            serverUrlObservable.Subscribe((observable) => {
                if (Uri.IsWellFormedUriString(observable.ServerUrl, UriKind.Absolute))
                    _client.BaseAddress = new Uri(observable.ServerUrl);
                else 
                    _client.BaseAddress = null;
            });

            this._options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }
        
        public async Task<Result<List<INote>, NoteServiceException>> SearchNote(string token)
        {
            Debug.Assert(IsOK, "Service should be OK when searching for notes");
            return await new GetStrategy<List<INote>>(_client, $"token/{token}", _options)
                        .DoQuery();            
        }

        public async Task<Result<List<INote>, NoteServiceException>> GetNoteByTitle(string title)
        {
            Debug.Assert(IsOK, "Service should be OK when searching for notes");
            return await new GetStrategy<List<INote>>(_client, $"note/title/{title}", _options)
                        .DoQuery();
           
        }


        public async Task<Result<INote, NoteServiceException>> CreateUpdateNote(long? id, string title, string note)
        {
            Debug.Assert(IsOK, "Service should be OK when creating notes");
            Debug.WriteLine($"[QUERY] {id} {title} {note}");
            if (id == null)
            {
                return await new PostStrategy<NoteDto, INote>(_client, $"/note", new NoteDto(title, note), _options)
                            .DoQuery();
            }
            else
            {
                return await new PatchStrategy<NoteDto, INote>(_client, $"/note", new NoteDto(title, note, id), _options)
                .DoQuery();
            }
        }

        public async Task<bool> Ping()
        {
            Debug.Assert(IsOK, "Service should be OK when pinging the server");
            try
            {
                var response = await _client.GetAsync("note");
                var content = await response.Content.ReadAsStringAsync();
                Debug.Write($"[QUERY] {response.StatusCode}{content}");
                Debug.Write($"[QUERY] {response.ReasonPhrase}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ServerUrlObersvable is an interface that allows the ServerNoteService to be notified when the server URL changes.
        /// It is a dependency of the ServerNoteService, and it is used to update the HttpClient's BaseAddress when the server URL changes.
        /// </summary>
        public interface ServerUrlObersvable
        {
            string? ServerUrl { get; set; }
            void Subscribe(Action<ServerUrlObersvable> updater);
        }
    }

}
