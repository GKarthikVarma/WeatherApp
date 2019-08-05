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

namespace WeatherApp
{

    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private MainViewModel viewModel;

		public MainPage()
        {
            InitializeComponent();
			this.BindingContext = viewModel = new ViewModel.MainViewModel();
        }

		private void LocationsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			viewModel.SelectedLocation = e.SelectedItem.ToString();
		}
	}
}
