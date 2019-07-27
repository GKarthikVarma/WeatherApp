using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModel;
using Xamarin.Forms;


//Save data to file
namespace WeatherApp
{

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
		private string API_KEY = "100c8e6320af16d670b0bcd550ec2213";
		private string API_BASE = "https://api.openweathermap.org/data/2.5/weather?q=";
		private string unit = "metric";
        //Use settings to store api key and api base
        //Use a select drop down to select between metrics
		private MainViewModel viewModel;

		public MainPage()
        {
            InitializeComponent();
			this.BindingContext = viewModel = new ViewModel.MainViewModel();
			GetWeather("Cincinnati");
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			GetWeather(Location.Text);
		}

		async public void GetWeather(string place)
		{
            // Bind to C# object 
			string url = API_BASE + place + "&appid=" + API_KEY + "&units=" + unit;
			string result = "";
			var handler = new HttpClientHandler();
			HttpClient client = new HttpClient(handler);
			result = await client.GetStringAsync(url);
			Console.WriteLine(result);
			var resultObject = JObject.Parse(result);
			string weatherDescription = resultObject["weather"][0]["description"].ToString();
			string icon = resultObject["weather"][0]["icon"].ToString();
			Temp.Text = resultObject["main"]["temp"].ToString();
			string placename = resultObject["name"].ToString();
			string country = resultObject["sys"]["country"].ToString();
		}
	}
}
