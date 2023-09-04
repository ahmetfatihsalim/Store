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

        public IActionResult OnPost(/*Models.Category category*/) // With bindeble property of Category classwe dont need to give another parameter to this function for it'll already be populated
        {
            _db.Categories.Add(/*category*/Category);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
