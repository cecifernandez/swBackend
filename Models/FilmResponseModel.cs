using swBackend.Interfaces;
using System.Text.Json.Serialization;

namespace swBackend.Models
{
    public class FilmResponseModel : IInfo
    {
        //public string Next { get; set; }
        //public string Previous { get; set; }

            public IEnumerable<FilmModel> Results { get; set; }

            [JsonPropertyName("title")]
            public string Name { get; set; }

            public string Url { get; set; }

            //[JsonPropertyName("characters")]
            //public string Name { get; set ; }
    }
}
