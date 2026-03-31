
using bus.logic.Result;
using bus.logic.ApiService;
using bus.logic.ApiService.Directors;
using System.Text.Json.Serialization;

namespace bus.logic.service
{
    public class NoteService
    {
        Api api;
        public NoteService(HttpClient client)
        {
            api = new Api(client);
        }
        
        public async Task<Result<List<Note>,Exception>> SearchNote(string token)
        {
            return await api.Send(new GetDirector<List<Note>>($"/token/{token}"));
                            
        }

        public async Task<Result<Note, Exception>> GetNoteByTitle(string title)
        {
            return await api.Send(new GetDirector<Note>($"/note/title/{title}"));
        }


        public async Task<Result<Unit,Exception>> AddNote(string title, string note)
        {
            return await api.Send(new PostNoteDirector<Unit>($"/note",new Note (title, note)));
        }

        //public void UpdateNote()
        //{
        //return api.Send<Note>(new PatchNoteDirector());
        //}
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

        public Note(string title, string note)
        {
            Title = title;
            RawContent = note;
            FormattedContent = note;
        }
    }
}
