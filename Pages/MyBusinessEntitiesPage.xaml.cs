using CommunityToolkit.Mvvm.ComponentModel;
using KseF.Interfaces;
using KseF.Models;
using KseF.Models.ViewModels;
using KseF.Services;

namespace KseF.Pages;

public partial class MyBusinessEntitiesPage : ContentPage
{
	private readonly ILocalDbService _dbService;
	public List<MyBusinessEntities> myBusinessEntities;
	private MyBusinessEntitiesViewModel _viewModel;

	public MyBusinessEntitiesPage(ILocalDbService dbService)
	{
		InitializeComponent();
		_dbService = dbService;
		_viewModel = new MyBusinessEntitiesViewModel(_dbService);
		myBusinessEntities = _dbService.GetItemsAsync<MyBusinessEntities>().Result;
		BindingContext = _viewModel;
	}

	private async void OnAddMyBusinessEntityButtonClicked(object sender, EventArgs e)
	{
		var newBusinessEntityPage = new AddMyBusinessEntityPage(_dbService, _viewModel); // Przeka¿ ViewModel do nowej strony
		newBusinessEntityPage.BusinessEntityAdded += OnBusinessEntityAdded; // Zarejestruj handler eventu
		await Navigation.PushAsync(newBusinessEntityPage);
	}

	private void OnBusinessEntityAdded(object sender, MyBusinessEntities businessEntity)
	{
		_viewModel.MyBusinessEntities.Add(businessEntity);
	}

	private async void OnMyBusinessEntityTapped(object sender, ItemTappedEventArgs e)
	{
		var businessEntitity = e.Item as MyBusinessEntities;
		if (businessEntitity != null)
		{
			var editBusinessEntityPage = new AddMyBusinessEntityPage(_dbService, _viewModel, businessEntitity);
			editBusinessEntityPage.BusinessEntityAdded += OnBusinessEntityAdded;
			await Navigation.PushAsync(editBusinessEntityPage);
		}
	}

	private async void OnBackButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//MainPage");
	}
}