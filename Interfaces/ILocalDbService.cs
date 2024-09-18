using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KseF.Models;

namespace KseF.Interfaces
{
	public interface ILocalDbService
	{
		Task<SQLiteAsyncConnection> GetDbConnection();
		Task<string> GetDbName();

		Task<List<T>> GetItemsAsync<T>() where T : DbRecord,  new();
		Task SaveItemAsync<T>(T item) where T : DbRecord, new();
		Task<T> GetItemAsyncById<T>(Guid id) where T : DbRecord, new();
		Task<int> DeleteItemAsync<T>(T item) where T : DbRecord, new();
		Task<MyBusinessEntities> SetBusinessEntityToContext(MyBusinessEntities myBusinessEntitity);
		Task<MyBusinessEntities> GetBusinessEntityFromContext();
	}
}
