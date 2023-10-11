using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleStore.Data;
using SaleStore.DataAccess.Repository.IRepository;
using SaleStore.Model;
using SaleStore.Utility;

namespace SaleStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetail.Role_Admin)]// so users wont be able to see the unauthorized pages by simply writing their URLs
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; // Example dependecy Injection
        }

        //[Authorize(Roles = StaticDetail.Role_Admin)] // can also be applied individually
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.CategoryRepository.GetAll().ToList(); // This should be in a service. This looks disgusting
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
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.Save();
                TempData["Success"] = "Category created successfully"; // To show error messages
            }
            //return RedirectToAction("Index"/*"Category"*/); // if it was in different Controller, you also pass the controller name as well
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound(); // We can also use any error view
            }
            //Different ways to retrive data from database
            Category? category = _unitOfWork.CategoryRepository.GetFirstOrDefault(u => u.ID == id); // fastest in this case
            //Category? category1 = _db.Categories.FirstOrDefault(category => category.ID == id);
            //Category? category2 = _db.Categories.Where(category => category.ID == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Update(category);
                _unitOfWork.Save();
                TempData["Success"] = "Category edited successfully";
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.CategoryRepository.GetFirstOrDefault(u => u.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")] // can't name get and post actions same when they get same type of parameters. So we add action name property
        public IActionResult DeletePOST(int? id)
        {
            Category? category = _unitOfWork.CategoryRepository.GetFirstOrDefault(u => u.ID == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.CategoryRepository.Remove(category);
            _unitOfWork.Save();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
