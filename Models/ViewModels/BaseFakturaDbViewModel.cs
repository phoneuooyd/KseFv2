using KseF.Interfaces;
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
	public class BaseFakturaDbViewModel
	{
		private readonly ILocalDbService _dbService;
		public ObservableCollection<BaseFaktura> _invoices;

		public ObservableCollection<BaseFaktura> Invoices
		{
			get => _invoices;
			set
			{
				_invoices = value;
				OnPropertyChanged();
			}
		}

		public BaseFakturaDbViewModel(ILocalDbService dbService)
		{
			_dbService = dbService;
			LoadClients();
		}

		private async void LoadClients()
		{
			var invoices = await _dbService.GetItemsAsync<BaseFaktura>();
			Invoices = new ObservableCollection<BaseFaktura>(invoices);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
