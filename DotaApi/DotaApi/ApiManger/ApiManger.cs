using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotaApi.ApiManger
{

    public class ApiManager
    {
        private const string apiUrl = "https://api.opendota.com/api";

        public async Task<string> GetMatch()
        {
            using (HttpClient client = new HttpClient())
            {
                string matchEndpoint = "/matches/7401120722";
                string findMatch = $"{apiUrl}{matchEndpoint}";
               
                try
                {
                    HttpResponseMessage response = await client.GetAsync(findMatch);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return $"Error: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
            }
        }
    }
}
