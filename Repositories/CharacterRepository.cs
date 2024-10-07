using swBackend.Interfaces;
using swBackend.Models;

namespace swBackend.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly HttpClient _httpClient;

        public CharacterRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(); 
        }

        public async Task<IEnumerable<CharacterModel>> GetAllCharacters()
        {
            var characters = new List<CharacterModel>();
            var filmsUrlToTitleMap = await GetFilmsUrlToTitleMap();
            var apiUrl = "https://swapi.dev/api/people/";
            ResponseModel response;

            do
            {
                response = await _httpClient.GetFromJsonAsync<ResponseModel>(apiUrl);
                if (response != null)
                {
                    foreach (var character in response.Results)
                    {
                        character.Films = character.Films.Select(filmUrl => filmsUrlToTitleMap.ContainsKey(filmUrl)
                            ? filmsUrlToTitleMap[filmUrl]
                            : "Unknown Film").ToList();
                    }
                    characters.AddRange(response.Results);
                    // Set the next page URL for the next loop iteration
                    apiUrl = response.Next;
                }
            } while (response != null && !string.IsNullOrEmpty(response.Next));

            return characters;
        }
        public async Task<Dictionary<string, string>> GetFilmsUrlToTitleMap()
        {
            var filmsUrlToTitle = new Dictionary<string, string>();

            // Get all films
            var apiUrl = "https://swapi.dev/api/films/";
            var response = await _httpClient.GetFromJsonAsync<FilmResponseModel>(apiUrl);

            if (response?.Results != null)
            {
                foreach (var film in response.Results)
                {

                    filmsUrlToTitle[film.Url] = film.Title;
                }
            }

            return filmsUrlToTitle;
        }
    }
}
