// See https://aka.ms/new-console-template for more information

using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;

namespace ConsoleClient
{
    static class Porgram
    {
        static async Task Main()
        {
            Console.WriteLine("Start...");
            using(var client = new HttpClient())
            {
                var result = await client.GetAsync("http://localhost:7175/api/Champions");
                var champions = await result.Content.ReadFromJsonAsync<List<ChampionDto>>();

                foreach(var champ in champions)
                {
                    Console.WriteLine(champ.Name);
                }
            }
        }
    }
}
