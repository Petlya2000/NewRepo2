﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace Weather1
{
    class Program
    {
        private static HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            try
            {
                WeatherResponse weatherResponse = await GetApiResponseAsync<WeatherResponse>("https://api.openweathermap.org/data/2.5/find?q=Sukhum&units=metric&appid=f5fe011467ae500e6f54951b409d5221");

                foreach (WeatherData weatherData in weatherResponse.list)
                {
                    Console.WriteLine(weatherData.name + ": " + weatherData.main.temp + " °C");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private static async Task<T> GetApiResponseAsync<T>(string url)
        {
            using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
}