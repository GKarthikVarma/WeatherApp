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
    }
}
