
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
						OnPropertyChanged();
                    }
            }
        }

		private string _weatherDescription;

		public string WeatherDesctiption
		{
			get
			{
				return _weatherDescription;
			}
			set
			{
				this._weatherDescription = value;
				OnPropertyChanged();
			}
		}

		private string _pressure;

		public string Pressure
		{
			get
			{
				return this._pressure;
			}
			set
			{
				this._pressure = value;
				OnPropertyChanged();
			}
		}

		private string _humidity;

		public string Humidity
		{
			get
			{
				return this._humidity;
			}
			set
			{
				this._humidity = value;
				OnPropertyChanged();
			}
		}

		private string _min;

		public string Temp_min
		{
			get
			{
				return this._min;
			}
			set
			{
				this._min = value;
				if (SelectedMetrics == "metric")
				{
					_min = _min + " C";
				}
				else
				{
					_min = _min + " F";
				}
				OnPropertyChanged();
			}
		}


		private string _max;

		public string Temp_max
		{
			get
			{
				return this._max;
			}
			set
			{
				this._max = value;
				if (SelectedMetrics == "metric")
				{
					_max = _max + " C";
				}
				else
				{
					_max = _max + " F";
				}
				OnPropertyChanged();
			}
		}

		private string _speed;

		public string WindSpeed
		{
			get
			{
				return this._speed;
			}
			set
			{
				this._speed = value + " m/s";
				OnPropertyChanged();
			}
		}

		public string GetTime
		{
			get
			{
				return DateTime.Now.ToString("h:mm:ss tt");
			}
		}

		private string _temperature;

        public string Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
				if(SelectedMetrics == "metric")
				{
					_temperature = _temperature + " C";
				}
				else
				{
					_temperature = _temperature + " F";
				}
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
					else if(_locationKeyword == "")
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
				if(!(value.Except(new List<string> { "Invalid" }).ToList()).Any())
				{
					GetListVisibility = false;
				}
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
					this.GetListVisibility = false;
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
						WeatherDesctiption = WeatherDetails.weatherDescription;
						Pressure = WeatherDetails.pressure;
						Humidity = WeatherDetails.humidity;
						Temp_min = WeatherDetails.temp_min;
						Temp_max = WeatherDetails.temp_max;
						WindSpeed = WeatherDetails.wind_speed;
						var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
						var filename = Path.Combine(documents, "newstory.txt");
						File.WriteAllText(filename, Location);
					});
            }
        }

		public Command SetCelciusCommand
		{
			get
			{
				return new Command(async () =>
				{
					this.SelectedMetrics = "metric";
				});
			}
		}

		public Command SetFarCommand
		{
			get
			{
				return new Command(async () =>
				{
					this.SelectedMetrics = "imperial";
				});
			}
		}

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
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine(documents, "newstory.txt");
			if (File.Exists(filename))
			{
				var text = File.ReadAllLines(filename);
				foreach(var line in text)
				{
					this.Location = line.ToString();
				}
				this.SelectedMetrics = "metric";
				this.GetWeatherCommand.Execute(null);
			}
		}

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
