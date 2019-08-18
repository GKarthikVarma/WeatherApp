using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace WeatherApp.BusinessServices
{
    class WeatherAppApiConnect
    {
        private static string API_KEY = "100c8e6320af16d670b0bcd550ec2213";
        private static string API_BASE = "https://api.openweathermap.org/data/2.5/weather?q=";

        public static async Task<WeatherDetails> GetWeatherDetails(string city, string metric)
        {
            WeatherDetails weather = new WeatherDetails();
            string url = API_BASE + city + "&appid=" + API_KEY + "&units=" + metric;
            string result = "";
            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            result = await client.GetStringAsync(url);
            Console.WriteLine(result);
            var resultObject = JObject.Parse(result);
            weather.weatherDescription = resultObject["weather"][0]["description"].ToString();
            weather.weatherIcon = resultObject["weather"][0]["icon"].ToString();
            weather.currentTemp = resultObject["main"]["temp"].ToString();
			weather.humidity = resultObject["main"]["humidity"].ToString();
			weather.pressure = resultObject["main"]["pressure"].ToString();
			weather.temp_min = resultObject["main"]["temp_min"].ToString();
			weather.temp_max = resultObject["main"]["temp_max"].ToString();
            weather.placeName = resultObject["name"].ToString();
			weather.wind_speed = resultObject["wind"]["speed"].ToString();
            weather.countryName = resultObject["sys"]["country"].ToString();
            return weather;
        }
    }

    
}
