using Microsoft.AspNetCore.Mvc;
using Client.Models;
using System.Threading.Tasks;
using Client.Services;
using System.Linq;
using Bogus;

namespace Client.Controllers
{
    public class TablesController : Controller
    {
        private readonly StorageTableService storageTableService;

        public TablesController(StorageTableService storageTableService)
        {
            this.storageTableService = storageTableService;
        }

        public async Task<IActionResult> Insert()
        {
            var categories = Enumerable.Range(0, 10).Select(x =>
            {
                var model = CategoryMother.Create();
                var entity = CategoryEntity.Create(model.Name);
                return entity;
            });
            await storageTableService.Create(categories);

            var products = Enumerable.Range(0, 10).Select(x =>
            {
                var model = ProductMother.Create();
                var entity = ProductEntity.Create(model.Name, model.Price, model.Quantity);
                return entity;
            });

            await storageTableService.Create(products);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProductIndex()
        {
            var entities = await storageTableService.GetAllProducts();
            var result = entities.Select(x => ProductModel.Map(x));
            return View(result);
        }

        public async Task<IActionResult> CategoryIndex()
        {
            var entities = await storageTableService.GetAllCategories();
            var result = entities.Select(x => CategoryModel.Map(x));
            return View(result);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductModel model)
        {
            var entity = ProductEntity.Create(model.Name, model.Price, model.Quantity);
            await storageTableService.Create(entity);
            ViewBag.Message = "Product was created correctly";
            return RedirectToAction(nameof(ProductIndex));
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryModel model)
        {
            var entity = CategoryEntity.Create(model.Name);
            await storageTableService.Create(entity);
            ViewBag.Message = "Category was created correctly";
            return RedirectToAction(nameof(CategoryIndex));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var entity = await storageTableService.GetProduct(id);
            if (entity is null)
            {
                return NotFound();
            }
            var model = ProductModel.Map(entity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(ProductModel model)
        {
            var searched = await storageTableService.GetProduct(model.Id);
            if (searched is null)
            {
                return NotFound();
            }

            var entity = ProductEntity.Load(model);
            await storageTableService.Create(entity);
            ViewBag.Message = "Product was updated correctly";
            return RedirectToAction(nameof(ProductIndex));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            var entity = await storageTableService.GetCategory(id);
            if (entity is null)
            {
                return NotFound();
            }
            var model = CategoryModel.Map(entity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(CategoryModel model)
        {
            var searched = await storageTableService.GetCategory(model.Id);
            if (searched is null)
            {
                return NotFound();
            }

            var entity = CategoryEntity.Load(model);
            await storageTableService.Create(entity);
            ViewBag.Message = "Category was udpated correctly";
            return RedirectToAction(nameof(CategoryIndex));
        }


        [HttpGet]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var entity = await storageTableService.GetProduct(id);
            if (entity is null)
            {
                return NotFound();
            }

            var model = ProductModel.Map(entity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductConfirmed(string id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var entity = await storageTableService.GetProduct(id);
            if (entity is null)
            {
                return NotFound();
            }

            await storageTableService.Delete(entity);

            return RedirectToAction(nameof(ProductIndex));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var entity = await storageTableService.GetCategory(id);
            if (entity is null)
            {
                return NotFound();
            }

            var model = CategoryModel.Map(entity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoryConfirmed(string id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var entity = await storageTableService.GetCategory(id);
            if (entity is null)
            {
                return NotFound();
            }

            await storageTableService.Delete(entity);

            return RedirectToAction(nameof(CategoryIndex));
        }
    }
}
