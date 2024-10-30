using KseF.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KseF.Interfaces;
using KseF.Models.Invoice_FA_2;
using Models;
using CommunityToolkit.Mvvm.Messaging;
using KseF.Models.ViewModels;

namespace KseF.Services
{
	public class LocalDbService : ILocalDbService
	{
		private const string DB_NAME = "KseF.db3";
		private readonly SQLiteAsyncConnection _dbConnection;
		public MyBusinessEntities MyBusinessEntitityInContext { get; set; }

		public LocalDbService()
		{
			_dbConnection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
			_dbConnection.CreateTableAsync<MyBusinessEntities>();
			_dbConnection.CreateTableAsync<ClientEntities>();
			_dbConnection.CreateTableAsync<BaseFaktura>();
			_dbConnection.CreateTableAsync<Product>();
#if DEBUG
            AddTestData();
#endif
        }

        public async Task<string> GetDbName()
		{
			return DB_NAME;
		}

		public async Task<MyBusinessEntities> SetBusinessEntityToContext(MyBusinessEntities myBusinessEntitity)
		{
			MyBusinessEntitityInContext = myBusinessEntitity;

			return MyBusinessEntitityInContext;
		}

		public async Task<MyBusinessEntities> GetBusinessEntityFromContext()
		{
			return MyBusinessEntitityInContext;
		}

		public async Task<SQLiteAsyncConnection> GetDbConnection()
		{
			return _dbConnection;
		}

		public async Task<List<T>> GetItemsAsync<T>() where T : DbRecord, new()
		{
			return _dbConnection.Table<T>().ToListAsync().Result;
		}

		public async Task<T> GetItemAsyncById<T>(Guid id) where T : DbRecord, new()
		{
			return await _dbConnection.FindAsync<T>(id);
		}

		public async Task SaveItemAsync<T>(T item) where T : DbRecord, new()
		{
			await _dbConnection.InsertAsync(item);
		}

		public async Task EditItemAsync<T>(T item) where T : DbRecord, new()
		{
			await _dbConnection.UpdateAsync(item);
		}

		public async Task<int> DeleteItemAsync<T>(T item) where T : DbRecord, new()
		{
			return await _dbConnection.DeleteAsync(item);
		}

        public async Task<List<ClientEntities>> GetClientsByBusinessEntityIdAsync(Guid businessEntityId)
        {
            return await _dbConnection.Table<ClientEntities>()
                                      .Where(c => c.MyBusinessEntityId == businessEntityId)
                                      .ToListAsync();
        }


#if DEBUG

