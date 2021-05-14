using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeatherForecast
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void WeatherForecastViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModels.WeatherForecastViewModel WeatherForecastViewModelObject = new ViewModels.WeatherForecastViewModel();
            WeatherForecastViewModelObject.LoadForecasts();

            WeatherForecastViewControl.DataContext = WeatherForecastViewModelObject;
        }
    }
}
