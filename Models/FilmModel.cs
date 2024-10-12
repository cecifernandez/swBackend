using System.Text.Json.Serialization;

namespace swBackend.Models
{
    public class FilmModel 
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("characters")]
        public List<string> Characters { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }


    }
}
