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

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
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

    private async void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
    {
        // Pobranie produktu z CommandParameter
        var swipeItem = sender as SwipeItem;
        var myBusinessEntitity = swipeItem?.CommandParameter as MyBusinessEntities;

        if (myBusinessEntitity != null)
        {
            // Wyœwietlenie okna dialogowego z potwierdzeniem
            bool isConfirmed = await DisplayAlert("Potwierdzenie",
                "Czy na pewno chcesz usun¹æ ten rekord?", "Tak", "Nie");

            // Jeœli u¿ytkownik potwierdzi, usuwamy produkt
            if (isConfirmed)
            {
                _viewModel.DeleteCommand.Execute(myBusinessEntitity);
            }
        }
    }
}