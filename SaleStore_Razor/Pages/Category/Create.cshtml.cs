using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaleStore_Razor.Data;
using SaleStore_Razor.Models;

namespace SaleStore_Razor.Pages.Category
{
    [BindProperties] // so we can bind stuff from Db
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Models.Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(Category);
                _db.SaveChanges();
                TempData["Success"] = "Category created succesfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
