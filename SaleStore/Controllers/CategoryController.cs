using Microsoft.AspNetCore.Mvc;
using SaleStore.Data;
using SaleStore.Models;

namespace SaleStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List <Category> categories = _db.Categories.ToList(); // This should be in a service. This looks disgusting
            return View(categories);
        }

        public IActionResult Create() // View name and action name must match
        {
            return View(); // If you define the object in the view there is no need to give any object as parameter.
        }
    }
}
