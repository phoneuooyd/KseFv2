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
using System.Windows.Input;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging;

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

        public ICommand DeleteCommand { get; }

        public MyProductsViewModel(ILocalDbService dbService)
		{
			_dbService = dbService;
            DeleteCommand = new Command<Product>(async (entity) => await DeleteEntity(entity));
            LoadClients();
		}

		private async void LoadClients()
		{
			var products = await _dbService.GetItemsAsync<Product>();
            Products = new ObservableCollection<Product>(products);
		}

        // Metoda do usuwania elementów
        private async Task DeleteEntity(Product entity)
        {
			if (entity == null) return;

            // Usuwamy z ObservableCollection (aby odświeżyć ListView)
            Products.Remove(entity);

            // Usuwamy z bazy danych
            await _dbService.DeleteItemAsync(entity);

            WeakReferenceMessenger.Default.Send(new EntityDeletedMessage<Product>(entity));
        }

        public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
