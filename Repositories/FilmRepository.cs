using swBackend.Interfaces;
using swBackend.Models;

namespace swBackend.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly HttpClient _httpClient;

        public FilmRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IEnumerable<FilmModel>> GetAllFilms()
        {
            var films = new List<FilmModel>();
            var characterToName = await GetCharactersUrlToNameMap();
            var apiUrl = "https://swapi.dev/api/films/";
            FilmResponseModel response;


            do
            {
                response = await _httpClient.GetFromJsonAsync<FilmResponseModel>(apiUrl);
                if (response != null)
                {
                    foreach (var film in response.Results)
                    {

                        film.Characters = film.Characters.Select(characterUrl => characterToName.ContainsKey(characterUrl)
                        ? characterToName[characterUrl]
                        : "Unknown").ToList();

                    }
                    films.AddRange(response.Results);
                    apiUrl = response.Next;
                }
            } while (response != null && !string.IsNullOrEmpty(response.Next));
            return films;
        }
        public async Task<Dictionary<string, string>> GetCharactersUrlToNameMap()
        {
            var characterToName = new Dictionary<string, string>();
            var apiUrl = "https://swapi.dev/api/people/";

            ResponseModel response;

            do
            {
                response = await _httpClient.GetFromJsonAsync<ResponseModel>(apiUrl);
                if (response?.Results != null)
                {
                    foreach (var character in response.Results)
                    {
                        characterToName[character.Url] = character.Name;
                    }
                    apiUrl = response.Next;

                }
            } while (response != null && !string.IsNullOrEmpty(response.Next));
            return characterToName;
        }
    }
}
