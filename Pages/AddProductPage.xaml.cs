using KseF.Models.Invoice_FA_2;
using CommunityToolkit.Mvvm.ComponentModel;
using KseF.Interfaces;
using KseF.Models;
using KseF.Models.ViewModels;
using KseF.Services;
using Microsoft.Maui;
using Models;

namespace KseF.Pages
{
    public partial class AddProductPage : ContentPage
    {
        private readonly ILocalDbService _dbService;
        private MyProductsViewModel _viewModel;
        public event EventHandler<Product> ProductAdded;

        private Product Product { get; set; }

        public AddProductPage(ILocalDbService dbService, MyProductsViewModel viewModel)
        {
            InitializeComponent();
            _dbService = dbService;
            _viewModel = viewModel;
            Product = new();
        }

        public AddProductPage(ILocalDbService dbService, MyProductsViewModel viewModel, Product product)
        {
            InitializeComponent();
            _dbService = dbService;
            _viewModel = viewModel;
            Product = product;

            ProductNameEntry.Text = product.Nazwa;
            ProductDescriptionEntry.Text = product.Opis;
            ProductPriceEntry.Text = product.Cena.ToString();
            ProductCategoryEntry.Text = product.Kategoria;
            ProductUnitPicker.SelectedIndex = (int)product.JednostkaMiary;
            ProductTaxRatePicker.SelectedIndex = (int)product.StawkaPodatku;
            ProductTypeOfPositionPicker.SelectedIndex = (int)product.RodzajPozycji;
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var check = await _dbService.GetItemAsyncById<Product>(Product.Id);
            if (check is null)
            {
                var newProduct = new Product
                {
                    Nazwa = ProductNameEntry.Text,
                    Opis = ProductDescriptionEntry.Text,
                    Cena = Decimal.Parse(ProductPriceEntry.Text),
                    Kategoria = ProductCategoryEntry.Text,
                    JednostkaMiary = (EnumLibrary.JednostkaMiary)ProductUnitPicker.SelectedIndex,
                    StawkaPodatku = (EnumLibrary.StawkiPodatkuPL)ProductTaxRatePicker.SelectedIndex,
                    RodzajPozycji = (EnumLibrary.RodzajPozycji)ProductTypeOfPositionPicker.SelectedIndex
                };

                try { await _dbService.SaveItemAsync<Product>(newProduct); ProductAdded?.Invoke(this, newProduct); }
                catch (Exception ex) { await DisplayAlert("Error", ex.Message, "OK"); }
                await DisplayAlert("Sukces", $"Dodano produkt {newProduct.Nazwa}", "OK");
            }
            else
            {
                Product.Nazwa = ProductNameEntry.Text;
                Product.Opis = ProductDescriptionEntry.Text;
                Product.Cena = Decimal.Parse(ProductPriceEntry.Text);
                Product.Kategoria = ProductCategoryEntry.Text;
                Product.JednostkaMiary = (EnumLibrary.JednostkaMiary)ProductUnitPicker.SelectedIndex;
                Product.StawkaPodatku = (EnumLibrary.StawkiPodatkuPL)ProductTaxRatePicker.SelectedIndex;
                Product.RodzajPozycji = (EnumLibrary.RodzajPozycji)ProductTypeOfPositionPicker.SelectedIndex;

                try { await _dbService.EditItemAsync<Product>(Product); }
                catch (Exception ex) { await DisplayAlert("Error", ex.Message, "OK"); }
                await DisplayAlert("Sukces", $"Edytowano produkt {Product.Nazwa}", "OK");
            }
            await Navigation.PopAsync();
        }
    }
}