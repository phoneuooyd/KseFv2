using Microsoft.Maui.Controls;
using KseF.Services;
using KseF.Models;
using KseF.Interfaces;
using KseF.Models.ViewModels;

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
		clientEntities = _dbService.GetItemsAsync<ClientEntities>().Result;
		BindingContext = _viewModel;
	}

	private async void OnAddClientEntityButtonClicked(object sender, EventArgs e)
	{
		var newClientPage = new AddClientEntityPage(_dbService, _viewModel); // Przeka¿ ViewModel do nowej strony
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
}