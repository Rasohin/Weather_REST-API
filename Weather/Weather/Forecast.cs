using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Weather
{
    delegate void Result(string[] str);

    class Forecast
    {
        Result ResDelegate;
        WeatherServerAnswer wsa;

        public void GetDelegate(Result R) => ResDelegate = R;

        public Forecast(string city)
        {
            HttpClient client = new HttpClient();
            string json = "";
            json = client.GetStringAsync("http://api.worldweatheronline.com/premium/v1/weather.ashx?key=e05c9b75c6794b8ab7e51224190306&q=" + city + "&num_of_days=3&fx=yes&tp=12&show_comments=no&mca=yes&format=json").Result;
            wsa = JsonConvert.DeserializeObject<WeatherServerAnswer>(json);
        }

        public void GetCurentResult()
        {
            string[] str = new string[9];
            try
            {
                str[0] = $"Погода в {wsa.data.request[0].query}";
                str[1] = $"{wsa.data.current_condition[0].temp_C}°C";
                str[2] = $"{wsa.data.current_condition[0].weatherIconUrl[0].value}";
                str[3] = $"Влажность воздуха: {wsa.data.current_condition[0].humidity}%";
                str[4] = $"Скорость ветра: {Math.Round((double)wsa.data.current_condition[0].windspeedKmph * 1000 / 3600, 2)} м/с";
                str[5] = $"Направление ветра: {wsa.data.current_condition[0].winddir16Point}";
                str[6] = $"Облачность: {wsa.data.current_condition[0].cloudcover}% ({wsa.data.current_condition[0].weatherDesc[0].value})";
                str[7] = $"Давление: {Convert.ToInt32(wsa.data.current_condition[0].pressure*0.75)} мм рт.ст.";
                str[8] = $"UV индекс: {wsa.data.current_condition[0].uvIndex}";
                ResDelegate?.Invoke(str);
            }
            catch
            {
                MessageBox.Show("Нет прогноза для этого города. Попробуйте другой");
            }
        }

        public void GetForecastResult()
        {
            string[] str = new string[9];
            try
            {
                str[0] = $"{wsa.data.weather[0].date}";
                str[1] = $"{wsa.data.weather[1].date}";
                str[2] = $"{wsa.data.weather[2].date}";
                str[3] = $"{wsa.data.weather[0].maxtempC}°C";
                str[4] = $"{wsa.data.weather[0].mintempC}°C";
                str[5] = $"{wsa.data.weather[1].maxtempC}°C";
                str[6] = $"{wsa.data.weather[1].mintempC}°C";
                str[7] = $"{wsa.data.weather[2].maxtempC}°C";
                str[8] = $"{wsa.data.weather[2].mintempC}°C";
                ResDelegate?.Invoke(str);
            }
            catch
            {
               
            }
        }

        public ClimateAverages  GetClimateResult()
        {
            try
            {
                return wsa.data.ClimateAverages[0];
            }
            catch
            {
                return null;
            }
        }
    }
}
