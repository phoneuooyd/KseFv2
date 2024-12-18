﻿using KseF.Interfaces;
using KseF.Models;
using KseF.Models.Invoice_FA_2;
using Models;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KseF.Services
{
	public class XmlCreationService
	{
		private MyBusinessEntities entities;
        private readonly ILocalDbService _dbService;
        public XmlCreationService(ILocalDbService dbService) 
		{
            _dbService = dbService;
            var MBE = _dbService.GetBusinessEntityFromContext().Result;
			entities = MBE;
        }
		public async Task<BaseFaktura> CreateDocument_FA2(BaseFaktura Faktura)
		{
			//Copilot look at Models.Invoice_FA_2.Faktura class to assign properties
			Models.Invoice_FA_2.Faktura FakturaKsef = new Models.Invoice_FA_2.Faktura();
			FakturaKsef.Naglowek = new TNaglowek();
			FakturaKsef.Naglowek.KodFormularza = new TNaglowekKodFormularza();
			FakturaKsef.Naglowek.WariantFormularza = 2;
			FakturaKsef.Naglowek.DataWytworzeniaFa = DateTime.Now;
			FakturaKsef.Naglowek.SystemInfo = "Praca inżynierska Przemysława Przybyszewskiego";
			FakturaKsef.Podmiot1 = new FakturaPodmiot1();
			FakturaKsef.Podmiot1.DaneIdentyfikacyjne = new TPodmiot1();
			FakturaKsef.Podmiot1.DaneIdentyfikacyjne.NIP = "9513128170";//Faktura.NipSprzedawcy;
			FakturaKsef.Podmiot1.DaneIdentyfikacyjne.Nazwa = Faktura.NazwaSprzedawcy;
			FakturaKsef.Podmiot1.Adres = new TAdres();
			FakturaKsef.Podmiot1.Adres.KodKraju = TKodKraju.PL;
			FakturaKsef.Podmiot1.Adres.AdresL1 = Faktura.SprzedawcaAdresL1;
			FakturaKsef.Podmiot1.Adres.AdresL2 = Faktura.SprzedawcaAdresL2;
			FakturaKsef.Podmiot1.AdresKoresp = new FakturaPodmiot1AdresKoresp();
			FakturaKsef.Podmiot1.AdresKoresp.KodKraju = TKodKraju.PL;
			FakturaKsef.Podmiot1.AdresKoresp.AdresL1 = Faktura.SprzedawcaAdresL1;
			FakturaKsef.Podmiot1.AdresKoresp.AdresL2 = Faktura.SprzedawcaAdresL2;
			FakturaKsef.Podmiot1.DaneKontaktowe = new FakturaPodmiot1DaneKontaktowe[1];
			FakturaKsef.Podmiot1.DaneKontaktowe[0] = new FakturaPodmiot1DaneKontaktowe();
			FakturaKsef.Podmiot1.DaneKontaktowe[0].Email = Faktura.EmailSprzedawcy;
			FakturaKsef.Podmiot1.DaneKontaktowe[0].Telefon = Faktura.TelefonSprzedawcy;
			FakturaKsef.Podmiot2 = new FakturaPodmiot2();
			FakturaKsef.Podmiot2.DaneIdentyfikacyjne = new TPodmiot2();
			if (String.IsNullOrEmpty(Faktura.NipNabywcy))
			{
				FakturaKsef.Podmiot2.DaneIdentyfikacyjne.Items = new[] { (object)(sbyte) 0 };
				FakturaKsef.Podmiot2.DaneIdentyfikacyjne.ItemsElementName = new[] { ItemsChoiceType.BrakID };
			}
			else
			{
				FakturaKsef.Podmiot2.DaneIdentyfikacyjne.Items = new[] { Faktura.NipNabywcy };
				FakturaKsef.Podmiot2.DaneIdentyfikacyjne.ItemsElementName = new[] { ItemsChoiceType.NIP };
			}
			FakturaKsef.Podmiot2.DaneIdentyfikacyjne.Nazwa = Faktura.NazwaNabywcy;
			FakturaKsef.Podmiot2.Adres = new TAdres();
			FakturaKsef.Podmiot2.Adres.KodKraju = TKodKraju.PL;
			FakturaKsef.Podmiot2.Adres.AdresL1 = Faktura.NabywcaAdresL1;
			FakturaKsef.Podmiot2.Adres.AdresL2 = Faktura.NabywcaAdresL2;
			if(!String.IsNullOrEmpty(FakturaKsef.Podmiot2.IDNabywcy))
			{
				FakturaKsef.Podmiot2.IDNabywcy = Faktura.IdNabywcy!.ToString();
			}
			FakturaKsef.Fa = new FakturaFA();
			FakturaKsef.Fa.KodWaluty = Enum.Parse<TKodWaluty>(Faktura.KodWaluty!);
			FakturaKsef.Fa.P_1 = Faktura.DataWystawienia;
			FakturaKsef.Fa.P_2 = Faktura.NumerFaktury;
			FakturaKsef.Fa.Item = Faktura.DataSprzedazy; // P_6
			FakturaKsef.Fa.P_15 = Faktura.CenaBruttoRazem;
			FakturaKsef.Fa.Adnotacje = new FakturaFAAdnotacje();
			FakturaKsef.Fa.Adnotacje.P_16 = 2;
			FakturaKsef.Fa.Adnotacje.P_17 = 2;
			FakturaKsef.Fa.Adnotacje.P_18 = 2;
			FakturaKsef.Fa.Adnotacje.P_18A = (Faktura.FormaPlatnosci ?? "").Contains("podziel", StringComparison.CurrentCultureIgnoreCase) ? (sbyte)1 : (sbyte)2;
			FakturaKsef.Fa.Adnotacje.Zwolnienie = new FakturaFAAdnotacjeZwolnienie();
			FakturaKsef.Fa.Adnotacje.Zwolnienie.Items = [(object)(sbyte)1];
			FakturaKsef.Fa.Adnotacje.Zwolnienie.ItemsElementName = [ItemsChoiceType2.P_19N];
			FakturaKsef.Fa.Adnotacje.NoweSrodkiTransportu = new FakturaFAAdnotacjeNoweSrodkiTransportu();
			FakturaKsef.Fa.Adnotacje.NoweSrodkiTransportu.Items = [(object)(sbyte)1];
			FakturaKsef.Fa.Adnotacje.NoweSrodkiTransportu.ItemsElementName = [ItemsChoiceType4.P_22N];
			FakturaKsef.Fa.Adnotacje.P_23 = 2;
			FakturaKsef.Fa.Adnotacje.PMarzy = new FakturaFAAdnotacjePMarzy();
			FakturaKsef.Fa.Adnotacje.PMarzy.Items = [(sbyte)1];
			FakturaKsef.Fa.Adnotacje.PMarzy.ItemsElementName = [ItemsChoiceType5.P_PMarzyN];
			FakturaKsef.Fa.RodzajFaktury = Faktura.TypFaktury == EnumLibrary.TypFaktury.Sprzedaz ? TRodzajFaktury.VAT : Faktura.TypFaktury == EnumLibrary.TypFaktury.SprzedazKor ? TRodzajFaktury.KOR : throw new ApplicationException("Nieobsługiwany rodzaj faktury: " + Faktura.TypFaktury);
			if (Faktura.IsTP)
			{
				FakturaKsef.Fa.TP = 1; 
				FakturaKsef.Fa.TPSpecified = true;
			}

			if (!String.IsNullOrEmpty(Faktura.Notatki)) FakturaKsef.Fa.DodatkowyOpis = new[] 
			{ 
				new TKluczWartosc() { Klucz = "Uwagi", Wartosc = Faktura.Notatki } 
			};
			
			FakturaKsef.Fa.Platnosc = new FakturaFAPlatnosc();
			if (Faktura.DoZapłatyPozostało == 0)
			{
				FakturaKsef.Fa.Platnosc.Items = new[] { (object)(sbyte)1, Faktura.ZaplaconeRaty.Last().DataZapłatyRaty };
				FakturaKsef.Fa.Platnosc.ItemsElementName = new[] { ItemsChoiceType8.Zaplacono, ItemsChoiceType8.DataZaplaty };
			}
			else if (Faktura.DoZapłatyPozostało < Faktura.CenaBruttoRazem)
			{
				FakturaKsef.Fa.Platnosc.Items = new[] { (object)(sbyte)1, Faktura.ZaplaconeRaty.Select(e => new FakturaFAPlatnoscZaplataCzesciowa { KwotaZaplatyCzesciowej = e.KwotaRaty, DataZaplatyCzesciowej = e.DataZapłatyRaty }) };
				FakturaKsef.Fa.Platnosc.ItemsElementName = Enumerable.Repeat(ItemsChoiceType8.ZnacznikZaplatyCzesciowej, 1).Concat(Enumerable.Repeat(ItemsChoiceType8.ZaplataCzesciowa, Faktura.ZaplaconeRaty.Count)).ToArray();
			}
			FakturaKsef.Fa.Platnosc.TerminPlatnosci = new[] { new FakturaFAPlatnoscTerminPlatnosci() };
			FakturaKsef.Fa.Platnosc.TerminPlatnosci[0].Termin = Faktura.TerminPlatnosci;
			FakturaKsef.Fa.Platnosc.TerminPlatnosci[0].TerminOpis = Faktura.FormaPlatnosci;
			FakturaKsef.Fa.Platnosc.Items1 = new[] { (object)TFormaPlatnosci.Item6 };
			if (!String.IsNullOrEmpty(Faktura.NrKontaBankowego))
			{
				FakturaKsef.Fa.Platnosc.RachunekBankowy = new[] { new TRachunekBankowy() };
				FakturaKsef.Fa.Platnosc.RachunekBankowy[0].NrRB = Faktura.NrKontaBankowego.Replace(" ", "");
			}
			if (Faktura.KorektaDoFaktury != null)
			{
                //Korekty nie będą obsługiwane
				throw new ApplicationException("Korekty nie są obsługiwane");
            }

            var faWiersze = new List<FakturaFAFaWiersz>();
			foreach (var pozycja in Faktura.PozycjeFaktury)
			{
				var fa2Wiersz = new FakturaFAFaWiersz();
				fa2Wiersz.NrWierszaFa = pozycja.NrPozycji.ToString();
				fa2Wiersz.UU_ID = pozycja.TowarId.ToString();
				fa2Wiersz.P_7 = pozycja.Opis;
				fa2Wiersz.Indeks = pozycja.NazwaTowaruUslugi;
				fa2Wiersz.P_8A = pozycja.JednostkaMiary.ToString();
				fa2Wiersz.P_8B = Math.Abs(pozycja.IloscTowaruUslugi);
				fa2Wiersz.P_8BSpecified = true;
                fa2Wiersz.P_9B = pozycja.CenaJednostkowaBrutto;
                fa2Wiersz.P_9BSpecified = true;
                fa2Wiersz.P_11A = Math.Abs(pozycja.WartoscBrutto);
                fa2Wiersz.P_11ASpecified = true;
                fa2Wiersz.P_11Vat = Math.Abs(pozycja.WartoscVat);
                fa2Wiersz.P_11VatSpecified = true;

                if (pozycja.GTU > 0)
				{
					fa2Wiersz.GTU = Enum.Parse<TGTU>("GTU_" + pozycja.GTU.ToString("00"));
					fa2Wiersz.GTUSpecified = true;
				}

				if (Faktura.IsWewnątrzwspólnotowaDostawaTowarow)
				{
					FakturaKsef.Fa.P_13_6_2 += pozycja.WartoscNetto;
					FakturaKsef.Fa.P_13_6_2Specified = true;
					fa2Wiersz.P_12 = TStawkaPodatku.np;
				}
				else if (pozycja.Vat.NazwaStawki == "np")
				{
					FakturaKsef.Fa.P_13_6_1 += pozycja.WartoscNetto;
					FakturaKsef.Fa.P_13_6_1Specified = true;
					fa2Wiersz.P_12 = TStawkaPodatku.np;
				}
				else if (pozycja.Vat.NazwaStawki == "oo")
				{
					FakturaKsef.Fa.P_13_6_1 += pozycja.WartoscNetto;
					FakturaKsef.Fa.P_13_6_1Specified = true;
					fa2Wiersz.P_12 = TStawkaPodatku.oo;
				}
				else if (pozycja.Vat.NazwaStawki == "zw")
				{
					FakturaKsef.Fa.P_13_7 += pozycja.WartoscNetto;
					FakturaKsef.Fa.P_13_7Specified = true;
					fa2Wiersz.P_12 = TStawkaPodatku.zw;
				}
				else if (pozycja.Vat.WysokośćPodatku == 0)
				{
					FakturaKsef.Fa.P_13_6_1 += pozycja.WartoscNetto;
					FakturaKsef.Fa.P_13_6_1Specified = true;
					fa2Wiersz.P_12 = TStawkaPodatku.Item0;
				}
				else if (pozycja.Vat.WysokośćPodatku == 3)
				{
					FakturaKsef.Fa.P_13_6_1 += pozycja.WartoscNetto;
					FakturaKsef.Fa.P_13_6_1Specified = true;
					fa2Wiersz.P_12 = TStawkaPodatku.Item3;
				}
				else if (pozycja.Vat.WysokośćPodatku == 4)
				{
					FakturaKsef.Fa.P_13_6_1 += pozycja.WartoscNetto;
					FakturaKsef.Fa.P_13_6_1Specified = true;
					fa2Wiersz.P_12 = TStawkaPodatku.Item4;
				}
				else if (pozycja.Vat.WysokośćPodatku == 5)
				{
					FakturaKsef.Fa.P_13_3 += pozycja.WartoscNetto;
					FakturaKsef.Fa.P_14_3 += pozycja.WartoscVat;
					fa2Wiersz.P_12 = TStawkaPodatku.Item5;
				}
				else if (pozycja.Vat.WysokośćPodatku == 7)
				{
					FakturaKsef.Fa.P_13_6_1 += pozycja.WartoscNetto;
					FakturaKsef.Fa.P_13_6_1Specified = true;
					fa2Wiersz.P_12 = TStawkaPodatku.Item7;
				}
				else if (pozycja.Vat.WysokośćPodatku == 8)
				{
					FakturaKsef.Fa.P_13_2 += pozycja.WartoscNetto;
					FakturaKsef.Fa.P_14_2 += pozycja.WartoscVat;
                    fa2Wiersz.P_12 = TStawkaPodatku.Item8;
				}
				else if (pozycja.Vat.WysokośćPodatku == 22)
				{
					FakturaKsef.Fa.P_13_1 += pozycja.WartoscNetto;
					FakturaKsef.Fa.P_14_1 += pozycja.WartoscVat;
					fa2Wiersz.P_12 = TStawkaPodatku.Item22;
				}
				else
				{
					FakturaKsef.Fa.P_13_1 += pozycja.WartoscNetto;
					FakturaKsef.Fa.P_14_1 += pozycja.WartoscVat;
					fa2Wiersz.P_12 = TStawkaPodatku.Item23;
				}
				fa2Wiersz.P_12Specified = true;

				if (pozycja.IsKor)
				{
					fa2Wiersz.StanPrzed = 1; fa2Wiersz.StanPrzedSpecified = true;
				}
                FakturaKsef.Fa.P_15 += pozycja.WartoscBrutto;
                faWiersze.Add(fa2Wiersz);
			}
			
			FakturaKsef.Fa.FaWiersz = faWiersze.ToArray();
            FakturaKsef.Stopka = new FakturaStopka();
			
			string FakturaKsefXML = await CreateReadyXml(FakturaKsef);
			
            try
            {
				Faktura = await XmlCreationService.SendInvoiceToKsef(FakturaKsefXML, Faktura, entities);
				Faktura.XMLFakturyKSeF = FakturaKsefXML;

                return Faktura;
            }
            catch (Exception ex)
			{
				throw new ApplicationException("Błąd podczas wysyłania faktury do KSeF w XmlCreatinService", ex);
			} 
		}

		public async Task<string> CreateReadyXml(Faktura FakturaKsef)
		{
			// Definicje przestrzeni nazw
			string ns = "http://crd.gov.pl/wzor/2023/06/29/12648/";
			string xsi = "http://www.w3.org/2001/XMLSchema-instance";
			string etd = "http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2022/01/05/eD/DefinicjeTypy/";

			var xmlAttributeOverride = new XmlAttributeOverrides();
			xmlAttributeOverride.Add(typeof(FakturaFA), "P_15ZK", new XmlAttributes() { XmlIgnore = true });
			var xmlSeriliser = new XmlSerializer(typeof(Faktura), xmlAttributeOverride);

			var xmlNamespaces = new XmlSerializerNamespaces();
			xmlNamespaces.Add("xsi", xsi);       // Przestrzeń nazw dla XML Schema instance
			xmlNamespaces.Add("etd", etd);       // Przestrzeń nazw dla definicji typów
			xmlNamespaces.Add(string.Empty, ns); // Główna przestrzeń nazw (domyślna)

			var xml = new StringBuilder();
			using var xmlWriter = XmlWriter.Create(xml, new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = true });
			xmlSeriliser.Serialize(xmlWriter, FakturaKsef, xmlNamespaces);

			return xml.ToString();
		}

		public static async Task<BaseFaktura> SendInvoiceToKsef(string FakturaKsef, BaseFaktura faktura, MyBusinessEntities entities)
		{
			try
			{
				using var api = new KsefApiService();
				var cts = new CancellationTokenSource();
				await api.AuthenticateAsync(entities.Nip, entities.TokenKSeF!);
				faktura.XMLFakturyKSeF = FakturaKsef;
				(faktura.NrFakturyKSeF, faktura.DataFakturyKSeF, faktura.URLFakturyKSeF, faktura.NumerReferencyjnyKSeF, faktura.StatusKSeF) = await api.SendInvoiceAsync(FakturaKsef, cts.Token);

				await api.Terminate();
				return faktura;
			}
			catch (Exception ex)
			{
                await Application.Current.MainPage.DisplayAlert("Wystąpił błąd w fukcj SendInvoiceToKsef", ex.Message, "OK");
				return default!;
            }
		}
	}
}

