using Microsoft.AspNetCore.Mvc;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Overview()
        {
            var item = new Item() { Name = "Mouse" };
            return View(item);
        }

        public IActionResult Edit(int ItemId)
        {
            return Content("id 1 = " + ItemId);
        }
    }
}
