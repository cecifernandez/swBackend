using swBackend.Interfaces;

namespace swBackend.Models
{
    public class ResponseModel : IInfo
    {
        public IEnumerable<CharacterModel> Results { get; set; }
        public string Next {  get; set; }
        public string Previous { get; set; }
        public string Url { get; set ; }
        public string Name { get; set; }
    }
}
