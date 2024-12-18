using Microsoft.Maui.Controls;
using KseF.Services;
using KseF.Models;
using KseF.Interfaces;
using KseF.Models.ViewModels;
using KseF.Models.Invoice_FA_2;
using System.Collections.ObjectModel;

namespace KseF.Pages;

public partial class MyClientsPage : ContentPage
{
	private readonly ILocalDbService _dbService;
	private List<ClientEntities> clientEntities;
	private MyClientsViewModel _viewModel;

    public MyClientsPage(ILocalDbService dbService)
	{
		InitializeComponent();
		_dbService = dbService;
		_viewModel = new MyClientsViewModel(_dbService);
        //clientEntities = _dbService.GetItemsAsync<ClientEntities>().Result;     
        //_viewModel.ClientEntities = new ObservableCollection<ClientEntities>(clientEntities);
        BindingContext = _viewModel;
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
    private async void OnAddClientEntityButtonClicked(object sender, EventArgs e)
	{
		var newClientPage = new AddClientEntityPage(_dbService, _viewModel); // Przeka� ViewModel do nowej strony
		newClientPage.ClientAdded += OnClientAdded; // Zarejestruj handler eventu
		await Navigation.PushAsync(newClientPage);
	}

    private void OnClientAdded(object sender, ClientEntities client)
    {
        _viewModel.ClientEntities.Add(client);
    }

	private async void OnClientTapped(object sender, ItemTappedEventArgs e)
	{
		var client = e.Item as ClientEntities;
		if (client != null)
		{
			var editProductPage = new AddClientEntityPage(_dbService, _viewModel, client);
			editProductPage.ClientAdded += OnClientAdded;
			await Navigation.PushAsync(editProductPage);
		}
	}

    private async void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var client = swipeItem?.CommandParameter as ClientEntities;

        if (client != null)
        {
            bool isConfirmed = await DisplayAlert("Potwierdzenie",
                "Czy na pewno chcesz usun�� ten rekord?", "Tak", "Nie");

            if (isConfirmed)
            {
                _viewModel.DeleteCommand.Execute(client);
            }
        }
    }
}