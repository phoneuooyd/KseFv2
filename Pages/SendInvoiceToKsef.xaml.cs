using Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Xml.Serialization;
using static Models.EnumLibrary;
using static KseF.Services.XmlCreationService;
using static KseF.Services.DateHandlingService;
using static KseF.Services.DataLoadingService;
using static KseF.Models.BaseFaktura;
using KseF.Models.Invoice_FA_2;
using KseF.Services;
using KseF.Models;
using KseF.Interfaces;

namespace KseF
{
    public partial class SendInvoiceToKsef : ContentPage
    {
		private readonly ILocalDbService _dbService;
		MyBusinessEntities MyBusinessEntity { get; set; }
		public KodyWalut currencyCode { get; set; }
        public CountryCodes countryCode { get; set; }
		public BaseFaktura Invoice { get; set; }
        private static int rowCount = 1;
        private Dictionary<Guid, int> rowGuidMap = new Dictionary<Guid, int>();
        private Dictionary<Guid, List<View>> rowElementsMap = new Dictionary<Guid, List<View>>();

        public SendInvoiceToKsef()
        {
            InitializeComponent();
            AddInitialData();
            Invoice = new BaseFaktura();
            DateTime dataWystawienia = DateTime.Now;
            MyBusinessEntity = new();
            MyBusinessEntity = _dbService.GetBusinessEntityFromContext().Result;
			//TODO 
			//LoadEnumValues<CountryCodes>(krajSprzedawcaPicker, "Polska");
			//LoadEnumValues<CountryCodes>(krajNabywcaPicker, "Polska");
		}

		private async void OnBackButtonClicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("//MainPage");
		}

		private void AddInitialData()
        {
            //var krajSprzedawcaPicker = new Picker();
            //var krajNabywcaPicker = new Picker();

            if(MyBusinessEntity != null)
            {
				NipSprzedawcy.Text = MyBusinessEntity.Nip;
				NazwaSprzedawcy.Text = MyBusinessEntity.NazwaPelna;
				ulicaSprzedawcy.Text = MyBusinessEntity.Ulica;
				nrDomuSprzedawcy.Text = MyBusinessEntity.NrDomu;
				nrLokaluSprzedawcy.Text = MyBusinessEntity.NrLokalu;
				kodPocztowySprzedawcy.Text = MyBusinessEntity.KodPocztowy;
				miejscowoscSprzedawcy.Text = MyBusinessEntity.Miejscowosc;
				EmailSprzedawcy.Text = MyBusinessEntity.AdresEmail;
				TelefonSprzedawcy.Text = MyBusinessEntity.NrTelefonu;
			}



			var nameEntry = new Entry { Placeholder = "Nazwa towaru/usługi" };
            var quantityEntry = new Entry { Placeholder = "Ilość", Keyboard = Keyboard.Numeric };
            var priceEntry = new Entry { Placeholder = "Cena netto", Keyboard = Keyboard.Numeric };
            var vatPicker = new Picker();
			var grossPriceEntry = new Entry { Placeholder = "Cena brutto" };

            Guid rowGuid = Guid.NewGuid();

            transakcjaGrid.Children.Add(nameEntry);
            Grid.SetRow(nameEntry, 1);
            Grid.SetColumn(nameEntry, 0);

            transakcjaGrid.Children.Add(quantityEntry);
            Grid.SetRow(quantityEntry, 1);
            Grid.SetColumn(quantityEntry, 1);

            transakcjaGrid.Children.Add(priceEntry);
            Grid.SetRow(priceEntry, 1);
            Grid.SetColumn(priceEntry, 2);

            transakcjaGrid.Children.Add(vatPicker);
            Grid.SetRow(vatPicker, 1);
            Grid.SetColumn(vatPicker, 3);

            transakcjaGrid.Children.Add(grossPriceEntry);
            Grid.SetRow(grossPriceEntry, 1);
            Grid.SetColumn(grossPriceEntry, 4);

            rowGuidMap.Add(rowGuid, rowCount);
            rowElementsMap.Add(rowGuid, new List<View> { nameEntry, quantityEntry, priceEntry, vatPicker, grossPriceEntry });
            rowCount++;

            LoadEnumValues<KodyWalut>(currencyPicker, "Złoty polski");
            LoadEnumValues<StawkiPodatkuPL>(vatPicker, "23%");
		}

