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
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;

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
        public ICommand DeleteCommand { get; }
        public MyClientsViewModel(ILocalDbService dbService)
		{
			_dbService = dbService;
            DeleteCommand = new Command<ClientEntities>(async (entity) => await DeleteEntity(entity));
            LoadClients();
		}

		private async void LoadClients()
		{
			var clients = await _dbService.GetItemsAsync<ClientEntities>();
			ClientEntities = new ObservableCollection<ClientEntities>(clients);
		}

        // Metoda do usuwania elementów
        private async Task DeleteEntity(ClientEntities entity)
        {
            if (entity == null) return;

			// Usuwamy z ObservableCollection (aby odświeżyć ListView)
			ClientEntities.Remove(entity);

            // Usuwamy z bazy danych
            await _dbService.DeleteItemAsync(entity);

            WeakReferenceMessenger.Default.Send(new EntityDeletedMessage<ClientEntities>(entity));
        }

        public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
