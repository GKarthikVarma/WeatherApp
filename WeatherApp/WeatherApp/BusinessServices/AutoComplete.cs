using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.BusinessServices
{
    class AutoComplete
    {
        private static string API_KEY = "AIzaSyCefDSiPHmfDAdfyPGjz5bdh-5xOg2uWnE";
        private static string API_BASE = "https://maps.googleapis.com/maps/api/place/autocomplete/json?";

        public static async Task<List<string>> GoogleAutoComplete(string letter)
        {
			if(letter != "")
			{
				WeatherDetails weather = new WeatherDetails();
				string url = API_BASE + "input=" + letter + "&types=(cities)"+"&key=" + API_KEY;
				string result = "";
				var handler = new HttpClientHandler();
				HttpClient client = new HttpClient(handler);
				result = await client.GetStringAsync(url);
				Console.WriteLine(result);
				var resultObject = JObject.Parse(result);
				/*weather.weatherDescription = resultObject["weather"][0]["description"].ToString();
				weather.weatherIcon = resultObject["weather"][0]["icon"].ToString();
				weather.currentTemp = resultObject["main"]["temp"].ToString();
				weather.placeName = resultObject["name"].ToString();
				weather.countryName = resultObject["sys"]["country"].ToString();*/

				List<string> predictions = new List<string> { };
				string[] foo;
				string bar;
				foreach(var location in resultObject["predictions"])
				{
					bar = location["description"].ToString()/*.Split(',')*/;
					/*bar = foo.First() + "," + foo.Last();*/
					predictions.Add(bar);
				}

				List<string> colors = new List<string>
				{
					"Invalid"
				};
				List<string> res = colors/*.Where(a => a.ToLower().Contains(letter.ToLower())) ?? "Test"*/;/*from c in colors where c.Contains(letter) select c;*/

				return predictions;
			}
			else
			{
				List<string> colors = new List<string>
				{
					"Invalid"
				};
				return colors;
			}

        }
    }
}
