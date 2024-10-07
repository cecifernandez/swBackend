namespace swBackend.Models
{
    public class ResponseModel
    {
        public IEnumerable<CharacterModel> Results { get; set; }
        public string Next {  get; set; }
        public int Count { get; set; }
        public string Previous { get; set; }
    }
}
