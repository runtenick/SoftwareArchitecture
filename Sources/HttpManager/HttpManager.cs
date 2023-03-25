using Api.Pagination;
using Model;
using System.Net.Http.Json;

namespace HttpManager
{
    public class HttpManager
    {
        private readonly HttpClient _httpClient;

        public HttpManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7175/");
        }

        public async Task<Page> GetChampions(int index, int count)
        {
            var result = await _httpClient.GetAsync($"api/Champions?index={index}&count={count}");
            var page = await result.Content.ReadFromJsonAsync<Page>();
            return page;
        }   

        public async Task<Champion> GetChampionByName(string name)
        {
            var result = await _httpClient.GetAsync($"api/Champions/{name}");
            var champion = await result.Content.ReadFromJsonAsync<Champion>();
            return champion;
        }

        public async Task<Champion> PostChampion(Champion champion)
        {
            var result = await _httpClient.GetAsync($"api/{champion}");
            var champRes = await result.Content.ReadFromJsonAsync<Champion>();
            return champion;
        }

        //public async Task<Champion> PutChampion(Champion )
    }
}