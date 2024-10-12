using swBackend.Interfaces;
using swBackend.Models;
using System.Net.Http;

namespace swBackend.Repositories
{
    public class InfoRepository
    {
        private readonly HttpClient _httpClient;

        public InfoRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<Dictionary<string, string>> GetInfo<T>(string api) where T : IInfo
        {
            var toUrl = new Dictionary<string, string>();
            //var response = await _httpClient.GetFromJsonAsync<ResponseWrapperModel<T>>(api);
            ResponseWrapperModel<T> response;

            do
            {
                response = await _httpClient.GetFromJsonAsync<ResponseWrapperModel<T>>(api);

                if (response?.Results != null)
                {
                    foreach (var ent in response.Results)
                    {
                        toUrl[ent.Url] = ent.Name;
                        //toUrl[ent.Url] = ent.Species;
                        //toUrl[ent.Url] = ent.Starships;
                        //toUrl[ent.Url] = ent.Vehicles;
                    }
                    api = response?.Next;
                }
            } while (response != null && !string.IsNullOrEmpty(response.Next));


            return toUrl;
        }
    }
}
