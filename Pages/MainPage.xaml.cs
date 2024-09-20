using Microsoft.Maui.Controls;
using KseF.Services;
using KseF.Interfaces;
using KseF.Models;
using KseF.Models.ViewModels;
using SQLite;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace KseF.Pages
{
	public partial class MainPage : ContentPage, INotifyPropertyChanged
	{
		private readonly ILocalDbService _dbService;
		private MainPageViewModel _viewModel;
		string DatabaseName { get; set; }
		List<MyBusinessEntities> MyBusinessEntities { get; set; }
		SQLiteAsyncConnection SQLiteConnection { get; set; }
		MyBusinessEntities BusinesEntityInContext { get; set; } = new MyBusinessEntities();

		public MainPage(ILocalDbService dbService)
		{
			InitializeComponent();
			_dbService = dbService;
			_viewModel = new MainPageViewModel(_dbService);
			SQLiteConnection = _dbService.GetDbConnection().Result;
			DatabaseName = SQLiteConnection.DatabasePath;
			DbNameLabel.Text = DatabaseName;
			var loadingMBE = _dbService.GetItemsAsync<MyBusinessEntities>().Result;
			MyBusinessEntities = loadingMBE;
            BindingContext = _viewModel;
			MyBusinessEntityContextPicker.SelectedIndex = 0;
		}

		public string HasConnection()
		{
			if (SQLiteConnection != null)
			{
				return "Connected";
			}
			else
			{
				return "not Connected";
			}
		}

		private async void OnMyBusinessEntityContextPickerSelectedIndexChanged(object sender, EventArgs e)
		{
			if (MyBusinessEntityContextPicker.SelectedItem is MyBusinessEntities selectedEntity)
			{
				await _dbService.SetBusinessEntityToContext(selectedEntity);
				var test = _dbService.GetBusinessEntityFromContext().Result;
				
				await DisplayAlert("Business Entity", "Business Entity in Context: " + test.Nip, "OK");
			}
		}

		private async void OnPickerTapped(object sender, EventArgs e)
		{
			//await _viewModel.ReloadDataAsync();
			
		}

		// Navigate to SendInvoiceToKsef page
		private async void OnSendInvoiceButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SendInvoiceToKsef(_dbService));
		}


	}
}
