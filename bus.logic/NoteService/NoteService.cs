
using bus.logic.ApiService;
using bus.logic.ApiService.Directors;
using bus.logic.Result;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace bus.logic.service
{
    public class NoteService
    {
        HttpClient _client;
        JsonSerializerOptions _options;
        public NoteService(HttpClient client)
        {
            this._client = client;
            this._options = new JsonSerializerOptions { 
                
            };
        }
        
        public async Task<Result<List<Note>,string>> SearchNote(string token)
        {
            var task = await new GetStrategy<List<Note>>(_client, $"/token/{token}", _options)
                        .DoQuery();
            return task.MapErr((err) => err.code switch
            {
                0 => "No connection was found", //httpRequestException retourn 0 quand il n'a pas de connection
                _ => "Internal Server Error, pls try in a bit",
            });                  
        }

        public async Task<Result<Note, string>> GetNoteByTitle(string title)
        {
            var task = await new GetStrategy<Note>(_client, $"/note/title/{title}", _options)
                        .DoQuery();
            return task.MapErr((err) => err.code switch
            {
                0 => "No connection was found", //httpRequestException retourn 0 quand il n'a pas de connection
                _ => "Internal Server Error, pls try in a bit",
            });
        }


        public async Task<Result<Note,string>> CreateUpdateNote(long? id,string title, string note)
        {
            if (id == null)
            {
                var task = await new PostStrategy<Note, Note>(_client, $"/note", new Note(title, note), _options)
                            .DoQuery();

                return task.MapErr((err) => err.code switch
                {
                    0 => "No connection was found", //httpRequestException retourn 0 quand il n'a pas de connection
                    _ => "Internal Server Error, pls try in a bit",
                });
            }
            else
            {
                var task = await new PatchStrategy<Note, Note>(_client, $"/note", new Note(title, note, id), _options)
                .DoQuery();

                return task.MapErr((err) => err.code switch
                {
                    0 => "No connection was found", //httpRequestException retourn 0 quand il n'a pas de connection
                    _ => "Internal Server Error, pls try in a bit",
                });
            }
        }
    }


    public class Note
    {
        [JsonPropertyName("id")]  
        public long? Id { get; set; } // Matches Long? = null

        [JsonPropertyName("title")]
        public string Title { get; set; } // Matches val title: String

        [JsonPropertyName("rawContent")] // WAS raw_content (Fixed to camelCase)
        public string RawContent { get; set; }

        [JsonPropertyName("formattedContent")] // WAS formated_content (Fixed spelling + case)
        public string FormattedContent { get; set; }

        public Note(string title, string note, long? id = null)
        {
            Title = title;
            RawContent = note;
            FormattedContent = note;
            Id = id;
        }
    }
}
