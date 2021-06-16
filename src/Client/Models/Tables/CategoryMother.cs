using Client.Models;
using System.Linq;
using Bogus;

namespace Client.Controllers
{
    public static class CategoryMother
    {
        public static CategoryModel Create()
        {
            var faker = new Faker<CategoryModel>()
                .StrictMode(false)
                .RuleFor(x => x.Name, f => f.Commerce.Categories(1).First());

            return faker.Generate();
        }
    }
}
