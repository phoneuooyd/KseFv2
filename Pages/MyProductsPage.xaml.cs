using Microsoft.Maui.Controls;
using KseF.Services;
using KseF.Interfaces;
using KseF.Models.Invoice_FA_2;
using KseF.Models.ViewModels;
using System;

namespace KseF.Pages;

public partial class MyProductsPage : ContentPage
{
	private readonly ILocalDbService _dbService;
	private List<Product> products;
	private MyProductsViewModel _viewModel;

	public MyProductsPage(ILocalDbService dbService)
	{
		InitializeComponent();
		_dbService = dbService;
		_viewModel = new MyProductsViewModel(_dbService);
		products = _dbService.GetItemsAsync<Product>().Result;
        BindingContext = _viewModel;
	}

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void OnAddProductButtonClicked(object sender, EventArgs e)
	{
		var newProductPage = new AddProductPage(_dbService, _viewModel); 
		newProductPage.ProductAdded += OnProductAdded; 
		await Navigation.PushAsync(newProductPage);
	}

	private void OnProductAdded(object sender, Product product)
	{
		_viewModel.Products.Add(product);
	}

	private async void OnProductTapped(object sender, ItemTappedEventArgs e)
	{
		var product = e.Item as Product;
		if (product != null)
		{
			var editProductPage = new AddProductPage(_dbService, _viewModel, product);
			editProductPage.ProductAdded += OnProductAdded;
			await Navigation.PushAsync(editProductPage);
		}
	}

    private async void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var product = swipeItem?.CommandParameter as Product;

        if (product != null)
        {
            bool isConfirmed = await DisplayAlert("Potwierdzenie",
                "Czy na pewno chcesz usun¹æ ten rekord?", "Tak", "Nie");

            if (isConfirmed)
            {
                _viewModel.DeleteCommand.Execute(product);
            }
        }
    }
}