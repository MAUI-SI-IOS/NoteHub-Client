

using bus.logic.ApiService;
using bus.logic.ApiService.Directors;
using System.Runtime.Serialization;
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

        public Task<List<Note>> GetNotes()
        {
            return api.Send(new GetDirector<List<Note>>("/note/"));
        }

        public Task<Note> GetNoteByTitle(string title)
        {
            return api.Send(new GetDirector<Note>("note/title/" + title));
        }


        public void AddNote(Note note)
        {
            api.Send(new PostNoteDirector<Note>(note));
        }

        //public void UpdateNote()
        //{
        //return api.Send<Note>(new PatchNoteDirector());
        //}
    }


    public class Note
    {
        [JsonPropertyName("id")]
        string? id;
        [JsonPropertyName("title")]
        string title;
        [JsonPropertyName("raw_content")]
        string RawContent;
    }
}
