using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using WeatherForecast.Models;

namespace WeatherForecast.ViewModels
{
    class WeatherForecastViewModel
    {
        public List<WeatherForecastModel> Forecasts;

        private const string API_KEY = "3c850b0463346d2fffad82b66d5eb561";

        public WeatherForecastViewModel LoadForecasts()
        {
            Forecasts = new List<WeatherForecastModel>();

            string url = $"https://api.openweathermap.org/data/2.5/forecast?appid={API_KEY}&units=metric&q=budapest";
            string jsonString = "";

            using (var client = new HttpClient())
            {
                var result = client.GetAsync(url).Result;
                
                if (result.IsSuccessStatusCode)
                {
                    jsonString = result.Content.ReadAsStringAsync().Result;
                }

                var json = JObject.Parse(jsonString).GetValue("list");

                foreach (var token in json)
                {
                    Forecasts.Add(new WeatherForecastModel(
                        Convert.ToDouble(token["main"]["temp"]),
                        Convert.ToDateTime(token["dt_txt"])
                        ));
                }
            }
            return this;
        }
    }
}
