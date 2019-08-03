using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.BusinessServices
{
    class AutoComplete
    {
        private static string API_KEY = "AIzaSyCefDSiPHmfDAdfyPGjz5bdh-5xOg2uWnE";
        private static string API_BASE = "https://maps.googleapis.com/maps/api/place/autocomplete/";

        public static async Task<List<string>> GoogleAutoComplete(string letter)
        {
            WeatherDetails weather = new WeatherDetails();
            string url = API_BASE + "input=" + letter + "&key" + API_KEY;
            string result = "";
            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            result = await client.GetStringAsync(url);
            Console.WriteLine(result);
            List<String> res = null;

            return res;
        }
    }


}
