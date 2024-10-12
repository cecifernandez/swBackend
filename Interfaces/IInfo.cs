using swBackend.Models;

namespace swBackend.Interfaces
{
    public interface IInfo
    {
        string Url { get; set; }
        string Name { get; set; }

        //public IEnumerable<T> Results { get; set; }
    }
}
