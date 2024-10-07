using System.Text.Json.Serialization;

namespace swBackend.Models
{
    public class CharacterModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("films")]
        public List<string> Films { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
