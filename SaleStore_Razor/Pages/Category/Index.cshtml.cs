using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaleStore_Razor.Data;
using SaleStore_Razor.Models;

namespace SaleStore_Razor.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Models.Category> CategoryList { get; set; } // Folder and class name is same so we need to be implicit

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet() // On[X] format is used to determine whichever action type the method going to perform
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}
