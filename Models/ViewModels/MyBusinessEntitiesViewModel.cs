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
	public class MyBusinessEntitiesViewModel
	{
		private readonly ILocalDbService _dbService;
		public ObservableCollection<MyBusinessEntities> _myBusinessEntities;

		public ObservableCollection<MyBusinessEntities> MyBusinessEntities
		{
			get => _myBusinessEntities;
			set
			{
				_myBusinessEntities = value;
				OnPropertyChanged();
			}
		}

		public MyBusinessEntitiesViewModel(ILocalDbService dbService)
		{
			_dbService = dbService;
			LoadClients();
		}

		private async void LoadClients()
		{
			var clients = await _dbService.GetItemsAsync<MyBusinessEntities>();
			MyBusinessEntities = new ObservableCollection<MyBusinessEntities>(clients);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
