using Microsoft.AspNetCore.Mvc;
using Northwind.EntityModels;
using Northwind.Mvc.Models;
using System.Diagnostics;

namespace Northwind.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindDatabaseContext db;


        public HomeController(ILogger<HomeController> logger, 
            NorthwindDatabaseContext injectedContext)
        {
            _logger = logger;
            db = injectedContext;
        }

        public IActionResult Index()
        {
            HomeIndexViewModel model = new(

                VisitorCount: Random.Shared.Next(1, 1001),
                Categories: db.Categories.ToList(),
                Products: db.Products.ToList()

            );

            return View(model);
        }

        public IActionResult ProductDetail(int? id) 
        {
            if(!id.HasValue) 
            {
                return BadRequest("You must pass a product ID in the route, " +
                    "for example, /Home/ProductDetail/21");
            }
            Product? model = db.Products.SingleOrDefault(p => p.ProductId == id);
            if(model is null) 
            {
                return NotFound($"ProductID{id} not found");
            }

            return View(model);
        }

        public IActionResult ModelBindning() 
        {
            return View(); //en sida med en formulär
        }

        [HttpPost]
        public IActionResult ModelBindning(Thing thing)
        {
            HomeModelBindningViewModel model = new(
                thing,
                HasErrors: !ModelState.IsValid,
                ValidationErrors: ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage)
            );

            return View(thing); // en sida som visar det anvndaren skickade
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Gene: Lägg till denna metod för att hantera kategori-sidan
        public IActionResult CategoryDetail(int? id)
        {
            //Gene: för att hämta relaterade produkter
            if (!id.HasValue)
            {
                return BadRequest("You must pass a category ID in the route, for example, /Home/CategoryDetail/1");
            }

            var category = db.Categories
                             .Where(c => c.CategoryId == id)
                             .Select(c => new Category
                             {
                                 CategoryId = c.CategoryId,
                                 CategoryName = c.CategoryName,
                                 Description = c.Description,
                                 Products = db.Products.Where(p => p.CategoryId == c.CategoryId).ToList()
                             })
                             .SingleOrDefault();

            if (category is null)
            {
                return NotFound($"Category ID {id} not found");
            }

            return View(category);
        }

    }
}