        public async Task AddTestData()
		{

            var MBECount = await _dbConnection.Table<MyBusinessEntities>().CountAsync();
            var ClientCount = await _dbConnection.Table<ClientEntities>().CountAsync();
            var ProductCount = await _dbConnection.Table<Product>().CountAsync();

			if (MBECount <= 0)
			{
                var myBusinessEntity1 = new MyBusinessEntities
                {
                    Id = Guid.NewGuid(),
                    NazwaSkrocona = "Test1",
                    NazwaPelna = "Test1",
                    Nip = "9513128170",
                    Ulica = "Test",
                    NrDomu = "1",
                    KodPocztowy = "00-000",
                    Miejscowosc = "Test",
                    AdresSiedziby = "Test",
                    AdresKorespondencyjny = "Test",
                    NrRachunku = "1234567890",
                    NrTelefonu = "123456789",
                    AdresEmail = "",
                    Notatki = "Test",
                    Regon = "123456789",
                    Krs = "123456789",
                    Bdo = "123456789",
                    IsDrukujStopke = true,
                    KodUS = "123456789",
                    ImieOsFiz = "Test",
                    NazwiskoOsFiz = "Test",
                    DataUrodzeniaOF = DateTime.Now,
                    FormaOpodatkowania = EnumLibrary.FormaOpodatkowania.ZasadyOgole,
                    StopkaFaktury = "Test",
                };

                var myBusinessEntity2 = new MyBusinessEntities
                {
                    Id = Guid.NewGuid(),
                    NazwaSkrocona = "Test2",
                    NazwaPelna = "Test2",
                    Nip = "9513128170",
                    Ulica = "Test",
                    NrDomu = "1",
                    KodPocztowy = "00-000",
                    Miejscowosc = "Test",
                    AdresSiedziby = "Test",
                    AdresKorespondencyjny = "Test",
                    NrRachunku = "1234567890",
                    NrTelefonu = "123456789",
                    AdresEmail = "",
                    Notatki = "Test",
                    Regon = "123456789",
                    Krs = "123456789",
                    Bdo = "123456789",
                    IsDrukujStopke = true,
                    KodUS = "123456789",
                    ImieOsFiz = "Test",
                    NazwiskoOsFiz = "Test",
                    DataUrodzeniaOF = DateTime.Now,
                    FormaOpodatkowania = EnumLibrary.FormaOpodatkowania.PodatekLiniowy,
                    StopkaFaktury = "Test",
                };
                WeakReferenceMessenger.Default.Send(new MessageSender<MyBusinessEntities>(myBusinessEntity1));
                WeakReferenceMessenger.Default.Send(new MessageSender<MyBusinessEntities>(myBusinessEntity2));
                await SaveItemAsync<MyBusinessEntities>(myBusinessEntity1);
                await SaveItemAsync<MyBusinessEntities>(myBusinessEntity2);
                if (ClientCount <= 0)
                {
                    var testClient1 = new ClientEntities
                    {
                        MyBusinessEntityId = myBusinessEntity1.Id,
                        IsPodmiot = true,
                        NazwaPelna = "Test1",
                        NazwaSkrocona = "Test1",
                        NrKlienta = "5278733163",
                        Imie = "Test",
                        Nazwisko = "Test",
                        Nip = "1234567890",
                        Ulica = "Test",
                        NrDomu = "1",
                        KodPocztowy = "00-000",
                        Miejscowosc = "Test",
                        AdresSiedziby = "Test",
                        AdresKorespondencyjny = "Test",
                        NrRachunku = "1234567890",
                        NrTelefonu = "123456789",
                        AdresEmail = "",
                        Notatki = "Test",
                    };
                    var testClient2 = new ClientEntities
                    {
                        MyBusinessEntityId = myBusinessEntity1.Id,
                        IsPodmiot = true,
                        NazwaPelna = "Test2",
                        NazwaSkrocona = "Test2",
                        NrKlienta = "5278733163",
                        Imie = "Test",
                        Nazwisko = "Test",
                        Nip = "1234567890",
                        Ulica = "Test",
                        NrDomu = "1",
                        KodPocztowy = "00-000",
                        Miejscowosc = "Test",
                        AdresSiedziby = "Test",
                        AdresKorespondencyjny = "Test",
                        NrRachunku = "1234567890",
                        NrTelefonu = "123456789",
                        AdresEmail = "",
                        Notatki = "Test",
                    };
                    var testClient3 = new ClientEntities
                    {
                        MyBusinessEntityId = myBusinessEntity2.Id,
                        IsPodmiot = true,
                        NazwaPelna = "Test3",
                        NazwaSkrocona = "Test3",
                        NrKlienta = "5278733163",
                        Imie = "Test",
                        Nazwisko = "Test",
                        Nip = "1234567890",
                        Ulica = "Test",
                        NrDomu = "1",
                        KodPocztowy = "00-000",
                        Miejscowosc = "Test",
                        AdresSiedziby = "Test",
                        AdresKorespondencyjny = "Test",
                        NrRachunku = "1234567890",
                        NrTelefonu = "123456789",
                        AdresEmail = "",
                        Notatki = "Test",
                    };

                    await SaveItemAsync<ClientEntities>(testClient1);
                    await SaveItemAsync<ClientEntities>(testClient2);
                    await SaveItemAsync<ClientEntities>(testClient3);
                }
            }
            
            if(ProductCount <= 0)
            {
                var testProduct = new Product
                {
                    Nazwa = "Test",
                    Opis = "Test",
                    Cena = 1,
                    Kategoria = "Test",
                    JednostkaMiary = EnumLibrary.JednostkaMiary.Szt,
                    RodzajPozycji = EnumLibrary.RodzajPozycji.Towar,
                    StawkaPodatku = EnumLibrary.StawkiPodatkuPL.Item23,
                    GTU = 0
                };
                await SaveItemAsync<Product>(testProduct);
            }
#endif
        }
	}
}
