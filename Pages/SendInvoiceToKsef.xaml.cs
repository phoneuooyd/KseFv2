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
using KseF.Models.ViewModels;
using KseF.Controls;
using System.Globalization;

namespace KseF
{
    public partial class SendInvoiceToKsef : ContentPage
    {
		private readonly ILocalDbService _dbService;
        private Dictionary<Guid, int> rowGuidMap = new Dictionary<Guid, int>();
        private Dictionary<Guid, List<View>> rowElementsMap = new Dictionary<Guid, List<View>>();
        
        MyBusinessEntities MyBusinessEntity { get; set; }
        public KodyWalut currencyCode { get; set; }
        public BaseFaktura Invoice { get; set; }
        public List<ClientEntities> Clients { get; set; }
        public List<Product> Products { get; set; }
        public ClientEntities SelectedClient { get; set; }
        public Product SelectedProduct { get; set; }

        private SendInvioceViewModel _viewModel;

        private static int rowCount = 1;

        public SendInvoiceToKsef(ILocalDbService dbService)
        {
            InitializeComponent();
            
            _dbService = dbService;
            _viewModel = new SendInvioceViewModel(_dbService);
            Invoice = new BaseFaktura();
            MyBusinessEntity =  _dbService.GetBusinessEntityFromContext().Result;
            Clients = _dbService.GetItemsAsync<ClientEntities>().Result;
            Products = _dbService.GetItemsAsync<Product>().Result;
            DateTime dataWystawienia = DateTime.Now;

            if (Clients != null)
            {
                SelectedClient = Clients[0];
            }
            else
            {
                SelectedClient = new ClientEntities();
            }
            AddInitialData();
            BindingContext = _viewModel;
            ClientPicker.SelectedIndex = 0;
            ProductPicker.ItemsSource = Products;
        }

        private async void SetFvNumber()
        {
            var invoicesSent = await _dbService.GetItemsAsync<BaseFaktura>();
            var currentYear = DateTime.Now.Year.ToString();
            var invoicesSentThisYear = invoicesSent.Where(i => i.DataWystawienia.Year == DateTime.Now.Year).ToList().Count();

            NrFaktury.Text = $"FV/{currentYear}/{invoicesSentThisYear + 1}";
        }

		private async void OnBackButtonClicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("//MainPage");
		}	

        private async void OnDataWystawieniaChanged(object sender, DateChangedEventArgs e)
        {
            SetFvNumber();
        }

        private void OnAddButtonClicked(object sender, EventArgs e)
        {
            AddNewRow();
        }

        private void OnClientChanged(object sender, EventArgs e)
        {
            SelectedClient = ClientPicker.SelectedItem as ClientEntities;
            UpdateCLientRows();
        }
        private void OnTapped(object sender, EventArgs e)
        {
            Guid rowGuidToDelete = (Guid)((KseFSmallButton)sender).BindingContext;
            RemoveRow(rowGuidToDelete);
        }

        private void OnVatRateChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            if (picker == null || picker.SelectedItem == null)
                return;

            int rowIndex = -1;
            for (int i = 0; i < transakcjaGrid.Children.Count; i++)
            {
                var child = transakcjaGrid.Children[i];
                if (child == picker)
                {
                    rowIndex = transakcjaGrid.GetRow(child);
                    break;
                }
            }

            if (rowIndex == -1)
                return;

            var priceEntry = transakcjaGrid.Children.FirstOrDefault(c => transakcjaGrid.GetRow(c) == rowIndex && transakcjaGrid.GetColumn(c) == 2) as Entry;
            var grossPriceEntry = transakcjaGrid.Children.FirstOrDefault(c => transakcjaGrid.GetRow(c) == rowIndex && transakcjaGrid.GetColumn(c) == 4) as Entry;

            if (priceEntry == null || grossPriceEntry == null || string.IsNullOrWhiteSpace(priceEntry.Text))
                return;

            if (!decimal.TryParse(priceEntry.Text, out decimal cenaNetto))
                return;

            var selectedVatRate = DataLoadingService.GetEnumValueFromPicker<StawkiPodatkuPL>(picker);
            if (!selectedVatRate.HasValue)
                return;

