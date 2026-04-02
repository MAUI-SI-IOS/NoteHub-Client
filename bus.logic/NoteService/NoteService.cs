
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

        public Task<Result<Note, Exception>> GetNoteByTitle(string title)
        {
            return api.Send(new GetDirector<Note>($"note/title/{title}"));
        }


        public async Task<Result<Note, Exception>> AddNote(Note note)
        {
            return await api.Send(new PostNoteDirector<Note>(note));
        }

        //public void UpdateNote()
        //{
        //return api.Send<Note>(new PatchNoteDirector());
        //}
    }


    public class Note
    {
        [JsonPropertyName("id")]
        public string? id;
        [JsonPropertyName("title")]
        public string title;
        [JsonPropertyName("raw_content")]
        public string RawContent;
    }
}
