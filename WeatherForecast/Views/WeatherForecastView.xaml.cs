using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherForecast.ViewModels;

namespace WeatherForecast.Views
{
    /// <summary>
    /// Interaction logic for WeatherForecastView.xaml
    /// </summary>
    public partial class WeatherForecastView : UserControl
    {
        public WeatherForecastView()
        {
            InitializeComponent();
            ForecastViewModel = new WeatherForecastViewModel()
                .LoadCurrent()
                .LoadForecasts();
        }

        private WeatherForecastViewModel ForecastViewModel;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            const double margin = 10;
            double xmin = margin;
            double xmax = canGraph.Width - margin;
            double ymin = margin;
            double ymax = canGraph.Height - margin;
            const double step = 10;

            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(
                new Point(0, ymax), new Point(canGraph.Width, ymax)));
            for (double x = xmin + step;
                x <= canGraph.Width - step; x += step)
            {
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(x, ymax - margin / 2),
                    new Point(x, ymax + margin / 2)));
            }

            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;

            canGraph.Children.Add(xaxis_path);

            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xmin, 0), new Point(xmin, canGraph.Height)));
            for (double y = step; y <= canGraph.Height - step; y += step)
            {
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xmin - margin / 2, y),
                    new Point(xmin + margin / 2, y)));
            }

            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;

            canGraph.Children.Add(yaxis_path);

            Brush brush = Brushes.Red;
            PointCollection points = new PointCollection();

            double LastX = xmin;
            double LastY;
            for (int i = 0; i < ForecastViewModel.Forecasts.Count; i ++)
            {
                LastY = ForecastViewModel.Forecasts.ToArray()[i].Temperature;
                if (LastY < ymin) LastY = (int)ymin;
                if (LastY > ymax) LastY = (int)ymax;
                points.Add(new Point(LastX, Math.Abs(LastY * 10 - ymax)));
                LastX += step;
            }

            Polyline polyline = new Polyline();
            polyline.StrokeThickness = 3;
            polyline.Stroke = brush;
            polyline.Points = points;

            canGraph.Children.Add(polyline);

        }
    }
}
