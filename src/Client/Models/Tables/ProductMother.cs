using Client.Models;
using Bogus;

namespace Client.Controllers
{
    public static class ProductMother
    {
        public static CreateProductModel Create()
        {
            var faker = new Faker<CreateProductModel>()
                .StrictMode(false)
                .RuleFor(x => x.Name, f => f.Commerce.Product())
                .RuleFor(x => x.Price, f => (double)f.Finance.Amount())
                .RuleFor(x => x.Quantity, f => f.Random.Int(1, 15));

            return faker.Generate();
        }
    }
}
