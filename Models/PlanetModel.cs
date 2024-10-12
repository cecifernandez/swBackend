using System.Text.Json.Serialization;

namespace swBackend.Models
{
    public class PlanetModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("residents")]
        public List<string> Residents { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
