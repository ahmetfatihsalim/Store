using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SaleStore.DataAccess.Repository.IRepository;
using SaleStore.Model;
using SaleStore.Model.ViewModels;
using SaleStore.Utility;
using System.Data;

namespace SaleStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetail.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment; // to access ImageUrl root path

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork; // Example dependecy Injection
            _webHostEnvironment = webHostEnvironment;   
        }

        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.ProductRepository.GetAll(includeProperties:"Category").ToList(); // This should be in a service. This looks disgusting
            return View(products);
        }

        #region Create Update Actions Before Upsert
        //public IActionResult Create() // View name and action name must match
        //{
        //    IEnumerable<SelectListItem> CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(/*Projection*/dropdownItem => new SelectListItem
        //    {
        //        Text = dropdownItem.Name,
        //        Value = dropdownItem.ID.ToString()
        //    }); // for dropdowns

        //    #region Alternative structures to pass data from controller to view

        //    #region ViewBag
        //    /*
        //     * Passes data from controller to view but now vice versa
        //     * Ideal for situations which the temporary data is not in a model
        //     * Dynamically typecasted
        //     * It's a dynamic property added in C# 4.0
        //     * Any number of properties and values can be assigned to viewbag
        //     * It's life time only lasts during the current http request
        //     * It's values will be null in case of any redirection
        //     * Actually just a wrapper around viewdata
        //     */

        //    //ViewBag.CategoryList/*name can be anything*/ = CategoryList;
        //    #endregion
        //    #region ViewData
        //    /*
        //     * Passes data from controller to view but now vice versa
        //     * Ideal for situations which the temporary data is not in a model
        //     * Must be typecasted before use
        //     * It's life time only lasts during the current http request
        //     * It's values will be null in case of any redirection
        //     */

        //    //ViewData["CatergoryList"] = CategoryList;
        //    #endregion
        //    // Internally ViewBag inserts data to ViewData dictionary so key of ViewData and property of ViewBag must not match
        //    #region TempData
        //    /* Can be used to store data between two consecutive requests
        //     * Internally uses session to store the data. More like a short lived session
        //     * Must be typecasted before use
        //     * Can be used to store one time messages or error messages
        //     */
        //    #endregion

        //    #endregion

        //    #region ViewModel
        //    /* ViewModel is strongly typed to view
        //     * This type of views are called strongly typed views. There is a model which is specific for the view
        //     */
        //    ProductViewModel productViewModel = new ProductViewModel()
        //    {
        //        CategoryList = CategoryList,
        //        Product = new Product()
        //    };
        //    #endregion

        //    return View(productViewModel); // If you define the object in the view there is no need to give any object as parameter. Except if you repopulate the view which in this case we do
        //}

        //[HttpPost]
        //public IActionResult Create(ProductViewModel productViewModel) // REST or something
        //{
        //    if (ModelState.IsValid) // checking validations
        //    {
        //        _unitOfWork.ProductRepository.Add(productViewModel.Product);
        //        _unitOfWork.Save();
        //        TempData["Success"] = "Product created successfully"; // To show error messages
        //    }
        //    else // if dropdown value is not valid. Not needed in our case. We use ValidateNever. Also we need to repopulate the dropdown for another request after an error, so...
        //    {
        //        productViewModel.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(dropdownItem => new SelectListItem()
        //        {
        //            Text= dropdownItem.Name,
        //            Value = dropdownItem.ID.ToString()
        //        });
        //    }
        //    //return RedirectToAction("Index"/*"Product"*/); // if it was in different Controller, you also pass the controller name as well
        //    return View(productViewModel);
        //}

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound(); // We can also use any error view
        //    }
        //    //Different ways to retrive data from database
        //    Product? product = _unitOfWork.ProductRepository.GetFirstOrDefault(u => u.ID == id); // fastest in this case
        //    //Product? product1 = _db.Categories.FirstOrDefault(product => product.ID == id);
        //    //Product? product2 = _db.Categories.Where(product => product.ID == id).FirstOrDefault();
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.ProductRepository.Update(product);
        //        _unitOfWork.Save();
        //        TempData["Success"] = "Product edited successfully";
        //    }
        //    return View();
        //}
        #endregion

        public IActionResult Upsert(int? id) // Update and insert in single page/action
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(dropdownItem => new SelectListItem
            {
                Text = dropdownItem.Name,
                Value = dropdownItem.ID.ToString()
            });

            ProductViewModel productViewModel = new ProductViewModel()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            if (id == null || id == 0) // create
            {
                return View(productViewModel);
            }
            else // update
            {
                productViewModel.Product = _unitOfWork.ProductRepository.GetFirstOrDefault(product => product.ID == id);
                return View(productViewModel);
            }
        }

        [HttpPost] // TO-DO
        public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file /*for ImageUrl*/) 
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath; // to access ImageUrl root path
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + /*to preserve file's extention*/ Path.GetExtension(file.FileName);// rename the file to a random GUID
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productViewModel.Product.ImageUrl)) // update // checking if there is an already image for our product
                    {
                        // delete and replace the image
                        var oldImagePath = Path.Combine(wwwRootPath, productViewModel.Product.ImageUrl.TrimStart('\\') /*we trim the backslash because we are using path combine*/);

                        if (System.IO.File.Exists(oldImagePath)) // check if anything exists in that path
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create)) //save image to related location
                    {
                        file.CopyTo(fileStream);
                    }

                    productViewModel.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (productViewModel.Product.ID == 0)
                {
                    _unitOfWork.ProductRepository.Add(productViewModel.Product);
                }
                else
                {
                    _unitOfWork.ProductRepository.Update(productViewModel.Product);
                }
                _unitOfWork.Save();
                TempData["Success"] = "Product created successfully";
            }
            else
            {
                productViewModel.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(dropdownItem => new SelectListItem()
                {
                    Text = dropdownItem.Name,
                    Value = dropdownItem.ID.ToString()
                });
            }
            return View(productViewModel);
        }

        /*public IActionResult Delete(int? id)
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
        }*/

        #region APICalls

        public IActionResult GetAll() 
        {
            List<Product> products = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category").ToList(); // This should be in a service. This looks disgusting
            return Json(new { data = products });
        }

        public IActionResult Delete(int? id)
        {
            var product = _unitOfWork.ProductRepository.GetFirstOrDefault(product => product.ID == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.ProductRepository.Remove(product);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete is successfull" });
        }

        #endregion


    }
}
