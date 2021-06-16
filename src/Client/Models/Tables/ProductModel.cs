namespace Client.Models
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public static ProductModel Map(Services.ProductEntity entity)
        {
            return new ProductModel()
            {
                Id = entity.RowKey,
                Name = entity.Name,
                Price = entity.Price,
                Quantity = entity.Quantity
            };
        }
    }
}
