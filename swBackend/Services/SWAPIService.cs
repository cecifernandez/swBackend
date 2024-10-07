using swBackend.Models;

namespace swBackend.Services
{
    public class SWAPIService
    {
        private readonly HttpClient _httpClient;

        public SWAPIService(HttpClient httpClient) { 
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CharacterModel>> GetAllCharactersAsync()
        {
            var characters = new List<CharacterModel>();
            var filmsUrlToTitleMap = await GetFilmsUrlToTitleMapAsync();
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

        public async Task<IEnumerable<FilmModel>> GetAllFilmsAsync()
        {
            var films = new List<FilmModel>();
            var characterToName = await GetCharactersUrlToNameMapAsync();
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

        public async Task<Dictionary<string, string>> GetFilmsUrlToTitleMapAsync()
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

        public async Task<Dictionary<string, string>> GetCharactersUrlToNameMapAsync()
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
