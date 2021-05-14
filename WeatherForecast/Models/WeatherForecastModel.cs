using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherForecast.Models
{
    class WeatherForecastModel
    {
        public WeatherForecastModel(double temperature, DateTime date)
        {
            this.Temperature = temperature;
            this.Date = date;
        }

        public double Temperature { get; }

        public DateTime Date { get; }
    }
}
