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
        public WeatherForecastModel CurrentWeather { get; set; }
        public List<WeatherForecastModel> Forecasts { get; set; }

        private const string API_KEY = "b2ecfac7410f0139da14742856e5f509";

        public WeatherForecastViewModel LoadCurrent()
        {
            string jsonString = "";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q=budapest&appid={API_KEY}&units=metric";

            using (var client = new HttpClient())
            {
                var result = client.GetAsync(url).Result;
                if (result.IsSuccessStatusCode)
                {
                    jsonString = result.Content.ReadAsStringAsync().Result;
                }
                var json = JObject.Parse(jsonString);

                CurrentWeather = new WeatherForecastModel(
                    Convert.ToDouble(json.GetValue("main")["temp"]),
                    Convert.ToDateTime(json["dt_txt"])
                    );
            }
            return this;
        }

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
                Forecasts.Add(new WeatherForecastModel(11, DateTime.Now));
            }
            return this;
        }
    }
}
