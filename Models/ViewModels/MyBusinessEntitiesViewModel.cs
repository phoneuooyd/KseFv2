using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using KseF.Services;
using KseF.Interfaces;
using System.Windows.Input;
using KseF.Pages;
using CommunityToolkit.Mvvm.Messaging;

namespace KseF.Models.ViewModels
{
    public class MyBusinessEntitiesViewModel : INotifyPropertyChanged
    {
        private readonly ILocalDbService _dbService;
        private ObservableCollection<MyBusinessEntities> _myBusinessEntities;

        public ObservableCollection<MyBusinessEntities> MyBusinessEntities
        {
            get => _myBusinessEntities;
            set
            {
                _myBusinessEntities = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; }

        public MyBusinessEntitiesViewModel(ILocalDbService dbService)
        {
            _dbService = dbService;
            DeleteCommand = new Command<MyBusinessEntities>(async (entity) => await DeleteEntity(entity));
            LoadClients();
        }

        // Metoda do ładowania elementów z bazy danych
        private async void LoadClients()
        {
            var businessEntities = await _dbService.GetItemsAsync<MyBusinessEntities>();
            MyBusinessEntities = new ObservableCollection<MyBusinessEntities>(businessEntities);
        }

        // Metoda do usuwania elementów
        private async Task DeleteEntity(MyBusinessEntities entity)
        {
            if (entity == null) return;

            // Usuwamy z ObservableCollection (aby odświeżyć ListView)
            MyBusinessEntities.Remove(entity);

            // Usuwamy z bazy danych
            await _dbService.DeleteItemAsync(entity);

            WeakReferenceMessenger.Default.Send(new EntityDeletedMessage<MyBusinessEntities>(entity));
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
