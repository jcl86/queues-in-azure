using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Services
{
    public class StorageTableService
    {
        public const string TableName = "main";

        private readonly string connectionString;

        public StorageTableService(IConfiguration configuration)
        {
            connectionString = configuration.GetSection("AzureStorage:ConnectionString").Value;
        }

        private async Task<TableClient> RetrieveTable()
        {
            var serviceClient = new TableServiceClient(connectionString);

            await serviceClient.CreateTableIfNotExistsAsync(TableName);

            var tableClient =  serviceClient.GetTableClient(TableName);
            return tableClient;
        }

        public async Task<ProductEntity> GetProduct(string id)
        {
            var client = await RetrieveTable();
            var result = await client.GetEntityAsync<ProductEntity>(ProductEntity.Partition, id);
            return result;
        }

        public async Task<CategoryEntity> GetCategory(string id)
        {
            var client = await RetrieveTable();
            var result = await client.GetEntityAsync<CategoryEntity>(CategoryEntity.Partition, id);
            return result;
        }

        public async Task<IEnumerable<ProductEntity>> GetAllProducts()
        {
            var client = await RetrieveTable();
            var result = client.Query<ProductEntity>(filter: $"PartitionKey eq '{ProductEntity.Partition}'");
            return result;
        }

        public async Task<IEnumerable<CategoryEntity>> GetAllCategories()
        {
            var client = await RetrieveTable();
            var result = client.Query<CategoryEntity>(filter: $"PartitionKey eq '{CategoryEntity.Partition}'");
            return result;
        }

        public async Task Create<T>(T entity) where T : class, ITableEntity, new()
        {
            var client = await RetrieveTable();
            await client.UpsertEntityAsync(entity, TableUpdateMode.Replace);
        }

        public async Task Create<T>(IEnumerable<T> entities) where T : class, ITableEntity, new()
        {
            var client = await RetrieveTable();
            foreach (var entity in entities)
            {
                await client.UpsertEntityAsync(entity, TableUpdateMode.Replace);
            }
        }

        public async Task Delete<T>(T entity) where T : class, ITableEntity, new()
        {
            var client = await RetrieveTable();
            client.DeleteEntity(entity.PartitionKey, entity.RowKey);
        }
    }
}
