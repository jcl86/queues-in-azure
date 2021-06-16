using Azure;
using Azure.Data.Tables;
using System;

namespace Client.Services
{
    public class ProductEntity : ITableEntity
    {
        public const string Partition = "product";

        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public static ProductEntity Create(string name, double price, int quantity)
        {
            return new ProductEntity()
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                PartitionKey = Partition,
                RowKey = Guid.NewGuid().ToString()
            };
        }

        public static ProductEntity Load(Models.ProductModel model)
        {
            return new ProductEntity()
            {
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
                PartitionKey = Partition,
                RowKey = model.Id,
            };
        }
    }
}
