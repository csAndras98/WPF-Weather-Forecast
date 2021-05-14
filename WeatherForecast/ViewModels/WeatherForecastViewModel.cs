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
        public ObservableCollection<WeatherForecastModel> Forecasts;

        public WeatherForecastViewModel LoadForecasts()
        {
            Forecasts = new ObservableCollection<WeatherForecastModel>();
            return this;
        }
    }
}
