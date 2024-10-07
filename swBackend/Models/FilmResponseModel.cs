namespace swBackend.Models
{
    public class FilmResponseModel
    {
            public int Count { get; set; }
            public string Next { get; set; }
            public string Previous { get; set; }
            public IEnumerable<FilmModel> Results { get; set; } 

    }
}
