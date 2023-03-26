using Api.Mapper;
using Api.Pagination;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpManager
{
    public partial class HttpManager
    {
        public class ChampionMgr : IChampionsManager
        {
            private readonly HttpManager parent;
            public ChampionMgr(HttpManager parent)
                => this.parent = parent;

            // GET
            public async Task<IEnumerable<Champion?>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                var response = await parent.HttpClient.GetAsync($"api/Champions?index={index}&count={count}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error while getting champions, status code: {response.StatusCode}");
                }
                var page = await response.Content.ReadFromJsonAsync<Page>();
                return page.MyChampions.Select(c => c.ToModel());
            }

            // GET by NAME
            public async Task<IEnumerable<Champion?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                var response = await parent.HttpClient.GetAsync($"api/Champions/{substring}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error while getting champion {substring}, status code: {response.StatusCode}");
                }
                var champion = await response.Content.ReadFromJsonAsync<Champion>();
                var champions = new List<Champion>
                {
                    champion
                };
                return champions;
            }

            // POST
            public async Task<Champion?> AddItem(Champion? item)
            {
                /* here we create a JsonContent object to serialize the champion
                * to JSON for the HTTP request body */
                var championContent = JsonContent.Create(item);
                var response = await parent.HttpClient.GetAsync($"api/{item}");
                if (response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error while posting champion {item.Name}, status code {response.StatusCode}");
                }
                var champRes = await response.Content.ReadFromJsonAsync<Champion>();
                return champRes;
            }

            // DELETE
            public async Task<bool> DeleteItem(Champion? item)
            {
                var response = await parent.HttpClient.DeleteAsync($"api/Champions/{item.Name}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error while deleting champion, status code: {response.StatusCode}");
                }
                else
                {
                    return true;
                }
            }

            //PUT
            public async Task<Champion?> UpdateItem(Champion? oldItem, Champion? newItem)
            {
                var json = JsonSerializer.Serialize(newItem);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await parent.HttpClient.PutAsync($"api/Champions/{oldItem.Name}", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error while updating champion, status code: {response.StatusCode}");
                }

                var champRes = await response.Content.ReadFromJsonAsync<Champion>();
                return champRes;
            }
            

            public Task<IEnumerable<Champion?>> GetItemsByCharacteristic(string charName, int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Champion?>> GetItemsByClass(ChampionClass championClass, int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Champion?>> GetItemsByRunePage(RunePage? runePage, int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Champion?>> GetItemsBySkill(Skill? skill, int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Champion?>> GetItemsBySkill(string skill, int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItems()
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItemsByCharacteristic(string charName)
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItemsByClass(ChampionClass championClass)
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItemsByName(string substring)
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItemsByRunePage(RunePage? runePage)
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItemsBySkill(Skill? skill)
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItemsBySkill(string skill)
            {
                throw new NotImplementedException();
            }
        }
    }
}
