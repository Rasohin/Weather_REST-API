using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using Newtonsoft.Json;


namespace Weather
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Result result;
        Forecast myForecast;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void ShowCurentResult(string[] s)
        {
            lbl1.Text = s[0];
            lbl2.Content = s[1];
            PicImg.Source = new BitmapImage(new Uri(s[2]));
            humidity.Content = s[3];
            windSpeed.Content = s[4];
            windDir.Content = s[5];
            cloudy.Content = s[6];
            preassure.Content = s[7];
            uvIndex.Content = s[8];
        }

        public void ShowForecastResult(string[] s)
        {
            forecast1.Text = s[0];
            forecast2.Text = s[1];
            forecast3.Text = s[2];
            d1.Text = s[3];
            d2.Text = s[5];
            d3.Text = s[7];
            n1.Text = s[4];
            n2.Text = s[6];
            n3.Text = s[8];
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            
            myForecast = new Forecast(findCity.Text);
            if (findCity.Items.Contains(findCity.Text) == false)
                findCity.Items.Add(findCity.Text);
            result = ShowCurentResult;
            myForecast.GetDelegate(result);
            myForecast.GetCurentResult();
            result = ShowForecastResult;
            myForecast.GetDelegate(result);
            myForecast.GetForecastResult();
            CreateGraph();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CreateGraph()
        {
           
            for(int i=0; i<7; i++)
            {
                Polyline p1 = new Polyline();
                PointCollection pLine = new PointCollection();
                forecastGrid.Children.Add(p1);
                Grid.SetRow(p1, 2);
                p1.Height = 180;
                p1.Width = 420;
                p1.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                p1.StrokeThickness = 1;
                pLine = new PointCollection();
                pLine.Add(new Point(0, p1.Height - i*30));
                pLine.Add(new Point(p1.Width, p1.Height - i*30));
                p1.Points = pLine;
                Label ord = new Label();
                forecastGrid.Children.Add(ord);
                Grid.SetRow(ord, 2);
                ord.Content = $"{i*15-45}";
                ord.Margin = new Thickness(10, 190-i*30, 390, 5+i*30);

            }
            

            ClimateAverages myClimate = new ClimateAverages();
            myClimate = myForecast.GetClimateResult();
            if (myClimate != null)
            {
                PointCollection points = new PointCollection();
                for (int i = 0; i < 12; i++)
                {
                    points.Add(new Point(i * 35, Graph.Height - myClimate.month[i].absMaxTemp * 2 - 90));
                    monthLbl.Content += $"{myClimate.month[i].name}  ";
                }
                Graph.Stroke = new SolidColorBrush(Color.FromRgb(250, 0, 0));
                Graph.Points = points;
                PointCollection points1 = new PointCollection();
                for (int i = 0; i < 12; i++)
                {
                    points1.Add(new Point(i * 35, Graph.Height - myClimate.month[i].avgMinTemp * 2 - 90));
                }
                Graph1.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 250));
                Graph1.Points = points1;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurentLocation myLocation = new CurentLocation();
            myForecast = new Forecast(myLocation.GetLocation());
            findCity.Items.Add(myLocation.GetLocation());
            result = ShowCurentResult;
            myForecast.GetDelegate(result);
            myForecast.GetCurentResult();
            result = ShowForecastResult;
            myForecast.GetDelegate(result);
            myForecast.GetForecastResult();

            CreateGraph();
        }
    }
}
