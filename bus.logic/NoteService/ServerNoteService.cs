using bus.logic.ApiService;
using bus.logic.ApiService.Directors;
using bus.logic.models;
using bus.logic.NoteService;
using bus.logic.Result;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace bus.logic.service
{
    public class ServerNoteService: INoteService
    {
        HttpClient _client;
        JsonSerializerOptions _options;
        public ServerNoteService(HttpClient client)
        {
            _client = client;
            this._options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }
        
        public async Task<Result<List<Note>, HttpException>> SearchNote(string token)
        {
            return await new GetStrategy<List<Note>>(_client, $"token/{token}", _options)
                        .DoQuery();            
        }

        public async Task<Result<Note, HttpException>> GetNoteByTitle(string title)
        {
            return await new GetStrategy<Note>(_client, $"note/title/{title}", _options)
                        .DoQuery();
           
        }


        public async Task<Result<Note, HttpException>> CreateUpdateNote(long? id,string title, string note)
        {
            Debug.WriteLine($"[QUERY] {id} {title} {note}");
            if (id == null)
            {
                return await new PostStrategy<Note, Note>(_client, $"/note", new Note(title, note), _options)
                            .DoQuery();
            }
            else
            {
                return await new PatchStrategy<Note, Note>(_client, $"/note", new Note(title, note, id), _options)
                .DoQuery();
            }
        }

        public async Task<bool> Ping()
        {
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
    }

}
