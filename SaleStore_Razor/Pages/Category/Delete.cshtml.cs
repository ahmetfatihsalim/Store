using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaleStore_Razor.Data;

namespace SaleStore_Razor.Pages.Category
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Models.Category Category { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = _db.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            Models.Category? category = _db.Categories.Find(Category.ID);
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
