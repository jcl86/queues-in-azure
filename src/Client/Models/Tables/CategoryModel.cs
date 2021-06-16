namespace Client.Models
{
    public class CategoryModel
    {
        public string Id { get; set; }
        public string Name { get; set; }


        public static CategoryModel Map(Services.CategoryEntity entity)
        {
            return new CategoryModel()
            {
                Id = entity.RowKey,
                Name = entity.Name,
            };
        }
    }
}
