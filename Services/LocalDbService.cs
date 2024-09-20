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

		public async Task<List<T>> GetItemsAsync<T>() where T :DbRecord,  new()
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
	}
}
