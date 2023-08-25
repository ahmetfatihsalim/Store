using Microsoft.AspNetCore.Mvc;

namespace SaleStore.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
