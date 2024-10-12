using swBackend.Interfaces;
using swBackend.Models;
using System.Collections.Generic;
using System.Linq;

namespace swBackend.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly HttpClient _httpClient;
        private readonly InfoRepository _infoRepository;

        public FilmRepository(IHttpClientFactory httpClientFactory, InfoRepository infoRepository)
        {
            _httpClient = httpClientFactory.CreateClient();
            _infoRepository = infoRepository;
        }

        public async Task<IEnumerable<FilmModel>> GetAllFilms()
        {
            var films = new List<FilmModel>();
            var characterToName = await _infoRepository.GetInfo<ResponseModel>("https://swapi.dev/api/people/");
            var apiUrl = "https://swapi.dev/api/films/";
            //FilmResponseModel response;
            ResponseWrapperModel<FilmModel> response;


            do
            {
                response = await _httpClient.GetFromJsonAsync<ResponseWrapperModel<FilmModel>>(apiUrl);
                if (response != null)
                {
                    foreach (var film in response.Results)
                    {

                        film.Characters = film.Characters.Select(characterUrl => 
                        characterToName.ContainsKey(characterUrl)
                        ? characterToName[characterUrl]
                        : "Unknown").ToList();
                    }
                    films.AddRange(response.Results);
                    apiUrl = response.Next;
                }
            } while (response != null && !string.IsNullOrEmpty(response.Next));
            return films;
        }

    }
}
