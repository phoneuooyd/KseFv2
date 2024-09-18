using KseF.Interfaces;
using KseF.Models;
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
