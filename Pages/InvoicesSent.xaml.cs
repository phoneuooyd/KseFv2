using Microsoft.Maui.Controls;
using KseF.Services;
using KseF.Interfaces;
using KseF.Models;
using KseF.Models.ViewModels;
using System;

namespace KseF.Pages;

public partial class InvoicesSent : ContentPage
{
    private readonly ILocalDbService _dbService;
    private List<BaseFaktura> invoices;
    private InvoicesSentViewModel _viewModel;

    public InvoicesSent(ILocalDbService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
        _viewModel = new InvoicesSentViewModel(_dbService);
        invoices = _dbService.GetItemsAsync<BaseFaktura>().Result;
        BindingContext = _viewModel;
    }
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private void OnInvoiceAdded(object sender, BaseFaktura invoice)
    {
       // _viewModel.Invoices.Add(invoice);
    }

    private async void OnInvoiceTapped(object sender, ItemTappedEventArgs e)
    {

    }

    private async void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var invoice = swipeItem?.CommandParameter as BaseFaktura;

        if (invoice != null)
        {
            bool isConfirmed = await DisplayAlert("Confirmation",
                "Are you sure you want to delete this record?", "Yes", "No");

            if (isConfirmed)
            {
                _viewModel.DeleteCommand.Execute(invoice);
            }
        }
    }
}
