using KseF.Interfaces;
using KseF.Models.Invoice_FA_2;
using KseF.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KseF.Models.ViewModels
{
	public class MyProductsViewModel
	{
		private readonly ILocalDbService _dbService;
		public ObservableCollection<Product> _products;

		public ObservableCollection<Product> Products
		{
			get => _products;
			set
			{
				_products = value;
				OnPropertyChanged();
			}
		}

		public MyProductsViewModel(ILocalDbService dbService)
		{
			_dbService = dbService;
			LoadClients();
		}

		private async void LoadClients()
		{
			var products = await _dbService.GetItemsAsync<Product>();
			Products = new ObservableCollection<Product>(products);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
