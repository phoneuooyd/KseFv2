using CommunityToolkit.Mvvm.Messaging;
using KseF.Interfaces;
using KseF.Models;
using KseF.Models.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class MainPageViewModel : INotifyPropertyChanged
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

	private MyBusinessEntities _selectedBusinessEntity;

	public MyBusinessEntities SelectedBusinessEntity
	{
		get => _selectedBusinessEntity;
		set
		{
			_selectedBusinessEntity = value;
			OnPropertyChanged();
		}
	}

	public MainPageViewModel(ILocalDbService dbService)
	{
		_dbService = dbService;
		LoadClients();

        WeakReferenceMessenger.Default.Register<MessageSender<MyBusinessEntities>>(this, (r, message) =>
        {
            MyBusinessEntities.Add(message.Value); // Dodaj nową firmę do listy
        });

        WeakReferenceMessenger.Default.Register<EntityUpdatedMessage<MyBusinessEntities>>(this, (r, message) =>
        {
            var updatedEntity = message.Value;

            // Znajdź encję w kolekcji i zaktualizuj jej dane
            var entityToUpdate = MyBusinessEntities.FirstOrDefault(e => e.Id == updatedEntity.Id);
            if (entityToUpdate != null)
            {
                var index = MyBusinessEntities.IndexOf(entityToUpdate);
                MyBusinessEntities[index] = updatedEntity;
            }
        });
    }

	private async void LoadClients()
	{
		var myBusinessEntities = await _dbService.GetItemsAsync<MyBusinessEntities>();
		MyBusinessEntities = new ObservableCollection<MyBusinessEntities>(myBusinessEntities);
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	public async Task ReloadDataAsync()
	{
		var myBusinessEntities = await _dbService.GetItemsAsync<MyBusinessEntities>();
		MyBusinessEntities = new ObservableCollection<MyBusinessEntities>(myBusinessEntities);
	}
}
