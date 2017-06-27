using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeirdBot.Forms;
using WeirdBot.Models;

namespace WeirdBot.Services
{
    public class ApiDataService
    {
        public ApiDataService() { }

        public async Task<Recommendation> GetComputerPartsRecommendation(BuildComputerForm result)//float price, List<Category> choices)
        {
            var run = new Recommendation();
            using (var httpClient = new HttpClient())
            {
                var url = new UriBuilder();
                httpClient.BaseAddress = url.Uri;
                //var response = await httpClient.PostAsJsonAsync($"/api/priceLimit/{result.PriceLimit}/usage/getRecommendation/", result.Category);

            }
            return run;
        }
    }
}
