using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AppCenter
{
    /// <summary>
    /// Interaction logic for WeatherApp.xaml
    /// </summary>
    public partial class WeatherApp : Window, INotifyPropertyChanged
    {
        private readonly string apiKey = "932171a9865d433d82894122242809";
        private readonly HttpClient httpClient;

        private string _cityName;

        public string CityName
        {
            get { return _cityName; }
            set
            {
                _cityName = value;
                OnPropertyChanged(nameof(CityName)); // Notify the UI that CityName has changed
            }
        }
        private DateTime _now;

        public DateTime Now
        {
            get { return _now; }
            set
            {
                _now = value;
                OnPropertyChanged(nameof(Now));
            }
        }

        public WeatherApp()
        {
            InitializeComponent();
            DataContext = this;

            httpClient = new HttpClient();

            lblDigitalClock.Visibility = Visibility.Hidden;

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, args) =>
            {
                Now = DateTime.Now;
            };
            timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private async void btnGetWeather_Click(object sender, RoutedEventArgs e)
        {
            CityName = txtCityName.Text.Trim();
            if (string.IsNullOrEmpty(CityName))
            {
                MessageBox.Show("Enter A City Name");
                return;
            }

            string apiUrl = $"http://api.weatherapi.com/v1/current.json?key={apiKey}&q={Uri.EscapeDataString(CityName)}&aqi=yes";

            try
            {
                var weatherData = await FetchWeatherData(apiUrl);
                if (weatherData != null)
                {
                    DisplayWeatherData(weatherData);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }


        private async Task<WeatherData> FetchWeatherData(string apiUrl)
        {
            using (HttpClient localClient = new HttpClient())
            {
                HttpResponseMessage response = await localClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WeatherData>(jsonResponse);

            }
        }

        private void DisplayWeatherData(WeatherData weatherData)
        {
            lblCityName.Content = weatherData.Location.Name;
            lblTemperature.Content = weatherData.Current.TempC + "°C";
            lblCondition.Content = weatherData.Current.Condition.Text;
            lblHumidity.Content = weatherData.Current.Humidity + "%";

            BitmapImage weatherIcon = new BitmapImage();
            weatherIcon.BeginInit();
            weatherIcon.UriSource = new Uri("http:" + weatherData.Current.Condition.Icon);
            weatherIcon.EndInit();
            imgWeatherIcon.Source = weatherIcon;

            lblWindSpeed.Content = weatherData.Current.WindKph + " km/h";
        }
    }
}
