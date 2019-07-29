
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.BusinessServices;
using Xamarin.Forms;

namespace WeatherApp.ViewModel 
{
	class MainViewModel : INotifyPropertyChanged
    {
        private string _location;

        public string Location
        {
            get { return _location; }
            set
            {
                    if (value != null)
                    {
                        this._location = value;
                    }
            }
        }

        private string _temperature;

        public string Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                OnPropertyChanged();
            }
        }


        private BusinessServices.WeatherDetails _weatherDetail;


        public BusinessServices.WeatherDetails WeatherDetails { get; set; }

        public Command GetWeatherCommand
        {
            get
            {
                return new Command(async () =>
                    {
                        WeatherDetails = 
                            await BusinessServices.WeatherAppApiConnect.GetWeatherDetails(Location, SelectedMetrics);
                        Temperature = WeatherDetails.currentTemp;
                    });
            }
        }

        public string _selectedMetrics;

        public string SelectedMetrics
        {
            get
            {
                if (_selectedMetrics == null)
                {
                    return "metric";
                }
                else
                {
                    return _selectedMetrics;
                }
            }

            set { this._selectedMetrics = value; }
        }



        public MainViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
