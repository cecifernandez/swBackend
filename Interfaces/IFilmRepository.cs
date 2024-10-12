using swBackend.Models;

namespace swBackend.Interfaces
{
    public interface IFilmRepository
    {
        Task<IEnumerable<FilmModel>> GetAllFilms();
        //Task<Dictionary<string, string>> GetCharactersUrlToNameMap();
    }
}
