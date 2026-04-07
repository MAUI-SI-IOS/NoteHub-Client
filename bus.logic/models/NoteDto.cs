using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace bus.logic.models
{
    public class NoteDto : INote
    {
        [JsonPropertyName("id")]
        public long? Id { get; set; } // Matches Long? = null

        [JsonPropertyName("title")]
        public string Title { get; set; } // Matches val title: String

        [JsonPropertyName("rawContent")] // WAS raw_content (Fixed to camelCase)
        public string RawContent { get; set; }

        [JsonPropertyName("formattedContent")] // WAS formated_content (Fixed spelling + case)
        public string FormattedContent { get; set; }
        public NoteDto() { }
        public NoteDto(string title, string note, long? id = null)
        {
            Title = title;
            RawContent = note;
            FormattedContent = note;
            Id = id;
        }
    }
}
