using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KseF.Services;
using KseF.Interfaces;

namespace KseF.Models.ViewModels
{
	public class MyClientsViewModel
	{
		private readonly ILocalDbService _dbService;
		public ObservableCollection<ClientEntities> _clientEntities;

		public ObservableCollection<ClientEntities> ClientEntities
		{
			get => _clientEntities;
			set
			{
				_clientEntities = value;
				OnPropertyChanged();
			}
		}

		public MyClientsViewModel(ILocalDbService dbService)
		{
			_dbService = dbService;
			LoadClients();
		}

		private async void LoadClients()
		{
			var clients = await _dbService.GetItemsAsync<ClientEntities>();
			ClientEntities = new ObservableCollection<ClientEntities>(clients);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
