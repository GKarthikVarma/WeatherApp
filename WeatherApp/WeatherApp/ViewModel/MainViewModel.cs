
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

		private string _locationKeyword;

		public string LocationKeyword
		{
			get
			{
				if (_locationKeyword != null)
				{
					return _locationKeyword;
				}
				else
				{
					return "";
				}
			}

			set
			{
				if (value != _locationKeyword)
				{
					this._locationKeyword = value;
					if (value != null)
					{
						GetLocationListCommand.Execute(null);
					}
					if (_locationKeyword != null && SelectedLocation == null)
					{
						GetListVisibility = true;
					}
					else
					{
						GetListVisibility = false;
					}
					OnPropertyChanged();

				} 

			}

		}

		private List<string> _locationList;

		public List<string> LocationList
		{
			get
			{
				return _locationList;
			}
			set
			{
				_locationList = value;
				PropertyChanged(this, new PropertyChangedEventArgs("LocationList"));
			}
		}

		private string _selectedLocation;

		public string SelectedLocation
		{
			get
			{
				return _selectedLocation;
			}
			set
			{
				_selectedLocation = value;
				if (_selectedLocation != null)
				{
					LocationKeyword = _selectedLocation;
					Location = LocationKeyword;
					_selectedLocation = null;
					OnPropertyChanged();
				}
			}
		}

		private bool _getListVisibility = false;

		public bool GetListVisibility
		{
			get
			{
				return _getListVisibility;
			}
			set
			{
				_getListVisibility = value;
				OnPropertyChanged();
			}
		}


		//private BusinessServices.WeatherDetails _weatherDetail;


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
						/*						if (File.Exists("WeatherApp.IOS/Resources/Weather.txt"))
												{
													File.Delete("WeatherApp.IOS/Resources/Weather.txt");
													File.WriteAllText("WeatherApp.IOS/Resources/Weather.txt", Location);
												}
												else
												{
													File.WriteAllText("WeatherApp.IOS/Resources/Weather.txt", Location);
												}*/
/*						var path = ;
						File.WriteAllText(path, Location);*/

					});
            }
        }

/*		public Command LocationSearchCommand
		{
			get
			{
				return new Command(async () =>
				{

				});
			}
		}*/

		public Command GetLocationListCommand
		{
			get
			{
				return new Command(async () =>
				{
					List<string> temp = new List<string> { };
					temp = await BusinessServices.AutoComplete.GoogleAutoComplete(LocationKeyword);
					if(temp != null)
					{
						LocationList = temp;
					}
					else
					{
						LocationList = new List<string>{ "Invalid" };
					}
				});
			}
		}

		public MainViewModel()
        {
/*			string path = Environment.CurrentDirectory + "WeatherApp.IOS/Resources/Weather.txt";
			if (File.Exists(path))
			{
				File.WriteAllText(path, Location);
				Location = File.ReadAllText(path);
				SelectedMetrics = "metric";
				this.GetWeatherCommand.Execute(null);
			}*/
		}

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
