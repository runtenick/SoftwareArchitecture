using Api.Pagination;
using Model;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace HttpManager
{
    public class HttpManagerBckp
    {
        private readonly HttpClient _httpClient;

        public HttpManagerBckp(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7175/");
        }

        public async Task<Page> GetChampions(int index, int count)
        {
            var response = await _httpClient.GetAsync($"api/Champions?index={index}&count={count}");
            if(!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error while getting champions, status code: {response.StatusCode}");
            }
            var page = await response.Content.ReadFromJsonAsync<Page>();
            return page;
        }   

        public async Task<Champion> GetChampionByName(string name)
        {
            var response = await _httpClient.GetAsync($"api/Champions/{name}");
            if(!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error while getting champion {name}, status code: {response.StatusCode}");
            }
            var champion = await response.Content.ReadFromJsonAsync<Champion>();
            return champion;
        }

        public async Task<Champion> PostChampion(Champion champion)
        {
            /* here we create a JsonContent object to serialize the champion
             * to JSON for the HTTP request body */
            var championContent = JsonContent.Create(champion);
            var response = await _httpClient.GetAsync($"api/{champion}");
            if(response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error while posting champion {champion.Name}, status code {response.StatusCode}");
            }
            var champRes = await response.Content.ReadFromJsonAsync<Champion>();
            return champRes;
        }

        public async Task<Champion> PutChampion(Champion champion)
        {
            var json = JsonSerializer.Serialize(champion);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Champions/{champion.Name}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error while updating champion, status code: {response.StatusCode}");
            }

            var champRes = await response.Content.ReadFromJsonAsync<Champion>();
            return champRes;
        }

        public async Task DeleteChampion(string name)
        {
            var response = await _httpClient.DeleteAsync($"api/Champions/{name}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error while deleting champion, status code: {response.StatusCode}");
            }
        }
    }
}