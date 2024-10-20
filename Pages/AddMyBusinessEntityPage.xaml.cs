using KseF.Models.Invoice_FA_2;
using CommunityToolkit.Mvvm.ComponentModel;
using KseF.Interfaces;
using KseF.Models;
using KseF.Models.ViewModels;
using KseF.Services;
using Microsoft.Maui;
using Models;
using CommunityToolkit.Mvvm.Messaging;

namespace KseF.Pages
{
	public partial class AddMyBusinessEntityPage : ContentPage
	{
		private readonly ILocalDbService _dbService;
		private MyBusinessEntitiesViewModel _viewModel;
		//private MainPageViewModel _mainPageViewModel;

		public event EventHandler<MyBusinessEntities> BusinessEntityAdded;

		public MyBusinessEntities BusinessEntity { get; set; }

		public AddMyBusinessEntityPage(ILocalDbService dbService, MyBusinessEntitiesViewModel viewModel)
		{
			InitializeComponent();
			_dbService = dbService;
			_viewModel = viewModel;

			BusinessEntity = new MyBusinessEntities();
            BusinessEntity.Id = new Guid();
		}

		public AddMyBusinessEntityPage(ILocalDbService dbService, MyBusinessEntitiesViewModel viewModel, MyBusinessEntities businessEntitity)
		{
			InitializeComponent();
			_dbService = dbService;
			_viewModel = viewModel;

			BusinessEntity = businessEntitity;

			NazwaSkroconaEntry.Text = businessEntitity.NazwaSkrocona;
			NazwaPelnaEntry.Text = businessEntitity.NazwaPelna;
			NipEntry.Text = businessEntitity.Nip;
			UlicaEntry.Text = businessEntitity.Ulica;
			NrDomuEntry.Text = businessEntitity.NrDomu;
			NrLokaluEntry.Text = businessEntitity.NrLokalu;
			KodPocztowyEntry.Text = businessEntitity.KodPocztowy;
			MiejscowoscEntry.Text = businessEntitity.Miejscowosc;
			AdresSiedzibyEntry.Text = businessEntitity.AdresSiedziby;
			AdresKorespondencyjnyEntry.Text = businessEntitity.AdresKorespondencyjny;
			NrRachunkuEntry.Text = businessEntitity.NrRachunku;
			NrTelefonuEntry.Text = businessEntitity.NrTelefonu;
			AdresEmailEntry.Text = businessEntitity.AdresEmail;
			NotatkiEditor.Text = businessEntitity.Notatki;
			RegonEntry.Text = businessEntitity.Regon;
			KrsEntry.Text = businessEntitity.Krs;
			BdoEntry.Text = businessEntitity.Bdo;
			//IsTPSwitch.IsToggled = businessEntitity.IsTP;
			IsDrukujStopkeSwitch.IsToggled = businessEntitity.IsDrukujStopke;
			KodUSEntry.Text = businessEntitity.KodUS;
			ImieOFEntry.Text = businessEntitity.ImieOsFiz;
			NazwiskoOFEntry.Text = businessEntitity.NazwiskoOsFiz;
			DataUrodzeniaOFPicker.Date = (DateTime)businessEntitity!.DataUrodzeniaOF!;
			FormaOpodatkowaniaPicker.SelectedIndex = (int)businessEntitity!.FormaOpodatkowania!;
		}

		private async void OnBackButtonClicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("//MyBusinessEntitiesPage");
		}

