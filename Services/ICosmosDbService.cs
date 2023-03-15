using CapitalPlacement.DTOs;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Linq.Expressions;
using System.Net;

namespace CapitalPlacement.Services
{
    public interface ICosmosDbService
    {
        Task<T> AddItemAsync<T>(T item) where T : class;
        Task<IEnumerable<T>> GetItemsAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> UpdateItemAsync<T>(string id, T item) where T : class;
    }
    public class CosmosDbService : ICosmosDbService
    {
        private readonly string _databaseId;
        private readonly string _containerId;
        private readonly string _connectionString;

        public CosmosDbService(string databaseId, string containerId, string connectionString)
        {
            _databaseId = databaseId;
            _containerId = containerId;
            _connectionString = connectionString;
        }

        public async Task<T> AddItemAsync<T>(T item) where T : class
        {
            using (var client = new CosmosClient(_connectionString))
            {
                var container = client.GetContainer(_databaseId, _containerId);
                var response = await container.CreateItemAsync(item);
                return response.Resource as T;
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var client = new CosmosClient(_connectionString))
            {
                var container = client.GetContainer(_databaseId, _containerId);
                var query = container.GetItemLinqQueryable<T>()
                    .Where(predicate)
                    .ToFeedIterator();

                var results = new List<T>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    results.AddRange(response.ToList());
                }

                return results;
            }
        }

        public async Task<T> UpdateItemAsync<T>(string id, T item) where T : class
        {
            using (var client = new CosmosClient(_connectionString))
            {
                var container = client.GetContainer(_databaseId, _containerId);

                try
                {
                    var existingItemResponse = await container.ReadItemAsync<T>(id, new PartitionKey(id));
                    var existingItem = existingItemResponse.Resource;

                    existingItem = item;

                    var response = await container.ReplaceItemAsync(existingItem, id, new PartitionKey(id));

                    return response.Resource as T;
                }
                catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    throw;
                }
            }
        }

    }
}