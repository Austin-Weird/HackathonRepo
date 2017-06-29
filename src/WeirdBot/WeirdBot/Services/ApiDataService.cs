using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeirdBot.Forms;
using WeirdBot.Models;

namespace WeirdBot.Services
{
    public class ApiDataService
    {
        public ApiDataService() { }

        public async Task<Recommendation> GetComputerPartsRecommendation(BuildComputerForm result)
        {
            var recommendation = new Recommendation();
            using (var httpClient = new HttpClient())
            {
                var apiKey = ConfigurationManager.AppSettings["ApiKey"];
                httpClient.BaseAddress = new Uri(apiKey);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsJsonAsync($"/api/priceLimit/{result.PriceLimit}/recommendation/", result.Category);
                response.EnsureSuccessStatusCode();

                // Deserialize the updated product from the response body.
                recommendation = await response.Content.ReadAsAsync<Recommendation>();
                return recommendation;
            }
        }

    }
}