		void OnCurrencySelected(object sender, EventArgs e)
        {
            var selectedCurrencyIndex = currencyPicker.SelectedIndex;
            if (selectedCurrencyIndex >= 0)
            {
                currencyCode = (KodyWalut)selectedCurrencyIndex;
            }
        }

        private void OnAddButtonClicked(object sender, EventArgs e)
        {
            AddNewRow();
        }

        private void AddNewRow()
        {
            // Usunięcie przycisku "Usuń" z poprzedniego ostatniego wiersza (jeśli istnieje)
            if (rowCount > 0)
            {
                var lastRowGuid = rowGuidMap.FirstOrDefault(x => x.Value == rowCount - 1).Key;
                if (lastRowGuid != Guid.Empty)
                {
                    var lastRowElements = rowElementsMap[lastRowGuid];
                    var lastDeleteButton = lastRowElements.OfType<Button>().FirstOrDefault();
                    if (lastDeleteButton != null)
                    {
                        transakcjaGrid.Children.Remove(lastDeleteButton);
                        lastRowElements.Remove(lastDeleteButton);
                    }
                }
            }

            transakcjaGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            Guid rowGuid = Guid.NewGuid();

            var nameEntry = new Entry
            {
                Placeholder = "Nazwa towaru/usługi",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var quantityEntry = new Entry
            {
                Placeholder = "Ilość",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Keyboard = Keyboard.Numeric
            };

            var priceEntry = new Entry
            {
                Placeholder = "Cena netto",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Keyboard = Keyboard.Numeric
            };

            var vatPicker = new Picker
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var grossPriceEntry = new Entry
            {
                Placeholder = "Cena brutto",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var deleteButton = new Button
            {
                Text = "Usuń",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End
            };

            transakcjaGrid.Children.Add(nameEntry);
            Grid.SetRow(nameEntry, rowCount);
            Grid.SetColumn(nameEntry, 0);

            transakcjaGrid.Children.Add(quantityEntry);
            Grid.SetRow(quantityEntry, rowCount);
            Grid.SetColumn(quantityEntry, 1);

            transakcjaGrid.Children.Add(priceEntry);
            Grid.SetRow(priceEntry, rowCount);
            Grid.SetColumn(priceEntry, 2);

            transakcjaGrid.Children.Add(vatPicker);
            Grid.SetRow(vatPicker, rowCount);
            Grid.SetColumn(vatPicker, 3);

            transakcjaGrid.Children.Add(grossPriceEntry);
            Grid.SetRow(grossPriceEntry, rowCount);
            Grid.SetColumn(grossPriceEntry, 4);

            transakcjaGrid.Children.Add(deleteButton);
            Grid.SetRow(deleteButton, rowCount);
            Grid.SetColumn(deleteButton, 5);

            // Dodanie rowGuid do BindingContext przycisku usuwania
            deleteButton.BindingContext = rowGuid;

            // Zarejestrowanie elementów w mapach
            rowGuidMap.Add(rowGuid, rowCount);
            rowElementsMap.Add(rowGuid, new List<View> { nameEntry, quantityEntry, priceEntry, vatPicker, grossPriceEntry, deleteButton });
            rowCount++;

            LoadEnumValues<StawkiPodatkuPL>(vatPicker, "23%");
            deleteButton.Clicked += (s, e) =>
            {
                // Pobranie rzeczywistego rowGuid z BindingContext i przekazanie go do RemoveRow
                Guid rowGuidToDelete = (Guid)((Button)s).BindingContext;
                RemoveRow(rowGuidToDelete);
            };
        }

        public void RemoveRow(Guid rowGuid)
        {
            if (transakcjaGrid.Children.Count != 16)
            {
                if (!rowGuidMap.ContainsKey(rowGuid))
                {
                    throw new ArgumentOutOfRangeException(nameof(rowGuid), "Invalid GUID");
                }


                // Pobierz elementy wiersza do usunięcia
                var childrenToRemove = rowElementsMap[rowGuid];

                // Usuń wszystkie elementy z Grid
                foreach (var child in childrenToRemove)
                {
                    transakcjaGrid.Children.Remove(child);
                }

                // Usuń definicję wiersza
                int rowIndex = rowGuidMap[rowGuid];
                transakcjaGrid.RowDefinitions.RemoveAt(rowIndex);

                // Usuń GUID z map
                rowGuidMap.Remove(rowGuid);
                rowElementsMap.Remove(rowGuid);

                // Aktualizacja rowCount
                rowCount--;

                // Dodaj przycisk "Usuń" do nowego ostatniego wiersza (jeśli istnieje)
                if (rowCount > 0)
                {
                    var lastRowGuid = rowGuidMap.FirstOrDefault(x => x.Value == rowCount - 1).Key;
                    if (lastRowGuid != Guid.Empty)
                    {
                        var lastRowElements = rowElementsMap[lastRowGuid];
                        var lastDeleteButton = new Button
                        {
                            Text = "Usuń",
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.End,
                            BindingContext = lastRowGuid
                        };

                        lastDeleteButton.Clicked += (s, e) =>
                        {
                            Guid rowGuidToDelete = (Guid)((Button)s).BindingContext;
                            RemoveRow(rowGuidToDelete);
                        };

                        lastRowElements.Add(lastDeleteButton);
                        transakcjaGrid.Children.Add(lastDeleteButton);
                        Grid.SetRow(lastDeleteButton, rowCount - 1);
                        Grid.SetColumn(lastDeleteButton, 6);
                    }
                }
            } 
        }

        public List<PozycjeFaktury> GetPozycjeFaktury()
        {
            var pozycjeFakturyList = new List<PozycjeFaktury>();
            var nrPozycji = 1;

			foreach (var rowPair in rowElementsMap)
            {
                var rowGuid = rowPair.Key;
                var rowElements = rowPair.Value;

                var nameEntry = rowElements.OfType<Entry>().FirstOrDefault(e => Grid.GetColumn(e) == 0);
                var quantityEntry = rowElements.OfType<Entry>().FirstOrDefault(e => Grid.GetColumn(e) == 1);
                var priceEntry = rowElements.OfType<Entry>().FirstOrDefault(e => Grid.GetColumn(e) == 2);
                var vatPicker = rowElements.OfType<Picker>().FirstOrDefault(p => Grid.GetColumn(p) == 3);
                var grossPriceEntry = rowElements.OfType<Entry>().FirstOrDefault(e => Grid.GetColumn(e) == 4);

                if (nameEntry == null || quantityEntry == null || priceEntry == null ||  vatPicker == null || grossPriceEntry == null)
                    continue;

                var pozycja = new PozycjeFaktury
                {
                    NazwaTowaruUslugi = nameEntry.Text,
                    Opis = nameEntry.Text,
                    TowarId = nrPozycji + 10000,
					IloscTowaruUslugi = decimal.TryParse(quantityEntry.Text, out decimal quantity) ? quantity : 0,
                    CenaJednostkowaNetto = decimal.TryParse(priceEntry.Text, out decimal priceNetto) ? priceNetto : 0,
                    Vat = MapujPodatekVat((int)vatPicker.SelectedIndex),
                    CenaJednostkowaBrutto = decimal.TryParse(grossPriceEntry.Text, out decimal priceBrutto) ? priceBrutto : 0,
                    CenaJednostkowaVat = priceBrutto - priceNetto,
                    WartoscNetto = priceNetto * quantity,
                    WartoscVat = priceBrutto - priceNetto,
                    WartoscBrutto = priceBrutto * quantity,
                    NrPozycji = nrPozycji
                };

                pozycjeFakturyList.Add(pozycja);
				nrPozycji++;
			}

            return pozycjeFakturyList;
        }
        public List<Rata> GetRaty()
        {
            var ratyList = new List<Rata>();
            var raty = new Rata { DataZapłatyRaty = DateTime.Now.Date, KwotaRaty = 123, RataId = Guid.NewGuid(), FakturaId = Guid.NewGuid() };
            ratyList.Add(raty);
            return ratyList;
        }
        public async void OnCreateDocumentClicked(object sender, EventArgs e)
        {
            var selectedVatRate = stawkaVatPicker.SelectedIndex >= 0 ? stawkaVatPicker.Items[stawkaVatPicker.SelectedIndex] : null;
            MyBusinessEntities myCompany = new MyBusinessEntities();
            ClientEntities myClinet = new ClientEntities();

            myCompany.Nip = "9513128170";
			myClinet.Nip = "5278733163";

			Invoice.DataWystawienia = DateTime.Now.Date;
			//Sprzedawca
			Invoice.SprzedawcaAdresL1 = ulicaSprzedawcy.Text + " " + nrDomuSprzedawcy.Text + " " + nrLokaluSprzedawcy.Text;
			Invoice.SprzedawcaAdresL2 = kodPocztowySprzedawcy.Text + " " + miejscowoscSprzedawcy.Text;
			Invoice.UlicaSprzedawcy = ulicaSprzedawcy.Text;
			Invoice.NrDomuSprzedawcy = nrDomuSprzedawcy.Text;
			Invoice.NrLokaluSprzedawcy = nrLokaluSprzedawcy.Text;
			Invoice.KodPocztowySprzedawcy = kodPocztowySprzedawcy.Text;
			Invoice.MiejscowoscSprzedawcy = miejscowoscSprzedawcy.Text;
            Invoice.NipSprzedawcy = NipSprzedawcy.Text;
            Invoice.NazwaSprzedawcy = NazwaSprzedawcy.Text;
			Invoice.DaneSprzedawcy = Invoice.NipSprzedawcy + " " + Invoice.NazwaSprzedawcy + " " + Invoice.SprzedawcaAdresL1 + " " + Invoice.SprzedawcaAdresL2;
            Invoice.EmailSprzedawcy = EmailSprzedawcy.Text;
            Invoice.TelefonSprzedawcy = TelefonSprzedawcy.Text;


            //Nabywca
            Invoice.NipNabywcy = "5278733163";//NipNabywcy.Text;
			Invoice.NazwaNabywcy = NazwaNabywcy.Text;
			Invoice.NabywcaAdresL1 = ulicaNabywcy.Text + " " + nrDomuNabywcy.Text + " " + nrLokaluNabywcy.Text;
            Invoice.NabywcaAdresL2 = kodPocztowyNabywcy.Text + " " + miejscowoscNabywcy.Text;
            Invoice.UlicaNabywcy = ulicaNabywcy.Text;
            Invoice.NrDomuNabywcy = nrDomuNabywcy.Text;
            Invoice.NrLokaluNabywcy = nrLokaluNabywcy.Text;
            Invoice.KodPocztowyNabywcy = kodPocztowyNabywcy.Text;
            Invoice.MiejscowoscNabywcy = miejscowoscNabywcy.Text;
            Invoice.DaneNabywcy = Invoice.NipNabywcy + " " + Invoice.NazwaNabywcy + " " + Invoice.NabywcaAdresL1 + " " + Invoice.NabywcaAdresL2;
            Invoice.EmailNabywcy = EmailNabywcy.Text;
			Invoice.TelefonNabywcy = TelefonNabywcy.Text;

			//Dane FV
			Invoice.NrKlienta = "1";
			Invoice.KodWaluty = currencyCode.ToString();
			Invoice.PozycjeFaktury = GetPozycjeFaktury();
			Invoice.ZaplaconeRaty = GetRaty();
			Invoice.DoZapłatyPozostało = 0;
			Invoice.TypFaktury = EnumLibrary.TypFaktury.Sprzedaz;
            Invoice.FormaPlatnosci = "Przelew";
			Invoice.NumerFaktury = NrFaktury.Text;

			XmlCreationService xmlCreationService = new XmlCreationService();

            
			await xmlCreationService.CreateDocument_FA2(Invoice, myCompany);

			await DisplayAlert("Eksport XML", $"Plik XML został zapisany", "OK");
        }

        public PodatekVat MapujPodatekVat(int stawka)
        {
            int wysokoscPodatku;

            switch (stawka)
            {
                case 33:
                case 32:
                case 31:
                    wysokoscPodatku = 0;
                    break;
                default:
                    wysokoscPodatku = stawka;
                    break;
            }

            return new PodatekVat
            {
                NazwaStawki = stawka.ToString(),
                WysokośćPodatku = wysokoscPodatku
            };
        }

		/* TO DO LATER
	  private void OnCountrySelected(object sender, EventArgs e)
	  {
		  var selectedCountryIndex = krajSprzedawcaPicker.SelectedIndex;
		  if (selectedCountryIndex >= 0)
		  {
			  countryCode = (CountryCodes)selectedCountryIndex;
		  }

		  DisplayAlert("Kraj", $"Wybrano kraj: {countryCode}", "OK");
	  }

	  */

	}
}
