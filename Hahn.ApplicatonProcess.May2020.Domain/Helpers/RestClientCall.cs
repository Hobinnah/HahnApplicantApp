using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Domain.Helpers
{
    public class RestClientCall<T> where T : class
    {
        public async Task<T> Get(string url)
        {
            List<T> responseObject = new List<T>();
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync(url);

               
                if (response.IsSuccessStatusCode)
                {
                    var reply = response.Content.ReadAsStringAsync().Result;
                    responseObject = JsonConvert.DeserializeObject<List<T>>(reply);
                    
                    return responseObject[0];
                }
                   
               
            }

            return null;
        }
    }
}
