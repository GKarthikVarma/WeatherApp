using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.BusinessServices
{
    public class WeatherDetails
    {
        public string weatherDescription { get; set; }
        public string weatherIcon { get; set; }
        public string currentTemp { get; set; }
        public string placeName { get; set; }
        public string countryName { get; set; }
		public string humidity { get; set; }
		public string pressure { get; set; }
		public string temp_min { get; set; }
		public string temp_max { get; set; }
		public string wind_speed { get; set; }
    }
}
