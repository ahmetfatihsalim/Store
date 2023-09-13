using Microsoft.AspNetCore.Mvc;
using SaleStore.DataAccess.Repository.IRepository;
using SaleStore.Model;

namespace SaleStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; // Example dependecy Injection
        }

        public IActionResult Index()
        {
            List<Product> categories = _unitOfWork.ProductRepository.GetAll().ToList(); // This should be in a service. This looks disgusting
            return View(categories);
        }

        public IActionResult Create() // View name and action name must match
        {
            return View(); // If you define the object in the view there is no need to give any object as parameter.
        }

        [HttpPost]
        public IActionResult Create(Product product) // REST or something
        {
            if (ModelState.IsValid) // checking validations
            {
                _unitOfWork.ProductRepository.Add(product);
                _unitOfWork.Save();
                TempData["Success"] = "Product created successfully"; // To show error messages
            }
            //return RedirectToAction("Index"/*"Product"*/); // if it was in different Controller, you also pass the controller name as well
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound(); // We can also use any error view
            }
            //Different ways to retrive data from database
            Product? product = _unitOfWork.ProductRepository.GetFirstOrDefault(u => u.ID == id); // fastest in this case
            //Product? product1 = _db.Categories.FirstOrDefault(product => product.ID == id);
            //Product? product2 = _db.Categories.Where(product => product.ID == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Update(product);
                _unitOfWork.Save();
                TempData["Success"] = "Product edited successfully";
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWork.ProductRepository.GetFirstOrDefault(u => u.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")] // can't name get and post actions same when they get same type of parameters. So we add action name property
        public IActionResult DeletePOST(int? id)
        {
            Product? product = _unitOfWork.ProductRepository.GetFirstOrDefault(u => u.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductRepository.Remove(product);
            _unitOfWork.Save();
            TempData["Success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
