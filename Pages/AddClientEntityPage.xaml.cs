using KseF.Models.Invoice_FA_2;
using CommunityToolkit.Mvvm.ComponentModel;
using KseF.Interfaces;
using KseF.Models;
using KseF.Models.ViewModels;
using KseF.Services;
using Microsoft.Maui;
using Models;
using System.Net;

namespace KseF.Pages
{
	public partial class AddClientEntityPage : ContentPage
	{
		private readonly ILocalDbService _dbService;
		private MyClientsViewModel _viewModel;
		public event EventHandler<ClientEntities> ClientAdded;

		public ClientEntities Client { get; set; }

		public AddClientEntityPage(ILocalDbService dbService, MyClientsViewModel viewModel)
		{
			InitializeComponent();
			_dbService = dbService;
			_viewModel = viewModel;

			Client = new();
		}

		public AddClientEntityPage(ILocalDbService dbService, MyClientsViewModel viewModel, ClientEntities client)
		{
			InitializeComponent();
			_dbService = dbService;
			_viewModel = viewModel;

			Client = client;

			NazwaSkroconaEntry.Text = client.NazwaSkrocona;
			NazwaPelnaEntry.Text = client.NazwaPelna;
			NipEntry.Text = client.Nip;
			UlicaEntry.Text = client.Ulica;
			NrDomuEntry.Text = client.NrDomu;
			NrLokaluEntry.Text = client.NrLokalu;
			KodPocztowyEntry.Text = client.KodPocztowy;
			MiejscowoscEntry.Text = client.Miejscowosc;
			AdresSiedzibyEntry.Text = client.AdresSiedziby;
			AdresKorespondencyjnyEntry.Text = client.AdresKorespondencyjny;
			NrRachunkuEntry.Text = client.NrRachunku;
			NrTelefonuEntry.Text = client.NrTelefonu;
			AdresEmailEntry.Text = client.AdresEmail;
			NotatkiEditor.Text = client.Notatki;
			NrKlientaEntry.Text = client.NrKlienta;
			ImieEntry.Text = client.Imie;
			NazwiskoEntry.Text = client.Nazwisko;
		}

		private async void OnBackButtonClicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("//MainPage");
		}

		private async void OnSaveButtonClicked(object sender, EventArgs e)
		{
			var check = await _dbService.GetItemAsyncById<ClientEntities>(Client.Id);
           
            if (check is null)
			{
                await DisplayAlert(Title, $"typ {check} dupa ", "OK");
                var newClient = new ClientEntities
				{
					NazwaSkrocona = NazwaSkroconaEntry.Text,
					NazwaPelna = NazwaPelnaEntry.Text,
					Nip = NipEntry.Text,
					Ulica = UlicaEntry.Text,
					NrDomu = NrDomuEntry.Text,
					NrLokalu = NrLokaluEntry.Text,
					KodPocztowy = KodPocztowyEntry.Text,
					Miejscowosc = MiejscowoscEntry.Text,
					AdresSiedziby = AdresSiedzibyEntry.Text,
					AdresKorespondencyjny = AdresKorespondencyjnyEntry.Text,
					NrRachunku = NrRachunkuEntry.Text,
					NrTelefonu = NrTelefonuEntry.Text,
					AdresEmail = AdresEmailEntry.Text,
					Notatki = NotatkiEditor.Text,
					NrKlienta = NrKlientaEntry.Text,
					Imie = ImieEntry.Text,
					Nazwisko = NazwiskoEntry.Text
				};

				try { await _dbService.SaveItemAsync<ClientEntities>(newClient); ClientAdded?.Invoke(this, newClient); }
				catch (Exception ex) { await DisplayAlert("Error", ex.Message, "OK"); }
				await DisplayAlert("Success", $"Dodano firmê {newClient.NazwaSkrocona}", "OK");
			}
			else
			{
                await DisplayAlert(Title, $"typ {check.GetType()} dupa ", "OK");
                Client.NazwaSkrocona = NazwaSkroconaEntry.Text;
				Client.NazwaPelna = NazwaPelnaEntry.Text;
				Client.Nip = NipEntry.Text;
				Client.Ulica = UlicaEntry.Text;
				Client.NrDomu = NrDomuEntry.Text;
				Client.NrLokalu = NrLokaluEntry.Text;
				Client.KodPocztowy = KodPocztowyEntry.Text;
				Client.Miejscowosc = MiejscowoscEntry.Text;
				Client.AdresSiedziby = AdresSiedzibyEntry.Text;
				Client.AdresKorespondencyjny = AdresKorespondencyjnyEntry.Text;
				Client.NrRachunku = NrRachunkuEntry.Text;
				Client.NrTelefonu = NrTelefonuEntry.Text;
				Client.AdresEmail = AdresEmailEntry.Text;
				Client.Notatki = NotatkiEditor.Text;
				Client.NrKlienta = NrKlientaEntry.Text;
				Client.Imie = ImieEntry.Text;
				Client.Nazwisko = NazwiskoEntry.Text;

				try { await _dbService.EditItemAsync<ClientEntities>(Client); }
				catch (Exception ex) { await DisplayAlert("Error", ex.Message, "OK"); }
				await DisplayAlert($"Sukces", $"Edytowano firmê {Client.NazwaSkrocona}", "OK");
			}
			await Navigation.PopAsync();
		}
	}
}
