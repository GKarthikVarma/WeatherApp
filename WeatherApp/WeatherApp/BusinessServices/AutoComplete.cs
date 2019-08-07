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

				List<string> predictions = new List<string> { };
				string[] foo;
				string bar;
				foreach(var location in resultObject["predictions"])
				{
					bar = location["description"].ToString();
					predictions.Add(bar);
				}

				List<string> colors = new List<string>
				{
					"Invalid"
				};
				List<string> res = colors;

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
