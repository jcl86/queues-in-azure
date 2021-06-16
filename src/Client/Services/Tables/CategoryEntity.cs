using Azure;
using Azure.Data.Tables;
using System;

namespace Client.Services
{
    public class CategoryEntity : ITableEntity
    {
        public const string Partition = "category";

        public string Name { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public static CategoryEntity Create(string name)
        {
            return new CategoryEntity()
            {
                Name = name,
                PartitionKey = Partition,
                RowKey = Guid.NewGuid().ToString()
            };
        }

        public static CategoryEntity Load(Models.CategoryModel model)
        {
            return new CategoryEntity()
            {
                Name = model.Name,
                PartitionKey = Partition,
                RowKey = model.Id
            };
        }
    }
}
