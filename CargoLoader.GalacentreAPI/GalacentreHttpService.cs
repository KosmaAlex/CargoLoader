using CargoLoader.Domain.Exceptions;
using CargoLoader.GalacentreAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.GalacentreAPI
{
    public class GalacentreHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GalacentreHttpService(HttpClient httpClient, GalacentreApiKey apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey.Key;
        }

        public async Task<GalacentreResponse> GetDataAsync()
        {
            HttpResponseMessage response = await _httpClient
                .GetAsync($"api/v2/catalog/json/?key={_apiKey}&store=msk&select=props,specifications,name,image");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidHttpResponseException
                    ($"{_httpClient.BaseAddress}api/v2/catalog/json/?key={_apiKey}&store=msk&select=props,specifications,name",
                    response.StatusCode.ToString());
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();

            if(jsonResponse.Contains("Не указан ключ доступа"))
            {
                throw new InvalidAccessKeyException(_apiKey, Assembly.GetExecutingAssembly().GetName().Name);
            }

            GalacentreResponse result = JsonConvert.DeserializeObject<GalacentreResponse>(jsonResponse);

            return result;
        }

        public async Task<byte[]> GetImageAsync(string url)
        {
            return await _httpClient.GetByteArrayAsync(url.Replace(_httpClient.BaseAddress.ToString(), string.Empty));
        }
    }
}
