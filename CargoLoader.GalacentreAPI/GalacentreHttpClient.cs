﻿using CargoLoader.Domain.Exceptions;
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
    public class GalacentreHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GalacentreHttpClient(HttpClient httpClient, GalacentreApiKey apiKey)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://www.galacentre.ru/api/v2/catalog/json/");
            _apiKey = apiKey.Key;
        }

        public async Task<GalacentreResponse> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"?key={_apiKey}{uri}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidHttpResponseException($"{_httpClient.BaseAddress}?key={_apiKey}{uri}");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();

            if(jsonResponse.Contains("Не указан ключ доступа"))
            {
                throw new InvalidAccessKeyException(_apiKey, Assembly.GetExecutingAssembly().GetName().Name);
            }

            GalacentreResponse result = JsonConvert.DeserializeObject<GalacentreResponse>(jsonResponse);

            return result;
        }
    }
}
