namespace swBackend.Models
{
    public class ResponseWrapperModel<T>
    {

        public IEnumerable<T> Results { get; set; }
        public string Previous { get; set; }


        public string Next { get; set; }

    }
}
