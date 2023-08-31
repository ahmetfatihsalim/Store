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

        [HttpPost]
        public IActionResult Create(Category category) // REST or something
        {
            if (category.Name == category.DisplayOrder.ToString()) // custom validations // these validations wont be hit for class property validations because those validations are handled by client-side thanks to consumed partial view
            {
                ModelState.AddModelError("Name", "Name and Display Order cannot be same"); // adding custom error check to given class property
            }
            if (category.Name != null && category.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "test is an invalid value");
            }
            if (ModelState.IsValid) // checking validations
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
            }
            //return RedirectToAction("Index"/*"Category"*/); // if it was in different Controller, you also pass the controller name as well
            return View();
        }
    }
}
