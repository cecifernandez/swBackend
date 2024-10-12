using swBackend.Interfaces;
using swBackend.Models;
using System.Linq;

namespace swBackend.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly HttpClient _httpClient;

        private readonly InfoRepository _infoRepository;

        public CharacterRepository(IHttpClientFactory httpClientFactory, InfoRepository infoRepository)
        {
            _httpClient = httpClientFactory.CreateClient();
            _infoRepository = infoRepository;
        }

        public async Task<IEnumerable<CharacterModel>> GetAllCharacters()
        {
            var characters = new List<CharacterModel>();
            
            var filmsUrlToTitleMap = await _infoRepository.GetInfo<FilmResponseModel>("https://swapi.dev/api/films/");

            var characterInfo = await _infoRepository.GetInfo<PlanetResponseModel>("https://swapi.dev/api/planets/");

            var apiUrl = "https://swapi.dev/api/people/";
            //ResponseModel response;
            ResponseWrapperModel<CharacterModel> response;

            
            do
            {
                response = await _httpClient.GetFromJsonAsync<ResponseWrapperModel<CharacterModel>>(apiUrl);
                

                if (response != null)
                {
                    foreach (var character in response.Results)
                    {
                        // Map film URLs to film titles
                        character.Films = character.Films.Select(filmUrl =>
                            filmsUrlToTitleMap.ContainsKey(filmUrl)
                                ? filmsUrlToTitleMap[filmUrl]
                                : "Unknown Film").ToList();

                        character.Planet = characterInfo.ContainsKey(character.Planet)
                                ? characterInfo[character.Planet]
                                : "Unknown";

                        //character.Starships = characterInfo.ContainsKey(character.Starships)
                        //        ? characterInfo[character.Starships]
                        //        : "Unknown";

                        //character.Vehicles = characterInfo.ContainsKey(character.Vehicles)
                        //        ? characterInfo[character.Vehicles]
                        //        : "Unknown";

                        //character.Species = characterInfo.ContainsKey(character.Species)
                        //        ? characterInfo[character.Species]
                        //        : "Unknown";

                    }

                    characters.AddRange(response.Results);

                    // Update apiUrl with the next page URL for pagination
                    apiUrl = response.Next;
                }
            } while (response != null && !string.IsNullOrEmpty(response.Next));

            return characters;
        }

        //public async Task<Dictionary<string, string>> GetUrl<T>(string api) where T : FilmResponseModel
        //{
        //    var toUrl = new Dictionary<string, string>();
        //    var response = await _httpClient.GetFromJsonAsync<T>(api);

        //    if(response?.Results != null)
        //    {
        //        foreach(var ent in response.Results)
        //        {
        //            toUrl[ent.Url] = ent.Title;
                    
        //        }
        //    }

        //    return toUrl;
        //}

        //public async Task<Dictionary<string, string>> GetInfo<T>(string api) where T : IHasUrlAndName
        //{
        //    var toUrl = new Dictionary<string, string>();
        //    //var response = await _httpClient.GetFromJsonAsync<ResponseWrapperModel<T>>(api);
        //    ResponseWrapperModel<T> response;

        //    do {
        //         response = await _httpClient.GetFromJsonAsync<ResponseWrapperModel<T>>(api);

        //        if (response?.Results != null)
        //        {
        //            foreach (var ent in response.Results)
        //            {
        //                toUrl[ent.Url] = ent.Name;
        //                //toUrl[ent.Url] = ent.Species;
        //                //toUrl[ent.Url] = ent.Starships;
        //                //toUrl[ent.Url] = ent.Vehicles;
        //            }
        //            api = response?.Next;
        //        }
        //    } while (response != null && !string.IsNullOrEmpty(response.Next)) ;
            

        //    return toUrl;
        //}

        //public async Task<Dictionary<string, string>> GetFilmsUrlToTitleMap()
        //{
        //    var filmsUrlToTitle = new Dictionary<string, string>();

        //    var apiUrl = "https://swapi.dev/api/films/";
        //    var response = await _httpClient.GetFromJsonAsync<FilmResponseModel>(apiUrl);

        //    if (response?.Results != null)
        //    {
        //        foreach (var film in response.Results)
        //        {

        //            filmsUrlToTitle[film.Url] = film.Title;
        //        }
        //    }

        //    return filmsUrlToTitle;
        //}
    }
}
