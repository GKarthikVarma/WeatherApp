
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace WeatherApp.ViewModel
{
	class MainViewModel
	{
		private string API_KEY = "100c8e6320af16d670b0bcd550ec2213";
		private string API_BASE = "https://api.openweathermap.org/data/2.5/weather?q=";
		private string unit = "metric";

		public string Location { get; set; } = "London";

		private string _currentTemp = "";
		public string currentTemp
		{
			get
			{
				return _currentTemp;
			}
			set
			{
				_currentTemp = value;
			}
		}

		public MainViewModel()
		{

		}

		async public void GetWeather(string place)
		{
			string url = API_BASE + place + "&appid=" + API_KEY + "&units=" + unit;
			string result = "";
			var handler = new HttpClientHandler();
			HttpClient client = new HttpClient(handler);
			result = await client.GetStringAsync(url);
			Console.WriteLine(result);
			var resultObject = JObject.Parse(result);
			string weatherDescription = resultObject["weather"][0]["description"].ToString();
			string icon = resultObject["weather"][0]["icon"].ToString();
			currentTemp = resultObject["main"]["temp"].ToString();
			string placename = resultObject["name"].ToString();
			string country = resultObject["sys"]["country"].ToString();
		}
	}
}
