using Microsoft.Maui.Controls;
using KseF.Services;
using KseF.Interfaces;
using KseF.Models;
using KseF.Models.ViewModels;
using System;
using System.Threading;

namespace KseF.Pages;

public partial class InvoicesSent : ContentPage
{
    private readonly ILocalDbService _dbService;
    private KsefApiService _apiService;
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

    private async void OnInvoiceTapped(object sender, ItemTappedEventArgs e)
    {
        var ksefToken = "";
        var invoice = e.Item as BaseFaktura;
        if (invoice != null)
        {
            bool isConfirmed = await DisplayAlert("Potwierdzenie",
                "Chcesz sprawdziæ status tej faktury?", "Tak", "Nie");

            if (isConfirmed)
            {
                if (invoice.StatusKSeF != null)
                {
                    await DisplayAlert("Status dla faktury", $"faktra o numerze {invoice.NrFakturyKSeF} posiada status {invoice.StatusKSeF}", "OK");
                }
                else
                {
                    try
                    {
                        ksefToken = _dbService.GetBusinessEntityFromContext().Result.TokenKSeF;
                        using var api = new KsefApiService();
                        var cancellationToken = new CancellationTokenSource();
                        await api.AuthenticateAsync(invoice.NipSprzedawcy, ksefToken);
                        (int status, string ksefReferenceNumber, DateTime acquisitionTimestamp) = await api.GetInvoiceStatusAsync(invoice.NumerReferencyjnyKSeF);

                       await DisplayAlert("Status dla faktury", $"faktra o numerze {invoice.NrFakturyKSeF} posiada status {status}", "OK");
                        await api.Terminate();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("B³¹d podczas wysy³ania faktury do KSeF", ex);
                    }
                }
            }
        }
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