            decimal stawkaVAT = DataLoadingService.VatRateToDecimal(selectedVatRate.Value);
            decimal cenaBrutto = cenaNetto * (1 + stawkaVAT);
            grossPriceEntry.Text = cenaBrutto.ToString("F2");
        }

        private async void OnProductChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            if (picker == null || picker.SelectedItem == null)
                return;

            var selectedProduct = picker.SelectedItem as Product;
            if (selectedProduct == null)
                return;

            int rowIndex = -1;
            for (int i = 0; i < transakcjaGrid.Children.Count; i++)
            {
                var child = transakcjaGrid.Children[i];
                if (child == picker)
                {
                    rowIndex = transakcjaGrid.GetRow(child);
                    break;
                }
            }

            if (rowIndex == -1)
                return;

            var quantityEntry = transakcjaGrid.Children.FirstOrDefault(c => transakcjaGrid.GetRow(c)== rowIndex && transakcjaGrid.GetColumn(c) == 1) as KseFNumericUpDown; 
            var priceEntry = transakcjaGrid.Children.FirstOrDefault(c => transakcjaGrid.GetRow(c) == rowIndex && transakcjaGrid.GetColumn(c) == 2) as Entry;
            var vatPicker = transakcjaGrid.Children.FirstOrDefault(c => transakcjaGrid.GetRow(c) == rowIndex && transakcjaGrid.GetColumn(c) == 3) as Picker;
            var grossPriceEntry = transakcjaGrid.Children.FirstOrDefault(c => transakcjaGrid.GetRow(c) == rowIndex && transakcjaGrid.GetColumn(c) == 4) as Entry;

            if (priceEntry != null)
            {
                priceEntry.Text = selectedProduct.Cena.ToString("F2"); 
            }

            if (quantityEntry != null)
            {
                quantityEntry.Value = 1;
            }

            if (vatPicker != null)
            {
                LoadEnumValues<StawkiPodatkuPL>(vatPicker);   
                //vatPicker.SelectedItem = selectedProduct.StawkaPodatku;

                var enumDisplayNameMap = vatPicker.BindingContext as Dictionary<string, StawkiPodatkuPL>;
                if (enumDisplayNameMap != null)
                {
                    var displayName = enumDisplayNameMap.FirstOrDefault(x => x.Value == selectedProduct.StawkaPodatku).Key;
                    if (!string.IsNullOrEmpty(displayName))
                    {
                        vatPicker.SelectedItem = displayName;
                    }
                }
            }

            if (grossPriceEntry != null && selectedProduct.StawkaPodatku != null)
            {
                var selectedVatRate = DataLoadingService.GetEnumValueFromPicker<StawkiPodatkuPL>(vatPicker!);
                if (selectedVatRate.HasValue)
                {
                    decimal stawkaVAT = DataLoadingService.VatRateToDecimal(selectedVatRate.Value);
                    decimal cenaBrutto = selectedProduct.Cena * (1 + stawkaVAT);
                    grossPriceEntry.Text = cenaBrutto.ToString("F2");
                }
            }
            await DisplayAlert("Produkt", $"Wybrano produkt: {selectedProduct.Nazwa}", "OK");
        }

        private void UpdateCLientRows()
        {
            if (MyBusinessEntity != null)
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

            if (Clients != null)
            {
                NipNabywcy.Text = SelectedClient.Nip;
                NazwaNabywcy.Text = SelectedClient.NazwaPelna;
                ulicaNabywcy.Text = SelectedClient.Ulica;
                nrDomuNabywcy.Text = SelectedClient.NrDomu;
                nrLokaluNabywcy.Text = SelectedClient.NrLokalu;
                kodPocztowyNabywcy.Text = SelectedClient.KodPocztowy;
                miejscowoscNabywcy.Text = SelectedClient.Miejscowosc;
                EmailNabywcy.Text = SelectedClient.AdresEmail;
                TelefonNabywcy.Text = SelectedClient.NrTelefonu;
            }
        }
        private void AddInitialData()
        {
            UpdateCLientRows();

            var nameEntry = ProductPicker;
            var quantityEntry = IloscTowaruUslugi;
            var priceEntry = CenaJednostkowaNetto;
            var vatPicker = stawkaVatPicker;
            var grossPriceEntry = CenaJednostkowaBrutto;

            Guid rowGuid = Guid.NewGuid();

            rowGuidMap.Add(rowGuid, rowCount);
            rowElementsMap.Add(rowGuid, new List<View> { nameEntry, quantityEntry, priceEntry, vatPicker, grossPriceEntry });

            rowCount++;

            //LoadEnumValues<KodyWalut>(currencyPicker, "Złoty polski");
            LoadEnumValues<StawkiPodatkuPL>(vatPicker);
            nameEntry.ItemsSource = Products;
            nameEntry.ItemDisplayBinding = new Binding("Nazwa");
            nameEntry.SelectedIndexChanged += OnProductChanged!;
            vatPicker.SelectedIndexChanged += OnVatRateChanged!;
            SetFvNumber();
        }

        private void AddNewRow()
        {
            // Usunięcie przycisku "Usuń" z poprzedniego ostatniego wiersza (jeśli istnieje)
            if (rowCount > 1)
            {
                var lastRowGuid = rowGuidMap.FirstOrDefault(x => x.Value == rowCount - 1).Key;
                if (lastRowGuid != Guid.Empty)
                {
                    var lastRowElements = rowElementsMap[lastRowGuid];
                    var lastDeleteButton = lastRowElements.OfType<KseFSmallButton>().FirstOrDefault();
                    if (lastDeleteButton != null)
                    {
                        transakcjaGrid.Children.Remove(lastDeleteButton);
                        lastRowElements.Remove(lastDeleteButton);
                    }
                }
            }

            transakcjaGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            Guid rowGuid = Guid.NewGuid();

            var nameEntry = new Picker
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var quantityEntry = new KseFNumericUpDown
            {
              
            };

            var priceEntry = new Entry
            {
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
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var deleteButton = new KseFSmallButton("----")
            {
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

            LoadEnumValues<StawkiPodatkuPL>(vatPicker);
            nameEntry.ItemsSource = Products;
            nameEntry.ItemDisplayBinding = new Binding("Nazwa");
            nameEntry.SelectedIndexChanged += OnProductChanged!;
            deleteButton.Clicked += (s, e) => OnTapped(s,e);
            vatPicker.SelectedIndexChanged += OnVatRateChanged!;
        }

        public void RemoveRow(Guid rowGuid)
        {
            if (transakcjaGrid.Children.Count >= 13)
            {
                if (!rowGuidMap.ContainsKey(rowGuid))
                {
                    throw new ArgumentOutOfRangeException(nameof(rowGuid), "Invalid GUID");
                }

                // Pobierz elementy wiersza do usunięcia
                var childrenToRemove = rowElementsMap[rowGuid];

                foreach (var child in childrenToRemove)
                {
                    transakcjaGrid.Children.Remove(child);
                }

                int rowIndex = rowGuidMap[rowGuid];
                transakcjaGrid.RowDefinitions.RemoveAt(rowIndex);

                rowGuidMap.Remove(rowGuid);
                rowElementsMap.Remove(rowGuid);
                rowCount--;

                // Dodaj przycisk "Usuń" do nowego ostatniego wiersza (jeśli istnieje)
                if (transakcjaGrid.Children.Count >= 13) //Ilosc elementow w gridzie
                {
                    var lastRowGuid = rowGuidMap.FirstOrDefault(x => x.Value == rowCount - 1).Key;
                    if (lastRowGuid != Guid.Empty)
                    {
                        var lastRowElements = rowElementsMap[lastRowGuid];
                        var lastDeleteButton = new KseFSmallButton("----")
                        {
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.End,
                            BindingContext = lastRowGuid
                        };

                        lastDeleteButton.Clicked += (s, e) =>
                        {
                            Guid rowGuidToDelete = (Guid)((KseFSmallButton)s!).BindingContext;
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

                var nameEntry = rowElements.OfType<Picker>().FirstOrDefault(e => Grid.GetColumn(e) == 0)!;
                var quantityEntry = rowElements.OfType<KseFNumericUpDown>().FirstOrDefault(e => Grid.GetColumn(e) == 1);
                var priceEntry = rowElements.OfType<Entry>().FirstOrDefault(e => Grid.GetColumn(e) == 2);
                var vatPicker = rowElements.OfType<Picker>().FirstOrDefault(p => Grid.GetColumn(p) == 3);
                var grossPriceEntry = rowElements.OfType<Entry>().FirstOrDefault(e => Grid.GetColumn(e) == 4);

                if (nameEntry == null || quantityEntry == null || priceEntry == null ||  vatPicker == null || grossPriceEntry == null)
                    continue;

                var pozycjaItem = nameEntry.SelectedItem as Product;
                var culture = CultureInfo.GetCultureInfo("en-US"); // Kultura, która używa kropki jako separatora dziesiętnego


                var pozycja = new PozycjeFaktury
                {
                    NazwaTowaruUslugi = pozycjaItem.Nazwa,
                    Opis = pozycjaItem.Opis!,
                    TowarId = nrPozycji + 10000,
					IloscTowaruUslugi = (decimal)quantityEntry.Value,
                    CenaJednostkowaNetto = decimal.TryParse(priceEntry.Text, NumberStyles.Any, culture, out decimal priceNetto) ? priceNetto : 0,
                    Vat = MapujPodatekVat(vatPicker.SelectedItem),
                    CenaJednostkowaBrutto = decimal.TryParse(grossPriceEntry.Text, out decimal priceBrutto) ? priceBrutto : 0,
                    CenaJednostkowaVat = priceBrutto - priceNetto,
                    WartoscNetto = priceNetto * (decimal)quantityEntry.Value,
                    WartoscVat = priceBrutto - priceNetto,
                    WartoscBrutto = priceBrutto * (decimal)quantityEntry.Value,
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
            try
            {
                exportButton.IsEnabled = false;

                var selectedVatRate = stawkaVatPicker.SelectedIndex >= 0 ? stawkaVatPicker.Items[stawkaVatPicker.SelectedIndex] : null;
                MyBusinessEntities myCompany = new MyBusinessEntities();
                ClientEntities myClient = new ClientEntities();

                myCompany.Nip = "9513128170";
                myClient.Nip = "5278733163";

                Invoice.DataWystawienia = DateTime.Now.Date;

                // Sprzedawca
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

                // Nabywca
                Invoice.NipNabywcy = "5278733163";
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

                // Dane FV
                Invoice.NrKlienta = "1";
                Invoice.KodWaluty = "PLN";
                Invoice.PozycjeFaktury = GetPozycjeFaktury();
                Invoice.ZaplaconeRaty = GetRaty();
                Invoice.DoZapłatyPozostało = 0;
                Invoice.TypFaktury = EnumLibrary.TypFaktury.Sprzedaz;
                Invoice.FormaPlatnosci = "Przelew";
                Invoice.NumerFaktury = NrFaktury.Text;

                XmlCreationService xmlCreationService = new XmlCreationService();

                BaseFaktura FakturaZapis = await xmlCreationService.CreateDocument_FA2(Invoice, myCompany);

                //save FakturaZapis to db
                await _dbService.SaveItemAsync(FakturaZapis);
                await DisplayAlert("Eksport XML", "Plik XML został zapisany", "OK");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", $"Wystąpił błąd podczas eksportu: {ex.Message}", "OK");
            }
            finally
            {
                exportButton.IsEnabled = true;
            }
        }
        public PodatekVat MapujPodatekVat(object stawka)
        {
            int wysokoscPodatku;
            string stawkaString = stawka.ToString()!;

            if (stawkaString.Contains("%"))
            {
                stawkaString = stawkaString.Substring(0, stawkaString.Length - 1);
            }
            if (stawkaString.Contains("zw"))
            {
                stawkaString = "31";
            }
            if (stawkaString.Contains("np"))
            {
                stawkaString = "32";
            }
            if (stawkaString.Contains("oo"))
            {
                stawkaString = "33";
            }

            switch (stawkaString)
            {
                case "33":
                    wysokoscPodatku = 0;
                    break;
                case "32":
                    wysokoscPodatku = 0;
                    break;
                case "31":
                    wysokoscPodatku = 0;
                    break;
                default:
                    if (!int.TryParse(stawkaString, out wysokoscPodatku))
                    {
                        wysokoscPodatku = 0;
                    }
                    break;
            }

            return new PodatekVat
            {
                NazwaStawki = stawka.ToString()!,
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

        
        void OnCurrencySelected(object sender, EventArgs e)
        {
            var selectedCurrencyIndex = currencyPicker.SelectedIndex;
            if (selectedCurrencyIndex >= 0)
            {
                currencyCode = (KodyWalut)selectedCurrencyIndex;
            }
        }*/
    }
}