		private async void OnSaveButtonClicked(object sender, EventArgs e)
		{
            var check = await _dbService.GetItemAsyncById<MyBusinessEntities>(BusinessEntity.Id);
            if (check is null)
            {
                var newBusinessEntitiy = new MyBusinessEntities
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
                    Regon = RegonEntry.Text,
                    Krs = KrsEntry.Text,
                    Bdo = BdoEntry.Text,
                   //IsPodmiot = IsPodmiotSwitch.IsToggled,
                    //IsTP = IsTPSwitch.IsToggled,
                    IsDrukujStopke = IsDrukujStopkeSwitch.IsToggled,
                    KodUS = KodUSEntry.Text,
                    ImieOsFiz = ImieOFEntry.Text,
                    NazwiskoOsFiz = NazwiskoOFEntry.Text,
                    DataUrodzeniaOF = DataUrodzeniaOFPicker.Date,
                    FormaOpodatkowania = (EnumLibrary.FormaOpodatkowania)FormaOpodatkowaniaPicker.SelectedIndex,
                    StopkaFaktury = StopkaFakturyEditor.Text,
                    TokenKSeF = TokenKSeFEntry.Text
                };

                try { await _dbService.SaveItemAsync<MyBusinessEntities>(newBusinessEntitiy); BusinessEntityAdded?.Invoke(this, newBusinessEntitiy); }
                catch (Exception ex) { await DisplayAlert("Error", ex.Message, "OK"); }

                WeakReferenceMessenger.Default.Send(new MessageSender<MyBusinessEntities>(newBusinessEntitiy));
                await DisplayAlert("Sukces", $"Dodano firmê {newBusinessEntitiy.NazwaSkrocona}", "OK");
            }
            else
            {
                BusinessEntity.NazwaSkrocona = NazwaSkroconaEntry.Text;
                BusinessEntity.NazwaPelna = NazwaPelnaEntry.Text;
                BusinessEntity.Nip = NipEntry.Text;
                BusinessEntity.Ulica = UlicaEntry.Text;
                BusinessEntity.NrDomu = NrDomuEntry.Text;
                BusinessEntity.NrLokalu = NrLokaluEntry.Text;
                BusinessEntity.KodPocztowy = KodPocztowyEntry.Text;
                BusinessEntity.Miejscowosc = MiejscowoscEntry.Text;
                BusinessEntity.AdresSiedziby = AdresSiedzibyEntry.Text;
                BusinessEntity.AdresKorespondencyjny = AdresKorespondencyjnyEntry.Text;
                BusinessEntity.NrRachunku = NrRachunkuEntry.Text;
                BusinessEntity.NrTelefonu = NrTelefonuEntry.Text;
                BusinessEntity.AdresEmail = AdresEmailEntry.Text;
                BusinessEntity.Notatki = NotatkiEditor.Text;
                BusinessEntity.Regon = RegonEntry.Text;
                BusinessEntity.Krs = KrsEntry.Text;
                BusinessEntity.Bdo = BdoEntry.Text;
                //BusinessEntity.IsPodmiot = IsPodmiotSwitch.IsToggled;
                //BusinessEntity.IsTP = IsTPSwitch.IsToggled;
                BusinessEntity.IsDrukujStopke = IsDrukujStopkeSwitch.IsToggled;
                BusinessEntity.KodUS = KodUSEntry.Text;
                BusinessEntity.ImieOsFiz = ImieOFEntry.Text;
                BusinessEntity.NazwiskoOsFiz = NazwiskoOFEntry.Text;
                BusinessEntity.DataUrodzeniaOF = DataUrodzeniaOFPicker.Date;
                BusinessEntity.FormaOpodatkowania = (EnumLibrary.FormaOpodatkowania)FormaOpodatkowaniaPicker.SelectedIndex;
                BusinessEntity.StopkaFaktury = StopkaFakturyEditor.Text;
                BusinessEntity.TokenKSeF = TokenKSeFEntry.Text;

                try { await _dbService.EditItemAsync<MyBusinessEntities>(BusinessEntity); }
                catch (Exception ex) { await DisplayAlert("Error", ex.Message, "OK"); }
                WeakReferenceMessenger.Default.Send(new EntityUpdatedMessage<MyBusinessEntities>(BusinessEntity));

                await DisplayAlert($"Sukces", $"Edytowano firmê {BusinessEntity.NazwaSkrocona}", "OK");
            }
            
            await Navigation.PopAsync();
		}
	}
}
